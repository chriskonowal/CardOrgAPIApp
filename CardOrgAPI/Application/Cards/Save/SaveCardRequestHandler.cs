using CardOrgAPI.Converters;
using CardOrgAPI.Entities;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Interfaces.Services;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Cards.Save
{
    public class SaveCardRequestHandler : IRequestHandler<SaveCardRequest, ApiResponse>
    {
        private readonly ICardRepository _cardRepository;
        private readonly IImageService _imageService;
        public SaveCardRequestHandler(ICardRepository cardRepository,
            IImageService imageService)
        {
            _cardRepository = cardRepository;
            _imageService = imageService;
        }

        public async Task<ApiResponse> Handle(SaveCardRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            //TODO: Do I want to check if card exists? Probably
            //if (request.Id <= 0)
            //{
            //    var exists = _gradeCompanyRepository.Exists(request.Name);
            //    if (exists)
            //    {
            //        response.ErrorMessage = "Grade Company entry already exists";
            //        response.IsSuccessful = false;
            //        return response;
            //    }
            //}

            var model = CardQueryFilterConverter.Convert(request.CardRequest);
            var fileContext = await _imageService.SavePicturesAsync(request.CardRequest, cancellationToken).ConfigureAwait(false);
            model.FrontCardMainImagePath = fileContext.FrontMainFileName;
            model.FrontCardThumbnailImagePath = fileContext.FrontThumbnailFileName;
            model.BackCardMainImagePath = fileContext.BackMainFileName;
            model.BackCardThumbnailImagePath = fileContext.BackThumbnailFileName;
            //string file = Newtonsoft.Json.JsonConvert.SerializeObject(request.FrontImage);

            var insertSucessful = await _cardRepository.InsertAsync(model, cancellationToken).ConfigureAwait(false);
            if (insertSucessful)
            {
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "Something happened. Card was not added.";
            }

            return response;
        }
    }
}
