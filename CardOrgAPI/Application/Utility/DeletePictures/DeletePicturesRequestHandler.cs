using CardOrgAPI.Responses;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Utility.DeletePictures
{
    public class DeletePicturesRequestHandler : IRequestHandler<DeletePicturesRequest, ApiResponse<int>>
    {
        private readonly IHostEnvironment _hostEnvironment;

        public DeletePicturesRequestHandler(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<ApiResponse<int>> Handle(DeletePicturesRequest request, CancellationToken cancellationToken)
        {
            var uploadsPath = _hostEnvironment.ContentRootPath + @"\clientapp\public\Uploads\";
            int count = 0;
            if (Directory.Exists(uploadsPath))
            {
                var files = Directory.GetFiles(uploadsPath).Where(x => !x.Contains("_thumb"));
                Parallel.ForEach(files,
                            file =>
                            {
                                File.Delete(file);
                                Interlocked.Increment(ref count);
                            });
            }

            return new ApiResponse<int>() { 
                IsSuccessful = true,
                Value = count
            };
        }
    }
}
