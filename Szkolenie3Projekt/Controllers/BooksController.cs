using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Szkolenie3Projekt.DataAccess;
using Szkolenie3Projekt.DataAccess.DbModels;
using Szkolenie3Projekt.DataAccess.Repositories;
using Szkolenie3Projekt.Services;

namespace Szkolenie3Projekt.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;

        public BooksController(IBookService bookService, IAuthorService authorService)
        {
            this._bookService = bookService;
            this._authorService = authorService;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _bookService.GetAll());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _bookService.Get(id.Value);

            if (book == null)
                return NotFound();

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AuthorId"] = new SelectList(await _authorService.GetAll(), "Id", "FirstName");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ReleaseDate,Score,AuthorId")] Book book)
        {
            if (!ModelState.IsValid)
            {
                ViewData["AuthorId"] = new SelectList(await _authorService.GetAll(), "Id", "FirstName", book.AuthorId);
                return View(book);
            }

            var sameBook = await _bookService.Get(book.Title, book.ReleaseDate, book.AuthorId);
            if (sameBook != null)
            {
                ModelState.AddModelError("", $"{book.Title} released {book.ReleaseDate:dd/MM/yyy} already exists");
                return View(book);
            }

            await _bookService.Create(book);
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _bookService.Get(id.Value);

            if (book == null)
                return NotFound();

            ViewData["AuthorId"] = new SelectList(await _authorService.GetAll(), "Id", "FirstName", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ReleaseDate,Score,AuthorId")] Book book)
        {
            if (id != book.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(book);

            var sameBook = await _bookService.Get(book.Title, book.ReleaseDate, book.AuthorId);
            if (sameBook != null && sameBook.Id != id)
            {
                ModelState.AddModelError("", $"{book.Title} released {book.ReleaseDate:dd/MM/yyy} already exists");
                return View(book);
            }

            await _bookService.Update(book);

            ViewData["AuthorId"] = new SelectList(await _authorService.GetAll(), "Id", "FirstName", book.AuthorId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _bookService.Get(id.Value);

            if (book == null)
                return NotFound();

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _bookService.Delete(id);
            if (result == null)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BookExists(int id)
        {
            var book = await _bookService.Get(id);
            return book!= null;
        }
    }
}
