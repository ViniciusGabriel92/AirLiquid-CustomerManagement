using System;
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
    public class DeleteHandlerTest
    {
        private Mock<IRepository<Cliente>> mockClienteRepository = new Mock<IRepository<Cliente>>();
        private Mock<IMemoryCache> mockMemoryCache;
        private Mock<IMediator> mockMediator = new Mock<IMediator>();
        const string uniqueKey = "22ef88c9-2847-4087-861c-dc2b90edb047";

        public DeleteHandlerTest()
        {
            mockMemoryCache = MockDefault.MockedMemoryCache();
        }

        [Fact]
        public async void Should_Delete_Client_And_Return_True()
        {
            mockClienteRepository.Setup(x => x.Delete(It.IsAny<Guid>())).Returns(Task.FromResult(true));
            DeleteClienteHandler handler = new DeleteClienteHandler(mockMediator.Object, mockClienteRepository.Object, mockMemoryCache.Object);
            DeleteClienteCommand command = new DeleteClienteCommand { UniqueKey = uniqueKey };

            bool result = await handler.Handle(command, new CancellationToken());

            Assert.True(result);
            mockClienteRepository.Verify(mock => mock.Delete(It.IsAny<Guid>()), Times.Once());
        }
    }
}