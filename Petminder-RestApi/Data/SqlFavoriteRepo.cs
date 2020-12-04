using System;
using System.Collections.Generic;
using System.Linq;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public class SqlFavoriteRepo : IFavoriteRepo
    {
        private readonly PetminderContext _context;

        public SqlFavoriteRepo(PetminderContext context)
        {
            _context = context;
        }

        public void CreateFavorite(Favorites Favorite)
        {
            if (Favorite != null)
            {
                throw new ArgumentNullException(nameof(Favorite));
            }

            _context.Favorites.Add(Favorite);

        }

        public void DeleteFavorite(Favorites Favorite)
        {
            if (Favorite != null)
            {
                throw new ArgumentNullException(nameof(Favorite));
            }

            _context.Favorites.Remove(Favorite);
        }

        public IEnumerable<Favorites> GetAllUserFavorites(Guid AccountId)
        {
            return _context.Favorites.Where(p => p.AccountId == AccountId);
        }

        public Favorites GetFavoiteById(Guid id, Guid AccountId)
        {
            return _context.Favorites.Where(p => p.AccountId == AccountId && p.Id == id).First();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateFavorite(Favorites Favorite)
        {
            //Nothing
        }
    }
}
