using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Storage
{
    public sealed class FileStorageOptions
    {
        public const string SectionName = "FileStorage";

        public string UploadPath { get; set; } = "uploads/adverts";
        public long MaxFileSizeBytes { get; set; } = 5 * 1024 * 1024;
    }

}
