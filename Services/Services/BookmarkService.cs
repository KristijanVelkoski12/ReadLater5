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
        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Add(bookmark);
            _ReadLaterDataContext.SaveChanges();
            return bookmark;
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public Bookmark GetBookmark(int Id)
        {
            return _ReadLaterDataContext.Bookmarks.Where(b => b.ID == Id).FirstOrDefault();
        }

        public Bookmark GetBookmarkByCategory(int CathegoryId)
        {
            return _ReadLaterDataContext.Bookmarks.Where(b => b.CategoryId == CathegoryId).FirstOrDefault();
        }

        public List<Bookmark> GetBookmarks()
        {
            return _ReadLaterDataContext.Bookmarks.ToList();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
