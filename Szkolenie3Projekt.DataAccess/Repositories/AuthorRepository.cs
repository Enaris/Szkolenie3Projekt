using System;
using System.Collections.Generic;
using System.Text;
using Szkolenie3Projekt.DataAccess.DbModels;

namespace Szkolenie3Projekt.DataAccess.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ProjectContext context) : base(context)
        {
        }
    }
}
