using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CourseWorkDonorSite.Models
{
	public class City
	{
		[Key]
		public int CityId { get; set; }
		public string Name { get; set; }
		public virtual List<BloodTransfusionStation> Stations { get; set; }
	}
}
