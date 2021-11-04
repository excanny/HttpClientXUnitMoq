using HttpClientXUnitMoq.Controllers;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HttpClientXUnitMoq.Tests
{
    public class PostTests
    {
        [Fact]
        public async void ShouldReturnPosts()
        {

            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
               .Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                   .ReturnsAsync(new HttpResponseMessage()
                   {
                       StatusCode = HttpStatusCode.OK,
                       //Content = new StringContent(@"[{ "id": 1, "title": "Cool post!"}, { "id": 100, "title": "Some title"}]"),
                       Content = new StringContent(@"[{ ""id"": 1, ""title"": ""Cool post!""}, { ""id"": 100, ""title"": ""Some title""}]"),
                   }).Verifiable();


            using (var httpClient = new HttpClient(handlerMock.Object))
            {
                // ACT
                using (var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts"))
                {
                    //ASSERT
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    Assert.NotNull(response);

                    handlerMock.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>());
                }
            }

        }
    }
}
