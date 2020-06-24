using System;
using System.Collections.Generic;
using System.Text;

namespace Szkolenie3Projekt.DataAccess.DbModels
{
    public class AuthorBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
