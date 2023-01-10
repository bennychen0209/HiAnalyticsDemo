using System;
using System.Collections.Generic;

namespace HiAnalyticsDemo.Services
{
    public interface IAnalyticsManager
    {
        void LogEvent(string eventId);
        void LogEvent(string eventId, string paramName, string value);
        void LogEvent(string eventId, IDictionary<string, string> parameters);
    }
}

