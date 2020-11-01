using System;
using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IFileRepo
    {
        bool SaveChanges();

        IEnumerable<Files> GetAllUserFiles(Guid AccountId);
        Files GetFileById(Guid id, Guid AccountId);
        void CreateFile(Files File, Filedata Data);
        void UpdateFile(Files File);
        void DeleteFile(Files File);
    }
}