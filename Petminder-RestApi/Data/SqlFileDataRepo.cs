using System;
using System.Linq;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class SqlFileDataRepo : IFileDataRepo
    {
        private PetminderContext _context;

        public SqlFileDataRepo(PetminderContext context)
        {
            _context = context;
        }

        public void CreateFileData(Filedata filedata)
        {
            if (filedata == null)
            {
                throw new ArgumentNullException(nameof(filedata));
            }

            _context.FileData.Add(filedata);
        }

        public void DeleteFileData(Filedata filedata)
        {
            if (filedata == null)
            {
                throw new ArgumentNullException(nameof(filedata));
            }

            _context.FileData.Remove(filedata);
        }

        public Filedata GetFileDataById(Guid id)
        {
            return _context.FileData.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}