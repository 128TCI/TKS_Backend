namespace DomainEntities.Dto;

public sealed class ReturnResult<T>
{
    public ReturnResult(T resultData, string messages, bool isSuccess = true, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        ResultData = resultData;
        Messages = messages;
        Errors = errors ?? [];
    }
    public bool IsSuccess { get; set; } = true;
    public T ResultData { get; set; }
    public List<string> Errors { get; set; }
    public string Messages { get; set; }
}
