using Android.Content;
using Xamarin_SMS_MMS_PhotoPicker.Droid;
using Xamarin_SMS_MMS_PhotoPicker.Interfaces;

#region SAMPLE - 7 : 사진 선택기 구현
[assembly: Xamarin.Forms.Dependency(typeof(PhotoPicker))]
namespace Xamarin_SMS_MMS_PhotoPicker.Droid
{
    public class PhotoPicker : IPhotoPicker
    {
        public System.Threading.Tasks.Task<System.IO.Stream> GetImageStreamAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new System.Threading.Tasks.TaskCompletionSource<System.IO.Stream>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}
#endregion