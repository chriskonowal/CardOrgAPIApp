using CardOrgAPI.Contexts;
using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using CsvHelper;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CardOrgAPI.Application.Utility.PopulateTables
{
    public class PopulateTablesRequestHandler : IRequestHandler<PopulateTablesRequest, ApiResponse<PopulateTablesResponse>>
    {
        private readonly CardOrgContext _context;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly string _path;

        public PopulateTablesRequestHandler(CardOrgContext context,
            IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _path = _hostEnvironment.ContentRootPath + @"\Database\";
        }

        public async Task<ApiResponse<PopulateTablesResponse>> Handle(PopulateTablesRequest request, CancellationToken cancellationToken)
        {
            var startTime = DateTime.Now;
            int processedAmount = 0;
            
            processedAmount = await PopulateYearsAsync(processedAmount, cancellationToken).ConfigureAwait(false);
            processedAmount = await PopulateTeamsAsync(processedAmount, cancellationToken).ConfigureAwait(false);
            processedAmount = await PopulateSportsAsync(processedAmount, cancellationToken).ConfigureAwait(false);
            processedAmount = await PopulateSetsAsync(processedAmount, cancellationToken).ConfigureAwait(false);
            processedAmount = await PopulatePlayersAsync(processedAmount, cancellationToken).ConfigureAwait(false);
            processedAmount = await PopulateLocationsAsync(processedAmount, cancellationToken).ConfigureAwait(false);
            processedAmount = await PopulateGradeCompaniesAsync(processedAmount, cancellationToken).ConfigureAwait(false);
            processedAmount = await PopulateCardsAsync(processedAmount, cancellationToken).ConfigureAwait(false);
            processedAmount = await PopulatePlayerCardsAsync(processedAmount, cancellationToken).ConfigureAwait(false);
            processedAmount = await PopulateTeamCardsAsync(processedAmount, cancellationToken).ConfigureAwait(false);

            var endTime = DateTime.Now;

            var response = new ApiResponse<PopulateTablesResponse>()
            {
                IsSuccessful = true,
                Value = new PopulateTablesResponse()
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    ProcessedAmount = processedAmount
                }
            };

            return response;
        }

        private async Task<int> PopulateYearsAsync(int processedAmount, CancellationToken cancellationToken)
        {
            var years = new List<Year>();
            using (var reader = new StreamReader(_path + "years.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    years = csv.GetRecords<Year>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Year] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Year]");
                await _context.Year.AddRangeAsync(years, cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Year] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }

        private async Task<int> PopulateTeamsAsync(int processedAmount, CancellationToken cancellationToken)
        {
            var teams = new List<Team>();
            using (var reader = new StreamReader(_path + "teams.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    teams = csv.GetRecords<Team>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Team] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Team]");
                await _context.Team.AddRangeAsync(teams, cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Team] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }

        private async Task<int> PopulateSportsAsync(int processedAmount, CancellationToken cancellationToken)
        {
            var sports = new List<Sport>();
            using (var reader = new StreamReader(_path + "sports.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    sports = csv.GetRecords<Sport>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Sport] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Sport]");
                await _context.Sport.AddRangeAsync(sports, cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Sport] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }

        private async Task<int> PopulateSetsAsync(int processedAmount, CancellationToken cancellationToken)
        {
            var sets = new List<Set>();
            using (var reader = new StreamReader(_path + "sets.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    sets = csv.GetRecords<Set>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Set] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Set]");
                await _context.Set.AddRangeAsync(sets, cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Set] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }

        private async Task<int> PopulatePlayersAsync(int processedAmount, CancellationToken cancellationToken) 
        {
            var players = new List<Player>();
            using (var reader = new StreamReader(_path + "players.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    players = csv.GetRecords<Player>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Player] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Player]");
                await _context.Player.AddRangeAsync(players, cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Player] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }

        private async Task<int> PopulateLocationsAsync(int processedAmount, CancellationToken cancellationToken)
        {
            var locations = new List<Location>();
            using (var reader = new StreamReader(_path + "locations.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    locations = csv.GetRecords<Location>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Location] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Location]");
                await _context.Location.AddRangeAsync(locations, cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Location] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }

        private async Task<int> PopulateGradeCompaniesAsync(int processedAmount, CancellationToken cancellationToken)
        {
            var gradeCompanies = new List<GradeCompany>();
            using (var reader = new StreamReader(_path + "gradeCompanies.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    gradeCompanies = csv.GetRecords<GradeCompany>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[GradeCompany] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[GradeCompany]");
                await _context.GradeCompany.AddRangeAsync(gradeCompanies, cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[GradeCompany] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }

        private async Task<int> PopulateCardsAsync(int processedAmount, CancellationToken cancellationToken)
        {
            var cards = new List<CVSModels.Card>();
            using (var reader = new StreamReader(_path + "cards.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    cards = csv.GetRecords<CVSModels.Card>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Card] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Card]");
                await _context.Card.AddRangeAsync(cards.Select(x => new Card()
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
                }), cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Card] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }

        private async Task<int> PopulatePlayerCardsAsync(int processedAmount, CancellationToken cancellationToken)
        {
            var playerCards = new List<CVSModels.PlayerCard>();
            using (var reader = new StreamReader(_path + "playerCards.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    playerCards = csv.GetRecords<CVSModels.PlayerCard>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerCard] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[PlayerCard]");
                await _context.PlayerCard.AddRangeAsync(playerCards.Select(x => new PlayerCard()
                {
                    CardId = x.CardId,
                    PlayerCardId = x.PlayerCardId,
                    PlayerId = x.PlayerId
                }), cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerCard] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }

        private async Task<int> PopulateTeamCardsAsync(int processedAmount, CancellationToken cancellationToken)
        {
            var teamCards = new List<CVSModels.TeamCard>();
            using (var reader = new StreamReader(_path + "teamCards.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    teamCards = csv.GetRecords<CVSModels.TeamCard>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[TeamCard] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[TeamCard]");
                await _context.TeamCard.AddRangeAsync(teamCards.Select(x => new TeamCard()
                {
                    CardId = x.CardId,
                    TeamCardId = x.TeamCardId,
                    TeamId = x.TeamId
                }), cancellationToken).ConfigureAwait(false);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[TeamCard] OFF");
                await transaction.CommitAsync();
            }

            return processedAmount;
        }
    }
}
