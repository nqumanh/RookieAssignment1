// namespace Api.Tests;
// internal class MockICategoryRepository
// {
//     public static Mock<ICategoryRepository> GetMock()
//     {
//         var mock = new Mock<ICategoryRepository>();
//         var categories = new List<Category>()
//         {
//             new Category()
//             {
//                 Id = 1,
//                 Name = "Art",
//                 Description = "Music and other art"
//             }
//         };
//         // Set up
//         mock.Setup(m => m.GetAll()).Returns(() => categories);

//         return mock;
//     }
// }