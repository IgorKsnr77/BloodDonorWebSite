using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWorkDonorSite.Models.Repositories.Abstract
{
	public interface IBloodRepository
	{
		public string[] GetCitiesOfDonation();
	}
}
