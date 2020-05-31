using System;
using System.Collections.Generic;
using System.Text;
using Szkolenie3Projekt.DataAccess.DbModels;

namespace Szkolenie3Projekt.DataAccess.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ProjectContext context) : base(context)
        {
        }
    }
}
