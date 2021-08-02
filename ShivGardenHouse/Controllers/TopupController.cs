using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ShivGardenHouse.Filter;
using ShivGardenHouse.Models;

namespace ShivGardenHouse.Controllers
{
    public class TopupController : AdminBaseController
    {

        public ActionResult Topup()
        {
            Topup model = new Models.Topup();

            List<SelectListItem> ddlEpin = new List<SelectListItem>();
            ddlEpin.Add(new SelectListItem { Text = "Select EPin", Value = "0" });
            ViewBag.ddlEpin = ddlEpin;
            return View();
        }

        [HttpPost]
        [ActionName("Topup")]
        [OnAction(ButtonName = "btnTopup")]
        public ActionResult MemberTopup(Topup model)
        {
            List<SelectListItem> ddlEpin = new List<SelectListItem>();
            ddlEpin.Add(new SelectListItem { Text = "Select EPin", Value = "0" });
            ViewBag.ddlEpin = ddlEpin;
            
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.TopupByEpin();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Topup"] = "ID successfully Topup";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Topup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Topup"] = ex.Message;
            }
            return View();
        }

        public ActionResult GetEPinList(string UserID)
        {
            try
            {
                Topup model = new Topup();
                model.Fk_UserId = UserID;
                model.Status = "Unused";

                List<SelectListItem> ddlEPin = new List<SelectListItem>();
                DataSet ds = model.GetEPinList();

                if (ds != null && ds.Tables.Count > 0)
                {
                    model.Result = "yes";
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ddlEPin.Add(new SelectListItem { Text = r["ePinNo"].ToString(), Value = r["PK_ePinId"].ToString() });
                    }
                }
                model.ddlEPin = ddlEPin;
                
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

    }
}
