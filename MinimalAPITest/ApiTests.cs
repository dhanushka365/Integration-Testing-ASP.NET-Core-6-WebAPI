using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace MinimalAPITest
{
    [TestClass]
    public class ApiTests
    {
        public ApiTests()
        {
         
        }
        [TestMethod]
        public async Task DefaultRoute_Returns_HelloWorld_Stream()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var HttpClient = webAppFactory.CreateDefaultClient();

            var response = await HttpClient.GetAsync("/");
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("Hello World!", stringResult);
            
        }

        [TestMethod]
        public async Task sumValueReturn()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();
            var response = await httpClient.GetAsync("/add?a=1&b=2");
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("3", stringResult);
        }
    }
}