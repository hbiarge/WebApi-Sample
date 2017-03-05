using MediatR;

namespace Aplication.Commands
{
    public class ValuesRequestHandler : IRequestHandler<ValuesRequest, ValuesResponse>
    {
        public ValuesResponse Handle(ValuesRequest message)
        {
            return new ValuesResponse
            {
                Values = new[] { "Vaue1", "Value2" }
            };
        }
    }
}