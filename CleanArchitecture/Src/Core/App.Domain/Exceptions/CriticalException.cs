namespace App.Service.ExceptionHandlers
{
    public class CriticalException(string message) : Exception(message);
}