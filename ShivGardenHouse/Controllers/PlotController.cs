using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShivGardenHouse.Models;
using System.Data;
using ShivGardenHouse.Filter;
using System.Net;
using System.Xml;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace ShivGardenHouse.Controllers
{
    public class PlotController : AdminBaseController
    {
        public ActionResult PlotBooking(string PK_BookingId)
        {
            Plot model = new Plot();
            model.Discount = "0";
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;
                    model.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsBookingDetails.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsBookingDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();

                    model.BookingAmount = dsBookingDetails.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.BranchID = dsBookingDetails.Tables[0].Rows[0]["BranchID"].ToString();
                    model.PaymentPlanID = dsBookingDetails.Tables[0].Rows[0]["PK_PlanID"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    model.NetPlotAmount = dsBookingDetails.Tables[0].Rows[0]["NetPlotAmount"].ToString();

                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                    model.BlockID = dsBookingDetails.Tables[0].Rows[0]["PK_BlockID"].ToString();
                    model.PlotRate = dsBookingDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PaymentDate = dsBookingDetails.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                    model.PlotNumber = dsBookingDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                    model.PlotAmount = dsBookingDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsBookingDetails.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    model.TransactionNumber = dsBookingDetails.Tables[0].Rows[0]["TransactionNo"].ToString();
                    model.TransactionDate = dsBookingDetails.Tables[0].Rows[0]["TransactionDate"].ToString();
                    model.BankName = dsBookingDetails.Tables[0].Rows[0]["BankName"].ToString();
                    model.BankBranch = dsBookingDetails.Tables[0].Rows[0]["BankBranch"].ToString();
                    model.MLMLoginId = dsBookingDetails.Tables[0].Rows[0]["MLMLoginId"].ToString();
                    model.PlotSize = dsBookingDetails.Tables[0].Rows[0]["Totalarea"].ToString();
                    model.BookingPercent = dsBookingDetails.Tables[0].Rows[0]["BookingPercent"].ToString();
                }
            }
            else
            {
                model.BookingDate = DateTime.Now.ToString("dd/MM/yyyy");
                model.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = obj.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion

            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            return View(model);
        }

        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Plot model = new Plot();
                model.SiteID = SiteID;

                #region GetSiteRate
                //DataSet dsSiteRate = model.GetSiteList();
                //if (dsSiteRate != null)
                //{
                //    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                //    model.Result = "yes";
                //}
                #endregion
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                model.Result = "yes";
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                model.ddlSector = ddlSector;
                #endregion
                //#region SitePLCCharge
                //List<Master> lstPlcCharge = new List<Master>();
                //DataSet dsPlcCharge = model.GetPLCChargeList();

                //if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                //{
                //    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                //    {
                //        Master obj = new Master();
                //        obj.SiteName = r["SiteName"].ToString();
                //        obj.PLCName = r["PLCName"].ToString();
                //        obj.PLCCharge = r["PLCCharge"].ToString();
                //        obj.PLCID = r["PK_PLCID"].ToString();

                //        lstPlcCharge.Add(obj);
                //    }
                //    model.lstPLC = lstPlcCharge;
                //}
                //#endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetBlockList(string SiteID, string SectorID)
        {
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Master model = new Master();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            DataSet dsblock = model.GetBlockList();

            #region ddlBlock
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

            model.lstBlock = lstBlock;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckPlot(string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            Plot model = new Plot();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            DataSet dsblock = model.CheckPlotAvailibility();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = "no";
                }
                else
                {
                    model.Result = "yes";
                    model.BookingPercent = dsblock.Tables[0].Rows[0]["BookingPercent"].ToString();
                    model.PlotSize = dsblock.Tables[0].Rows[0]["TotalArea"].ToString();
                    model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmount"].ToString();
                    model.TotalPLC = dsblock.Tables[0].Rows[0]["TotalPLC"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
            //return View();
        }

        public ActionResult GetCustomerNameFromCustomerID(string CustomerID)
        {
            try
            {
                Plot model = new Plot();

                model.LoginId = CustomerID;

                #region GetCustomerName
                DataSet dsCustomerName = model.GetCustomerName();
                if (dsCustomerName != null && dsCustomerName.Tables[0].Rows.Count > 0)
                {
                    model.CustomerName = dsCustomerName.Tables[0].Rows[0]["Name"].ToString();
                    model.LoginId = dsCustomerName.Tables[0].Rows[0]["LoginId"].ToString();
                    model.AssociateID = dsCustomerName.Tables[0].Rows[0]["AssociateLoginId"].ToString();
                    model.AssociateName = dsCustomerName.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.CustomerID = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetAssociateName(string AssociateID)
        {
            try
            {
                Plot model = new Plot();
                model.LoginId = AssociateID;

                #region GetSiteRate
                DataSet dsAssociateName = model.GetSponsorName();
                if (dsAssociateName != null && dsAssociateName.Tables[0].Rows.Count > 0)
                {
                    model.AssociateName = dsAssociateName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsAssociateName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.AssociateName = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("SavePlotBooking")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotBooking(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.Discount = string.IsNullOrEmpty(obj.Discount) ? "0" : obj.Discount;
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                obj.BookingDate = string.IsNullOrEmpty(obj.BookingDate) ? null : Common.ConvertToSystemDate(obj.BookingDate, "dd/MM/yyyy");

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.MLMLoginId = "1";
                DataSet ds = obj.SavePlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Plot"] = "Plot Booked successfully !";
                        TempData["Booking"] = "Booking ID : " + ds.Tables[0].Rows[0]["BookingNo"].ToString();

                        string Bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        string Bookamt = ds.Tables[0].Rows[0]["BookingAmt"].ToString();
                        string AsstName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                        string plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string str = BLSMS.Booking(Bookno, Bookamt, AsstName, plot);
                        try
                        {
                            BLSMS.SendSMS(mob, str);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        TempData["Booking"] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
                TempData["Booking"] = "";
            }
            FormName = "PlotBooking";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PlotBookingList(Plot model)
        {

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }

        [HttpPost]
        [ActionName("PlotBookingList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetBookingList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetBookingDetailsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
                        obj.PK_BookingId = r["PK_BookingId"].ToString();
                        obj.BranchID = r["BranchID"].ToString();
                        obj.BranchName = r["BranchName"].ToString();
                        obj.CustomerID = r["CustomerID"].ToString();
                        obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.AssociateID = r["AssociateID"].ToString();
                        obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.BookingDate = r["BookingDate"].ToString();
                        obj.BookingAmount = r["BookingAmt"].ToString();
                        obj.PaymentPlanID = r["PlanName"].ToString();
                        obj.BookingNumber = r["BookingNo"].ToString();
                        obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_BookingDetailsId"].ToString());
                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }

        //----------------------------------------------------------------------------------------------

        public ActionResult PlotBookingListForCancel(Plot model)
        {

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }

        [HttpPost]
        [ActionName("PlotBookingListForCancel")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetBookingList1(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetBookingDetailsList1();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
                        obj.PK_BookingId = r["PK_BookingId"].ToString();
                        obj.BranchID = r["BranchID"].ToString();
                        obj.BranchName = r["BranchName"].ToString();
                        obj.CustomerID = r["CustomerID"].ToString();
                        obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.AssociateID = r["AssociateID"].ToString();
                        obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.BookingDate = r["BookingDate"].ToString();
                        obj.BookingAmount = r["BookingAmt"].ToString();
                        obj.PaymentPlanID = r["PlanName"].ToString();
                        obj.BookingNumber = r["BookingNo"].ToString();

                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }


        //-----------------------------------------------------------------------------------------------

        [HttpPost]
        [ActionName("SavePlotBooking")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdatePlotBooking(Plot model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (model.TransactionDate == "")
                {
                    model.TransactionDate = null;
                }
                else
                {
                    model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/mm/yyyy");
                }
                model.BookingDate = string.IsNullOrEmpty(model.BookingDate) ? null : Common.ConvertToSystemDate(model.BookingDate, "dd/mm/yyyy");
                model.PaymentDate = string.IsNullOrEmpty(model.PaymentDate) ? null : Common.ConvertToSystemDate(model.PaymentDate, "dd/mm/yyyy");

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.MLMLoginId = "1";
                DataSet ds = model.UpdatePlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot updated successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotBooking";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult CancelPlotBooking(string BookingID, string Remark)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.PK_BookingId = BookingID;
                model.CancelRemark = Remark;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.CancelPlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot Booking Cancelled successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotBookingListForCancel";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult CancelledPlotBookingList(Plot model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("CancelledPlotBookingList")]
        [OnAction(ButtonName = "SearchCancelled")]
        public ActionResult GetCancelledBookingList(Plot model)
        {
            List<Plot> lst = new List<Plot>();

            DataSet ds = model.GetCancelledBookingDetailsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingId = r["PK_BookingId"].ToString();
                    obj.BranchID = r["BranchName"].ToString();
                    obj.CustomerName = r["CustomerInfo"].ToString();
                    obj.AssociateName = r["AssociateInfo"].ToString();
                    obj.PlotNumber = r["Plot"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.BookingAmount = r["BookingAmt"].ToString();
                    obj.PaymentPlanID = r["PlanName"].ToString();
                    obj.BookingStatus = r["BookingStatus"].ToString();
                    obj.CancelRemark = r["CancelRemark"].ToString();
                    obj.CancelDate = r["CancelledDate"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        #region  HoldPlot
        public ActionResult HoldPlot(string PK_PlotHoldID)
        {
            Plot model = new Plot();
            if (PK_PlotHoldID != null)
            {


                model.PK_PlotHoldID = PK_PlotHoldID;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_PlotHoldID;
                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();

                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                    model.BlockID = dsBookingDetails.Tables[0].Rows[0]["FK_BlockID"].ToString();
                    model.HoldFrom = dsBookingDetails.Tables[0].Rows[0]["HoldFrom"].ToString();
                    model.HoldTo = dsBookingDetails.Tables[0].Rows[0]["HoldTo"].ToString();
                    model.Name = dsBookingDetails.Tables[0].Rows[0]["Name"].ToString();
                    model.Mobile = dsBookingDetails.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Remark = dsBookingDetails.Tables[0].Rows[0]["Remark1"].ToString();
                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion


            #region ddlPlotHoldType
            List<SelectListItem> PlotHoldType = Common.PlotHoldType();
            ViewBag.PlotHoldType = PlotHoldType;
            #endregion ddlPlotHoldType

            return View(model);

        }

        [HttpPost]
        [ActionName("SavePlotHold")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotHold(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SavePlotHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = " Plot is on Hold !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "HoldPlot";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PlotHoldList(Plot model)
        {
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }

        [HttpPost]
        [ActionName("PlotHoldList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetPlotHoldList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            DataSet ds = model.GetPlotHoldList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_PlotHoldID = r["PK_PlotHoldID"].ToString();
                    obj.PlotNumber = r["Plot"].ToString();
                    obj.HoldFrom = r["HoldFrom"].ToString();
                    obj.HoldTo = r["HoldTo"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.HoldType = r["HoldType"].ToString();
                    obj.Amount = r["Amount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock = model.GetBlockList();


            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            return View(model);
            #endregion
        }



        public ActionResult DeletePlotHold(string PK_PlotHoldID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.PK_PlotHoldID = PK_PlotHoldID;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeletePlotHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DelPlot"] = "Deleted successfully !";
                    }
                    else
                    {
                        TempData["DelPlot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DelPlot"] = ex.Message;
            }
            FormName = "PlotHoldList";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }


        #endregion

        #region  Plot Allotment
        public ActionResult PlotAllotment(string PK_BookingId)
        {

            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion


                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion


            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }

        //[HttpPost]
        //[ActionName("PlotBookingDetails")]
        //[OnAction(ButtonName = "SearchPlot")]
        public ActionResult PlotBookingDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();

            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.FillBookedPlotDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";

                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                }
                else
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        [ActionName("SavePlotAllotment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotAllotment(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                //obj.TransactionDate = obj.TransactionDate == "" || obj.TransactionDate = null&&(Convert.obj.TransactionDate);

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SavePlotAllotment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = " Plot Allotted successfully !";
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string amt = obj.PaidAmount;

                        string str = BLSMS.PlotAllotment(name, Plot, amt);
                        try
                        {
                            BLSMS.SendSMS(mob, str);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotAllotment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        [HttpPost]
        [ActionName("SavePlotAllotment")]
        [OnAction(ButtonName = "SaveAndPrint")]
        public ActionResult SavePlotAllotmentAndPrint(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                //obj.TransactionDate = obj.TransactionDate == "" || obj.TransactionDate = null&&(Convert.obj.TransactionDate);

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");

                DataSet ds = obj.SavePlotAllotment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["PK_BookingDetailsId"] = ds.Tables[0].Rows[0]["PK_BookingDetailsId"].ToString();
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string amt = obj.PaidAmount;

                        string str = BLSMS.PlotAllotment(name, Plot, amt);
                        try
                        {
                            BLSMS.SendSMS(mob, str);
                        }
                        catch { }

                        return RedirectToAction("PrintAllotment", "AdminReports");



                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotAllotment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region EMIPayment

        public ActionResult EMIPayment(string PK_BookingId)
        {

            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion


            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }

        public ActionResult Details(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.FillBookedPlotDetailsForEmi();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    model.Result = "yes";
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();
                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsblock.Tables[1].Rows)
                        {
                            Plot obj = new Plot();
                            obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                            obj.PK_BookingId = r["Fk_BookingId"].ToString();
                            obj.InstallmentNo = r["InstallmentNo"].ToString();
                            obj.InstallmentDate = r["InstallmentDate"].ToString();
                            obj.PaymentDate = r["PaymentDate"].ToString();
                            obj.PaidAmount = r["PaidAmount"].ToString();
                            obj.InstallmentAmount = r["InstAmt"].ToString();
                            obj.PaymentMode = r["PaymentModeName"].ToString();
                            obj.DueAmount = r["DueAmount"].ToString();
                            obj.CssClass = r["CssClass"].ToString();

                            lst.Add(obj);
                        }
                        model.lstPlot = lst;
                    }

                }
                else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("SaveEMI")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveEMI(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                //obj.TransactionDate = obj.TransactionDate == "" || obj.TransactionDate = null&&(Convert.obj.TransactionDate);

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveEMIPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {

                        TempData["Plot"] = " EMI Paid !";
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        string instno = ds.Tables[0].Rows[0]["InstallmentNo"].ToString();
                        string amt = obj.PaidAmount;


                        string str = BLSMS.EMIPayment(name, Plot, bookno, instno, amt);
                        try
                        {
                            BLSMS.SendSMS(mob, str);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "EMIPayment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("SaveEMI")]
        [OnAction(ButtonName = "SaveAndPrint")]
        public ActionResult SaveEMIandPrint(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                //obj.TransactionDate = obj.TransactionDate == "" || obj.TransactionDate = null&&(Convert.obj.TransactionDate);

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveEMIPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["PK_BookingDetailsId"] = ds.Tables[0].Rows[0]["PK_BookingDetailsId"].ToString();
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        string instno = ds.Tables[0].Rows[0]["InstallmentNo"].ToString();
                        string amt = obj.PaidAmount;


                        string str = BLSMS.EMIPayment(name, Plot, bookno, instno, amt);
                        try
                        {
                            BLSMS.SendSMS(mob, str);
                        }
                        catch { }
                        return RedirectToAction("PrintAllotment", "AdminReports");
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "EMIPayment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region  ChequNeftCash

        public ActionResult Payment(Plot model)

        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("Payment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            DataSet ds = model.GetPaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Details"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }

        public ActionResult ApprovePayment(string UserID, string Description)
        {
            string FormName = "";
            string Controller = "";

            Plot model = new Plot();
            try
            {
                model.UserID = UserID;
                model.Description = Description;

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Result = "yes";
                DataSet ds = model.ApprovePayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                    }
                    else
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "Payment";
            Controller = "Plot";

            return Json(model, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RejectPayment(string UserID, string Description)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.UserID = UserID;
                model.Description = Description;

                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.RejectPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Payment Rejected  !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "Payment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region PaymentApproval/RejectionReport

        public ActionResult PaymentReport(Plot model)
        {
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus
            return View(model);
        }

        [HttpPost]
        [ActionName("PaymentReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetReport(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/mm/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/mm/yyyy");
            DataSet ds = model.GetPaymentReportList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    //  obj.RejectedDate = r["RejectedDate"].ToString();
                    obj.ApproveDescription = r["Description"].ToString();
                    //  obj.RejectDescription = r["RejectDescription"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus
            return View(model);
        }


        #endregion 

        #region ApproveRejectedPayment

        public ActionResult ApproveRejectedPayment(Plot model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion

            #region ddlPaymentMode

            List<SelectListItem> ddlPaymentMode1 = new List<SelectListItem>();

            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode1.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode1.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode1 = ddlPaymentMode1;
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("ApproveRejectedPayment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetListOfRejectedPayment(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Details"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            #region ddlPaymentMode

            List<SelectListItem> ddlPaymentMode1 = new List<SelectListItem>();

            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode1.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode1.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode1 = ddlPaymentMode1;
            #endregion
            return View(model);
        }

        public ActionResult ApproveRejPayment(string UserID, string Description, string ApprovedDate, string PaymentMode, string ChequeNo, string ChequeDate, string BankName, string BankBranch)
        {
            Plot model = new Plot();
            string FormName = "";
            string Controller = "";
            try
            {


                model.UserID = UserID;
                model.Description = Description;
                model.ApprovedDate = string.IsNullOrEmpty(ApprovedDate) ? null : Common.ConvertToSystemDate(ApprovedDate, "dd/MM/yyyy");
                model.PaymentMode = PaymentMode;
                model.TransactionNumber = ChequeNo;
                model.TransactionDate = string.IsNullOrEmpty(ChequeDate) ? null : Common.ConvertToSystemDate(ChequeDate, "dd/MM/yyyy");
                model.BankBranch = BankBranch;
                model.BankName = BankName;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.ApproveRejectPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                    }
                    else
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "ApproveRejectedPayment";
            Controller = "Plot";

            return Json(model, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region RejectPaymentApproveReport

        public ActionResult RejectPaymentApproveReport(Plot model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("RejectPaymentApproveReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetRejectPaymentApproveReport(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            DataSet ds = model.GetPaymentRejAppReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    // obj.ApproveDescription = r["ApproveDescription"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    //   obj.RejectedDate = r["RejectedDate"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }


        #endregion 

        #region TransferBooking

        public ActionResult TransferPlotBooking(string PK_BookingId)
        {
            Plot model = new Plot();
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);



        }
        public ActionResult BookingDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();

            DataSet dsBookingDetails = model.GetBookingDetailsList();


            //model.SiteID = SiteID;
            //model.SectorID = SectorID;
            //model.BlockID = BlockID;
            //model.PlotNumber = PlotNumber;
            //model.BookingNumber = BookingNumber;
            model.SiteID = SiteID == "0" ? null : SiteID;
            model.SectorID = SectorID == "0" ? null : SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
            DataSet dd = model.FillBookedPlotDetails();
            if (dd != null && dd.Tables[0].Rows.Count > 0)
            {

                if (dd.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";

                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dd.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dd.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dd.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dd.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dd.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dd.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dd.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dd.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dd.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dd.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dd.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dd.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dd.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dd.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dd.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dd.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dd.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dd.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dd.Tables[0].Rows[0]["BalanceAmount"].ToString();
                }
                else
                {
                    model.Result = dd.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {

            }
            #region ddlSite
            int count1 = 0;
            Master objmaster = new Master();
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = objmaster.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = objmaster.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = objmaster.GetBlockList();


            if (dsblock1 != null && dsblock1.Tables.Count > 0 && dsblock1.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock1.Tables[0].Rows)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            #endregion
            return Json(model, JsonRequestBehavior.AllowGet);


        }
        #endregion 

        public ActionResult GetMLMLoginId(string LoginId)
        {
            Common datalist = new Common();
            string soapResult = "";
            HttpWebRequest request = GetMLmUserUrl();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            string json1 = "{\"ReferBy\": \"" + LoginId + "\"}";
            byte[] postBytes = Encoding.UTF8.GetBytes(json1);
            Stream requestStream = request.GetRequestStream();
            // now send it
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();

                }

                datalist = JsonConvert.DeserializeObject<Common>(soapResult);
            }
            return Json(datalist, JsonRequestBehavior.AllowGet);
        }
        public static HttpWebRequest GetMLmUserUrl()
        {

            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"http://www.Manglam Infraworld.com/Home/GetName");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }
        public static HttpWebRequest GeneratePinUrl()
        {

            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"http://www.Manglam Infraworld.com/Home/GeneratePlotPin");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
        }


        #region PlotRegistree
        public ActionResult PlotRegistry(string PK_BookingId)
        {
            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion


                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion


            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);


        }
        public ActionResult PlotBookDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();

            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.FillBookedPlotDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";

                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                }
                else
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        [ActionName("PlotRegistry")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotRegistry(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.RegistrationDate = string.IsNullOrEmpty(obj.RegistrationDate) ? null : Common.ConvertToSystemDate(obj.RegistrationDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();


                DataSet ds = obj.SavePlotRegistry();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["SavePlotRegistry"] = " Plot Registered successfully !";
                    }
                    else
                    {
                        TempData["SavePlotRegistry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SavePlotRegistry"] = ex.Message;
            }
            FormName = "PlotRegistry";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult PlotRegistryList(string PK_BookingId)
        {
            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion


                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion


            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);


        }
        [HttpPost]
        [ActionName("PlotRegistryList")]
        [OnAction(ButtonName ="Search")]
        public ActionResult PlotRegistryListById(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetplotRegistryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["PlotRegistry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
                        obj.PK_BookingId = r["PK_BookingId"].ToString();
                        obj.BranchID = r["BranchID"].ToString();
                        obj.BranchName = r["BranchName"].ToString();
                        obj.CustomerID = r["CustomerID"].ToString();
                        obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                        obj.CustomerName = r["CustomerName"].ToString();
                        obj.AssociateID = r["AssociateID"].ToString();
                        obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.BookingNumber = r["BookingNo"].ToString();
                        obj.PlotNumber = r["PlotNumber"].ToString();
                        obj.RegistreeNo = r["RegistreeNo"].ToString();
                        obj.KhadraNo = r["KhadraNo"].ToString();
                        obj.RegistrationDate = r["RegistreeDate"].ToString();
                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }
        #endregion

        #region UpdateMLMID

        public ActionResult UpdateMLMID(string PK_BookingId)
        {

            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion


                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    }
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion


            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }


        public ActionResult UpdateMLMIDDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();

            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.UpdateMLMIDDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";

                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.Discount = dsblock.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentPlanID = dsblock.Tables[0].Rows[0]["Fk_PlanId"].ToString();
                    model.PlanName = dsblock.Tables[0].Rows[0]["PlanName"].ToString();
                    model.PK_BookingId = dsblock.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    model.TotalAllotmentAmount = dsblock.Tables[0].Rows[0]["TotalAllotmentAmount"].ToString();
                    model.PaidAllotmentAmount = dsblock.Tables[0].Rows[0]["PaidAllotmentAmount"].ToString();
                    model.BalanceAllotmentAmount = dsblock.Tables[0].Rows[0]["BalanceAllotmentAmount"].ToString();
                    model.TotalInstallment = dsblock.Tables[0].Rows[0]["TotalInstallment"].ToString();
                    model.InstallmentAmount = dsblock.Tables[0].Rows[0]["InstallmentAmount"].ToString();
                    model.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                    model.Balance = dsblock.Tables[0].Rows[0]["BalanceAmount"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                }
                else
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        [ActionName("UpdateMLMID")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveUpdateMLMID(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.SaveUpdateMLMID();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PlotMLMID"] = " MLM ID updated successfully !";

                    }
                    else
                    {
                        TempData["PlotMLMID"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "UpdateMLMID";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region CalculationWith

        public ActionResult CalculationWith(Plot model)

        {

            return View(model);
        }

        [HttpPost]
        [ActionName("CalculationWith")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetCalculationWith(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.ListCalculationWith();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Details"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.CalculatedWith = r["CalculatedWith"].ToString();
                    lst.Add(obj);
                }
                model.lstCalculationWith = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }


        public ActionResult Traditional(string PK_BookingDetailsId)
        {
            string FormName = "";
            string Controller = "";

            Plot model = new Plot();
            try
            {
                model.PK_BookingDetailsId = PK_BookingDetailsId;

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Result = "yes";
                model.CalculatedWith = "MLM";
                model.CalculatedWith = "Traditional";
                DataSet ds = model.PaymentCalculatedWith();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                    }
                    else
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["CalculationWith"] = ex.Message;
            }
            FormName = "CalculationWith";
            Controller = "Plot";

            return Json(model, JsonRequestBehavior.AllowGet);

        }


        public ActionResult MLM(string PK_BookingDetailsId)
        {
            string FormName = "";
            string Controller = "";

            Plot model = new Plot();
            try
            {
                model.PK_BookingDetailsId = PK_BookingDetailsId;

                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Result = "yes";
                model.CalculatedWith = "MLM";
                DataSet ds = model.PaymentCalculatedWith();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Result = "1";
                    }
                    else
                    {
                        model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["CalculationWith"] = ex.Message;
            }
            FormName = "CalculationWith";
            Controller = "Plot";

            return Json(model, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region CalculatedReport

        public ActionResult CalculatedReport(Plot model)
        {
            return View(model);
        }

        [HttpPost]
        [ActionName("CalculatedReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetCalculatedReport(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.ListGetCalculatedReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Details"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.CalculatedWith = r["CalculatedWith"].ToString();
                    lst.Add(obj);
                }
                model.lstCalculation = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }

        #endregion

    }
}
 