using chat.Entities;
using Microsoft.EntityFrameworkCore;

namespace chat.DAL
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
    }
}
