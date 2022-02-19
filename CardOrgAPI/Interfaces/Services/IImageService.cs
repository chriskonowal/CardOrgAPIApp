using CardOrgAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Services
{
    public interface IImageService
    {
        Task ResizeImageAsync(string fullPath,
            Dimensions newDimensions);
    }
}
