using CardOrgAPI.Converters;
using CardOrgAPI.Entities;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.QueryFilters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Repositories
{
    public class SearchSortRepository : ISearchSortRepository
    {
        private readonly CardOrgContext _context;

        public SearchSortRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync(SearchSortQueryFilter queryFilter, CancellationToken cancellationToken)
        {
            try
            {
                var model = SearchSortConverter.Convert(queryFilter);

                //delete all relationships
                if (model.SearchSortId > 0)
                {
                    if (queryFilter.GradeCompanyIds.Any())
                    {
                        _context.Database.ExecuteSqlRaw($@"DELETE FROM [dbo].[SearchSortGradeCompany] WHERE SearchSortId = {model.SearchSortId}");
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    if (queryFilter.LocationIds.Any())
                    {
                        _context.Database.ExecuteSqlRaw($@"DELETE FROM [dbo].[SearchSortLocation] WHERE SearchSortId = {model.SearchSortId}");
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    if (queryFilter.PlayerIds.Any())
                    {
                        _context.Database.ExecuteSqlRaw($@"DELETE FROM [dbo].[SearchSortPlayer] WHERE SearchSortId = {model.SearchSortId}");
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    if (queryFilter.SetIds.Any())
                    {
                        _context.Database.ExecuteSqlRaw($@"DELETE FROM [dbo].[SearchSortSet] WHERE SearchSortId = {model.SearchSortId}");
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    if (queryFilter.SportIds.Any())
                    {
                        _context.Database.ExecuteSqlRaw($@"DELETE FROM [dbo].[SearchSortSport] WHERE SearchSortId = {model.SearchSortId}");
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    if (queryFilter.TeamIds.Any())
                    {
                        _context.Database.ExecuteSqlRaw($@"DELETE FROM [dbo].[SearchSortTeam] WHERE SearchSortId = {model.SearchSortId}");
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    if (queryFilter.YearIds.Any())
                    {
                        _context.Database.ExecuteSqlRaw($@"DELETE FROM [dbo].[SearchSortYear] WHERE SearchSortId = {model.SearchSortId}");
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
                await _context.SearchSorts.AddAsync(model, cancellationToken).ConfigureAwait(false);
                if (model.SearchSortId > 0)
                {
                    _context.Entry(model).State = EntityState.Modified;
                   
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);
                await SaveGradeCompaniesAsync(model.SearchSortId, queryFilter, cancellationToken).ConfigureAwait(false);
                await SaveLocationsAsync(model.SearchSortId, queryFilter, cancellationToken).ConfigureAwait(false);



                var result = 1;
                return result == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        private async Task SaveGradeCompaniesAsync(int searchSortId, 
            SearchSortQueryFilter queryFilter, 
            CancellationToken cancellationToken)
        {

                var gradeCompanies = new List<SearchSortGradeCompany>();
                foreach (var gradeCompany in queryFilter.GradeCompanyIds)
                {
                    var relationModel = new SearchSortGradeCompany()
                    {
                        GradeCompanyId = gradeCompany,
                        SearchSortId = searchSortId
                    };
                    gradeCompanies.Add(relationModel);

                }

                await _context.SearchSortGradeCompanies.AddRangeAsync(gradeCompanies, cancellationToken).ConfigureAwait(false);
                await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SaveLocationsAsync(int searchSortId,
            SearchSortQueryFilter queryFilter,
            CancellationToken cancellationToken)
        {

            var locations = new List<SearchSortLocation>();
            foreach (var location in queryFilter.LocationIds)
            {
                var relationModel = new SearchSortLocation()
                {
                    LocationId = location,
                    SearchSortId = searchSortId
                };
                locations.Add(relationModel);

            }

            await _context.SearchSortLocations.AddRangeAsync(locations, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SavePlayersAsync(int searchSortId,
            SearchSortQueryFilter queryFilter,
            CancellationToken cancellationToken)
        {

            var players = new List<SearchSortPlayer>();
            foreach (var player in queryFilter.LocationIds)
            {
                var relationModel = new SearchSortPlayer()
                {
                    PlayerId = player,
                    SearchSortId = searchSortId
                };
                players.Add(relationModel);

            }

            await _context.SearchSortPlayers.AddRangeAsync(players, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
