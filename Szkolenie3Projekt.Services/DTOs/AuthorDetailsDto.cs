using System;
using System.Collections.Generic;
using System.Text;

namespace Szkolenie3Projekt.Services.DTOs
{
    public class AuthorDetailsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<AuthorBookDto> Books { get; set; }
        public double MeanScore { get; set; }
    }
}
