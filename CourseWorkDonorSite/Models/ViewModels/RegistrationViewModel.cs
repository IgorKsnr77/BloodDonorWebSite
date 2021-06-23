using System.ComponentModel.DataAnnotations;

namespace CourseWorkDonorSite.Models.ViewModels
{
	public class RegistrationViewModel
	{
        [Display(Name = "Ваше ім'я")]
        public string FirstName { get; set; }

        [Display(Name = "Ваше прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не вказано Email")]
        [Display(Name = "Ваша електронна пошта")]
        [UIHint("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не вказано пароль")]
        [Display(Name = "Введіть пароль")]
        [UIHint("Password")]
        public string Password { get; set; }

        [Display(Name = "Підтвердіть введений пароль")]
        [UIHint("Password")]
        [Compare("Password", ErrorMessage = "Пароль введений невірно")]
        public string ConfirmPassword { get; set; }
    }
}
