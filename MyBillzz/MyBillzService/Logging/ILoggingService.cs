namespace MyBillzService.Logging;

public interface ILoggingService
{
    void LogInfo(string strMessage);
    void LogWarning(string strMessage);
    void LogError(string strMessage, Exception? Ex = null);
}
