using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreateFile(Files File)
        {
            if (File == null)
            {
                throw new ArgumentNullException(nameof(File));
            }

            _context.Files.Add(File);
        }

        public void DeleteFile(Files File)
        {
            if (File == null)
            {
                throw new ArgumentNullException(nameof(File));
            }

            _context.Files.Remove(File);
        }

        public IEnumerable<Files> GetAllUserFiles(Guid AccountId)
        {
            return _context.Files.Where(p => p.AccountId == AccountId);
        }

        public Files GetFileById(Guid id, Guid AccountId)
        {
            return _context.Files.FirstOrDefault(p => p.Id == id && p.AccountId == AccountId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public void UpdateFile(Files File)
        {
            //Nothing
        }
    }
}