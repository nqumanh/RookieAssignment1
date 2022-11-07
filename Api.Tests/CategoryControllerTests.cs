using Xunit;
using Apis.Repository;
using Apis.Models;
using AutoMapper;
using FakeItEasy;
using Moq;
using Apis.Controllers;
using Apis.Profiles;
using SharedViewModels;

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
    public async Task Get_WhenSuccess_ReturnOk()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Art",
            Description = "Books in the art nonfiction genre are about some sort of artistic form: painting, sculpting, etc.",
            Products = null,
        };

        var returnCategoryDTO = new CategoryDTO
        {
            Id = 1,
            Name = "Art",
            Description = "Books in the art nonfiction genre are about some sort of artistic form: painting, sculpting, etc.",
        };

        int id = 1;

        var categoryRepositoryMock = new Mock<ICategoryRepository>();
        categoryRepositoryMock.Setup(s => s.Get(id)).Returns(Task.FromResult(category));

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(s => s.Map<CategoryDTO>(category)).Returns(returnCategoryDTO);

        var controller = new CategoryController(categoryRepositoryMock.Object, mapperMock.Object);

        // Act
        var result = (await controller.Get(id));// as OkObjectResult;

        // Assert
        Assert.NotNull(result);

        var data = result.Value as CategoryDTO;
        Assert.NotNull(data);
        Assert.Equal(returnCategoryDTO, data);
    }
}
