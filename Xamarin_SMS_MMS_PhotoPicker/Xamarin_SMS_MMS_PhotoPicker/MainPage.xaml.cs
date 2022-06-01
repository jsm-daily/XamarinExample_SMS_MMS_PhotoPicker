using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin_SMS_MMS_PhotoPicker.Interfaces;

namespace Xamarin_SMS_MMS_PhotoPicker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private  void btn_SMS_Clicked(object sender, EventArgs e)
        {
            Xamarin.Forms.DependencyService.Get<ISMS>().Send(ent_recipients.Text, ent_msg.Text);
        }

        private async void btn_MMS_Clicked(object sender, EventArgs e)
        {
            System.IO.Stream stream = await DependencyService.Get<IPhotoPicker>().GetImageStreamAsync();

            if (stream != null)
            {
                if (stream.Length > 1024 * 1024)
                {
                    Console.WriteLine("사진 최대 용량은 1MB입니다.");
                    return;
                }

                byte[] Photo_Bytes = new byte[stream.Length];
                stream.Read(Photo_Bytes, 0, Photo_Bytes.Length);
                string imageBase64 = System.Convert.ToBase64String(Photo_Bytes);

                //가상머신에서는 MMS 발송이 되지 않고 한참 뒤에 "전송되지 않았습니다" 문구가 문자에 찍힘
                Xamarin.Forms.DependencyService.Get<ISMS>().Send(ent_recipients.Text, ent_msg.Text, imageBase64);
            }
        }
    }
}
