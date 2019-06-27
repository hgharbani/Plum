using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plum.Data.Contex;
using SQLite.CodeFirst;

namespace Plum.Data
{
  public  class PlumContext:DbContext
    {
        public PlumContext() : base("PlumContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            var initializer = new InitialDb(modelBuilder);
            Database.SetInitializer(initializer);
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialPrice> MaterialsPrice { get; set; }

        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodMaterial> FoodMaterials { get; set; }
        public DbSet<FoodSurplusPrice> FoodSurplusPrices { get; set; }
        public DbSet<User> Users { get; set; }

    }

   
}
