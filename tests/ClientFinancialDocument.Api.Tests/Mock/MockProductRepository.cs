using ClientFinancialDocument.Domain.Common;
using ClientFinancialDocument.Domain.Products;
using Moq;

namespace ClientFinancialDocument.Api.Tests.Mock
{
    public static class MockProductRepository
    {
        public static Mock<IProductRepository> GetMockProductRepository()
        {
            Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();

            _productRepositoryMock.Setup(p => p.GetProductAsync(/*ProductCode.ProductA.ToString()*/It.IsAny<string>(), default))
                .ReturnsAsync(new Product { Id = 1, ProductCode = ProductCode.ProductA.ToString() });

            return _productRepositoryMock;
        }
    }
}
