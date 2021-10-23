using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AsterHeight.Models
{
    public class Common
    {
        private string BookingNo { get; set; }
        public List<SelectListItem> ddlType { get; set; }
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ReferBy { get; set; }
        public string Result { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string DisplayName { get; set; }
     public string AssociateLoginID { get; set; }
        public string AddedOn { get; set; }
        public string FK_DesignationId { get; set; }
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string Url { get; set; }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;
            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];
                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }
            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }
  
        public static List<SelectListItem> BindDdlRate()
        {
            List<SelectListItem> ddlRate = new List<SelectListItem>();
            ddlRate.Add(new SelectListItem { Text = "Select Rate", Value = null });
            //ddlRate.Add(new SelectListItem { Text = "N/A", Value = "N/A" });
            //ddlRate.Add(new SelectListItem { Text = "201/- per	sq	ft", Value = "201" });
            ddlRate.Add(new SelectListItem { Text = "151/- per	sq	ft", Value = "151" });
            ddlRate.Add(new SelectListItem { Text = "251/- per	sq	ft", Value = "251" });
            ddlRate.Add(new SelectListItem { Text = "301/- per	sq	ft", Value = "301" });
            ddlRate.Add(new SelectListItem { Text = "401/- per	sq	ft", Value = "401" });
            //ddlRate.Add(new SelectListItem { Text = "501/- per	sq	ft", Value = "501" });
            return ddlRate;
        }
        public static List<SelectListItem> BookingType()
        {
            List<SelectListItem> BookingType = new List<SelectListItem>();
            BookingType.Add(new SelectListItem { Text = "Select Booking Type", Value = null });
            BookingType.Add(new SelectListItem { Text = "EMI", Value = "Lucky Draw/EMI" });
            BookingType.Add(new SelectListItem { Text = "Full Payment", Value = "Full Payment" });
            BookingType.Add(new SelectListItem { Text = "Rental	Plan", Value = "Rental Plan" });
            //BookingType.Add(new SelectListItem { Text = "Associate", Value = "Associate" });
            return BookingType;
        }
        public static List<SelectListItem> PLC()
        {
            List<SelectListItem> ddlPLC = new List<SelectListItem>();
            ddlPLC.Add(new SelectListItem { Text = "N/A", Value = "0" });
            ddlPLC.Add(new SelectListItem { Text = "2 %", Value = "2" });
            ddlPLC.Add(new SelectListItem { Text = "3 %", Value = "3" });
            ddlPLC.Add(new SelectListItem { Text = "5 %", Value = "5" });
            return ddlPLC;
        }
        public static List<SelectListItem> Status()
        {
            List<SelectListItem> Status = new List<SelectListItem>();
            Status.Add(new SelectListItem { Text = "All", Value = "" });
            Status.Add(new SelectListItem { Text = "Paid", Value = "1" });
            Status.Add(new SelectListItem { Text = "Unpaid", Value = "0" });

            return Status;
        }

        public DataSet GetPaymentMode()
        {

            DataSet ds = Connection.ExecuteQuery("GetPaymentModeList");
            return ds;
        }
        public static List<SelectListItem> CustomerBindDdlRate()
        {
            List<SelectListItem> ddlRate = new List<SelectListItem>();
            ddlRate.Add(new SelectListItem { Text = "Select Rate", Value = null });
            ddlRate.Add(new SelectListItem { Text = "N/A", Value = "N/A" });
            ddlRate.Add(new SelectListItem { Text = "251/- per	sq	ft", Value = "251" });
            ddlRate.Add(new SelectListItem { Text = "301/- per	sq	ft", Value = "301" });
            ddlRate.Add(new SelectListItem { Text = "501/- per	sq	ft", Value = "501" });
            return ddlRate;
        }

        public static List<SelectListItem> BindAmount()
        {
            List<SelectListItem> BookingType = new List<SelectListItem>();
            BookingType.Add(new SelectListItem { Text = "1000", Value = "1000" });
            BookingType.Add(new SelectListItem { Text = "21000", Value = "21000" });

            return BookingType;
        }
        public static List<SelectListItem> AssociateBenefit()
        {
            List<SelectListItem> AssociateBenefit = new List<SelectListItem>();
            AssociateBenefit.Add(new SelectListItem { Text = "Select", Value = "0" });
            AssociateBenefit.Add(new SelectListItem { Text = "One Time", Value = "One Time" });
            AssociateBenefit.Add(new SelectListItem { Text = "Rental", Value = "Rental" });
            return AssociateBenefit;
        }
        public static List<SelectListItem> CustomerBenefit()
        {
            List<SelectListItem> CustomerBenefit = new List<SelectListItem>();
            CustomerBenefit.Add(new SelectListItem { Text = "Select", Value = "0" });
            CustomerBenefit.Add(new SelectListItem { Text = "Buy Back", Value = "Buy Back" });
            CustomerBenefit.Add(new SelectListItem { Text = "PlotBuy", Value = "Plot Buy" });
            return CustomerBenefit;
        }
        public static List<SelectListItem> BindDdlType()
        {
            List<SelectListItem> ddlType = new List<SelectListItem>();
            ddlType.Add(new SelectListItem { Text = "Select Type", Value = null });
            //ddlType.Add(new SelectListItem { Text = "N/A", Value = "N/A" });
            //ddlType.Add(new SelectListItem { Text = "21000/-", Value = "21000" });
            ddlType.Add(new SelectListItem { Text = "26000/-", Value = "26000" });
            ddlType.Add(new SelectListItem { Text = "31000/-", Value = "31000" });
            ddlType.Add(new SelectListItem { Text = "62000/-", Value = "62000" });
            return ddlType;
        }

        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;
        }
        public static List<SelectListItem> Leg()
        {
            List<SelectListItem> Leg = new List<SelectListItem>();
            Leg.Add(new SelectListItem { Text = "All", Value = null });
            Leg.Add(new SelectListItem { Text = "Left", Value = "L" });
            Leg.Add(new SelectListItem { Text = "Right", Value = "R" });

            return Leg;
        }

        public static List<SelectListItem> AssociateStatus()
        {
            List<SelectListItem> AssociateStatus = new List<SelectListItem>();
            AssociateStatus.Add(new SelectListItem { Text = "All", Value = null });
            AssociateStatus.Add(new SelectListItem { Text = "Active", Value = "P" });
            AssociateStatus.Add(new SelectListItem { Text = "Inactive", Value = "T" });

            return AssociateStatus;
        }
        public static List<SelectListItem> BindGender()
        {
            List<SelectListItem> Gender = new List<SelectListItem>();
            Gender.Add(new SelectListItem { Text = "Male", Value = "M" });
            Gender.Add(new SelectListItem { Text = "Female", Value = "F" });

            return Gender;
        }
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetMemberName", para);

            return ds;
        }
        public DataSet GetCustomerDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetCustomerName", para);

            return ds;
        }
        public DataSet GetTradMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetTradMemberName", para);

            return ds;
        }
        public DataSet BindProduct()
        {

            DataSet ds = Connection.ExecuteQuery("GetProductList");
            return ds;
        }
        public DataSet BindDesignation(string FK_DesignationId)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_DesignationId", FK_DesignationId),

                                  };
            DataSet ds = Connection.ExecuteQuery("GetDesignation");
            return ds;
        }

        public DataSet GetStateCity()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", Pincode) };
            DataSet ds = Connection.ExecuteQuery("GetStateCity", para);
            return ds;
        }

        public static List<SelectListItem> BindPaymentMode()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            PaymentMode.Add(new SelectListItem { Text = "Cheque", Value = "Cheque" });
            PaymentMode.Add(new SelectListItem { Text = "NEFT", Value = "NEFT" });
            PaymentMode.Add(new SelectListItem { Text = "RTGS", Value = "RTGS" });
            PaymentMode.Add(new SelectListItem { Text = "Demand Draft", Value = "DD" });
            return PaymentMode;
        }
        public static List<SelectListItem> BindPasswordType()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            PasswordType.Add(new SelectListItem { Text = "Select", Value = "0" });
            PasswordType.Add(new SelectListItem { Text = "Profile Password", Value = "P" });
            PasswordType.Add(new SelectListItem { Text = "Transaction Password", Value = "T" });

            return PasswordType;
        }
        public static List<SelectListItem> TransactionType()
        {
            List<SelectListItem> TransactionType = new List<SelectListItem>();
            TransactionType.Add(new SelectListItem { Text = "Select", Value = "0" });
            TransactionType.Add(new SelectListItem { Text = "Credit", Value = "Credit" });
            TransactionType.Add(new SelectListItem { Text = "Debit", Value = "Debit" });

            return TransactionType;
        }
        public static List<SelectListItem> BindKYCStatus()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            PasswordType.Add(new SelectListItem { Text = "Select", Value = "0" });
            PasswordType.Add(new SelectListItem { Text = "Not Uploaded", Value = "N" });
            PasswordType.Add(new SelectListItem { Text = "Pending", Value = "P" });
            PasswordType.Add(new SelectListItem { Text = "Approved", Value = "A" });

            return PasswordType;
        }

        public static List<SelectListItem> AttendanceStatus()
        {
            List<SelectListItem> AttendType = new List<SelectListItem>();
            AttendType.Add(new SelectListItem { Text = "Select", Value = "0" });
             AttendType.Add(new SelectListItem { Text = "Present", Value = "P" });
            AttendType.Add(new SelectListItem { Text = "Absent", Value = "A" });

            return AttendType;
        }

        public string Fk_UserId { get; set; }


        public static List<SelectListItem> BindPaymentStatus()
        {
            List<SelectListItem> PaymentStatus = new List<SelectListItem>();
            PaymentStatus.Add(new SelectListItem { Text = "All", Value = null });
            PaymentStatus.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            PaymentStatus.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            PaymentStatus.Add(new SelectListItem { Text = "Rejected", Value = "Rejected" });

            return PaymentStatus;
        }

        public static List<SelectListItem> RequestStatus()
        {
            List<SelectListItem> RequestStatus = new List<SelectListItem>();
            RequestStatus.Add(new SelectListItem { Text = "All", Value = null });
            RequestStatus.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            RequestStatus.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            RequestStatus.Add(new SelectListItem { Text = "Declined", Value = "Declined" });

            return RequestStatus;
        }
        public static List<SelectListItem> NewsForList()
        {
            List<SelectListItem> NewsFor = new List<SelectListItem>();
            NewsFor.Add(new SelectListItem { Text = "Select", Value = null });
            NewsFor.Add(new SelectListItem { Text = "Associate", Value = "Associate" });
            NewsFor.Add(new SelectListItem { Text = "Customer", Value = "Customer" });
           

            return NewsFor;
        }
        public DataSet FormPermissions(string FormName, string AdminId)
        {
            try
            {
                SqlParameter[] para = {
                                          new SqlParameter("@FormName", FormName) ,
                                          new SqlParameter("@AdminId", AdminId)
                                      };

                DataSet ds = Connection.ExecuteQuery("PermissionsOfForm", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BindFormMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormMasterManage", para);

            return ds;

        }
        public DataSet BindFormTypeMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = Connection.ExecuteQuery("FormTypeMasterManage", para);

            return ds;

        }
        public DataSet GetBookingNo(string BookingNo)
        {
            SqlParameter[] para = {
                                      new SqlParameter("@BookingNo", BookingNo),
                                  };
            DataSet ds = Connection.ExecuteQuery("GetBookingNo", para);
            return ds;
        }


    }
    public class SMSCredential
    {
        public static string UserName = "";
        public static string Password = "";
        public static string SenderId = "";
    }
    public class SoftwareDetails
    {
        public static  string CompanyName = "Moonlite Agrofarm Real Estate";
        public static string CompanyName1 = "Blossom City";
        public static string CompanyAddress = "Flat No. T5-CU-03 Parsvnath Planet, Tower No.5, Vibhuti Khand, Gomti Nagar, Lucknow";
        public static string Pin1 = "226010";
        public static string State1="UP";
        public static string City1="Lucknow";
        public static string ContactNo= "1800 102 5898";
        public static string LandLine = "+91 9120 760 925"; 
        public static string ContactNo2 = "8052949381";
        public static string Website= "blossom.AsterHeightgroups.org";
        public static string EmailID= "info@AsterHeightgroups.org";
    }
}