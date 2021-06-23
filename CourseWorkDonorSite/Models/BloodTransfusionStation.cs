using System.ComponentModel.DataAnnotations;

namespace CourseWorkDonorSite.Models
{
	public class BloodTransfusionStation
	{
		[Key]
		public int StationId { get; set; }
		public int CityId { get; set; }
		public virtual City City { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }

	}
}
