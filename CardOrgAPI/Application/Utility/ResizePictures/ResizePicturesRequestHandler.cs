using CardOrgAPI.Interfaces.Services;
using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using ImageMagick;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Utility.ResizePictures
{
    public class ResizePicturesRequestHandler : IRequestHandler<ResizePicturesRequest, ApiResponse<ResizePicturesResponse>>
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IImageService _imageService;

        public ResizePicturesRequestHandler(IHostEnvironment hostEnvironment,
            IImageService imageService)
        {
            _hostEnvironment = hostEnvironment;
            _imageService = imageService;
        }

        public async Task<ApiResponse<ResizePicturesResponse>> Handle(ResizePicturesRequest request, CancellationToken cancellationToken)
        {
            var uploadsPath = _hostEnvironment.ContentRootPath + @"\clientapp\public\Uploads\";
            DateTime startTime = DateTime.Now;
            
            int count = 0;
            var dimensions = new Dimensions() { 
               Height = 1600,
               Width = 1600
            };

            if (Directory.Exists(uploadsPath))
            {             
                var files = Directory.GetFiles(uploadsPath).Where(x => !x.Contains("_thumb"));
                Parallel.ForEach(files,
                            async file =>
                             {
                                 await _imageService.ResizeImageAsync(file, dimensions);
                                 Interlocked.Increment(ref count);
                             });
            }
            DateTime endTime = DateTime.Now;
            var response = new ResizePicturesResponse()
            {
                ProcessedAmount = count,
                StartTime = startTime,
                EndTime = endTime
            };

            return new ApiResponse<ResizePicturesResponse>()
            {
                IsSuccessful = true,
                Value = response
            };
        }
    }
}
