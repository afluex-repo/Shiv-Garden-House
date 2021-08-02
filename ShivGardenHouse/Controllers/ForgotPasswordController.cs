using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShivGardenHouse.Filter;
using ShivGardenHouse.Models;

namespace ShivGardenHouse.Controllers
{
    public class ForgotPasswordController : Controller 
    {
        //
        // GET: /ForgotPassword/

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        [OnAction(ButtonName = "btnGetPassword")]
        public ActionResult ValidateData(ForgotPassword obj)
        {

            DataSet ds = obj.ValidateData();

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["recoverPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    //ViewBag["OTP"] = ds.Tables[0].Rows[0]["OTP"].ToString();
                    //string passwordRecoveryMessage = BLSMS.ForgetPassword(ds.Tables[0].Rows[0]["FirstName"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                  //  BLSMS.SendSMS(ds.Tables[0].Rows[0]["Mobile"].ToString(), passwordRecoveryMessage);
                    TempData["recoverPassword"] = "Password is send on your registered mobile no.";
                }
            }
            else
            {
                TempData["recoverPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return RedirectToAction("ForgotPassword", "ForgotPassword");
        }
    }
}
