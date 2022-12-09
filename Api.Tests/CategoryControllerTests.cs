using Apis.Models;
using AutoMapper;
using Moq;
using Apis.Controllers;
using Apis.Profiles;
using SharedViewModels;
using System.Reflection;
using Apis.Interface;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace Api.UnitTests;

public class CategoryControllerTests
{
    private readonly IMapper _mapper;
    public CategoryControllerTests()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CategoryProfile());
        });
        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public async Task GetCatetegoryById_WhenSuccess_ReturnOk()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Art",
            Description = "Books in the art nonfiction genre are about some sort of artistic form: painting, sculpting, etc.",
            Products = null,
        };

        var expected = new CategoryDTO
        {
            Id = 1,
            Name = "Art",
            Description = "Books in the art nonfiction genre are about some sort of artistic form: painting, sculpting, etc.",
        };

        const int ID = 1;

        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        categoryRepositoryMock.Setup(s => s.Get(ID)).Returns(Task.FromResult<Category?>(category));

        var controller = new CategoryController(categoryRepositoryMock.Object, _mapper);

        // Act
        var result = (await controller.Get(ID));

        // Assert
        Assert.NotNull(result);
        IList<PropertyInfo> props = new List<PropertyInfo>(expected.GetType().GetProperties());
        foreach (PropertyInfo prop in props)
        {
            Assert.Equal(prop.GetValue(expected), prop.GetValue(result.Value));
        }
    }

    [Fact]
    public async Task GetCatetegoryById_WhenNull_ReturnNotFound()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Art",
            Description = "Books in the art nonfiction genre are about some sort of artistic form: painting, sculpting, etc.",
            Products = null,
        };

        var expected = new CategoryDTO
        {
            Id = 1,
            Name = "Art",
            Description = "Books in the art nonfiction genre are about some sort of artistic form: painting, sculpting, etc.",
        };

        var categoryRepository = new Mock<ICategoryRepository>();
        categoryRepository.Setup(x => x.Get(1))
            .ReturnsAsync(category);

        var controller = new CategoryController(categoryRepository.Object, _mapper);

        //Act
        var result = await controller.Get(1);
        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
    }
}
