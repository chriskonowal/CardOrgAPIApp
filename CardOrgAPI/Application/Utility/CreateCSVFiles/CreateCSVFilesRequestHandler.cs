using CardOrgAPI.Contexts;
using CardOrgAPI.Responses;
using CsvHelper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Utility.CreateCSVFiles
{
    public class CreateCSVFilesRequestHandler : IRequestHandler<CreateCSVFilesRequest, ApiResponse<CreateCSVFilesResponse>>
    {
        private readonly CardOrgContext _context;
        private readonly IHostEnvironment _hostEnvironment;

        public CreateCSVFilesRequestHandler(CardOrgContext context,
            IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<ApiResponse<CreateCSVFilesResponse>> Handle(CreateCSVFilesRequest request, CancellationToken cancellationToken)
        {
            var startTime = DateTime.Now;
            int processedAmount = 0;
            var path = _hostEnvironment.ContentRootPath + @"\Database\";

            //years
            var years = await _context.Years.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "years.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecordsAsync(years);
            }

            processedAmount += years.Count();

            //teams
            var teams = await _context.Teams.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "teams.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(teams);
            }

            processedAmount += teams.Count();

            //sports
            var sports = await _context.Sports.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "sports.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(sports);
            }

            processedAmount += sports.Count();

            //sets
            var sets = await _context.Sets.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "sets.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(sets);
            }

            processedAmount += sets.Count();

            //players
            var players = await _context.Players.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "players.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(players);
            }

            processedAmount += players.Count();

            //locations
            var locations = await _context.Locations.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "locations.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(locations);
            }

            processedAmount += locations.Count();

            //gradeCompanies
            var gradeCompanies = await _context.GradeCompanies.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "gradeCompanies.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(gradeCompanies);
            }

            processedAmount += gradeCompanies.Count();

            //cards
            var cards = await _context.Cards.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "cards.csv"))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(cards.Select(x => new CardOrgAPI.CVSModels.Card
                    {
                        BackCardMainImagePath = x.BackCardMainImagePath,
                        BackCardThumbnailImagePath = x.BackCardThumbnailImagePath,
                        CardDescription = x.CardDescription,
                        CardId = x.CardId,
                        CardNumber = x.CardNumber,
                        Copies = x.Copies,
                        EbayPrice = x.EbayPrice,
                        FrontCardMainImagePath = x.FrontCardMainImagePath,
                        FrontCardThumbnailImagePath = x.FrontCardThumbnailImagePath,
                        Grade = x.Grade,
                        GradeCompanyId = x.GradeCompanyId,
                        HighestBeckettPrice = x.HighestBeckettPrice,
                        IsAutograph = x.IsAutograph,
                        IsGameWornJersey = x.IsGameWornJersey,
                        IsGraded = x.IsGraded,
                        IsOnCardAutograph = x.IsOnCardAutograph,
                        IsPatch = x.IsPatch,
                        IsRookie = x.IsRookie,
                        LocationId = x.LocationId,
                        LowestBeckettPrice = x.LowestBeckettPrice,
                        LowestComcprice = x.LowestComcprice,
                        PricePaid = x.PricePaid,
                        SerialNumber = x.SerialNumber,
                        SetId = x.SetId,
                        SportId = x.SportId,
                        TimeStamp = x.TimeStamp,
                        YearId = x.YearId
                    } 
                    ));
                }
            }
            processedAmount += cards.Count();

            //playerCards
            var playerCards = await _context.PlayerCards.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "playerCards.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(playerCards.Select(x => new CardOrgAPI.CVSModels.PlayerCard {
                    CardId = x.CardId,
                    PlayerCardId = x.PlayerCardId,
                    PlayerId = x.PlayerId
                }));
            }

            processedAmount += playerCards.Count();

            //teamCards
            var teamCards = await _context.TeamCards.ToListAsync().ConfigureAwait(false);
            using (var writer = new StreamWriter(path + "teamCards.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(teamCards.Select(x => new CardOrgAPI.CVSModels.TeamCard {
                    CardId = x.CardId,
                    TeamCardId = x.TeamCardId,
                    TeamId = x.TeamId
                }));
            }

            processedAmount += teamCards.Count();

            var endTime = DateTime.Now;

            var response = new ApiResponse<CreateCSVFilesResponse>()
            {
                IsSuccessful = true,
                Value = new CreateCSVFilesResponse() 
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    ProcessedAmount = processedAmount
                }
            };

            return response;
        }
    }
}
