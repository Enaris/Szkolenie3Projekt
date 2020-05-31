using System;
using System.Collections.Generic;
using System.Text;

namespace Szkolenie3Projekt.DataAccess.DbModels
{
    public class Book
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Score { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
