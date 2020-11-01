using Microsoft.EntityFrameworkCore;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class PetminderContext : DbContext
    {
        public PetminderContext(DbContextOptions<PetminderContext> opt) : base(opt)
        {
            
        }

        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Pets> Pets { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Filedata> FileData { get; set; }
    }
}