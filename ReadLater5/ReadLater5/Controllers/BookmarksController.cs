using Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ReadLater5.Controllers
{

    public class BookmarksController : Controller
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarksController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            List<Bookmark> model = _bookmarkService.GetBookmarks();
            return View(model);
        }

        public IActionResult GetBookmarksByUser(string username)
        {
            var userName = User.FindFirstValue(username);
            List<Bookmark> model = _bookmarkService.GetBookmarksByUser(userName);
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);

        }

        [HttpPost]
        public IActionResult Create(Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);

                bookmark.BookmarkUser = userName;
                bookmark.CreateDate = DateTime.Now;

                _bookmarkService.CreateBookmark(bookmark);

                return RedirectToAction("Index");
            }

            return View(bookmark);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
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
        [Authorize]
        public IActionResult Edit(Bookmark editedBookmark)
        {
            if (ModelState.IsValid)
            {
                var bookmark = _bookmarkService.GetBookmark(editedBookmark.ID);
                bookmark.ShortDescription = editedBookmark.ShortDescription;
                bookmark.URL = editedBookmark.URL;

                _bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index");
            }
            return View(editedBookmark);
        }

        // GET: Categories/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            Bookmark bookmark = _bookmarkService.GetBookmark((int)id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound);
            }
            return View(bookmark);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Bookmark bookmark = _bookmarkService.GetBookmark(id);
            _bookmarkService.DeleteBookmark(bookmark);
            return RedirectToAction("Index");
        }
    }
}
