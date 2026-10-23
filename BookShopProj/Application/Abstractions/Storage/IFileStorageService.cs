using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Storage
{


    public interface IFileStorageService
    {
        Task<string> SaveAsync(
            Stream content,
            string contentType,
            Guid advertId,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(
            string filePath,
            CancellationToken cancellationToken = default);
    }


}
