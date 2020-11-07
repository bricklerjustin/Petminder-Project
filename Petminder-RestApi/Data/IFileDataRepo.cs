using System;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IFileDataRepo
    {
        bool SaveChanges();

        Filedata GetFileDataById(Guid id);
        void CreateFileData(Filedata filedata);
        void DeleteFileData(Filedata filedata);
    }
}