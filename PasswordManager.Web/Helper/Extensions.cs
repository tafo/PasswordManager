namespace PasswordManager.Web.Helper;

public static class Extensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        return source.Skip(pageIndex * pageSize).Take(pageSize);
    }

}