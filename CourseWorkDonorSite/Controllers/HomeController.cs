using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CourseWorkDonorSite.Settings;

namespace CourseWorkDonorSite.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public HomeController()
		{
			Config.SidebarVisible = true;
			Config.UseBootstrap = false;
		}
		public IActionResult Index()
		{
			ViewData["Title"] = "Головна";
			return View();
		}
		public IActionResult Contacts()
		{
			ViewData["Title"] = "Контакти";
			return View();
		}
	}
}
