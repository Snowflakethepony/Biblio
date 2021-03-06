﻿using Biblio.Server.Data;
using Biblio.Server.Interfaces;
using Biblio.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblio.Server.Repositories
{
    public class LibraryRepository : RepositoryBase<Library>, ILibraryRepository
    {
        public LibraryRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext) { }

        public void CreateLibrary(Shared.Models.Library library)
        {
            Create(library);
        }

        public void DeleteLibrary(Shared.Models.Library library)
        {
            Delete(library);
        }

        public async Task<IEnumerable<Shared.Models.Library>> GetAllLibraries()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Shared.Models.Library> GetLibraryById(int id)
        {
            return await FindByCondition(l => l.LibraryId == id).Include(l => l.OwnedBooks).Include(l => l.ForeignBooks).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Library>> GetLibrariesByName(string name)
        {
            try
            {
                //var model = typeof(Library);
                //return await FindBySqlLike(model.FullName + "," + model.Assembly.FullName, nameof(Library.Name), name).ToListAsync();
                return await FindByCondition(l => l.Name.ToUpper().Contains(name.ToUpper())).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateLibrary(Library library)
        {
            Update(library);
        }

        public async Task<Shared.Models.Library> GetLibraryByName(string name)
        {
            return await FindByCondition(l => l.Name.ToUpper().Contains(name.ToUpper())).SingleOrDefaultAsync();
        }
    }
}
