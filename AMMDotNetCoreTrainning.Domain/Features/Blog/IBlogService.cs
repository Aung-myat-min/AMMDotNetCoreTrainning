using AMMDotNetCoreTrainning.Database.Models;

namespace AMMDotNetCoreTrainning.Domain.Features.Blog
{
    public interface IBlogService
    {
        TblBlog CreateBlog(TblBlog blog);
        bool? DeleteBlog(int id);
        TblBlog? EditBlog(int id, TblBlog blog);
        TblBlog? GetTblBlog(int id);
        List<TblBlog> GetTblblogs();
        TblBlog? UpdateBlog(int id, TblBlog blog);
    }
}