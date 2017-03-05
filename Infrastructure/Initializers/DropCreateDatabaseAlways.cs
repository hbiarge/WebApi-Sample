using System.Data.Entity;

namespace Infrastructure.Initializers
{
    public class DropCreateDatabaseAlways
        : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Initiaizer.Seed(context);
        }
    }
}