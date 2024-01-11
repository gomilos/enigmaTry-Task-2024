using ClientFinancialDocument.Application.Products.Query;
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
        private readonly ITenantRepository _tenantRepository;
        private readonly ILogger<FinancialDocumentController> _logger;

        public FinancialDocumentController(ILogger<FinancialDocumentController> logger, ISender sender, ITenantRepository tenantRepository)
        {
            _logger = logger;
            _sender = sender;
            _tenantRepository = tenantRepository;
        }

        [HttpGet]
        //[ProducesResponseType<Product>(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Get(string productCode, Guid tenantId, Guid documentId)
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

            var product = await _sender.Send(new GetProductQuery(productCode));

            return Ok(true);
        }

        [HttpGet("getAllTenants")]
        public async Task<IActionResult> GetAllTenats()
        {
            var result = await _tenantRepository.GetAllWhitelistedTenantsAsync();
            return Ok(result);
        }
    }
}
