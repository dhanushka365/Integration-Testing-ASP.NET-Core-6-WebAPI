using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;


namespace MinimalAPITest
{
    [TestClass]
    public class ApiTests
    {
        private static HttpClient _httpClient;
        private static WebApplicationFactory<Program> _webAppFactory;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create the WebApplicationFactory and HttpClient once for all tests in this class.
            _webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = _webAppFactory.CreateClient();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // Dispose of resources after all tests in this class are complete.
            _httpClient.Dispose();
            _webAppFactory.Dispose();
        }

        [TestMethod]
        public async Task DefaultRoute_Returns_HelloWorld_Stream()
        {
            var response = await _httpClient.GetAsync("/");
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("Hello World!", stringResult);
        }

        [TestMethod]
        public async Task SumValue_Returns_CorrectResult()
        {
            var response = await _httpClient.GetAsync("/add?a=1&b=2");
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("3", stringResult);
        }
    }
}