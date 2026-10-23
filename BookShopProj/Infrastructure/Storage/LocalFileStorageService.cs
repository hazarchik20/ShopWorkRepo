using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;


namespace Infrastructure.Storage
{

    public sealed class LocalFileStorageService : IFileStorageService
    {
        private readonly string _rootPath;
        private readonly string _uploadPath;
        private readonly long _maxFileSize;

        public LocalFileStorageService(
            IHostEnvironment environment,
            IOptions<FileStorageOptions> options)
        {
            var settings = options.Value;

            if (string.IsNullOrWhiteSpace(settings.UploadPath) ||
                Path.IsPathRooted(settings.UploadPath) ||
                settings.UploadPath.Contains('\\') ||
                settings.UploadPath.Split('/').Any(x =>
                    x is "" or "." or ".." ||
                    x.Contains(':')))
            {
                throw new ArgumentException(
                    "Invalid upload path.");
            }

            if (settings.MaxFileSizeBytes <= 0)
                throw new ArgumentException(
                    "Max file size must be positive.");

            _uploadPath = settings.UploadPath.Trim('/');

            _rootPath = Path.GetFullPath(
                Path.Combine(
                    environment.ContentRootPath,
                    "wwwroot",
                    _uploadPath.Replace(
                        '/',
                        Path.DirectorySeparatorChar)));

            _maxFileSize = settings.MaxFileSizeBytes;
        }

        public async Task<string> SaveAsync(
            Stream content,
            string contentType,
            Guid advertId,
            CancellationToken cancellationToken = default)
        {
            if (advertId == Guid.Empty)
                throw new ArgumentException(
                    "AdvertId cannot be empty.");

            if (content is null || !content.CanRead)
                throw new ArgumentException(
                    "Invalid file stream.");

            var extensions = new Dictionary<string, string>(
                StringComparer.OrdinalIgnoreCase)
            {
                ["image/jpeg"] = ".jpg",
                ["image/png"] = ".png",
                ["image/webp"] = ".webp"
            };

            if (!extensions.TryGetValue(
                contentType,
                out var extension))
            {
                throw new InvalidDataException(
                    "Unsupported image type.");
            }

            var folder = Path.Combine(
                _rootPath,
                advertId.ToString("N"));

            Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid():N}{extension}";
            var fullPath = Path.Combine(folder, fileName);

            try
            {
                await using var output = new FileStream(
                    fullPath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 81920,
                    useAsync: true);

                var buffer = new byte[81920];
                long totalBytes = 0;
                var header = new byte[12];
                var headerLength = 0;

                while (true)
                {
                    var read = await content.ReadAsync(
                        buffer.AsMemory(),
                        cancellationToken);

                    if (read == 0)
                        break;

                    totalBytes += read;

                    if (totalBytes > _maxFileSize)
                        throw new InvalidDataException(
                            "Image exceeds allowed size.");

                    if (headerLength < header.Length)
                    {
                        var copyLength = Math.Min(
                            read,
                            header.Length - headerLength);

                        buffer.AsSpan(0, copyLength).CopyTo(
                            header.AsSpan(headerLength));

                        headerLength += copyLength;
                    }

                    await output.WriteAsync(
                        buffer.AsMemory(0, read),
                        cancellationToken);
                }

                if (totalBytes == 0 ||
                    !IsValidSignature(
                        header.AsSpan(0, headerLength),
                        contentType))
                {
                    throw new InvalidDataException(
                        "Invalid image signature.");
                }
            }
            catch
            {
                if (File.Exists(fullPath))
                    File.Delete(fullPath);

                throw;
            }

            return $"/{_uploadPath}/{advertId:N}/{fileName}";
        }

        public Task DeleteAsync(
            string filePath,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(filePath))
                return Task.CompletedTask;

            var normalizedPath = filePath.Replace('\\', '/');
            var prefix = $"/{_uploadPath}/";

            if (!normalizedPath.StartsWith(
                prefix,
                StringComparison.Ordinal))
            {
                throw new InvalidOperationException(
                    "Invalid image path.");
            }

            var relativePath = normalizedPath[prefix.Length..];
            var segments = relativePath.Split('/');

            if (segments.Length != 2 ||
                !Guid.TryParseExact(segments[0], "N", out _) ||
                !Guid.TryParseExact(
                    Path.GetFileNameWithoutExtension(segments[1]),
                    "N",
                    out _) ||
                !new[] { ".jpg", ".png", ".webp" }.Contains(
                    Path.GetExtension(segments[1]),
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Invalid image path.");
            }

            var fullPath = Path.GetFullPath(
                Path.Combine(_rootPath, segments[0], segments[1]));

            var rootPrefix = _rootPath.TrimEnd(
                Path.DirectorySeparatorChar)
                + Path.DirectorySeparatorChar;

            if (!fullPath.StartsWith(
                rootPrefix,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Path is outside storage directory.");
            }

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }

        private static bool IsValidSignature(
            ReadOnlySpan<byte> header,
            string contentType)
        {
            return contentType.ToLowerInvariant() switch
            {
                "image/jpeg" =>
                    header.Length >= 3 &&
                    header[0] == 0xFF &&
                    header[1] == 0xD8 &&
                    header[2] == 0xFF,

                "image/png" =>
                    header.Length >= 8 &&
                    header[..8].SequenceEqual(
                        new byte[]
                        {
                        137, 80, 78, 71, 13, 10, 26, 10
                        }),

                "image/webp" =>
                    header.Length >= 12 &&
                    header[..4].SequenceEqual("RIFF"u8) &&
                    header[8..12].SequenceEqual("WEBP"u8),

                _ => false
            };
        }
    }

}
