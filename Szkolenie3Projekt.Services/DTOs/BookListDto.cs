using System;
using System.Collections.Generic;
using System.Text;

namespace Szkolenie3Projekt.Services.DTOs
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<AuthorListDto> Authors { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Score { get; set; }

    }
}
