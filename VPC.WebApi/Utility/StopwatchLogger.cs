 
using System;
using NLog;


namespace VPC.WebApi.Utility
{
    public class StopwatchLogger
    {
        private readonly Logger _logger;
        private readonly DateTime _startTime;

        private StopwatchLogger(Logger logger)
        {
            _logger = logger;
            _startTime = DateTime.UtcNow;
            
        }

        public static StopwatchLogger Start(Logger logger)
        {
            return new StopwatchLogger(logger);
        }

        public void StopAndLog(string message)
        {
            try
            {
                TimeSpan span = DateTime.Now - _startTime;
                _logger.Info($"{message} took {(int) span.TotalMilliseconds} milliseconds");   }
             
            catch
            {
                //Purposely silencing error as this is only a utility method
            }
        }

    }
}
