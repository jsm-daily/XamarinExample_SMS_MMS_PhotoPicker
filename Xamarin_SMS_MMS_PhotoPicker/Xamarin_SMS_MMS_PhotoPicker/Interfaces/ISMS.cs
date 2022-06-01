using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_SMS_MMS_PhotoPicker.Interfaces
{
    #region SAMPLE - 1 : Interface 선언
    public interface ISMS
    {
        /// <summary>
        /// SMS 및 MMS 발송
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="txtMessage"></param>
        /// <param name="imageBase64"></param>
        /// <returns></returns>
        bool Send(string phoneNumber, string txtMessage, string imageBase64 = null);
    }

    public interface IPhotoPicker
    {
        System.Threading.Tasks.Task<System.IO.Stream> GetImageStreamAsync();
    }
    #endregion
}
