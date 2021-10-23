using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AsterHeight.Models
{
    public class Customer : Common
    {
        #region Properties
        public string AssociateID { get; set; }
        public string JoiningFromDate { get; set; }
        public string JoiningToDate { get; set; }
        public bool IsDownline { get; set; }
        public string AssociateName { get; set; }
        public string isBlocked { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string FK_SponsorId { get; set; }

        public string LoginID { get; set; }
        public string Password { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
        public string BranchID { get; set; }
        public string Address { get; set; }
        public string PanNo { get; set; }
        public string Pin { get; set; }
        public string BranchName { get; set; }
        public List<Customer> ListCust { get; set; }
        public string Amount { get; set; }
        public string PaymentMode { get; set; }
        public string Description { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string RegistrationType { get; set; }
        public string CouponNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LuckyDrawId { get; set; }
        public string status { get; set; }
        public string Leg { get; set; }
        public string BookingNo { get; set; }
        public string Action { get; set; }
        public HttpPostedFileBase profilePhoto { get; set; }
        public string Photo { get; set; }
        public string CustomerId { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string AccountHolderName { get; set; }
        public string AdjustmentId { get; set; }
        public decimal CouponAmount { get; set; }
        public string PK_TopUpId { get; set; }
        #endregion

        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("AssociateListTraditional", para);
            return ds;
        }

        #region CustomerRegistration

        public DataSet CustomerRegistration()
        {
            SqlParameter[] para = { new SqlParameter("@BranchID", BranchID) ,
                                  new SqlParameter("@SponsorID", UserID) ,
                                  new SqlParameter("@RoleID", 3) ,
                                  new SqlParameter("@FirstName", FirstName) ,
                                  new SqlParameter("@LastName", LastName) ,
                                  new SqlParameter("@Contact", Contact) ,
                                  new SqlParameter("@Email", Email) ,
                                  new SqlParameter("@Pincode", Pincode) ,
                                  new SqlParameter("@State", State) ,
                                  new SqlParameter("@City", City) ,
                                  new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@PanNo", PanNo) ,
                                  new SqlParameter("@AddedBy", AddedBy) ,
                                  new SqlParameter("@Password", Password) ,
                                  new SqlParameter("@RegistrationType",RegistrationType),
                                  new SqlParameter("@Amount",Amount),
                                  new SqlParameter("@CouponNo",CouponNo),
                                  new SqlParameter("@PaymentMode",PaymentMode),
                                  new SqlParameter("@TransactionNo",TransactionNo),
                                  new SqlParameter("@BookingNo",BookingNo),
                                  new SqlParameter("@TransactionDate",TransactionDate),
                                  new SqlParameter("@BankName",BankName),
                                  new SqlParameter("@BankBranch",BankBranch),
                                  new SqlParameter("@Leg",Leg),
                                  new SqlParameter( "@Action", Action),
                                  new SqlParameter( "@Photo", Photo),
                                   new SqlParameter( "@AdjustmentId", AssociateID),
                                  };
            DataSet ds = Connection.ExecuteQuery("CustomerRegistration", para);
            return ds;
        }

        public DataSet CustomerRegistrationByAssociate()
        {
            SqlParameter[] para = { new SqlParameter("@BranchID", BranchID) ,
                                  new SqlParameter("@SponsorID", UserID) ,
                                  new SqlParameter("@FirstName", FirstName) ,
                                  new SqlParameter("@LastName", LastName) ,
                                  new SqlParameter("@Contact", Contact) ,
                                  new SqlParameter("@Email", Email) ,
                                  new SqlParameter("@Pincode", Pincode) ,
                                  new SqlParameter("@State", State) ,
                                  new SqlParameter("@City", City) ,
                                  new SqlParameter("@Address", Address) ,
                                  new SqlParameter("@PanNo", PanNo) ,
                                  new SqlParameter("@AddedBy", AddedBy) ,
                                  new SqlParameter("@Password", Password) ,
                                  new SqlParameter("@RegistrationType",RegistrationType),
                                  new SqlParameter("@Amount",Amount),
                                  new SqlParameter("@CouponNo",CouponNo),
                                  new SqlParameter("@PaymentMode",PaymentMode),
                                  new SqlParameter("@TransactionNo",TransactionNo),
                                  new SqlParameter("@TransactionDate",TransactionDate),
                                  new SqlParameter("@BankName",BankName),
                                  new SqlParameter("@BankBranch",BankBranch),
                                  new SqlParameter("@Leg",Leg),
                                  new SqlParameter("@Photo",Photo),
                                  new SqlParameter("@AdjustmentId",AssociateID)
                                  };
            DataSet ds = Connection.ExecuteQuery("CustomerRegistrationByAssociate", para);
            return ds;
        }
        public DataSet GetList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", UserID),
                                  new SqlParameter("@CustomerLoginID", CustomerLoginID),
                                  new SqlParameter("@CustomerName", CustomerName),
                                  new SqlParameter("@AssociateLoginID", AssociateLoginID),
                                  new SqlParameter("@AssociateName", AssociateName),
                                  new SqlParameter("@FromDate", JoiningFromDate),
                                  new SqlParameter("@ToDate", JoiningToDate)

                                  };
            DataSet ds = Connection.ExecuteQuery("SelectCustomer", para);
            return ds;
        }
        public DataSet UpdateCustomer()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", UserID) ,
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
                                  new SqlParameter("@UpdatedBy", AddedBy),
                                    new SqlParameter("@BankName", BankName) ,
                                  new SqlParameter("@BankBranch", BankBranch) ,
                                  new SqlParameter("@AccountNo", AccountNo) ,
                                  new SqlParameter("@IFSCCode", IFSCCode) ,
                                   new SqlParameter("@ProfilePic", Photo) ,
                                   new SqlParameter("@AccountHolderName", AccountHolderName) ,
                                  };
            DataSet ds = Connection.ExecuteQuery("UpdateCustomerRegistrationDetails", para);
            return ds;
        }

        public DataSet DeleteCustomer()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@PK_UserId", UserID) ,
                                      new SqlParameter("@DeletedBy", AddedBy) };
            DataSet ds = Connection.ExecuteQuery("DeleteCustomerRegistration", para);
            return ds;
        }
        public DataSet GetSponsorName()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("GetSponsorForCustomerRegistraton", para);
            return ds;
        }

        public DataSet GetCustomerDetail()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginID) };
            DataSet ds = Connection.ExecuteQuery("GetCustomerDetail", para);
            return ds;
        }
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



        public string JoiningDate { get; set; }

        public string EncryptKey { get; set; }



        public DataSet GetLuckyDrawCustomerList()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginID", CustomerLoginID),
                                      new SqlParameter("@Name", CustomerName),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                        new SqlParameter("@PaymentMode", PaymentMode),
                                         new SqlParameter("@UserID", UserID),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetLuckyDrawCustomerList", para);
            return ds;
        }
        public DataSet GetLuckyDrawRequestList()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginID", CustomerLoginID),
                                      new SqlParameter("@Name", CustomerName),
                                      new SqlParameter("@AssociateLoginID",AssociateLoginID),
                                      new SqlParameter("@AssociateName",AssociateName),
                                      new SqlParameter("@status",status),
                                      new SqlParameter("@Leg",Leg),
                                      new SqlParameter("@IsDownline",IsDownline),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                        new SqlParameter("@PaymentMode", PaymentMode),
                                        new SqlParameter("@UserId",UserID)
                                  };

            DataSet ds = Connection.ExecuteQuery("GetLuckyDrawRequestList", para);
            return ds;
        }
        //public  DataSet GetLuckyDrawRequestList()
        //{
        //    SqlParameter[] para = {   new SqlParameter("@LoginID", CustomerLoginID),
        //                              new SqlParameter("@Name", CustomerName),
        //                              new SqlParameter("@AssociateLoginID",AssociateLoginID),
        //                              new SqlParameter("@AssociateName",AssociateName),
        //                              new SqlParameter("@FromDate", FromDate),
        //                              new SqlParameter("@ToDate", ToDate),
        //                                new SqlParameter("@PaymentMode", PaymentMode)
        //                          };

        //    DataSet ds = Connection.ExecuteQuery("GetLuckyDrawRequestList", para);
        //    return ds;
        //}

        public DataSet SaveTopUp()
        {
            SqlParameter[] para = { new SqlParameter("@FK_AssociateId",UserID),
                                      new SqlParameter("@Amount", CouponAmount),
                                   new SqlParameter("@Remarks", Description),
                                   new SqlParameter("@PaymentMode", PaymentMode),
                                      new SqlParameter("@TransactionNo", TransactionNo),
                                   new SqlParameter("@TransactionDate", TransactionDate),
                                   new SqlParameter("@BankName", BankName),
                                      new SqlParameter("@BranchName", BankBranch),
                                   new SqlParameter("@AddedBy", AddedBy),
                                   new SqlParameter("@AdjustmentId", AdjustmentId),
            };

            DataSet ds = Connection.ExecuteQuery("SaveTopUp", para);
            return ds;
        }
        public DataSet GetTopupReport()
        {
            SqlParameter[] para = {
                new SqlParameter("@FK_AssociateId", AssociateID),
                new SqlParameter("@AssociateID", AssociateLoginID),
                                      new SqlParameter("@AssociateName", AssociateName),
                                   new SqlParameter("@FromDate", FromDate),
                                   new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = Connection.ExecuteQuery("GetDetailsForTopup", para);
            return ds;
        }
    }
}