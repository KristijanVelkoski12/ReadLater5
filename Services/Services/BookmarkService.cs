using Data;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;

        public BookmarkService(ReadLaterDataContext readLaterDataContext)
        {
            _ReadLaterDataContext = readLaterDataContext;
        }
        public Bookmark CreateBookmark(Bookmark bookmark, string userId)
        {
            UserBookmark userBookmark = new UserBookmark();
            userBookmark.UserId = userId;
            userBookmark.BookmarkId = bookmark.ID;
            //Category category = _ReadLaterDataContext.Categories.Where(c => c.Name == bookmark.Category.Name).FirstOrDefault();
            bookmark.CreateDate = DateTime.Now;
            _ReadLaterDataContext.Add(bookmark);
            _ReadLaterDataContext.Add(userBookmark);
            _ReadLaterDataContext.SaveChanges();
            
            return bookmark;
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            UserBookmark userBookmark = _ReadLaterDataContext.UserBookmarks.Where(u => u.BookmarkId == bookmark.ID).FirstOrDefault();
            _ReadLaterDataContext.Bookmark.Remove(bookmark);
            _ReadLaterDataContext.UserBookmarks.Remove(userBookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public Bookmark GetBookmark(int Id, string userId)
        {
            UserBookmark userBookmark = _ReadLaterDataContext.UserBookmarks.Where( u => u.BookmarkId == Id && u.UserId == userId).FirstOrDefault();
            if (userBookmark != null)
            {
                Bookmark bookmark = _ReadLaterDataContext.Bookmark.Where(b => b.ID == Id).FirstOrDefault();

                return bookmark;
            }
            else
                throw new System.Exception("not found");
        }

        public Bookmark GetBookmarkByCategory(int CathegoryId)
        {
            return _ReadLaterDataContext.Bookmark.Where(b => b.CategoryId == CathegoryId).FirstOrDefault();
        }

        public List<Bookmark> GetBookmarks(string userId)
        {
            List<int> bookmarkIds = _ReadLaterDataContext.UserBookmarks.Where(u => u.UserId == userId).Select(u => u.BookmarkId).ToList();
            return _ReadLaterDataContext.Bookmark.Where(u => bookmarkIds.Contains(u.ID)).ToList();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            bookmark.CreateDate = DateTime.Now;
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
