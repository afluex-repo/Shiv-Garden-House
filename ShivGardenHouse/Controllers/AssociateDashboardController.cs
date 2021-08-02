using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShivGardenHouse.Filter;
using ShivGardenHouse.Models;

namespace ShivGardenHouse.Controllers
{
    public class AssociateDashboardController : BaseController
    {
        public ActionResult AssociateDashBoard()
        {
            //ViewBag.Name = Session["FullName"].ToString();
            AssociateBooking newdata = new AssociateBooking();
            try
            {
                List<AssociateBooking> lstInst = new List<AssociateBooking>();
                newdata.AssociateID = Session["Pk_userId"].ToString();
                DataSet dsInst = newdata.GetDueInstallmentList();
                if (dsInst != null && dsInst.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsInst.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();

                        obj.CustomerID = r["PK_UserId"].ToString();
                        obj.CustomerLoginID = r["LoginId"].ToString();
                        obj.CustomerName = r["FirstName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        obj.InstallmentAmount = r["InstAmt"].ToString();


                        lstInst.Add(obj);
                    }

                    newdata.ListInstallment = lstInst;
                }



                List<AssociateBooking> lstNews = new List<AssociateBooking>();
                DataSet dsAssociate = newdata.GetNews();
                if (dsAssociate != null && dsAssociate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsAssociate.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();
                        //   obj.PK_BookingId = r["PK_UserId"].ToString();

                        obj.PK_NewsID = r["PK_NewsID"].ToString();
                        obj.NewsHeading = r["NewsHeading"].ToString();
                        obj.NewsBody = r["NewsBody"].ToString();


                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lstNews.Add(obj);
                    }

                    newdata.ListNEWS = lstNews;
                }
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(newdata);
        }

        public ActionResult GetGraphDetailsOfPlot()
        {
            List<AssociateBooking> dataList = new List<AssociateBooking>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            AssociateBooking newdata = new AssociateBooking();
            newdata.LoginId = Session["LoginId"].ToString();
            Ds = newdata.BindGraphDetails();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    AssociateBooking details = new AssociateBooking();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPlotBookingOnGraph()
        {

            List<AssociateBooking> dataList3 = new List<AssociateBooking>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            AssociateBooking newdata = new AssociateBooking();
            newdata.AssociateID = Session["Pk_userId"].ToString();

            Ds = newdata.GetDetailsForBarGraph();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[0].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    AssociateBooking details = new AssociateBooking();


                    details.TotalBooking = (dr["TotalBooking"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList3.Add(details);

                    count++;
                }
            }
            return Json(dataList3, JsonRequestBehavior.AllowGet);
        }

        #region AssociateBookings
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

        public ActionResult AssociateBookingList(AssociateBooking model)
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
        [ActionName("AssociateBookingList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult BookingList(AssociateBooking model)
        {
            model.LoginId = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.List();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
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
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    obj.BookingStatus = r["BookingStatus"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
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

            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Sector", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }


        #endregion

        #region Customer Ledger Report
        public ActionResult CustomerLedgerReport(string PK_BookingId)
        {


            AssociateBooking model = new AssociateBooking();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PK_BookingId = PK_BookingId;

            DataSet dsBookingDetails = model.GetBookingDetailsList();
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

        public ActionResult Details(string BookingNumber, string LoginId, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            AssociateBooking model = new AssociateBooking();
            model.LoginId = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();

            model.SiteID = SiteID == "0" ? null : SiteID;
            model.SectorID = SectorID == "0" ? null : SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
            DataSet dsblock = model.FillDetails();
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


                }

            }
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[1].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.PK_BookingId = r["Fk_BookingId"].ToString();
                    obj.InstallmentNo = r["InstallmentNo"].ToString();
                    obj.InstallmentDate = r["InstallmentDate"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.InstallmentAmount = r["InstAmt"].ToString();
                    obj.PaymentMode = r["PaymentModeName"].ToString();
                    obj.DueAmount = r["DueAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            else
            {
                model.Result = "No record found !";

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

        #region AssociateDownline

        public ActionResult AssociateDownlineDetail(AssociateBooking model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }


        #endregion 

        #region AssociateUpline

        public ActionResult AssociateUplineDetail(AssociateBooking model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }


        #endregion

        #region ChangePasswordAssociate


        public ActionResult ChangePasswordAssociate()
        {

            return View();
        }
        [HttpPost]
        [ActionName("ChangePasswordAssociate")]
        [OnAction(ButtonName = "Change")]
        public ActionResult UpdatePassword(AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.Password = Crypto.Encrypt(obj.Password);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                obj.UpdatedBy = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Login"] = "Password updated successfully..";
                        FormName = "Login";
                        Controller = "Home";
                    }
                    else
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangePasswordAssociate";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "ChangePasswordAssociate";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }



        #endregion

        #region EditProfile


        public ActionResult EditProfile(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            model.LoginId = Session["LoginId"].ToString();

            try
            {
                model.UserID = Session["Pk_userId"].ToString();
                model.LoginId = Session["LoginId"].ToString();
                DataSet dsPlotDetails = model.GetList();
                if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                {
                    model.UserID = Session["Pk_userId"].ToString();
                    model.SponsorID = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                    model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                    model.DesignationID = dsPlotDetails.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    model.DesignationName = dsPlotDetails.Tables[0].Rows[0]["DesignationName"].ToString();
                    model.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                    model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                    model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                    model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                    model.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                    model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.BranchName = dsPlotDetails.Tables[0].Rows[0]["BranchName"].ToString();
                    model.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();
                }

                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        [ActionName("EditProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult Update(HttpPostedFileBase postedFile, AssociateBooking model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (postedFile != null)
                {
                    model.ProfilePic = "/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                    Session["ProfilePic"] = model.ProfilePic;
                }
                model.UpdatedBy = Session["Pk_userId"].ToString();

                DataSet ds = model.UpdatePersonalDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = " Updated successfully !";
                    }
                    else
                    {
                        TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "EditProfile";
            Controller = "AssociateDashboard";

            return RedirectToAction(FormName, Controller);
        }

        #endregion 
        public ActionResult Tree1(DashBoard model)
        {
            model.LoginId = Session["LoginId"].ToString();

            List<DashBoard> lst = new List<DashBoard>();

            DataSet ds = model.GetTreeDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DashBoard obj = new DashBoard();
                    obj.Fk_ParentId = r["Parentid"].ToString();
                    obj.DisplayName = r["MemberName"].ToString();
                    obj.Fk_UserId = r["PK_UserId"].ToString();
                    lst.Add(obj);
                }
                model.lsttree = lst;
            }

            return View(model);


        }

        public ActionResult Tree()
        {
            List<object> treedata = new List<object>();
            treedata.Add(new
            {
                id = 1,
                name = "Australia",
                hasChild = true,
                expanded = true
            });
            treedata.Add(new
            {
                id = 2,
                pid = 1,
                name = "New South Wales",
                is_selected = true


            });
            treedata.Add(new
            {
                id = 3,
                pid = 1,
                name = "Victoria"
            });

            treedata.Add(new
            {
                id = 4,
                pid = 1,
                name = "South Australia"
            });
            treedata.Add(new
            {
                id = 6,
                pid = 1,
                name = "Western Australia",
                is_selected = true

            });
            treedata.Add(new
            {
                id = 7,
                name = "Brazil",
                hasChild = true
            });
            treedata.Add(new
            {
                id = 8,
                pid = 7,
                name = "Paraná"
            });
            treedata.Add(new
            {
                id = 9,
                pid = 7,
                name = "Ceará"
            });
            treedata.Add(new
            {
                id = 10,
                pid = 7,
                name = "Acre"
            });
            treedata.Add(new
            {
                id = 11,
                name = "China",
                hasChild = true
            });
            treedata.Add(new
            {
                id = 12,
                pid = 11,
                name = "Guangzhou",
            });
            treedata.Add(new
            {
                id = 13,
                pid = 11,
                name = "Shanghai"
            });
            treedata.Add(new
            {
                id = 14,
                pid = 11,
                name = "Beijing"
            });
            treedata.Add(new
            {
                id = 15,
                pid = 11,
                name = "Shantou"

            });
            treedata.Add(new
            {
                id = 16,
                name = "France",
                hasChild = true

            });
            treedata.Add(new
            {
                id = 17,
                pid = 16,
                name = "Pays de la Loire"

            });
            treedata.Add(new
            {
                id = 18,
                pid = 16,
                name = "Aquitaine"

            });
            treedata.Add(new
            {
                id = 19,
                pid = 16,
                name = "Brittany"

            });
            treedata.Add(new
            {
                id = 20,
                pid = 16,
                name = "Lorraine"
            });
            treedata.Add(new
            {
                id = 21,
                name = "India",
                hasChild = true

            });
            treedata.Add(new
            {
                id = 22,
                pid = 21,
                name = "Assam"

            });
            treedata.Add(new
            {
                id = 23,
                pid = 21,
                name = "Bihar"
            });
            treedata.Add(new
            {
                id = 24,
                pid = 21,
                name = "Tamil Nadu"

            });
            ViewBag.dataSource = treedata;
            return View();
        }


        #region  associate tree
        public ActionResult AssociateTree(AssociateBooking model, string AssociateID)
        {
            if (AssociateID != null)
            {
                model.Fk_UserId = AssociateID;
            }
            else
            {
                model.Fk_UserId = Session["Pk_UserId"].ToString();
            }
            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineTree();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Fk_SponsorId = ds.Tables[0].Rows[0]["Fk_SponsorId"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                   
                    obj.Status = r["Status"].ToString();
                    obj.ActiveStatus = r["ActiveStatus"].ToString();
                    lst.Add(obj);
                   
                }
                model.lstPlot = lst;
            }


            return View(model);
        }

        //[HttpPost]
        //[ActionName("AssociateTree")]
        //[OnAction(ButtonName = "Search")]
        //public ActionResult GetDownlineList(AssociateBooking model)
        //{
        //    model.LoginId = Session["LoginId"].ToString();
        //    List<AssociateBooking> lst = new List<AssociateBooking>();

        //    DataSet ds = model.GetDownlineDetails();

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds.Tables[0].Rows)
        //        {
        //            AssociateBooking obj = new AssociateBooking();
        //            obj.AssociateID = r["AssociateId"].ToString();
        //            obj.AssociateName = r["AssociateName"].ToString();
        //            obj.DesignationName = r["DesignationName"].ToString();
        //            obj.Percentage = r["Percentage"].ToString();
        //            obj.BranchName = r["BranchName"].ToString();
        //            obj.Contact = r["Mobile"].ToString();
        //            lst.Add(obj);
        //        }
        //        model.lstPlot = lst;
        //    }

        //    return View(model);
        //}


        public ActionResult LevelTree()
        {

            return View();
        }
        #endregion


        public ActionResult PayoutDetails(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();
                    obj.PK_PaidPayoutId = r["PK_PaidPayoutId"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("PayoutDetails")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutDetailsBy(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }

        public ActionResult ClosingWisePayoutDetails(string PK_PaidPayoutId)
        {
            AssociateBooking model = new AssociateBooking();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            model.PK_PaidPayoutId = PK_PaidPayoutId;
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetPayoutWiseIncomeDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                ViewBag.DisplayName = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.DisplayName = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.FromDate = ds.Tables[0].Rows[0]["FromDate"].ToString();
                ViewBag.ToDate = ds.Tables[0].Rows[0]["ToDate"].ToString();
                ViewBag.TotalIncome = ds.Tables[0].Rows[0]["TotalIncome"].ToString();
                ViewBag.TDS = ds.Tables[0].Rows[0]["TDS"].ToString();
                ViewBag.Processing = ds.Tables[0].Rows[0]["Processing"].ToString();
                ViewBag.NetIncome = ds.Tables[0].Rows[0]["NetIncome"].ToString();
                ViewBag.SelfIncome = ds.Tables[0].Rows[0]["SelfIncome"].ToString();
                ViewBag.SelfPercentage = ds.Tables[0].Rows[0]["SelfPercentage"].ToString();
                ViewBag.SelfBusiness = ds.Tables[0].Rows[0]["SelfBusiness"].ToString();
                ViewBag.PayoutNo = ds.Tables[0].Rows[0]["PayoutNo"].ToString();
                ViewBag.TeamBusiness = ds.Tables[0].Rows[0]["TeamBusiness"].ToString();
                ViewBag.Difference = Convert.ToDecimal(ViewBag.TotalIncome) - Convert.ToDecimal(ViewBag.SelfIncome);
                ViewBag.TotalBusiness = Convert.ToDecimal(ViewBag.SelfBusiness) + Convert.ToDecimal(ViewBag.TeamBusiness);
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.FromTeam = r["FromTeam"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.ProjectName = r["ProjectName"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();

                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.AmountType = r["AmountType"].ToString();
                    obj.DepositAmount = r["DepositAmount"].ToString();
                    obj.IncomeType = r["IncomeType"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.Income = Convert.ToDecimal(r["Income"]);
                    obj.PlotNumber = r["PlotNumber"].ToString();

                    lst.Add(obj);
                }
                model.ClosingWisePayoutlist = lst;
            }
            return View(model);
        }

    }
}
