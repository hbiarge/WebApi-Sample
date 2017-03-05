using System.Linq;
using System.Threading.Tasks;
using Aplication.Queries.Infrastructure;
using Aplication.Queries.ViewModels;
using MediatR;
using Dapper;

namespace Aplication.Queries
{
    public class GetCinemasQueryHandler : IAsyncRequestHandler<GetCinemasQuery, GetCinemasResponse>
    {
        private readonly IConnectionProvider _connectionProvider;

        public GetCinemasQueryHandler(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<GetCinemasResponse> Handle(GetCinemasQuery message)
        {
            using (var conn = _connectionProvider.CreateConnection())
            {
                const string sql = @"
SELECT C.Id, C.Name FROM cine.Cinemas C
";
                var cinemas = await conn.QueryAsync<CinemaViewModel>(sql);

                return new GetCinemasResponse(cinemas.ToArray());
            }
        }
    }
}
