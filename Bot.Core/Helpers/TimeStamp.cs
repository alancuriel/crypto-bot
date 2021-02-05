using System;

namespace Bot.Core.Helpers
{
    public static class TimeStamp
    {
        public static string Now =>
            Convert.ToString((int)
            DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))
            .TotalSeconds);
    }
}