using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CourseWorkDonorSite.Models
{
	public class DonorContext: IdentityDbContext<User>
	{
		public DonorContext(DbContextOptions<DonorContext> options): base(options) {}
		public DbSet<BloodTransfusionStation> BloodTransfusionStations { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<BloodRecipient> BloodRecipients { get; set; }
		public DbSet<BloodDonor> BloodDonors { get; set; } 

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<BloodTransfusionStation>().HasData(new BloodTransfusionStation[] {
				new BloodTransfusionStation {StationId = 1,  CityId = 1, Name = "Центр служби крові Національної дитячої спеціалізованої лікарні ОХМАТДИТ", Address = "Київ, вул. В. Чорновола, 28/1, новий корпус, Сектор Б, Центр служби крові"},
				new BloodTransfusionStation {StationId = 2, CityId = 1, Name = "Київський міський центр крові", Address = "Київ, вул. Максима Берлинського, 12"},
				new BloodTransfusionStation {StationId = 3, CityId = 2, Name = "Чернігівський обласний центр крові", Address = "Чернігів, вул. Пирогова, 13"},
				new BloodTransfusionStation {StationId = 4, CityId = 2, Name = "Корюківська Центральна районна лікарня", Address = "Корюківка, вул. Шевченка, 101"},
				new BloodTransfusionStation {StationId = 5, CityId = 3, Name = "Львівський обласний центр служби крові", Address = "Львів, вул. Пекарська, 65"},
				new BloodTransfusionStation {StationId = 6, CityId = 4, Name = "Рівненський обласний центр служби крові", Address = "Рівне, вул. С. Бандери, 31"},
				new BloodTransfusionStation {StationId = 7, CityId = 4, Name = "Здолбунівська центральна районна лікарня №1, відділення трансфузіології", Address = "Здолбунів, вул. Степана Бандери, 1"},
				new BloodTransfusionStation {StationId = 8, CityId = 5, Name = "Житомирський обласний центр крові", Address = "Житомир, вул. Кибальчича, 16"}
			});

			builder.Entity<City>().HasData(new City[]{
				new City { CityId = 1, Name = "Kiyv"},
				new City { CityId = 2, Name = "Chernihiv"},
				new City { CityId = 3, Name = "Lviv"},
				new City { CityId = 4, Name = "Rivne"},
				new City { CityId = 5, Name = "Zhytomyr"}
			});

			base.OnModelCreating(builder);
		}

	}
}
