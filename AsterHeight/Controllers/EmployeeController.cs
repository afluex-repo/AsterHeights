﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AsterHeight.Filter;
using AsterHeight.Models;

namespace AsterHeight.Controllers
{
    public class EmployeeController : AdminBaseController
    {
        #region Employee
        public ActionResult EmployeeRegistration(string UserID)
        {

            Employee model = new Employee();
       
            model.JoiningDate = DateTime.Now.ToString("dd/MM/yyyy");
            try
            {
                if (UserID != null)
                {

                    model.UserID = Crypto.Decrypt(UserID);

                    DataSet dsPlotDetails = model.GetList();
                    if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                    {
                        //  model.UserID = UserID;
                        model.UserID = dsPlotDetails.Tables[0].Rows[0]["PK_AdminId"].ToString();
                        model.Name = dsPlotDetails.Tables[0].Rows[0]["Name"].ToString();
                        model.Mobile = dsPlotDetails.Tables[0].Rows[0]["Contact"].ToString();
                        model.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                        model.JoiningDate = dsPlotDetails.Tables[0].Rows[0]["JoiningDate"].ToString();
                        model.UserTypeID = dsPlotDetails.Tables[0].Rows[0]["Fk_UserTypeId"].ToString();
                        model.UserTypeName = dsPlotDetails.Tables[0].Rows[0]["UserType"].ToString();
                        model.Salary = dsPlotDetails.Tables[0].Rows[0]["Salary"].ToString();
                    }
                }

                else
                {


                }
                #region ddlUserType
                Employee obj = new Employee();
                int count = 0;
                List<SelectListItem> ddlUserType = new List<SelectListItem>();
                DataSet dsBranch = obj.UserTypeList();
                if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsBranch.Tables[0].Rows)
                    {
                        if (count == 0)
                        {
                            ddlUserType.Add(new SelectListItem { Text = "Select UserType", Value = "0" });
                        }
                        ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["PK_UserTypeId"].ToString() });
                        count = count + 1;
                    }
                }

                ViewBag.ddlUserType = ddlUserType;

                #endregion
                return View(model);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        [ActionName("EmployeeRegistration")]
        [OnAction(ButtonName = "Registration")]
        public ActionResult Registration(Employee model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Random rnd = new Random();
                string ctrpass = rnd.Next(111111, 999999).ToString();
                model.Password = Crypto.Encrypt(ctrpass);
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.JoiningDate = string.IsNullOrEmpty(model.JoiningDate) ? null : Common.ConvertToSystemDate(model.JoiningDate, "dd/MM/yyyy");
                DataSet dsRegistration = model.EmployeeReg();
                if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                {
                    if (model.Email != null)
                    {
                        string mailbody = "";
                        try
                        {
                            mailbody = "Dear  " + dsRegistration.Tables[0].Rows[0]["Name"].ToString() + ",You have been successfully registered as RealEstateDemo Employee.Given below are your login details .!<br/>  <b>Login ID</b> :  " + dsRegistration.Tables[0].Rows[0]["LoginId"].ToString() + "<br/> <b>Passoword</b>  : " + dsRegistration.Tables[0].Rows[0]["Password"].ToString();

                            var fromAddress = new MailAddress("prakher.afluex@gmail.com");
                            var toAddress = new MailAddress(model.Email);

                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential("prakher.afluex@gmail.com", "Baby8542816119")

                            };

                            using (var message = new MailMessage("prakher.afluex@gmail.com", model.Email)
                            {
                                IsBodyHtml = true,
                                Subject = "Employee Registration",
                                Body = mailbody
                            })
                                smtp.Send(message);
                            TempData["Message"] = "Registration Successfull !";
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                {
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        Session["DisplayNameConfirm"] = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        Session["LoginIDConfirm"] = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["PasswordConfirm"] = dsRegistration.Tables[0].Rows[0]["Password"].ToString();


                        string name = dsRegistration.Tables[0].Rows[0]["Name"].ToString();
                        string id = dsRegistration.Tables[0].Rows[0]["LoginId"].ToString();
                        string pass = Crypto.Decrypt(dsRegistration.Tables[0].Rows[0]["Password"].ToString());
                        string mob = model.Mobile;

                        //string str = BLSMS.CustomerRegistration(name, id, pass);
                        //try
                        //{
                        //    BLSMS.SendSMS(mob, str);
                        //}
                        //catch { }
                    }
                    else
                    {
                        TempData["Employee"] = dsRegistration.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Employee"] = ex.Message;
            }
            FormName = "ConfirmEmployeeRegistration";
            Controller = "Employee";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ConfirmEmployeeRegistration()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            #region ddlUserType
            Employee obj = new Employee();
            int count = 0;
            List<SelectListItem> ddlUserType = new List<SelectListItem>();
            DataSet dsBranch = obj.UserTypeList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUserType.Add(new SelectListItem { Text = "Select UserType", Value = "0" });
                    }
                    ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["PK_UserTypeId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUserType = ddlUserType;

            #endregion
            return View();
        }
        [HttpPost]
        [ActionName("EmployeeList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult EmpList(Employee model)
        {

            List<Employee> lst = new List<Employee>();
            model.UserTypeID = model.UserTypeID == "0" ? null : model.UserTypeID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "MM/dd/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "MM/dd/yyyy");
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Employee obj = new Employee();


                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Password = r["Password"].ToString();
                    obj.Mobile = r["Contact"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.UserTypeID = r["FK_UserTypeId"].ToString();
                    obj.UserTypeName = r["UserType"].ToString();
                    obj.UserID = r["PK_AdminId"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Salary = r["Salary"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_AdminId"].ToString());
                    lst.Add(obj);
                }
                model.EmpList = lst;
            }
            #region ddlUserType
            Employee obj1 = new Employee();
            int count = 0;
            List<SelectListItem> ddlUserType = new List<SelectListItem>();
            DataSet dsBranch = obj1.UserTypeList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlUserType.Add(new SelectListItem { Text = "Select UserType", Value = "0" });
                    }
                    ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["PK_UserTypeId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlUserType = ddlUserType;

            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("EmployeeRegistration")]
        [OnAction(ButtonName = "Update")]
        public ActionResult Update(Employee model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                model.JoiningDate = string.IsNullOrEmpty(model.JoiningDate) ? null : Common.ConvertToSystemDate(model.JoiningDate, "dd/MM/yyyy");
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.UpdateEmployee();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Employee"] = " Employee Updated successfully !";
                    }
                    else
                    {
                        TempData["Employee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            FormName = "EmployeeList";
            Controller = "Employee";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult Delete(string UserID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Employee obj = new Employee();
                obj.UserID = UserID;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeleteEmployee();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Employee"] = "Employee deleted successfully";
                        FormName = "EmployeeList";
                        Controller = "Employee";
                    }
                    else
                    {
                        TempData["RegistraEmployeetion"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "EmployeeList";
                        Controller = "Employee";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Employee"] = ex.Message;
                FormName = "EmployeeList";
                Controller = "Employee";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
    }
}