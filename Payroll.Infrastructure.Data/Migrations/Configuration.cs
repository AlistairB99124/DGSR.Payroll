namespace Payroll.Infrastructure.Data.Migrations
{
    using Context;
    using Initialiser;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<ContextBank>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Payroll.Infrastructure.Data.Context.ContextBank";
        }

        protected override void Seed(ContextBank context)
        {
            //Populate tables with data
            try
            {
                UserDatabaseInitializer.GetJobs().ForEach(c => context.Jobs.Add(c));                
                UserDatabaseInitializer.GetLocations().ForEach(c => context.Locations.Add(c));
                UserDatabaseInitializer.GetBanks().ForEach(c => context.Banks.Add(c));
                UserDatabaseInitializer.GetEmployees().ForEach(c => context.Employees.Add(c));
                UserDatabaseInitializer.GetContracts().ForEach(c => context.Contracts.Add(c));
                UserDatabaseInitializer.GetTimeSheets().ForEach(c => context.TimeSheets.Add(c));
                UserDatabaseInitializer.GetTimeSheetItems().ForEach(c => context.TimeSheetItems.Add(c));
            }
            catch(DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }

            //Delete all stored procs, views
            foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", ""), "Sql\\Seed"), "*.sql"))
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            }

            //Add Stored Procedures
            foreach (var file in Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", ""), "Sql\\StoredProcs"), "*.sql"))
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file), new object[0]);
            }
        }
    }
}
