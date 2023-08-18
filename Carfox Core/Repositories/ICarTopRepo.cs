using Carfox_Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carfox_Core.Repositories
{
	public interface ICarTopRepo
	{
		public Task<IEnumerable<TopCars>> GetAllTopCars();

		public Task AddTopCar (TopCars topCar , int time);

		public Task RemoveTopCar (string topCar);

		public Task<TopCars> GetTopCarById(string id);

    }
}
