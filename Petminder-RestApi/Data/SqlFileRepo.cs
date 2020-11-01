using System;
using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class SqlFileRepo : IFileRepo
    {
        private readonly PetminderContext _context;

        public SqlFileRepo(PetminderContext context)
        {
            _context = context;
        }

        public void CreateFile(Files File, Filedata Data)
        {
            if (File != null)
            {
                
            }
        }

        public void DeleteFile(Files File)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Files> GetAllUserFiles(Guid AccountId)
        {
            throw new NotImplementedException();
        }

        public Files GetFileById(Guid id, Guid AccountId)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateFile(Files File)
        {
            throw new NotImplementedException();
        }
    }
}