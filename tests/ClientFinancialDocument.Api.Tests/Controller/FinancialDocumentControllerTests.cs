using ClientFinancialDocument.Api.Controllers;
using ClientFinancialDocument.Application.Clients.Query;
using ClientFinancialDocument.Application.Products.Query;
using ClientFinancialDocument.Application.Tenants.Query;
using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Common;
using ClientFinancialDocument.Domain.FinancialDocuments;
using ClientFinancialDocument.Domain.Products;
using ClientFinancialDocument.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using ClientFinancialDocument.Api.Tests.Mock;
using Microsoft.AspNetCore.Http;

namespace ClientFinancialDocument.Api.Tests.Controller
{
    public class FinancialDocumentControllerTests
    {
        private readonly Mock<ISender> _senderMock;
        private readonly Mock<ILogger<FinancialDocumentController>> _loggerMock;
        private readonly Mock<IClientService> _clientServiceMock;
        private readonly Mock<IFinancialDocumentsService> _financialDocumentsServiceMock;
        private readonly Mock<IHandleFinancialDocumentService> _handleFinancialDocumentServiceMock;
        private readonly FinancialDocumentController _controller;

        //public FinancialDocumentControllerTests(ISender sender, ILogger<FinancialDocumentControllerTests> logger, IClientService clientService, IFinancialDocumentsService financialDocumentsService, IHandleFinancialDocumentServise handleFinancialDocumentServise)
        public FinancialDocumentControllerTests()
        {
            _senderMock = new Mock<ISender>();
            _loggerMock = new Mock<ILogger<FinancialDocumentController>>();
            _clientServiceMock = new Mock<IClientService>();
            _financialDocumentsServiceMock = new Mock<IFinancialDocumentsService>();
            _handleFinancialDocumentServiceMock = new Mock<IHandleFinancialDocumentService>();
            _controller = new FinancialDocumentController(_loggerMock.Object, _senderMock.Object, _clientServiceMock.Object,
                _financialDocumentsServiceMock.Object, _handleFinancialDocumentServiceMock.Object);
        }

        [Fact]
        public async Task Get_InvalidProductCodeInput_ThrowArgumentException()
        {
            // arrange

            var productCode = "";
            var tenantId = new Guid();
            var documentId = new Guid();

            // act and assert
            // 
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _controller.Get(productCode, tenantId, documentId, default));
        }

        [Fact]
        public async Task Get_InvalidTenantIdInput_ShouldThrowArgumentNullException()
        {
            // arrange

            var productCode = "ProductA";
            var tenantId = new Guid();
            var documentId = new Guid();

            // act and assert
            // 
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _controller.Get(productCode, tenantId, documentId, default));
        }

        [Fact]
        public async Task Get_InvalidDocumentIdInput_ThrowArgumentNullException()
        {
            // arrange

            var productCode = "ProductA";
            var tenantId = Guid.NewGuid();
            var documentId = new Guid();

            // act and assert
            // 
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await _controller.Get(productCode, tenantId, documentId, default));
        }

        [Fact]
        public async Task Get_ProductNotFound_ReturnResultErrorAnd404()
        {
            // arrange

            var productCode = "ProductD";
            var tenantId = Guid.NewGuid();
            var documentId = Guid.NewGuid();
            var productObj = new Product { Id = 1, ProductCode = productCode };

            _senderMock.Setup(m => m.Send(It.IsAny<GetProductQuery>(), default))
                .ReturnsAsync(Result.Failure<ProductResponse>(ProductErrors.NotFound));

            // act

            var result = await _controller.Get(productCode, tenantId, documentId, default) as ProblemHttpResult;

            // assert

            _senderMock.Verify(x => x.Send(It.IsAny<GetProductQuery>(), default), Times.Once);

            var errorsObj = result!.ProblemDetails.Extensions["errors"];
            if (errorsObj != null && errorsObj is Error[] errors)
            {
                Assert.Single(errors);
                Assert.Equal(ErrorType.NotFound, errors[0].ErrorType);
            }

            Assert.Equal(StatusCodes.Status404NotFound, result.ProblemDetails.Status);
        }

        [Fact]
        public async Task Get_ProductNotSupported_ReturnResultErrorAnd403()
        {
            // arrange

            var productCode = "ProductC";
            var tenantId = Guid.NewGuid();
            var documentId = Guid.NewGuid();

            _senderMock.Setup(m => m.Send(It.IsAny<GetProductQuery>(), default))
                .ReturnsAsync(Result.Failure<ProductResponse>(ProductErrors.NotSupportedProduct));

            // act

            var result = await _controller.Get(productCode, tenantId, documentId, default) as ProblemHttpResult;

            // assert

            _senderMock.Verify(x => x.Send(It.IsAny<GetProductQuery>(), default), Times.Once);

            var errorsObj = result!.ProblemDetails.Extensions["errors"];
            if (errorsObj != null && errorsObj is Error[] errors)
            {
                Assert.Single(errors);
                Assert.Equal(ErrorType.Forbiden, errors[0].ErrorType);
            }

            Assert.Equal(StatusCodes.Status403Forbidden, result.ProblemDetails.Status);
        }

        // FIXME: we can add other Controllers' test in use casses for other Request Handlers like Tenat, Client, service for WhiteListed CLient, Client Additional data

        [Fact]
        public async Task Get_ValidInputData_ReturnResultOk()
        {
            // arrange

            var productCode = "ProductA";
            var tenantId = Guid.NewGuid();
            var documentId = Guid.NewGuid();

            _senderMock.Setup(m => m.Send(It.IsAny<GetProductQuery>(), default))
                .ReturnsAsync(Result.Success(It.IsAny<ProductResponse>()));

            _senderMock.Setup(m => m.Send(It.IsAny<GetTenantWhitelistedQuery>(), default))
                .ReturnsAsync(Result.Success(It.IsAny<bool>()));

            var clientResponseMock = new ClientResponse(Guid.NewGuid(), Guid.NewGuid());
            _senderMock.Setup(m => m.Send(It.IsAny<GetClientQuery>(), default))
                .ReturnsAsync(Result.Success(clientResponseMock));

            _clientServiceMock.Setup(cs => cs.IsClientPerTenantWhitelisted(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .ReturnsAsync(Result.Success());

            _senderMock.Setup(m => m.Send(It.IsAny<GetClientAdditionalInformationQuery>(), default))
                .ReturnsAsync(Result.Success(It.IsAny<ClientAdditionalInformationResponse>()));

            _financialDocumentsServiceMock.Setup(fd => fd.GetFinancialDocumentJsonString(tenantId, documentId, default))
                .ReturnsAsync(MockFinancialDocument.FDForProductA);//It.IsAny<string>

            _handleFinancialDocumentServiceMock.Setup(hfd => hfd.ModifyFinancialDocument(It.IsAny<string>(), It.IsAny<int>(),
                It.IsAny<CompanyType>(), It.IsAny<ProductCode>()))
                .Returns(MockFinancialDocument.FDForProductAAnonimizedAndExtended);//It.IsAny<Result<dynamic>>

            //var handlerMock = new Mock<GetProductQueryHandler>();
            //handlerMock.Setup(h => h.Handle(new GetProductQuery(productCode), default))
            //.ReturnsAsync(It.IsAny<Result<ProductResponse>>());

            //Mock<IProductRepository> _productRepositoryMock = new();
            //_productRepositoryMock.Setup(p => p.GetProductAsync(productCode, default))
            //    .ReturnsAsync(productObj);

            // act

            var result = await _controller.Get(productCode, tenantId, documentId, default) as Ok<string>;

            //Assert

            Assert.IsType<Ok<string>>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(MockFinancialDocument.FDForProductAAnonimizedAndExtended, result.Value);
        }
    }
}
