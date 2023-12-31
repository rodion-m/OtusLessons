namespace ShopClientLib.Responses;

public class RegisterResponse
{
    public RegisterResponse(string? message = null)
    {
        Message = message;
    }

    public string? Message { get; set; }
    public string Token { get; set; }
}