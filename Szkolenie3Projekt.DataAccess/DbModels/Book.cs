using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Szkolenie3Projekt.DataAccess.DbModels
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Range(1, 10)]
        public int Score { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
