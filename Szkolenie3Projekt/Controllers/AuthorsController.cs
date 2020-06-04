using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Szkolenie3Projekt.DataAccess;
using Szkolenie3Projekt.DataAccess.DbModels;
using Szkolenie3Projekt.Services;

namespace Szkolenie3Projekt.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this._authorService = authorService;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _authorService.GetAll());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            //var author = await _authorService.Get(id.Value);
            var author = await _authorService.GetDeatils(id.Value);
            
            if (author == null)
                return NotFound();

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth")] Author author)
        {
            if (!ModelState.IsValid)
                return View(author);

            var sameAuthor = await _authorService.Get(author.FirstName, author.LastName, author.DateOfBirth);
            if (sameAuthor != null)
            {
                ModelState.AddModelError("", $"{author.FirstName} {author.LastName} born {author.DateOfBirth:dd/MM/yyy} already exists");
                return View(author);
            }

            await _authorService.Create(author);
            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var author = await _authorService.Get(id.Value);

            if (author == null)
                return NotFound();
            
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateOfBirth")] Author author)
        {
            if (id != author.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(author);

            var sameAuthor = await _authorService.Get(author.FirstName, author.LastName, author.DateOfBirth);
            if (sameAuthor != null && sameAuthor.Id != id)
            {
                ModelState.AddModelError("", $"{author.FirstName} {author.LastName} born {author.DateOfBirth:dd/MM/yyy} already exists");
                return View(author);
            }

            await _authorService.Update(author);

            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var author = await _authorService.GetDeatils(id.Value);

            if (author == null)
                return NotFound();

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _authorService.Delete(id);
            if (result == null)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AuthorExists(int id)
        {
            var author = await _authorService.Get(id);
            return author != null;
        }
    }
}
