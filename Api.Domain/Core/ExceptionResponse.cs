namespace Api.Domain.Core;


public class ExceptionResponse
{
    public int Status { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Detail  { get; set; } = string.Empty;
}