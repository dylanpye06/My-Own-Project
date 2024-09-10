
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Osiansdrystonewalls.com.Repositories
{
    public class ImageRepository(IConfiguration configuration) : IImageRepository
    {
        private readonly IConfiguration configuration = configuration;
        private readonly Account account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);

        public async Task<string> UploadASync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            else
                return null;
        }
    }
}
