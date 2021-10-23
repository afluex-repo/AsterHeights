using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsterHeight.Models;
using System.Data;
using AsterHeight.Filter;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace AsterHeight.Controllers
{
    public class CustomerController : AdminBaseController
    {
        Common obj = new Common();
        #region CustomerRegistration
        public ActionResult CustomerRegistration(string UserID)
        {
            Customer model = new Customer();
            try
            {
                if (UserID != null)
                {
                    model.UserID = Crypto.Decrypt(UserID);
                    //  model.UserID = UserID;
                    DataSet dsPlotDetails = model.GetList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.UserID = UserID;
                        model.FK_SponsorId = dsPlotDetails.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                        model.SponsorID = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                        model.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                        model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                        model.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                        model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                        model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                        model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                        model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                        model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                        model.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                        model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                        model.AssociateID = dsPlotDetails.Tables[0].Rows[0]["AssociateId"].ToString();
                        model.AssociateName = dsPlotDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                        model.JoiningDate = dsPlotDetails.Tables[0].Rows[0]["JoiningDate"].ToString();
                    }
                }

                else
                {


                }
                #region ddlBranch
                TraditionalAssociate obj = new TraditionalAssociate();
                int count = 0;
                List<SelectListItem> ddlBranch = new List<SelectListItem>();
                DataSet dsBranch = obj.GetBranchList();
                if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsBranch.Tables[0].Rows)
                    {
                        //if (count == 0)
                        //{
                        //    ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                        //}
                        ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlBranch = ddlBranch;

                #endregion
                #region ddlPaymentMode
                int count3 = 0;
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                DataSet dsPayMode = obj.GetPaymentMode();
                if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsPayMode.Tables[0].Rows)
                    {
                       
                        if (count3 == 0)
                        {
                            ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                        }
                        //if (count3 == dsPayMode.Tables[0].Rows.Count - 1)
                        //{
                        //    ddlPaymentMode.Add(new SelectListItem { Text = "Booking Reference", Value = "12" });
                        //}
                        //else
                        //{
                            ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                        //}
                        count3 = count3 + 1;
                    }
                }
                ViewBag.ddlPaymentMode = ddlPaymentMode;
                #endregion
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
   
        public ActionResult GetSponsorName(string SponsorID)
        {
            try
            {
                Customer model = new Customer();
                model.LoginID = SponsorID;

                #region GetSiteRate
                DataSet dsSponsorName = model.GetSponsorName();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.SponsorName = dsSponsorName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.SponsorName = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("CustomerRegistration")]
        [OnAction(ButtonName = "btnRegistration")]
        public ActionResult CustomerRegistration(Customer model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                string ctrpass = rnd.Next(111111, 999999).ToString();
                model.Password = Crypto.Encrypt(ctrpass);
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[10];
                var random = new Random();
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);

                model.CouponNo = finalString;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                Random ran = new Random();
                string imageFile = "";
                string path = "";
            
                if (model.profilePhoto != null)
                {
                    imageFile = "photo_" + ran.Next(000, 999) + model.profilePhoto.FileName;
                    path = Server.MapPath("~/ProfilePhoto/");
                    model.profilePhoto.SaveAs(path + imageFile);
                    model.Photo = imageFile;
                }
                DataSet dsRegistration = model.CustomerRegistration();
                if (model.RegistrationType == "luckyDraw")
                {
                    if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                    {
                        if (dsRegistration.Tables[3] != null && dsRegistration.Tables[3].Rows.Count > 0)
                        {
                            if (dsRegistration.Tables[3].Rows[0][0].ToString() == "1")
                            {
                                if (model.Contact != null && model.Contact != "")
                                {
                                    string mailbody = "";
                                    try
                                    {
                                        mailbody = "Dear " + dsRegistration.Tables[3].Rows[0]["Name"].ToString() + ", You have been successfully registered as Customer in Blossom City. Your Login Id " + dsRegistration.Tables[3].Rows[0]["LoginId"].ToString() + " and Password is " + Crypto.Decrypt(dsRegistration.Tables[3].Rows[0]["Password"].ToString()) + " .";
                                        BLSMS.SendSMSNew(model.Contact, mailbody);
                                        TempData["Message"] = "Registration Successfull !";
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                                {
                                    if (dsRegistration.Tables[3].Rows[0][0].ToString() == "1")
                                    {
                                        Session["DisplayNameConfirm"] = dsRegistration.Tables[3].Rows[0]["Name"].ToString();
                                        Session["LoginIDConfirm"] = dsRegistration.Tables[3].Rows[0]["LoginId"].ToString();
                                        Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[3].Rows[0]["Password"].ToString());
                                        Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[3].Rows[0]["PKUserID"].ToString());
                                    }
                                    else
                                    {
                                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                    {
                        if (dsRegistration.Tables[1] != null && dsRegistration.Tables[1].Rows.Count > 0)
                        {
                            if (dsRegistration.Tables[1].Rows[0][0].ToString() == "1")
                            {
                                if (model.Contact != null && model.Contact != "")
                                {
                                    string mailbody = "";
                                    try
                                    {
                                        mailbody = "Dear " + dsRegistration.Tables[1].Rows[0]["Name"].ToString() + ", You have been successfully registered as Customer in Blossom City. Your Login Id " + dsRegistration.Tables[1].Rows[0]["LoginId"].ToString() + " and Password is " + Crypto.Decrypt(dsRegistration.Tables[1].Rows[0]["Password"].ToString()) + " .";
                                        BLSMS.SendSMSNew(model.Contact, mailbody);
                                        TempData["Message"] = "Registration Successfull !";
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }
                                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                                {
                                    if (dsRegistration.Tables[1].Rows[0][0].ToString() == "1")
                                    {
                                        Session["DisplayNameConfirm"] = dsRegistration.Tables[1].Rows[0]["Name"].ToString();
                                        Session["LoginIDConfirm"] = dsRegistration.Tables[1].Rows[0]["LoginId"].ToString();
                                        Session["PasswordConfirm"] = Crypto.Decrypt(dsRegistration.Tables[1].Rows[0]["Password"].ToString());
                                        Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[1].Rows[0]["PKUserID"].ToString());
                                    }
                                    else
                                    {
                                        TempData["Registration"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                    }
                                }
                            }
                        }

                    }
                }
               
                
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "ConfirmRegistration";
            Controller = "Customer";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        #endregion

        #region Customer list

        public ActionResult CustomerList()
        {
           
            return View();
        }
        [HttpPost]
        [ActionName("CustomerList")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult CustomerList(Customer model)
        {
            List<Customer> lst = new List<Customer>();
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();
                    obj.FK_SponsorId = r["FK_SponsorId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.LoginID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_UserId"].ToString();
                    obj.SponsorID = r["SponsorId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.isBlocked = r["isBlocked"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.LastName = r["LastName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    lst.Add(obj);
                }
                model.ListCust = lst;
            }
            return View(model);
        }
        public ActionResult EditCustomerDetail(string UserID)
        {
            Customer model = new Customer();
            try
            {
                #region ddlBranch
                TraditionalAssociate obj = new TraditionalAssociate();
                int count = 0;
                List<SelectListItem> ddlBranch = new List<SelectListItem>();
                DataSet dsBranch = obj.GetBranchList();
                if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsBranch.Tables[0].Rows)
                    {
                        //if (count == 0)
                        //{
                        //    ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                        //}
                        ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlBranch = ddlBranch;

                #endregion
                if (UserID != null)
                {
                    model.UserID = Crypto.Decrypt(UserID);
                    //  model.UserID = UserID;
                    DataSet dsPlotDetails = model.GetList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        model.UserID = UserID;
                        model.FK_SponsorId = dsPlotDetails.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                        model.SponsorID = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                        model.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                        model.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                        model.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                        model.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                        model.Contact = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                        model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                        model.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                        model.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                        model.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                        model.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                        model.PanNo = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                        model.AssociateID = dsPlotDetails.Tables[0].Rows[0]["AssociateId"].ToString();
                        model.AssociateName = dsPlotDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                        model.JoiningDate = dsPlotDetails.Tables[0].Rows[0]["JoiningDate"].ToString();
                        model.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                        model.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                        model.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                        model.AccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                        model.AccountHolderName = dsPlotDetails.Tables[0].Rows[0]["BankHolderName"].ToString();
                        if (dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString()!=null && dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString()!="")
                        {
                            model.Photo = "/ProfilePhoto/" + dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();
                        }
                        
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("EditCustomerDetail")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult Update(Customer model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Random ran = new Random();
                string imageFile = "";
                model.BranchID = "1";
                string path = "";
                if (model.profilePhoto != null)
                {
                    imageFile = "photo_" + ran.Next(000, 999) + model.profilePhoto.FileName;
                    path = Server.MapPath("~/ProfilePhoto/");
                    model.profilePhoto.SaveAs(path + imageFile);
                    model.Photo = imageFile;
                }
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.UserID = Crypto.Decrypt(model.UserID);
                DataSet ds = model.UpdateCustomer();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = " Updated successfully !";
                    }
                    else
                    {
                        TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "CustomerList";
            Controller = "Customer";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult Delete(string UserID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Customer obj = new Customer();
                obj.UserID = UserID;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteCustomer();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = "Customer deleted successfully";
                        FormName = "CustomerList";
                        Controller = "Customer";
                    }
                    else
                    {
                        TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "CustomerList";
                        Controller = "Customer";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
                FormName = "CustomerList";
                Controller = "Customer";
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region LuckyDrawDetails
        public ActionResult LuckyDrawDetails()
        {
            Customer model = new Customer();
            List<Customer> lst1 = new List<Customer>();
            DataSet ds11 = model.GetLuckyDrawCustomerList();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentMode();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Customer Obj = new Customer();
                    Obj.LuckyDrawId = r["Pk_LuckyDrawId"].ToString();
                    Obj.CustomerLoginID = r["LoginId"].ToString();
                    Obj.CustomerName = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.Contact = r["Mobile"].ToString();
                    Obj.AssociateID = r["AssociateId"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.AddedOn = r["CreatedDate"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.CouponNo = r["CouponNo"].ToString();
                    Obj.status = r["Status"].ToString();
                    string afterFounder = "";
                    int pos = 4;
                    if (pos >= 0)
                    {
                        afterFounder = Obj.CouponNo.Remove(pos);
                    }
                    Obj.CouponNo = afterFounder + "******";
                    Obj.Description = r["Description"].ToString();
                    lst1.Add(Obj);
                }
                model.ListCust = lst1;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult LuckyDrawDetails(Customer model)
        {

            List<Customer> lst1 = new List<Customer>();
            if (model.PaymentMode == "0")
            {
                model.PaymentMode = null;
            }
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds11 = model.GetLuckyDrawCustomerList();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentMode();
            if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPayMode.Tables[0].Rows)
                {
                    if (count3 == 0)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Customer Obj = new Customer();
                    Obj.LuckyDrawId = r["Pk_LuckyDrawId"].ToString();
                    Obj.CustomerLoginID = r["LoginId"].ToString();
                    Obj.CustomerName = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.Contact = r["Mobile"].ToString();
                    Obj.AssociateID = r["AssociateId"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.AddedOn = r["CreatedDate"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.CouponNo = r["CouponNo"].ToString();
                    Obj.status = r["CouponNo"].ToString();
                    string afterFounder = "";
                    int pos = 4;
                    if (pos >= 0)
                    {
                        afterFounder = Obj.CouponNo.Remove(pos);
                    }
                    Obj.CouponNo = afterFounder + "******";
                    Obj.Description = r["Description"].ToString();

                    lst1.Add(Obj);
                }
                model.ListCust = lst1;
            }
            return View(model);
        }
        public ActionResult PrintLuckyDrawReceipt(string LuckyDrawId)
        {
            List<Reports> list = new List<Reports>();
            Reports model = new Reports();
            if (LuckyDrawId != null)
            {
                model.LuckyDrawId = LuckyDrawId;
                try
                {
                    DataSet ds = model.PrintLuckyDrawReceipt();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Reports obj = new Reports();

                            obj.LuckyDrawId = r["Pk_LuckyDrawId"].ToString();
                            ViewBag.Mobile = r["Mobile"].ToString();
                            //obj.EncryptKey = Crypto.Encrypt(r["Fk_SaleOrderId"].ToString());
                            //obj.ProductID = r["Fk_ProductId"].ToString();
                            obj.MRP = r["FinalAmount"].ToString();
                            obj.IGST = r["IGST"].ToString();
                            obj.CGST = r["CGST"].ToString();
                            obj.SGST = r["SGST"].ToString();
                            obj.FinalAmount = r["FinalAmount"].ToString();
                            ViewBag.OrderNo = r["InvoiceNo"].ToString();

                            ViewBag.TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString();
                            ViewBag.TotalFinalAmountWords = ds.Tables[1].Rows[0]["TotalFinalAmountWords"].ToString();
                            ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["UpgradtionDate"].ToString();
                            ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            ViewBag.Loginid = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            ViewBag.AssociateAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                            ViewBag.ValueBeforeTax = ds.Tables[1].Rows[0]["Taxable"].ToString();
                            ViewBag.TaxAdded = ds.Tables[1].Rows[0]["TaxAmount"].ToString();

                            ViewBag.CompanyName = SoftwareDetails.CompanyName1;
                            ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                            ViewBag.Pin1 = SoftwareDetails.Pin1;
                            ViewBag.State1 = SoftwareDetails.State1;
                            ViewBag.City1 = SoftwareDetails.City1;
                            ViewBag.ContactNo = SoftwareDetails.ContactNo2;
                            ViewBag.LandLine = SoftwareDetails.LandLine;
                            ViewBag.Website = SoftwareDetails.Website;
                            ViewBag.EmailID = SoftwareDetails.EmailID;
                            list.Add(obj);

                        }
                        model.lsttopupreport = list;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            return View(model);
        }
        #endregion

        #region CustomerApproval
        public ActionResult ApproveCustomer()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ApproveCustomer")]
        [OnAction(ButtonName = "GetList")]
        public ActionResult GetCustomerList(Reports model)
        {
            try
            {
                List<Reports> lstassociate = new List<Reports>();
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.GetCustomerList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.Name = r["Name"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.Mobile = r["MobileNo"].ToString();
                        obj.CustomerId = r["PK_UserId"].ToString();
                        obj.PaymentMode = (r["PaymentMode"].ToString());
                        obj.TransactionDate = r["TransactionDate"].ToString();
                        obj.PaymentDate = r["CreatedDate"].ToString();
                        obj.TransactionNo = r["TransactionNo"].ToString();
                        obj.BankName = r["BankName"].ToString();
                        obj.BankBranch = (r["BankBranch"].ToString());
                        obj.Status = (r["Status"].ToString());
                        lstassociate.Add(obj);
                    }
                    model.lstassociate = lstassociate;
                }
                return View(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult Approved(string FK_UserID)
        {
            string FormName = "'";
            string Controller = "";
            try
            {
                if (FK_UserID != null)
                {
                    Reports model = new Reports();
                    model.CustomerId = FK_UserID;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.LoginId = Session["LoginId"].ToString();
                    DataSet ds = model.ApprovedCustomer();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[3] !=null && ds.Tables[3].Rows[0][0].ToString() == "1")
                        {
                            string mailbody = "";
                            try
                            {
                                mailbody = "Dear " + ds.Tables[3].Rows[0]["Name"].ToString() + ", You have been successfully approved as Customer in Blossom City. Your Login Id " + ds.Tables[3].Rows[0]["LoginId"].ToString() + " and Password is " + Crypto.Decrypt(ds.Tables[3].Rows[0]["Password"].ToString()) + " .";
                                BLSMS.SendSMSNew(ds.Tables[3].Rows[0]["MobileNo"].ToString(), mailbody);
                                TempData["Message"] = "Registration Successfull !";
                            }
                            catch (Exception ex)
                            {

                            }
                            TempData["Approved"] = "Lucky Draw Approved Successfully";
                            FormName = "ApproveCustomer";
                            Controller = "Customer";
                        }
                        else
                        {
                            TempData["Approved"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "ApproveCustomer";
                            Controller = "Customer";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult RejetCustomer(string FK_UserID)
        {
            string FormName = "'";
            string Controller = "";
            try
            {
                if (FK_UserID != null)
                {
                    Reports model = new Reports();
                    model.AssociateID = FK_UserID;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.LoginId = Session["LoginId"].ToString();
                    DataSet ds = model.RejectCustomer();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Approved"] = "Customer Rejected  Successfull";
                            FormName = "ApproveCustomer";
                            Controller = "Customer";
                        }
                        else
                        {
                            TempData["Approved"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "ApproveCustomer";
                            Controller = "Customer";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(FormName, Controller);
        }
        #endregion

       
    }
}