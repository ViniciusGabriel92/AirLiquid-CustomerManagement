using System.Collections.Generic;
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
    public class GetClienteHandlerTest
    {
        private Mock<IRepository<Cliente>> mockClienteRepository = new Mock<IRepository<Cliente>>();
        private Mock<IMemoryCache> mockMemoryCache;
        private Mock<IMediator> mockMediator = new Mock<IMediator>();

        public GetClienteHandlerTest()
        {
            mockMemoryCache = MockDefault.MockedMemoryCache();
        }
        [Fact]
        public async void Should_Called_GetAll_Method_In_ClientRepository()
        {
            mockClienteRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(new List<Cliente>()));
            GetClienteHandler handler = new GetClienteHandler(mockMediator.Object, mockClienteRepository.Object, mockMemoryCache.Object);
            GetClienteCommand command = new GetClienteCommand();

            await handler.Handle(command, new CancellationToken());

            mockClienteRepository.Verify(mock => mock.GetAll(), Times.Once());
        }
    }
}