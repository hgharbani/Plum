using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Plum.Utility;

namespace Plum.Data.Contex
{

    public class InitialDb: SQLite.CodeFirst.SqliteCreateDatabaseIfNotExists<PlumContext>
    {
        public InitialDb(DbModelBuilder modelBuilder) : base(modelBuilder)
        {
        }
        string data = "jelveh";
        byte[] hash;
        protected override void Seed(PlumContext context)
        {
            IList<User> defaultStandards = new List<User>();
            var userPassword = "jelveh";
            var addMinPassword = "Hossein2220968";
            defaultStandards.Add(new User()
            {
                UserName = "jelveh",
                Password = userPassword.ToHashCode()
            });

            defaultStandards.Add(new User()
            {
                UserName = "Hossein",
                Password = addMinPassword.ToHashCode()
            });
            context.Users.AddRange(defaultStandards);
            base.Seed(context);
        }

       
    }
}
