using System.ComponentModel.DataAnnotations;

namespace CourseWorkDonorSite.Models
{
	public class BloodDonor
	{
		[Key]
		public int BloodDonorId { get; set; }
		public int CityId { get; set; }
		public virtual City City { get; set; }
		public string DonorName { get; set; }
		public string Phone { get; set; }
		public string BloodType { get; set; }
		public string RhesusBlood { get; set; }

		private static string[] _typesOfBlood = {"І", "ІІ", "ІІІ", "ІV" };
		public static string[] GetTypesOfBlood() => _typesOfBlood;

		private static string[] _rhesusOfBlood = { "+", "-"};
		public static string[] GetRhesusOfBlood() => _rhesusOfBlood;
				

	}
}
