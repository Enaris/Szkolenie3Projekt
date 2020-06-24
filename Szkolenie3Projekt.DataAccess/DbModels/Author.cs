using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Szkolenie3Projekt.DataAccess.DbModels
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date of birth")]

        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; }
    }
}
