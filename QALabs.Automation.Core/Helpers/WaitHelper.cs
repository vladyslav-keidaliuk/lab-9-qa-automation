namespace QALabs.Automation.Core.Helpers
{
    public static class WaitHelper
    {
        private static readonly TimeSpan DefaultTimeStep = TimeSpan.FromMilliseconds(100);

        public static bool WaitForCondition(Func<bool> condition, int timeoutInSeconds = 15, bool throwTimeoutException = false, string errorMessage = null, TimeSpan? polling = null)
        {
            polling ??= DefaultTimeStep;
            var stopDate = DateTime.Now.Add(TimeSpan.FromSeconds(timeoutInSeconds));

            while (stopDate > DateTime.Now)
            {
                try
                {
                    if (condition.Invoke())
                    {
                        return true;
                    }

                    if (polling.Value.TotalSeconds > timeoutInSeconds)
                    {
                        break;
                    }

                    Thread.Sleep(polling.Value);
                }
                catch (Exception e)
                {
                    if (errorMessage == null)
                    {
                        errorMessage = e.Message;
                    }
                }
            }

            if (throwTimeoutException)
            {
                throw new TimeoutException(errorMessage);
            }

            return false;
        }

        public static T WaitForEquals<T>(Func<T> func, T expected, int timeoutInSec = 10)
        {
            return WaitForActualResult(func, expected, EqualityComparer<T>.Default.Equals, timeoutInSec);
        }

        private static T WaitForActualResult<T>(Func<T> func, T expected, Func<T, T, bool> condition, int timeoutInSeconds = 15, bool throwExeption = true, TimeSpan? polling = null)
        {
            polling ??= DefaultTimeStep;
            var stopDate = DateTime.Now.Add(TimeSpan.FromSeconds(timeoutInSeconds));
            var actualResult = default(T);
            string errorMessage;
            do
            {
                errorMessage = null;
                try
                {
                    actualResult = func();

                    if (condition(actualResult, expected))
                    {
                        return actualResult;
                    }

                    if (polling.Value.TotalSeconds > timeoutInSeconds)
                    {
                        break;
                    }

                    Thread.Sleep(polling.Value);
                }
                catch (Exception e)
                {
                    errorMessage = e.Message;
                }
            }
            while (stopDate > DateTime.Now);

            if (throwExeption && errorMessage != null)
            {
                throw new TimeoutException(errorMessage);
            }

            return actualResult;
        }
    }
}
