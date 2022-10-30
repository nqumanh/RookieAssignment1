// using Apis.Controllers;
// using SharedViewModels;

// namespace Api.Tests;

// public class CategoryControllerTest
// {
//     private ILoggerManager _logger;
//     private IRepositoryWrapper _repository;
//     private IMapper _mapper;

//     public CategoryControllerTest(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
//     {
//         _logger = logger;
//         _repository = repository;
//         _mapper = mapper;
//     }

//     [Fact]
//     public void Get_AllCategories_ReturnsAllCategories()
//     {
//         // Arrange
//         var repositoryWrapperMock = MockIRepositoryWrapper.GetMock();
//         var mapper = GetMapper();
//         var logger = new LoggerManager();
//         var categoryController = new CategoryController(logger, repositoryWrapperMock.Object, mapper);

//         // Act
//         var result = categoryController.GetAllCategories() as ObjectResult;

//         // Assert
//         Assert.NotNull(result);
//     }
// }