using System;
using System.Collections.Generic;
using System.Text;

namespace Szkolenie3Projekt.Services.DTOs
{
    public class AuthorBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public int AuthorBookId { get; set; }

    }
}
