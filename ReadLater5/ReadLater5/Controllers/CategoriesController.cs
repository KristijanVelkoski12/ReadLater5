using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace ReadLater5.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        ICategoryService _categoryService;
        UserManager<IdentityUser> _userManager;
        public CategoriesController(
            ICategoryService categoryService,
            UserManager<IdentityUser> userManager
            )
        {
            _categoryService = categoryService;
            _userManager = userManager;
        }
        // GET: Categories
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            List<Category> model = _categoryService.GetCategories(userId);
            return View(model);
        }

        // GET: Categories/Details/5
        public IActionResult Details(int? id)
        {
            string userId = _userManager.GetUserId(User);
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Category category = _categoryService.GetCategory((int)id, userId);
            if (category == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(category);

        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            string userId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _categoryService.CreateCategory(category, userId);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            string userId = _userManager.GetUserId(User);
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Category category = _categoryService.GetCategory((int)id, userId);
            if (category == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            string userId = _userManager.GetUserId(User);
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Category category = _categoryService.GetCategory((int)id, userId);
            if (category == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            string userId = _userManager.GetUserId(User);
            Category category = _categoryService.GetCategory(id, userId);
            if (_categoryService.IsCategoryUsed(category))
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict);
            }
            else
            {
                _categoryService.DeleteCategory(category);
                return RedirectToAction("Index");
            }
        }
    }
}
