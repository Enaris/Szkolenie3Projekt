using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Szkolenie3Projekt.DataAccess.DbModels
{
    public class Author
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
