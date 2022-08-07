using CardOrgAPI.Entities;
using CardOrgAPI.Interfaces.Services;
using CardOrgAPI.Responses;
using ImageMagick;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace CardOrgAPI.Application.Utility.IPhone7Cards
{
    public class IPhone7CardsRequestHandler : IRequestHandler<IPhone7CardsRequest, ApiResponse<IPhone7CardsResponse>>
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly CardOrgContext _context;

        public IPhone7CardsRequestHandler(IHostEnvironment hostEnvironment, 
            CardOrgContext context,
            IImageService imageService)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        public async Task<ApiResponse<IPhone7CardsResponse>> Handle(IPhone7CardsRequest request, CancellationToken cancellationToken)
        {
            var path = _hostEnvironment.ContentRootPath + @"\Reports\";
            var cards = _context.Cards.AsQueryable();
            cards = IncludeAllModels(cards)
                    .Where(x => !String.IsNullOrWhiteSpace(x.FrontCardMainImagePath) &&
                    !String.IsNullOrWhiteSpace(x.BackCardMainImagePath));
            var returnCards = await cards.ToListAsync(cancellationToken)
                        .ConfigureAwait(false);

            var response = new IPhone7CardsResponse();
            string propertyInfo = string.Empty;
            var reportList = new List<CVSModels.iPhone7Report>();
            foreach (var card in returnCards)
            {
                var fullPath = _hostEnvironment.ContentRootPath + @$"\clientapp\public\Uploads\Thumb\{card.FrontCardThumbnailImagePath}";
                Image image = new Bitmap(fullPath);
                PropertyItem[] propItems = image.PropertyItems;

                foreach (PropertyItem propItem in propItems)
                {

                    if (propItem.Id == 272)
                    {
                        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                        string manufacturer = encoding.GetString(propItem.Value);
                        if (manufacturer.ToLower().Contains("iphone 7"))
                        {
                            reportList.Add(new CVSModels.iPhone7Report()
                            {
                                BackCardMainImagePath = card.BackCardMainImagePath,
                                BackCardThumbnailImagePath = card.BackCardMainImagePath,
                                CardDescription = card.CardDescription,
                                CardNumber = card.CardNumber,
                                FrontCardMainImagePath = card.FrontCardMainImagePath,
                                FrontCardThumbnailImagePath = card.FrontCardThumbnailImagePath,
                                Location = card.Location.Name,
                                Players = String.Join(", ", card.PlayerCards.Select(x => $"{x.Player.FirstName} {x.Player.LastName}")),
                                Set = card.Set.Name,
                                Year = $"{card.Year.BeginningYear}-{card.Year.EndingYear}"
                            });
                            response.Count += 1;
                        }
                    }
                }
            }
            using (var writer = new StreamWriter(path + "iphone7cards.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(reportList);
            }

            var duplicateImages = new List<CVSModels.iPhone7Report>();
            foreach (var card in returnCards)
            {
                if (returnCards.Any(x => x.FrontCardMainImagePath.ToLower() == card.FrontCardMainImagePath.ToLower() && x.CardId != card.CardId))
                {
                    duplicateImages.Add(new CVSModels.iPhone7Report()
                    {
                        BackCardMainImagePath = card.BackCardMainImagePath,
                        BackCardThumbnailImagePath = card.BackCardMainImagePath,
                        CardDescription = card.CardDescription,
                        CardNumber = card.CardNumber,
                        FrontCardMainImagePath = card.FrontCardMainImagePath,
                        FrontCardThumbnailImagePath = card.FrontCardThumbnailImagePath,
                        Location = card.Location.Name,
                        Players = String.Join(", ", card.PlayerCards.Select(x => $"{x.Player.FirstName} {x.Player.LastName}")),
                        Set = card.Set.Name,
                        Year = $"{card.Year.BeginningYear}-{card.Year.EndingYear}"
                    });
                    response.Count += 1;
                }
            }

            using (var writer = new StreamWriter(path + "duplicate_images.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(duplicateImages);
            }

            return new ApiResponse<IPhone7CardsResponse>
            {
                IsSuccessful = true,
                Value = response
            };
        }

        private IQueryable<Card> IncludeAllModels(IQueryable<Card> query)
        {
            query = query.Include(x => x.PlayerCards).ThenInclude(x => x.Player);
            query = query.Include(x => x.TeamCards).ThenInclude(x => x.Team);
            return query.Include(x => x.GradeCompany)
                    .Include(x => x.Location)
                    .Include(x => x.Set)
                    .Include(x => x.Sport)
                    .Include(x => x.Year)
                    .AsQueryable();
        }
    }
}
