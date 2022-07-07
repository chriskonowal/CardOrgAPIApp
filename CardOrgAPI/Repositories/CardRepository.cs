using CardOrgAPI.Constants;
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
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly CardOrgContext _context;

        public CardRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetCardsByAmountAsync(int count, CancellationToken cancellationToken)
        {
            var query = _context.Cards.AsQueryable();
            query = IncludeAllModels(query)
                    .Where(x => !String.IsNullOrWhiteSpace(x.FrontCardMainImagePath) &&
                    !String.IsNullOrWhiteSpace(x.BackCardMainImagePath))
                    .OrderByDescending(x => x.TimeStamp).Take(count);

            return await query.ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Card>> GetAsync(CardSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Cards.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, filter);

            if (String.IsNullOrEmpty(filter.QuickSearchTerm) && String.IsNullOrEmpty(filter.SortByField))
            {
                query = FullSearchSort(query, filter.SearchSortQueryFilter);
            }

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

        public int GetTotal(CardSearchQueryFilter filter)
        {
            var query = _context.Cards.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, filter);
            if (String.IsNullOrEmpty(filter.QuickSearchTerm) && String.IsNullOrEmpty(filter.SortByField))
            {
                query = FullSearchSort(query, filter.SearchSortQueryFilter);
            }
            return query.Count();
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

        private IQueryable<Card> QuickSearch(IQueryable<Card> cards, CardSearchQueryFilter filter)
        {
            
            if (!String.IsNullOrWhiteSpace(filter.QuickSearchTerm))
            {
                string quickSearch = filter.QuickSearchTerm.ToLower();
                cards = cards.Where(x => x.CardDescription.ToLower().Contains(quickSearch) || 
                x.CardNumber.ToLower().Contains(quickSearch) || 
                x.GradeCompany.Name.ToLower().Contains(quickSearch) || 
                x.Location.Name.ToLower().Contains(quickSearch) ||
                x.PlayerCards.Any(x => x.Player.FirstName.ToLower().Contains(quickSearch) || 
                x.Player.LastName.ToLower().Contains(quickSearch)) ||
                x.Set.Name.ToLower().Contains(quickSearch) || 
                x.Sport.Name.ToLower().Contains(quickSearch) || 
                x.TeamCards.Any(x => x.Team.City.ToLower().Contains(quickSearch) ||
                x.Team.Name.ToLower().Contains(quickSearch)));
            }

            if (!String.IsNullOrWhiteSpace(filter.SortByField))
            {
                if (filter.SortByField.ToLower().Equals("pictures"))
                {
                    if (filter.IsSortDesc)
                    {
                        cards = cards.OrderByDescending(x => !string.IsNullOrEmpty(x.FrontCardMainImagePath));
                    }
                    else
                    {
                        cards = cards.OrderBy(x => !string.IsNullOrEmpty(x.FrontCardMainImagePath));
                    }
                }

                if (filter.SortByField.ToLower().Equals("year"))
                {
                    if (filter.IsSortDesc)
                    {
                        cards = cards.OrderByDescending(x => x.Year.BeginningYear);
                    }
                    else
                    {
                        cards = cards.OrderBy(x => x.Year.BeginningYear);
                    }
                }

                if (filter.SortByField.ToLower().Equals("carddescription"))
                {
                    if (filter.IsSortDesc)
                    {
                        cards = cards.OrderByDescending(x => x.CardDescription);
                    }
                    else
                    {
                        cards = cards.OrderBy(x => x.CardDescription);
                    }
                }

                if (filter.SortByField.ToLower().Equals("cardnumber"))
                {
                    if (filter.IsSortDesc)
                    {
                        cards = cards.OrderByDescending(x => x.CardNumber);
                    }
                    else
                    {
                        cards = cards.OrderBy(x => x.CardNumber);
                    }
                }
            }

            return cards;
        }

        public async Task<bool> InsertAsync(CardQueryFilter queryFilter, CancellationToken cancellationToken)
        {
            try
            {
                var model = CardQueryFilterConverter.Convert(queryFilter);

                //delete all relationships
                if (model.CardId > 0)
                {
                    await DeleteRelationshipsAsync(model.CardId, cancellationToken).ConfigureAwait(false);
                }
                model.TimeStamp = DateTime.Now;
                await _context.Cards.AddAsync(model, cancellationToken).ConfigureAwait(false);
                if (model.CardId > 0)
                {
                    _context.Entry(model).State = EntityState.Modified;
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);
                await SavePlayersAsync(model.CardId, queryFilter, cancellationToken).ConfigureAwait(false);
                await SaveTeamsAsync(model.CardId, queryFilter, cancellationToken).ConfigureAwait(false);

                var result = 1;
                return result == 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id > 0)
            {
                var card = new Card { CardId = id };
                await DeleteRelationshipsAsync(id, cancellationToken).ConfigureAwait(false);
                if (_context.Entry(card) == null)
                {
                    return false;
                }
                _context.Entry(card).State = EntityState.Deleted;
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result == 1;
            }
            return true;
        }

        private async Task DeleteRelationshipsAsync(int id, CancellationToken cancellationToken)
        {
            _context.Database.ExecuteSqlRaw($@"DELETE FROM [dbo].[PlayerCard] WHERE CardId = {id}");
            await _context.SaveChangesAsync(cancellationToken);

            _context.Database.ExecuteSqlRaw($@"DELETE FROM [dbo].[TeamCard] WHERE CardId = {id}");
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SavePlayersAsync(int cardId,
            CardQueryFilter queryFilter,
            CancellationToken cancellationToken)
        {
            var players = new List<PlayerCard>();
            foreach (var player in queryFilter.Players)
            {
                var relationModel = new PlayerCard()
                {
                    PlayerId = player,
                    CardId = cardId
                };
                players.Add(relationModel);
            }

            await _context.PlayerCards.AddRangeAsync(players, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SaveTeamsAsync(int cardId,
            CardQueryFilter queryFilter,
            CancellationToken cancellationToken)
        {
            var teams = new List<TeamCard>();
            foreach (var player in queryFilter.Teams)
            {
                var relationModel = new TeamCard()
                {
                    TeamId = player,
                    CardId = cardId
                };
                teams.Add(relationModel);
            }

            await _context.TeamCards.AddRangeAsync(teams, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private IQueryable<Card> FullSearchSort(IQueryable<Card> cards, SearchSortQueryFilter filter )
        {
            if (!String.IsNullOrWhiteSpace(filter.CardDescription))
            {
                cards = cards.Where(x => x.CardDescription.ToLower().Contains(filter.CardDescription.ToLower()));
            }

            if ((filter.PlayerIds?.Any()).GetValueOrDefault())
            {
                cards = cards.Where(x => x.PlayerCards.Any(y => filter.PlayerIds.Contains(y.Player.PlayerId)));
            }

            if ((filter.TeamIds?.Any()).GetValueOrDefault())
            {
                cards = cards.Where(x => x.TeamCards.Any(y => filter.TeamIds.Contains(y.Team.TeamId)));
            }

            if ((filter.SportIds?.Any()).GetValueOrDefault())
            {
                cards = cards.Where(x => filter.SportIds.Contains(x.SportId.GetValueOrDefault()));
            }

            if ((filter.YearIds?.Any()).GetValueOrDefault()) 
            {
                cards = cards.Where(x => filter.YearIds.Contains(x.Year.YearId));
            }

            if ((filter.SetIds?.Any()).GetValueOrDefault())
            {
                cards = cards.Where(x => filter.SetIds.Contains(x.Set.SetId));
            }

            if ((filter.GradeCompanyIds?.Any()).GetValueOrDefault())
            {
                cards = cards.Where(x => filter.GradeCompanyIds.Contains(x.GradeCompany.GradeCompanyId));
            }

            if ((filter.LocationIds?.Any()).GetValueOrDefault())
            {
                cards = cards.Where(x => filter.LocationIds.Contains(x.Location.LocationId));
            }

            if (filter.IsGraded)
            {
                cards = cards.Where(x => x.IsGraded);
            }

            if (filter.IsRookie)
            {
                cards = cards.Where(x => x.IsRookie);
            }

            if (filter.IsAutograph)
            {
                cards = cards.Where(x => x.IsAutograph);
            }

            if (filter.IsOnCardAutograph)
            {
                cards = cards.Where(x => x.IsOnCardAutograph);
            }

            if (filter.IsPatch)
            {
                cards = cards.Where(x => x.IsPatch);
            }

            if (filter.IsGameWornJersey)
            {
                cards = cards.Where(x => x.IsGameWornJersey);
            }

            if (filter.HasImage)
            {
                cards = cards.Where(x => x.FrontCardMainImagePath.ToLower() != CardConstants.MissingImage);
            }

            if (filter.IsSerialNumbered)
            {
                cards = cards.Where(x => x.SerialNumber > 0);
            }

            if (filter.LowestBeckettPriceLow >= 0 && filter.LowestBeckettPriceHigh > 0)
            {
                cards = cards.Where(x => x.LowestBeckettPrice >= filter.LowestBeckettPriceLow &&
                x.LowestBeckettPrice <= filter.LowestBeckettPriceHigh);
            }

            if (filter.HighestBeckettPriceLow >= 0 && filter.HighestBeckettPriceHigh > 0)
            {
                cards = cards.Where(x => x.HighestBeckettPrice >= filter.HighestBeckettPriceLow &&
                x.HighestBeckettPrice <= filter.HighestBeckettPriceHigh);
            }

            if (filter.LowestComcpriceLow >= 0 && filter.LowestComcpriceHigh > 0)
            {
                cards = cards.Where(x => x.LowestComcprice >= filter.LowestComcpriceLow &&
                x.LowestComcprice <= filter.LowestComcpriceHigh);
            }
            
            if (filter.EbayPriceLow >= 0 && filter.EbayPriceHigh > 0)
            {
                cards = cards.Where(x => x.EbayPrice >= filter.EbayPriceLow &&
                x.EbayPrice <= filter.EbayPriceHigh);
            }

            if (filter.PricePaidLow >= 0 && filter.PricePaidHigh > 0)
            {
                cards = cards.Where(x => x.PricePaid >= filter.PricePaidLow &&
                x.PricePaid <= filter.PricePaidHigh);
            }

            if (filter.GradeLow >= 0 && filter.GradeHigh > 0)
            {
                cards = cards.Where(x => x.Grade >= filter.GradeLow &&
                x.Grade <= filter.GradeHigh);
            }

            if (filter.CopiesLow >= 0 && filter.CopiesHigh > 0)
            {
                cards = cards.Where(x => x.Copies >= filter.CopiesLow &&
                x.Copies <= filter.CopiesHigh);
            }

            if (filter.SerialNumberLow >= 0 && filter.SerialNumberHigh > 0)
            {
                cards = cards.Where(x => x.SerialNumber >= filter.SerialNumberLow &&
                x.SerialNumber <= filter.SerialNumberHigh);
            }

            var orderedCards = cards.OrderByDescending(x => 0);

            if (filter.PlayerNameSort > 0)
            {
                if (filter.PlayerNameSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.PlayerCards.Select(p => p.Player).OrderBy(y => y.LastName).ThenBy(y => y.FirstName).FirstOrDefault().LastName).ThenBy(x => x.PlayerCards.Select(p => p.Player).OrderBy(y => y.LastName).ThenBy(y => y.FirstName).FirstOrDefault().FirstName);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.PlayerCards.Select(p => p.Player).OrderByDescending(y => y.LastName).ThenByDescending(y => y.FirstName).FirstOrDefault().LastName).ThenByDescending(x => x.PlayerCards.Select(p => p.Player).OrderByDescending(y => y.LastName).ThenByDescending(y => y.FirstName).FirstOrDefault().FirstName);
                }
            }

            if (filter.TeamSort > 0)
            {
                if (filter.TeamSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.TeamCards.Select(p => p.Team).OrderBy(y => y.City).ThenBy(y => y.Name).FirstOrDefault().City).ThenBy(x => x.TeamCards.Select(p => p.Team).OrderBy(y => y.Name).ThenBy(y => y.Name).FirstOrDefault().Name);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.TeamCards.Select(p => p.Team).OrderByDescending(y => y.City).ThenByDescending(y => y.Name).FirstOrDefault().City).ThenByDescending(x => x.TeamCards.Select(p => p.Team).OrderByDescending(y => y.Name).ThenByDescending(y => y.Name).FirstOrDefault().Name);
                }
            }

            if (filter.CardDescriptionSort > 0)
            {
                if (filter.CardDescriptionSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.CardDescription);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.CardDescription);
                }
            }

            if (filter.LowestBeckettPriceSort > 0)
            {
                if (filter.LowestBeckettPriceSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.LowestBeckettPrice);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.LowestBeckettPrice);
                }
            }

            if (filter.HighestBeckettPriceSort > 0)
            {
                if (filter.HighestBeckettPriceSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.HighestBeckettPrice);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.HighestBeckettPrice);
                }
            }

            if (filter.LowestComcpriceSort > 0)
            {
                if (filter.LowestComcpriceSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.LowestComcprice);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.LowestComcprice);
                }
            }

            if (filter.EbayPriceSort > 0)
            {
                if (filter.EbayPriceSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.EbayPrice);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.EbayPrice);
                }
            }

            if (filter.PricePaidSort > 0)
            {
                if (filter.PricePaidSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.PricePaid);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.PricePaid);
                }
            }

            if (filter.HasImageSort > 0)
            {
                if (filter.HasImageSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.FrontCardMainImagePath.ToLower() != CardConstants.MissingImage);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.FrontCardMainImagePath.ToLower() != CardConstants.MissingImage);
                }
            }

            if (filter.IsGradedSort > 0)
            {
                if (filter.IsGradedSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.Grade > 0);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.Grade > 0);
                }
            }

            if (filter.CopiesSort > 0)
            {
                if (filter.CopiesSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.Copies);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.Copies);
                }
            }

            if (filter.SerialNumberSort > 0)
            {
                if (filter.SerialNumberSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.SerialNumber);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.SerialNumber);
                }
            }

            if (filter.GradeSort > 0)
            {
                if (filter.GradeSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.Grade);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.Grade);
                }
            }

            if (filter.IsRookieSort > 0)
            {
                if (filter.IsRookieSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.IsRookie);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.IsRookie);
                }
            }

            if (filter.IsAutographSort > 0)
            {
                if (filter.IsAutographSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.IsAutograph);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.IsAutograph);
                }
            }

            if (filter.IsPatchSort > 0)
            {
                if (filter.IsPatchSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.IsPatch);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.IsPatch);
                }
            }

            if (filter.IsOnCardAutographSort > 0)
            {
                if (filter.IsOnCardAutographSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.IsOnCardAutograph);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.IsOnCardAutograph);
                }
            }

            if (filter.IsGameWornJerseySort > 0)
            {
                if (filter.IsGameWornJerseySort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.IsGameWornJersey);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.IsGameWornJersey);
                }
            }

            if (filter.SportSort > 0)
            {
                if (filter.SportSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.Sport.Name);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.Sport.Name);
                }
            }

            if (filter.YearSort > 0)
            {
                if (filter.YearSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.Year.BeginningYear);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.Year.BeginningYear);
                }
            }

            if (filter.SetSort > 0)
            { 
                if (filter.SetSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.Set.Name);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.Set.Name);
                }
            }

            if (filter.GradeCompanySort > 0)
            {
                if (filter.GradeCompanySort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.GradeCompany != null ? x.GradeCompany.Name : string.Empty);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.GradeCompany != null ? x.GradeCompany.Name : string.Empty);
                }
            }

            if (filter.LocationSort > 0)
            {
                if (filter.LocationSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.Location.Name);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.Location.Name);
                }
            }

            if (filter.TimeStampSort > 0)
            {
                if (filter.TimeStampSort == 1)
                {
                    orderedCards = orderedCards.ThenBy(x => x.TimeStamp);
                }
                else
                {
                    orderedCards = orderedCards.ThenByDescending(x => x.TimeStamp);
                }
            }

            return orderedCards.ToList().AsQueryable();
        }
    }
}
