using AsterHeight.Filter;
using AsterHeight.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace AsterHeight.Controllers
{
    public class PlotController : AdminBaseController
    {


        #region PlotBooking

        public ActionResult PlotBooking(string PK_BookingId)
        {
            Plot model = new Plot();
            model.Discount = "0";
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;
                    model.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsBookingDetails.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsBookingDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();

                    model.BookingAmount = dsBookingDetails.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.BranchID = dsBookingDetails.Tables[0].Rows[0]["BranchID"].ToString();
                    model.PaymentPlanID = dsBookingDetails.Tables[0].Rows[0]["PK_PlanID"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                    model.BlockID = dsBookingDetails.Tables[0].Rows[0]["PK_BlockID"].ToString();
                    model.PlotRate = dsBookingDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PaymentDate = dsBookingDetails.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                    model.PlotNumber = dsBookingDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                    model.PlotAmount = dsBookingDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsBookingDetails.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    model.TransactionNumber = dsBookingDetails.Tables[0].Rows[0]["TransactionNo"].ToString();
                    model.TransactionDate = dsBookingDetails.Tables[0].Rows[0]["TransactionDate"].ToString();
                    model.BankName = dsBookingDetails.Tables[0].Rows[0]["BankName"].ToString();
                    model.BankBranch = dsBookingDetails.Tables[0].Rows[0]["BankBranch"].ToString();
                    model.MLMLoginId = dsBookingDetails.Tables[0].Rows[0]["MLMLoginId"].ToString();

                }
            }
            else
            {
                model.BookingDate = DateTime.Now.ToString("dd/MM/yyyy");
                model.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");

                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                ViewBag.ddlSector = ddlSector;
                #endregion

                #region BlockList
                int blockcount = 0;
                List<SelectListItem> lstBlock = new List<SelectListItem>();
                Master objmodel = new Master();
                DataSet dsblock = model.GetBlockList();

                if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                {
                    if (blockcount == 0)
                    {
                        lstBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                    }
                    foreach (DataRow dr in dsblock.Tables[0].Rows)
                    {
                        lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    }

                }

                ViewBag.ddlBlock = lstBlock;
                #endregion
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    //if (count == 0)
                    //{
                    ddlBranch.Add(new SelectListItem { Text = "Lucknow", Value = "1" });
                    //}
                    //ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion
            List<SelectListItem> ddlPLC = Common.PLC();
            ViewBag.ddlPLC = ddlPLC;
            #region ddlRate
            List<SelectListItem> ddlRate = Common.BindDdlRate();
            ViewBag.ddlRate = ddlRate;
            #endregion ddlRate

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = obj.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion

            #region ddlType
            List<SelectListItem> ddlType = Common.BindDdlType();
            ViewBag.ddlType = ddlType;
            #endregion ddlType

            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();

            DataSet dsPayMode = obj.GetPaymentModeList();
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



            return View(model);
        }
        public ActionResult Edit(string id)
        {
            Plot model = new Plot();
            model.Discount = "0";
            if (id != null)
            {
                model.PK_BookingId = Crypto.Decrypt(id);
                DataSet dsBookingDetails = model.GetBooking();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = id;
                    model.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsBookingDetails.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsBookingDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsBookingDetails.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.BranchID = dsBookingDetails.Tables[0].Rows[0]["BranchID"].ToString();
                    model.PaymentPlanID = dsBookingDetails.Tables[0].Rows[0]["PK_PlanID"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    model.BlockID = dsBookingDetails.Tables[0].Rows[0]["PK_BlockID"].ToString();
                    model.PlotRate = dsBookingDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PaymentDate = dsBookingDetails.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                    model.PlotNumber = dsBookingDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                    model.PlotAmount = dsBookingDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsBookingDetails.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    model.TransactionNumber = dsBookingDetails.Tables[0].Rows[0]["TransactionNo"].ToString();
                    model.TransactionDate = dsBookingDetails.Tables[0].Rows[0]["TransactionDate"].ToString();
                    model.BankName = dsBookingDetails.Tables[0].Rows[0]["BankName"].ToString();
                    model.BankBranch = dsBookingDetails.Tables[0].Rows[0]["BankBranch"].ToString();
                    model.PlotSize = dsBookingDetails.Tables[0].Rows[0]["PlotSize"].ToString();
                    // model.MLMLoginId = dsBookingDetails.Tables[0].Rows[0]["MLMLoginId"].ToString();
                }
                #region BlockList
                List<SelectListItem> lstBlock = new List<SelectListItem>();
                Master objmodel = new Master();
                objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                DataSet dsblock = model.GetBlockList();

                if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in dsblock.Tables[0].Rows)
                    {
                        lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    }

                }
                #region ddlBranch
                Plot obj = new Plot();
                int count = 0;
                List<SelectListItem> ddlBranch = new List<SelectListItem>();
                DataSet dsBranch = obj.GetBranchList();
                if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsBranch.Tables[0].Rows)
                    {
                        //if (count == 0)
                        //{
                        ddlBranch.Add(new SelectListItem { Text = "Lucknow", Value = "1" });
                        //}
                        //ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                        count = count + 1;
                    }
                }
                ViewBag.ddlBranch = ddlBranch;
                #endregion
                ViewBag.ddlBlock = lstBlock;
                #endregion
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                ViewBag.ddlSector = ddlSector;
                #endregion
                List<SelectListItem> ddlPLC = Common.PLC();
                ViewBag.ddlPLC = ddlPLC;
                #region ddlRate
                List<SelectListItem> ddlRate = Common.BindDdlRate();
                ViewBag.ddlRate = ddlRate;
                #endregion ddlRate

                #region ddlSite
                int count1 = 0;
                List<SelectListItem> ddlSite = new List<SelectListItem>();
                DataSet dsSite = model.GetSiteList();
                if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsSite.Tables[0].Rows)
                    {
                        ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                        count1 = count1 + 1;

                    }
                }
                ViewBag.ddlSite = ddlSite;
                #endregion

                #region ddlPlan
                int count2 = 0;
                List<SelectListItem> ddlPlan = new List<SelectListItem>();
                model.PaymentPlanID = dsBookingDetails.Tables[0].Rows[0]["PK_PlanID"].ToString();
                DataSet dsPlan = model.GetPaymentPlanListEdit();
                if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsPlan.Tables[0].Rows)
                    {
                        if (model.PaymentPlanID == r["PK_PLanID"].ToString())
                        {
                            ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString(), Selected = true });
                        }
                        else
                        {
                            ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                        }
                        count2 = count2 + 1;
                    }
                }
                ViewBag.ddlPlan = ddlPlan;
                #endregion
                #region ddlPaymentMode
                int count3 = 0;
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                DataSet dsPayMode = model.GetPaymentModeList();
                if (dsPayMode != null && dsPayMode.Tables.Count > 0 && dsPayMode.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsPayMode.Tables[0].Rows)
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                        count3 = count3 + 1;
                    }
                }
                ViewBag.ddlPaymentMode = ddlPaymentMode;
                #endregion
                #region ddlType
                List<SelectListItem> ddlType = Common.BindDdlType();
                ViewBag.ddlType = ddlType;
                #endregion ddlType
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("Edit")]
        [OnAction(ButtonName = "UpdateBooking")]
        public ActionResult Update(Plot model)
        {
            string FormName = "";
            string Controller = "";
            model.PK_BookingId = Crypto.Decrypt(model.PK_BookingId);
            try
            {
                model.Discount = string.IsNullOrEmpty(model.Discount) ? "0" : model.Discount;
                model.BookingDate = string.IsNullOrEmpty(model.BookingDate) ? null : Common.ConvertToSystemDate(model.BookingDate, "dd/MM/yyyy");
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Update"] = " Booking Update successfully ";

                    }
                    else
                    {
                        TempData["Update"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            FormName = "PlotBookingList";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);

        }
        public ActionResult BindType(Plot obj)
        {
            #region ddlType
            List<SelectListItem> ddlType = Common.BindDdlType();
            ViewBag.ddlType = ddlType;

            obj.ddlType = ddlType;
            #endregion ddlType
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult checkplotnumber(string PlotNumber)
        {
            Plot obj = new Plot();
            obj.PlotNumber = PlotNumber;
            DataSet ds = obj.CheckPlotNumber();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    obj.PlotSize = ds.Tables[0].Rows[0]["PlotSize"].ToString();
                    obj.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    obj.PlotID = ds.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    obj.Result = "1";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    obj.Result = "Invalid Plot Number";
                }
            }
            else
            {
                obj.Result = "Invalid Plot Number";

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindData()
        {
            Plot obj = new Plot();
            #region ddlRate
            List<SelectListItem> ddlRate = Common.BindDdlRate();
            ViewBag.ddlRate = ddlRate;
            obj.ddlRate = ddlRate;
            #endregion ddlRate

            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = obj.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            obj.ddlPlan = ddlPlan;
            #endregion

            List<SelectListItem> ddlPLC = Common.PLC();
            ViewBag.ddlPLC = ddlPLC;
            obj.ddlPLC = ddlPLC;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindPlan(Plot obj)
        {
            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = obj.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            obj.ddlPlan = ddlPlan;
            #endregion
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Plot model = new Plot();
                model.SiteID = SiteID;

                #region GetSiteRate
                //DataSet dsSiteRate = model.GetSiteList();
                //if (dsSiteRate != null)
                //{
                //    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                //    model.Result = "yes";
                //}
                #endregion
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                model.Result = "yes";
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                model.ddlSector = ddlSector;
                #endregion
                //#region SitePLCCharge
                //List<Master> lstPlcCharge = new List<Master>();
                //DataSet dsPlcCharge = model.GetPLCChargeList();

                //if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                //{
                //    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                //    {
                //        Master obj = new Master();
                //        obj.SiteName = r["SiteName"].ToString();
                //        obj.PLCName = r["PLCName"].ToString();
                //        obj.PLCCharge = r["PLCCharge"].ToString();
                //        obj.PLCID = r["PK_PLCID"].ToString();

                //        lstPlcCharge.Add(obj);
                //    }
                //    model.lstPLC = lstPlcCharge;
                //}
                //#endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetBlockList(string SiteID, string SectorID)
        {
            List<SelectListItem> lstBlock = new List<SelectListItem>();
            Master model = new Master();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            DataSet dsblock = model.GetBlockList();

            #region ddlBlock
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }

            model.lstBlock = lstBlock;
            #endregion

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckPlot(string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            Plot model = new Plot();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            DataSet dsblock = model.CheckPlotAvailibility();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = "no";
                }
                else
                {
                    model.Result = "yes";
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
            return Json(model, JsonRequestBehavior.AllowGet);
            //return View();
        }

        public ActionResult GetCustomerNameFromCustomerID(string CustomerID)
        {
            try
            {
                Plot model = new Plot();

                model.LoginId = CustomerID;

                #region GetCustomerName
                DataSet dsCustomerName = model.GetCustomerName();

                if (dsCustomerName != null && dsCustomerName.Tables[0].Rows.Count > 0)
                {
                    if (dsCustomerName.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.CustomerName = dsCustomerName.Tables[0].Rows[0]["Name"].ToString();
                        model.LoginId = dsCustomerName.Tables[0].Rows[0]["LoginId"].ToString();
                        model.AssociateID = dsCustomerName.Tables[0].Rows[0]["AssociateLoginId"].ToString();
                        model.AssociateName = dsCustomerName.Tables[0].Rows[0]["AssociateName"].ToString();
                        model.Result = "yes";
                    }
                    else if (dsCustomerName.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        model.Result = "AlreadyBooking";
                    }
                    else
                    {
                        model.CustomerID = "";
                        model.Result = "no";
                    }

                }

                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public ActionResult GetAssociateName(string AssociateID)
        {
            try
            {
                Plot model = new Plot();
                model.LoginId = AssociateID;

                #region GetSiteRate
                DataSet dsAssociateName = model.GetSponsorName();
                if (dsAssociateName != null && dsAssociateName.Tables[0].Rows.Count > 0)
                {
                    model.AssociateName = dsAssociateName.Tables[0].Rows[0]["Name"].ToString();
                    model.UserID = dsAssociateName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.AssociateName = "";
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
        [ActionName("PlotBooking")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotBooking(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.Discount = string.IsNullOrEmpty(obj.Discount) ? "0" : obj.Discount;
                obj.BookingDate = string.IsNullOrEmpty(obj.BookingDate) ? null : Common.ConvertToSystemDate(obj.BookingDate, "dd/MM/yyyy");
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SavePlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Plot"] = " Plot has been booked successfully ";
                        TempData["Booking"] = "Booking ID : " + ds.Tables[0].Rows[0]["BookingNo"].ToString();

                        string Bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        string Bookamt = ds.Tables[0].Rows[0]["BookingAmt"].ToString();
                        string AsstName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                        string plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string str = BLSMS.SendSMSNew(mob, "Dear " + AsstName + ", Your " + TempData["Plot"] + ". Your Booking No is " + ds.Tables[0].Rows[0]["BookingNo"].ToString() + ".");
                        //string str = BLSMS.SendSMSNew(mob, TempData["Plot"] + ", Your Booking No is " + TempData["Booking"]);
                        try
                        {
                            BLSMS.SendSMS(mob, str);
                        }
                        catch
                        {
                        }

                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotBooking";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }


        public ActionResult PlotBookingList(Plot model)
        {

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {

                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = sectorcount + 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion
            #region ddlPaymentMode
            Plot obj = new Plot();
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
            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = model.GetBlockList();


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




            int count2 = 0;
            List<SelectListItem> ddlBookingType = new List<SelectListItem>();
            DataSet dlBookingType = model.GetPlanList();
            if (dlBookingType != null && dlBookingType.Tables.Count > 0 && dlBookingType.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dlBookingType.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlBookingType.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlBookingType.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PlanID"].ToString() });
                    count2 = count2 + 1;

                }
            }
            ViewBag.ddlBookingType = ddlBookingType;



            return View(model);
        }

        [HttpPost]
        [ActionName("PlotBookingList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetBookingList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PaymentPlanID = model.PaymentPlanID == "0" ? null : model.PaymentPlanID;
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetBookingDetailsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
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
                        obj.PaymentMode = r["PaymentMode"].ToString();
                        obj.PlotRate = r["PlotRate"].ToString();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.Type = r["Type"].ToString();
                        obj.EncryptKey = Crypto.Encrypt(r["PK_BookingId"].ToString());
                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
            #region ddlPaymentMode
            Plot obj12 = new Plot();
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj12.GetPaymentMode();
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

            int count2 = 0;
            List<SelectListItem> ddlBookingType = new List<SelectListItem>();
            DataSet dlBookingType = model.GetPlanList();
            if (dlBookingType != null && dlBookingType.Tables.Count > 0 && dlBookingType.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dlBookingType.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlBookingType.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlBookingType.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PlanID"].ToString() });
                    count2 = count2 + 1;

                }
            }
            ViewBag.ddlBookingType = ddlBookingType;

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {

                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = sectorcount + 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion
            //List<SelectListItem> ddlSector = new List<SelectListItem>();
            //ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            //ViewBag.ddlSector = ddlSector;
            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = model.GetBlockList();


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

            return View(model);
        }
        #endregion

        #region SaveRowHouseBooking
        public ActionResult RowHouseBooking(string RowHouseBookingID)
        {
            Plot model = new Plot();
            model.Discount = "0";
            if (RowHouseBookingID != null)
            {
                model.RowHouseBookingID = RowHouseBookingID;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.RowHouseBookingID = RowHouseBookingID;
                    model.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsBookingDetails.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsBookingDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();

                    model.BookingAmount = dsBookingDetails.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.BranchID = dsBookingDetails.Tables[0].Rows[0]["BranchID"].ToString();
                    model.PaymentPlanID = dsBookingDetails.Tables[0].Rows[0]["PK_PlanID"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    //  model.PlotID = dsBookingDetails.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();

                    model.PlotRate = dsBookingDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PaymentDate = dsBookingDetails.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                    //  model.PlotNumber = dsBookingDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                    //  model.PlotAmount = dsBookingDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                    //   model.ActualPlotRate = dsBookingDetails.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    //    model.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    model.TransactionNumber = dsBookingDetails.Tables[0].Rows[0]["TransactionNo"].ToString();
                    model.TransactionDate = dsBookingDetails.Tables[0].Rows[0]["TransactionDate"].ToString();
                    model.BankName = dsBookingDetails.Tables[0].Rows[0]["BankName"].ToString();
                    model.BankBranch = dsBookingDetails.Tables[0].Rows[0]["BankBranch"].ToString();
                    //     model.MLMLoginId = dsBookingDetails.Tables[0].Rows[0]["MLMLoginId"].ToString();
                }
            }
            else
            {
                model.BookingDate = DateTime.Now.ToString("dd/MM/yyyy");
                model.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");


            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
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

            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = obj.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion

            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
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

            List<SelectListItem> ddlPlotArea = new List<SelectListItem>();
            ddlPlotArea.Add(new SelectListItem { Text = "Select Plot Area", Value = "0" });
            ViewBag.ddlPlotArea = ddlPlotArea;
            return View(model);
        }

        public ActionResult GetRateandPLC(string SiteID)
        {
            try
            {
                Plot model = new Plot();
                model.SiteID = SiteID;


                #region GetPlotArea
                List<SelectListItem> ddlPlotArea = new List<SelectListItem>();
                model.Result = "1";
                DataSet dsSector = model.GetRateandPLC();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlPlotArea.Add(new SelectListItem { Text = r["Area"].ToString(), Value = r["PK_RowHouseSizeID"].ToString() });

                    }
                }
                model.ddlPlotArea = ddlPlotArea;
                #endregion

                #region

                List<Plot> lst = new List<Plot>();


                if (dsSector != null && dsSector.Tables.Count > 0 && dsSector.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[1].Rows)
                    {
                        Plot obj = new Plot();
                        obj.PK_PLCCharge = r["PK_PLCCharge"].ToString();
                        obj.PLCName = r["PLCName"].ToString();
                        obj.TotalPLC = r["PLCCharge"].ToString();

                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                    #endregion
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return View();
        }


        public ActionResult GetRate(string PlotArea)
        {

            Plot model = new Plot();
            model.PlotArea = PlotArea;


            DataSet dsSiteRate = model.GetRate();
            if (dsSiteRate != null)
            {
                model.PlotRate = dsSiteRate.Tables[0].Rows[0]["Price"].ToString();
                model.BookingAmount = dsSiteRate.Tables[0].Rows[0]["BookingAmount"].ToString();
                model.BookingPercent = dsSiteRate.Tables[0].Rows[0]["BookingPercentage"].ToString();
                model.Result = "1";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("RowHouseBooking")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveRowHouse(Plot obj)
        {
            string FormName = "";
            string Controller = "";



            obj.Discount = string.IsNullOrEmpty(obj.Discount) ? "0" : obj.Discount;

            obj.BookingDate = string.IsNullOrEmpty(obj.BookingDate) ? null : Common.ConvertToSystemDate(obj.BookingDate, "dd/MM/yyyy");
            obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
            obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
            obj.PlotArea = string.IsNullOrEmpty(obj.PlotArea) ? "0" : obj.PlotArea;
            obj.GroundFloorArea = string.IsNullOrEmpty(obj.GroundFloorArea) ? "0" : obj.GroundFloorArea;
            obj.FirstFloorArea = string.IsNullOrEmpty(obj.FirstFloorArea) ? "0" : obj.FirstFloorArea;

            try
            {

                string noofrows = Request["hdrows"].ToString();
                string plc = "";
                string chk = "";
                string plccharge = "";
                DataTable dtst = new DataTable();

                dtst.Columns.Add("FK_PLCID", typeof(string));
                dtst.Columns.Add("PLCCharge", typeof(string));

                for (int i = 0; i < int.Parse(noofrows) - 1; i++)
                {
                    plccharge = Request["plccharge_" + i].ToString();
                    if (plccharge != "0" && plccharge != "")
                    {
                        plc = Request["Pk_Plcid_" + i].ToString();
                        plccharge = Request["plccharge_" + i].ToString();
                        dtst.Rows.Add(plc, plccharge);
                    }


                }

                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.dtTable = dtst;

                DataSet ds = obj.SaveRowHouse();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["RowHouse"] = " Row House Saved successfully !";
                        TempData["Booking"] = "Booking ID : " + ds.Tables[0].Rows[0]["BookingNo"].ToString();


                        string Bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        string Bookamt = ds.Tables[0].Rows[0]["BookingAmt"].ToString();
                        string AsstName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                        string plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string str = BLSMS.HouseBooking(Bookno, Bookamt, AsstName, plot);
                        try
                        {
                            BLSMS.SendSMS(mob, str);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["RowHouse"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["RowHouse"] = ex.Message;
            }
            FormName = "RowHouseBooking";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        #endregion

        #region  Plot Allotment
        public ActionResult PlotAllotment(string PK_BookingId)
        {

            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion


                }
            }
            else
            {

                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                DataSet dsSector = model.GetSectorList();
                int sectorcount = 0;

                if (dsSector != null && dsSector.Tables.Count > 0)
                {

                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {

                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                        sectorcount = sectorcount + 1;
                    }

                }

                ViewBag.ddlSector = ddlSector;
                #endregion

                #region BlockList
                List<SelectListItem> lstBlock = new List<SelectListItem>();

                int blockcount = 0;
                //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
                //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
                DataSet dsblock1 = model.GetBlockList();


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
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion


            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
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
            return View(model);
        }


        public ActionResult PlotBookingDetails(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();
            //model.SiteID = SiteID == "0" ? null : model.SiteID;
            //model.SectorID = SectorID == "0" ? null : model.SectorID;
            //model.BlockID = BlockID == "0" ? null : model.BlockID;
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.FillBookedPlotDetails();
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
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                }
                else
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        [ActionName("PlotAllotment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotAllotment(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                //obj.TransactionDate = obj.TransactionDate == "" || obj.TransactionDate = null&&(Convert.obj.TransactionDate);

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PaymentDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.PaymentDate) ? null : obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SavePlotAllotment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = " Plot Allotted successfully !";
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string amt = obj.PaidAmount;

                        string str = BLSMS.PlotAllotment(name, Plot, amt);
                        try
                        {
                            BLSMS.SendSMSNew(mob, str);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotAllotment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        #endregion


        #region EMIPayment

        public ActionResult EMIPayment(string PK_BookingId)
        {

            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                }
            }
            else
            {

                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                DataSet dsSector = model.GetSectorList();
                int sectorcount = 0;

                if (dsSector != null && dsSector.Tables.Count > 0)
                {

                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {

                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                        sectorcount = sectorcount + 1;
                    }
                }

                ViewBag.ddlSector = ddlSector;
                #endregion

                #region BlockList
                List<SelectListItem> lstBlock = new List<SelectListItem>();

                int blockcount = 0;
                //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
                //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
                DataSet dsblock1 = model.GetBlockList();


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
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion


            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = obj.GetPaymentModeList();
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
            return View(model);
        }

        public ActionResult Details(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.FillBookedPlotDetailsForEmi();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {
                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {
                    model.Result = "yes";
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsblock.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PlotRate = dsblock.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.PayAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.BookingDate = dsblock.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsblock.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.PaymentDate = DateTime.Now.ToString("dd/MM/yyyy");
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
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in dsblock.Tables[1].Rows)
                        {
                            Plot obj = new Plot();
                            obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                            obj.PK_BookingId = r["Fk_BookingId"].ToString();
                            obj.InstallmentNo = r["InstallmentNo"].ToString();
                            obj.InstallmentDate = r["InstallmentDate"].ToString();
                            obj.PaymentDate = r["PaymentDate"].ToString();
                            obj.PaidAmount = r["PaidAmount"].ToString();
                            obj.InstallmentAmount = r["InstAmt"].ToString();
                            obj.PaymentMode = r["PaymentModeName"].ToString();
                            obj.DueAmount = r["DueAmount"].ToString();
                            obj.CssClass = r["CssClass"].ToString();

                            lst.Add(obj);
                        }
                        model.lstPlot = lst;
                    }

                }
                else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ActionName("EMIPayment")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveEMI(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                //obj.TransactionDate = obj.TransactionDate == "" || obj.TransactionDate = null&&(Convert.obj.TransactionDate);

                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PaymentDate = Common.ConvertToSystemDate(string.IsNullOrEmpty(obj.PaymentDate) ? null : obj.PaymentDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveEMIPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = " EMI Paid !";
                        string name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        string instno = ds.Tables[0].Rows[0]["InstallmentNo"].ToString();
                        string amt = obj.PaidAmount;

                        string str = BLSMS.EMIPayment(name, Plot, bookno, instno, amt);
                        try
                        {
                            BLSMS.SendSMSNew(mob, str);
                        }
                        catch { }
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "EMIPayment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region Cancel Plot

        public ActionResult PlotBookingListForCancel(Plot model)
        {

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {

                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {

                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = sectorcount + 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = model.GetBlockList();


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

            return View(model);
        }

        [HttpPost]
        [ActionName("PlotBookingListForCancel")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetBookingList1(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetBookingDetailsList1();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Plot obj = new Plot();
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

                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {

                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {

                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = sectorcount + 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = model.GetBlockList();


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
            return View(model);
        }


        [HttpPost]
        [ActionName("SavePlotBooking")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdatePlotBooking(Plot model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (model.TransactionDate == "")
                {
                    model.TransactionDate = null;
                }
                else
                {
                    model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/mm/yyyy");
                }
                model.BookingDate = string.IsNullOrEmpty(model.BookingDate) ? null : Common.ConvertToSystemDate(model.BookingDate, "dd/mm/yyyy");
                model.PaymentDate = string.IsNullOrEmpty(model.PaymentDate) ? null : Common.ConvertToSystemDate(model.PaymentDate, "dd/mm/yyyy");

                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.UpdatePlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot updated successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotBooking";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult CancelPlotBooking(string BookingID, string Remark)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.PK_BookingId = BookingID;
                model.CancelRemark = Remark;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.CancelPlotBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Plot Booking Cancelled successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotBookingListForCancel";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult CancelledPlotBookingList(Plot model)
        {

            return View(model);
        }

        [HttpPost]
        [ActionName("CancelledPlotBookingList")]
        [OnAction(ButtonName = "SearchCancelled")]
        public ActionResult GetCancelledBookingList(Plot model)
        {
            List<Plot> lst = new List<Plot>();

            DataSet ds = model.GetCancelledBookingDetailsList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingId = r["PK_BookingId"].ToString();
                    obj.BranchID = r["BranchName"].ToString();
                    obj.CustomerName = r["CustomerInfo"].ToString();
                    obj.AssociateName = r["AssociateInfo"].ToString();
                    obj.PlotNumber = r["Plot"].ToString();
                    obj.BookingDate = r["BookingDate"].ToString();
                    obj.BookingAmount = r["BookingAmt"].ToString();
                    obj.PaymentPlanID = r["PlanName"].ToString();
                    obj.BookingStatus = r["BookingStatus"].ToString();
                    obj.CancelRemark = r["CancelRemark"].ToString();
                    obj.CancelDate = r["CancelledDate"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        #endregion

        #region  HoldPlot
        public ActionResult HoldPlot(string PK_PlotHoldID)
        {
            Plot model = new Plot();
            if (PK_PlotHoldID != null)
            {


                model.PK_PlotHoldID = PK_PlotHoldID;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_PlotHoldID;
                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();

                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                    model.BlockID = dsBookingDetails.Tables[0].Rows[0]["FK_BlockID"].ToString();
                    model.HoldFrom = dsBookingDetails.Tables[0].Rows[0]["HoldFrom"].ToString();
                    model.HoldTo = dsBookingDetails.Tables[0].Rows[0]["HoldTo"].ToString();
                    model.Name = dsBookingDetails.Tables[0].Rows[0]["Name"].ToString();
                    model.Mobile = dsBookingDetails.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Remark = dsBookingDetails.Tables[0].Rows[0]["Remark1"].ToString();
                }
            }
            else
            {
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                DataSet dsSector = model.GetSectorList();
                int sectorcount = 0;

                if (dsSector != null && dsSector.Tables.Count > 0)
                {

                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {

                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                        sectorcount = sectorcount + 1;
                    }
                }

                ViewBag.ddlSector = ddlSector;
                #endregion

                #region BlockList
                List<SelectListItem> lstBlock = new List<SelectListItem>();

                int blockcount = 0;
                //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
                //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
                DataSet dsblock1 = model.GetBlockList();


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
            }
            #region ddlBranch
            Plot obj = new Plot();
            int count = 0;
            List<SelectListItem> ddlBranch = new List<SelectListItem>();
            DataSet dsBranch = obj.GetBranchList();
            if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsBranch.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlBranch.Add(new SelectListItem { Text = "Select Branch", Value = "0" });
                    }
                    ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlBranch = ddlBranch;
            #endregion

            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {

                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
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
                    else
                    {
                        ddlPaymentMode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    }
                    count3 = count3 + 1;
                }
            }
            ViewBag.ddlPaymentMode = ddlPaymentMode;
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("HoldPlot")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePlotHold(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SavePlotHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = " Plot is on Hold !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "HoldPlot";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PlotHoldList(Plot model)
        {
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {

                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {

                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = sectorcount + 1;
                }
            }

            ViewBag.ddlSector = ddlSector;
            #endregion

            #region BlockList
            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock1 = model.GetBlockList();


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
            return View(model);
        }

        [HttpPost]
        [ActionName("PlotHoldList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetPlotHoldList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetPlotHoldList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
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
                model.lstPlot = lst;
            }
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = model.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
            #endregion

            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
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
            DataSet dsblock = model.GetBlockList();


            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {

                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            return View(model);
            #endregion
        }



        public ActionResult DeletePlotHold(string PK_PlotHoldID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.PK_PlotHoldID = PK_PlotHoldID;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeletePlotHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Deleted successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotHoldList";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult ConvertBooking(string PK_PlotHoldID)
        {
            Plot model = new Plot();
            if (PK_PlotHoldID != null)
            {

                model.PK_PlotHoldID = PK_PlotHoldID;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet dsBookingDetails = model.ConvertToBooking();
                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    //model.PK_BookingId = PK_BookingId;
                    model.CustomerID = dsBookingDetails.Tables[0].Rows[0]["CustomerId"].ToString();
                    model.CustomerName = dsBookingDetails.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.AssociateID = dsBookingDetails.Tables[0].Rows[0]["AssociateId"].ToString();
                    model.AssociateName = dsBookingDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.BookingAmount = dsBookingDetails.Tables[0].Rows[0]["BookingAmt"].ToString();
                    model.BranchID = dsBookingDetails.Tables[0].Rows[0]["BranchID"].ToString();
                    model.PaymentPlanID = dsBookingDetails.Tables[0].Rows[0]["PK_PlanID"].ToString();
                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["fk_plotid"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                    model.BlockID = dsBookingDetails.Tables[0].Rows[0]["PK_BlockID"].ToString();
                    model.PlotRate = dsBookingDetails.Tables[0].Rows[0]["PlotRate"].ToString();
                    model.BookingDate = dsBookingDetails.Tables[0].Rows[0]["BookingDate"].ToString();
                    model.PaymentDate = dsBookingDetails.Tables[0].Rows[0]["PaymentDate"].ToString();
                    model.Discount = dsBookingDetails.Tables[0].Rows[0]["Discount"].ToString();
                    model.PaymentMode = dsBookingDetails.Tables[0].Rows[0]["PaymentMode"].ToString();
                    model.PlotNumber = dsBookingDetails.Tables[0].Rows[0]["PlotNumber"].ToString();
                    model.PlotAmount = dsBookingDetails.Tables[0].Rows[0]["PlotAmount"].ToString();
                    model.ActualPlotRate = dsBookingDetails.Tables[0].Rows[0]["ActualPlotRate"].ToString();
                    model.PayAmount = dsBookingDetails.Tables[0].Rows[0]["PaidAmount"].ToString();
                    model.TotalPLC = dsBookingDetails.Tables[0].Rows[0]["PLCCharge"].ToString();
                    model.TransactionNumber = dsBookingDetails.Tables[0].Rows[0]["TransactionNo"].ToString();
                    model.TransactionDate = dsBookingDetails.Tables[0].Rows[0]["TransactionDate"].ToString();
                    model.BankName = dsBookingDetails.Tables[0].Rows[0]["BankName"].ToString();
                    model.BankBranch = dsBookingDetails.Tables[0].Rows[0]["BankBranch"].ToString();
                    model.FK_SponsorId = dsBookingDetails.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                    model.PlotSize = dsBookingDetails.Tables[0].Rows[0]["PlotSize"].ToString();
                    //model.MLMLoginId = dsBookingDetails.Tables[0].Rows[0]["MLMLoginId"].ToString();
                }
                #region BlockList
                List<SelectListItem> lstBlock = new List<SelectListItem>();
                Master objmodel = new Master();
                objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["PK_SiteID"].ToString();
                objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["PK_SectorID"].ToString();
                DataSet dsblock = model.GetBlockList();

                if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dr in dsblock.Tables[0].Rows)
                    {
                        lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    }

                }
                #region ddlBranch
                Plot obj = new Plot();
                int count = 0;
                List<SelectListItem> ddlBranch = new List<SelectListItem>();
                DataSet dsBranch = obj.GetBranchList();
                if (dsBranch != null && dsBranch.Tables.Count > 0 && dsBranch.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsBranch.Tables[0].Rows)
                    {
                        //if (count == 0)
                        //{
                        ddlBranch.Add(new SelectListItem { Text = "Lucknow", Value = "1" });
                        //}
                        //ddlBranch.Add(new SelectListItem { Text = r["BranchName"].ToString(), Value = r["PK_BranchID"].ToString() });
                        count = count + 1;
                    }
                }
                ViewBag.ddlBranch = ddlBranch;
                #endregion
                ViewBag.ddlBlock = lstBlock;
                #endregion
                #region GetSectors
                List<SelectListItem> ddlSector = new List<SelectListItem>();
                DataSet dsSector = model.GetSectorList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                    }
                }
                ViewBag.ddlSector = ddlSector;
                #endregion
                List<SelectListItem> ddlPLC = Common.PLC();
                ViewBag.ddlPLC = ddlPLC;
                #region ddlRate
                List<SelectListItem> ddlRate = Common.BindDdlRate();
                ViewBag.ddlRate = ddlRate;
                #endregion ddlRate

                #region ddlSite
                int count1 = 0;
                List<SelectListItem> ddlSite = new List<SelectListItem>();
                DataSet dsSite = model.GetSiteList();
                if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsSite.Tables[0].Rows)
                    {
                        ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                        count1 = count1 + 1;

                    }
                }
                ViewBag.ddlSite = ddlSite;
                #endregion

                #region ddlPlan
                int count2 = 0;
                List<SelectListItem> ddlPlan = new List<SelectListItem>();
                DataSet dsPlan = model.GetPaymentPlanList();
                if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsPlan.Tables[0].Rows)
                    {
                        if (count2 == 0)
                        {
                            ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                        }
                        ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                        count2 = count2 + 1;
                    }
                }
                ViewBag.ddlPlan = ddlPlan;
                #endregion
                #region ddlPaymentMode
                int count3 = 0;
                List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
                DataSet dsPayMode = model.GetPaymentModeListforconvert();
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
                #region ddlType
                List<SelectListItem> ddlType = Common.BindDdlType();
                ViewBag.ddlType = ddlType;
                #endregion ddlType


            }
            else
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ConvertBooking")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveConvertBooking(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.Discount = string.IsNullOrEmpty(obj.Discount) ? "0" : obj.Discount;
                obj.BookingDate = string.IsNullOrEmpty(obj.BookingDate) ? null : Common.ConvertToSystemDate(obj.BookingDate, "dd/MM/yyyy");
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveConvertBooking();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["Plot"] = " Plot has been booked successfully ";
                        TempData["ConvertBookingMessage"] = "Booking ID : " + ds.Tables[0].Rows[0]["BookingNo"].ToString();

                        //string Bookno = ds.Tables[0].Rows[0]["BookingNo"].ToString();
                        //string Bookamt = ds.Tables[0].Rows[0]["BookingAmt"].ToString();
                        //string AsstName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                        //string plot = ds.Tables[0].Rows[0]["Plot"].ToString();
                        //string mob = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        //string str = BLSMS.SendSMSNew(mob, "Dear " + AsstName + ", Your " + TempData["Plot"] + ". Your Booking No is " + ds.Tables[0].Rows[0]["BookingNo"].ToString() + ".");
                        ////string str = BLSMS.SendSMSNew(mob, TempData["Plot"] + ", Your Booking No is " + TempData["Booking"]);
                        //try
                        //{
                        //    BLSMS.SendSMS(mob, str);
                        //}
                        //catch
                        //{
                        //}

                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ConvertBooking"] = ex.Message;
            }
            FormName = "PlotHoldList";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult PrintPlotHold(string id)
        {
            Plot newdata = new Plot();
            newdata.EncryptKey = Crypto.Decrypt(id);
            newdata.PK_PlotHoldID = Crypto.Decrypt(id);
            //  ViewBag.Name = Session["Name"].ToString();
            DataSet ds = newdata.GetPlotHoldList();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    newdata.Result = "yes";
                    ViewBag.PK_PlotHoldID = ds.Tables[0].Rows[0]["PK_PlotHoldID"].ToString();
                    ViewBag.CustomerName = ds.Tables[0].Rows[0]["Name"].ToString();

                    ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    ViewBag.SectorName = ds.Tables[0].Rows[0]["SectorName"].ToString();
                    ViewBag.BlockName = ds.Tables[0].Rows[0]["BlockName"].ToString();
                    ViewBag.PlotNo = ds.Tables[0].Rows[0]["PlotNumber"].ToString();

                    //  ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotInfo"].ToString();
                    ViewBag.HoldAmount = ds.Tables[0].Rows[0]["HoldAmount"].ToString();
                    ViewBag.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    ViewBag.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();

                    ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                    ViewBag.Amountwords = ds.Tables[0].Rows[0]["AmountInWords"].ToString();



                    ViewBag.CompanyName = SoftwareDetails.CompanyName;
                    ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                    ViewBag.Pin1 = SoftwareDetails.Pin1;
                    ViewBag.State1 = SoftwareDetails.State1;
                    ViewBag.City1 = SoftwareDetails.City1;
                    ViewBag.ContactNo = SoftwareDetails.ContactNo;
                    ViewBag.LandLine = SoftwareDetails.LandLine;
                    ViewBag.Website = SoftwareDetails.Website;
                    ViewBag.EmailID = SoftwareDetails.EmailID;
                }
            }

            return View(newdata);
        }
        #endregion

        #region UploadPlot/HouseDocument

        public ActionResult UploadPlotDocument(string PK_BookingId)
        {

            Plot model = new Plot();
            if (PK_BookingId != null)
            {
                model.PK_BookingId = PK_BookingId;
                DataSet dsBookingDetails = model.GetBookingDetailsList();

                if (dsBookingDetails != null && dsBookingDetails.Tables.Count > 0)
                {
                    model.PK_BookingId = PK_BookingId;

                    model.PlotID = dsBookingDetails.Tables[0].Rows[0]["Fk_PlotId"].ToString();
                    model.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();


                    #region GetSectors
                    List<SelectListItem> ddlSector = new List<SelectListItem>();
                    DataSet dsSector = model.GetSectorList();

                    if (dsSector != null && dsSector.Tables.Count > 0)
                    {
                        foreach (DataRow r in dsSector.Tables[0].Rows)
                        {
                            ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });

                        }
                    }
                    ViewBag.ddlSector = ddlSector;
                    #endregion
                    model.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    #region BlockList
                    List<SelectListItem> lstBlock = new List<SelectListItem>();
                    Master objmodel = new Master();
                    objmodel.SiteID = dsBookingDetails.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    objmodel.SectorID = dsBookingDetails.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    DataSet dsblock = model.GetBlockList();

                    if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dr in dsblock.Tables[0].Rows)
                        {
                            lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                        }

                    }

                    ViewBag.ddlBlock = lstBlock;
                    #endregion

                }
            }
            else
            {

                List<SelectListItem> ddlSector = new List<SelectListItem>();
                ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
                ViewBag.ddlSector = ddlSector;

                List<SelectListItem> ddlBlock = new List<SelectListItem>();
                ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                ViewBag.ddlBlock = ddlBlock;
            }

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

            return View(model);
        }


        public ActionResult PlotBookingDetailsForDocument(string SiteID, string SectorID, string BlockID, string PlotNumber, string BookingNumber)
        {
            Plot model = new Plot();

            model.SiteID = SiteID;
            model.SectorID = SectorID;
            model.BlockID = BlockID;
            model.PlotNumber = PlotNumber;
            model.BookingNumber = BookingNumber;
            DataSet dsblock = model.PlotBookingDetailsForDocument();
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
                    model.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    model.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    model.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                    model.CustomerLoginID = dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    model.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString();

                }
                else
                {
                    model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "No record found !";
            }
            return Json(model, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        [ActionName("UploadPlotDocument")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveUploadPlotDocument(HttpPostedFileBase postedFile, Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (postedFile != null)
                {
                    obj.DocumentFile = "/PlotHouseDocument/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(obj.DocumentFile)));

                }
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.SaveUploadPlotDocument();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Doc"] = " Document Uploaded successfully !";

                    }
                    else
                    {
                        TempData["Doc"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Doc"] = ex.Message;
            }
            FormName = "UploadPlotDocument";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region  ChequNeftCash

        public ActionResult Payment(Plot model)

        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
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
            return View(model);
        }

        [HttpPost]
        [ActionName("Payment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetPaymentList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Details"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
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
            return View(model);
        }

        public ActionResult ApprovePayment(string UserID, string Description, string ApprovedDate)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();
                model.UserID = UserID;
                model.Description = Description;
                model.ApprovedDate = ApprovedDate;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Result = "yes";
                DataSet ds = model.ApprovePayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Payment Approved successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "Payment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);

        }
        public ActionResult RejectPayment(string UserID, string Description, string ApprovedDate)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.UserID = UserID;
                model.Description = Description;
                model.ApprovedDate = ApprovedDate;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.RejectPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Payment Rejected  !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "Payment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region ApproveRejectedPayment

        public ActionResult ApproveRejectedPayment(Plot model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
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
            return View(model);
        }

        [HttpPost]
        [ActionName("ApproveRejectedPayment")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetListOfRejectedPayment(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Details"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
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
            return View(model);
        }

        public ActionResult ApproveRejPayment(string UserID, string Description, string ApprovedDate)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.UserID = UserID;
                model.Description = Description;
                model.ApprovedDate = ApprovedDate;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.ApproveRejectPayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Payment Approved successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "ApproveRejectedPayment";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }

        #endregion

        #region PaymentApproval/RejectionReport

        public ActionResult PaymentReport(Plot model)
        {
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus
            return View(model);
        }

        [HttpPost]
        [ActionName("PaymentReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetReport(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetPaymentReportList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    //  obj.RejectedDate = r["RejectedDate"].ToString();
                    obj.ApproveDescription = r["Description"].ToString();
                    //  obj.RejectDescription = r["RejectDescription"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlpaymentStatus
            List<SelectListItem> ddlpaymentStatus = Common.BindPaymentStatus();
            ViewBag.ddlpaymentStatus = ddlpaymentStatus;
            #endregion ddlpaymentStatus
            return View(model);
        }


        #endregion

        #region RejectPaymentApproveReport

        public ActionResult RejectPaymentApproveReport(Plot model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
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
            return View(model);
        }

        [HttpPost]
        [ActionName("RejectPaymentApproveReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetRejectPaymentApproveReport(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.PaymentMode = model.PaymentMode == "0" ? null : model.PaymentMode;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetPaymentRejAppReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.UserID = r["PK_BookingDetailsId"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentStatus = r["PaymentStatus"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    // obj.ApproveDescription = r["ApproveDescription"].ToString();
                    obj.ApprovedDate = r["ApprovedDate"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    //   obj.RejectedDate = r["RejectedDate"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeList();
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
            return View(model);
        }


        #endregion
        public ActionResult LedgerReport(string BookingNumber)
        {
            string FormName = "";
            string Controller = "";
            AssociateBooking model = new AssociateBooking();
            model.LoginId = Session["LoginId"].ToString();
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            List<AssociateBooking> lstBlockList = new List<AssociateBooking>();
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
                FormName = "CustomerLedgerReport";
                Controller = "AssociateDashboard";
            }
            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock.Tables[1].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.PK_BookingId = r["Fk_BookingId"].ToString();
                    obj.InstallmentNo = r["InstallmentNo"].ToString();
                    obj.InstallmentDate = r["InstallmentDate"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.InstallmentAmount = r["InstAmt"].ToString();
                    obj.PaymentMode = r["PaymentModeName"].ToString();
                    obj.DueAmount = r["DueAmount"].ToString();
                    lstBlockList.Add(obj);
                    FormName = "CustomerLedgerReport";
                    Controller = "AssociateDashboard";
                }
                model.lstPlot = lstBlockList;
            }
            else
            {
                TempData["Login"] = "Data Not Found";

            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult UnholdPlot(string PK_PlotHoldID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Plot model = new Plot();

                model.PK_PlotHoldID = PK_PlotHoldID;
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.PlotUnHold();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Plot"] = "Unhold successfully !";
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "PlotHoldList";
            Controller = "Plot";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult GetCustomerDetail(string CustomerLoginID)
        {
            try
            {
                Customer model = new Customer();
                model.LoginID = CustomerLoginID;

                #region GetSiteRate
                DataSet dsSponsorName = model.GetCustomerDetail();
                if (dsSponsorName != null && dsSponsorName.Tables[0].Rows.Count > 0)
                {
                    model.UserID = dsSponsorName.Tables[0].Rows[0]["FK_SponsorId"].ToString();
                    model.SponsorID = dsSponsorName.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = dsSponsorName.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.CustomerId = dsSponsorName.Tables[0].Rows[0]["PK_UserID"].ToString();
                    model.CustomerLoginID = dsSponsorName.Tables[0].Rows[0]["CustomerId"].ToString();
                    model.CustomerName = dsSponsorName.Tables[0].Rows[0]["CustomerName"].ToString();
                    model.Contact = dsSponsorName.Tables[0].Rows[0]["Mobile"].ToString();
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

        public ActionResult PlotRegistration(Plot model)
        {
            #region GetSectors
            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = model.GetSectorList();
            if (dsSector != null && dsSector.Tables.Count > 0)
            {
                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                }
            }
            ViewBag.ddlSector = ddlSector;
            #endregion
            #region GetBock
            int blockcount = 0;
            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            DataSet dsblock = model.GetBlockList();

            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {
                if (blockcount == 0)
                {
                    ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
                }
                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {
                    ddlBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                }

            }
            ViewBag.ddlBlock = ddlBlock;
            #endregion
            #region ddlBranch
            Plot obj = new Plot();
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
            #region ddlSite
            int count1 = 0;
            List<SelectListItem> ddlSite = new List<SelectListItem>();
            DataSet dsSite = obj.GetSiteList();
            if (dsSite != null && dsSite.Tables.Count > 0 && dsSite.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsSite.Tables[0].Rows)
                {
                    //if (count1 == 0)
                    //{
                    //    ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    //}
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString(), Selected = true });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
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
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            List<SelectListItem> ddlPLC = Common.PLC();
            ViewBag.ddlPLC = ddlPLC;
            #region ddlRate
            List<SelectListItem> ddlRate = Common.BindDdlRate();
            ViewBag.ddlRate = ddlRate;
            #endregion ddlRate
            #region ddlPlan
            int count2 = 0;
            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            DataSet dsPlan = obj.GetPaymentPlanList();
            if (dsPlan != null && dsPlan.Tables.Count > 0 && dsPlan.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsPlan.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
                    }
                    ddlPlan.Add(new SelectListItem { Text = r["PlanName"].ToString(), Value = r["PK_PLanID"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlPlan = ddlPlan;
            #endregion

            #region ddlType
            List<SelectListItem> ddlType = Common.BindDdlType();
            ViewBag.ddlType = ddlType;
            #endregion ddlType
            return View(model);
        }
        [HttpPost]
        [ActionName("PlotRegistration")]
        [OnAction(ButtonName = "Save")]
        public ActionResult PlotRegistrationbyAdmin(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Hold_From = DateTime.Now;
                if (obj.Amount != null && obj.Amount != "0")
                {
                    obj.Hold_To = obj.Hold_From.AddHours(480);
                }
                else
                {
                    obj.Hold_To = obj.Hold_From.AddHours(48);
                }
                obj.PaymentDate = string.IsNullOrEmpty(obj.PaymentDate) ? null : Common.ConvertToSystemDate(obj.PaymentDate, "dd/MM/yyyy");
                obj.BookingDate = string.IsNullOrEmpty(obj.BookingDate) ? null : Common.ConvertToSystemDate(obj.BookingDate, "dd/MM/yyyy");
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/MM/yyyy");

                string password = Common.GenerateRandom();
                obj.Password = Crypto.Encrypt(password);
                DataSet ds = obj.SavePlotRegistrationByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        try
                        {
                            string body = "Dear " + ds.Tables[0].Rows[0]["Name"].ToString() + ", your plot has been successfully registered as customer in blossom city. your login id is " + ds.Tables[0].Rows[0]["LoginId"].ToString() + " and Password is " + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            BLSMS.SendSMSNew(obj.Mobile, body);
                            Session["Admin_Confirm_Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                            Session["Admin_Confirm_LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Admin_Confirm_Password"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            Session["Admin_Type"] = "Associate";
                            FormName = "ConfirmPlotRegistrationAdmin";
                            Controller = "Plot";
                        }
                        catch (Exception ex)
                        {
                            TempData["PlotRegistration"] = ex.Message;
                            FormName = "PlotRegistration";
                            Controller = "Plot";
                        }
                    }

                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PlotRegistration";
                        Controller = "Plot";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PlotRegistration"] = ex.Message;
                FormName = "PlotRegistration";
                Controller = "Plot";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ConfirmPlotRegistrationAdmin()
        {
            return View();
        }
        public ActionResult CheckUTR(string UtrNumber)
        {
            Plot model = new Plot();
            model.UtrNumber = UtrNumber;
            List<PlotUtr> lstUtr = new List<PlotUtr>();
            DataSet ds = model.CheckUtrNumber();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            PlotUtr obj = new PlotUtr();
                            obj.CustomerID = r["CustomerId"].ToString();
                            obj.CustomerName = r["CustomerName"].ToString();
                            obj.PlotNumber = r["PlotNumber"].ToString();
                            obj.UtrPaidAmount = r["UtrPaidAmount"].ToString();
                            obj.UtrDate = r["UTRDate"].ToString();
                            lstUtr.Add(obj);
                        }
                        model.lstUtr = lstUtr;
                    }
                    model.UtrAmount = ds.Tables[1].Rows[0]["UTR_Amount"].ToString();
                    model.RemainingUtrAmount = ds.Tables[1].Rows[0]["RemainingUtrAmount"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                model.Result = "Invalid";

            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}