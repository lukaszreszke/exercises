namespace Acme.Decorator._2;

public class Result<T>
{
    public T Value { get; set; }
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
}