using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Collections.Generic;

namespace ReadLater5.Controllers
{
    [Authorize]
    public class BookmarksController : Controller
    {
        IBookmarkService _bookmarkService;
        UserManager<IdentityUser> _userManager;

        public BookmarksController(
            IBookmarkService bookmarkService,
            UserManager<IdentityUser> userManager
            )
        {
            _bookmarkService = bookmarkService;
            _userManager = userManager;
        }

        // GET: Bookmarks
        public IActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            List<Bookmark> model = _bookmarkService.GetBookmarks(userId);
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
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id, userId);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);

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
        public IActionResult Create(Bookmark bookmark)
        {
            string userId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _bookmarkService.CreateBookmark(bookmark, userId);
                return RedirectToAction("Index");
            }

            return View(bookmark);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            string userId = _userManager.GetUserId(User);
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id, userId);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                _bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index");
            }
            return View(bookmark);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            string userId = _userManager.GetUserId(User);
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id, userId);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            string userId = _userManager.GetUserId(User);
            Bookmark bookmark = _bookmarkService.GetBookmark(id, userId);
            _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }
    }
}
