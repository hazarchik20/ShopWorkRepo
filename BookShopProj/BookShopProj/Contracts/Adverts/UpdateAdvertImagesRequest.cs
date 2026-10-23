using Microsoft.AspNetCore.Http;
namespace BookShopProj.Contracts.Adverts
{
    public sealed class UpdateAdvertImagesRequest
    {
        public List<string> KeepImages { get; set; } = new();

        public List<IFormFile> NewImages { get; set; } = new();
    }

}
