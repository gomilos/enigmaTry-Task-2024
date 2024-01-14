using ClientFinancialDocument.Api.Extensions;
using ClientFinancialDocument.Application.Clients.Query;
using ClientFinancialDocument.Application.Products.Query;
using ClientFinancialDocument.Application.Tenants.Query;
using ClientFinancialDocument.Domain.Abstraction;
using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Common;
using ClientFinancialDocument.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClientFinancialDocument.Api.Controllers
{
    [Route("api/financialDocument")]
    [ApiController]
    public class FinancialDocumentController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<FinancialDocumentController> _logger;
        private readonly IClientService _clientService;
        private readonly IFinancialDocumentsService _financialDocumentsService;
        private readonly IHandleFinancialDocumentServise _handleFinancialDocumentServise;

        public FinancialDocumentController(ILogger<FinancialDocumentController> logger,
            ISender sender,
            IClientService clientService,
            IFinancialDocumentsService financialDocumentsService,
            IHandleFinancialDocumentServise handleFinancialDocumentServise)
        {
            _logger = logger;
            _sender = sender;
            _clientService = clientService;
            _financialDocumentsService = financialDocumentsService;
            _handleFinancialDocumentServise = handleFinancialDocumentServise;
        }

        [HttpGet]
        public async Task<IResult> Get(string productCode, Guid tenantId, Guid documentId, CancellationToken cancellationToken)
        {
            Guard.IsNotNulOrWhiteSpace(productCode, nameof(productCode));
            Guard.IsEmptyGuid(tenantId, nameof(tenantId));
            Guard.IsEmptyGuid(documentId, nameof(documentId));

            var product = await _sender.Send(new GetProductQuery(productCode), cancellationToken);
            if (product.IsFailure)
            {
                return product.ToProblemDetails();
            }

            var tenant = await _sender.Send(new GetTenantWhitelistedQuery(tenantId), cancellationToken);
            if (tenant.IsFailure)
            {
                return tenant.ToProblemDetails();
            }

            var client = await _sender.Send(new GetClientQuery(tenantId, documentId), cancellationToken);
            if (client.IsFailure)
            {
                return client.ToProblemDetails();
            }

            var isClientPerTenantWhitelisted = await _clientService.IsClientPerTenantWhitelisted(tenantId, client.Value.ClientId);
            if (isClientPerTenantWhitelisted.IsFailure)
            {
                return isClientPerTenantWhitelisted.ToProblemDetails();
            }

            var clientAdditionalInformation = await _sender.Send(new GetClientAdditionalInformationQuery(client.Value.ClientVAT));
            if (clientAdditionalInformation.IsFailure)
            {
                return clientAdditionalInformation.ToProblemDetails();
            }

            var finacialDocument = await _financialDocumentsService.GetFinancialDocumentJsonString(tenantId, documentId, cancellationToken);
            var json = _handleFinancialDocumentServise.ModifyFinancialDocument(finacialDocument, 1000, CompanyType.Large,
                (ProductCode)Enum.Parse(typeof(ProductCode), productCode));
            if (json.IsFailure)
            {
                return json.ToProblemDetails();
            }

            return Results.Ok(json.Value);
        }
    }
}
