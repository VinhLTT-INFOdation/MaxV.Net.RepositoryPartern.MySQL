using App.Data;
using App.Data.Entities;
using App.DTO;
using App.Infrastructures.Dbcontexts;
using App.Mapper;
using App.Repositories.Interface;
using App.XUnitTest;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace App.UniTest.Repositories
{
    public class DepartmentRepositoryTest
    {
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        public readonly ApplicationDbContext _context;
        private static IMapper _mapper;
        private readonly DepartmentRepository _repository;

        private List<IdentityRole> _roleSources = new List<IdentityRole>(){
                             new IdentityRole("test1"),
                             new IdentityRole("test2"),
                             new IdentityRole("test3"),
                             new IdentityRole("test4")
                        };
        public DepartmentRepositoryTest()
        {
            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);
            _context = new InMemoryDbContextFactory().GetApplicationDbContext();

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }


            _repository = new DepartmentRepository(_context, _mapper);

            Assert.NotNull(_repository);
        }

        [Fact]
        public async Task DepartmentRepository_GetDepartmentsPaging_NotNull_Success()
        {

            var mockList = new List<Department>()
            {
                new Department()
                {
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success1",
                    Uuid = Guid.NewGuid(),
                },
                new Department()
                {
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success2",
                    Uuid = Guid.NewGuid(),
                },
                new Department()
                {
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success3",
                    Uuid = Guid.NewGuid(),
                },
                new Department()
                {
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success4",
                    Uuid = Guid.NewGuid()
                }
            };

            await _repository.CreateAsync(mockList);
            var result = _repository.GetDepartmentsPagingAsync("", 1, 2);

            Assert.Equal(2,result.Result.Count());
        }

        [Fact]
        public async Task DepartmentRepository_Post_NotNull_Success()
        {
            var newObj = new DepartmentRequest()
            {
                Name = "DepartmentRepository_Post_NotNull_Success",
            };

            var result = await _repository.PostAsync(newObj);

            Assert.NotNull(result);
            Assert.IsType<Department>(result);
        }

        [Fact]
        public async Task DepartmentRepository_Post_Null()
        {
            var result = await _repository.PostAsync(null);
            Assert.Null(result);
        }

        [Fact]
        public async Task DepartmentRepository_Put_Null()
        {
            var result = await _repository.PutAsync(Guid.NewGuid(),null);
            Assert.Equal(0,result);
        }

        [Fact]
        public async Task DepartmentRepository_Put_NotNull_Failed()
        {
            var newObj = new DepartmentNonRequest()
            {
                Name = "DepartmentRepository_Put_NotNull_Failed",
                Uuid = Guid.NewGuid(),
            };
            var result = await _repository.PutAsync(Guid.NewGuid(), newObj);
            Assert.Equal(0,result);
        }
        [Fact]
        public async Task DepartmentRepository_Put_NotNull_Success()
        {
            var mockList = new List<Department>()
            {
                new Department()
                {
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success1",
                    Uuid = new Guid("00000000-0000-0000-0000-000000000001"),
                },
                new Department()
                {
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success2",
                    Uuid = new Guid("00000000-0000-0000-0000-000000000002"),
                },
                new Department()
                {
                    Name = "DepartmentRepository_GetDepartmentsPaging_NotNull_Success3",
                    Uuid = new Guid("00000000-0000-0000-0000-000000000003"),
                }
            };

            var departments = await _repository.CreateAsync(mockList);

            var newObj = new DepartmentNonRequest()
            {
                Name = "DepartmentRepository_Put_NotNull_Failed",
                Uuid = departments.FirstOrDefault().Uuid,
            };
            var result = await _repository.PutAsync(newObj.Uuid, newObj);
            Assert.Equal(1,result);
        }
    }
}
