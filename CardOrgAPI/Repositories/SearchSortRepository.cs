using CardOrgAPI.Converters;
using CardOrgAPI.Entities;
using CardOrgAPI.Helpers;
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
                await SavePlayersAsync(model.SearchSortId, queryFilter, cancellationToken).ConfigureAwait(false);
                await SaveSetsAsync(model.SearchSortId, queryFilter, cancellationToken).ConfigureAwait(false);
                await SaveSportsAsync(model.SearchSortId, queryFilter, cancellationToken).ConfigureAwait(false);
                await SaveTeamsAsync(model.SearchSortId, queryFilter, cancellationToken).ConfigureAwait(false);
                await SaveYearsAsync(model.SearchSortId, queryFilter, cancellationToken).ConfigureAwait(false);

                var result = 1;
                return result == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public async Task<IEnumerable<SearchSort>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.SearchSorts.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, filter.SearchTerm);
            query = RepositoryHelpers.Paging(query, filter.RowsPerPage, filter.PageNumber);
            try
            {
                return await Task.FromResult(query.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int GetTotal(string searchTerm)
        {
            var query = _context.SearchSorts.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, searchTerm);
            return query.Count();
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
            foreach (var player in queryFilter.PlayerIds)
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

        private async Task SaveSetsAsync(int searchSortId,
            SearchSortQueryFilter queryFilter,
            CancellationToken cancellationToken)
        {

            var sets = new List<SearchSortSet>();
            foreach (var set in queryFilter.SetIds)
            {
                var relationModel = new SearchSortSet()
                {
                    SetId = set,
                    SearchSortId = searchSortId
                };
                sets.Add(relationModel);
            }

            await _context.SearchSortSets.AddRangeAsync(sets, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SaveSportsAsync(int searchSortId,
            SearchSortQueryFilter queryFilter,
            CancellationToken cancellationToken)
        {

            var sports = new List<SearchSortSport>();
            foreach (var sport in queryFilter.SportIds)
            {
                var relationModel = new SearchSortSport()
                {
                    SportId = sport,
                    SearchSortId = searchSortId
                };
                sports.Add(relationModel);
            }

            await _context.SearchSortSports.AddRangeAsync(sports, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SaveTeamsAsync(int searchSortId,
            SearchSortQueryFilter queryFilter,
            CancellationToken cancellationToken)
        {

            var teams = new List<SearchSortTeam>();
            foreach (var team in queryFilter.TeamIds)
            {
                var relationModel = new SearchSortTeam()
                {
                    TeamId = team,
                    SearchSortId = searchSortId
                };
                teams.Add(relationModel);
            }

            await _context.SearchSortTeams.AddRangeAsync(teams, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SaveYearsAsync(int searchSortId,
            SearchSortQueryFilter queryFilter,
            CancellationToken cancellationToken)
        {

            var years = new List<SearchSortYear>();
            foreach (var year in queryFilter.YearIds)
            {
                var relationModel = new SearchSortYear()
                {
                    YearId = year,
                    SearchSortId = searchSortId
                };
                years.Add(relationModel);
            }

            await _context.SearchSortYears.AddRangeAsync(years, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private IQueryable<SearchSort> IncludeAllModels(IQueryable<SearchSort> query)
        {
            query = query.Include(x => x.SearchSortPlayers).ThenInclude(x => x.Player);
            query = query.Include(x => x.SearchSortTeams).ThenInclude(x => x.Team);
            query = query.Include(x => x.SearchSortGradeCompanies).ThenInclude(x => x.GradeCompany);
            query = query.Include(x => x.SearchSortLocations).ThenInclude(x => x.Location);
            query = query.Include(x => x.SearchSortSets).ThenInclude(x => x.Set);
            query = query.Include(x => x.SearchSortSports).ThenInclude(x => x.Sport);
            query = query.Include(x => x.SearchSortYears).ThenInclude(x => x.Year);

            return query;
        }

        private IQueryable<SearchSort> QuickSearch(IQueryable<SearchSort> searchSorts, string quickSearch)
        {
            if (!String.IsNullOrWhiteSpace(quickSearch))
            {
                quickSearch = quickSearch.ToLower();
                searchSorts = searchSorts.Where(x => x.Name.ToLower().Contains(quickSearch) ||
                x.Description.ToLower().Contains(quickSearch));
            }
            return searchSorts;
        }
    }
}
