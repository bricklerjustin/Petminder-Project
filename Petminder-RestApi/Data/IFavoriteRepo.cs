using System;
using System.Collections.Generic;
using Petminder_RestApi.Models;

namespace Petminder_RestApi.Data
{
    public interface IFavoriteRepo
    {
        bool SaveChanges();

        IEnumerable<Favorites> GetAllUserFavorites(Guid AccountId);
        Favorites GetFavoiteById(Guid id, Guid AccountId);
        void CreateFavorite(Favorites Favorite);
        void UpdateFavorite(Favorites Favorite);
        void DeleteFavorite(Favorites Favorite);
    }
}