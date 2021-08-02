using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShivGardenHouse.Models;
using ShivGardenHouse.Filter;
using System.Data;

using System.IO;
namespace ShivGardenHouse.Controllers
{
    public class SMSController : AdminBaseController
    {
        // GET: SMS

        #region SMS Template

        public ActionResult SMSTemplate(string PK_TemplateId)
        {
            SMS model = new SMS();
            model.PK_TemplateId = PK_TemplateId;
            if (model.PK_TemplateId != null)
            {
                DataSet ds = model.GettingSMSTemplate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    model.PK_TemplateId = ds.Tables[0].Rows[0]["PK_TemplateId"].ToString();
                    model.TemplateName = ds.Tables[0].Rows[0]["TemplateName"].ToString();
                    model.Msg = ds.Tables[0].Rows[0]["Msg"].ToString();
                }
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "btnSave")]
        [ActionName("SMSTemplate")]
        public ActionResult SaveSMSTemplate(SMS model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingSMSTemplate();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SMSTemplate"] = "SMS Template Saved Successfully";

                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SMSTemplate"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SMSTemplate"] = ex.Message;
                return View(model);
            }

            return RedirectToAction("SMSTemplate");
        }

        public ActionResult SMSTemplateList(SMS model)
        {
            List<SMS> list = new List<SMS>();
            DataSet ds = model.GettingSMSTemplate();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    SMS obj = new SMS();
                    obj.PK_TemplateId = r["PK_TemplateId"].ToString();
                    obj.TemplateName = r["TemplateName"].ToString();
                    obj.Msg = r["Msg"].ToString();
                    list.Add(obj);
                }
                model.ListSMSTemplate = list;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "btnUpdate")]
        [ActionName("SMSTemplate")]
        public ActionResult UpdateSMSTemplate(SMS model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdatingSMSTemplate();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SMSTemplateList"] = "SMS Template Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SMSTemplate"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SMSTemplate"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("SMSTemplateList");
        }

        public ActionResult DeleteSMSTemplate(string PK_TemplateId)
        {
            SMS model = new SMS();
            try
            {
               
                model.AddedBy= Session["Pk_AdminId"].ToString();
                model.PK_TemplateId = PK_TemplateId;
                DataSet ds = model.DeletingSMSTemplate();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["SMSTemplateList"] = "SMS Template Deleted Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["SMSTemplateList"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SMSTemplateList"] = ex.Message;
                return View(model);
            }
            return RedirectToAction("SMSTemplateList");
        }

        #endregion

        #region SendSMS


        public ActionResult SMS(SMS model)
        {
            List<SelectListItem> ddlSMSTemplateName = new List<SelectListItem>();
            DataSet ds = model.GettingSMSTemplate();
            int count = 0;
            if (ds!=null && ds.Tables.Count > 0)
            {
                
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSMSTemplateName.Add(new SelectListItem { Text = "Select SMS Template", Value = "0" });
                    }
                    ddlSMSTemplateName.Add(new SelectListItem { Text = r["TemplateName"].ToString(), Value = r["Msg"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlSMSTemplateName = ddlSMSTemplateName;


            List<SelectListItem> ddlAssocCustom = Common.ddlAssocCustom();
            ViewBag.ddlAssocCustom = ddlAssocCustom;
            return View(model);
        }


        [HttpPost]
        [ActionName("SMS")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult GetDetails(SMS obj)
        {
          
            obj.AssocCustom = obj.AssocCustom == "0" ? null : obj.AssocCustom;
            List<SMS> list = new List<SMS>();
            DataSet ds = obj.GetSMSData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        SMS obj1 = new SMS();
                        obj1.LoginId = r["LoginId"].ToString();
                        obj1.PK_UserId = r["PK_UserId"].ToString();
                        obj1.FirstName = r["FirstName"].ToString();
                        obj1.PK_RoleId = r["Fk_RoleId"].ToString();
                        obj1.Mobile = r["Mobile"].ToString();
                        list.Add(obj1);

                    }
                    obj.lstsmsdata = list;
                }
            }


            #region Bind SMSTemplate
            SMS model1 = new SMS();
            List<SelectListItem> ddlSMSTemplateName = new List<SelectListItem>();
            DataSet ds1 = model1.GettingSMSTemplate();
            int count = 0;
            if (ds1 != null && ds1.Tables.Count > 0)
            {

                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlSMSTemplateName.Add(new SelectListItem { Text = "Select SMS Template", Value = "0" });
                    }
                    ddlSMSTemplateName.Add(new SelectListItem { Text = r["TemplateName"].ToString(), Value = r["Msg"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlSMSTemplateName = ddlSMSTemplateName;


            List<SelectListItem> ddlAssocCustom = Common.ddlAssocCustom();
            ViewBag.ddlAssocCustom = ddlAssocCustom;
            #endregion Bind SMSTemplate

            return View(obj);

        }



        [HttpPost]
        [ActionName("SMS")]
        [OnAction(ButtonName = "SendSMS")]
        public ActionResult SendSMS(SMS obj)
        {
            Session["MobileNo"] = null;
            obj.TotalSMS = obj.TotalSMS.Replace("SMS", "");
            string Hdrows = Request["Hdrows"].ToString();

            string chk = "";
            DataTable dtSMS = new DataTable();
            dtSMS.Columns.Add("FirstName", typeof(string));
            dtSMS.Columns.Add("Mobile", typeof(string));
            dtSMS.Columns.Add("Status", typeof(string));
            
            for (int i = 0; i <= int.Parse(Hdrows); i++)
            {
                chk = Request["chk_" + i];
                string mobile = Request["Mobile_" + i];
                string FirstName = Request["FirstName_" + i];
                string Status = "";
                if (chk == "on")
                {
                    if (mobile.Length < 10)
                    {
                        Status = "Failed";
                    }
                    else if (mobile.Length > 10)
                    {
                        Status = "Failed";
                    }
                    else
                    {
                        Status = "Send";
                    }
                    dtSMS.Rows.Add(FirstName, mobile, Status);
                    string message = obj.Msg + ' ' + Common.SoftwareDetails.CompanyName;
                    BLSMS.SendSMS(mobile, message);
                }
            }

            obj.dtSMS = dtSMS;
            obj.AddedBy = Session["Pk_AdminId"].ToString();

            DataSet ds = obj.SaveSMSData();
            TempData["SMS"] = "Message Send Successfully.";
            return RedirectToAction("SMS");

        }

        #endregion

    }
}