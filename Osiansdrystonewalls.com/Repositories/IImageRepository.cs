

namespace Osiansdrystonewalls.com.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadASync(IFormFile file);
    }
}
