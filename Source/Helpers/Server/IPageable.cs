namespace Helpers.Server
{
    public interface IPageable
    {
        int PagesCount { get; set; }

        int MaxVisiblePages { get; set; }

        int CurrentPage { get; set; }

        int NextPage { get; }

        int PreviousPage { get; }
    }
}