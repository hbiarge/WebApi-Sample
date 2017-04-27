using System;
using Aplication.Queries.ViewModels;
using MediatR;

namespace Aplication.Queries
{
    public class GetScheduleQuery
        : IRequest<QueryResponse<ScheduleViewModel[]>>
    {
        public int CinemaId { get; set; }

        public DateTime Date { get; set; }
    }
}