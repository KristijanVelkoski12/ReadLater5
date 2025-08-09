using Data;
using Entity;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        public CategoryService(ReadLaterDataContext readLaterDataContext) 
        {
            _ReadLaterDataContext = readLaterDataContext;            
        }

        public Category CreateCategory(Category category, string userId)
        {
            UserCategory userCategory = new UserCategory();
            userCategory.UserId = userId;
            userCategory.CategoryId = category.ID;
            _ReadLaterDataContext.Add(category);
            _ReadLaterDataContext.Add(userCategory);
            _ReadLaterDataContext.SaveChanges();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _ReadLaterDataContext.Update(category);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<Category> GetCategories(string userId)
        {
            List<int> categoriesIds = _ReadLaterDataContext.UserCategories.Where(u => u.UserId == userId).Select(u => u.CategoryId).ToList();
            return _ReadLaterDataContext.Categories.Where(u => categoriesIds.Contains(u.ID)).ToList();
        }

        public Category GetCategory(int Id, string userId)
        {
            UserCategory userCategory = _ReadLaterDataContext.UserCategories.Where(u => u.CategoryId == Id && u.UserId == userId).FirstOrDefault();
            if (userCategory != null)
            {
                Category category = _ReadLaterDataContext.Categories.Where(c => c.ID == Id).FirstOrDefault();

                return category;
            }
            else
                throw new System.Exception("not found");
        }

        public Category GetCategory(string Name)
        {
            return _ReadLaterDataContext.Categories.Where(c => c.Name == Name).FirstOrDefault();
        }

        public void DeleteCategory(Category category)
        {
            UserCategory userCategory = _ReadLaterDataContext.UserCategories.Where(u => u.CategoryId == category.ID).FirstOrDefault();
            _ReadLaterDataContext.Categories.Remove(category);
            _ReadLaterDataContext.UserCategories.Remove(userCategory);
            _ReadLaterDataContext.SaveChanges();
        }

        public bool IsCategoryUsed(Category category)
        {
            return _ReadLaterDataContext.Bookmark.Any(b => b.CategoryId == category.ID);
        }

    }
}
