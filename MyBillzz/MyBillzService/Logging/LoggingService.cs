namespace MyBillzService.Logging;

public class LoggingService : ILoggingService
{
    private readonly ILogger<LoggingService> _logger;

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
    }

    public void LogInfo(string strMessage)
    {
        _logger.LogInformation(strMessage);
    }

    public void LogWarning(string strMessage)
    {
        _logger.LogWarning(strMessage);
    }

    public void LogError(string strMessage, Exception? ex = null)
    {
        _logger.LogError(ex, strMessage);
    }
}
