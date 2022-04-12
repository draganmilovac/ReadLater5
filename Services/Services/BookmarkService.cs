using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly ReadLaterDataContext _context;

        public BookmarkService(ReadLaterDataContext context)
        {
            _context = context;
        }
        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            _context.Add(bookmark);
            _context.SaveChanges();
            return bookmark;
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _context.Update(bookmark);
            _context.SaveChanges();
        }

        public List<Bookmark> GetBookmarks()
        {
            return _context.Bookmark.Include(x => x.Category).ToList();
        }
        public List<Bookmark> GetBookmarksByUser(string username)
        {
            return _context.Bookmark.Include(x => x.Category).Where(x => x.BookmarkUser == username).ToList();
        }
        public Bookmark GetBookmark(int Id)
        {
            return _context.Bookmark.Include(x => x.Category).Where(x => x.ID == Id).FirstOrDefault();
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _context.Bookmark.Remove(bookmark);
            _context.SaveChanges();
        }

    }
}
