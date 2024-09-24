namespace Osiansdrystonewalls.com.Repositories
{
    public interface iImageRepository
    {
        Task<string> UploadASync(IFormFile file);

    }
}
