﻿using CardOrgAPI.Contexts;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Cards.RookieAutoPatchGraph
{
    public class RookieAutoPatchGraphRequestHandler : IRequestHandler<RookieAutoPatchGraphRequest, ApiResponse<RookieAutoPatchGraphResponse>>
    {
        private readonly CardOrgContext _context;

        public RookieAutoPatchGraphRequestHandler(CardOrgContext context)
        {
            _context = context;
        }

        public Task<ApiResponse<RookieAutoPatchGraphResponse>> Handle(RookieAutoPatchGraphRequest request, CancellationToken cancellationToken)
        {
            var rookieCount = _context.Card
                .Where(x => x.IsRookie).Count();

            var autosCount = _context.Card
                .Where(x => x.IsAutograph).Count();

            var patchCount = _context.Card
                .Where(x => x.IsPatch).Count();

            return Task.FromResult(new ApiResponse<RookieAutoPatchGraphResponse>()
            {
                IsSuccessful = true,
                Value = new RookieAutoPatchGraphResponse()
                {
                    Autos = autosCount,
                    Patches = patchCount,
                    Rookies = rookieCount
                }
            });
        }
    }
}
