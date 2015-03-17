namespace Helpers.Server
{
    public interface IPageable
    {
        int TotalPages { get; set; }

        int CurrentPage { get; set; }
    }
}