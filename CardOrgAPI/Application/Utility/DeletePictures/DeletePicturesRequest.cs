using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Utility.DeletePictures
{
    public class DeletePicturesRequest : IRequest<ApiResponse<int>>
    {

    }
}
