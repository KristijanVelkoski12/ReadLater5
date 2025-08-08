using Entity;
using System.Collections.Generic;

namespace Services
{
    public interface ICategoryService
    {
        Category CreateCategory(Category category);
        List<Category> GetCategories();
        Category GetCategory(int Id);
        Category GetCategory(string Name);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
