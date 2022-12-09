using Apis.Models;

namespace Api.UnitTests;
public class CategoryMockData
{
    public static List<Category> GetCategories()
    {
        return new List<Category>{
            new Category{
                Id= 1,
                Name= "Art",
                Description= "Books in the art nonfiction genre are about some sort of artistic form: painting, sculpting, etc."
            },
            new Category{
                Id= 2,
                Name= "Art",
                Description= "Books in the art nonfiction genre are about some sort of artistic form: painting, sculpting, etc."
            },
            new Category{
                Id= 3,
                Name= "Art",
                Description= "Books in the art nonfiction genre are about some sort of artistic form: painting, sculpting, etc."
            },
        };
    }
}