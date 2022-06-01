using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace Xamarin_SMS_MMS_PhotoPicker.Droid
{
    [Activity(Label = "Xamarin_SMS_MMS_PhotoPicker", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// 앱 사용 권한 확인 및 부여
        /// </summary>
        /// <returns></returns>
        private bool CheckAppPermissions()
        {
            bool allPermsGranted = true;

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessNetworkState) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.Internet) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReadCalendar) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.WriteCalendar) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReadContacts) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.WriteContacts) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            // 문자 읽기 권한
            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReadSms) != (int)Permission.Granted)
                allPermsGranted = false;
            
            // 문자 발송 권한
            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.SendSms) != (int)Permission.Granted)
                allPermsGranted = false;
            

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReceiveSms) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }


            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.WriteSms) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReceiveMms) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission_group.Sms) != (int)Permission.Granted)
            //{
            //    allPermsGranted = false;
            //}

            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteApnSettings) != (int)Permission.Granted)
            //{
            //    allPermsGranted = false;
            //}

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.ReceiveWapPush) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            // TODO NOTE - not sure if i need this one.. test
            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.UseCredentials) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.BindVoiceInteraction) != (int)Permission.Granted)
            //{
            //    allPermsGranted = false;
            //}

            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadPhoneNumbers) != (int)Permission.Granted)
            //{
            //    allPermsGranted = false;
            //}

            if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.WakeLock) != (int)Permission.Granted)
            {
                allPermsGranted = false;
            }

            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.SendRespondViaMessage) != (int)Permission.Granted)
            //{
            //    allPermsGranted = false;
            //}

            //if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.SetActivityWatcher) != (int)Permission.Granted)
            //{
            //    allPermsGranted = false;
            //}


            if (!allPermsGranted)
            {
                string[] permissions = new string[] {
                    Android.Manifest.Permission.AccessNetworkState,
                    Android.Manifest.Permission.Internet,
                    Android.Manifest.Permission.ReadExternalStorage,
                    Android.Manifest.Permission.ReadCalendar,
                    Android.Manifest.Permission.WriteCalendar,
                    Android.Manifest.Permission.ReadContacts,
                    Android.Manifest.Permission.WriteContacts,
                    Android.Manifest.Permission.WriteExternalStorage,
                    Android.Manifest.Permission.WakeLock,
                    Android.Manifest.Permission.ReceiveSms,
                    Android.Manifest.Permission.ReadSms,
                    Android.Manifest.Permission.WriteSms,
                    Android.Manifest.Permission.SendSms,
                    Android.Manifest.Permission.ReceiveMms,
                    Android.Manifest.Permission.ReceiveWapPush
                    //Manifest.Permission.SendRespondViaMessage
                    //Manifest.Permission_group.Sms,
                    //Manifest.Permission.ReadPhoneNumbers,
                    //Manifest.Permission.SetActivityWatcher,
                    //Manifest.Permission.BindVoiceInteraction
                };

                AndroidX.Core.App.ActivityCompat.RequestPermissions(this, permissions, 1);


            }

            return allPermsGranted;

        }
        //SAMPLE - 5 : 사진 선택
        #region 사진 보관함
        internal static MainActivity Instance { get; private set; }

        public static readonly int PickImageId = 1000;
        public System.Threading.Tasks.TaskCompletionSource<System.IO.Stream> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    System.IO.Stream stream = ContentResolver.OpenInputStream(uri);

                    //// Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //SAMPLE - 6 : 초기화
            #region 사진 보관함
            Instance = this;
            #endregion
            
            CheckAppPermissions();

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}