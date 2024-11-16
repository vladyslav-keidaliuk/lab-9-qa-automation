namespace QALabs.Automation.Core.Interaction
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

        public static bool WaitNoError(Action action, int timeoutInSec = 10)
        {
            return WaitForCondition(
                () =>
            {
                action();
                return true;
            },
                timeoutInSec,
                true);
        }

        public static T WaitForEquals<T>(Func<T> func, T expected, int timeoutInSec = 10)
        {
            return WaitForActualResult(func, expected, EqualityComparer<T>.Default.Equals, timeoutInSec);
        }

        public static string WaitForContains(Func<string> func, string expected, int timeoutInSec = 10)
        {
            return WaitForActualResult(func, expected, (s, s1) => s.Contains(s1), timeoutInSec);
        }

        public static T WaitForStringIsNotEmpty<T>(Func<T> func, int timeoutInSec = 15)
        {
            return WaitForNotEmptyResult(func, (s) => !string.IsNullOrEmpty(s as string), timeoutInSec);
        }

        public static IEnumerable<T> WaitForCollectionIsNotEmpty<T>(Func<IEnumerable<T>> func, int timeoutInMin = 15)
        {
            return WaitForNotEmptyResult(func, (s) => s.Any(), timeoutInMin);
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

        private static T WaitForNotEmptyResult<T>(Func<T> func, Func<T, bool> condition, int timeoutInMinutes = 15, bool throwExeption = true, TimeSpan? polling = null)
        {
            polling ??= TimeSpan.FromMinutes(3);
            var stopDate = DateTime.Now.Add(TimeSpan.FromMinutes(timeoutInMinutes));
            var actualResult = default(T);
            string errorMessage;
            do
            {
                errorMessage = null;
                try
                {
                    actualResult = func();

                    if (condition(actualResult))
                    {
                        return actualResult;
                    }

                    if (polling.Value.TotalMinutes > timeoutInMinutes)
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
