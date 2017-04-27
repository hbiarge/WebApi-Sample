using System.Linq;
using System.Threading.Tasks;
using Aplication.Queries.Infrastructure;
using Aplication.Queries.ViewModels;
using MediatR;
using Dapper;

namespace Aplication.Queries
{
    public class GetScheduleQueryHandler : IAsyncRequestHandler<GetScheduleQuery, QueryResponse<ScheduleViewModel[]>>
    {
        private readonly IConnectionProvider _connectionProvider;

        public GetScheduleQueryHandler(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<QueryResponse<ScheduleViewModel[]>> Handle(GetScheduleQuery message)
        {
            using (var conn = _connectionProvider.CreateConnection())
            {
                const string sql = @"
SELECT SE.Id AS SessionId
      ,SC.Id AS ScreenId
	  ,SC.Name AS ScreenName
	  ,F.Id AS FilmId
	  ,F.Title AS FilmTitle
	  ,F.DurationInMinutes AS FilmDuration
FROM [session].[Sessions] SE
    INNER JOIN [cine].[Screens] SC ON SE.ScreenId = SC.Id
	INNER JOIN [cine].[Films] F ON SE.FilmId = F.Id
WHERE SC.CinemaId = @cinemaId AND SE.[Start] BETWEEN @start AND @end
";
                var cinemas = await conn.QueryAsync<ScheduleViewModel>(sql, new
                {
                    CinemaId = message.CinemaId,
                    Start = message.Date.Date,
                    End = message.Date.Date.AddDays(1).AddSeconds(-1)
                });

                return new QueryResponse<ScheduleViewModel[]>
                {
                    Data = cinemas.ToArray()
                };
            }
        }
    }
}
