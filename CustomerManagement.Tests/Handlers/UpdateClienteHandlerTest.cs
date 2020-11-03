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
    public class UpdateClienteHandlerTest
    {
        private Mock<IRepository<Cliente>> mockClienteRepository = new Mock<IRepository<Cliente>>();
        private Mock<IMemoryCache> mockMemoryCache;
        private Mock<IMediator> mockMediator = new Mock<IMediator>();
        const string uniqueKey = "22ef88c9-2847-4087-861c-dc2b90edb047";

        public UpdateClienteHandlerTest()
        {
            mockMemoryCache = MockDefault.MockedMemoryCache();
        }
        [Fact]
        public async void Should_Update_Client_And_Return_True()
        {
            mockClienteRepository.Setup(x => x.Update(It.IsAny<Cliente>())).Returns(Task.FromResult(true));
            UpdateClienteHandler handler = new UpdateClienteHandler(mockMediator.Object, mockClienteRepository.Object, mockMemoryCache.Object);
            UpdateClienteCommand command = new UpdateClienteCommand { UniqueKey = uniqueKey, Nome = "NameMock", Idade = 18 };

            bool result = await handler.Handle(command, new CancellationToken());

            Assert.True(result);
            mockClienteRepository.Verify(mock => mock.Update(It.IsAny<Cliente>()), Times.Once());
        }
    }
}