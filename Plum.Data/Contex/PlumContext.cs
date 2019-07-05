using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Plum.Data.Contex
{
  public  class PlumContext:DbContext
    {
        public PlumContext() : base("PlumContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodMaterial>().
            HasRequired<MaterialPrice>(x => x.MaterialPrice)
                .WithMany(y => y.FoodMaterials)
                .HasForeignKey(x => x.MaterialPriceId);


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
