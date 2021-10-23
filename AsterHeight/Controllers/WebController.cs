using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AsterHeight.Models;
using AsterHeight.Filter;
using System.Data;
namespace AsterHeight.Controllers
{
    public class WebController : Controller
    {
        // GET: Website
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OurPlots()
        {
            return View();
        }

        public ActionResult OurProfile()
        {
            return View();
        }


        public ActionResult OurHouse()
        {
            return View();
        }

        public ActionResult Events()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Home model)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = model.SaveEnquiry();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Msg"] = "Thank you for contacting us.";
                        string str2 = "Dear " + model.Name + ", Thank you for contacting us. ";
                        BLSMS.SendSMSNew(model.Mobile, str2);
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["News"] = ex.Message;
            }
            return RedirectToAction("Contact", "Web");
        }
        public ActionResult OurVillas()
        {
            return View();
        }

        public ActionResult PlotAvailability()
        {
            return View();
        }

        public ActionResult SiteVisit()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult BankDetails()
        {
            return View();
        }
        public ActionResult OurProjects()
        {
            return View();
        }
    }
}