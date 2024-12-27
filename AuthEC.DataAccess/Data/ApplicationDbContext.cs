using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthEC.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthEC.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


		// Define Database Tables
		public DbSet<AppUser> AppUsers { get; set; }


		// Override default behaviour here if neccessary (Like adding constrains, seeding data, ...etc)
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


			// Configure AppUser Library ID as unique
			builder.Entity<AppUser>()
				.HasIndex(u => u.LibraryId)
				.IsUnique();
		}
	}
}
