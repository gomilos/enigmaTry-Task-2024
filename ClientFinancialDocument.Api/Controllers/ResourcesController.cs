using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Products;
using ClientFinancialDocument.Domain.Tenants;
using Microsoft.AspNetCore.Mvc;

namespace ClientFinancialDocument.Api.Controllers
{
    [Route("api/resources")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;

        public ResourcesController(ITenantRepository tenantRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _tenantRepository = tenantRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        [HttpGet("notSupportedProducts")]
        public async Task<IActionResult> GetNotSupportedProducts(CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetNotSupportedProductAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("supportedProducts")]
        public async Task<IActionResult> GetSupportedProducts(CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetSupportedProductsAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("whitelistedTenants")]
        public async Task<IActionResult> GetWhitelistedTenants(CancellationToken cancellationToken)
        {
            var result = await _tenantRepository.GetWhitelistedTenantsAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("notWhitelistedTenants")]
        public async Task<IActionResult> GetNotWhitelistedTenants(CancellationToken cancellationToken)
        {
            var result = await _tenantRepository.GetNotWhitelistedTenantsAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("whitelistedClientsPerTenant")]
        public async Task<IActionResult> GetWhitelistedClientsPerTenant(CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetWhitelistedClientsPerTenantAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("notWhitelistedClientsPerTenant")]
        public async Task<IActionResult> GetNotWhitelistedClientsPerTenant(CancellationToken cancellationToken)
        {
            var result = await _clientRepository.GetNotWhitelistedClientsPerTenantAsync(cancellationToken);
            return Ok(result);
        }
    }
}
