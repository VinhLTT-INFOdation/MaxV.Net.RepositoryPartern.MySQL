using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using App.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace App.XUnitTest.Controllers
{
    public class WeatherForecastControllerTest
    {
        private readonly WeatherForecastController _controller;
        HttpClient _client;
        string baseUrl = "/Api/WeatherForecast";
        public WeatherForecastControllerTest()
        {
            _client = new HttpClient
            {
                //BaseAddress = new Uri(baseUrl)
            };
            var logger = new Mock<ILogger<WeatherForecastController>>();
            _controller = new WeatherForecastController(logger.Object);
        }
        [Fact]

        public async Task WeatherForecastController_GetDepartmentsPaging_NotNull_FailedAsync()
        {

            //Assert.IsType<BadRequestResult>();
        }
    }
}
