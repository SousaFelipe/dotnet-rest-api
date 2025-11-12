namespace Api.Service.Dtos;


public class PagedResponse<T>(List<T> items, int total, int page, int size)
{
    public List<T> Items { get; set; } = items;
    public int Total { get; set; } = total;
    public int Page { get; set; } = page;
    public int Size { get; set; } = size;
}
