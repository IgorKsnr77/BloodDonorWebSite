using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWorkDonorSite.Models.ViewModels
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Не вказано Email")]
        [Display(Name = "Логін")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Не вказано пароль")]
        [UIHint("Password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене?")]
        public bool RememberMe { get; set; }
    }
}
