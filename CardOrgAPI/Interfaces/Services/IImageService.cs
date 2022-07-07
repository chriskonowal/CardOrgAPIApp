using CardOrgAPI.Contexts;
using CardOrgAPI.Model;
using CardOrgAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Services
{
    public interface IImageService
    {
        Task ResizeImageAsync(string fullPath,
            Dimensions newDimensions);

        Task<FileContext> SavePicturesAsync(CardRequest model, 
            CancellationToken cancellationToken);
    }
}
