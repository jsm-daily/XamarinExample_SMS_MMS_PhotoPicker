using Android.Content;
using Com.Google.Mms.Pdu;
using System;
using System.Text;

using Xamarin_SMS_MMS_PhotoPicker.Droid.Redefinitions;
using Xamarin_SMS_MMS_PhotoPicker.Interfaces;

#region SAMPLE - 8 : MMS 및 SMS 구현
[assembly: Xamarin.Forms.Dependency(typeof(SMS))]
namespace Xamarin_SMS_MMS_PhotoPicker.Droid.Redefinitions
{
    public class SMS : ISMS
    {
        public bool Send(string phoneNumber, string txtMessage, string imageBase64 = null)
        {
            Context ctx = Android.App.Application.Context;

            #pragma warning disable CS0618
            Android.Telephony.SmsManager smsManager = Android.Telephony.SmsManager.Default;

            try
            {                
                if(!string.IsNullOrEmpty(phoneNumber) && (string.IsNullOrEmpty(imageBase64) && (!string.IsNullOrEmpty(txtMessage))))
                {
                    //SMS 발송
                    var msgs = smsManager.DivideMessage(txtMessage);

                    smsManager.SendMultipartTextMessage(phoneNumber, null, msgs, null, null);
                }
                else if (!string.IsNullOrEmpty(phoneNumber) && (!string.IsNullOrEmpty(imageBase64) || !string.IsNullOrEmpty(txtMessage)))
                {
                    // MMS 전송
                    #region PDU
                    SendReq sendReqPdu = new SendReq();
                    // 수신자 등록
                    sendReqPdu.AddTo(new EncodedStringValue(phoneNumber));

                    using (PduBody pduBody = new PduBody())
                    {
                        // 텍스트 등록
                        if (!string.IsNullOrEmpty(txtMessage))
                        {
                            PduPart txtPart = new PduPart();
                            txtPart.Charset = CharacterSets.Utf8;   //한글 사용
                            txtPart.SetData(Encoding.UTF8.GetBytes(txtMessage));
                            txtPart.SetContentType(new EncodedStringValue("text/plain").GetTextString());
                            txtPart.SetName(new EncodedStringValue("Message").GetTextString());
                            pduBody.AddPart(txtPart);
                        }

                        // 이미지 등록
                        if (!string.IsNullOrEmpty(imageBase64))
                        {
                            PduPart imgPart = new PduPart();
                            byte[] sampleImageData = System.Convert.FromBase64String(imageBase64);
                            imgPart.SetData(sampleImageData);
                            imgPart.SetContentType(new EncodedStringValue("image/jpg").GetTextString());
                            imgPart.SetFilename(new EncodedStringValue(new Random().Next().ToString() + ".jpg").GetTextString());
                            pduBody.AddPart(imgPart);
                        }

                        // MMS 본문 설정
                        sendReqPdu.Body = pduBody;
                    }

                    // MMS 제공자에게 보낼 바이트 배열을 생성.
                    PduComposer composer = new PduComposer(sendReqPdu);
                    byte[] pduData = composer.Make();

                    // 캐시 파일에 pdu 쓰기
                    string cacheFilePath = System.IO.Path.Combine(ctx.CacheDir.AbsolutePath, "send." + new Random().Next().ToString() + ".dat");
                    System.IO.File.WriteAllBytes(cacheFilePath, pduData);
                    #endregion

                    #region Send          
                    if (System.IO.File.Exists(cacheFilePath))
                    {
                        Android.Net.Uri contentUri = AndroidX.Core.Content.FileProvider.GetUriForFile(ctx, ctx.PackageName + ".fileprovider", new Java.IO.File(cacheFilePath));

                        smsManager.SendMultimediaMessage(ctx, contentUri, null, null, null);
                    }
                    #endregion
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


    }
}
#endregion