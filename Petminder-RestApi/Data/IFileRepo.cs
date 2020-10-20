using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    interface IFileRepo
    {
        File GetFileById();
        IEnumerable<File> GetAllUserFiles();
    }
}
