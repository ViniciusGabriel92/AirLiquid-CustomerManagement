using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace CustomerManagement.Tests.Mocks
{
    public static class MockDefault
    {
        public static Mock<IMemoryCache> MockedMemoryCache()
        {
            Mock<IMemoryCache> mockMemoryCache = new Mock<IMemoryCache>();
            ICacheEntry cachEntry = Mock.Of<ICacheEntry>();
            mockMemoryCache.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cachEntry);
            return mockMemoryCache;
        }
    }
}