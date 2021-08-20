using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ShivGardenHouse.Models
{
    public class TraditionalAssociate : Common
    {
        #region Properties
        public string FatherName { get; set; }
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }

        public string Password { get; set; }
        public string JoiningFromDate { get; set; }
        public string JoiningToDate { get; set; }
        public string isBlocked { get; set; }
        public string  AssociateID { get; set; }
        public string AssociateName { get; set; }
        public string UserID { get; set; }
        public string LoginID { get; set; }
        public string BranchID { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
        public string SponsorDesignationID { get; set; }
        public string DesignationID { get; set; }
        public string RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PanNo { get; set; }
        public string PanImage { get; set; }
        public string BranchName { get; set; }
        public string Pin { get; set; }
        public string DesignationName { get; set; }
        public string PK_DesignationID { get; set; }
        public string AdharNo { get; set; }
        public List<TraditionalAssociate> lstTrad { get; set; }
        public List<SelectListItem> ddlDesignation { get; set; }
        #endregion

        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }

        #region AssociateRegistrationTraditional

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
            DataSet ds = Connection.ExecuteQuery("GetDesignationList",para);

            return ds;
        }
        public DataSet GetAdharDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@AdharNumber", AdharNo)
            };
            DataSet ds = Connection.ExecuteQuery("GetAdharDetails", para);
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
                                  };
            DataSet ds = Connection.ExecuteQuery("AssociateRegistrationTraditional", para);
            return ds;
        }
        
        public DataSet GetList()
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
        
        public DataSet UpdateAssociate()
        {
            SqlParameter[] para = {
                                      
                                  new SqlParameter("@PK_UserId", UserID) ,
                                  new SqlParameter("@BranchID", BranchID) ,
                               
                                  new SqlParameter("@FirstName", FirstName) ,
                                  new SqlParameter("@LastName", LastName) ,
                                  new SqlParameter("@Mobile", Contact) ,
                                  new SqlParameter("@Email", Email) ,
                                  new SqlParameter("@PinCode", Pincode) ,
                                  new SqlParameter("@State", State) ,
                                  new SqlParameter("@City", City) ,
                                  new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@PanNumber", PanNo) ,
                                  new SqlParameter("@UpdatedBy", AddedBy) ,
                                      new SqlParameter("@FatherName", FatherName) ,
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateAssociateRegistrationDetails", para);
            return ds;
        }
        
        public DataSet DeleteAssociate()
        {
            SqlParameter[] para = { 
                                       new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteAssociate", para);
            return ds;
        }
        
        #endregion

        #region AssociateUpline
        
        public DataSet GetDetails()
        {
            SqlParameter[] para = { 
                                        new SqlParameter("@LoginId", AssociateID) };
            DataSet ds = Connection.ExecuteQuery("GetUplineAssociateDetails", para);
            return ds;
        }
        
        #endregion
        
        #region Associatedownline

        public DataSet GetDownlineDetails()
        {
            SqlParameter[] para = { 
                                       
                                      new SqlParameter("@LoginId", AssociateID) };
            DataSet ds = Connection.ExecuteQuery("GetDownlineAssociateDetails", para);
            return ds;
        }
        public string Percentage { get; set; }
        #endregion

        public DataSet BlockUser()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID),
                                      new SqlParameter("@FK_UserID", Fk_UserId),
                                   new SqlParameter("@BlockedBy", UpdatedBy)};

            DataSet ds = Connection.ExecuteQuery("BlockAssociate", para);
            return ds;
        }

        public DataSet UnblockUser()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID),
                                      new SqlParameter("@FK_UserID", Fk_UserId),
                                   new SqlParameter("@BlockedBy", UpdatedBy)};
            DataSet ds = Connection.ExecuteQuery("UnblockAssociate", para);
            return ds;
        }



        public string EncryptKey { get; set; }

        public string Body { get; set; }

        public string Char { get; set; }
        public string Sms { get; set; }
    }
}