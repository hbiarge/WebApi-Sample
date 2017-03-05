using System.Data.Entity;

namespace Infrastructure.Initializers
{
    public class MigrateDatabaseToLatestVersion
        : MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>
    {
        public override void InitializeDatabase(DatabaseContext context)
        {
            Initiaizer.Seed(context);
        }
    }
}
