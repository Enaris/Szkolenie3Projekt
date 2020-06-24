using System;
using System.Collections.Generic;
using System.Text;
using Szkolenie3Projekt.DataAccess.DbModels;

namespace Szkolenie3Projekt.DataAccess.Repositories
{
    public class AuthorBookRepository : BaseRepository<AuthorBook>, IAuthorBookRepository
    {
        public AuthorBookRepository(ProjectContext context) : base(context)
        {
        }
    }
}
