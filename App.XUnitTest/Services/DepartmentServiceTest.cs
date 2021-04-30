using App.Data.Entities;
using App.DTO;
using App.Mapper;
using App.Repositories.Interface;
using App.Services;
using App.Services.Interface;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace App.XUnitTest.Services
{
    public class DepartmentServiceTest
    {
        private readonly Mock<IDepartmentRepository> _mockDepartmentRepository;
        private readonly List<Department> _mockList;
        private static IMapper _mapper;
        public DepartmentServiceTest()
        {
            _mockList = new List<Department>() {
                new Department()
                {
                    Id = 1,
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success1",
                    Uuid = new Guid("00000000-0000-0000-0000-000000000001"),
                },
                new Department()
                {
                    Id = 2,
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success2",
                    Uuid = new Guid("00000000-0000-0000-0000-000000000002"),
                },
                new Department()
                {
                    Id = 3,
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success3",
                    Uuid = new Guid("00000000-0000-0000-0000-000000000003"),
                }
            };
            _mockDepartmentRepository = new Mock<IDepartmentRepository>();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
        [Fact]
        public async Task DepartmentService_GetAllAsync_NotNull_Success()
        {
            _mockDepartmentRepository.Setup(e => e.GetAllAsync("")).ReturnsAsync(_mockList);

            var departmentService = new DepartmentService(_mockDepartmentRepository.Object, _mapper);

            var result = await departmentService.GetAllAsync("");
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }
        [Fact]
        public async Task DepartmentService_PutAsync_NotNull_ReturnOne()
        {
            var request = new DepartmentNonRequest()
            {
                Uuid = Guid.NewGuid(),
                Name = "123456",
            };

            var respon = new Department()
            {
                Id = 5,
                Uuid = request.Uuid,
                Name = "123"
            };

            _mockDepartmentRepository.Setup(e => e.GetByUuidTrackingAsync(request.Uuid)).ReturnsAsync(respon);
            _mockDepartmentRepository.Setup(e => e.UpdateAsync(respon)).ReturnsAsync(1);
            var departmentService = new DepartmentService(_mockDepartmentRepository.Object, _mapper);

            var result = await departmentService.PutAsync(request.Uuid, request);
            Assert.Equal(1,result);
        }
        [Fact]
        public async Task DepartmentService_PutAsync_NotNull_ReturnZero()
        {
            var request = new DepartmentNonRequest()
            {
                Uuid = Guid.NewGuid(),
                Name = "123456",
            };

            var respon = new Department()
            {
                Id = 5,
                Uuid = request.Uuid,
                Name = "123"
            };

            _mockDepartmentRepository.Setup(e => e.GetByUuidTrackingAsync(request.Uuid)).ReturnsAsync(respon);
            _mockDepartmentRepository.Setup(e => e.UpdateAsync(respon)).ReturnsAsync(1);
            var departmentService = new DepartmentService(_mockDepartmentRepository.Object, _mapper);

            var result = await departmentService.PutAsync(Guid.NewGuid(), request);
            Assert.Equal(0,result);
        }
        [Fact]
        public async Task DepartmentService_PutAsync_Null_ReturnZero()
        {
            var request = new DepartmentNonRequest()
            {
                Uuid = Guid.NewGuid(),
                Name = "123456",
            };

            Department respon = null;

            _mockDepartmentRepository.Setup(e => e.GetByUuidTrackingAsync(request.Uuid)).ReturnsAsync(respon);
            _mockDepartmentRepository.Setup(e => e.UpdateAsync(respon)).ReturnsAsync(1);
            var departmentService = new DepartmentService(_mockDepartmentRepository.Object, _mapper);

            var result = await departmentService.PutAsync(Guid.NewGuid(), request);
            Assert.Equal(0,result);
        }
    }
}
