using CardOrgAPI.Constants;
using CardOrgAPI.Contexts;
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
            var query = _context.Card.AsQueryable();
            query = IncludeAllModels(query)
                    .Where(x => !String.IsNullOrWhiteSpace(x.FrontCardMainImagePath) &&
                    !String.IsNullOrWhiteSpace(x.BackCardMainImagePath))
                    .OrderByDescending(x => x.TimeStamp).Take(count);

            return await query.ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Card>> GetAsync(CardSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Card.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, filter.QuickSearchTerm);
            query = FullSearchSort(query, filter.SearchSortQueryFilter);
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
            var query = _context.Card.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, filter.QuickSearchTerm);
            query = FullSearchSort(query, filter.SearchSortQueryFilter);
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

        private IQueryable<Card> QuickSearch(IQueryable<Card> cards, string quickSearch)
        {
            quickSearch = quickSearch.ToLower();
            if (!String.IsNullOrWhiteSpace(quickSearch))
            {
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
            return cards;
        }

        private IQueryable<Card> FullSearchSort(IQueryable<Card> cards, SearchSortQueryFilter filter )
        {
            bool hasCustomOrder = false;
            if (!String.IsNullOrWhiteSpace(filter.CardDescription))
            {
                cards = cards.Where(x => x.CardDescription.ToLower().Contains(filter.CardDescription.ToLower()));
            }

            if (!String.IsNullOrWhiteSpace(filter.PlayerIds))
            {
                var players = filter.PlayerIds.Split(",").Select(x => int.Parse(x));
                cards = cards.Where(x => x.PlayerCards.Any(y => players.Contains(y.Player.PlayerId)));
            }

            if (!String.IsNullOrWhiteSpace(filter.TeamIds))
            {
                var teams = filter.TeamIds.Split(",").Select(x => int.Parse(x));
                cards = cards.Where(x => x.TeamCards.Any(y => teams.Contains(y.Team.TeamId)));
            }

            if (!String.IsNullOrWhiteSpace(filter.SportIds))
            {
                var sports = filter.SportIds.Split(",").Select(x => int.Parse(x));
                cards = cards.Where(x => sports.Contains(x.SportId.GetValueOrDefault()));
            }

            if (!String.IsNullOrWhiteSpace(filter.YearIds)) 
            {
                var years = filter.YearIds.Split(",").Select(x => int.Parse(x));
                cards = cards.Where(x => years.Contains(x.Year.YearId));
            }

            if (!String.IsNullOrWhiteSpace(filter.SetIds))
            {
                var sets = filter.SetIds.Split(",").Select(x => int.Parse(x));
                cards = cards.Where(x => sets.Contains(x.Set.SetId));
            }

            if (!String.IsNullOrWhiteSpace(filter.GradeCompanyIds))
            {
                var gradeCompanies = filter.GradeCompanyIds.Split(",").Select(x => int.Parse(x));
                cards = cards.Where(x => gradeCompanies.Contains(x.GradeCompany.GradeCompanyId));
            }


            if (!String.IsNullOrWhiteSpace(filter.LocationIds))
            {
                var locations = filter.LocationIds.Split(",").Select(x => int.Parse(x));
                cards = cards.Where(x => locations.Contains(x.Location.LocationId));
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

            if (filter.PlayerNameSort > 0)
            {
                hasCustomOrder = true;
                if (filter.PlayerNameSort == 1)
                {
                    cards = cards.OrderBy(x => x.PlayerCards.Select(p => p.Player).OrderBy(y => y.LastName).ThenBy(y => y.FirstName).FirstOrDefault().LastName).ThenBy(x => x.PlayerCards.Select(p => p.Player).OrderBy(y => y.LastName).ThenBy(y => y.FirstName).FirstOrDefault().FirstName).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.PlayerCards.Select(p => p.Player).OrderByDescending(y => y.LastName).ThenByDescending(y => y.FirstName).FirstOrDefault().LastName).ThenByDescending(x => x.PlayerCards.Select(p => p.Player).OrderByDescending(y => y.LastName).ThenByDescending(y => y.FirstName).FirstOrDefault().FirstName).ToList().AsQueryable();
                }
            }

            if (filter.TeamSort > 0)
            {
                hasCustomOrder = true;
                if (filter.TeamSort == 1)
                {
                    cards = cards.OrderBy(x => x.TeamCards.Select(p => p.Team).OrderBy(y => y.City).ThenBy(y => y.Name).FirstOrDefault().City).ThenBy(x => x.TeamCards.Select(p => p.Team).OrderBy(y => y.Name).ThenBy(y => y.Name).FirstOrDefault().Name).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.TeamCards.Select(p => p.Team).OrderByDescending(y => y.City).ThenByDescending(y => y.Name).FirstOrDefault().City).ThenByDescending(x => x.TeamCards.Select(p => p.Team).OrderByDescending(y => y.Name).ThenByDescending(y => y.Name).FirstOrDefault().Name).ToList().AsQueryable();
                }
            }

            if (filter.CardDescriptionSort > 0)
            {
                hasCustomOrder = true;
                if (filter.CardDescriptionSort == 1)
                {
                    cards = cards.OrderBy(x => x.CardDescription).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.CardDescription).ToList().AsQueryable();
                }
            }

            if (filter.LowestBeckettPriceSort > 0)
            {
                hasCustomOrder = true;
                if (filter.LowestBeckettPriceSort == 1)
                {
                    cards = cards.OrderBy(x => x.LowestBeckettPrice).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.LowestBeckettPrice).ToList().AsQueryable();
                }
            }

            if (filter.HighestBeckettPriceSort > 0)
            {
                hasCustomOrder = true;
                if (filter.HighestBeckettPriceSort == 1)
                {
                    cards = cards.OrderBy(x => x.HighestBeckettPrice).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.HighestBeckettPrice).ToList().AsQueryable();
                }
            }

            if (filter.LowestComcpriceSort > 0)
            {
                hasCustomOrder = true;
                if (filter.LowestComcpriceSort == 1)
                {
                    cards = cards.OrderBy(x => x.LowestComcprice).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.LowestComcprice).ToList().AsQueryable();
                }
            }

            if (filter.EbayPriceSort > 0)
            {
                hasCustomOrder = true;
                if (filter.EbayPriceSort == 1)
                {
                    cards = cards.OrderBy(x => x.EbayPrice).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.EbayPrice).ToList().AsQueryable();
                }
            }

            if (filter.PricePaidSort > 0)
            {
                hasCustomOrder = true;
                if (filter.PricePaidSort == 1)
                {
                    cards = cards.OrderBy(x => x.PricePaid).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.PricePaid).ToList().AsQueryable();
                }
            }

            if (filter.HasImageSort > 0)
            {
                hasCustomOrder = true;
                if (filter.HasImageSort == 1)
                {
                    cards = cards.OrderBy(x => x.FrontCardMainImagePath.ToLower() != CardConstants.MissingImage).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.FrontCardMainImagePath.ToLower() != CardConstants.MissingImage).ToList().AsQueryable();
                }
            }

            if (filter.IsGradedSort > 0)
            {
                hasCustomOrder = true;
                if (filter.IsGradedSort == 1)
                {
                    cards = cards.OrderBy(x => x.Grade > 0).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.Grade > 0).ToList().AsQueryable();
                }
            }

            if (filter.CopiesSort > 0)
            {
                hasCustomOrder = true;
                if (filter.CopiesSort == 1)
                {
                    cards = cards.OrderBy(x => x.Copies).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.Copies).ToList().AsQueryable();
                }
            }

            if (filter.SerialNumberSort > 0)
            {
                hasCustomOrder = true;
                if (filter.SerialNumberSort == 1)
                {
                    cards = cards.OrderBy(x => x.SerialNumber).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.SerialNumber).ToList().AsQueryable();
                }
            }

            if (filter.GradeSort > 0)
            {
                hasCustomOrder = true;
                if (filter.GradeSort == 1)
                {
                    cards = cards.OrderBy(x => x.Grade).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.Grade).ToList().AsQueryable();
                }
            }

            if (filter.IsRookieSort > 0)
            {
                hasCustomOrder = true;
                if (filter.IsRookieSort == 1)
                {
                    cards = cards.OrderBy(x => x.IsRookie).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.IsRookie).ToList().AsQueryable();
                }
            }

            if (filter.IsAutographSort > 0)
            {
                hasCustomOrder = true;
                if (filter.IsAutographSort == 1)
                {
                    cards = cards.OrderBy(x => x.IsAutograph).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.IsAutograph).ToList().AsQueryable();
                }
            }

            if (filter.IsPatchSort > 0)
            {
                hasCustomOrder = true;
                if (filter.IsPatchSort == 1)
                {
                    cards = cards.OrderBy(x => x.IsPatch).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.IsPatch).ToList().AsQueryable();
                }
            }

            if (filter.IsOnCardAutographSort > 0)
            {
                hasCustomOrder = true;
                if (filter.IsOnCardAutographSort == 1)
                {
                    cards = cards.OrderBy(x => x.IsOnCardAutograph).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.IsOnCardAutograph).ToList().AsQueryable();
                }
            }

            if (filter.IsGameWornJerseySort > 0)
            {
                hasCustomOrder = true;
                if (filter.IsGameWornJerseySort == 1)
                {
                    cards = cards.OrderBy(x => x.IsGameWornJersey).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.IsGameWornJersey).ToList().AsQueryable();
                }
            }

            if (filter.SportSort > 0)
            {
                hasCustomOrder = true;
                if (filter.SportSort == 1)
                {
                    cards = cards.OrderBy(x => x.Sport.Name).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.Sport.Name).ToList().AsQueryable();
                }
            }

            if (filter.YearSort > 0)
            {
                hasCustomOrder = true;
                if (filter.YearSort == 1)
                {
                    cards = cards.OrderBy(x => x.Year.BeginningYear).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.Year.BeginningYear).ToList().AsQueryable();
                }
            }

            if (filter.SetSort > 0)
            {
                hasCustomOrder = true;
                if (filter.SetSort == 1)
                {
                    cards = cards.OrderBy(x => x.Set.Name).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.Set.Name).ToList().AsQueryable();
                }
            }

            if (filter.GradeCompanySort > 0)
            {
                hasCustomOrder = true;
                if (filter.GradeCompanySort == 1)
                {
                    cards = cards.OrderBy(x => x.GradeCompany != null ? x.GradeCompany.Name : string.Empty).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.GradeCompany != null ? x.GradeCompany.Name : string.Empty).ToList().AsQueryable();
                }
            }

            if (filter.LocationSort > 0)
            {
                hasCustomOrder = true;
                if (filter.LocationSort == 1)
                {
                    cards = cards.OrderBy(x => x.Location.Name).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.Location.Name).ToList().AsQueryable();
                }
            }

            if (filter.TimeStampSort > 0)
            {
                hasCustomOrder = true;
                if (filter.TimeStampSort == 1)
                {
                    cards = cards.OrderBy(x => x.TimeStamp).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.TimeStamp).ToList().AsQueryable();
                }
            }

            if (!hasCustomOrder)
            {
                cards = cards.OrderByDescending(x => x.TimeStamp);
            }

            return cards;
        }
    }
}
