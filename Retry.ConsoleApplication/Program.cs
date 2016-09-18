using System;
using System.Net;
using System.Net.Http;

namespace Retry.ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebTimeoutRetry().ExecuteAction(async () =>
            {
                using (var client = new HttpClient())
                {
                    await client.GetAsync("http://some-website.com");
                }
            });
        }

        public class WebTimeoutRetry : Retry
        {
            public override bool IsTemporaryException(Exception ex)
            {
                var webException = ex as WebException;
                if (webException?.Status == WebExceptionStatus.Timeout)
                    return true;

                return false;
            }
        }
    }
}
