using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Szkolenie3Projekt.DataAccess.DbModels;
using Szkolenie3Projekt.Services.DTOs;

namespace Szkolenie3Projekt.Automapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Author, AuthorDetailsDto>();
            CreateMap<Author, AuthorListDto>();
            CreateMap<Book, AuthorBookDto>();
            CreateMap<Book, BookListDto>()
                .ForMember(b => b.Authors, o => o.MapFrom(s => s.AuthorBooks.Select(ab => ab.Author)));
            CreateMap<Book, BookEditDto>()
                .ForMember(b => b.AuthorsIds, o => o.MapFrom(s => s.AuthorBooks.Select(ab => ab.AuthorId).ToList()));
            CreateMap<BookEditDto, Book>()
                .ForMember(b => b.AuthorBooks, o => o.Ignore());
            CreateMap<BookAddDto, Book>();
        }
    }
}
