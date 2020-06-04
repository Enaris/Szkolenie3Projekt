using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public BookService(IMapper mapper, IBookRepository bookRepo)
        {
            this._mapper = mapper;
            this._bookRepo = bookRepo;
        }

        public async Task Create(Book book)
        {
            await _bookRepo.CreateAsync(book);
            await _bookRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookListDto>> GetAll()
        {
            var booksDb = await _bookRepo
                .GetAll()
                .Include(b => b.Author)
                .ToListAsync();

            return _mapper.Map<IEnumerable<BookListDto>>(booksDb);
        }

        public async Task<Book> Get(int id)
        {
            return await _bookRepo
                .GetAll()
                .Include(b => b.Author)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> Get(string title, DateTime releaseDate, int authorId)
        {
            return await _bookRepo
                .GetAll()
                .FirstOrDefaultAsync(x => x.Title == title && x.ReleaseDate == releaseDate && x.AuthorId == authorId);
        }

        public async Task Update(Book book)
        {
            _bookRepo.Update(book);
            await _bookRepo.SaveChangesAsync();
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
