using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsterHeight.Models
{
    public class Reports : Common
    {
        public string ErrorMessage { get; set; }
        public string ReturnBenefitStartDate { get; set; }
        public string SponsorName { get; set; }
        public string SponsorId { get; set; }
        public string Ispaid { get; set; }
        public string InstAmt { get; set; }
        public string DueDate { get; set; }

        public string TransactionNo { get; set; }
        public string BankBranch { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string SiteName { get; set; }
        public string BlockName { get; set; }
        public string PlotID { get; set; }
        public string PlotStatus { get; set; }
        public string PlotSize { get; set; }
        public string SectorName { get; set; }
        public string AssociateName { get; set; }
        public string AssociateID { get; set; }
        public string CustomerId { get; set; }
        public string Customername { get; set; }
        public string CustomerLoginID { get; set; }
        public string ColorCSS { get; set; }
        public List<Reports> lstPlot { get; set; }
        public string BlockID { get; set; }
        public string SiteID { get; set; }
        public string SectorID { get; set; }
        public string MemberAccNo { get; set; }
        public string IFSCCode { get; set; }
        public int Id { get; set; }
        public List<Reports> lstLuckyDraw { get; set; }
        public string Month { get; set; }
        public string ReceiptNo { get; set; }
        public string FK_InvestmentID { get; set; }
        public string UpgradtionDate { get; set; }
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string PlotNumber { get; set; }
        public List<Reports> lsttopupreport { get; set; }

        public string LeadershipBonus { get; set; }
        public string NetAmount { get; set; }
        public string ClosingDate { get; set; }
        public bool IsDownline { get; set; }
        public string Password { get; set; }
        public List<Reports> lstassociate { get; set; }
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
        public string PK_SallaryDetailsId { get; set; }
        public string PaymentDate { get; set; }
        public string PaidAmount { get; set; }
        public string InstallmentAmount { get; set; }
        public string AdjustmentId { get; set; }
        public string PaymentMode { get; set; }
        public string DueAmount { get; set; }
        public string LoginId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Name { get; set; }
        public string PanNo { get; set; }
        public string Leg { get; set; }
        public string Status { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string Mobile { get; set; }
        public string JoiningDate { get; set; }
        public string Package { get; set; }
        public string PermanentDate { get; set; }
        public string Email { get; set; }
        public List<SelectListItem> ddlSector { get; set; }
        public List<SelectListItem> lstBlock { get; set; }
        public string LuckyDrawId { get;  set; }
        public string Quantity { get;  set; }
        public string MRP { get;  set; }
        public string IGST { get;  set; }
        public string CGST { get;  set; }
        public string SGST { get;  set; }
        public string FinalAmount { get;  set; }
        public string CalculationStatus { get; set; }
        public string RewardID { get;  set; }
        public string QualifyDate { get;  set; }
        public string RewardImage { get;  set; }
        public string RewardName { get;  set; }
        public string Contact { get;  set; }
        public string PK_RewardItemId { get; set; }
        public List<Reports> lstReward { get; set; }
        public string MatchingTarget { get;  set; }
        public string LeftBusiness { get;  set; }
        public string RightBusines { get;  set; }
        public string RightBusiness { get; set; }
        public string AdminId { get; set; }
        public List<Reports> ActualLeftRightBusinessList { get; set; }


        

        public DataSet GetBookingDetailsList()
        {
            SqlParameter[] para = { new SqlParameter("@PK_BookingId", PK_BookingId )};
            DataSet ds = Connection.ExecuteQuery("GetPlotBooking", para);
            return ds;
        }
        public DataSet GetSiteList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("SiteList", para);
            return ds;
        }
        public DataSet GetSalaryInstallment()
        {

            SqlParameter[] para = {
                                      new SqlParameter("@UserID", Fk_UserId),
                                       new SqlParameter("@InstallmentNo", InstallmentNo),
                                        new SqlParameter("@LoginId", LoginId),
                                     };
            DataSet ds = Connection.ExecuteQuery("GetDataForBenefit", para);
            return ds;
        }

        public DataSet GetDataForBenefitDetails()
        {

            SqlParameter[] para = {
                                      new SqlParameter("@UserID", Fk_UserId),
                                       new SqlParameter("@InstallmentNo", InstallmentNo),
                                        new SqlParameter("@LoginId", LoginId),
                                     };
            DataSet ds = Connection.ExecuteQuery("GetDataForBenefitDetails", para);
            return ds;
        }

        public DataSet SaveBenefit()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PaymentDate", PaymentDate),
                                      new SqlParameter("@InstallmentNo", InstallmentNo),
                                       new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@TransactionNo", TransactionNo),
                                    new SqlParameter("@TransactionDate", TransactionDate),
                                    new SqlParameter("@BankName", BankName),
                                    new SqlParameter("@BankBranch", BankBranch)};


            DataSet ds = Connection.ExecuteQuery("PayBenefit", para);
            return ds;
        }
        public DataSet ReturnBenefit()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@FirstName", DisplayName),
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Status", Ispaid),
                                         new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate)
                                     };
            DataSet ds = Connection.ExecuteQuery("ReturnBenefitData", para);
            return ds;
        }
        public DataSet ReturnBenefitAssociate()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@FirstName", DisplayName),
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Status", Ispaid),
                                           new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate)
                                     };
            DataSet ds = Connection.ExecuteQuery("ReturnBenefitAssociate", para);
            return ds;
        }

        public DataSet GetPaymentMode()
        {

            DataSet ds = Connection.ExecuteQuery("GetPaymentModeList");
            return ds;
        }
        public DataSet GetPaymentModeForBenefitPay()
        {

            DataSet ds = Connection.ExecuteQuery("GetPaymentModeForBenefitPay");
            return ds;
        }
        public DataSet ReturnBenefitView()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@FirstName", DisplayName),
                                        new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@Status", Ispaid),
                                     };
            DataSet ds = Connection.ExecuteQuery("ReturnBenefitReport", para);
            return ds;
        }
        public DataSet SaveSalary()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@SallaryDetailsId",PK_SallaryDetailsId),
                                        new SqlParameter("@PaymentDate",PaymentDate),
                                        new SqlParameter("@PaymentMode",PaymentMode),
                                        new SqlParameter("@TransactionNo",TransactionNo),
                                        new SqlParameter("@TransactionDate",TransactionDate),
                                        new SqlParameter("@BankName",BankName),
                                        new SqlParameter("@BankBranch",BankBranch),
                                         new SqlParameter("@UpdatedBy", UpdatedBy),
                                     };
            DataSet ds = Connection.ExecuteQuery("SaveSalary", para);
            return ds;
        }
        public DataSet GetBlockList()
        {
            SqlParameter[] para ={ new SqlParameter("@SiteID",SiteID),
                                     new SqlParameter("@SectorID",SectorID),
                                     new SqlParameter("@BlockID",BlockID),
                                 };
            DataSet ds = Connection.ExecuteQuery("GetBlockList", para);
            return ds;
        }
        public DataSet GetDetails()
        {
            SqlParameter[] para =
                            {

                                new SqlParameter("@SiteID",SiteID),
                                new SqlParameter("@SectorID",SectorID),
                                new SqlParameter("@BlockID",BlockID),
                                new SqlParameter("@PlotNumber",PlotNumber)

                            };
            DataSet ds = Connection.ExecuteQuery("GetPlotAvailabilityStatus", para);
            return ds;
        }
        public DataSet ProductRewardList()
        {

            DataSet ds = Connection.ExecuteQuery("RewardList");
            return ds;
        }
        public DataSet RewardListForAchiever()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId),
                                    new SqlParameter("@FK_RewardId", RewardID), }
                                    ;
            DataSet ds = Connection.ExecuteQuery("RewardListForAchiever", para);
            return ds;
        }
        public DataSet Reward()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate), }
                                    ;
            DataSet ds = Connection.ExecuteQuery("GetRewardAchieverList", para);
            return ds;
        }
        public DataSet SalaryList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId)
            }
                                    ;
            DataSet ds = Connection.ExecuteQuery("GetSalarylistbyid", para);
            return ds;
        }
        public DataSet fillDetails()
        {
            SqlParameter[] para =
                            {
                                 new SqlParameter("@SallaryDetailsId",PK_SallaryDetailsId),

                            };
            DataSet ds = Connection.ExecuteQuery("fillDetailsforsalary", para);
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
        public DataSet GetSectorList()
        {
            SqlParameter[] para = { new SqlParameter("@SiteID", SiteID) };
            DataSet ds = Connection.ExecuteQuery("GetSectorList", para);
            return ds;
        }

        public DataSet GetDownlineList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                     new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status), };
            DataSet ds = Connection.ExecuteQuery("DownlineList", para);
            return ds;
        }
        public DataSet LuckyDrawList()
        {
            DataSet ds = Connection.ExecuteQuery("LuckyDrawListForAdmin");
            return ds;
        }


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

        public DataSet BusinessReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),

                                     new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@IsDownline", IsDownline),

            };
            DataSet ds = Connection.ExecuteQuery("GetBusiness", para);

            return ds;
        }

        public DataSet GetTopupReport()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginID", LoginId),
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@Package", Package),
                                      new SqlParameter("@ClaculationStatus", Status),
                                  };

            DataSet ds = Connection.ExecuteQuery("GetTopupreport", para);
            return ds;
        }
        public DataSet PayReport()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@UserID", Fk_UserId),
                                          new SqlParameter("@LoginID", LoginId),
                                            new SqlParameter("@Name", DisplayName),
                                             new SqlParameter("@FromDate", FromDate),
                                              new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = Connection.ExecuteQuery("PayReport", para);
            return ds;
        }
        public DataSet RoyalityClubList()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@UserID", Fk_UserId)
            };
            DataSet ds = Connection.ExecuteQuery("RoyalityClubList", para);
            return ds;
        }
        public DataSet GetAssociateList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@UserID",Fk_UserId),
                new SqlParameter("@LoginID",LoginId),
                new SqlParameter("@ToDate",ToDate),
            };
            DataSet ds = Connection.ExecuteQuery("GetAssociateListForApprove", para);
            return ds;
        }
        
        public DataSet ApprovedAssociate()
        {

            SqlParameter[] para = {
                                   
                                      new SqlParameter("@AssociateID", AssociateID),
                                        new SqlParameter("@ApprovedBy", AddedBy),
                                        new SqlParameter("LoginId",LoginId),
                                     };
            DataSet ds = Connection.ExecuteQuery("ApprovedAssociate", para);
            return ds;
        }
        public DataSet RejectAssociate()
        {

            SqlParameter[] para = {

                                      new SqlParameter("@AssociateID", AssociateID),
                                        new SqlParameter("@RejectedBy", AddedBy),
                                        new SqlParameter("LoginId",LoginId),
                                     };
            DataSet ds = Connection.ExecuteQuery("RejectAssociate", para);
            return ds;
        }

        public DataSet PrintLuckyDrawReceipt()
        {
            SqlParameter[] para = { new SqlParameter("@PK_LuckyDrawId", LuckyDrawId), };
            DataSet ds = Connection.ExecuteQuery("PrintLuckyDrawReport", para);
            return ds;
        }

        public DataSet GetCustomerList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@UserID",Fk_UserId),
                new SqlParameter("@LoginID",LoginId),
                new SqlParameter("@ToDate",ToDate),
            };
            DataSet ds = Connection.ExecuteQuery("GetCustomerListForApprove", para);
            return ds;
        }

        public DataSet ApprovedCustomer()
        {

            SqlParameter[] para = {
                                    new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@CustomerID", CustomerId),
                                        new SqlParameter("@ApprovedBy", AddedBy)
                                     };
            DataSet ds = Connection.ExecuteQuery("ApprovedCustomer", para);
            return ds;
        }
        public DataSet RejectCustomer()
        {

            SqlParameter[] para = {

                                     new SqlParameter("@LoginId",LoginId),
                                      new SqlParameter("@CustomerID", CustomerId),
                                        new SqlParameter("@ApprovedBy", AddedBy)
                                     };
            DataSet ds = Connection.ExecuteQuery("RejectCustomer", para);
            return ds;
        }
        public DataSet GetAllRentalBenefitList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@Status",Status),
                new SqlParameter("@LoginID",LoginId),
                new SqlParameter("@ToDate",ToDate),
            };
            DataSet ds = Connection.ExecuteQuery("GetAllRentalBenefitList", para);
            return ds;
        }
        public DataSet Getlistpayout()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@LoginID",LoginId),
                new SqlParameter("@ToDate",ToDate),
            };
            DataSet ds = Connection.ExecuteQuery("GetPayoutListForPaid", para);
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
        #region PayPayout
        public DataSet GetPayPayout()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@IsDownline", IsDownline),
                                    new SqlParameter("@Leg", Leg), };
            DataSet ds = Connection.ExecuteQuery("GetBalancePayoutforPayment", para);
            return ds;
        }

        public DataSet SavePayPayout()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId),
                                    new SqlParameter("@TransactionNo", TransactionNo),
                                    new SqlParameter("@TransactionDate", TransactionDate),
                                    new SqlParameter("@Amount", Amount),
                                    new SqlParameter("@AddedBy", AddedBy), };
            DataSet ds = Connection.ExecuteQuery("PayPayout", para);
            return ds;
        }

        public DataSet SaveBenefitNew()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PaymentDate", PaymentDate),
                                      new SqlParameter("@InstallmentNo", InstallmentNo),
                                       new SqlParameter("@PaymentMode", PaymentMode),
                                    new SqlParameter("@TransactionNo", TransactionNo),
                                    new SqlParameter("@TransactionDate", TransactionDate),
                                    new SqlParameter("@BankName", BankName),
                                    new SqlParameter("@BankBranch", BankBranch)};


            DataSet ds = Connection.ExecuteQuery("PayPayoutNew", para);
            return ds;
        }
        #endregion


        public DataSet GetActualLeftRightBusinessList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@FromDate", FromDate),
                                       new SqlParameter("@ToDate", ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetDetailsActualLeftRightBussness", para);
            return ds;
        }
        
    }
}


