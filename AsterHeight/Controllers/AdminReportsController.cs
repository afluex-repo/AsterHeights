using AsterHeight.Filter;
using AsterHeight.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;

namespace AsterHeight.Controllers
{
    public class AdminReportsController : AdminBaseController
    {
        #region Due Installment Report

        public ActionResult DueInstallmentReport(string PK_BookingId)
        {
            Plot model = new Plot();
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
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
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

        public ActionResult FillDueInstsDetails(string BookingNumber, string FromDate, string ToDate, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            Plot model = new Plot();
            List<Plot> lst = new List<Plot>();
            model.SiteID = SiteID == "0" ? null : SiteID;
            model.SectorID = SectorID == "0" ? null : SectorID;
            model.BlockID = BlockID == "0" ? null : BlockID;
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            // model.PlotNumber = PlotNumber;
            DataSet dsblock = model.FillDueInstDetails();
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

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            else
            {
                model.Result = "No record found !";
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
                        ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
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

        #endregion




        public ActionResult SummaryReport(Plot model)
        {

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
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }
        [HttpPost]
        [ActionName("SummaryReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetSummaryRep(Plot model)
        {
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



            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = objmaster.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;


            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock = objmaster.GetBlockList();


            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
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
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetSummaryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerName = r["CustomerInfo"].ToString();
                    obj.AssociateID = r["AssociateInfo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentDate = r["LastPaymentDate"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.PlotAmount = r["NetPlotAmount"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    obj.Amount = r["PlotAmount"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.Discount = r["Discount"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.PlotAmount = double.Parse(ds.Tables[0].Compute("sum(NetPlotAmount)", "").ToString()).ToString("n2");
                ViewBag.PaidAmount = double.Parse(ds.Tables[0].Compute("sum(PaidAmount)", "").ToString()).ToString("n2");
                ViewBag.Balance = double.Parse(ds.Tables[0].Compute("sum(Balance)", "").ToString()).ToString("n2");

            }
            return View(model);
            


        }

        public ActionResult EditPaymentDetail(Plot model)
        {

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
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;

            return View(model);
        }
        [HttpPost]
        [ActionName("EditPaymentDetail")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetPaymentDetail(Plot model)
        {
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



            List<SelectListItem> ddlSector = new List<SelectListItem>();
            DataSet dsSector = objmaster.GetSectorList();
            int sectorcount = 0;

            if (dsSector != null && dsSector.Tables.Count > 0)
            {

                foreach (DataRow r in dsSector.Tables[0].Rows)
                {
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
                    }
                    ddlSector.Add(new SelectListItem { Text = r["SectorName"].ToString(), Value = r["PK_SectorID"].ToString() });
                    sectorcount = 1;
                }
            }

            ViewBag.ddlSector = ddlSector;


            List<SelectListItem> lstBlock = new List<SelectListItem>();

            int blockcount = 0;
            //objmodel.SiteID = ds.Tables[0].Rows[0]["PK_SiteID"].ToString();
            //objmodel.SectorID = ds.Tables[0].Rows[0]["PK_SectorID"].ToString();
            DataSet dsblock = objmaster.GetBlockList();


            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
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
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetPaymentDetailList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerName = r["CustomerInfo"].ToString();
                    obj.AssociateID = r["AssociateInfo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    //obj.PaymentDate = r["PaymentDate"].ToString();// ("dd'/'MM'/'yyyy");
                    if (r["PaymentDate"].ToString() != "")
                    {
                        obj.PaymentDate = Convert.ToDateTime(r["PaymentDate"].ToString()).ToString("dd'/'MM'/'yyyy");
                    }
                    else
                    {
                        obj.PaymentDate = "";
                    }
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.PlotAmount = r["NetPlotAmount"].ToString();
                    obj.Amount = r["PlotAmount"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.Discount = r["Discount"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdatePaymentDetail(Plot model)
        {
            try
            {
                // model.BookingDate = string.IsNullOrEmpty(model.BookingDate) ? null : Common.ConvertToSystemDate(model.BookingDate, "dd/MM/yyyy");
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.UpdateBookingDetail();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Json(new { StatusCode = 200, Message = "Payment Date Update Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { StatusCode = 500, Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString() }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { StatusCode = 500, Message = "Some error occured" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { StatusCode = 500, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        #region CustomerLedgerReport
        public ActionResult CustomerLedgerReport(Plot model)
        {

            //Plot model = new Plot();
            //model.PK_BookingId= Convert.ToInt32(Session["PK_BookingId"].ToString()).ToString();
            //string SessionBookingNumber = Session["BookingNumber"] as string;
            //if (model.BookingNumber == null && SessionBookingNumber != null || SessionBookingNumber == "")
            //{
            //    model.BookingNumber = Session["BookingNumber"].ToString();
            //}
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            //model.PK_BookingId = PK_BookingId;
            DataSet dsBookingDetails = model.GetBookingDetailsList();
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

        public ActionResult Details(string BookingNumber, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {
            Plot model = new Plot();
            try
            {
                List<Plot> lst = new List<Plot>();
                model.SiteID = SiteID == "0" ? null : SiteID;
                model.SectorID = SectorID == "0" ? null : SectorID;
                model.BlockID = BlockID == "0" ? null : BlockID;
                model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
                model.PlotNumber = string.IsNullOrEmpty(PlotNumber) ? null : PlotNumber;
                // model.PlotNumber = PlotNumber;
                DataSet dsblock = model.FillDetails();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = dsblock.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {

                        model.Result = "yes";
                        model.hdBookingNo = Crypto.Encrypt(dsblock.Tables[0].Rows[0]["BookingNo"].ToString());
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

                                lst.Add(obj);
                            }
                            model.lstPlot = lst;
                        }
                    }
                }
                else
                {
                    model.Result = "No record found !";
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
                            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
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
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        public ActionResult LedgerReport(string BookingNumber)
        {
            string FormName = "";
            string Controller = "";
            Plot model = new Plot();
            model.LoginId = Session["LoginId"].ToString();
            model.BookingNumber = string.IsNullOrEmpty(BookingNumber) ? null : BookingNumber;
            List<Plot> lstBlockList = new List<Plot>();

            DataSet dsblock = model.FillDetails();
            if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
            {

                if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    model.Result = "yes";
                    // model.PlotID = dsblock.Tables[0].Rows[0]["PK_PlotID"].ToString();
                    model.PlotAmount = dsblock.Tables[0].Rows[0]["PlotAmount"].ToString();
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
                    Session["BookingNumber"] = model.BookingNumber;

                }
                Session["PK_BookingId"] = model.PK_BookingId;
                FormName = "CustomerLedgerReport";
                Controller = "AdminReports";
            }
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
                    Session["BookingNumber"] = model.BookingNumber;
                    Session["PK_BookingId"] = model.PK_BookingId;
                    lstBlockList.Add(obj);
                    FormName = "CustomerLedgerReport";
                    Controller = "AdminReports";
                }
                model.lstPlot = lstBlockList;

            }

            else
            {
                TempData["Login"] = "Data Not Found";

            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PrintCustomerLedger(string bn)
        {
            Plot model = new Plot();
            try
            {
                List<Plot> lst = new List<Plot>();
                model.BookingNumber = Crypto.Decrypt(bn);
                DataSet dsblock = model.FillDetails();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        model.Result = "yes";
                        ViewBag.CustomerName = dsblock.Tables[0].Rows[0]["CustomerName"].ToString() + " (" + dsblock.Tables[0].Rows[0]["CustomerLoginID"].ToString() + ")";
                        ViewBag.CustomerMobile = dsblock.Tables[0].Rows[0]["Mobile"].ToString();
                        ViewBag.CustomerAddress = dsblock.Tables[0].Rows[0]["Address"].ToString();
                        ViewBag.Pincode = dsblock.Tables[0].Rows[0]["Pincode"].ToString();
                        ViewBag.State = dsblock.Tables[0].Rows[0]["statename"].ToString();
                        ViewBag.City = dsblock.Tables[0].Rows[0]["Districtname"].ToString();
                        ViewBag.SiteName = dsblock.Tables[0].Rows[0]["SiteName"].ToString();
                        ViewBag.SiteAddress = dsblock.Tables[0].Rows[0]["SiteAddress"].ToString();
                        ViewBag.SectorName = dsblock.Tables[0].Rows[0]["SectorName"].ToString();
                        ViewBag.BlockName = dsblock.Tables[0].Rows[0]["BlockName"].ToString();
                        ViewBag.PlotNumber = dsblock.Tables[0].Rows[0]["PlotNumber"].ToString();

                        ViewBag.NetPlotAmount = dsblock.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                        ViewBag.PaidAmount = dsblock.Tables[0].Rows[0]["PaidAmount"].ToString();
                        ViewBag.NetAmtWords = dsblock.Tables[0].Rows[0]["NetAmountInWords"].ToString();
                        ViewBag.PaidAmtWords = dsblock.Tables[0].Rows[0]["PaidAmountInWords"].ToString();

                        ViewBag.AssociateName = dsblock.Tables[0].Rows[0]["AssociateName"].ToString();
                        ViewBag.AssociateLoginID = dsblock.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                        ViewBag.PlotArea = dsblock.Tables[0].Rows[0]["PlotArea"].ToString();
                        ViewBag.PLC = dsblock.Tables[0].Rows[0]["PLCCharge"].ToString();

                    }
                }
                else
                {
                    model.Result = "No record found !";
                }
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

                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
            //return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region PlotAvailability

        public ActionResult PlotAvailability(Master model)
        {
            #region ddlSiteType
            Master objSiteType = new Master();
            int count1 = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    //if (count1 == 0)
                    //{
                    //    ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    //}
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion


            #region ddlSite
            int count2 = 0;
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

            return View();
        }

        public ActionResult GetSiteBySiteType(string SiteTypeID)
        {
            try
            {
                Master model = new Master();
                model.SiteTypeID = SiteTypeID;

                #region GetSite
                List<SelectListItem> ddlSite = new List<SelectListItem>();
                DataSet dsSector = model.GetSiteList();

                if (dsSector != null && dsSector.Tables.Count > 0)
                {
                    foreach (DataRow r in dsSector.Tables[0].Rows)
                    {
                        ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });

                    }
                }
                model.ddlSite = ddlSite;
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Master model = new Master();
                model.SiteID = SiteID;

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
                model.ddlSector = ddlSector;
                #endregion

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


        [HttpPost]
        [ActionName("PlotAvailability")]
        [OnAction(ButtonName = "Search")]

        public ActionResult Details(Master model)
        {
            //Master model = new Master();
            List<Master> lst = new List<Master>();

            //model.SiteID = SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.SiteTypeID = model.SiteTypeID == "0" ? null : model.SiteTypeID;

            DataSet dsblock1 = model.GetDetails();
            if (dsblock1 != null && dsblock1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsblock1.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PlotID = r["PK_PlotID"].ToString();
                    obj.SiteID = r["FK_SiteID"].ToString();
                    obj.SectorID = r["FK_SectorID"].ToString();
                    obj.BlockID = r["FK_BlockID"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    obj.PlotStatus = r["Status"].ToString();
                    obj.ColorCSS = r["ColorCSS"].ToString();
                    obj.PlotAmount = r["PlotAmount"].ToString();
                    obj.PlotArea = r["PlotArea"].ToString();
                    obj.SiteName = r["SiteName"].ToString();
                    obj.BlockName = r["BlockName"].ToString();
                    obj.SectorName = r["SectorName"].ToString();
                    //model.PlotID = dsblock.Tables[0].Rows[0]["PK_PLotID"].ToString();
                    //model.SiteID = dsblock.Tables[0].Rows[0]["FK_SiteID"].ToString();
                    //model.SectorID = dsblock.Tables[0].Rows[0]["FK_SectorID"].ToString();
                    //model.BlockID = dsblock.Tables[0].Rows[0]["FK_BlockID"].ToString();
                    //model.PlotNumber = dsblock.Tables[0].Rows[0]["PlotNumber"].ToString();
                    //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                    lst.Add(obj);
                }

                model.lstPlot = lst;
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
            DataSet dsblock = objmaster.GetBlockList();


            if (dsblock != null && dsblock.Tables.Count > 0 && dsblock.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dsblock.Tables[0].Rows)
                {

                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            #endregion

            #region ddlSiteType
            Master objSiteType = new Master();
            int countType = 0;
            List<SelectListItem> ddlSiteType = new List<SelectListItem>();
            DataSet ds2 = objSiteType.GetSiteTypeList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (countType == 0)
                    {
                        ddlSiteType.Add(new SelectListItem { Text = "Select Site Type", Value = "0" });
                    }
                    ddlSiteType.Add(new SelectListItem { Text = r["SiteTypeName"].ToString(), Value = r["PK_SiteTypeID"].ToString() });
                    countType = countType + 1;
                }
            }

            ViewBag.ddlSiteType = ddlSiteType;

            #endregion

            return View(model);
        }
        #endregion


        #region PlotAllotmentReport

        public ActionResult PlotAllotmentReport(Plot model)
        {

            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.PlotNumber = string.IsNullOrEmpty(model.PlotNumber) ? null : model.PlotNumber;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

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
                    if (sectorcount == 0)
                    {
                        ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
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
        [ActionName("PlotAllotmentReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetAllotRep(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;

            DataSet ds = model.List();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Plot obj = new Plot();
                    obj.PK_BookingDetailsId = r["PK_BookingDetailsId"].ToString();
                    obj.PK_BookingId = r["PK_BookingID"].ToString();
                    obj.CustomerID = r["CustomerLoginID"].ToString();
                    obj.CustomerName = r["CustomerName"].ToString();
                    obj.AssociateID = r["AssociateLoginID"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    //obj.TransactionDate = r["TransactionDate"].ToString();
                    //obj.TransactionNumber = r["TransactionNo"].ToString();
                    obj.PaidAmount = r["PaidAmount"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    obj.PlotNumber = r["PlotInfo"].ToString();
                    obj.BookingNumber = r["BookingNo"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_BookingDetailsId"].ToString());
                    lst.Add(obj);
                }
                model.lstPlot = lst;
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

                    lstBlock.Add(new SelectListItem { Text = dr["BlockName"].ToString(), Value = dr["PK_BlockID"].ToString() });
                    blockcount = 1;
                }

            }


            ViewBag.ddlBlock = lstBlock;
            #endregion
            return View(model);
        }

        public ActionResult PrintAllotment(string id)
        {
            Plot newdata = new Plot();
            newdata.EncryptKey = Crypto.Decrypt(id);
            newdata.PK_BookingDetailsId = Crypto.Decrypt(id);
            ViewBag.Name = Session["Name"].ToString();
            DataSet ds = newdata.List();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    newdata.Result = "yes";
                    ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    ViewBag.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    ViewBag.CustomerFatherName = ds.Tables[0].Rows[0]["FathersName"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                    ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                    ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    ViewBag.Contact = ds.Tables[0].Rows[0]["AssociateMobile"].ToString();
                    ViewBag.CustomerID = ds.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    ViewBag.SectorName = ds.Tables[0].Rows[0]["SectorName"].ToString();
                    ViewBag.BlockName = ds.Tables[0].Rows[0]["BlockName"].ToString();
                    ViewBag.PlotNo = ds.Tables[0].Rows[0]["PlotNumber"].ToString();

                    ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotInfo"].ToString();
                    ViewBag.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                    ViewBag.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    ViewBag.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                    ViewBag.ReasonOfPayment = ds.Tables[0].Rows[0]["ReasonOfPayment"].ToString();
                    ViewBag.PaymentDate = ds.Tables[0].Rows[0]["PaymentDate"].ToString();
                    ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                    ViewBag.CorporateOffice = ds.Tables[0].Rows[0]["CorporateOffice"].ToString();
                    ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();

                    ViewBag.customerMobile = ds.Tables[0].Rows[0]["customerMobile"].ToString();
                    ViewBag.PLC = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PLC"].ToString()) ? "N/A" : ds.Tables[0].Rows[0]["PLC"].ToString();
                    ViewBag.AmountInWords = ds.Tables[0].Rows[0]["PaidAmountInWords"].ToString();
                    ViewBag.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    ViewBag.NetPlotAmountInWords = ds.Tables[0].Rows[0]["NetPlotAmountInWords"].ToString();
                    ViewBag.TotalPaidAmount = ds.Tables[0].Rows[0]["TotalPaid"].ToString();
                    ViewBag.TotalPaidAmountInWords = ds.Tables[0].Rows[0]["TotalPaidInWords"].ToString();

                    ViewBag.TransactionNo = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                    ViewBag.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                    ViewBag.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                    ViewBag.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                    ViewBag.PlotBookingDate = ds.Tables[0].Rows[0]["BookingDate"].ToString();
                    ViewBag.PaymentTillDate = ds.Tables[0].Rows[0]["TotalPaid"].ToString();
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
        public ActionResult PrintAllotmentNew(string id)

        {
            Plot newdata = new Plot();
            newdata.EncryptKey = Crypto.Decrypt(id);
            newdata.PK_BookingDetailsId = Crypto.Decrypt(id);
            ViewBag.Name = Session["Name"].ToString();
            DataSet ds = newdata.ListNew();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                {

                    newdata.Result = "yes";
                    ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                    ViewBag.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    ViewBag.CustomerFatherName = ds.Tables[0].Rows[0]["FathersName"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                    ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                    ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateLoginID"].ToString();
                    ViewBag.Contact = ds.Tables[0].Rows[0]["AssociateMobile"].ToString();
                    ViewBag.CustomerID = ds.Tables[0].Rows[0]["CustomerLoginID"].ToString();
                    ViewBag.SiteName = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    ViewBag.SectorName = ds.Tables[0].Rows[0]["SectorName"].ToString();
                    ViewBag.BlockName = ds.Tables[0].Rows[0]["BlockName"].ToString();
                    ViewBag.PlotNo = ds.Tables[0].Rows[0]["PlotNumber"].ToString();
                    ViewBag.PlotNumber = ds.Tables[0].Rows[0]["PlotInfo"].ToString();
                    ViewBag.PaidAmount = ds.Tables[0].Rows[0]["PaidAmount"].ToString();
                    ViewBag.PlotArea = ds.Tables[0].Rows[0]["PlotArea"].ToString();
                    ViewBag.PaymentMode = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                    ViewBag.ReasonOfPayment = ds.Tables[0].Rows[0]["ReasonOfPayment"].ToString();
                    ViewBag.PaymentDate = ds.Tables[0].Rows[0]["PaymentDate"].ToString();
                    ViewBag.ReceiptNo = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                    ViewBag.CorporateOffice = ds.Tables[0].Rows[0]["CorporateOffice"].ToString();
                    ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                    ViewBag.customerMobile = ds.Tables[0].Rows[0]["customerMobile"].ToString();
                    ViewBag.PLC = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PLC"].ToString()) ? "N/A" : ds.Tables[0].Rows[0]["PLC"].ToString();
                    ViewBag.AmountInWords = ds.Tables[0].Rows[0]["PaidAmountInWords"].ToString();
                    ViewBag.NetPlotAmount = ds.Tables[0].Rows[0]["NetPlotAmount"].ToString();
                    ViewBag.NetPlotAmountInWords = ds.Tables[0].Rows[0]["NetPlotAmountInWords"].ToString();
                    ViewBag.TotalPaidAmount = ds.Tables[0].Rows[0]["TotalPaid"].ToString();
                    ViewBag.TotalPaidAmountInWords = ds.Tables[0].Rows[0]["TotalPaidInWords"].ToString();
                    ViewBag.TransactionNo = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
                    ViewBag.TransactionDate = ds.Tables[0].Rows[0]["TransactionDate"].ToString();
                    ViewBag.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                    ViewBag.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                    ViewBag.PlotBookingDate = ds.Tables[0].Rows[0]["BookingDate"].ToString();
                    ViewBag.PaymentTillDate = ds.Tables[0].Rows[0]["TotalPaid"].ToString();
                    ViewBag.InstallmentNo = ds.Tables[0].Rows[0]["InstallmentNo"].ToString();
                    ViewBag.PLCCharge = ds.Tables[0].Rows[0]["PLCCharge"].ToString();
                    ViewBag.PLCRate = ds.Tables[0].Rows[0]["PlotRate"].ToString();
                    ViewBag.PlotSize = ds.Tables[0].Rows[0]["PlotSize"].ToString();
                    ViewBag.Balance = ds.Tables[0].Rows[0]["BalanceAmt"].ToString();
                    ViewBag.AdjustmentloginId = ds.Tables[0].Rows[0]["AdjustmentloginId"].ToString();
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

        #region WelcomeLetter Associate

        public ActionResult WelcomeLetter()
        {
            return View();
        }
        [HttpPost]
        [ActionName("WelcomeLetter")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult AssociateList(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            DataSet ds = model.GetList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.UserID = r["PK_UserId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.SponsorID = r["SponsorId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    //   obj.LoginID = r["LoginId"].ToString();
                    //  obj.DesignationID = r["FK_DesignationID"].ToString();
                    // obj.FirstName = r["AName"].ToString();
                    obj.isBlocked = r["isBlocked"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    // obj.PanNo = r["PanNumber"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }

        public ActionResult PrintWelcomeLetter(string id)
        {
            TraditionalAssociate obj = new TraditionalAssociate();
            obj.UserID = Crypto.Decrypt(id);

            DataSet ds = obj.GetList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                // obj.Result = "yes";
                //ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                ViewBag.Contact = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["DesignationName"].ToString();

                ViewBag.MemberAccNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                ViewBag.MemberBankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                ViewBag.MemberBranch = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                ViewBag.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                ViewBag.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();


                ViewBag.CompanyName = SoftwareDetails.CompanyName;
            }

            return View(obj);
        }
        public ActionResult PrintIDCard(string id)
        {
            TraditionalAssociate obj = new TraditionalAssociate();
            obj.UserID = Crypto.Decrypt(id);

            DataSet ds = obj.GetList();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                // obj.Result = "yes";
                //ViewBag.PK_BookingId = ds.Tables[0].Rows[0]["PK_BookingId"].ToString();
                ViewBag.AssociateID = ds.Tables[0].Rows[0]["AssociateId"].ToString();
                ViewBag.AssociateName = ds.Tables[0].Rows[0]["AssociateName"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.Pin = ds.Tables[0].Rows[0]["PinCode"].ToString();
                ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                ViewBag.Contact = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["DesignationName"].ToString();
            }

            return View(obj);
        }
        #endregion
        #region Approve KYC
        public ActionResult AssociateListForKYC()
        {
            List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
            ViewBag.ddlKYCStatus = ddlKYCStatus;
            List<Reports> lst = new List<Reports>();

            return View();
        }
        [HttpPost]
        [ActionName("AssociateListForKYC")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult AssociateListForKYC(AssociateBooking model)
        {
            List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
            ViewBag.ddlKYCStatus = ddlKYCStatus;
            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.AssociateListForKYC();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PK_DocumentID = r["PK_UserDocumentID"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.DisplayName = r["FirstName"].ToString();
                    obj.DocumentNumber = r["DocumentNumber"].ToString();
                    obj.DocumentType = r["DocumentType"].ToString();
                    obj.DocumentImage = (r["DocumentImage"].ToString());
                    obj.Status = (r["Status"].ToString());

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        public ActionResult ApproveKYC(string Id, string DocumentType, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                List<SelectListItem> ddlKYCStatus = Common.BindKYCStatus();
                ViewBag.ddlKYCStatus = ddlKYCStatus;
                List<AssociateBooking> lst = new List<AssociateBooking>();

                AssociateBooking model = new AssociateBooking();
                model.LoginId = LoginID;
                model.PK_DocumentID = Id;
                model.DocumentType = DocumentType;
                model.Status = "Approved";
                model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = new DataSet();
                ds = model.ApproveKYC();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["KYCVerification"] = "KYC Approved Successfully..";
                        FormName = "AssociateListForKYC";
                        Controller = "AdminReports";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["KYCVerification"] = "Error : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateListForKYC";
                        Controller = "AdminReports";
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["KYCVerification"] = ex.Message;
                FormName = "AssociateListForKYC";
                Controller = "AdminReports";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region  PayoutDetails
        public ActionResult PayoutDetails(AssociateBooking model)
        {

            //model.FromDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            //model.ToDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.PayoutDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.BinaryIncome = r["BinaryIncome"].ToString();
                    obj.DirectIncome = r["DirectIncome"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.GrossAmount = double.Parse(ds.Tables[0].Compute("sum(GrossAmount)", "").ToString()).ToString("n2");
                ViewBag.BinaryIncome = double.Parse(ds.Tables[0].Compute("sum(BinaryIncome)", "").ToString()).ToString("n2");
                ViewBag.DirectIncome = double.Parse(ds.Tables[0].Compute("sum(DirectIncome)", "").ToString()).ToString("n2");
                ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
                ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
                ViewBag.NetAmount = double.Parse(ds.Tables[0].Compute("sum(NetAmount)", "").ToString()).ToString("n2");

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutDetails")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutDetailsBy(AssociateBooking model)
        {
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayOutNo = r["PayoutNo"].ToString();
                    obj.ClosingDate = r["ClosingDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.TDS = r["TDS"].ToString();
                    obj.BinaryIncome = r["BinaryIncome"].ToString();
                    obj.DirectIncome = r["DirectIncome"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
                ViewBag.GrossAmount = double.Parse(ds.Tables[0].Compute("sum(GrossAmount)", "").ToString()).ToString("n2");
                ViewBag.BinaryIncome = double.Parse(ds.Tables[0].Compute("sum(BinaryIncome)", "").ToString()).ToString("n2");
                ViewBag.DirectIncome = double.Parse(ds.Tables[0].Compute("sum(DirectIncome)", "").ToString()).ToString("n2");
                ViewBag.TDS = double.Parse(ds.Tables[0].Compute("sum(TDS)", "").ToString()).ToString("n2");
                ViewBag.Processing = double.Parse(ds.Tables[0].Compute("sum(Processing)", "").ToString()).ToString("n2");
                ViewBag.NetAmount = double.Parse(ds.Tables[0].Compute("sum(NetAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult PayoutRequestReport(AssociateBooking model)
        {
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutRequestReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutRequestReportBy(AssociateBooking model)
        {
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutRequestReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.RequestID = r["Pk_RequestId"].ToString();
                    obj.ClosingDate = r["RequestedDate"].ToString();
                    obj.AssociateLoginID = r["LoginId"].ToString();
                    obj.FirstName = r["Name"].ToString();
                    obj.GrossAmount = r["AMount"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.DisplayName = r["BackColor"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }

        public ActionResult ApproveRequest(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.RequestID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.ApproveRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Request"] = "Request Approved";
                    }
                    else
                    {
                        TempData["Request"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Request"] = ex.Message;
            }
            return RedirectToAction("PayoutRequestReport");
        }
        public ActionResult DeclineRequest(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.RequestID = id;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.DeclineRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Request"] = "Request Declined";
                    }
                    else
                    {
                        TempData["Request"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Request"] = ex.Message;
            }
            return RedirectToAction("PayoutRequestReport");
        }
        #endregion
        #region UnpaidIncome
        public ActionResult UnpaidIncome(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.JoiningFromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();


                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("UnpaidIncome")]
        [OnAction(ButtonName = "Search")]
        public ActionResult UnpaidIncomeBy(TraditionalAssociate model)
        {
            List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            //model.FromID = string.IsNullOrEmpty(model.FromID) ? null : Common.ConvertToSystemDate(model.FromID, "dd/MM/yyyy");
            //model.ToID = string.IsNullOrEmpty(model.ToID) ? null : Common.ConvertToSystemDate(model.ToID, "dd/MM/yyyy");
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TraditionalAssociate obj = new TraditionalAssociate();
                    obj.JoiningFromDate = r["CurrentDate"].ToString();
                    obj.FromID = r["FromLoginId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToID = r["ToLoginId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.Amount = r["BusinessAmount"].ToString();
                    obj.DifferencePercentage = r["DifferencePerc"].ToString();
                    obj.Income = r["Income"].ToString();


                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }
        #endregion
        #region TransactionLogReport
        public ActionResult TransactionLogReport(AssociateBooking model)
        {

            return View(model);
        }
        [HttpPost]
        [ActionName("TransactionLogReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TransactionLogReportBy(AssociateBooking model)
        {

            List<AssociateBooking> lst = new List<AssociateBooking>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.TransactionLogReportBy();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.DisplayName = r["ActionName"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    obj.FromDate = r["CreatedDate"].ToString();
                    obj.TransactionNumber = r["TransactionBy"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }
        #endregion
        public ActionResult Direct()
        {
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View();
        }
        [HttpPost]
        [ActionName("Direct")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DirectList(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FromActivationDate = string.IsNullOrEmpty(model.FromActivationDate) ? null : Common.ConvertToSystemDate(model.FromActivationDate, "dd/MM/yyyy");
            model.ToActivationDate = string.IsNullOrEmpty(model.ToActivationDate) ? null : Common.ConvertToSystemDate(model.ToActivationDate, "dd/MM/yyyy");


            DataSet ds = model.GetDirectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Leg = r["Leg"].ToString();
                    obj.Name = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;


            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View(model);
        }
        public ActionResult DownLine(Reports model)
        {
            List<Reports> lst = new List<Reports>();
            model.FromDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");
            model.ToDate = Common.ConvertToSystemDate(DateTime.Today.ToShortDateString(), "MM/dd/yyyy");

            DataSet ds = model.GetDownlineList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View();
        }
        [HttpPost]
        [ActionName("DownLine")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult DownLineList(Reports model)
        {

            List<Reports> lst = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FromActivationDate = string.IsNullOrEmpty(model.FromActivationDate) ? null : Common.ConvertToSystemDate(model.FromActivationDate, "dd/MM/yyyy");
            model.ToActivationDate = string.IsNullOrEmpty(model.ToActivationDate) ? null : Common.ConvertToSystemDate(model.ToActivationDate, "dd/MM/yyyy");
            DataSet ds = model.GetDownlineList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());

                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View(model);
        }


        public ActionResult ReturnBenefitReport(Reports model)
        {

            List<SelectListItem> Status = Common.Status();
            ViewBag.Status = Status;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = model.ReturnBenefit();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.AssociateLoginID = r["AssociateLoginID"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.ReturnBenefitStartDate = r["ReturnBenefitStartDate"].ToString();
                    Obj.PlotNumber = r["PlotNumber"].ToString();
                    //Obj.DueDate = r["Installmentdate"].ToString();
                    // Obj.Status = r["IsPaid"].ToString();
                    //Obj.ReceiptNo = r["ReceiptNo"].ToString();
                    //Obj.TransactionNo = r["TransactionNo"].ToString();
                    //Obj.TransactionDate = r["TransactionDate"].ToString();
                    //Obj.BankBranch = r["BankBranch"].ToString();
                    //Obj.BankName = r["BankName"].ToString();
                    //Obj.InstAmt = r["InstAmt"].ToString();
                    lst1.Add(Obj);
                }

                model.lstassociate = lst1;
            }
            return View(model);
        }

        public ActionResult ReturnBenefitByLogin(Reports model)
        {

            //model.LoginId = LoginId;
            List<SelectListItem> Status = Common.Status();

            ViewBag.Status = Status;
            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = model.ReturnBenefitView();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
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
                    Obj.PlotNumber = r["PlotNumber"].ToString();
                    lst1.Add(Obj);
                }

                model.lstassociate = lst1;
            }
            return View(model);
        }
        public ActionResult ApproveAssociateList()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ApproveAssociateList")]
        [OnAction(ButtonName = "GetList")]
        public ActionResult GetAssociateList(Reports model)
        {
            try
            {
                List<Reports> lstassociate = new List<Reports>();
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.GetAssociateList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.Name = r["Name"].ToString();
                        obj.CustomerId = r["RefCustId"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.AssociateID = r["AssociateID"].ToString();
                        obj.PaymentMode = (r["PaymentMode"].ToString());
                        obj.TransactionDate = r["TransactionDate"].ToString();
                        obj.PaymentDate = r["Entrydate"].ToString();
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
                    model.AssociateID = FK_UserID;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.LoginId = Session["LoginId"].ToString();
                    DataSet ds = model.ApprovedAssociate();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Approved"] = "Associate Approved  Successfull";
                            FormName = "ApproveAssociateList";
                            Controller = "AdminReports";
                        }
                        else
                        {
                            TempData["Approved"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "ApproveAssociateList";
                            Controller = "AdminReports";
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
        public ActionResult RejetAssociate(string FK_UserID)
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
                    DataSet ds = model.RejectAssociate();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Approved"] = "Associate Rejected  Successfull";
                            FormName = "ApproveAssociateList";
                            Controller = "AdminReports";
                        }
                        else
                        {
                            TempData["Approved"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "ApproveAssociateList";
                            Controller = "AdminReports";
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
        public ActionResult TopupReport()
        {
            Customer model = new Customer();
            return View(model);
        }
        [HttpPost]
        public ActionResult TopupReport(Customer model)
        {
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Customer> lst = new List<Customer>();
            DataSet ds = model.GetTopupReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer obj = new Customer();
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
            return View(model);
        }
        public ActionResult AllReturnBenefitList(Reports model)
        {
            List<SelectListItem> ddlStatus = Common.Status();
            ViewBag.ddlStatus = ddlStatus;
            model.Status = model.Status == "0" ? null : model.Status;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetAllRentalBenefitList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.PK_BookingDetailsId = r["PK_tblReturnBenefitDetailsId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.InstallmentAmount = r["InstAmt"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.DueDate = r["DueDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.MemberAccNo = r["AccountNumber"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.BankName = r["MemberBankName"].ToString();
                    obj.AssociateLoginID= r["AssociateLoginId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AllReturnBenefitList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AllReturnBenefitListReport(Reports model)
        {
            List<SelectListItem> ddlStatus = Common.Status();
            ViewBag.ddlStatus = ddlStatus;
            model.Status = model.Status == "0" ? null : model.Status;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetAllRentalBenefitList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.PK_BookingDetailsId = r["PK_tblReturnBenefitDetailsId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.InstallmentAmount= r["InstAmt"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.DueDate = r["DueDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.MemberAccNo = r["AccountNumber"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.BankName = r["MemberBankName"].ToString();
                    obj.AssociateLoginID = r["AssociateLoginId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.PlotNumber = r["PlotNumber"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AllReturnBenefitList")]
        [OnAction(ButtonName = "Export")]
        public ActionResult ExportToExcelPayout(Reports model)
        {
            //List<SelectListItem> ddlStatus = Common.Status();
            //ViewBag.ddlStatus = ddlStatus;
            //model.Status = model.Status == "0" ? null : model.Status;
            //model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            //model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.GetAllRentalBenefitList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string filename = "AllReturnBenefitList" + ".xls";
                GridView GridView1 = new GridView();
             // ds.Tables[0].Columns.Remove("PK_tblReturnBenefitDetailsId");
                //ds.Tables[0].Columns.Remove("LoginId");
                //ds.Tables[0].Columns.Remove("InstAmt");
                //ds.Tables[0].Columns.Remove("Name");
                //ds.Tables[0].Columns.Remove("DueDate");
                //ds.Tables[0].Columns.Remove("Status");
                // ds.Tables[0].Columns.Remove("AccountNumber");
                //ds.Tables[0].Columns.Remove("IFSCCode");
                //ds.Tables[0].Columns.Remove("BankBranch");
                //ds.Tables[0].Columns.Remove("MemberBankName");
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                //string style = @" .text { mso-number-format:\@; }  ";
                string style = @"<style> td { mso-number-format:\@; } </style> ";

                Response.Clear();
                // Response.AddHeader("content-disposition", "attachment;filename=MemberDetailsReport.xls");
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                System.IO.StringWriter s_Write = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter h_write = new HtmlTextWriter(s_Write);
                GridView1.ShowHeader = true;
                GridView1.RenderControl(h_write);
                Response.Write(style);
                Response.Write(s_Write.ToString());
                Response.End();

            }

            return null;
        }
        public ActionResult PayPayout(Reports model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentMode();
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
            return View();
        }
        [HttpPost]
        [ActionName("PayPayout")]
        [OnAction(ButtonName = "Search")]
        public ActionResult Payoutlist(Reports model)
        {
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeForBenefitPay();
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
            List<SelectListItem> ddlStatus = Common.Status();
            ViewBag.ddlStatus = ddlStatus;
            model.Status = model.Status == "0" ? null : model.Status;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();
            DataSet ds = model.Getlistpayout();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.PK_BookingDetailsId = r["PK_tblReturnBenefitDetailsId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.InstallmentAmount = r["InstAmt"].ToString();
                    obj.InstallmentNo = r["InstallmentNo"].ToString();
                    obj.Fk_UserId = r["Fk_UserId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.DueDate = r["DueDate"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("PayPayout")]
        [OnAction(ButtonName = "Save")]
        public ActionResult PayPayoutAction(Reports model)
        {
            string hdrows2 = Request["hdRows2"].ToString();
            string transactiono = "";
            string transactiondate = "";
            string bankname = "";
            string branchname = "";
            string Pk_PaidBoosterId_ = "";
            string PaymentMode = "";
            string InstallmentNo = "";
            string LoginId = "";
            for (int i = 1; i < int.Parse(hdrows2); i++)
            {
                Pk_PaidBoosterId_ = Request["Fk_UserId_" + i].ToString();
                transactiono = Request["TransactionNo_" + i].ToString();
                transactiondate = Request["TransactionDate_" + i].ToString();
                bankname = Request["BankName_" + i].ToString();
                branchname = Request["BankBranch_" + i].ToString();
                model.Amount = Request["Amount_" + i].ToString();
                PaymentMode = Request["paymentmode_" + i].ToString();
                InstallmentNo= Request["InstallmentNo_" + i].ToString();
                LoginId= Request["LoginId_" + i].ToString();
                model.Fk_UserId = Pk_PaidBoosterId_;
                model.TransactionNo = transactiono;
                model.BankName = bankname;
                model.BankBranch = branchname;
                model.InstallmentNo = InstallmentNo;
                model.LoginId = LoginId;
                DataSet ds = null;
                if (!string.IsNullOrEmpty(PaymentMode))
                {
                    model.PaymentMode = PaymentMode;
                    model.TransactionNo = transactiono;
                    model.BankName = bankname;
                    model.BankBranch = branchname;
                    model.TransactionDate = string.IsNullOrEmpty(transactiondate) ? null : Common.ConvertToSystemDate(transactiondate, "dd/MM/yyyy");
                    model.InstallmentNo = InstallmentNo;
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.LoginId = LoginId;
                    ds = model.SaveBenefitNew();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["PayPayout"] = "Paymnent Done";
                    }
                    else
                    {
                        TempData["PayPayout"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }

            return RedirectToAction("PayPayout");
        }
        #region RewardAchiever

        public ActionResult RewardAchiever(Reports model)
        {

            #region ddlReward
            int count1 = 0;
            List<SelectListItem> ddlReward = new List<SelectListItem>();
            Reports model1 = new Reports();
            DataSet ds1P = model1.ProductRewardList();
            if (ds1P != null && ds1P.Tables.Count > 0 && ds1P.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1P.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlReward.Add(new SelectListItem { Text = "Select Reward", Value = "0" });
                    }
                    ddlReward.Add(new SelectListItem { Text = r["RewardName"].ToString(), Value = r["PK_RewardItemId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlReward = ddlReward;
            #endregion



            return View(model);
        }


        public ActionResult Reward(Reports model)
        {
            return View(model);
        }


        [HttpPost]
        [ActionName("Reward")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetRewardsList(Reports model)
        {
            List<Reports> lst1 = new List<Reports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.Reward();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();

                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.Status = r["Status"].ToString();
                    lst1.Add(obj);
                }
                model.lstassociate = lst1;


            }
            return View(model);
        }

        [HttpPost]
        [ActionName("RewardAchiever")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetRewards(Reports model)
        {
            List<Reports> lst1 = new List<Reports>();
            model.RewardID = model.RewardID == "0" ? null : model.RewardID;
            DataSet ds = model.RewardListForAchiever();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();

                    obj.PK_RewardItemId = r["PK_UserRewardID"].ToString();
                    obj.RewardID = r["FK_RewardId"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Name = r["FirstName"].ToString();

                    lst1.Add(obj);
                }
                model.lstassociate = lst1;


            }

            #region ddlReward
            int count1 = 0;
            List<SelectListItem> ddlReward = new List<SelectListItem>();
            Reports model1 = new Reports();
            DataSet ds1P = model1.ProductRewardList();
            if (ds1P != null && ds1P.Tables.Count > 0 && ds1P.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1P.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlReward.Add(new SelectListItem { Text = "Select Reward", Value = "0" });
                    }
                    ddlReward.Add(new SelectListItem { Text = r["RewardName"].ToString(), Value = r["PK_RewardItemId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlReward = ddlReward;
            #endregion


            return View(model);
        }

        public ActionResult ApprovePayment(string PK_RewardItemId, string Description, string ApprovedDate, string PaidDate)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Reports model = new Reports();
                model.PK_RewardItemId = PK_RewardItemId;
                model.TransactionNo = Description;
                model.TransactionDate = ApprovedDate;
                model.PaymentDate = PaidDate;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Result = "yes";
                DataSet ds = model.ApprovePayment();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ApprovePayment"] = "Paid successfully !";
                    }
                    else
                    {
                        TempData["ApprovePayment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["ApprovePayment"] = ex.Message;
            }
            FormName = "RewardAchiever";
            Controller = "AdminReports";

            return RedirectToAction(FormName, Controller);

        }
        public ActionResult RoyalityClub(Reports model)
        {
            List<Reports> lst = new List<Reports>();

            DataSet ds = model.RoyalityClubList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.QualifyDate = r["CalculationDate"].ToString();
                    obj.LoginId= r["LoginId"].ToString();
                    obj.MatchingTarget = r["Business"].ToString();
                    obj.PK_RewardItemId = r["PK_RoyalityClubId"].ToString();
                    lst.Add(obj);
                }
                model.lstReward = lst;
            }

            return View(model);
        }
        public ActionResult SalaryDetails()
        {
            return View();
        }
        public ActionResult SalaryList(Reports model)
        {
            List<Reports> lst1 = new List<Reports>();
            DataSet ds = model.SalaryList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();

                    obj.PK_SallaryDetailsId = r["PK_SalaryDetailsId"].ToString();
                    obj.InstallmentDate = r["InstallmentDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.InstallmentNo = r["InstallmentNo"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.InstallmentAmount = r["InstallmentNo"].ToString();
                    obj.PaymentDate = r["PaymentDate"].ToString();
                    lst1.Add(obj);
                }
                model.lstassociate = lst1;
            }
            return View(model);
        }
        public ActionResult Pay(string SallaryDetailsId)
        {
            Reports model = new Reports();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeForBenefitPay();
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
            if (SallaryDetailsId !="")
            {
                model.PK_SallaryDetailsId = SallaryDetailsId;
                DataSet dsblock = model.fillDetails();
                if (dsblock != null && dsblock.Tables[0].Rows.Count > 0)
                {

                    if (dsblock.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {

                        model.Result = "yes";
                       model.PK_SallaryDetailsId = dsblock.Tables[0].Rows[0]["PK_SalaryDetailsId"].ToString();
                        model.InstallmentDate = dsblock.Tables[0].Rows[0]["InstallmentDate"].ToString();
                        
                        model.InstallmentNo = dsblock.Tables[0].Rows[0]["InstallmentNo"].ToString();
                        model.LoginId = dsblock.Tables[0].Rows[0]["LoginId"].ToString();
                        model.InstallmentAmount = dsblock.Tables[0].Rows[0]["Salary"].ToString();
                        model.PaymentDate = dsblock.Tables[0].Rows[0]["PaymentDate"].ToString();

                    }

                }
            }
            return View();
        }
        [HttpPost]
        [ActionName("Pay")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveSalary()
        {
            string FormName = "";
            string Controller = "";
            Reports model = new Reports();
            #region ddlPaymentMode
            int count3 = 0;
            List<SelectListItem> ddlPaymentMode = new List<SelectListItem>();
            DataSet dsPayMode = model.GetPaymentModeForBenefitPay();
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
            model.UpdatedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.SaveSalary();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Payment"] = "Salary Saved Successfully";
                    FormName = "SalaryDetails";
                    Controller = "AdminReports";
                }
                else
                {
                    TempData["Payment"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    FormName = "SalaryDetails";
                    Controller = "AdminReports";
                }
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
    }
}