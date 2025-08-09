using Entity;
using System.Collections.Generic;

namespace Services
{
    public interface IBookmarkService
    {
        Bookmark CreateBookmark(Bookmark bookmark, string userId);
        List<Bookmark> GetBookmarks(string userId);
        Bookmark GetBookmark(int Id, string userId);
        Bookmark GetBookmarkByCategory(int CathegoryId);
        void UpdateBookmark(Bookmark bookmark);
        void DeleteBookmark(Bookmark bookmark);
    }
}
