using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Szkolenie3Projekt.Services.DTOs
{
    public class BookEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayName("Release date")]
        public DateTime ReleaseDate { get; set; } = new DateTime();
        [Range(1, 10)]
        public int Score { get; set; }
        [DisplayName("Select authors (use ctrl key to select multiple)")]
        public List<int> AuthorsIds { get; set; }
    }
}
