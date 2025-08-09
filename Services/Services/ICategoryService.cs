using Entity;
using System.Collections.Generic;

namespace Services
{
    public interface ICategoryService
    {
        Category CreateCategory(Category category, string userId);
        List<Category> GetCategories(string userId);
        Category GetCategory(int Id, string userId);
        Category GetCategory(string Name);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
        bool IsCategoryUsed(Category category);
    }
}
