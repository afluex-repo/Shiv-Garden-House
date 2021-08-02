using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ShivGardenHouse.Models
{
    public class SMS:Common
    {
        public string TemplateName { get; set; }
        public string PK_TemplateId { get; set; }
        public string Msg { get; set; }
        public List<SMS> ListSMSTemplate { get; set; }
        public string MessageCount { get; set; }
        public string TotalSMS { get; set; }
        public string PK_SendSMSId { get; set; }
        public string AssocCustom { get; set; }
        public List<SMS> lstsmsdata { get; set; }
        public string FirstName { get; set; }
        public string LoginId { get; set; }
        public string PK_UserId { get; set; }
        public string PK_RoleId { get; set; }
        public string Mobile { get; set; }
        public DataTable dtSMS { get; set; }
        public string TemplateNameText { get; set; }

        #region SMSTemplate
        public DataSet SavingSMSTemplate()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@TemplateName",TemplateName),
                new SqlParameter("@Msg",Msg)
            };
            DataSet ds = Connection.ExecuteQuery("SMSTemplate", para);
            return ds;
        }

        public DataSet GettingSMSTemplate()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_TemplateId",PK_TemplateId)
            };
            DataSet ds = Connection.ExecuteQuery("SMSTemplateList", para);
            return ds;
        }

        public DataSet UpdatingSMSTemplate()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@UpdatedBy",UpdatedBy),
                new SqlParameter("@TemplateName",TemplateName),
                new SqlParameter("@Msg",Msg),
                new SqlParameter("@PK_TemplateId",PK_TemplateId)
            };
            DataSet ds = Connection.ExecuteQuery("UpdateSMSTemplate", para);
            return ds;
        }

        public DataSet DeletingSMSTemplate()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@DeletedBy",AddedBy),
                new SqlParameter("@PK_TemplateId",PK_TemplateId)
            };
            DataSet ds = Connection.ExecuteQuery("DeleteSMSTemplate", para);
            return ds;
        }


        #endregion

        #region SendSMS

        public DataSet GetSMSData()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@AssocCustom",AssocCustom)
            };
            DataSet ds = Connection.ExecuteQuery("SMSData", para);
            return ds;
        }

        public DataSet SaveSMSData()
        {
            SqlParameter[] para ={
                                   new SqlParameter("@dtSMS",dtSMS),
                                   new SqlParameter("@Message", Msg),
                                   new SqlParameter("@SMSCount",TotalSMS),
                                   new SqlParameter("@AddedBy",AddedBy),
                                   new SqlParameter("@MessageTemplate",TemplateNameText),
                               };
            DataSet ds = Connection.ExecuteQuery("SaveSMSReport", para);
            return ds;
        }

        #endregion
    }
}