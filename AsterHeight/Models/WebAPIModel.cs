using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AsterHeight.Models
{
    public class WebAPIModel
    {

    }
    public class LoginAPI
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Pk_userId { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string ProfilePic { get; set; }
        public string StatusColor { get; set; }

        public DataSet LoginAction()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
            new SqlParameter("@Password",Password)
            };
            DataSet ds = Connection.ExecuteQuery("LoginAsAssociateForMobile", para);
            return ds;
        }
    }




    public class AssociateSignup
    {
        public string BranchID { get; set; }
        public string UserID { get; set; }
        public string DesignationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PanNo { get; set; }
        public string PanImage { get; set; }
        public string Password { get; set; }
        public string AdharNumber { get; set; }
        public string BankAccountNo { get; set; }
        public string AddedBy { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSCCode { get; set; }
        public string Signature { get; set; }
        public string ProfilePic { get; set; }
        public string Gender { get; set; }
        public string Leg { get; set; }
        public string Amount { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNo { get;  set; }
        public string TransactionDate { get;  set; }
        public string AdjustmentId { get;  set; }
        public string Bank_Name { get; set; }
        public string Bank_Branch { get; set; }
    }
    public class ResponseRegistration
    {
        public string Status { get; set; }
        public string SuccessMessage { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Pk_userId { get; set; }
        public string ErrorMessage { get; set; }
        public DataSet AssociateRegistration(AssociateSignup reg)
        {
            SqlParameter[] para = {
                                  new SqlParameter("@SponsorID", reg.UserID) ,
                                  new SqlParameter("@DesignationID", reg.DesignationID) ,
                                  new SqlParameter("@RoleID", 2) ,
                                  new SqlParameter("@FirstName", reg.FirstName) ,
                                  new SqlParameter("@LastName", reg.LastName) ,
                                  new SqlParameter("@Contact", reg.Contact) ,
                                  new SqlParameter("@Email", reg.Email) ,
                                  new SqlParameter("@Gender", reg.Gender) ,
                                  new SqlParameter("@Pincode", reg.PinCode) ,
                                  new SqlParameter("@State", reg.State) ,
                                  new SqlParameter("@City", reg.City) ,
                                  new SqlParameter("@Address", reg.Address) ,
                                  new SqlParameter("@PanNo", reg.PanNo) ,
                                  new SqlParameter("@PanImage", reg.PanImage) ,
                                  new SqlParameter("@AddedBy", reg.AddedBy) ,
                                  new SqlParameter("@Password", reg.Password) ,
                                  new SqlParameter("@AdharNumber", reg.AdharNumber) ,
                                  new SqlParameter("@BankAccountNo", reg.BankAccountNo) ,
                                  new SqlParameter("@BankName", reg.BankName) ,
                                  new SqlParameter("@BankBranch", reg.BankBranch) ,
                                  new SqlParameter("@IFSCCode", reg.IFSCCode),
                                  new SqlParameter("@ProfilePic", reg.ProfilePic) ,
                                  new SqlParameter("@Signature", reg.Signature),
                                  new SqlParameter("@Leg", reg.Leg),
                                  new SqlParameter ("@Amount",reg.Amount),
                                      new SqlParameter("@PaymentMode",reg.PaymentMode),
                                         new SqlParameter("@TransactionNo",reg.TransactionNo),
                                           new SqlParameter("@TransactionDate",reg.TransactionDate),
                                             new SqlParameter("@Bank_Name",reg.Bank_Name),
                                               new SqlParameter("@Bank_Branch",reg.Bank_Branch),
                                               new SqlParameter("@AdjustmentId",reg.AdjustmentId),

                                  };
            DataSet ds = Connection.ExecuteQuery("AssociateRegistrationTraditional", para);
            return ds;
        }

    }
    public class ChangePassword
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string Pk_userId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public DataSet ChangePasswordAssociate()
        {
            SqlParameter[] para ={new SqlParameter("@OldPassword",OldPassword),
                                new SqlParameter("@NewPassword",NewPassword),
                                new SqlParameter("@UpdatedBy",Pk_userId),
            };
            DataSet ds = Connection.ExecuteQuery("ChangePasswordAssociate", para);
            return ds;
        }
    }
    public class ForgetPass
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string LoginID { get; set; }
        public string Contact { get; set; }

        public DataSet ForgetPassword()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginID),
                                  new SqlParameter("@Mobile", Contact)};
            DataSet ds = Connection.ExecuteQuery("ForgotPassword", para);
            return ds;
        }
    }

    public class AssociateDashboard
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string AssociateID { get; set; }
        public List<DueInstallment> lstdueinstallment { get; set; }
        public List<NewsDetails> lstnewsdetail { get; set; }
        public List<Associate> lstassociate { get; set; }
        public string PaidBusinessLeft { get; set; }
        public string PaidBusinessRight { get; set; }
        public string TotalBusinessLeft { get; set; }
        public string TotalBusinessRight { get; set; }
        public string CarryLeft { get; set; }
        public string CarryRight { get; set; }

        public DataSet GetAssociateForDashboard()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_UserId", AssociateID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDashboardForMobile", para);
            return ds;
        }

    }
    public class NewsDetails
    {
        public string PK_NewsID { get; set; }
        public string NewsHeading { get; set; }
        public string NewsBody { get; set; }
    }
    public class DueInstallment
    {
        public string CustomerID { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string PlotNumber { get; set; }
        public string InstallmentNo { get; set; }
        public string InstallmentAmount { get; set; }
    }
    public class Associate
    {
        public string TotalActiveId { get; set; }
        public string TotalLuckyDraw { get; set; }
        public string TotalLuckyDrawRequest { get; set; }
        public string TotalDirect { get; internal set; }
        public string TotalDownline { get; internal set; }
        public string PayoutWalletBalance { get; internal set; }
        public string TotalPayout { get; internal set; }
        public string TotalPaidPayout { get; internal set; }
        public string TotalDeduction { get; internal set; }
        public string TotalInActive { get; internal set; }
        public string LeftApprovedBusiness { get; internal set; }
        public string RightApprovedBusiness { get; internal set; }
    }
    public class Graph
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string LoginId { get; set; }
        public List<GraphData> lstdata { get; set; }
        public DataSet BindGraphDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("PlotDetailsOnGraphForAssociateDashboard", para);
            return ds;
        }
    }
    public class GraphData
    {
        public string Title { get; set; }
        public List<GraphDetails> lstgraphdetails { get; set; }
    }
    public class GraphDetails
    {
        public string Total { get; set; }
        public string Status { get; set; }
    }
    public class GetViaPin
    {
        public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Result { get; set; }
        public DataSet GetStateCity()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", Pincode) };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;
        }
    }
    public class Site
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public List<SiteList> lstsite { get; set; }
        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteListMobile");
            return ds;
        }
    }
    public class SiteList
    {
        public string PK_SiteID { get; set; }
        public string SiteName { get; set; }
    }
    public class SitePLCCharge
    {
        public string SiteID { get; set; }
        public string Rate { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
        public List<PLCChargeList> lstPlcCharge { get; set; }
        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteListMobile");
            return ds;
        }
        public DataSet GetPLCChargeList()
        {
            SqlParameter[] para = { new SqlParameter("@FK_SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetPlotPLCCharge", para);
            return ds;

        }
    }
    public class SiteType
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public List<SiteTypeList> lstsitetype { get; set; }
        public DataSet GetSiteTypeList()
        {

            DataSet ds = Connection.ExecuteQuery("SiteTypeListMobile");
            return ds;
        }
    }
    public class SiteTypeList
    {
        public string PK_SiteTypeID { get; set; }
        public string SiteTypeName { get; set; }
    }
    public class PLCChargeList
    {
        public string SiteName { get; set; }
        public string PLCName { get; set; }
        public string PLCCharge { get; set; }
        public string PLCID { get; set; }
    }
    public class BookingListDetails
    {
        public string SiteName { get; set; }
        public string SectorName { get; set; }
        public string Discount { get; set; }
        public string BlockName { get; set; }
        public string BranchName { get; set; }
        public string CustomerID { get; set; }
        public string AssociateID { get; set; }
        public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
        public string BookingStatus { get; set; }
        public string PK_BookingId { get; set; }

        public string BookingDate { get; set; }
        public string BookingAmount { get; set; }
        public string PaymentPlanID { get; set; }
        public string BookingNumber { get; set; }
        public string PaidAmount { get; set; }
        public string PlotArea { get; set; }
        public string PlotAmount { get; set; }
        public string NetPlotAmount { get; set; }
        public string PK_PLCCharge { get; set; }
        public string PlotRate { get; set; }
        public string PlotNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLoginID { get; set; }
        public string BranchID { get; set; }
    }
    public class BookingList
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string SiteID { get; set; }
        public string BookingNumber { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BranchID { get; set; }
        public string PlotNumber { get; set; }
        public string PK_BookingId { get; set; }
        public string LoginId { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public List<BookingListDetails> lstbooking { get; set; }
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
        public DataSet BookingListForCustomer()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PK_BookingId", PK_BookingId),
                                     new SqlParameter("@CustomerID", CustomerId)   ,
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
            DataSet ds = Connection.ExecuteQuery("GetPlotBookingForCustomer", para);
            return ds;
        }
    }
    public class LedgerReport
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string BookingNumber { get; set; }
        public string LoginId { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PK_BookingId { get; set; }
        public string AssociateID { get; set; }
        public string CustomerID { get; set; }
        public string PlotNumber { get; set; }
        public string Result { get; set; }
        public string PlotAmount { get; set; }
        public string ActualPlotRate { get; set; }
        public string PlotRate { get; set; }
        public string PayAmount { get; set; }
        public string BookingDate { get; set; }
        public string BookingAmount { get; set; }
        public string PaymentDate { get; set; }
        public string PaidAmount { get; set; }
        public string Discount { get; set; }
        public string PaymentPlanID { get; set; }
        public string PlanName { get; set; }
        public string TotalAllotmentAmount { get; set; }
        public string PaidAllotmentAmount { get; set; }
        public string BalanceAllotmentAmount { get; set; }
        public string TotalInstallment { get; set; }
        public string InstallmentAmount { get; set; }
        public string PlotArea { get; set; }
        public string Balance { get; set; }
        public List<PaymentDetails> lstpayment { get; set; }
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
        public DataSet GetSiteList()
        {
            DataSet ds = Connection.ExecuteQuery("SiteList");
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
    }

    public class PaymentDetails
    {
        public string PK_BookingDetailsId { get; set; }
        public string PK_BookingId { get; set; }
        public string InstallmentNo { get; set; }
        public string InstallmentDate { get; set; }
        public string PaymentDate { get; set; }
        public string PaidAmount { get; set; }
        public string InstallmentAmount { get; set; }
        public string PaymentMode { get; set; }
        public string DueAmount { get; set; }
    }
    public class Downline
    {
        public string Status { get; set; }
        public string LoginId { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string Leg { get; set; }
        public List<DownlineDetails> lstdownline { get; set; }
        public DataSet GetDownlineList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                     new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status)};
            DataSet ds = Connection.ExecuteQuery("DownlineList", para);
            return ds;
        }
        public DataSet GetDetails()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId) };
            DataSet ds = Connection.ExecuteQuery("GetUplineAssociateDetails", para);
            return ds;
        }
    }
    public class DownlineDetails
    {
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string PlotNumber { get; set; }
        public string JoiningDate { get; set; }
        public string Leg { get; set; }
        public string PermanentDate { get; set; }
        public string Status { get; set; }
        public string Mobile { get; set; }
        public string Package { get; set; }
        public string AssociateID { get; set; }
        public string AssociateName { get; set; }
        public string DesignationName { get; set; }
        public string Percentage { get; set; }
        public string BranchName { get; set; }
        public string Contact { get; set; }
    }
    public class TreeList
    {
        public string LoginId { get; set; }
        public List<TreeDetails> lsttree { get; set; }

        public DataSet GetTreeDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@RootAgentCode", LoginId),
                                      new SqlParameter("@AgentCode", LoginId),
                                  };
            DataSet ds = Connection.ExecuteQuery("BrokerTree", para);
            return ds;
        }
    }
    public class TreeDetails
    {
        public string Fk_ParentId { get; set; }
        public string Fk_UserId { get; set; }
        public string DisplayName { get; set; }
    }
    public class DownlineTree
    {
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public List<DownlineTreeDetails> lstdownline { get; set; }
        public DataSet GetDownlineTree()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_UserId", Fk_UserId),
                                          new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = Connection.ExecuteQuery("GetAssociateDownlineTree", para);
            return ds;
        }
    }
    public class DownlineTreeDetails
    {
        public string Fk_UserId { get; set; }
        public string Fk_SponsorId { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string Status { get; set; }
        public string ActiveStatus { get; set; }
    }
    public class Reward
    {
        public string Status { get; set; }
        public string RewardID { get; set; }
        public string UserID { get; set; }
        public string ErrorMessage { get; set; }
        public List<RewardData> lstreward { get; set; }
        public DataSet RewardList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_RewardId", RewardID),
                                        new SqlParameter("@FK_UserId", UserID)};
            DataSet ds = Connection.ExecuteQuery("_GetRewardData", para);
            return ds;
        }
    }
    public class RewardData
    {
        public string Status { get; set; }
        public string QualifyDate { get; set; }
        public string RewardImage { get; set; }
        public string RewardName { get; set; }
        public string Contact { get; set; }
        public string PK_RewardItemId { get; set; }
    }
    public class RewardClaim
    {
        public string Fk_UserId { get; set; }
        public string Status { get; set; }
        public string PK_RewardItemId { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public DataSet ClaimReward()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_RewardItemId", PK_RewardItemId),
                                        new SqlParameter("@FK_UserId", Fk_UserId),
                                        new SqlParameter("@Status", Status),
            };
            DataSet ds = Connection.ExecuteQuery("ClaimReward", para);
            return ds;
        }
    }
    public class UnpaidIncome
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string FromID { get; set; }
        public string ToID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string UserID { get; set; }
        public List<UnpaidIncomeDetails> lstunpaid { get; set; }
        public DataSet UnpaidIncomes()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId", UserID),
                 new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate", ToDate),
                new SqlParameter("@FromLoginId", FromID),
                new SqlParameter("@ToLoginId", ToID),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetUnpaidIncomes", para);
            return ds;
        }
    }
    public class UnpaidIncomeDetails
    {
        public string FromID { get; set; }
        public string FromName { get; set; }
        public string ToID { get; set; }
        public string ToName { get; set; }
        public string Amount { get; set; }
        public string Income { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DifferencePercentage { get; set; }
    }
    public class PayoutBalance
    {
        public string Status { get; set; }
        public string UserID { get; set; }
        public string Balance { get; set; }
        public DataSet GetPayoutBalance()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserID),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }
    }
    public class PayoutRequest
    {
        public string Status { get; set; }
        public string UserID { get; set; }
        public string Amount { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public DataSet SavePayoutRequest()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", UserID),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@AddedBy", Amount),
                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutRequest", para);
            return ds;
        }
    }
    public class Payout
    {
        public string Status { get; set; }
        public string UserID { get; set; }
        public string PayOutNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LoginId { get; set; }
        public List<PayoutDetailData> lstpayout { get; set; }
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

    }
    public class PayoutDetailData
    {
        public string PayOutNo { get; set; }
        public string ClosingDate { get; set; }
        public string AssociateLoginID { get; set; }
        public string FirstName { get; set; }
        public string GrossAmount { get; set; }
        public string TDS { get; set; }
        public string Processing { get; set; }
        public string NetAmount { get; set; }
    }
    public class PayoutLedgerData
    {
        public string Status { get; set; }
        public string UserID { get; set; }
        public List<PayoutLedgerDetail> lstpayoutledger { get; set; }
        public DataSet PayoutLedger()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserID),
                                      };
            DataSet ds = Connection.ExecuteQuery("PayoutLedgerAssociate", para);
            return ds;
        }
    }
    public class PayoutLedgerDetail
    {
        public string Narration { get; set; }
        public string DrAmount { get; set; }
        public string CrAmount { get; set; }
        public string AddedOn { get; set; }
        public string PayoutBalance { get; set; }
    }
    public class PayoutRequestReports
    {
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public List<PayoutRequestReportData> lstpayout { get; set; }
        public DataSet PayoutRequestReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                     new SqlParameter("@Status", Status),
                                      };
            DataSet ds = Connection.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }
    }
    public class PayoutRequestReportData
    {
        public string RequestID { get; set; }
        public string ClosingDate { get; set; }
        public string AssociateLoginID { get; set; }
        public string FirstName { get; set; }
        public string GrossAmount { get; set; }
        public string Status { get; set; }
        public string DisplayName { get; set; }
    }
    public class KYCDocument
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string UserID { get; set; }
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string AdharStatus { get; set; }
        public string ActionStatus { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string PanStatus { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImage { get; set; }
        public string DocumentStatus { get; set; }
        public DataSet UploadKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",UserID ) ,
                                      new SqlParameter("@AdharNumber", AdharNumber) ,
                                      new SqlParameter("@AdharImage", AdharImage) ,
                                      new SqlParameter("@PanNumber", PanNumber),
                                      new SqlParameter("@PanImage", PanImage) ,
                                      new SqlParameter("@DocumentNumber", DocumentNumber) ,
                                      new SqlParameter("@DocumentImage", DocumentImage),
                                        new SqlParameter("@Action", ActionStatus),

                                  };
            DataSet ds = Connection.ExecuteQuery("UploadKYC", para);
            return ds;

        }
    }
    public class Enquiry
    {
        public string Status { get; set; }
        public List<EnquiryList> lstBlock1 { get; set; }
        public DataSet EnquiryList()
        {

            DataSet ds = Connection.ExecuteQuery("EnquiryList");
            return ds;
        }

    }
    public class EnquiryList
    {
        public string EnquiryID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
    }
    public class EnquiryData
    {
        public string AddedBy { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string Details { get; set; }
        public string Name { get; set; }
        public DataSet SaveEnquiry()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@Details", Details),
                                        new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = Connection.ExecuteQuery("EnquiryMaster", para);
            return ds;
        }

    }
    public class Sector
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string SiteID { get; set; }
        public List<SectorList> lstsite { get; set; }
        public DataSet GetSectorList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetSectorListMobile", para);
            return ds;
        }
    }
    public class SectorList
    {
        public string PK_SectorID { get; set; }
        public string SectorName { get; set; }
    }
    public class Block
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string PK_BlockID { get; set; }
        public List<BlockList> lstBlock { get; set; }
        public DataSet GetBlockList()
        {
            SqlParameter[] para ={ new SqlParameter("@SiteID",SiteID),
                                     new SqlParameter("@SectorID",SectorID),
                                     new SqlParameter("@BlockID",PK_BlockID),
                                 };
            DataSet ds = Connection.ExecuteQuery("GetBlockListMobile", para);
            return ds;
        }
    }
    public class BlockList
    {
        public string PK_BlockID { get; set; }
        public string BlockName { get; set; }
    }
    public class PlotDetails
    {
        public string Message { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string SiteTypeID { get; set; }
        public string PlotNumber { get; set; }
        public string LoginId { get; set; }
        public List<PlotList> lstPlot { get; set; }
        public string Status { get; internal set; }

        public DataSet GetPlotDetails()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@FK_SiteTypeID",SiteTypeID),
                                new SqlParameter("@PlotNumber",PlotNumber)

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotAvailabilityStatus", para);
            return ds;
        }
    }
    public class PlotList
    {
        public string PlotID { get; set; }
        public string PlotStatus { get; set; }
        public string ColorCSS { get; set; }
        public string PlotAmount { get; set; }
        public string PlotArea { get; set; }
        public string SiteName { get; set; }
        public string BlockName { get; set; }
        public string SectorName { get; set; }
    }
    public class ProfileMobile
    {
        public string Status { get; set; }
        public string UserID { get; set; }
        public string LoginId { get; set; }
        public string SponsorID { get; set; }
        public string SponsorName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DesignationID { get; set; }
        public string DesignationName { get; set; }
        public string BranchID { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string PanNo { get; set; }
        public string BranchName { get; set; }
        public string ProfilePic { get; set; }
        public string AdharNumber { get; set; }
        public string BankAccountNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSCCode { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
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
                                            new SqlParameter("@UpdatedBy", UserID) ,
                                            new SqlParameter("@ProfilePic", ProfilePic),
                                              new SqlParameter("@MemberAccNo", BankAccountNo),
                                                 new SqlParameter("@MemberBankName", BankName),
                                                  new SqlParameter("@MemberBranch", BankBranch),
                                                     new SqlParameter("@IFSCCode", IFSCCode)
                                  };
            DataSet ds = Connection.ExecuteQuery("EditAssociateDetailsForProfile", para);
            return ds;
        }
    }
    public class SummaryReport
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string Leg { get; set; }
        public string IsDownline { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TotalNetAmount { get; set; }
        public List<SummaryList> lstSummary { get; set; }
        public DataSet GetSummaryList()
        {
            SqlParameter[] para =
                            {
                           new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                     new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@IsDownline", IsDownline),
                            };

            DataSet ds = Connection.ExecuteQuery("GetBusiness", para);
            return ds;
        }
    }
    public class SummaryList
    {
        public string LoginId { get; set; }
        public string AssociateID { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerId { get; set; }
        public string Customername { get; set; }
        public string AssociateName { get; set; }
        public string DisplayName { get; set; }
        public string Leg { get; set; }
        public string PaidAmount { get; set; }
        public string ClosingDate { get; set; }
        public string NetAmount { get; set; }
        public string SiteName { get; set; }
        public string SectorName { get; set; }
        public string BlockName { get; set; }
        public string PlotNumber { get; set; }
        public string LeadershipBonus { get; set; }
        public string PaymentDate { get; set; }
        public string Status { get; set; }
        public string CalculationStatus { get; set; }
    }
    public class Branch
    {
        public string Status { get; set; }
        public string PK_BranchID { get; set; }
        public string BranchName { get; set; }
        public string ErrorMessage { get; set; }
        public DataSet GetBranchList()
        {
            DataSet ds = Connection.ExecuteQuery("GetBranchList");
            return ds;
        }
    }
    public class Designation
    {
        public string Status { get; set; }
        public string Percentage { get; set; }
        public string ErrorMessage { get; set; }
        public List<DesignationDetails> lstdesign { get; set; }
        public DataSet GetDesignationList()
        {

            SqlParameter[] para = {

                                      new SqlParameter("@Percentage", Percentage)

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDesignationList", para);

            return ds;
        }

    }
    public class DesignationDetails
    {
        public string PK_DesignationID { get; set; }
        public string DesignationName { get; set; }
    }
    public class TestLoginAPI
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string Pk_userId { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string ProfilePic { get; set; }
        public string StatusColor { get; set; }
        public DataSet LoginActionTest()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId)
            };
            DataSet ds = Connection.ExecuteQuery("LoginAsAssociateForMobileTest", para);
            return ds;
        }
    }


    public class TreeAPI
    {
        public List<Tree1> GetGenelogy { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string Fk_headId { get; set; }
        public DataSet GetTree()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@Fk_headId", Fk_headId)
                                  };

            DataSet ds = Connection.ExecuteQuery("GetTree", para);
            return ds;
        }
    }

    public class Tree1
    {
        public string Fk_UserId { get; set; }
        public string SponsorId { get; set; }
        public string Fk_ParentId { get; set; }
        public string TeamPermanent { get; set; }
        public string LoginId { get; set; }
        public string Fk_SponsorId { get; set; }
        public string MemberName { get; set; }
        public string MemberLevel { get; set; }

        public string Id { get; set; }
        public string Leg { get; set; }

        public string ActivationDate { get; set; }
        public string ActiveLeft { get; set; }
        public string ActiveRight { get; set; }
        public string InactiveLeft { get; set; }
        public string InactiveRight { get; set; }
        public string BusinessLeft { get; set; }
        public string BusinessRight { get; set; }
        public string ImageURL { get; set; }
    }
    public class Profile1
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class RegistrationAPI
    {

        public string Status { get; set; }
        public string Message { get; set; }
        public string Leg { get; set; }

        public string Password { get; set; }
        public string RegistrationBy { get; set; }
        public string SponsorId { get; set; }
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LoginId { get; set; }

        public string TransPassword { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string PanCard { get; set; }
        public string Gender { get; set; }

        public DataSet Registration()
        {
            SqlParameter[] para = {

                                   new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@Email",Email),
                                   new SqlParameter("@MobileNo",MobileNo),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                    new SqlParameter("@PanCard",PanCard),
                                    new SqlParameter("@RegistrationBy",RegistrationBy),
                                     new SqlParameter("@Address",Address),
                                     new SqlParameter("@Gender",Gender),
                                     new SqlParameter("@PinCode",PinCode),
                                     new SqlParameter("@Leg",Leg),
                                     new SqlParameter("@Password",Password)

                                   };
            DataSet ds = Connection.ExecuteQuery("RegistrationMobile", para);
            return ds;
        }

    }
    public class SponsorNameA
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string UserID { get; set; }
        public string SponsorName { get; set; }
        
    }
    public class SponsorNameAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string SponsorId { get; set; }


        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", SponsorId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetMemberName", para);

            return ds;
        }
    }

    public class CustomerRegistration
    {
        public string BranchID { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PanNo { get; set; }
        public string Password { get; set; }
        public string AdharNumber { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string AddedBy { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string ProfilePic { get; set; }
        public string Amount { get; set; }
        public string RegistrationType { get; set; }
        public string CouponNo { get; set; }
        public string PaymentMode { get; set; }
        public string Leg { get; set; }
        public string Action { get; set; }
        public string Photo { get; set; }
        public string AssociateID { get; set; }
        public string BookingNo { get;  set; }
    }
    public class ResponseCustomerRegistration
    {
        public string Status { get; set; }
        public string SuccessMessage { get; set; }
        public string Name { get; set; }
        public string Pk_userId { get; set; }
        public string ErrorMessage { get; set; }
        public DataSet CustomerRegistration(CustomerRegistration reg)
        {
            SqlParameter[] para = { new SqlParameter("@BranchID", reg.BranchID) ,
                                  new SqlParameter("@SponsorID", reg.UserID) ,
                                  new SqlParameter("@RoleID", 3) ,
                                  new SqlParameter("@FirstName", reg.FirstName) ,
                                  new SqlParameter("@LastName", reg.LastName) ,
                                  new SqlParameter("@Contact", reg.Contact) ,
                                  new SqlParameter("@Email", reg.Email) ,
                                  new SqlParameter("@Pincode", reg.PinCode) ,
                                  new SqlParameter("@State", reg.State) ,
                                  new SqlParameter("@City", reg.City) ,
                                  new SqlParameter("@Address", reg.Address) ,
                                  new SqlParameter("@PanNo", reg.PanNo) ,
                                  new SqlParameter("@AddedBy", reg.AddedBy) ,
                                  new SqlParameter("@Password", reg.Password) ,
                                  new SqlParameter("@RegistrationType",reg.RegistrationType),
                                  new SqlParameter("@Amount",reg.Amount),
                                  new SqlParameter("@CouponNo",reg.CouponNo),
                                  new SqlParameter("@PaymentMode",reg.PaymentMode),
                                  new SqlParameter("@TransactionNo",reg.TransactionNo),
                                  new SqlParameter("@BookingNo",reg.BookingNo),
                                  new SqlParameter("@TransactionDate",reg.TransactionDate),
                                  new SqlParameter("@BankName",reg.BankName),
                                  new SqlParameter("@BankBranch",reg.BankBranch),
                                  new SqlParameter("@Leg",reg.Leg),
                                  new SqlParameter( "@Action", reg.Action),
                                  new SqlParameter( "@Photo", reg.Photo),
                                  new SqlParameter( "@AdjustmentId", reg.AssociateID),
                                  };
            DataSet ds = Connection.ExecuteQuery("CustomerRegistration", para);
            return ds;
        }
        public DataSet CustomerRegistrationByAssociate(CustomerRegistration reg)
        {
            SqlParameter[] para = { new SqlParameter("@BranchID", reg.BranchID) ,
                                  new SqlParameter("@SponsorID", reg.UserID) ,
                                  new SqlParameter("@FirstName", reg.FirstName) ,
                                  new SqlParameter("@LastName", reg.LastName) ,
                                  new SqlParameter("@Contact", reg.Contact) ,
                                  new SqlParameter("@Email", reg.Email) ,
                                  new SqlParameter("@Pincode", reg.PinCode) ,
                                  new SqlParameter("@State", reg.State) ,
                                  new SqlParameter("@City", reg.City) ,
                                  new SqlParameter("@Address", reg.Address) ,
                                  new SqlParameter("@PanNo", reg.PanNo) ,
                                  new SqlParameter("@AddedBy",reg.AddedBy) ,
                                  new SqlParameter("@Password", reg.Password) ,
                                  new SqlParameter("@RegistrationType",reg.RegistrationType),
                                  new SqlParameter("@Amount",reg.Amount),
                                  new SqlParameter("@CouponNo",reg.CouponNo),
                                  new SqlParameter("@PaymentMode",reg.PaymentMode),
                                  new SqlParameter("@TransactionNo",reg.TransactionNo),
                                  new SqlParameter("@TransactionDate",reg.TransactionDate),
                                  new SqlParameter("@BankName",reg.BankName),
                                  new SqlParameter("@BankBranch",reg.BankBranch),
                                  new SqlParameter("@Leg",reg.Leg),
                                  new SqlParameter("@Photo",reg.Photo),
                                  new SqlParameter("@AdjustmentId",reg.AssociateID)
                                  };
            DataSet ds = Connection.ExecuteQuery("CustomerRegistrationByAssociate", para);
            return ds;
        }
    }
    public class Payment
    {

        public string Status { get; set; }
        public string Message { get; set; }
        public List<Payment_Mode> lstPayment { get; set; }
        public DataSet GetPaymentMode()
        {
            DataSet ds = Connection.ExecuteQuery("GetPaymentModeListMobile");
            return ds;
        }
    }
    public class Payment_Mode
    {
        public string PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
    }
    public class CustomerLedger
    {
        public string BookingNumber { get; set; }
        public string LoginId { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotNumber { get; set; }
        public List<LedgerReport> lstledger { get; set; }
        public DataSet FillDetailsForCustomer()
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
            DataSet ds = Connection.ExecuteQuery("GetLedgerDetailsForCustomer", para);
            return ds;
        }
    }
    public class LuckyDraw
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string CustomerLoginID { get; set; }
        public string FromDate { get; set; }
        public string CustomerName { get; set; }
        public string ToDate { get; set; }
        public string PaymentMode { get; set; }
        public string UserID { get; set; }
        public List<LuckyDrawList> lst { get; set; }
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
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                        new SqlParameter("@PaymentMode", PaymentMode),
                                        new SqlParameter("@UserId",UserID)
                                  };

            DataSet ds = Connection.ExecuteQuery("GetLuckyDrawRequestList", para);
            return ds;
        }
    }
    public class LuckyDrawList
    {
        public string LuckyDrawId { get; set; }
        public string CustomerLoginID { get; set; }
        public string FromDate { get; set; }
        public string CustomerName { get; set; }
        public string ToDate { get; set; }
        public string PaymentMode { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string AssociateID { get; set; }
        public string AssociateName { get; set; }
        public string Amount { get; set; }
        public string CouponNo { get; set; }
        public string ApprovalStatus { get; set; }
    }
    public class CustomerDashboardforMobile
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string LoginId { get; set; }
        public string CustomerID { get; set; }
        public string PK_NewsID { get; set; }
        public List<DueInstallment> lstdueinstallment { get; set; }
        public List<NewsDetails> lstnewsdetail { get; set; }
        public List<CustomerDashboardDetails> lstStatus { get; set; }

        public DataSet GetDetails()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId)
                                  };
            DataSet ds = Connection.ExecuteQuery("DetailsCustomerDashboard", para);
            return ds;

        }
        public DataSet GetDueInstallmentList()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@Fk_CustomerId", CustomerID)
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDueInstallmentForCustomerDashboard", para);
            return ds;
        }
        public DataSet GetNews()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PK_NewsID", PK_NewsID),
                                        new SqlParameter("@NewsFor", "Customer")
                                  };
            DataSet ds = Connection.ExecuteQuery("GetNewsList", para);
            return ds;
        }
    }
    public class CustomerDashboardDetails
    {
        public string TotalBooking { get; set; }
        public string TotalPaidAmount { get; internal set; }
        public string TotalPending { get; internal set; }
        public string TotalPlotAmount { get; internal set; }
    }
    public class DueInstallmentMobile
    {
        public string BookingNumber { get; set; }
        public string LoginId { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotNumber { get; set; }
        public string FromDate { get; private set; }
        public string ToDate { get; private set; }

        public DataSet FillDueInstDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@BookingNo",BookingNumber),
                                  new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                   new SqlParameter("@FK_SiteID",SiteID),
                                   new SqlParameter("@FK_SectorID",SectorID),
                                   new SqlParameter("@FK_BlockID",BlockID),
                                   new SqlParameter("@PlotNumber",PlotNumber),

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotDetailsForDueInstallment", para);
            return ds;
        }
    }
    public class ReturnBenefit
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Ispaid { get; set; }
        public string LoginId { get; set; }
        public List<ReturnBenefitView> lstbenefit { get; set; }
        public DataSet ReturnBenefitView()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@FirstName", Name),
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Status", Ispaid),
                                     };
            DataSet ds = Connection.ExecuteQuery("ReturnBenefitReport", para);
            return ds;
        }
    }
    public class ReturnBenefitView
    {
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string PaidAmount { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        public string ReceiptNo { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string InstAmt { get; set; }
    }
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class RewardAPI
    {
        public string UserId { get; set; }
        public List<RewardList> lstReward { get; set; }
        public string RewardID { get; set; }

        public DataSet RewardList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Fk_RewardId", RewardID),
                                        new SqlParameter("@FK_UserId", UserId)};
            DataSet ds = Connection.ExecuteQuery("_GetRewardData", para);
            return ds;
        }
    }

    public class RewardList
    {
        public string Contact { get; set; }
        public string LeftBusiness { get; set; }
        public string MatchingTarget { get; set; }
        public string PK_RewardItemId { get; set; }
        public string QualifyDate { get; set; }
        public string RewardImage { get; set; }
        public string RewardName { get; set; }
        public string RightBusines { get; set; }
        public string Status { get; set; }
    }
    public class CustomerDetails
    {

        public string Address { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Fk_SponsorId { get; set; }
        public string JoiningFromDate { get; set; }
        public string PanNo { get; set; }
        public string Password { get; set; }
        public string AssociateID { get; set; }
        public string AssociateName { get; set; }
        public string State { get; set; }
        public string UserID { get; set; }
    }
    public class CustomerList
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string UserID { get; set; }
        public List<CustomerDetails> lstCustomer { get; set; }

        public DataSet GetListCustomer()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", UserID),
                                  new SqlParameter("@CustomerLoginID", CustomerLoginID),
                                  new SqlParameter("@CustomerName", CustomerName),
                                  new SqlParameter("@AssociateLoginID", AssociateLoginID),
                                  new SqlParameter("@AssociateName", AssociateName),
                                  new SqlParameter("@FromDate", FromDate),
                                  new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = Connection.ExecuteQuery("SelectCustomerForAssociate", para);
            return ds;
        }
    }
    public class PlotAPI
    {
        public string AddedBy { get; set; }
        public string Amount { get; set; }
        public string BlockID { get; set; }
        public DateTime HoldFrom { get; set; }
        public DateTime HoldTo { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string PlotID { get; set; }
        public string PlotNumber { get; set; }
        public string Remark { get; set; }
        public string SectorID { get; set; }
        public string SiteID { get; set; }
        public string CustomerName { get; set; }
        public string SponsorName { get; set; }
        public string SponsorLoginID { get; set; }
        public string SponsorID { get; set; }
        public string CustomerID { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AssociateID { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PanCard { get; set; }
        public string Leg { get; set; }
        public string PaymentDate { get; set; }
        public string Address { get; set; }
       

        //public DataSet SavePlotHoldByAssociate()
        //{
        //    SqlParameter[] para =
        //                   {
        //                                new SqlParameter("@Fk_PlotId ",PlotID),
        //                                new SqlParameter("@FK_SiteID ",SiteID),
        //                                new SqlParameter("@FK_SectorID" , SectorID),
        //                                new SqlParameter("@FK_BlockID" , BlockID),
        //                                new SqlParameter("@PlotNumber"  , PlotNumber),
        //                                new SqlParameter("@HoldFrom" ,HoldFrom),
        //                                new SqlParameter("@HoldTo" ,HoldTo),
        //                                new SqlParameter("@FirstName" ,FirstName),
        //                                new SqlParameter("@Mobile" ,Mobile),
        //                                new SqlParameter("@AddedBy",AddedBy)  ,
        //                                 new SqlParameter("@Remark1",Remark),
        //                                   new SqlParameter("@HoldAmount",Amount),
        //                                   new SqlParameter("@SponsorName" ,SponsorName),
        //                                     new SqlParameter("@SponsorLoginId" ,SponsorLoginID),
        //                                        new SqlParameter("@SponsorId" ,SponsorID),
        //                                    new SqlParameter("@CustomerId" ,CustomerID),
        //                                new SqlParameter("@PaymentMode" ,PaymentMode),
        //                                new SqlParameter("@TransactionNo",TransactionNo)  ,
        //                                new SqlParameter("@TransactionDate",TransactionDate)  ,
        //                                 new SqlParameter("@BankName",BankName),
        //                                  new SqlParameter("@L",BankName),
        //                                   new SqlParameter("@LastName",LastName),
        //                                   new SqlParameter("@BankBranch",BankBranch),
        //                                    new SqlParameter("@AdjustmentId",AssociateID),
        //                    };
        //    DataSet ds = Connection.ExecuteQuery("PlotHoldByAssociate", para);
        //    return ds;
        //}
        public DataSet SavePlotHoldByAssociateNew()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_PlotId ",PlotID),
                                        new SqlParameter("@FK_SiteID ",SiteID),
                                        new SqlParameter("@FK_SectorID" , SectorID),
                                        new SqlParameter("@FK_BlockID" , BlockID),
                                        new SqlParameter("@PlotNumber"  , PlotNumber),
                                        new SqlParameter("@HoldFrom" ,HoldFrom),
                                        new SqlParameter("@HoldTo" ,HoldTo),
                                        new SqlParameter("@FirstName" ,FirstName),
                                         new SqlParameter("@LastName" ,LastName),
                                         new SqlParameter("@Password",Password),
                                          new SqlParameter("@Gender" ,Gender),
                                          new SqlParameter("@Email" ,Email),
                                           new SqlParameter("@PanCard" ,PanCard),
                                            new SqlParameter("@Leg" ,Leg),
                                        new SqlParameter("@MobileNo" ,Mobile),
                                        new SqlParameter("@AddedBy",AddedBy)  ,
                                         new SqlParameter("@Remark1",Remark),
                                           new SqlParameter("@HoldAmount",Amount),
                                           new SqlParameter("@SponsorName" ,SponsorName),
                                             new SqlParameter("@SponsorLoginId" ,SponsorID),
                                        new SqlParameter("@PaymentMode" ,PaymentMode),
                                        new SqlParameter("@TransactionNo",TransactionNo)  ,
                                        new SqlParameter("@TransactionDate",TransactionDate)  ,
                                         new SqlParameter("@BankName",BankName),
                                           new SqlParameter("@BankBranch",BankBranch),
                                            new SqlParameter("@AdjustmentId",AssociateID),
                                             new SqlParameter("@PaymentDate",PaymentDate),
                                              new SqlParameter("@Address",Address),
                            };
            DataSet ds = Connection.ExecuteQuery("PlotHoldByAssociateNew", para);
            return ds;
        }
        public DataSet SaveTemporaryPlotHold()
        {
            SqlParameter[] para =
                            {
                                        new SqlParameter("@Fk_PlotId ",PlotID),
                                        new SqlParameter("@FK_SiteID ",SiteID),
                                        new SqlParameter("@FK_SectorID" , SectorID),
                                        new SqlParameter("@FK_BlockID" , BlockID),
                                        new SqlParameter("@PlotNumber"  , PlotNumber),
                                        new SqlParameter("@HoldFrom" ,HoldFrom),
                                        new SqlParameter("@HoldTo" ,HoldTo),
                                        new SqlParameter("@Name" ,Name),
                                        new SqlParameter("@Mobile" ,Mobile),
                                        new SqlParameter("@AddedBy",AddedBy)  ,
                                         new SqlParameter("@Remark1",Remark),
                                           new SqlParameter("@HoldAmount",Amount),
                            };
            DataSet ds = Connection.ExecuteQuery("TemporaryPlotHold", para);
            return ds;
        }
    }
    public class ResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string LoginId { get; set; }
        public string Type { get; set; }
    }
    public class WalletAPI
    {
        public string EncryptLoginID { get; set; }
        public string EncryptPayoutNo { get; set; }
        public string LoginId { get; set; }
        public string DisplayName { get; set; }
        public string PayoutNo { get; set; }
        public string ClosingDate { get; set; }
        public string DirectIncome { get; set; }
        public string BinaryIncome { get; set; }
    }
    public class PayoutAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string PayoutNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<WalletAPI> lstPayout { get; set; }
        public DataSet GetPayoutReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PayoutNo", PayoutNo),
                                         new SqlParameter("@FromDate", FromDate),
                                         new SqlParameter("@ToDate", ToDate),

            };
            DataSet ds = Connection.ExecuteQuery("PayoutReportForMember", para);
            return ds;
        }
    }
    public class BenefitReport
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string DisplayName { get; set; }
        public string LoginId { get; set; }
        public string Ispaid { get; set; }
        public List<BenefitReportResponse> lstassociate { get; set; }
        public DataSet ReturnBenefitAssociate()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@FirstName", DisplayName),
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Status", Ispaid),
                                     };
            DataSet ds = Connection.ExecuteQuery("ReturnBenefitAssociate", para);
            return ds;
        }
    }
    public class BenefitReportResponse
    {
        public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string ReturnBenefitStartDate { get; set; }
    }
    public class PlotStatus
    {
        public string ActualPlotRate { get; set; }
        public string BookingAmount { get; set; }
        public string BookingPercent { get; set; }
        public string Message { get; set; }
        public string NetPlotAmount { get; set; }
        public string PlotAmount { get; set; }
        public string PlotID { get; set; }
        public string PlotSize { get; set; }
        public string Status { get; set; }
        public string TotalPLC { get; set; }
    }
    public class PlotStatusReq
    {
        public string BlockID { get; set; }
        public string PlotNumber { get; set; }
        public string SectorID { get; set; }
        public string SiteID { get; set; }
        public DataSet CheckPlotAvailibility()
        {
            SqlParameter[] para =
                            {
                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber)
                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotStatus", para);
            return ds;
        }
    }
    public class PaymentStatus
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public DataSet GetPaymentStatus()
        {
            DataSet ds = Connection.ExecuteQuery("GetPaymentStatus");
            return ds;
        }
    }
    public class PaymentStatusResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PaymentStatus> lst { get; set; }
    }
    public class CustomerDetail
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string DisplayName { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DataSet GetCustomerDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetCustomerName", para);

            return ds;
        }
    }
    public class CustomerInfo
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string SponsorID { get; set; }
        public string SponsorLoginID { get; set; }
        public string SponsorName { get; set; }
        public string CustomerID { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public DataSet GetCustomerDetail(string CustomerLoginId)
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", CustomerLoginId) };
            DataSet ds = Connection.ExecuteQuery("GetCustomerDetail", para);
            return ds;
        }
    }
    public class PlotHoldList
    {
        public string PK_PlotHoldID { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotNumber { get; set; }
        public string CustomerID { get; set; }
        public string AddedBy { get; set; }
        public DataSet GetPlotHoldListAssociate()
        {
            SqlParameter[] para = { new SqlParameter("@PK_PlotHoldID", PK_PlotHoldID),
                                   new SqlParameter("@FK_SiteID" ,SiteID),
                                        new SqlParameter("@FK_SectorID" ,SectorID),
                                        new SqlParameter("@FK_BlockID" ,BlockID),
                                        new SqlParameter("@PlotNumber" ,PlotNumber),
                                            new SqlParameter("@CustomerID" ,CustomerID),
                                  
                                   new SqlParameter("@AddedBy" ,AddedBy),

                                  };


            DataSet ds = Connection.ExecuteQuery("GetPlotHoldListAssociate", para);
            return ds;
        }

    }
   public class ApprovedBookingList
    {
        public string AssociateID { get; set;}
        public string BookingNumber { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string BlockID { get; set; }
        public string PlotNumber { get; set; }
        public string PaymentPlanID { get; set; }
        public string AddedBy { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PlotMobile> lstApprove { get; set; }
        public DataSet GetBookingDetailsListAssociate()
        {
            SqlParameter[] para = { 
                                      new SqlParameter("@AssociateID", AssociateID),
                                      new SqlParameter("@BookingNo", BookingNumber),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@FK_SiteID", SiteID),
                                      new SqlParameter("@FK_SectorID", SectorID),
                                      new SqlParameter("@FK_BlockID", BlockID),
                                      new SqlParameter("@PlotNumber", PlotNumber),
                                       new SqlParameter("@Fk_PlanId", PaymentPlanID),
                                       new SqlParameter("@AddedBy",AddedBy)
                                  };

            DataSet ds = Connection.ExecuteQuery("GetPlotBookingAssociate", para);
            return ds;
        }
    }
    public class PlotMobile
    {
        public string BookingStatus { get; set; }
        public string PK_BookingId { get; set; }
        public string BranchID { get; set; }
        public string BranchName { get; set; }
        public string CustomerID { get; set; }
        public string CustomerLoginID { get; set; }
        public string CustomerName { get; set; }
        public string AssociateID { get; set; }
        public string AssociateLoginID { get; set; }
        public string Discount { get; set; }
        public string AssociateName { get; set; }
        public string SiteName { get; set; }
        public string SectorName { get; set; }
        public string BlockName { get; set; }
        public string PlotNumber { get; set; }
        public string BookingDate { get; set; }
        public string BookingAmount { get; set; }
        public string PaymentPlanID { get; set; }
        public string BookingNumber { get; set; }
        public string PaidAmount { get; set; }
        public string PlotArea { get; set; }
        public string PlotAmount { get; set; }
        public string NetPlotAmount { get; set; }
        public string PK_PLCCharge { get; set; }
        public string PlotRate { get; set; }
        public string Type { get; set; }
    }
    public class TopupMobile
    {
        public string AssociateID { get; set; }
        public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<CustomerMobile> ListCust { get; set; }
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
    public class CustomerMobile
    {
        public string PK_TopUpId { get; set; }
        public decimal CouponAmount { get; set; }
        public string TransactionDate { get; set; }
        public string AccountNo { get; set; }
        public string PaymentMode { get; set; }
        public string status { get; set; }
        public string CustomerName { get; set; }
        public string AssociateLoginID { get; set; }
        public string AssociateName { get; set; }
    }
    public class WalletMobile
    {
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<WalletListMobile> lstpayoutledger { get; set; }
        public DataSet GetPaidPayout()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId),
                                    new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate), };
            DataSet ds = Connection.ExecuteQuery("GetPaidPayoutDetails", para);
            return ds;
        }
    }
    public class WalletListMobile
    {
      
        public string LoginId { get; set; }
        public string  DisplayName { get; set; }
        public string PaymentDate { get; set; }
        public string Amount { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionNo { get; set; }
    }
    public class PlotListMobile
    {
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BlockID { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string PK_PlotHoldID { get; set; }
        public string EncryptKey { get; set; }
        public string PlotNumber { get; set; }
        public string CustomerID { get; set; }
        public string HoldFrom { get; set; }
        public string HoldTo { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string SponsorID { get; set; }
        public string AddedBy { get; set; }
        public List<Plotlistmobile> lstPlotlistmobile { get; set; }
       
        public DataSet GetPlotHoldListAssociate()
        {
            SqlParameter[] para = { new SqlParameter("@PK_PlotHoldID", PK_PlotHoldID),
                                   new SqlParameter("@FK_SiteID" ,SiteID),
                                        new SqlParameter("@FK_SectorID" ,SectorID),
                                        new SqlParameter("@FK_BlockID" ,BlockID),
                                        new SqlParameter("@PlotNumber" ,PlotNumber),
                                            new SqlParameter("@CustomerID" ,CustomerID),
                                   new SqlParameter("@SponsorID" ,SponsorID),
                                   new SqlParameter("@FromDate" ,FromDate),
                                   new SqlParameter("@ToDate" ,ToDate),
                                   new SqlParameter("@AddedBy" ,AddedBy),

                                  };


            DataSet ds = Connection.ExecuteQuery("GetPlotHoldListAssociate", para);
            return ds;
        }
    }
    public class Plotlistmobile
    {

        public string PK_PlotHoldID { get; set; }
        public string EncryptKey { get; set; }
        public string PlotNumber { get; set; }
        public string CustomerID { get; set; }
        public string HoldFrom { get; set; }
        public string HoldTo { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    }

    public class DirectListMobile
    {
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string Leg { get; set; }
        public List<ReportsMobile> lstassociateDirect { get; set; }
        public DataSet GetDirectList()
        {

            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status),
                                  };
            DataSet ds = Connection.ExecuteQuery("GetDirectList", para);
            return ds;
        }
    }
    public class ReportsMobile
    {

        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PlotNumber { get; set; }
        public string JoiningDate { get; set; }
        public string Leg { get; set; }
        public string PermanentDate { get; set; }
        public string Status { get; set; }
        public string SponsorId { get; set; }
        public string Password { get; set; }
        public string SponsorName { get; set; }
        public string Package { get; set; }
    }
}

