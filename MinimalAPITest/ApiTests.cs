using Integration_Testing_ASP.NET_Core_6_WebAPI;
using Integration_Testing_ASP.NET_Core_6_WebAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Net;

namespace MinimalAPITest
{
    [TestClass]
    public class ApiTests
    {
        private static HttpClient _httpClient;
        private static WebApplicationFactory<Program> _webAppFactory;
        private static Mock<AppDbContext> _dbContextMock;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Create the WebApplicationFactory and HttpClient once for all tests in this class.
            //_webAppFactory = new WebApplicationFactory<Program>();
            //_httpClient = _webAppFactory.CreateClient();

            // Create the mock DbContext and set it up with mock data
            _dbContextMock = new Mock<AppDbContext>();
            var mockTodoItems = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "string" },
                new TodoItem { Id = 2, Title = "Item 2" },
                // Add more mock data as needed
            }.AsQueryable();

            // Set up the DbContext's DbSet to return the mock data
            var mockDbSet = new Mock<DbSet<TodoItem>>();
            mockDbSet.As<IQueryable<TodoItem>>().Setup(m => m.Provider).Returns(mockTodoItems.Provider);
            mockDbSet.As<IQueryable<TodoItem>>().Setup(m => m.Expression).Returns(mockTodoItems.Expression);
            mockDbSet.As<IQueryable<TodoItem>>().Setup(m => m.ElementType).Returns(mockTodoItems.ElementType);
            mockDbSet.As<IQueryable<TodoItem>>().Setup(m => m.GetEnumerator()).Returns(mockTodoItems.GetEnumerator());

            // Configure the DbContext to return the DbSet with mock data
            _dbContextMock.Setup(db => db.TodoItems).Returns(mockDbSet.Object);

            // Create the WebApplicationFactory with the mock DbContext
            _webAppFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Replace the real DbContext with the mock DbContext in the DI container
                    services.AddScoped(_ => _dbContextMock.Object);
                });
            });

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
        public async Task Get_Title_By_Id()
        {
            var response = await _httpClient.GetAsync("/api/Todo/title/1");
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("string", stringResult);

        }


        //"id": 1,
        //"title": "string",
        //"isComplete": true,
        //"dueDate": "2023-09-18T10:05:15.34"

        //[TestMethod]
        //public async Task Delete_By_Id()
        //{
            
        //    var deleteResponse = await _httpClient.DeleteAsync("/api/Todo/14");

           
        //    Assert.AreEqual(HttpStatusCode.OK, deleteResponse.StatusCode);

        //    var getItemResponse = await _httpClient.GetAsync("/api/Todo/14");

           
        //    Assert.AreEqual(HttpStatusCode.OK, getItemResponse.StatusCode);

            
        //    var stringResult = await getItemResponse.Content.ReadAsStringAsync();

            
        //    Assert.AreEqual("Item not found", stringResult);

        //}

        [TestMethod]
        public async Task SumValue_Returns_CorrectResult()
        {
            var response = await _httpClient.GetAsync("/add?a=1&b=2");
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("3", stringResult);
        }
    }
}