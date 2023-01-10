using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Firebase.Analytics;
using HiAnalyticsDemo.Services;
using Huawei.Hms.Analytics;
using Xamarin.Forms;

namespace HiAnalyticsDemo.Droid
{
    public class AnalyticsManager : IAnalyticsManager
    {
        static HiAnalyticsInstance instance;
        static string TAG = "android_AnalyticsManager";

        public static void Init(Activity mainActivity)
        {
            try
            {
                //Enable Analytics Kit Log
                HiAnalyticsTools.EnableLog();

                //Generate the Analytics Instance
                instance = HiAnalytics.GetInstance(mainActivity);

                //You can also use Context initialization
                //Context context = ApplicationContext;
                //instance = HiAnalytics.GetInstance(context);

                //Enable collection capability
                instance.SetAnalyticsEnabled(true);
            }
            catch (Exception ex)
            {
                App.ExceptionDescription(TAG, "Init", ex);
            }
        }

        public void LogEvent(string eventId)
        {
            LogEvent(eventId, null);
        }

        public void LogEvent(string eventId, string paramName, string value)
        {
            LogEvent(eventId, new Dictionary<string, string>
            {
                {paramName, value}
            });
        }

        public void LogEvent(string eventId, IDictionary<string, string> parameters)
        {
            try
            {
                var fireBaseAnalytics = FirebaseAnalytics.GetInstance(Forms.Context);
                var isHuaweiAvailable = Helpers.HmsUtilities.IsHuaweiServicesAvailable();

                if (parameters == null)
                {
                    fireBaseAnalytics.LogEvent(eventId, null);
                    if (isHuaweiAvailable)
                        instance.OnEvent(eventId, null);
                    return;
                }

                var bundle = new Bundle();

                foreach (var item in parameters)
                {
                    bundle.PutString(item.Key, App.FirebaseLimit(item.Value));
                }

                fireBaseAnalytics.LogEvent(eventId, bundle);
                if (isHuaweiAvailable)
                    instance.OnEvent(eventId, bundle);
            }
            catch (Exception ex)
            {
                App.ExceptionDescription(TAG, "LogEvent", ex);
            }
        }
    }
}

