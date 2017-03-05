using System.Data;

namespace Aplication.Queries.Infrastructure
{
    public interface IConnectionProvider
    {
        IDbConnection CreateConnection();
    }
}
