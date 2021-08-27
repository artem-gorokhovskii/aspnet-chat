using chat.Entities;
using chat.Services;
using Microsoft.EntityFrameworkCore;

namespace chat.DAL
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminLogin = "admin";
            string adminPassword = "8A3D0141922C98DA2539C0A8B6C14AE95D7F95A15F93A8AE014D9C08CBC7E821"; // "admin"

            Role adminRole = new Role() { Id = 1, Name = adminRoleName };
            Role userRole = new Role() { Id = 2, Name = userRoleName };

            User adminUser = new User() {
                Id = 1,
                Name = "Artem",
                Login = adminLogin,
                Password = adminPassword,
                Description = "Test admin user",
                RoleId = adminRole.Id
            };

            builder.Entity<User>(entity => {
                entity.HasData(new User[] { adminUser });
                entity.HasIndex(e => e.Login).IsUnique();
            });

            builder.Entity<Role>(entity => {
                entity.HasData(new Role[] { adminRole, userRole });
                entity.HasIndex(e => e.Name).IsUnique();
            });

            base.OnModelCreating(builder);
        }
    }
}
