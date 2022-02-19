using CardOrgAPI.Interfaces.Services;
using CardOrgAPI.Model;
using ImageMagick;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly string _savePath; 

        public ImageService(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _savePath = _hostEnvironment.ContentRootPath + @"\clientapp\public\Uploads\Large\";
        }

        public async Task ResizeImageAsync(string fullPath, 
            Dimensions newDimensions)
        {
            using (var image = new MagickImage(fullPath))
            {
                Dimensions orginalDimensions = new Dimensions()
                {
                    Width = image.Width,
                    Height = image.Height
                };

                var resizedDimensions = ResizeMaintainAspectRatio(orginalDimensions, newDimensions);
                FileInfo fileInfo = new FileInfo(fullPath);
                image.Resize(resizedDimensions.Width, resizedDimensions.Height);
                await image.WriteAsync(_savePath + fileInfo.Name);
            }
        }

        private Dimensions ResizeMaintainAspectRatio(Dimensions orginalDimensions, Dimensions newDimensions)
        {
            var originalHeight = orginalDimensions.Height;
            var originalWidth = orginalDimensions.Width;
            double ratioX = (double)newDimensions.Width / (double)originalWidth;
            double ratioY = (double)newDimensions.Height / (double)originalHeight;
            // use whichever multiplier is smaller
            double ratio = ratioX < ratioY ? ratioX : ratioY;
            // now we can get the new height and width
            int newHeight = Convert.ToInt32(originalHeight * ratio);
            int newWidth = Convert.ToInt32(originalWidth * ratio);
            return new Dimensions() {
                Height = newHeight,
                Width = newWidth
            };
        }
    }
}
