using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.IO;

namespace ShivGardenHouse
{
    static public class BLSMS
    {
        static public void SendSMS(string Mobile, string Message)
        {
            try
            {
                string SMSAPI = ConfigurationSettings.AppSettings["SMSAPI"].ToString();
                SMSAPI = SMSAPI.Replace("[AND]", "&");
                SMSAPI = SMSAPI.Replace("[MOBILE]", Mobile);
                SMSAPI = SMSAPI.Replace("[MESSAGE]", Message);

                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(SMSAPI, false));
                HttpWebResponse httpResponse = (HttpWebResponse)(httpReq.GetResponse());
            }
            catch (Exception ex)
            {
            }
        }

        static public string Booking(string BookingNo, string BookingAmount, string AssociateName, string Plot , string mob)
        {
          string url= "http://sms.sandhyasoftware.com/api/mt/SendSMS?user=Shiv&password=SHVGRD&senderid=SHVGRD&channel=Trans&DCS=0&flashsms=0&number="+ mob + "&text=Dear"+AssociateName+ ",Amount paid forallotmentofPlot "+ Plot + "isRs."+ BookingAmount + "and yourBookingNo="+ BookingNo + "&route=10";
            WebRequest request = HttpWebRequest.Create(url);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
            //string Message = ConfigurationSettings.AppSettings["Booking"].ToString();


            //Message = Message.Replace("[AssociateName]", AssociateName);
            //Message = Message.Replace("[BookingNo]", BookingNo);
            //Message = Message.Replace("[Plot]", Plot);
            //Message = Message.Replace("[BookingAmt]", BookingAmount);
            //return Message;


        }
        static public string AssociateRegistration(string Name, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["RegistrationAssociate"].ToString();

            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Password]", Password);
            return Message;
        }
        static public string CustomerRegistration(string Name, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["CustomerRegistration"].ToString();

            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Name]", Name);
            Message = Message.Replace("[Password]", Password);
            return Message;
        }
        static public string PlotAllotment(string name, string Plot, string amt)
        {
            string Message = ConfigurationSettings.AppSettings["PlotAllotment"].ToString();

            Message = Message.Replace("[Name]", name);
            Message = Message.Replace("[Plot]", Plot);
            Message = Message.Replace("[amt]", amt);
            return Message;
        }

        static public string EMIPayment(string name, string Plot, string bookno, string instno, string amt)
        {
            string Message = ConfigurationSettings.AppSettings["EMIPayment"].ToString();

            Message = Message.Replace("[Name]", name);
            Message = Message.Replace("[Plot]", Plot);
            Message = Message.Replace("[amt]", amt);
            Message = Message.Replace("[BookingNo]", bookno);
            Message = Message.Replace("[InstallmentNo]", instno);
            return Message;
        }



        static public string ForgetPassword(string FirstName, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["ForgetPassword"].ToString();

            Message = Message.Replace("[FirstName]", FirstName);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }


        static public string Registration(string MemberName, string LoginId, string Password)
        {
            string Message = ConfigurationSettings.AppSettings["REGISTRATION"].ToString();
            Message = Message.Replace("[Member-Name]", MemberName);
            Message = Message.Replace("[LoginId]", LoginId);
            Message = Message.Replace("[Password]", Password);

            return Message;
        }

        public static string CustomerRegistrationSMS(string mob, string Message)
        {
            string strUrl = "http://sms.sandhyasoftware.com/api/mt/SendSMS?user=Shiv&password=SHVGRD&senderid=SHVGRD&channel=Trans&DCS=0&flashsms=0&number=" + mob + "&text=" + Message;
            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }

        public static string AssociateRegistrationSMS(string mob, string Message, string id, string pass, string name)
        {
            string strUrl = "http://sms.sandhyasoftware.com/api/mt/SendSMS?user=Shiv&password=SHVGRD&senderid=SHVGRD&channel=Trans&DCS=0&flashsms=0&number=" + mob + "&text=" + Message;
            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
            return dataString;
        }

    }
}
