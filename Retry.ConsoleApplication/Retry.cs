using System;

namespace Retry.ConsoleApplication
{
    public abstract class Retry
    {
        public int NumberOfRetries { get; set; }

        public void ExecuteAction(Action action)
        {
            int currentRetry = 0;

            for (;;)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    currentRetry++;

                    if (currentRetry > NumberOfRetries || !IsTemporaryException(ex))
                        throw;
                }
            }
        }

        public abstract bool IsTemporaryException(Exception ex);
    }
}
