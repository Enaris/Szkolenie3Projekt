using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Szkolenie3Projekt.DataAccess.DbModels;
using Szkolenie3Projekt.Services.DTOs;

namespace Szkolenie3Projekt.Services
{
    public interface IAuthorService
    {
        Task Create(Author author);
        Task<IEnumerable<Author>> GetAll();
        Task<IEnumerable<Author>> GetAllWBooks();
        Task<Author> Get(int id);
        Task<Author> Get(string firstName, string lastName, DateTime dateOfBrith);
        Task<AuthorDetailsDto> GetDeatils(int id);
        Task Update(Author author);
        Task<bool?> Delete(int id);
    }
}