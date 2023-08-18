using Carfox.Core.Specifications;
using Carfox_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Specification
{
    public class FavoritesProduct : BaseSpecifications<Favorite>
    {
        public FavoritesProduct(string id):base(P => P.AppUserId == id)
        {
        }

        public FavoritesProduct(int id):base(P=>P.ProductId==id) { }
    }
}
