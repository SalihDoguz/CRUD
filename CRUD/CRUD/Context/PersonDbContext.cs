using CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Context
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions options) : base(options)   
        {
                
        }

        public DbSet<Person> Persons { get; set; }

    }
}
