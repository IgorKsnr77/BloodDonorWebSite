using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CourseWorkDonorSite.Models.Repositories.Abstract;

namespace CourseWorkDonorSite.Models.Repositories.EF
{
	public class EfBloodRepository: IBloodRepository
	{
        private DonorContext _context;

		public EfBloodRepository(DonorContext context)
		{
            _context = context;
		}
        
        private string[] _citiesOfDonation;
        public string[] GetCitiesOfDonation()
        { 
                  
            var citiesDonation = _context.Cities.OrderBy(t => t.Name).ToList();

            _citiesOfDonation = new string[citiesDonation.Count];

            City currentCity;

            for (int i = 0; i < _citiesOfDonation.Length; i++)
            {
                foreach (var item in citiesDonation)
                {
                    currentCity = citiesDonation[i];

                    _citiesOfDonation[i] = currentCity.Name;
                }

            }

            return _citiesOfDonation;
        
        }
    }
}
