using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using AndroidX.Core.OS;
using Plugin.CurrentActivity;
using static Android.Provider.SyncStateContract;
using Huawei.Agconnect.Config;
using Huawei.Hms.Aaid;

namespace HiAnalyticsDemo.Droid
{
    [Activity(Label = "HiAnalyticsDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", Exported = true, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitializeHMSToken();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private static readonly string TAG = "android_MainActivity";
        private void InitializeHMSToken()
        {
            try
            {
                if (Helpers.HmsUtilities.IsHuaweiServicesAvailable())
                {
                    //Initialize Analytics
                    AnalyticsManager.Init(this);
                    System.Threading.Thread thread = new System.Threading.Thread(() =>
                    {
                        try
                        {
                            string appid = AGConnectServicesConfig.FromContext(this).GetString("client/app_id");
                            string token = HmsInstanceId.GetInstance(this).GetToken(appid, "HCM");
                        }
                        catch (Java.Lang.Exception e)
                        {
                            App.ExceptionDescription(TAG, "InitializeHMSToken -- Threading", e);
                        }
                    });
                    thread.Start();
                }
            }
            catch (Java.Lang.Exception ex)
            {
                App.ExceptionDescription(TAG, "InitializeHMSToken", ex);
            }
        }
    }
}
