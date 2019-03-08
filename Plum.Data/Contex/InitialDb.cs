using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plum.Data.Contex
{
   public class InitialDb: SQLite.CodeFirst.SqliteCreateDatabaseIfNotExists<PlumContext>
    {
        public InitialDb(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }

        protected override void Seed(PlumContext context)
        {
         
            base.Seed(context);
        }
    }
}
