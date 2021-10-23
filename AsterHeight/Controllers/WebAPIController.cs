using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsterHeight.Models;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Web.Http;

namespace AsterHeight.Controllers
{
    public class WebAPIController : Controller
    {
        #region AssociateLogin
        public ActionResult Login(LoginAPI objParameters)
        {
            LoginAPI obj = new LoginAPI();
            if (objParameters.LoginId == "" || objParameters.LoginId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Password == "" || objParameters.Password == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Password";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.Password = Crypto.Encrypt(objParameters.Password);
                DataSet dsResult = objParameters.LoginAction();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1" && dsResult.Tables[0].Rows[0]["UserType"].ToString() == "MLM Associate")
                    {
                        obj.Status = "0";
                        obj.SuccessMessage = "Successfully logged in!";
                        obj.Pk_userId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                        obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.UserType = dsResult.Tables[0].Rows[0]["UserType"].ToString();
                        obj.FullName = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                        obj.ProfilePic = dsResult.Tables[0].Rows[0]["ProfilePic"].ToString();
                        obj.StatusColor = dsResult.Tables[0].Rows[0]["StatusColor"].ToString();
                    }
                    else if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1" && dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Customer")
                    {
                        obj.Status = "0";
                        obj.SuccessMessage = "Successfully logged in!";
                        obj.Pk_userId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                        obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.UserType = dsResult.Tables[0].Rows[0]["UserType"].ToString();
                        obj.FullName = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                        obj.ProfilePic = dsResult.Tables[0].Rows[0]["ProfilePic"].ToString();
                        obj.StatusColor = dsResult.Tables[0].Rows[0]["StatusColor"].ToString();
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion AssociateLogin
        #region SignUp
        public ActionResult AssociateSignup(AssociateSignup objParameters)
        {
            ResponseRegistration obj = new ResponseRegistration();

            if (objParameters.UserID == "" || objParameters.UserID == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Select User";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            //if (objParameters.DesignationID == "" || objParameters.DesignationID == null)
            //{
            //    obj.Status = "1";
            //    obj.ErrorMessage = "Please Select Designation";
            //    return Json(obj, JsonRequestBehavior.AllowGet);
            //}
            if (objParameters.FirstName == "" || objParameters.FirstName == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter First Name";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.LastName == "" || objParameters.LastName == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Last Name";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.Contact == "" || objParameters.Contact == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Contact";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                Random rnd = new Random();
                objParameters.BranchID = "1";
                string pass = rnd.Next(111111, 999999).ToString();
                objParameters.Password = Crypto.Encrypt(pass);
                DataSet dsResult = obj.AssociateRegistration(objParameters);
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.Password = Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());
                        obj.Name = dsResult.Tables[0].Rows[0]["Name"].ToString();
                        obj.Pk_userId = dsResult.Tables[0].Rows[0]["PKUserID"].ToString();
                        obj.SuccessMessage = "Registration Successful !";
                        if (objParameters.Contact != null)
                        {
                            try
                            {
                                /* mailbody = "Dear  " + dsResult.Tables[0].Rows[0]["Name"].ToString() + ",You have been successfully registered as Dolphin Associate.Given below are your login details .!<br/>  <b>Login ID</b> :  " + dsResult.Tables[0].Rows[0]["LoginId"].ToString() + "<br/> <b>Passoword</b>  : " + Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());

                                 System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                                 {
                                     Host = "smtp.gmail.com",
                                     Port = 587,
                                     EnableSsl = true,
                                     DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                     UseDefaultCredentials = false,
                                     Credentials = new NetworkCredential("abc@gmail.com", "awneesh1")

                                 };

                                 using (var message = new MailMessage("abc@gmail.com", objParameters.Email)
                                 {
                                     IsBodyHtml = true,
                                     Subject = "Associate Registration",
                                     Body = mailbody
                                 })
                                     smtp.Send(message);*/
                                string str2 = "Dear " + dsResult.Tables[0].Rows[0]["Name"].ToString() + ", Your Login Id is " + dsResult.Tables[0].Rows[0]["LoginId"].ToString() + " and password is " + Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());
                                BLSMS.SendSMSNew(objParameters.Contact, str2);
                            }
                            catch (Exception ex)
                            {
                                throw;
                            }
                        }
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Problem in connection.Please try after some times.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion SignUp
        #region ChangePassword
        public ActionResult ChangePassword(ChangePassword objParameters)
        {
            ChangePassword obj = new ChangePassword();
            if (objParameters.Pk_userId == "" || objParameters.Pk_userId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter User Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (objParameters.OldPassword == "" || objParameters.OldPassword == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter OldPassword";
                return Json(obj, JsonRequestBehavior.AllowGet);

            }
            if (objParameters.NewPassword == "" || objParameters.NewPassword == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter NewPassword";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.OldPassword = Crypto.Encrypt(objParameters.OldPassword);
                objParameters.NewPassword = Crypto.Encrypt(objParameters.NewPassword);
                DataSet dsResult = objParameters.ChangePasswordAssociate();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.SuccessMessage = "Password Changed Successfuly.";

                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Problem In connection";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion ChangePassword
        #region ForgetPassword
        public ActionResult ForgetPass(ForgetPass objParameters)
        {
            ForgetPass obj = new ForgetPass();
            if (objParameters.Contact == "" || objParameters.Contact == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Mobile No.";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }

            try
            {
                DataSet dsResult = objParameters.ForgetPassword();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else
                    {
                        obj.Status = "0";
                        string passwordRecoveryMessage = "Dear " + dsResult.Tables[0].Rows[0]["FirstName"].ToString() + ", Your Password is " + Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());
                        BLSMS.SendSMSNew(dsResult.Tables[0].Rows[0]["Mobile"].ToString(), passwordRecoveryMessage);
                        obj.SuccessMessage = "Password is sent on your registered mobile no.";
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion ForgetPassword

        #region AssociateDashBoard
        public ActionResult AssociateDashBoard(AssociateDashboard newdata)
        {
            AssociateDashboard obj = new AssociateDashboard();
            try
            {
                List<DueInstallment> objdueinstallment = new List<DueInstallment>();
                List<NewsDetails> lst = new List<NewsDetails>();
                List<Associate> lstAssociate = new List<Associate>();
                DataSet ds = newdata.GetAssociateForDashboard();
                if (ds != null && ds.Tables.Count > 0)
                {
                    obj.Status = "0";
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region DueInstallment
                        foreach (DataRow r in (ds.Tables[0].Rows))
                        {
                            DueInstallment obj1 = new DueInstallment();
                            obj1.CustomerID = r["PK_UserId"].ToString();
                            obj1.CustomerLoginID = r["LoginId"].ToString();
                            obj1.CustomerName = r["FirstName"].ToString();
                            obj1.PlotNumber = r["PlotInfo"].ToString();
                            obj1.InstallmentNo = r["InstallmentNo"].ToString();
                            obj1.InstallmentAmount = r["InstAmt"].ToString();
                            objdueinstallment.Add(obj1);
                        }
                        obj.lstdueinstallment = objdueinstallment;
                        #endregion DueInstallment
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        #region NewsDetails
                        foreach (DataRow r in (ds.Tables[1].Rows))
                        {
                            NewsDetails obj1 = new NewsDetails();
                            obj1.PK_NewsID = r["PK_NewsID"].ToString();
                            obj1.NewsHeading = r["NewsHeading"].ToString();
                            obj1.NewsBody = r["NewsBody"].ToString();
                            lst.Add(obj1);
                        }
                        obj.lstnewsdetail = lst;
                        #endregion NewsDetails
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        #region Associate
                        foreach (DataRow r in (ds.Tables[2].Rows))
                        {
                            Associate obj1 = new Associate();
                            obj1.TotalActiveId = r["TotalActive"].ToString();
                            obj1.TotalDirect = r["TotalDirect"].ToString();
                            obj1.TotalDownline = r["TotalDownline"].ToString();
                            obj1.PayoutWalletBalance = r["PayoutWalletBalance"].ToString();
                            obj1.TotalPayout = r["TotalPayout"].ToString();
                            obj1.TotalPaidPayout = r["TotalPaidPayout"].ToString();
                            obj1.TotalDeduction = r["TotalDeduction"].ToString();
                            obj1.TotalInActive = r["TotalInActive"].ToString();
                            obj1.TotalLuckyDraw = r["TotalLuckyDraw"].ToString();
                            obj1.TotalLuckyDrawRequest = r["TotalLuckyDrawRequest"].ToString();
                            obj1.LeftApprovedBusiness = r["LeftApprovedBusiness"].ToString();
                            obj1.RightApprovedBusiness = r["RightApprovedBusiness"].ToString();

                            lstAssociate.Add(obj1);
                        }
                        obj.PaidBusinessLeft = ds.Tables[3].Rows[0]["PaidBusinessLeft"].ToString();
                        obj.PaidBusinessRight = ds.Tables[3].Rows[0]["PaidBusinessRight"].ToString();
                        obj.TotalBusinessLeft = ds.Tables[3].Rows[0]["TotalBusinessLeft"].ToString();
                        obj.TotalBusinessRight = ds.Tables[3].Rows[0]["TotalBusinessRight"].ToString();
                        obj.CarryLeft = ds.Tables[3].Rows[0]["CarryLeft"].ToString();
                        obj.CarryRight = ds.Tables[3].Rows[0]["CarryRight"].ToString();
                        obj.lstassociate = lstAssociate;
                        #endregion Associate
                    }
                    else
                    {
                        obj.Status = "1";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetNewsDetailsForAssociateDashBoard(AssociateDashboard newdata)
        {
            AssociateDashboard obj = new AssociateDashboard();
            try
            {
                List<NewsDetails> lst = new List<NewsDetails>();
                DataSet ds = newdata.GetAssociateForDashboard();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        obj.Status = "0";
                        #region NewsDetails
                        foreach (DataRow r in (ds.Tables[1].Rows))
                        {
                            NewsDetails obj1 = new NewsDetails();
                            obj1.PK_NewsID = r["PK_NewsID"].ToString();
                            obj1.NewsHeading = r["NewsHeading"].ToString();
                            obj1.NewsBody = r["NewsBody"].ToString();
                            lst.Add(obj1);
                        }
                        obj.lstnewsdetail = lst;
                        #endregion NewsDetails
                    }
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    obj.Status = "1";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetTotalForAssociateDashBoard(AssociateDashboard newdata)
        //{
        //    AssociateDashboard obj = new AssociateDashboard();
        //    try
        //    {
        //        List<Associate> lst = new List<Associate>();
        //        DataSet ds = newdata.GetAssociateForDashboard();
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[2].Rows.Count > 0)
        //            {
        //                obj.Status = "0";
        //                #region Associate
        //                foreach (DataRow r in (ds.Tables[2].Rows))
        //                {
        //                    Associate obj1 = new Associate();
        //                    obj1.TotalAssociate = r["TotalAssociate"].ToString();
        //                    obj1.TotalBusiness = r["TotalBusiness"].ToString();
        //                    obj1.TotalActiveId = r["TotalActiveId"].ToString();
        //                    obj1.SelfBusiness = r["SelfBusiness"].ToString();
        //                    obj1.TeamBusiness = r["TeamBusiness"].ToString();
        //                    obj1.TotalBooking = r["TotalBooking"].ToString();
        //                    obj1.TeamBooking = r["TeamBooking"].ToString();
        //                    obj1.SelfBooking = r["SelfBooking"].ToString();
        //                    obj1.Totalregistry = r["Totalregistry"].ToString();
        //                    obj1.SelfRegistry = r["SelfRegistry"].ToString();
        //                    obj1.TeamRegistry = r["TeamRegistry"].ToString();
        //                    lst.Add(obj1);
        //                }
        //                obj.lstassociate = lst;
        //                #endregion NewsDetails
        //            }
        //            return Json(obj, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            obj.Status = "1";
        //            return Json(obj, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.Status = "1";
        //        obj.ErrorMessage = ex.Message;
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //}

        #endregion AssociateDashBoard



        #region GraphDetails
        public ActionResult GetGraphDetailsOfPlot(Graph objParameters)
        {
            Graph newdata = new Graph();
            try
            {
                List<GraphData> datalist = new List<GraphData>();
                DataSet ds = new DataSet();
                ds = objParameters.BindGraphDetails();
                if (ds.Tables.Count > 0)
                {
                    newdata.Status = "0";
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        newdata.lstdata = datalist;
                    }
                    List<GraphDetails> objgraphdetails = new List<GraphDetails>();
                    foreach (DataRow r in (ds.Tables[0].Rows))
                    {
                        objgraphdetails.Add(new GraphDetails
                        {
                            Total = r["Total"].ToString(),
                            Status = r["Status"].ToString()
                        });
                    }
                    datalist.Add(new GraphData
                    {
                        Title = "Graph Details",
                        lstgraphdetails = objgraphdetails
                    });

                }
                else
                {
                    newdata.Status = "1";
                    newdata.ErrorMessage = "No Record Found!";
                }
                return Json(newdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                newdata.ErrorMessage = ex.Message;
                return Json(newdata, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                GetViaPin model = new GetViaPin();
                model.Pincode = Pincode;

                #region GetStateCity
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State = model.City = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult GetSite(Site model)
        {
            List<SiteList> lst = new List<SiteList>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    SiteList obj = new SiteList();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.PK_SiteID = r["PK_SiteID"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstsite = lst;
            }
            else
            {
                model.Status = "1";
                model.ErrorMessage = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSiteType(SiteType model)
        {
            List<SiteTypeList> lst = new List<SiteTypeList>();
            DataSet ds = model.GetSiteTypeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    SiteTypeList obj = new SiteTypeList();
                    obj.SiteTypeName = r["SiteTypeName"].ToString();
                    obj.PK_SiteTypeID = r["PK_SiteTypeID"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstsitetype = lst;
            }
            else
            {
                model.Status = "1";
                model.ErrorMessage = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #region ddlSector
        public ActionResult GetSector(Sector model)
        {
            List<SectorList> lst = new List<SectorList>();
            DataSet ds = model.GetSectorList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    SectorList obj = new SectorList();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.PK_SectorID = r["PK_SectorID"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstsite = lst;
            }
            else
            {
                model.Status = "1";
                model.ErrorMessage = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ddlBlock
        public ActionResult GetBlock(Block model)
        {
            List<BlockList> lst = new List<BlockList>();
            DataSet ds = model.GetBlockList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    BlockList obj = new BlockList();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PK_BlockID = r["PK_BlockID"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstBlock = lst;
            }
            else
            {
                model.Status = "1";
                model.ErrorMessage = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult BookingList(BookingList model)
        {
            List<BookingListDetails> lst = new List<BookingListDetails>();
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
                    BookingListDetails obj = new BookingListDetails();
                    obj.BookingStatus = r["BookingStatus"].ToString();
                    obj.PK_BookingId = r["PK_BookingId"].ToString();
                    obj.BranchID = r["BranchID"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.CustomerID = r["CustomerID"].ToString();
                    obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateID"].ToString();
                    obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.BookingAmount = r["BookingAmt"].ToString();
                    obj.PaymentPlanID = r["PlanName"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.Discount = r["Discount"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.NetPlotAmount = r["NetPlotAmount"].ToString();
                    obj.PK_PLCCharge = r["PLCCharge"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    lst.Add(obj);
                }
                model.lstbooking = lst;
                model.Status = "0";
                model.Message = "Record Found.";
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found.";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CustomerLedgerReport(string PK_BookingId)

        {
            LedgerReport model = new LedgerReport();
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
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(string BookingNumber, string LoginId, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {
            LedgerReport model = new LedgerReport();
            List<PaymentDetails> lst = new List<PaymentDetails>();
            model.LoginId = LoginId;
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
                    PaymentDetails obj = new PaymentDetails();

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
                model.lstpayment = lst;
                model.Status = "0";
                model.Message = "Record Found !";
            }
            else
            {
                model.Status = "1";
                model.Message = "No record found !";

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
        #region AssociateDownline

        public ActionResult AssociateDownlineDetail(Downline model)
        {

            List<DownlineDetails> lst = new List<DownlineDetails>();
            DataSet ds = model.GetDownlineList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DownlineDetails obj = new DownlineDetails();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.SponsorId = r["SponsorLoginId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                model.lstdownline = lst;
                model.Message = "Record Found !";
                model.Status = "0";
            }
            else
            {
                model.Message = "No Record Found !";
                model.Status = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        #endregion 

        #region AssociateUpline

        public ActionResult AssociateUplineDetail(Downline model)
        {

            List<DownlineDetails> lst = new List<DownlineDetails>();

            DataSet ds = model.GetDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DownlineDetails obj = new DownlineDetails();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstdownline = lst;
                model.Status = "0";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Tree
        public ActionResult Tree1(TreeList model)
        {

            List<TreeDetails> lst = new List<TreeDetails>();

            DataSet ds = model.GetTreeDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TreeDetails obj = new TreeDetails();
                    obj.Fk_ParentId = r["Parentid"].ToString();
                    obj.DisplayName = r["MemberName"].ToString();
                    obj.Fk_UserId = r["PK_UserId"].ToString();
                    lst.Add(obj);
                }
                model.lsttree = lst;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Tree
        public ActionResult AssociateTree(DownlineTree model, string AssociateID)
        {
            if (AssociateID != null)
            {
                model.Fk_UserId = AssociateID;
            }
            List<DownlineTreeDetails> lst = new List<DownlineTreeDetails>();
            DataSet ds = model.GetDownlineTree();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DownlineTreeDetails obj = new DownlineTreeDetails();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ActiveStatus = r["ActiveStatus"].ToString();
                    lst.Add(obj);
                }
                model.lstdownline = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region UserReward
        public ActionResult UserReward(Reward model)
        {
            model.RewardID = "1";
            List<RewardData> lst = new List<RewardData>();
            DataSet ds = model.RewardList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    RewardData obj = new RewardData();

                    obj.Status = r["Status"].ToString();
                    obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Contact = r["BackColor"].ToString();
                    obj.PK_RewardItemId = r["PK_RewardItemId"].ToString();
                    lst.Add(obj);
                }
                model.lstreward = lst;
                model.Status = "0";
                model.ErrorMessage = "Record Found!";
            }
            else
            {
                model.Status = "1";
                model.ErrorMessage = "No Record Found!";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClaimReward(RewardClaim obj)
        {
            try
            {
                obj.Status = "Claim";
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.SuccessMessage = "Reward Claimed";
                    }
                    else
                    {
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                obj.ErrorMessage = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SkipReward(RewardClaim obj)
        {
            try
            {
                obj.Status = "Skip";
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.SuccessMessage = "Reward Skipped";
                    }
                    else
                    {
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                obj.ErrorMessage = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region UnpaidIncomeReport
        public ActionResult UnpaidIncome(UnpaidIncome model)
        {
            List<UnpaidIncomeDetails> lst = new List<UnpaidIncomeDetails>();
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UnpaidIncomeDetails obj = new UnpaidIncomeDetails();
                    obj.FromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();
                    lst.Add(obj);
                }
                model.lstunpaid = lst;
                model.Message = "Record Found !";
                model.Status = "0";
            }
            else
            {
                model.Message = "No Record Found !";
                model.Status = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PayoutLedger
        public ActionResult PayoutRequest(PayoutBalance model)
        {
            model.Status = "0";
            DataSet Ds = model.GetPayoutBalance();
            model.Balance = Ds.Tables[0].Rows[0]["Balance"].ToString();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SavePayoutRequest(PayoutRequest obj)
        {
            try
            {
                DataSet ds = obj.SavePayoutRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[1].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.SuccessMessage = " Payout requested successfully !";
                        try
                        {
                            string mob = ds.Tables[0].Rows[0]["Column1"].ToString();
                            string name = ds.Tables[0].Rows[0]["Column3"].ToString();
                            BLSMS.SendSMS(mob, "Dear " + name + ", Your request of Payout Request of Rs " + ds.Tables[0].Rows[0]["Column2"].ToString() + " has been processed Successfully. Your amount will credited in your account within 1 to 5 working days.");
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.ErrorMessage = ex.Message;
            }

            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PayoutDetails(Payout model)
        {
            List<PayoutDetailData> lst = new List<PayoutDetailData>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PayoutDetailData obj = new PayoutDetailData();
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
                model.lstpayout = lst;
                model.Status = "0";
            }
            else
            {
                model.Status = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PayoutLedger(PayoutLedgerData model)
        {
            List<PayoutLedgerDetail> lst = new List<PayoutLedgerDetail>();
            DataSet ds = model.PayoutLedger();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PayoutLedgerDetail obj = new PayoutLedgerDetail();
                    obj.Narration = r["Narration"].ToString();
                    obj.DrAmount = r["DrAMount"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.AddedOn = r["TransactionDate"].ToString();
                    obj.PayoutBalance = r["Balance"].ToString();


                    lst.Add(obj);
                }
                model.lstpayoutledger = lst;
                model.Status = "0";
            }
            else
            {
                model.Status = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region PayoutRequestReport
        public ActionResult PayoutRequestReport(PayoutRequestReports model)
        {
            model.Status = model.Status == "0" ? null : model.Status;
            List<PayoutRequestReportData> lst = new List<PayoutRequestReportData>();
            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PayoutRequestReportData obj = new PayoutRequestReportData();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();

                    lst.Add(obj);
                }
                model.lstpayout = lst;
                model.Message = "Record Found !";
            }
            else
            {
                model.Message = "No Record Found !";
            }
            #region ddl 
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion  

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region KYC

        public ActionResult KYCDocuments(KYCDocuments model)
        {
            KYCDocuments obj = new KYCDocuments();
            DataSet ds = model.GetKYCDocuments();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found!";
                obj.AdharNumber = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.AdharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.AdharStatus = "Status : " + ds.Tables[0].Rows[0]["AdharStatus"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                obj.PanStatus = "Status : " + ds.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.DocumentNumber = ds.Tables[0].Rows[0]["DocumentNumber"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                obj.DocumentStatus = "Status : " + ds.Tables[0].Rows[0]["DocumentStatus"].ToString();
            }
            else
            {
                obj.Status = "1";
                obj.Message = "Record Not Found!";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        public ActionResult KYCDocuments2(KYCDocument obj, IEnumerable<HttpPostedFileBase> postedFile)
        {
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        //E:\BitBucket\DolphinZone\Dolphin\files\assets\images\

                        obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));


                    }

                }
                obj.ActionStatus = "Pan";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.SuccessMessage = "Document uploaded successfully..";

                    }
                    else
                    {
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                obj.ErrorMessage = ex.Message;

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult KYCDocuments3(KYCDocument obj, IEnumerable<HttpPostedFileBase> postedFile)
        {
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        //E:\BitBucket\DolphinZone\Dolphin\files\assets\images\

                        obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));


                    }

                }
                obj.ActionStatus = "Doc";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.SuccessMessage = "Document uploaded successfully..";

                    }
                    else
                    {
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                obj.ErrorMessage = ex.Message;

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult EnquiryList(Enquiry model)
        {

            List<EnquiryList> lst = new List<EnquiryList>();

            DataSet ds = model.EnquiryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    EnquiryList obj = new EnquiryList();
                    obj.EnquiryID = r["PK_EnquiryMasterId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Details = r["Details"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstBlock1 = lst;
            }
            else
            {
                model.Status = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaveEnquiry(EnquiryData obj)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = obj.SaveEnquiry();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.SuccessMessage = "Enquiry saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                obj.ErrorMessage = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlotAvailability(PlotDetails model)
        {
            List<PlotList> lst = new List<PlotList>();
            DataSet ds = model.GetPlotDetails();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                model.SiteID = ds.Tables[0].Rows[0]["FK_SiteID"].ToString();
                model.SectorID = ds.Tables[0].Rows[0]["FK_SectorID"].ToString();
                model.BlockID = ds.Tables[0].Rows[0]["FK_BlockID"].ToString();
                model.PlotNumber = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PlotList obj = new PlotList();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.PlotStatus = r["Status"].ToString();
                    obj.ColorCSS = r["ColorCSS"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
                model.Message = "Record Found !";
                model.Status = "0";
            }
            else
            {
                model.Message = "No Record Found !";
                model.Status = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSitesDetails(SitePLCCharge model)
        {
            try
            {
                #region GetSiteRate
                DataSet dsSiteRate = model.GetSiteList();
                if (dsSiteRate != null)
                {
                    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                    model.Result = "yes";
                }
                #endregion

                #region SitePLCCharge
                List<PLCChargeList> lst = new List<PLCChargeList>();
                DataSet dsPlcCharge = model.GetPLCChargeList();

                if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                {
                    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                    {
                        PLCChargeList obj = new PLCChargeList();
                        obj.SiteName = r["SiteName"].ToString();
                        obj.PLCName = r["PLCName"].ToString();
                        obj.PLCCharge = r["PLCCharge"].ToString();
                        obj.PLCID = r["PK_PLCID"].ToString();

                        lst.Add(obj);
                    }
                    model.lstPlcCharge = lst;
                }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewProfile(ProfileMobile model)
        {
            try
            {
                DataSet dsPlotDetails = model.GetList();
                if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                {
                    model.Status = "0";
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

                    model.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    model.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                    model.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                    model.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                }
                else
                {
                    model.Status = "1";
                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateProfile(HttpPostedFileBase postedFile, ProfileMobile model)
        {
            try
            {
                if (postedFile != null)
                {
                    model.ProfilePic = "/ProfilePic/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                }
                DataSet ds = model.UpdatePersonalDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.SuccessMessage = " Updated successfully !";
                    }
                    else
                    {
                        model.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSummaryReport(SummaryReport model)
        {
            List<SummaryList> lst = new List<SummaryList>();
            DataSet ds = model.GetSummaryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    SummaryList obj = new SummaryList();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                    obj.CustomerId = r["CustomerID"].ToString();
                    obj.Customername = r["CustomerName"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DisplayName = r["FirstName"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.ClosingDate = r["CalculationDate"].ToString();
                    obj.NetAmount = r["AMount"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    //obj.LeadershipBonus = r["BV"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.Status = r["PaymentStatus"].ToString();
                    obj.CalculationStatus = r["CalculationStatus"].ToString();
                    lst.Add(obj);
                }
                model.TotalNetAmount = double.Parse(ds.Tables[0].Compute("sum(AMount)", "").ToString()).ToString("n2");
                model.lstSummary = lst;
                model.Message = "Record Found !";
                model.Status = "0";
            }
            else
            {
                model.Message = "No Record Found !";
                model.Status = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBranch(Branch model)
        {
            List<Branch> lst = new List<Branch>();
            int count1 = 0;
            DataSet ds = model.GetBranchList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    if (count1 == 0)
                    {
                        model.BranchName = "Select Branch";
                        model.PK_BranchID = "0";
                    }
                    model.Status = "0";
                    model.BranchName = r["BranchName"].ToString();
                    model.PK_BranchID = r["PK_BranchID"].ToString();
                    //ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDesignation(Designation model)
        {
            List<DesignationDetails> lst = new List<DesignationDetails>();
            DataSet ds = model.GetDesignationList();
            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        DesignationDetails obj = new DesignationDetails();
                        obj.DesignationName = r["DesignationName"].ToString();
                        obj.PK_DesignationID = r["PK_DesignationID"].ToString();
                        lst.Add(obj);
                    }
                    model.Status = "0";
                    model.lstdesign = lst;
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.ErrorMessage = ex.Message;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult TestLogin(TestLoginAPI objParameters)
        {
            LoginAPI obj = new LoginAPI();
            if (objParameters.LoginId == "" || objParameters.LoginId == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                objParameters.Password = Crypto.Encrypt(objParameters.Password);
                DataSet dsResult = objParameters.LoginActionTest();
                if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1" && dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Trad Associate")
                    {
                        obj.Status = "0";
                        obj.SuccessMessage = "Successfully logged in!";
                        obj.Pk_userId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                        obj.UserType = dsResult.Tables[0].Rows[0]["UserType"].ToString();
                        obj.FullName = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                        obj.ProfilePic = dsResult.Tables[0].Rows[0]["ProfilePic"].ToString();
                        obj.StatusColor = dsResult.Tables[0].Rows[0]["StatusColor"].ToString();
                        obj.Password = Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    obj.Status = "1";
                    obj.ErrorMessage = "Invalid LoginId and Password.";
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #region Tree
        public ActionResult Tree(TreeAPI model)
        {

            Profile1 sta = new Profile1();
            TreeAPI obj = new TreeAPI();
            if (model.LoginId == "" || model.LoginId == null)
            {
                model.Status = "1";
                model.Message = "Please enter LoginId";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            if (model.Fk_headId == "" || model.Fk_headId == null)
            {
                model.Status = "1";
                model.Message = "Please enter headId";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            try
            {
                DataSet ds = model.GetTree();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["msg"].ToString() == "0")
                    {

                        List<Tree1> GetGenelogy = new List<Tree1>();
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Tree1 obj1 = new Tree1();
                            obj1.Fk_UserId = r["Fk_UserId"].ToString();
                            obj1.Fk_ParentId = r["Fk_ParentId"].ToString();
                            obj1.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                            obj1.SponsorId = r["SponsorId"].ToString();
                            obj1.LoginId = r["LoginId"].ToString();
                            obj1.TeamPermanent = r["TeamPermanent"].ToString();
                            obj1.MemberName = r["MemberName"].ToString();
                            obj1.MemberLevel = r["MemberLevel"].ToString();
                            obj1.Leg = r["Leg"].ToString();
                            obj1.Id = r["Id"].ToString();

                            obj1.ActivationDate = r["ActivationDate"].ToString();
                            obj1.ActiveLeft = r["ActiveLeft"].ToString();
                            obj1.ActiveRight = r["ActiveRight"].ToString();
                            obj1.InactiveLeft = r["InactiveLeft"].ToString();
                            obj1.InactiveRight = r["InactiveRight"].ToString();
                            obj1.BusinessLeft = r["BusinessLeft"].ToString();
                            obj1.BusinessRight = r["BusinessRight"].ToString();
                            obj1.ImageURL = r["ImageURL"].ToString();
                            GetGenelogy.Add(obj1);
                        }
                        obj.GetGenelogy = GetGenelogy;
                        obj.Message = "Tree";
                        obj.Status = "0";
                        obj.LoginId = model.LoginId;
                        obj.Fk_headId = model.Fk_headId;

                    }
                    else
                    {
                        sta.Status = "1";
                        sta.Message = "No Data Found";
                        return Json(sta, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    sta.Status = "1";
                    sta.Message = "No Data Found";
                    return Json(sta, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                sta.Status = "1";
                sta.Message = ex.Message;
                return Json(sta, JsonRequestBehavior.AllowGet);
            }


            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Registration

        public ActionResult Registration(RegistrationAPI model)
        {
            RegistrationAPI obj = new RegistrationAPI();
            if (model.SponsorId == "" || model.SponsorId == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Sponsor Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.FirstName == "" || model.FirstName == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter First Name";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.MobileNo == "" || model.MobileNo == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Mobile No";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.Leg == "" || model.Leg == null)
            {
                obj.Status = "1";
                obj.Message = "Please Select Leg";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            model.SponsorId = model.SponsorId;
            try
            {
                string password = Common.GenerateRandom();
                model.Password = Crypto.Encrypt(password);
                model.RegistrationBy = "Mobile";
                DataSet ds = model.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.FullName = ds.Tables[0].Rows[0]["Name"].ToString();
                        obj.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.TransPassword = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        obj.Leg = model.Leg;
                        obj.RegistrationBy = model.RegistrationBy;
                        obj.SponsorId = model.SponsorId;
                        obj.Email = model.Email;
                        obj.LastName = model.LastName;
                        obj.Address = model.Address;
                        obj.PinCode = model.PinCode;
                        obj.PanCard = model.PanCard;
                        obj.Gender = model.Gender;

                        try
                        {
                            string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            BLSMS.SendSMSNew(model.MobileNo, str2);
                        }
                        catch { }
                        obj.Status = "0";
                        obj.Message = "Registered Successfully";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }


        }

        #endregion

        #region SponsporName
        public ActionResult GetSponsorName(SponsorNameAPI sponsorname)
        {
            SponsorNameA obj = new SponsorNameA();
            DataSet ds = sponsorname.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.UserID = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                obj.SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.Status = "0";
                obj.Message = "Sponsor Name Fetched";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                sponsorname.Status = "1";
                sponsorname.Message = "Invalid SponsorId"; return Json(sponsorname, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region CustomerRegistration
        public ActionResult CustomerRegistration(CustomerRegistration model)
        {
            ResponseCustomerRegistration obj = new ResponseCustomerRegistration();
            if (model.UserID == "" || model.UserID == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Select User";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.FirstName == "" || model.FirstName == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter First Name";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.Contact == "" || model.Contact == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Enter Contact";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.Leg == "" || model.Leg == null)
            {
                obj.Status = "1";
                obj.ErrorMessage = "Please Select Leg";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            try
            {
                Random rnd = new Random();
                string pass = rnd.Next(111111, 999999).ToString();
                model.BranchID = "1";
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[10];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);

                model.CouponNo = finalString;
                model.Password = Crypto.Encrypt(pass);
                DataSet dsResult = new DataSet();
                if (model.RegistrationType == "onlyRegistration")
                {
                    dsResult = obj.CustomerRegistration(model);
                    if (dsResult != null && dsResult.Tables[0].Rows.Count > 0)
                    {
                        if (dsResult.Tables[1].Rows.Count > 0)
                        {
                            if (dsResult.Tables[1].Rows[0]["Msg"].ToString() == "1")
                            {
                                obj.Status = "0";
                                obj.Name = dsResult.Tables[1].Rows[0]["Name"].ToString();
                                obj.Pk_userId = dsResult.Tables[1].Rows[0]["PKUserID"].ToString();
                                obj.SuccessMessage = "Registration Successful !";
                                if (model.Contact != null)
                                {
                                    try
                                    {
                                        string message = "Dear " + dsResult.Tables[1].Rows[0]["Name"].ToString() + ", You have been successfully registered as Customer in Blossom City. You Login Id is " + dsResult.Tables[1].Rows[0]["LoginId"].ToString() + " and Password is " + Crypto.Decrypt(dsResult.Tables[1].Rows[0]["Password"].ToString()) + "";
                                        BLSMS.SendSMSNew(model.Contact, message);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                            }
                            else
                            {
                                obj.Status = "1";
                                obj.ErrorMessage = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            }
                        }
                    }
                }
                else
                {
                    dsResult = obj.CustomerRegistrationByAssociate(model);
                    if (dsResult.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.SuccessMessage = "Registration Request Successful !";
                        obj.Name = dsResult.Tables[0].Rows[0]["Name"].ToString();
                        obj.Pk_userId = dsResult.Tables[0].Rows[0]["PKUserID"].ToString();
                        if (model.Contact != null)
                        {
                            string body = "";
                            try
                            {
                                body = "Dear " + model.FirstName + model.LastName + ", You have been successfully requested as Customer in Blossom City. After Some time you will get Login Id and Password.";
                                BLSMS.SendSMSNew(model.Contact, body);

                            }
                            catch (Exception ex)
                            {
                                obj.ErrorMessage = ex.Message;
                            }
                        }
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.ErrorMessage = "Problem in connection.Please try after some times.";
                    }
                }

            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.ErrorMessage = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult GetPaymentMode(Payment model)
        {
            List<Payment_Mode> lst = new List<Payment_Mode>();
            DataSet dsSite = model.GetPaymentMode();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    Payment_Mode obj = new Payment_Mode();
                    obj.PaymentModeId = r["PK_paymentID"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    lst.Add(obj);
                }
                model.Status = "0";
                model.lstPayment = lst;
                model.Message = "Record Found";
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerBookingList(BookingList model)
        {
            List<BookingListDetails> lst = new List<BookingListDetails>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.BookingListForCustomer();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    BookingListDetails obj = new BookingListDetails();
                    obj.BookingStatus = r["BookingStatus"].ToString();
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
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.NetPlotAmount = r["NetPlotAmount"].ToString();
                    obj.PK_PLCCharge = r["PLCCharge"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    lst.Add(obj);
                }
                model.lstbooking = lst;
                model.Status = "0";
                model.Message = "Record Found.";
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found.";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult LedgerReportCustomer(CustomerLedger req)
        {
            LedgerReport model = new LedgerReport();
            List<PaymentDetails> lst = new List<PaymentDetails>();
            DataSet dsblock = req.FillDetailsForCustomer();
            if (dsblock != null && dsblock.Tables.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found!";
                if (dsblock.Tables[0].Rows.Count > 0)
                {
                    if (dsblock.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Result = "yes";
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
                if (dsblock.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in dsblock.Tables[1].Rows)
                    {
                        PaymentDetails obj = new PaymentDetails();

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
                    model.lstpayment = lst;
                }
            }
            else
            {
                model.Status = "0";
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LuckyDrawReport(LuckyDraw model)
        {
            List<LuckyDrawList> lst1 = new List<LuckyDrawList>();
            DataSet ds11 = model.GetLuckyDrawCustomerList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found !";
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    LuckyDrawList Obj = new LuckyDrawList();
                    Obj.LuckyDrawId = r["Pk_LuckyDrawId"].ToString();
                    Obj.CustomerLoginID = r["LoginId"].ToString();
                    Obj.CustomerName = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.Contact = r["Mobile"].ToString();
                    Obj.AssociateID = r["AssociateId"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.CouponNo = r["CouponNo"].ToString();
                    string afterFounder = "";
                    int pos = 4;
                    if (pos >= 0)
                    {
                        afterFounder = Obj.CouponNo.Remove(pos);
                    }
                    Obj.CouponNo = afterFounder + "******";
                    lst1.Add(Obj);
                }
                model.lst = lst1;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found !";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CustomerDashboard(CustomerDashboardforMobile newdata)
        {
            try
            {
                List<CustomerDashboardDetails> lstDetails = new List<CustomerDashboardDetails>();
                DataSet Ds = newdata.GetDetails();
                if (Ds != null && Ds.Tables[0].Rows.Count > 0)
                {
                    newdata.Status = "0";
                    foreach (DataRow r in Ds.Tables[0].Rows)
                    {
                        CustomerDashboardDetails obj = new CustomerDashboardDetails();

                        obj.TotalBooking = Ds.Tables[0].Rows[0]["TotalBooking"].ToString();
                        obj.TotalPaidAmount = Ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                        obj.TotalPending = Ds.Tables[0].Rows[0]["PendingAmt"].ToString();
                        obj.TotalPlotAmount = Ds.Tables[0].Rows[0]["Plotamount"].ToString();
                        lstDetails.Add(obj);
                    }
                    newdata.lstStatus = lstDetails;
                }
                List<DueInstallment> lstInst = new List<DueInstallment>();
                DataSet dsInst = newdata.GetDueInstallmentList();
                if (dsInst != null && dsInst.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsInst.Tables[0].Rows)
                    {
                        DueInstallment obj = new DueInstallment();

                        obj.CustomerID = r["PK_UserId"].ToString();
                        obj.CustomerLoginID = r["LoginId"].ToString();
                        obj.CustomerName = r["FirstName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        obj.InstallmentAmount = r["InstAmt"].ToString();


                        lstInst.Add(obj);
                    }

                    newdata.lstdueinstallment = lstInst;
                }

                List<NewsDetails> lstNews = new List<NewsDetails>();
                DataSet dsAssociate = newdata.GetNews();
                if (dsAssociate != null && dsAssociate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsAssociate.Tables[0].Rows)
                    {
                        NewsDetails obj = new NewsDetails();
                        //   obj.PK_BookingId = r["PK_UserId"].ToString();

                        obj.PK_NewsID = r["PK_NewsID"].ToString();
                        obj.NewsHeading = r["NewsHeading"].ToString();
                        obj.NewsBody = r["NewsBody"].ToString();

                        lstNews.Add(obj);
                    }


                    newdata.lstnewsdetail = lstNews;
                }
                else
                {
                    newdata.Status = "1";
                }
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return Json(newdata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DueInstallmentReport(DueInstallmentMobile req)
        {
            LedgerReport model = new LedgerReport();
            List<PaymentDetails> lst = new List<PaymentDetails>();
            DataSet dsblock = req.FillDueInstDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found!";
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
                    PaymentDetails obj = new PaymentDetails();

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
                model.lstpayment = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReturnBenefitByLogin(ReturnBenefit model)
        {
            List<ReturnBenefitView> lst1 = new List<ReturnBenefitView>();
            DataSet ds11 = model.ReturnBenefitView();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found!";
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    ReturnBenefitView Obj = new ReturnBenefitView();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PaymentDate = r["PaymentDate"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.PaidAmount = r["PaidAmount"].ToString();
                    Obj.DueDate = r["Installmentdate"].ToString();
                    Obj.Status = r["IsPaid"].ToString();
                    Obj.ReceiptNo = r["ReceiptNo"].ToString();
                    Obj.TransactionNo = r["TransactionNo"].ToString();
                    Obj.TransactionDate = r["TransactionDate"].ToString();
                    Obj.BankBranch = r["BankBranch"].ToString();
                    Obj.BankName = r["BankName"].ToString();
                    Obj.InstAmt = r["InstAmt"].ToString();
                    lst1.Add(Obj);
                }
                model.lstbenefit = lst1;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found!";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LuckyDrawListForAssociate(LuckyDraw model)
        {
            List<LuckyDrawList> lst1 = new List<LuckyDrawList>();
            DataSet ds11 = model.GetLuckyDrawCustomerList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found !";
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    LuckyDrawList Obj = new LuckyDrawList();
                    Obj.LuckyDrawId = r["Pk_LuckyDrawId"].ToString();
                    Obj.CustomerLoginID = r["LoginId"].ToString();
                    Obj.CustomerName = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.Contact = r["Mobile"].ToString();
                    Obj.AssociateID = r["AssociateId"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.CouponNo = r["CouponNo"].ToString();
                    string afterFounder = "";
                    int pos = 4;
                    if (pos >= 0)
                    {
                        afterFounder = Obj.CouponNo.Remove(pos);
                    }
                    Obj.CouponNo = afterFounder + "******";
                    lst1.Add(Obj);
                }
                model.lst = lst1;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found !";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LuckyDrawRequestForAssociate(LuckyDraw model)
        {
            List<LuckyDrawList> lst1 = new List<LuckyDrawList>();
            DataSet ds11 = model.GetLuckyDrawRequestList();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found !";
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    LuckyDrawList Obj = new LuckyDrawList();
                    Obj.CustomerLoginID = r["LoginId"].ToString();
                    Obj.CustomerName = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.Contact = r["MobileNo"].ToString();
                    Obj.AssociateID = r["AssociateId"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.CouponNo = r["CouponNo"].ToString();
                    Obj.ApprovalStatus = r["Status"].ToString();
                    string afterFounder = "";
                    int pos = 4;
                    if (pos >= 0)
                    {
                        afterFounder = Obj.CouponNo.Remove(pos);
                    }
                    Obj.CouponNo = afterFounder + "******";
                    lst1.Add(Obj);
                }
                model.lst = lst1;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found !";
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserRewardAPI(RewardAPI model)
        {
            model.RewardID = "1";
            List<RewardList> lst = new List<RewardList>();

            DataSet ds = model.RewardList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    RewardList obj = new RewardList();
                    obj.Status = r["Status"].ToString();
                    obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Contact = r["BackColor"].ToString();
                    obj.MatchingTarget = r["Target"].ToString();
                    obj.LeftBusiness = r["LeftBusiness"].ToString();
                    obj.RightBusines = r["RightBusiness"].ToString();
                    obj.PK_RewardItemId = r["PK_RewardItemId"].ToString();
                    lst.Add(obj);
                }
                model.lstReward = lst;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        public ActionResult CustomerDetails(CustomerList model)
        {
            try
            {
                List<CustomerDetails> lstCustomer = new List<CustomerDetails>();
                DataSet ds = model.GetListCustomer();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Status = "0";
                        model.Message = "Record Found!";
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            CustomerDetails obj = new CustomerDetails();
                            obj.Fk_SponsorId = r["FK_SponsorId"].ToString();
                            obj.CustomerID = r["AssociateId"].ToString();
                            obj.CustomerName = r["AssociateName"].ToString();
                            obj.UserID = r["PK_UserId"].ToString();
                            obj.AssociateID = r["SponsorId"].ToString();
                            obj.AssociateName = r["SponsorName"].ToString();
                            obj.Contact = r["Mobile"].ToString();
                            obj.Email = r["Email"].ToString();
                            obj.PanNo = r["PanNumber"].ToString();
                            obj.City = r["City"].ToString();
                            obj.State = r["State"].ToString();
                            obj.Address = r["Address"].ToString();
                            obj.BranchName = r["BranchName"].ToString();
                            obj.JoiningFromDate = r["JoiningDate"].ToString();
                            obj.Password = Crypto.Decrypt(r["Password"].ToString());
                            lstCustomer.Add(obj);
                        }
                        model.lstCustomer = lstCustomer;
                    }
                    else
                    {
                        model.Status = "1";
                        model.Message = "No Record Found!";
                    }
                }
                else
                {
                    model.Status = "1";
                    model.Message = "No Record Found!";
                }
            }
            catch (Exception ex)
            {
                model.Status = "1";
                model.Message = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult HoldPlot(PlotAPI obj)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                obj.HoldFrom = DateTime.Now;
                if (obj.Amount != null && obj.Amount != "0")
                {
                    obj.HoldTo = obj.HoldFrom.AddHours(480);
                }
                else
                {
                    obj.HoldTo = obj.HoldFrom.AddHours(48);
                }
                string password = Common.GenerateRandom();
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.Password = Crypto.Encrypt(password);
                DataSet ds = obj.SavePlotHoldByAssociateNew();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        string body = "Dear " + ds.Tables[0].Rows[0]["Name"].ToString() + ", You have been successfully registered as Customer in Blossom City. Your Login Id is " + ds.Tables[0].Rows[0]["LoginId"].ToString() + " and Password is " + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        BLSMS.SendSMSNew(obj.Mobile, body);
                        res.Status = "0";
                        res.Message = "Plot is on hold.";
                        res.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        res.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        res.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        res.Type = "Associate";
                    }
                    else
                    {
                        res.Status = "1";
                        res.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                res.Status = "1";
                res.Message = ex.Message;
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DirectIncome(PayoutAPI obj)
        {
            try
            {
                List<WalletAPI> lst1 = new List<WalletAPI>();
                DataSet ds11 = obj.GetPayoutReport();
                if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                {
                    obj.Status = "0";
                    obj.Message = "Record Found";
                    foreach (DataRow r in ds11.Tables[0].Rows)
                    {
                        WalletAPI Obj = new WalletAPI();
                        Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                        Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                        Obj.LoginId = r["LoginId"].ToString();
                        Obj.DisplayName = r["FirstName"].ToString();
                        Obj.PayoutNo = r["PayoutNo"].ToString();
                        Obj.ClosingDate = r["ClosingDate"].ToString();

                        Obj.DirectIncome = r["DirectIncome"].ToString();

                        lst1.Add(Obj);
                    }
                    obj.lstPayout = lst1;
                }
                else
                {
                    obj.Status = "1";
                    obj.Message = "No Record forund";
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReturnBenefitReport(BenefitReport model)
        {
            List<BenefitReportResponse> lst1 = new List<BenefitReportResponse>();
            DataSet ds11 = model.ReturnBenefitAssociate();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    BenefitReportResponse Obj = new BenefitReportResponse();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Name = r["FirstName"].ToString();
                    Obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.ReturnBenefitStartDate = r["ReturnBenefitStartDate"].ToString();
                    lst1.Add(Obj);
                }

                model.lstassociate = lst1;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PairIncome(PayoutAPI obj)
        {
            try
            {
                List<WalletAPI> lst1 = new List<WalletAPI>();
                DataSet ds11 = obj.GetPayoutReport();
                if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                {
                    obj.Status = "0";
                    obj.Message = "Record Found";
                    foreach (DataRow r in ds11.Tables[0].Rows)
                    {
                        WalletAPI Obj = new WalletAPI();
                        Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                        Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                        Obj.LoginId = r["LoginId"].ToString();
                        Obj.DisplayName = r["FirstName"].ToString();
                        Obj.PayoutNo = r["PayoutNo"].ToString();
                        Obj.ClosingDate = r["ClosingDate"].ToString();
                        Obj.BinaryIncome = r["BinaryIncome"].ToString();

                        lst1.Add(Obj);
                    }
                    obj.lstPayout = lst1;
                }
                else
                {
                    obj.Status = "1";
                    obj.Message = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckPlot(string SiteID, string SectorID, string BlockID, string PlotNumber)
        {
            PlotStatus model = new PlotStatus();
            PlotStatusReq req = new PlotStatusReq();
            req.SiteID = SiteID;
            req.SectorID = SectorID;
            req.BlockID = BlockID;
            req.PlotNumber = PlotNumber;
            DataSet dsblock = req.CheckPlotAvailibility();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Status = "1";
                    model.Message = "Plot Not Available";
                }
                else
                {
                    model.Status = "0";
                    model.Message = "Plot Available";
                    model.BookingPercent = dsblock.Tables[0].Rows[0]["BookingPercent"].ToString();
                    model.PlotSize = dsblock.Tables[0].Rows[0]["TotalArea"].ToString();
                    model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmount"].ToString();
                    model.TotalPLC = dsblock.Tables[0].Rows[0]["TotalPLC"].ToString();
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                }
            }
            else
            {
                model.Status = "1";
                model.Message = "Plot Not Available";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Status(PaymentStatus model)
        {
            PaymentStatusResponse res = new PaymentStatusResponse();
            List<PaymentStatus> lst = new List<PaymentStatus>();
            DataSet dsSite = model.GetPaymentStatus();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    PaymentStatus obj = new PaymentStatus();
                    obj.Text = r["Text"].ToString();
                    obj.Value = r["Value"].ToString();
                    lst.Add(obj);
                }
                res.Status = "0";
                res.lst = lst;
            }
            else
            {
                res.Status = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerDetails(string CustomerLoginId)
        {
            CustomerDetail obj = new CustomerDetail();
            obj.LoginId = CustomerLoginId;
            DataSet ds = obj.GetCustomerDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                //obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();

                obj.Message = "Record Found!";

            }
            else
            {
                obj.Status = "1";
                obj.Message = "Invalid Customer Id";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCustomerInfo(string CustomerLoginId)
        {
            try
            {
                CustomerInfo model = new CustomerInfo();

                DataSet dsSponsorName = model.GetCustomerDetail(CustomerLoginId);
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.Status = "0";
                    model.Message = "Record Found!";
                    model.SponsorID = dsSponsorName.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                    model.SponsorLoginID = dsSponsorName.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = dsSponsorName.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.CustomerID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.CustomerLoginID = dsSponsorName.Tables[0].Rows[0]["CustomerId"].ToString();
                    model.CustomerName = dsSponsorName.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.Contact = dsSponsorName.Tables[0].Rows[0]["Mobile"].ToString();
                }
                else
                {
                    model.Status = "1";
                    model.Message = "Invalid Customer Login Id!";
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult TemporaryPlotHold(PlotAPI obj)
        {
            ResponseModel res = new ResponseModel();
            try
            {
                obj.HoldFrom = DateTime.Now;
                obj.HoldTo = obj.HoldFrom.AddHours(48);
                DataSet ds = obj.SaveTemporaryPlotHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        res.Status = "1";
                        res.Message = "Plot is on hold.";
                    }
                    else
                    {
                        res.Status = "0";
                        res.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                res.Status = "1";
                res.Message = ex.Message;
            }


            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApprovedBookingList(ApprovedBookingList model)
        {
            List<PlotMobile> lst = new List<PlotMobile>();
            DataSet ds11 = model.GetBookingDetailsListAssociate();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    PlotMobile obj = new PlotMobile();
                    obj.BookingStatus = r["BookingStatus"].ToString();
                    obj.PK_BookingId = r["PK_BookingId"].ToString();
                    obj.BranchID = r["BranchID"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.CustomerID = r["CustomerID"].ToString();
                    obj.CustomerLoginID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateID"].ToString();
                    obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    obj.Discount = r["Discount"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.BookingAmount = r["BookingAmt"].ToString();
                    obj.PaymentPlanID = r["PlanName"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.NetPlotAmount = r["NetPlotAmount"].ToString();
                    obj.PK_PLCCharge = r["PLCCharge"].ToString();
                    obj.PlotRate = r["PlotRate"].ToString();
                    obj.Type = r["Type"].ToString();
                    lst.Add(obj);
                }
                model.lstApprove = lst;

            }
            else
            {
                model.Status = "0";
                model.Message = "Record Not Found.";

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TopupReport(TopupMobile model)
        {

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<CustomerMobile> lst = new List<CustomerMobile>();
            DataSet ds = model.GetTopupReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    CustomerMobile obj = new CustomerMobile();
                    obj.PK_TopUpId = r["PK_TopUpId"].ToString();
                    obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.CouponAmount = Convert.ToDecimal(r["Amount"]);
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.AccountNo = r["PaymentDetail"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.status = r["TopUpStatus"].ToString();
                    lst.Add(obj);
                }
                model.ListCust = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PaidPayout(WalletMobile objewallet)
        {
            List<WalletListMobile> lst = new List<WalletListMobile>();
            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            DataSet ds = objewallet.GetPaidPayout();
            ViewBag.Total = "0";
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                objewallet.Status = "0";
                objewallet.Message = "No Record Found";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WalletListMobile Objload = new WalletListMobile();
                    Objload.LoginId = dr["Loginid"].ToString();
                    Objload.DisplayName = dr["Name"].ToString();
                    Objload.PaymentDate = dr["Paymentdate"].ToString();

                    Objload.Amount = dr["Amount"].ToString();
                    Objload.TransactionDate = dr["TransactionDate"].ToString();
                    Objload.TransactionNo = dr["TransactionNo"].ToString();
                    ViewBag.Total = Convert.ToDecimal(ViewBag.Total) + Convert.ToDecimal(dr["Amount"].ToString());
                    lst.Add(Objload);
                }
                objewallet.lstpayoutledger = lst;
            }
            else
            {
                objewallet.Status = "0";
                objewallet.Message = "No Record Found";
            }
            return Json(objewallet, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPlotHoldList(PlotListMobile model)
        {
            List<Plotlistmobile> lst = new List<Plotlistmobile>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetPlotHoldListAssociate();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "1";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {

                    Plotlistmobile obj = new Plotlistmobile();
                    obj.PK_PlotHoldID = r["PK_PlotHoldID"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_PlotHoldID"].ToString());
                    obj.PlotNumber = r["Plot"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.HoldFrom = r["HoldFrom"].ToString();
                    obj.HoldTo = r["HoldTo"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Mobile = r["Mobile"].ToString();

                    lst.Add(obj);
                }
                model.lstPlotlistmobile = lst;
            }
            else
            {
                model.Status = "0";
                model.Message = "Record Not Found.";

            }
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        public ActionResult DirectList(DirectListMobile model)
        {
            List<ReportsMobile> lst = new List<ReportsMobile>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetDirectList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "1";
                model.Message = "Record Found.";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    ReportsMobile obj = new ReportsMobile();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.SponsorId = (r["LoginId"].ToString());
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.SponsorName = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                model.lstassociateDirect = lst;

            }
            else
            {
                model.Status = "0";
                model.Message = "Record Not Found.";

            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}