using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Cards.Delete
{
    public class DeleteCardRequestHandler : IRequestHandler<DeleteCardRequest, ApiResponse>
    {
        private readonly ICardRepository _cardRepository;

        public DeleteCardRequestHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<ApiResponse> Handle(DeleteCardRequest request, CancellationToken cancellationToken)
        {
            var result = await _cardRepository.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
            var response = new ApiResponse();
            response.IsSuccessful = result;
            return response;
        }
    }
}
