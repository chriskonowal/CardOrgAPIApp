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

        public PopulateTablesRequestHandler(CardOrgContext context,
            IHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<ApiResponse<PopulateTablesResponse>> Handle(PopulateTablesRequest request, CancellationToken cancellationToken)
        {
            var startTime = DateTime.Now;
            int processedAmount = 0;
            var path = _hostEnvironment.ContentRootPath + @"\Database\";

            //years
            var years = new List<Year>();
            using (var reader = new StreamReader(path + "years.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    years = csv.GetRecords<Year>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Years] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Years]");
                await _context.Years.AddRangeAsync(years);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Years] OFF");
                await transaction.CommitAsync();
            }

            //teams
            var teams = new List<Team>();
            using (var reader = new StreamReader(path + "teams.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    teams = csv.GetRecords<Team>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Teams] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Teams]");
                await _context.Teams.AddRangeAsync(teams);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Teams] OFF");
                await transaction.CommitAsync();
            }

            //sports
            var sports = new List<Sport>();
            using (var reader = new StreamReader(path + "sports.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    sports = csv.GetRecords<Sport>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Sports] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Sports]");
                await _context.Sports.AddRangeAsync(sports);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Sports] OFF");
                await transaction.CommitAsync();
            }

            //sets
            var sets = new List<Set>();
            using (var reader = new StreamReader(path + "sets.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    sets = csv.GetRecords<Set>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Sets] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Sets]");
                await _context.Sets.AddRangeAsync(sets);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Sets] OFF");
                await transaction.CommitAsync();
            }

            //players
            var players = new List<Player>();
            using (var reader = new StreamReader(path + "players.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    players = csv.GetRecords<Player>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Players] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Players]");
                await _context.Players.AddRangeAsync(players);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Players] OFF");
                await transaction.CommitAsync();
            }

            //locations
            var locations = new List<Location>();
            using (var reader = new StreamReader(path + "locations.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    locations = csv.GetRecords<Location>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Locations] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Locations]");
                await _context.Locations.AddRangeAsync(locations);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Locations] OFF");
                await transaction.CommitAsync();
            }

            //gradeCompanies
            var gradeCompanies = new List<GradeCompany>();
            using (var reader = new StreamReader(path + "gradeCompanies.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    gradeCompanies = csv.GetRecords<GradeCompany>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[GradeCompanies] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[GradeCompanies]");
                await _context.GradeCompanies.AddRangeAsync(gradeCompanies);
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[GradeCompanies] OFF");
                await transaction.CommitAsync();
            }


            //cards
            var cards = new List<CVSModels.Card>();
            try
            {
                using (var reader = new StreamReader(path + "cards.csv"))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        cards = csv.GetRecords<CVSModels.Card>().ToList();
                    }
                }

                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Cards] ON");
                    _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[Cards]");
                    await _context.Cards.AddRangeAsync(cards.Select(x =>new Card() { 
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
                    }));
                    processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                    _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[Cards] OFF");
                    await transaction.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            

            //playerCards
            var playerCards = new List<CVSModels.PlayerCard>();
            using (var reader = new StreamReader(path + "playerCards.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    playerCards = csv.GetRecords<CVSModels.PlayerCard>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerCards] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[PlayerCards]");
                await _context.PlayerCards.AddRangeAsync(playerCards.Select(x => new PlayerCard() { 
                    CardId = x.CardId,
                    PlayerCardId = x.PlayerCardId,
                    PlayerId = x.PlayerId
                }));
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[PlayerCards] OFF");
                await transaction.CommitAsync();
            }

            //teamCards
            var teamCards = new List<CVSModels.TeamCard>();
            using (var reader = new StreamReader(path + "teamCards.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    teamCards = csv.GetRecords<CVSModels.TeamCard>().ToList();
                }
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[TeamCards] ON");
                _context.Database.ExecuteSqlRaw(@"DELETE FROM [dbo].[TeamCards]");
                await _context.TeamCards.AddRangeAsync(teamCards.Select(x => new TeamCard() { 
                    CardId = x.CardId,
                    TeamCardId = x.TeamCardId,
                    TeamId = x.TeamId
                }));
                processedAmount += await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                _context.Database.ExecuteSqlRaw(@"SET IDENTITY_INSERT [dbo].[TeamCards] OFF");
                await transaction.CommitAsync();
            }

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
    }
}
