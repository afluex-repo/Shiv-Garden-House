using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShivGardenHouse.Models
{
    public class Reports : Common
    {
        public string RewardID { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionNo { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string Status { get; set; }
        public List<Reports> lsttopupreport { get; set; }
        public List<Reports> lstassociate { get; set; }
        public string QualifyDate { get; set; }
        public string RewardImage { get; set; }
        public string Contact { get; set; }
        public string PK_RewardItemId { get; set; }
        public string RewardName { get; set; }
        public string PK_BookingId { get; set; }
        public string BookingNumber { get; set; }
        public List<Reports> lstP { get; set; }
        public string PlotAmount { get; set; }
        public string ActualPlotRate { get; set; }
        public string PlotRate { get; set; }
        public string PayAmount { get; set; }
        public string BookingDate { get; set; }
        public string BookingAmount { get; set; }
        
       
        public string Discount { get; set; }
        public string PaymentPlanID { get; set; }
        public string PlanName { get; set; }
        public string TotalAllotmentAmount { get; set; }
        public string PaidAllotmentAmount { get; set; }
        public string BalanceAllotmentAmount { get; set; }
        public string TotalInstallment { get; set; }
     
        public string Balance { get; set; }
        public string PlotArea { get; set; }
        public string PK_BookingDetailsId { get; set; }
        public string InstallmentNo { get; set; }
        public string InstallmentDate { get; set; }
        public string PaymentDate { get; set; }
        public string PaidAmount { get; set; }
        public string InstallmentAmount { get; set; }
        public string PaymentMode { get; set; }
        public string DueAmount { get; set; }


        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId )};
            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }

        public DataSet RewardList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_RewardId", RewardID),
                                        new SqlParameter("@FK_UserId", Fk_UserId)};
            DataSet ds = Connection.ExecuteQuery("_GetRewardData", para);
            return ds;
        }
        public DataSet RewardListForAchiever()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId) }
                                    ;
            DataSet ds = Connection.ExecuteQuery("GetRewardAchieverList", para);
            return ds;
        }
        public DataSet ProductRewardList()
        {

            DataSet ds = Connection.ExecuteQuery("RewardList");
            return ds;
        }
        public DataSet ApprovePayment()
        {
            SqlParameter[] para = { new SqlParameter("@PKID", PK_RewardItemId),
                                    new SqlParameter("@TxnNo", TransactionNo),
                                    new SqlParameter("@TxnDate", TransactionDate),
                                    new SqlParameter("@PaidDate", PaymentDate),
                                    new SqlParameter("@AddedBy", AddedBy),


            }
                                    ;
            DataSet ds = Connection.ExecuteQuery("ApproveReward", para);
            return ds;
        }



    }
}


