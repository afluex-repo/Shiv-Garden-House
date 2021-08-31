using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShivGardenHouse.Models
{
    public class AssociateBooking : Common
    {
        #region Properties
        public string FromTeam { get; set; }
        public string ProjectName { get; set; }
        public string AmountType { get; set; }
        public string DepositAmount { get; set; }
        public string IncomeType { get; set; }

        public string SponsorDesignationID { get; set; }
        public List<SelectListItem> ddlDesignation { get; set; }
        public List<AssociateBooking> ClosingWisePayoutlist { get; set; }
        public string CommPercentage { get; set; }
        public string PK_PaidPayoutId { get; set; }
        public string NetAmount { get; set; }
        public string TDS { get; set; }
        public string Processing { get; set; }
        public string GrossAmount { get; set; }
        public string ClosingDate { get; set; }
        public string PayOutNo { get; set; }
        public decimal Income { get; set; }
        public string ToName { get; set; }
        public string ToID { get; set; }
        public string Fk_SponsorId { get; set; }
        public string ActiveStatus { get; set; }
        public string ProfilePic { get; set; }
        public string PK_BookingId { get; set; }
        public string UserID { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string PlotID { get; set; }
        public string PlotNumber { get; set; }
        public string CustomerID { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string AssociateID { get; set; }
        public string PK_DesignationID { get; set; }
        public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotAmount { get; set; }
        public string PlotRate { get; set; }
        public string PaymentPlanID { get; set; }
        public string BookingAmount { get; set; }
        public string PayAmount { get; set; }
        public string Discount { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string Remark { get; set; }
        public string TotalPLC { get; set; }
        public string LoginId { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public List<SelectListItem> ddlSite { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public string BookingDate { get; set; }
        public string ActualPlotRate { get; set; }
        public string DevelopmentCharge { get; set; }
        public List<AssociateBooking> lstPlot { get; set; }
        public string BookingStatus { get; set; }

        public string AdharNo { get; set; }
        public string BankHolderName { get; set; }
        public string MemberAccNo { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get; set; }
        public string IFSCCode { get; set; }
        public string Nominee { get; set; }
        public string NomineeAge { get; set; }
        public string NomineeRelation { get; set; }
        #endregion
        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }
        public DataSet GetPayoutWiseIncomeDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_PaidPayoutId", PK_PaidPayoutId)

                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutWiseIncomeDetails", para);
            return ds;
        }
        public DataSet List()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                     new SqlParameter("@AssociateID", LoginId)   ,

                                     new SqlParameter("@CustomerLoginID", CustomerLoginID)   ,
                                    new SqlParameter("@CustomerName", CustomerName)   ,
                                    new SqlParameter("@PK_SiteID", SiteID)   ,
                                    new SqlParameter("@PK_SectorID", SectorID)   ,
                                    new SqlParameter("@PK_BlockID", BlockID)   ,
                                    new SqlParameter("@PlotNumber", PlotNumber)   ,
                                    new SqlParameter("@BookingNumber", BookingNumber)   ,
                                    new SqlParameter("@FromDate", FromDate)   ,
                                    new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForAssociate", para);
            return ds;
        }

        public DataSet SaveDistributedPayment()
        {
            SqlParameter[] para = { new SqlParameter("@ClosingDate", ToDate),


                                      };
            DataSet ds = Connection.ExecuteQuery("_AutoDistributePayment", para);
            return ds;
        }
        public DataSet PayoutDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_Userid", UserID),
                  new SqlParameter("@PayoutNo", PayOutNo),
                    new SqlParameter("@FromDate", FromDate),
                     new SqlParameter("@ToDate", ToDate),
                      new SqlParameter("@LoginId", LoginId),

                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutDetails", para);
            return ds;
        }

        public DataSet GetDownlineDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetDownlineAssociateDetails", para);
            return ds;
        }

        public DataSet GetDetailsForDistributePayment()
        {

            DataSet ds = Connection.ExecuteQuery("GetDetailsForDistributePayment");
            return ds;
        }



        public string DesignationName { get; set; }

        public string Percentage { get; set; }

        public string Contact { get; set; }

        public string BookingNumber { get; set; }

        public string PaidAmount { get; set; }

        public string PlanName { get; set; }

        public string TotalAllotmentAmount { get; set; }

        public string PaidAllotmentAmount { get; set; }

        public string BalanceAllotmentAmount { get; set; }

        public string TotalInstallment { get; set; }

        public string InstallmentAmount { get; set; }

        public string PlotArea { get; set; }

        public string Balance { get; set; }

        public string PK_BookingDetailsId { get; set; }

        public string InstallmentNo { get; set; }

        public string InstallmentDate { get; set; }

        public string DueAmount { get; set; }
        public DataSet FillDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),
                                  new SqlParameter("@LoginId",LoginId),

                                   new SqlParameter("@FK_SiteID",SiteID),
                                    new SqlParameter("@FK_SectorID",SectorID),
                                     new SqlParameter("@FK_BlockID",BlockID),
                                      new SqlParameter("@PlotNumber",PlotNumber)


                            };
            DataSet ds = Connection.ExecuteQuery("GetLedgerDetailsForAssociate", para);
            return ds;
        }
        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                        new SqlParameter("@CustomerID", CustomerID),
                                          new SqlParameter("@AssociateID", AssociateID)

                                  };

            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }

        public DataSet GetDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetUplineAssociateDetails", para);
            return ds;
        }
        public DataSet UpdatePassword()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@OldPassword", Password) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("ChangePasswordAssociate", para);
            return ds;
        }

        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public string Total { get; set; }

        public string Status { get; set; }

        public string TotalBooking { get; set; }

        public string Month { get; set; }
        public DataSet GetDetailsForBarGraph()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_AssociateId", AssociateID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForGraphOnAssociateDashboard", para);
            return ds;
        }
        public List<AssociateBooking> dataList3 { get; set; }
        public List<AssociateBooking> ListInstallment { get; set; }

        public DataSet GetDueInstallmentList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_AssociateId", AssociateID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDueInstallmentForAssociateDashboard", para);
            return ds;
        }
        public DataSet BindGraphDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("PlotDetailsOnGraphForAssociateDashboard", para);
            return ds;
        }


        public string SponsorID { get; set; }

        public string SponsorName { get; set; }

        public string DesignationID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PanNo { get; set; }

        public string Address { get; set; }
        public string FatherName { get; set; }


        #region EditProfile


        public DataSet GetList()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@AssociateId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDetailsForEditProfile", para);
            return ds;
        }



        public DataSet UpdatePersonalDetails()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@Email", Email) ,
                                       new SqlParameter("@PinCode", Pincode) ,
                                        new SqlParameter("@State", State) ,
                                         new SqlParameter("@City", City) ,
                                          new SqlParameter("@Address", Address) ,
                                           new SqlParameter("@PanNumber", PanNo) ,
                                            new SqlParameter("@UpdatedBy", UpdatedBy) ,
                                            new SqlParameter("@ProfilePic", ProfilePic)
                                  };
            DataSet ds = Connection.ExecuteQuery("EditAssociateDetailsForProfile", para);
            return ds;
        }



        #endregion


        public List<AssociateBooking> ListNEWS { get; set; }
        public DataSet GetNews()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_NewsID", PK_NewsID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetNewsList", para);
            return ds;
        }


        public string PK_NewsID { get; set; }
        public string NewsHeading { get; set; }
        public string NewsBody { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string JoiningFromDate { get; set; }
        public string PanImage { get; set; }
        public string JoiningToDate { get; set; }
        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
            return ds;
        }

        public DataSet AssociateRegistration()
        {
            SqlParameter[] para = { new SqlParameter("@BranchID", BranchID) ,
                                  new SqlParameter("@SponsorID", UserID) ,
                                  new SqlParameter("@DesignationID", DesignationID) ,
                                  new SqlParameter("@RoleID", 2) ,
                                  new SqlParameter("@FirstName", FirstName) ,
                                  new SqlParameter("@LastName", LastName) ,
                                  new SqlParameter("@Contact", Contact) ,
                                  new SqlParameter("@Email", Email) ,
                                  new SqlParameter("@Pincode", Pincode) ,
                                  new SqlParameter("@State", State) ,
                                  new SqlParameter("@City", City) ,
                                  new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@PanNo", PanNo) ,
                                  new SqlParameter("@AdharNo",AdharNo) ,
                                  new SqlParameter("@PanImage", PanImage) ,
                                  new SqlParameter("@AddedBy", AddedBy) ,
                                  new SqlParameter("@Password", Password) ,
                                    new SqlParameter("@FatherName", FatherName) ,
                                      new SqlParameter("@BankHolderName", BankHolderName) ,
                                      new SqlParameter("@MemberAccNo",MemberAccNo) ,
                                       new SqlParameter("@MemberBankName", MemberBankName) ,
                                        new SqlParameter("@MemberBranch", MemberBranch) ,
                                         new SqlParameter("@IFSCCode", IFSCCode) ,
                                          new SqlParameter("@Nominee",Nominee),
                                           new SqlParameter("@NomineeAge",NomineeAge),
                                            new SqlParameter("@NomineeRelation",NomineeRelation)

                                  };
            DataSet ds = Connection.ExecuteQuery("AssociateRegistrationTraditional", para);
            return ds;
        }
        public DataSet GetBranchList()
        {
            DataSet ds = Connection.ExecuteQuery("GetBranchList");
            return ds;
        }
        public DataSet GetDesignationList()
        {

            SqlParameter[] para = {
                        new SqlParameter("@PK_DesignationID", PK_DesignationID),
                                      new SqlParameter("@Percentage", Percentage)

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList", para);

            return ds;
        }
        public DataSet GetListforAssociate()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", UserID),
                                  new SqlParameter("@AssociateLoginID", AssociateID),
                                  new SqlParameter("@AssociateName", AssociateName),
                                  new SqlParameter("@SponsorLoginID", SponsorID),
                                  new SqlParameter("@SponsorName", SponsorName),
                                  new SqlParameter("@FromDate", JoiningFromDate),
                                  new SqlParameter("@ToDate", JoiningToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("SelectAssociate", para);
            return ds;
        }

        public DataSet GetDownlineTree()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_UserId", Fk_UserId) };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDownlineTree", para);
            return ds;
        }



        public DataSet TransactionLogReportBy()
        {
            SqlParameter[] para = { new SqlParameter("@ActionName", DisplayName),
                 new SqlParameter("@FromDate", FromDate),
                  new SqlParameter("@ToDate", ToDate),


                                      };
            DataSet ds = Connection.ExecuteQuery("TransactionReport", para);
            return ds;
        }




    }

}



