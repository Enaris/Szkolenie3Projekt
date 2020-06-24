using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Szkolenie3Projekt.DataAccess.DbModels;
using Szkolenie3Projekt.Services.DTOs;

namespace Szkolenie3Projekt.Services
{
    public interface IBookService
    {
        Task Create(BookAddDto book);
        Task<IEnumerable<BookListDto>> GetAll();
        Task<Book> Get(int id);
        Task<Book> Get(string title, DateTime releaseDate);
        Task Update(Book book);
        Task<bool?> Delete(int id);
    }
}