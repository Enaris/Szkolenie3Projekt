using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szkolenie3Projekt.DataAccess.DbModels;
using Szkolenie3Projekt.DataAccess.Repositories;
using Szkolenie3Projekt.Services.DTOs;

namespace Szkolenie3Projekt.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorBookRepository _authorBookRepo;

        public BookService(IMapper mapper, IBookRepository bookRepo, IAuthorBookRepository authorBookRepo)
        {
            this._mapper = mapper;
            this._bookRepo = bookRepo;
            this._authorBookRepo = authorBookRepo;
        }

        public async Task Create(BookAddDto book)
        {
            var bookToAdd = _mapper.Map<Book>(book);
            await _bookRepo.CreateAsync(bookToAdd);
            await _bookRepo.SaveChangesAsync();

            var bookAuthorsToAdd = book.AuthorsIds.Select(aId => new AuthorBook
            {
                AuthorId = aId,
                BookId = bookToAdd.Id
            });

            foreach (var ba in bookAuthorsToAdd)
            {
                await _authorBookRepo.CreateAsync(ba);
            }
            await _authorBookRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookListDto>> GetAll()
        {
            var booksDb = await _bookRepo
                .GetAll()
                .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookListDto>>(booksDb);
        }

        public async Task<Book> Get(int id)
        {
            return await _bookRepo
                .GetAll()
                .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BookEditDto> Get4Edit(int id)
        {
            var bookDb = await _bookRepo
                .GetAll()
                .Include(b => b.AuthorBooks)
                    .ThenInclude(ab => ab.Author)
                .FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<BookEditDto>(bookDb);
        }

        public async Task<Book> Get(string title, DateTime releaseDate)
        {
            return await _bookRepo
                .GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Title == title && x.ReleaseDate == releaseDate);
        }

        public async Task Update(BookEditDto book)
        {
            var bookDb = await _bookRepo
                .GetAll()
                .FirstOrDefaultAsync(b => b.Id == book.Id);

            _mapper.Map(book, bookDb);

            _bookRepo.Update(bookDb);
            await _bookRepo.SaveChangesAsync();

            var oldBookAuthors = await _authorBookRepo
                .GetAll()
                .Where(ab => ab.BookId == book.Id)
                .ToListAsync();

            foreach (var ab in oldBookAuthors)
                _authorBookRepo.Delete(ab);

            await _authorBookRepo.SaveChangesAsync();

            var newBookAuthors = book
                .AuthorsIds
                .Select(aId => new AuthorBook { AuthorId = aId, BookId = bookDb.Id });

            foreach (var ab in newBookAuthors)
                await _authorBookRepo.CreateAsync(ab);

            await _authorBookRepo.SaveChangesAsync();
        }

        public async Task<bool?> Delete(int id)
        {
            var bookDb = await Get(id);
            if (bookDb == null)
                return null;

            _bookRepo.Delete(bookDb);
            await _bookRepo.SaveChangesAsync();
            return true;
        }
    }
}
