namespace StratmanMedia.Utilities.Extensions;

public static class ExceptionExtensions
{
    public static string JoinAllMessages(this Exception ex)
    {
        var messages = new List<string>();
        var currentException = ex;
        messages.Add(currentException.Message);
        while (currentException.InnerException != null)
        {
            currentException = currentException.InnerException;
            messages.Add(currentException.Message);
        }

        return messages.JoinFormat("[{0}]", ",");
    }
}