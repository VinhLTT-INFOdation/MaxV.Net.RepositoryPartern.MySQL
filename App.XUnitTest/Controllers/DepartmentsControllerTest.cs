using App.DTO;
using App.Services.Interface;
using AppS4.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace App.XUnitTest.Controllers
{
    public class DepartmentsControllerTest
    {
        private Mock<IDepartmentService> _mockDepartmentService;
        public DepartmentsControllerTest()
        {
            _mockDepartmentService = new Mock<IDepartmentService>();
        }

        [Fact]
        public async Task DepartmentsController_Post_NotNull_Ok()
        {
            DepartmentRequest request = new DepartmentRequest() { };

            _mockDepartmentService.Setup(e => e.PostAsync(request)).ReturnsAsync(new DepartmentNonRequest() { });

            var departmentController = new DepartmentsController(_mockDepartmentService.Object);

            var respon = await departmentController.Post(request);
            Assert.IsType<OkObjectResult>(respon);
        }

        [Fact]
        public async Task DepartmentsController_Post_NotNull_NotFound()
        {
            DepartmentRequest request = new DepartmentRequest() { };
            DepartmentNonRequest response = null;
            _mockDepartmentService.Setup(e => e.PostAsync(request)).ReturnsAsync(response);

            var departmentController = new DepartmentsController(_mockDepartmentService.Object);

            var result = await departmentController.Post(request);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DepartmentsController_Put_NotNull_NotFound()
        {
            var newUuid = Guid.NewGuid();
            DepartmentNonRequest request = new DepartmentNonRequest() { Uuid = newUuid };

            _mockDepartmentService.Setup(e => e.PutAsync(newUuid, request)).ReturnsAsync(0);

            var departmentController = new DepartmentsController(_mockDepartmentService.Object);

            var result = await departmentController.Put(newUuid, request);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DepartmentsController_Put_NotNull_BadRequest()
        {
            var newUuid = Guid.NewGuid();
            DepartmentNonRequest request = new DepartmentNonRequest() { Uuid = Guid.NewGuid() };

            var departmentController = new DepartmentsController(_mockDepartmentService.Object);

            var result = await departmentController.Put(newUuid, request);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DepartmentsController_Put_NotNull_Ok()
        {
            var newUuid = Guid.NewGuid();
            DepartmentNonRequest request = new DepartmentNonRequest() { Uuid = newUuid };

            _mockDepartmentService.Setup(e => e.PutAsync(newUuid, request)).ReturnsAsync(1);

            var departmentController = new DepartmentsController(_mockDepartmentService.Object);

            var result = await departmentController.Put(newUuid, request);
            Assert.IsType<OkResult>(result);
        }
    }
}
