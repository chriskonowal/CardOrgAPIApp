using CardOrgAPI.Contexts;
using CardOrgAPI.Extensions;
using CardOrgAPI.Interfaces.Services;
using CardOrgAPI.Model;
using CardOrgAPI.Requests;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly string _largeSavePath;
        private readonly string _midSavePath;
        private readonly string _thumbSavePath;
        private readonly string _tempSavePath;
        private const string DEFAULT_IMAGE_NAME = "bball.jpg";

        public ImageService(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _largeSavePath = _hostEnvironment.ContentRootPath + @"\clientapp\public\Uploads\Large\";
            _midSavePath = _hostEnvironment.ContentRootPath + @"\clientapp\public\Uploads\Mid\";
            _thumbSavePath = _hostEnvironment.ContentRootPath + @"\clientapp\public\Uploads\Thumb\";
            _tempSavePath = _hostEnvironment.ContentRootPath + @"\clientapp\public\Uploads\Temp\";
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
                await image.WriteAsync(_largeSavePath + fileInfo.Name);
            }
        }

        public async Task<FileContext> SavePicturesAsync(CardRequest model, CancellationToken cancellationToken)
        {
            var fileContext = new FileContext();
            if (model.FrontImage == null &&
                String.IsNullOrWhiteSpace(model.FrontCardMainImagePath) &&
                String.IsNullOrWhiteSpace(model.FrontCardThumbnailImagePath))
            {
                fileContext.FrontMainFileName = DEFAULT_IMAGE_NAME;
                fileContext.FrontThumbnailFileName = DEFAULT_IMAGE_NAME;
            }
            else if (model.FrontImage == null &&
                !String.IsNullOrWhiteSpace(model.FrontCardMainImagePath) &&
                !String.IsNullOrWhiteSpace(model.FrontCardThumbnailImagePath))
            {
                fileContext.FrontMainFileName = model.FrontCardMainImagePath;
                fileContext.FrontThumbnailFileName = model.FrontCardThumbnailImagePath;
            }
            else
            {
                var fileName = Path.GetFileNameWithoutExtension($"{model.CardDescription.ReturnAlphaNumericCharacters()}{model.CardNumber.ReturnAlphaNumericCharacters()}{model.YearId}_front");
                fileName = fileName.StripInvalidFileNameCharacters();
                var extension = Path.GetExtension(model.FrontImage.FileName);
                if (!String.IsNullOrWhiteSpace(fileName))
                {
                    await SaveResizedImagesAsync(fileName, extension, model.FrontImage, cancellationToken).ConfigureAwait(false);

                    fileContext.FrontMainFileName = $"{fileName}{extension}";
                    fileContext.FrontThumbnailFileName = $"{fileName}_thumb{extension}";
                }
                else
                {
                    fileContext.FrontMainFileName = DEFAULT_IMAGE_NAME;
                    fileContext.FrontThumbnailFileName = DEFAULT_IMAGE_NAME;
                }
            }

            if (model.BackImage == null &&
                String.IsNullOrWhiteSpace(model.BackCardMainImagePath) &&
                String.IsNullOrWhiteSpace(model.BackCardThumbnailImagePath))
            {
                fileContext.BackMainFileName = DEFAULT_IMAGE_NAME;
                fileContext.BackThumbnailFileName = DEFAULT_IMAGE_NAME;
                return fileContext;
            }
            else if (model.BackImage == null &&
               !String.IsNullOrWhiteSpace(model.BackCardMainImagePath) &&
               !String.IsNullOrWhiteSpace(model.BackCardThumbnailImagePath))
            {
                fileContext.BackMainFileName = model.BackCardMainImagePath;
                fileContext.BackThumbnailFileName = model.BackCardThumbnailImagePath;
            }
            else
            {
                var fileName = Path.GetFileNameWithoutExtension($"{model.CardDescription.ReturnAlphaNumericCharacters()}{model.CardNumber.ReturnAlphaNumericCharacters()}{model.YearId}_back");
                fileName = fileName.StripInvalidFileNameCharacters();
                var extension = Path.GetExtension(model.BackImage.FileName);
                if (!String.IsNullOrWhiteSpace(fileName))
                {
                    await SaveResizedImagesAsync(fileName, extension, model.BackImage, cancellationToken).ConfigureAwait(false);

                    fileContext.BackMainFileName = $"{fileName}{extension}";
                    fileContext.BackThumbnailFileName = $"{fileName}_thumb{extension}";
                }
                else
                {
                    fileContext.BackMainFileName = DEFAULT_IMAGE_NAME;
                    fileContext.BackThumbnailFileName = DEFAULT_IMAGE_NAME;
                }
            }

            return fileContext;
        }

        private async Task SaveResizedImagesAsync(string fileName, 
            string extension, 
            IFormFile formFile,
            CancellationToken cancellationToken)
        {
            var file = $"{_tempSavePath}\\{fileName}{extension}";
            using (var input = formFile.OpenReadStream())
            {
                using (var output = new MemoryStream())
                {
                    using (var image = new MagickImage(input))
                    {
                        input.Position = 0;
                        await image.WriteAsync(file);
                    }
                }
            }

            using (var image = new MagickImage(file))
            {
                Dimensions orginalDimensions = new Dimensions()
                {
                    Width = image.Width,
                    Height = image.Height
                };

                var resizedDimensions = ResizeMaintainAspectRatio(orginalDimensions, new Dimensions() { Height = 1200, Width = 1600 });
                FileInfo fileInfo = new FileInfo(file);
                image.Resize(resizedDimensions.Width, resizedDimensions.Height);
                await image.WriteAsync(_largeSavePath + fileInfo.Name, cancellationToken).ConfigureAwait(false);
            }

            using (var image = new MagickImage(file))
            {
                Dimensions orginalDimensions = new Dimensions()
                {
                    Width = image.Width,
                    Height = image.Height
                };

                var resizedDimensions = ResizeMaintainAspectRatio(orginalDimensions, new Dimensions() { Height = 450, Width = 600 });
                FileInfo fileInfo = new FileInfo(file);
                image.Resize(resizedDimensions.Width, resizedDimensions.Height);
                await image.WriteAsync(_midSavePath + fileInfo.Name, cancellationToken).ConfigureAwait(false);
            }

            using (var image = new MagickImage(file))
            {
                Dimensions orginalDimensions = new Dimensions()
                {
                    Width = image.Width,
                    Height = image.Height
                };

                var resizedDimensions = ResizeMaintainAspectRatio(orginalDimensions, new Dimensions() { Height = 90, Width = 120 });
                FileInfo fileInfo = new FileInfo(file);
                image.Resize(resizedDimensions.Width, resizedDimensions.Height);
                await image.WriteAsync($"{_thumbSavePath}/{Path.GetFileNameWithoutExtension(fileInfo.Name)}_thumb{extension}", cancellationToken).ConfigureAwait(false);
            }


            if (File.Exists(file))
            {
                File.Delete(file);
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
