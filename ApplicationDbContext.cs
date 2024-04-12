using Angular_Crud_C_.Models;
using Microsoft.EntityFrameworkCore;

namespace Angular_Crud_C_
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Product> products { get; set; }
		public object Products { get; internal set; }
	}
}
