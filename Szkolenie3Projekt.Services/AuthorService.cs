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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepo, IMapper mapper)
        {
            this._authorRepo = authorRepo;
            this._mapper = mapper;
        }

        public async Task Create(Author author)
        {
            await _authorRepo.CreateAsync(author);
            await _authorRepo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _authorRepo.GetAll().ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAllWBooks()
        {
            return await _authorRepo
                .GetAll()
                .Include(a => a.AuthorBooks)
                    .ThenInclude(ab => ab.Book)
                .ToListAsync();
        }

        public async Task<Author> Get(int id)
        {
            return await _authorRepo
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author> Get(string firstName, string lastName, DateTime dateOfBrith)
        {
            return await _authorRepo
                .GetAll()
                .FirstOrDefaultAsync(x => x.FirstName == firstName && x.LastName == lastName && x.DateOfBirth == dateOfBrith);
        }

        public async Task<AuthorDetailsDto> GetDeatils(int id)
        {
            var authorDb = await _authorRepo
                .GetAll()
                .Include(a => a.AuthorBooks)
                    .ThenInclude(ab => ab.Book)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (authorDb == null)
                return null;
            var authorDto = _mapper.Map<AuthorDetailsDto>(authorDb);
            authorDto.MeanScore = authorDto.Books.Any() ? authorDto.Books.Average(b => b.Score) : 0;
            return authorDto;
        }

        public async Task Update(Author author)
        {
            _authorRepo.Update(author);
            await _authorRepo.SaveChangesAsync();
        }

        public async Task<bool?> Delete(int id)
        {
            var authorDb = await Get(id);
            if (authorDb == null)
                return null;

            _authorRepo.Delete(authorDb);
            await _authorRepo.SaveChangesAsync();
            return true;
        }
    }
}
