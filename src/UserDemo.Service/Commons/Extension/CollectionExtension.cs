
using UserDemo.Domain.Configuration;

namespace UserDemo.Service.Commons.Extension
{
    public static class CollectionExtension
    {
        public static IQueryable<TSource> ToPagedList<TSource>(this IQueryable<TSource> sources, PaginationParams @params)
        {
            int numToSkip = (@params.PageIndex - 1) * @params.PageSize;
            int total = sources.Count();

            if (numToSkip >= total && total > 0)
            {
                numToSkip = total - total % @params.PageSize;
            }

            return sources.Skip(numToSkip).Take(@params.PageSize);
        }
    }
}
