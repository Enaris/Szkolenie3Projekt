using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Szkolenie3Projekt.DataAccess.DbModels;

namespace Szkolenie3Projekt.Services.DTOs
{
    public class BookAddDto
    {
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayName("Release date")]
        public DateTime ReleaseDate { get; set; }
        [Range(1, 10)]
        public int Score { get; set; }
        [DisplayName("Select authors (use ctrl key to select multiple)")]
        public List<int> AuthorsIds { get; set; }
    }
}
