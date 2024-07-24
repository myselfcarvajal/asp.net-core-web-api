namespace AwpaAcademic.Api.Models.Dtos;

public class ResultDto<T>
{
    public T? Results { get; set; }
    public List<string> ErrorsMessages { get; set; }
    public List<string> Messages { get; set; }
    public int StatusCode { get; set; }

    public ResultDto()
    {
        ErrorsMessages = new List<string>();
        Messages = new List<string>();
    }
}
