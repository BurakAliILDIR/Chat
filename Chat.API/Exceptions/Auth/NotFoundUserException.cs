namespace Chat.API.Exceptions.Auth
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException(string? message) : base("User not found.")
        {
        }
    }
}