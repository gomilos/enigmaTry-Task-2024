using ClientFinancialDocument.Application.Clients.Query;
using ClientFinancialDocument.Application.Products.Query;
using ClientFinancialDocument.Application.Tenants.Query;
using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Tenants;
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

        public FinancialDocumentController(ILogger<FinancialDocumentController> logger,
            ISender sender,
            ITenantRepository tenantRepository,
            IClientRepository clientRepository,
            IClientService clientService)
        {
            _logger = logger;
            _sender = sender;
            _clientService = clientService;
        }

        [HttpGet]
        //[ProducesResponseType<Product>(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Get(string productCode, Guid tenantId, Guid documentId, CancellationToken cancellationToken)
        {
            //if (String.IsNullOrEmpty(productCode))
            //{
            //    throw new ArgumentNullException(nameof(productCode));
            //}

            //if (tenantId == Guid.Empty)
            //{
            //    throw new ArgumentNullException(nameof(tenantId));
            //}

            //if (documentId == Guid.Empty)
            //{
            //    throw new ArgumentNullException(nameof(documentId));
            //}

            var product = await _sender.Send(new GetProductQuery(productCode), cancellationToken);
            var tenant = await _sender.Send(new GetTenantQuery(tenantId), cancellationToken);
            var client = await _sender.Send(new GetClientQuery(tenantId, documentId), cancellationToken);

            if (client.IsFailure)
            {
                //return BadRequest(client.Error);
                return NotFound(client.Error);//($"Client for TenatId:{tenant.ToString()} and DocumentId:{documentId.ToString()} not found");
            }

            var isClientPerTenantWhitelisted = await _clientService.IsClientPerTenantWhitelisted(tenantId, client.Value.ClientId);
            // FIXME: check with EnigmaTry should we return 400 status???
            if (isClientPerTenantWhitelisted.IsFailure)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status403Forbidden,
                    Type = "ValidationFailure",
                    Title = "Validation error",
                    Detail = isClientPerTenantWhitelisted.Error.Name
                };
                var or = new ObjectResult(problemDetails);
                return or;
                //return problemDetails;
                //return BadRequest(isClientPerTenantWhitelisted.Error);
            }

            return Ok(true);
        }
    }
}
