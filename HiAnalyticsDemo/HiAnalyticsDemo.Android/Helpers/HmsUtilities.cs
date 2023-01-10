using System;
using Huawei.Hms.Api;

namespace HiAnalyticsDemo.Droid.Helpers
{
    public class HmsUtilities
    {
        public static bool IsHuaweiServicesAvailable()
        {
            var context = global::Android.App.Application.Context;
            var resultCode = HuaweiApiAvailability.Instance.IsHuaweiMobileServicesAvailable(context);
            return resultCode == ConnectionResult.Success;
        }
    }
}

