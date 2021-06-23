using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using CourseWorkDonorSite.Settings;
using CourseWorkDonorSite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CourseWorkDonorSite.Models.Repositories.Abstract;
using CourseWorkDonorSite.Models.Repositories.EF;

namespace CourseWorkDonorSite
{
	public class Startup
	{
		private IConfiguration Configuration { get; set; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			Configuration.Bind("CompanyInfo", new CompanyInfo());
			Configuration.Bind("Config", new Config());

			services.AddTransient<IBloodRepository,EfBloodRepository>();

			services.AddDbContext<DonorContext>(options => 
				options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<User, IdentityRole>(options =>
			{
				options.Password.RequiredLength = 5;   // ����������� �����
				options.Password.RequireNonAlphanumeric = false;   // ��������� �� �� ���������-�������� �������
				options.Password.RequireLowercase = false; // ��������� �� ������� � ������ ��������
				options.Password.RequireUppercase = false; // ��������� �� ������� � ������� ��������
				options.Password.RequireDigit = false; // ��������� �� �����
			}).AddEntityFrameworkStores<DonorContext>();

			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.Name = "myCompanyAuth";
				options.Cookie.HttpOnly = true;
				options.LoginPath = "/account/login";
				options.SlidingExpiration = true;
			});

			services.AddMvc();
		}

		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
