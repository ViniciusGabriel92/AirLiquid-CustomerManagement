using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Domain.Commands;
using CustomerManagement.Domain.Entities;
using CustomerManagement.Domain.Handlers;
using CustomerManagement.Domain.Interfaces.Repositories;
using CustomerManagement.Tests.Mocks;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace CustomerManagement.Tests.Handlers
{
    public class InsertClienteHandlerTest
    {
        private Mock<IRepository<Cliente>> mockClienteRepository = new Mock<IRepository<Cliente>>();
        private Mock<IMemoryCache> mockMemoryCache;
        private Mock<IMediator> mockMediator = new Mock<IMediator>();

        const string uniqueKey = "22ef88c9-2847-4087-861c-dc2b90edb047";

        public InsertClienteHandlerTest()
        {
            mockMemoryCache = MockDefault.MockedMemoryCache();
        }

        [Fact]
        public async void Should_Save_Client_And_Return_True()
        {
            mockClienteRepository.Setup(x => x.Insert(It.IsAny<Cliente>())).Returns(Task.FromResult(true));
            InsertClienteHandler handler = new InsertClienteHandler(mockMediator.Object, mockClienteRepository.Object, mockMemoryCache.Object);
            InsertClienteCommand command = new InsertClienteCommand { Nome = "NameMock", Idade = 18 };

            bool result = await handler.Handle(command, new CancellationToken());

            Assert.True(result);
            mockClienteRepository.Verify(mock => mock.Insert(It.IsAny<Cliente>()), Times.Once());
        }
    }
}