using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShivGardenHouse.Models;
using ShivGardenHouse.Filter;
namespace ShivGardenHouse.Controllers
{
    public class AdminController : AdminBaseController
    {
        public ActionResult AdminDashBoard()
        {
            DashBoard newdata = new DashBoard();
            try
            {
                DataSet Ds = newdata.GetDetails();
                ViewBag.Associates = Ds.Tables[0].Rows[0]["Associates"].ToString();
                ViewBag.Customers = Ds.Tables[0].Rows[0]["TotalCustomers"].ToString();
                ViewBag.Plots = Ds.Tables[0].Rows[0]["Plots"].ToString();
                ViewBag.TotalBusiness = Ds.Tables[0].Rows[0]["TotalBusiness"].ToString();

                List<DashBoard> lst = new List<DashBoard>();
                DataSet dsblock = newdata.GetBookingDetailsList();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsblock.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();
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
                        obj.BookingDate = r["BookingDate"].ToString();
                        obj.BookingStatus = r["BookingStatus"].ToString();
                        obj.PlotRate = r["PlotRate"].ToString();
                        obj.Amount = r["NetPlotAmount"].ToString();
                        
                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lst.Add(obj);
                    }

                    newdata.List = lst;
                }
                List<DashBoard> lstAssociate = new List<DashBoard>();
                DataSet dsAssociate = newdata.GetAssociateDetails();
                if (dsAssociate != null && dsAssociate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsAssociate.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();
                        //   obj.PK_BookingId = r["PK_UserId"].ToString();

                        obj.AssociateName = r["AssociateName"].ToString();
                        obj.JoiningDate = r["JoiningDate"].ToString();
                        obj.FK_DesignationID = r["FK_DesignationID"].ToString();
                        obj.DesignationName = r["DesignationName"].ToString();
                        obj.ProfilePic = r["ProfilePic"].ToString();
                       
                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lstAssociate.Add(obj);
                    }

                    newdata.ListAssociate = lstAssociate;
                }

                List<DashBoard> lstInst = new List<DashBoard>();
                DataSet dsInst = newdata.GetDueInstallmentList();
                if (dsInst != null && dsInst.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsInst.Tables[0].Rows)
                    {
                        DashBoard obj = new DashBoard();

                        obj.CustomerID = r["Customer"].ToString();
                        obj.CustomerLoginID = r["LoginId"].ToString();
                        obj.CustomerName = r["FirstName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        obj.InstallmentAmount = r["InstAmt"].ToString();
                        obj.IntallmentDate = r["InstallmentDate"].ToString();

                        lstInst.Add(obj);
                    }

                    newdata.ListInstallment = lstInst;
                }
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(newdata);
        }

        //public ActionResult BookingList()
      
        public ActionResult GetGraphDetails()
        {
            List<DashBoard> dataList = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.BindGraphDetails();
            if (Ds.Tables.Count > 0)
            {
                
                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetGraphDetailsAssociate()
        {
            List<DashBoard> dataList2 = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.BindGraphDetailsAssociate();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList2.Add(details);

                    count++;
                }
            }
            return Json(dataList2, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetAssociateJoiningDetails()
        {
            List<DashBoard> dataList3 = new List<DashBoard>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            DashBoard newdata = new DashBoard();

            Ds = newdata.GetAssociateJoining();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[0].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    DashBoard details = new DashBoard();


                    details.TotalUser = (dr["TotalUser"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList3.Add(details);

                    count++;
                }
            }
            return Json(dataList3, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeDashBoard()
        {
            return View();
        }
   
    }
}
