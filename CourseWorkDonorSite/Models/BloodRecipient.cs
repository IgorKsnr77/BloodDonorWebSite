using System.ComponentModel.DataAnnotations;

namespace CourseWorkDonorSite.Models
{
	public class BloodRecipient
	{
		[Key]
		public int BloodRecipientId { get; set; }
		public int CityId { get; set; }
		public virtual City City { get; set; }
		public string RecipientName { get; set; }
		public string Phone { get; set;}
		public string BloodType { get; set; }
		public string RhesusBlood { get; set; }

	}
}
