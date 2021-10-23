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
    public class AssociateDashboardController : BaseController
    {
        // GET: AssociateDashboard
        public ActionResult AssociateDashBoard()
        {
            ViewBag.Name = Session["FullName"].ToString();
            AssociateBooking newdata = new AssociateBooking();
            try
            {
                List<AssociateBooking> lstInst = new List<AssociateBooking>();
                newdata.AssociateID = Session["Pk_userId"].ToString();
                DataSet dsInst = newdata.GetDueInstallmentList();
                if (dsInst != null && dsInst.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsInst.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();

                        obj.CustomerID = r["PK_UserId"].ToString();
                        obj.CustomerLoginID = r["LoginId"].ToString();
                        obj.CustomerName = r["FirstName"].ToString();
                        obj.PlotNumber = r["PlotInfo"].ToString();
                        obj.InstallmentNo = r["InstallmentNo"].ToString();
                        obj.InstallmentAmount = r["InstAmt"].ToString();


                        lstInst.Add(obj);
                    }

                    newdata.ListInstallment = lstInst;
                }
                DashBoard obj1 = new DashBoard();
                List<DashBoard> lstinvestment = new List<DashBoard>();
                obj1.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj1.GetAssociateDashboard();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString();
                    ViewBag.TotalDirects = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                    //ViewBag.ProductWalletBalance = ds.Tables[0].Rows[0]["ProductWalletBalance"].ToString();
                    ViewBag.PayoutWalletBalance = ds.Tables[0].Rows[0]["PayoutWalletBalance"].ToString();
                    ViewBag.TotalPayout = ds.Tables[0].Rows[0]["TotalPayout"].ToString();
                    ViewBag.TotalDeduction = ds.Tables[0].Rows[0]["TotalDeduction"].ToString();
                    ViewBag.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
                    ViewBag.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();
                    ViewBag.TotalPaidPayout = ds.Tables[0].Rows[0]["TotalPaidPayout"].ToString();
                    ViewBag.TotalLuckyDrawRequest = ds.Tables[0].Rows[0]["TotalLuckyDrawRequest"].ToString();
                    ViewBag.TotalLuckyDraw = ds.Tables[0].Rows[0]["TotalLuckyDraw"].ToString();
                    ViewBag.LeftApprovedBusiness = ds.Tables[4].Rows[0]["LeftApprovedBusiness"].ToString();
                    ViewBag.RightApprovedBusiness = ds.Tables[5].Rows[0]["RightApprovedBusiness"].ToString();
                    //if (ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["PlotNumber"].ToString() != "")
                    //{

                    //    ViewBag.PlotNumber = ds.Tables[1].Rows[0]["PlotNumber"].ToString();
                    //}
                    //else
                    //{
                    //    ViewBag.PlotNumber = "N/A";
                    //}

                    ViewBag.PaidBusinessLeft = ds.Tables[2].Rows[0]["PaidBusinessLeft"].ToString();
                    ViewBag.PaidBusinessRight = ds.Tables[2].Rows[0]["PaidBusinessRight"].ToString();
                    ViewBag.TotalBusinessLeft = ds.Tables[2].Rows[0]["TotalBusinessLeft"].ToString();
                    ViewBag.TotalBusinessRight = ds.Tables[2].Rows[0]["TotalBusinessRight"].ToString();
                    ViewBag.CarryLeft = ds.Tables[2].Rows[0]["CarryLeft"].ToString();
                    ViewBag.CarryRight = ds.Tables[2].Rows[0]["CarryRight"].ToString();
                    //ViewBag.TopupDays = ds.Tables[4].Rows[0]["TopupDays"].ToString();
                    //ViewBag.ProductPaidBusinessLeft = ds.Tables[3].Rows[0]["PaidBusinessLeft"].ToString();
                    //ViewBag.ProductPaidBusinessRight = ds.Tables[3].Rows[0]["PaidBusinessRight"].ToString();

                    //ViewBag.ProductCarryLeft = ds.Tables[3].Rows[0]["CarryLeft"].ToString();
                    //ViewBag.ProductCarryRight = ds.Tables[3].Rows[0]["CarryRight"].ToString();
                }
                DashBoard model = new DashBoard();
                List<AssociateBooking> lstNews = new List<AssociateBooking>();
                newdata.NewsFor = "Associate";
                DataSet dsAssociate = newdata.GetNews();
                if (dsAssociate != null && dsAssociate.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsAssociate.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();
                        //   obj.PK_BookingId = r["PK_UserId"].ToString();

                        obj.PK_NewsID = r["PK_NewsID"].ToString();
                        obj.NewsHeading = r["NewsHeading"].ToString();
                        obj.NewsBody = r["NewsBody"].ToString();


                        //model.PlotStatus = dsblock.Tables[0].Rows[0]["Status"].ToString();

                        lstNews.Add(obj);
                    }

                    newdata.ListNEWS = lstNews;
                }
                DataSet dsStatus = newdata.GetKYCApprovalStatus();
                if (dsStatus.Tables[0].Rows.Count > 0)
                {
                    if (dsStatus.Tables[0].Rows[0]["msg"].ToString() == "1")
                    {
                        newdata.KYCApprovalStatus = "Approved";
                    }
                    else
                    {
                        newdata.KYCApprovalStatus = "Pending";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Dashboard"] = ex.Message;
            }
            return View(newdata);
        }

        #region ChangePasswordAssociate
        public ActionResult ChangePasswordAssociate()
        {

            return View();
        }
        [HttpPost]
        [ActionName("ChangePasswordAssociate")]
        [OnAction(ButtonName = "Change")]
        public ActionResult UpdatePassword(AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.Password = Crypto.Encrypt(obj.Password);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                obj.UpdatedBy = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Login"] = "Password updated successfully..";
                        FormName = "Login";
                        Controller = "Home";
                    }
                    else
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ChangePasswordAssociate";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "ChangePasswordAssociate";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region Tree
        public ActionResult Tree()
        {
            return View();
        }
        #endregion
        #region PayoutRequest
        public ActionResult PayoutRequest(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            DataSet Ds = model.GetPayoutBalance();
            ViewBag.Balance = Ds.Tables[0].Rows[0]["Balance"].ToString();

            return View(model);
        }

        [HttpPost]
        [ActionName("PayoutRequest")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SavePayoutRequest(AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {

                obj.AddedBy = Session["Pk_userId"].ToString();
                obj.UserID = Session["LoginId"].ToString();
                DataSet ds = obj.SavePayoutRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        TempData["PayoutReq"] = " Payout requested successfully !";

                    }
                    else
                    {
                        TempData["PayoutReq"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["PayoutReq"] = ex.Message;
            }
            FormName = "PayoutRequest";
            Controller = "AssociateDashboard";

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PayoutDetails(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
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
                    obj.TDS = r["TDS"].ToString();
                    obj.BinaryIncome = r["BinaryIncome"].ToString();
                    obj.DirectIncome = r["DirectIncome"].ToString();
                    obj.Processing = r["Processing"].ToString();
                    obj.NetAmount = r["NetAmount"].ToString();

                    lst.Add(obj);
                }
                model.lstPlot = lst;
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
            model.UserID = Session["Pk_userId"].ToString();
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
            }

            return View(model);
        }
        public ActionResult PayoutLedger(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.PayoutLedger();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.PayoutWalletID = r["Pk_PayoutId"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.Credit = r["CRAmount"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    //obj.TType = r["TransactionType"].ToString();
                    obj.Debit = r["DrAmount"].ToString();


                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }
            return View(model);
        }
        #endregion
        #region KYC

        public ActionResult KYCDocuments()
        {
            AssociateBooking model = new AssociateBooking();

            model.UserID = Session["Pk_userId"].ToString();
            AssociateBooking obj = new AssociateBooking();
            DataSet ds = model.GetKYCDocuments();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
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
            return View(obj);
        }

        [HttpPost]
        [ActionName("KYCDocuments")]
        [OnAction(ButtonName = "btnUpdateAdhar")]
        public ActionResult KYCDocuments(IEnumerable<HttpPostedFileBase> postedFile, AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";

            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        //E:\BitBucket\TejInfraZone\TejInfra\files\assets\images\

                        obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));

                    }

                }

                obj.UserID = Session["Pk_userId"].ToString();
                obj.ActionStatus = "Adhar";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DocumentUpload"] = "Document uploaded successfully..";
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                    else
                    {
                        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DocumentUpload"] = ex.Message;
                FormName = "KYCDocuments";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("KYCDocuments")]
        [OnAction(ButtonName = "btnUpdatePan")]
        public ActionResult KYCDocuments2(IEnumerable<HttpPostedFileBase> postedFile, AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";

            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        //E:\BitBucket\TejInfraZone\TejInfra\files\assets\images\

                        obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));


                    }

                }

                obj.UserID = Session["Pk_userId"].ToString();
                obj.ActionStatus = "Pan";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DocumentUpload"] = "Document uploaded successfully..";
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                    else
                    {
                        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DocumentUpload"] = ex.Message;
                FormName = "KYCDocuments";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("KYCDocuments")]
        [OnAction(ButtonName = "btnUpdateDoc")]
        public ActionResult KYCDocuments3(IEnumerable<HttpPostedFileBase> postedFile, AssociateBooking obj)
        {
            string FormName = "";
            string Controller = "";

            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        //E:\BitBucket\TejInfraZone\TejInfra\files\assets\images\

                        obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));


                    }

                }

                obj.UserID = Session["Pk_userId"].ToString();
                obj.ActionStatus = "Doc";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["DocumentUpload"] = "Document uploaded successfully..";
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                    else
                    {
                        TempData["DocumentUpload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "KYCDocuments";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["DocumentUpload"] = ex.Message;
                FormName = "KYCDocuments";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region Enquiry
        public ActionResult EnquiryList(Master model)
        {

            List<Master> lst = new List<Master>();

            DataSet ds = model.EnquiryList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.EnquiryID = r["PK_EnquiryMasterId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Details = r["Details"].ToString();

                    lst.Add(obj);
                }

                model.lstBlock1 = lst;
            }
            return View(model);

        }
        [HttpPost]
        [ActionName("EnquiryList")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveEnquiry(Master obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_userId"].ToString();
                DataSet ds = new DataSet();
                ds = obj.SaveEnquiry();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Enquiry"] = "Enquiry saved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Enquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Enquiry"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Enquiry"] = ex.Message;
            }
            return RedirectToAction("EnquiryList", "AssociateDashboard");
        }

        #endregion
        #region UserReward
        public ActionResult UserReward(AssociateBooking model)
        {

            model.UserID = Session["Pk_userId"].ToString();
            model.RewardID = "1";

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.RewardList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.Status = r["Status"].ToString();
                    obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Contact = r["BackColor"].ToString();
                    obj.PK_RewardItemId = r["PK_RewardItemId"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }

        public ActionResult ClaimReward(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.PK_RewardItemId = id;
                obj.Status = "Claim";
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Rewardmsg"] = "Reward Claimed";
                    }
                    else
                    {
                        TempData["Rewardmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Rewardmsg"] = ex.Message;
            }
            return RedirectToAction("UserReward");
        }
        public ActionResult SkipReward(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.PK_RewardItemId = id;
                obj.Status = "Skip";
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Rewardmsg"] = "Reward Skipped";
                    }
                    else
                    {
                        TempData["Rewardmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Rewardmsg"] = ex.Message;
            }
            return RedirectToAction("UserReward");
        }
        #endregion
        #region AssociateBookings
        public ActionResult AssociateBookingList(AssociateBooking model)
        {
            Plot objmaster = new Plot();
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
        [ActionName("AssociateBookingList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult BookingList(AssociateBooking model)
        {
            Plot objmaster = new Plot();
            model.LoginId = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
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
                    AssociateBooking obj = new AssociateBooking();
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
            return View(model);
        }
        #endregion
        #region Customer Ledger Report
        public ActionResult CustomerLedgerReport(AssociateBooking model)
        {

            Plot objmaster = new Plot();
            //AssociateBooking model = new AssociateBooking();
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
            DataSet dsSector = objmaster.GetSectorList();
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
            return View(model);
        }

        public ActionResult Details(string BookingNumber, string LoginId, string SiteID, string SectorID, string BlockID, string PlotNumber)
        {

            AssociateBooking model = new AssociateBooking();
            model.LoginId = Session["LoginId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();

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
        #endregion
        #region AssociateUpline
        public ActionResult AssociateUplineDetail(AssociateBooking model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }
        #endregion
        #region AssociateDownline
        public ActionResult AssociateDownlineDetail(AssociateBooking model)
        {

            model.LoginId = Session["LoginId"].ToString();

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.DesignationName = r["DesignationName"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }
        #endregion
        #region UnpaidIncomeReport
        public ActionResult UnpaidIncome(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
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
                model.lstPlot = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("UnpaidIncome")]
        [OnAction(ButtonName = "Search")]
        public ActionResult UnpaidIncomeBy(AssociateBooking model)
        {
            model.UserID = Session["Pk_userId"].ToString();
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.UnpaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
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
                model.lstPlot = lst;
            }
            return View(model);
        }
        #endregion
        #region PayoutRequestReport
        public ActionResult PayoutRequestReport(AssociateBooking model)
        {
            model.Status = model.Status == "0" ? null : model.Status;
            model.UserID = Session["LoginId"].ToString();
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

            #region ddl 
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion  

            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutRequestReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult PayoutRequestReportBy(AssociateBooking model)
        {
            model.UserID = Session["LoginId"].ToString();
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
            #region ddl 
            List<SelectListItem> RequestStatus = Common.RequestStatus();
            ViewBag.RequestStatus = RequestStatus;
            #endregion  
            return View(model);
        }
        #endregion
        public ActionResult GetSiteDetails(string SiteID)
        {
            try
            {
                Master model = new Master();
                model.SiteID = SiteID;

                #region GetSiteRate
                DataSet dsSiteRate = model.GetSiteList();
                if (dsSiteRate != null)
                {
                    model.Rate = dsSiteRate.Tables[0].Rows[0]["Rate"].ToString();
                    model.Result = "yes";
                }
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
                model.ddlSector = ddlSector;
                #endregion
                #region SitePLCCharge
                List<Master> lstPlcCharge = new List<Master>();
                DataSet dsPlcCharge = model.GetPLCChargeList();

                if (dsPlcCharge != null && dsPlcCharge.Tables.Count > 0)
                {
                    foreach (DataRow r in dsPlcCharge.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.SiteName = r["SiteName"].ToString();
                        obj.PLCName = r["PLCName"].ToString();
                        obj.PLCCharge = r["PLCCharge"].ToString();
                        obj.PLCID = r["PK_PLCID"].ToString();

                        lstPlcCharge.Add(obj);
                    }
                    model.lstPLC = lstPlcCharge;
                }
                #endregion

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public ActionResult GetPlotBookingOnGraph()
        {

            List<AssociateBooking> dataList3 = new List<AssociateBooking>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            AssociateBooking newdata = new AssociateBooking();
            newdata.AssociateID = Session["Pk_userId"].ToString();

            Ds = newdata.GetDetailsForBarGraph();
            if (Ds.Tables.Count > 0)
            {
                ViewBag.TotalUsers = Ds.Tables[0].Rows.Count;
                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    AssociateBooking details = new AssociateBooking();


                    details.TotalBooking = (dr["TotalBooking"].ToString());
                    details.Month = (dr["Month"].ToString());


                    dataList3.Add(details);

                    count++;
                }
            }
            return Json(dataList3, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetGraphDetailsOfPlot()
        {
            List<AssociateBooking> dataList = new List<AssociateBooking>();
            DataSet Ds = new DataSet();
            DataTable dt = new DataTable();
            AssociateBooking newdata = new AssociateBooking();
            newdata.LoginId = Session["LoginId"].ToString();
            Ds = newdata.BindGraphDetails();
            if (Ds.Tables.Count > 0)
            {

                int count = 0;
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    AssociateBooking details = new AssociateBooking();


                    details.Total = (dr["Total"].ToString());
                    details.Status = (dr["Status"].ToString());


                    dataList.Add(details);

                    count++;
                }
            }
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AssociateTree(AssociateBooking model, string AssociateID)
        {
            if (AssociateID != null)
            {
                model.Fk_UserId = AssociateID;
            }
            else
            {
                model.Fk_UserId = Session["Pk_UserId"].ToString();
            }
            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.GetDownlineTree();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Fk_SponsorId = ds.Tables[0].Rows[0]["Fk_SponsorId"].ToString();
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();

                    obj.Status = r["Status"].ToString();
                    obj.ActiveStatus = r["ActiveStatus"].ToString();
                    lst.Add(obj);

                }
                model.lstPlot = lst;
            }


            return View(model);
        }
        public ActionResult ViewProfile()
        {
            Profile objprofile = new Profile();

            List<Profile> lstprofile = new List<Profile>();
            objprofile.LoginId = Session["LoginId"].ToString();
            Profile obj = new Profile();
            DataSet ds = objprofile.GetUserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
                obj.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                obj.ProfilePicture = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.AccountHolderName = ds.Tables[0].Rows[0]["BankHolderName"].ToString();
            }
            return View(obj);
        }
        [HttpPost]
        [ActionName("ViewProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateProfile(HttpPostedFileBase fileProfilePicture, Profile obj)
        {
            string FormName = "";
            string Controller = "";
            obj.DOB = string.IsNullOrEmpty(obj.DOB) ? null : Common.ConvertToSystemDate(obj.DOB, "dd/MM/yyyy");

            try
            {
                if (fileProfilePicture != null)
                {
                    obj.ProfilePicture = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
                    fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePicture)));
                }

                //Profile objProfile = new Profile();
                obj.PK_UserID = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdateProfile();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Profile updated successfully..";
                        FormName = "ViewProfile";
                        Controller = "AssociateDashboard";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ViewProfile";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "ViewProfile";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult Registration(string UserID)
        {
            Home obj = new Home();

            if (UserID != null)
            {

                obj.UserID = Crypto.Decrypt(UserID);
                //model.UserID = UserID;
                DataSet dsPlotDetails = obj.GetList();
                if (dsPlotDetails != null && dsPlotDetails.Tables.Count > 0)
                {
                    obj.UserID = UserID;
                    obj.SponsorId = dsPlotDetails.Tables[0].Rows[0]["SponsorId"].ToString();
                    obj.SponsorName = dsPlotDetails.Tables[0].Rows[0]["SponsorName"].ToString();
                    obj.LoginId = dsPlotDetails.Tables[0].Rows[0]["AssociateID"].ToString();
                    obj.AssociateName = dsPlotDetails.Tables[0].Rows[0]["AssociateName"].ToString();
                    obj.FirstName = dsPlotDetails.Tables[0].Rows[0]["FirstName"].ToString();
                    obj.LastName = dsPlotDetails.Tables[0].Rows[0]["LastName"].ToString();
                    obj.DesignationID = dsPlotDetails.Tables[0].Rows[0]["FK_DesignationID"].ToString();
                    obj.DesignationName = dsPlotDetails.Tables[0].Rows[0]["DesignationName"].ToString();
                    obj.BranchID = dsPlotDetails.Tables[0].Rows[0]["Fk_BranchId"].ToString();
                    obj.MobileNo = dsPlotDetails.Tables[0].Rows[0]["Mobile"].ToString();
                    obj.Email = dsPlotDetails.Tables[0].Rows[0]["Email"].ToString();
                    obj.State = dsPlotDetails.Tables[0].Rows[0]["State"].ToString();
                    obj.City = dsPlotDetails.Tables[0].Rows[0]["City"].ToString();
                    obj.Address = dsPlotDetails.Tables[0].Rows[0]["Address"].ToString();
                    obj.Pincode = dsPlotDetails.Tables[0].Rows[0]["PinCode"].ToString();
                    obj.PanCard = dsPlotDetails.Tables[0].Rows[0]["PanNumber"].ToString();
                    obj.BranchName = dsPlotDetails.Tables[0].Rows[0]["BranchName"].ToString();
                    // objViewBag.Disabled = "disabled";
                    obj.AdharNumber = dsPlotDetails.Tables[0].Rows[0]["AdharNumber"].ToString();
                    obj.BankAccountNo = dsPlotDetails.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    obj.BankName = dsPlotDetails.Tables[0].Rows[0]["MemberBankName"].ToString();
                    obj.BankBranch = dsPlotDetails.Tables[0].Rows[0]["MemberBranch"].ToString();
                    obj.IFSCCode = dsPlotDetails.Tables[0].Rows[0]["IFSCCode"].ToString();
                    obj.ProfilePic = dsPlotDetails.Tables[0].Rows[0]["ProfilePic"].ToString();
                    obj.Signature = dsPlotDetails.Tables[0].Rows[0]["Signature"].ToString();
                }
            }

            else
            {
                // ViewBag.Disabled = "";

            }

            #region ForQueryString
            if (Request.QueryString["Pid"] != null)
            {
                obj.SponsorId = Request.QueryString["Pid"].ToString();
            }
            if (Request.QueryString["lg"] != null)
            {
                obj.Leg = Request.QueryString["lg"].ToString();
                if (obj.Leg == "Right")
                {
                    ViewBag.RightChecked = "checked";
                    ViewBag.LeftChecked = "";
                    ViewBag.Disabled = "Disabled";
                }
                else
                {
                    ViewBag.RightChecked = "";
                    ViewBag.LeftChecked = "checked";
                    ViewBag.Disabled = "Disabled";
                }
            }
            if (Request.QueryString["Pid"] != null)
            {
                Common objcomm = new Common();
                objcomm.ReferBy = obj.SponsorId;
                DataSet ds = objcomm.GetMemberDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    obj.SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();



                }
            }
            else
            {
                ViewBag.RightChecked = "";
                ViewBag.LeftChecked = "checked";
            }
            #endregion ForQueryString

            //obj.SponsorId = Session["LoginId"].ToString();
            //   obj.SponsorName = Session["FullName"].ToString();
            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender
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
            return View(obj);
        }
        public ActionResult RegistrationAction(string CustomerID, string SponsorId, string FirstName, string LastName, string Email, string MobileNo, string PanCard, string Gender, string OTP, string Leg, string Amount, string PaymentMode, string TransactionNo, string BankName, string BankBranch, string TransactionDate, string AdjustmentId, string PaymentDate, string Address)
        {
            Home obj = new Home();

            try
            {
                obj.CustomerID = CustomerID;
                obj.SponsorId = SponsorId;
                obj.FirstName = FirstName;
                obj.LastName = LastName;
                obj.Email = Email;
                obj.MobileNo = MobileNo;
                obj.PanCard = PanCard;
                obj.RegistrationBy = "Web";
                obj.Gender = Gender;
                obj.Leg = Leg;
                obj.PaymentMode = PaymentMode;
                obj.Amount = Amount == "" ? "0" : Amount;
                obj.TransactionNo = TransactionNo;
                obj.BankName = BankName;
                obj.BankBranch = BankBranch;
                obj.AdjustmentId = AdjustmentId;
                obj.Address = Address;
                obj.PaymentDate = string.IsNullOrEmpty(PaymentDate) ? null : Common.ConvertToSystemDate(PaymentDate, "dd/MM/yyyy");
                obj.TransactionDate = string.IsNullOrEmpty(TransactionDate) ? null : Common.ConvertToSystemDate(TransactionDate, "dd/MM/yyyy");
                string password = Common.GenerateRandom();
                obj.Password = Crypto.Encrypt(password);
                string FormName = "";
                string Controller = "";
                DataSet ds = obj.AssociateRegistration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["Confirm_LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Confirm_Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["Confirm_PassWord"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["Confirm_MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        Session["Type"] = "Associate";
                        try
                        {
                            string str2 = "Dear " + ds.Tables[0].Rows[0]["Name"].ToString() + ", Your Login Id is " + ds.Tables[0].Rows[0]["LoginId"].ToString() + " and password is " + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            BLSMS.SendSMSNew(MobileNo, str2);
                        }
                        catch { }
                        obj.Response = "1";

                    }
                    else
                    {
                        obj.Response = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Response = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ActionName("Registration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateAssociateProfile(Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateAssociateProfileByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfilebyadmin"] = "Profile updated successfully..";
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfilebyadmin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "TraditionalAssociate";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfilebyadmin"] = ex.Message;
                FormName = "AssociateList";
                Controller = "TraditionalAssociate";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult GetSponserDetails(string ReferBy)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();

                obj.Result = "Yes";

            }
            else { obj.Result = "Invalid SponsorId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        #region RegistrationDetails
        public ActionResult RegistrationDetails(TraditionalAssociate model)
        {
            //= new TraditionalAssociate();
            //List<TraditionalAssociate> lst = new List<TraditionalAssociate>();
            //DataSet ds = model.GetList();

            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        TraditionalAssociate obj = new TraditionalAssociate();

            //        obj.UserID = r["PK_UserId"].ToString();
            //        obj.AssociateID = r["AssociateId"].ToString();
            //        obj.AssociateName = r["AssociateName"].ToString();
            //        obj.SponsorID = r["SponsorId"].ToString();
            //        obj.SponsorName = r["SponsorName"].ToString();
            //        //   obj.LoginID = r["LoginId"].ToString();
            //        //  obj.DesignationID = r["FK_DesignationID"].ToString();
            //        // obj.FirstName = r["AName"].ToString();
            //        obj.isBlocked = r["isBlocked"].ToString();
            //        obj.Contact = r["Mobile"].ToString();
            //        obj.Email = r["Email"].ToString();
            //        obj.PanNo = r["PanNumber"].ToString();
            //        obj.City = r["City"].ToString();
            //        obj.State = r["State"].ToString();
            //        obj.Status = r["MemberStatus"].ToString();
            //        obj.Address = r["Address"].ToString();
            //        // obj.PanNo = r["PanNumber"].ToString();
            //        obj.BranchName = r["BranchName"].ToString();
            //        obj.DesignationName = r["DesignationName"].ToString();
            //        obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
            //        obj.Password = Crypto.Decrypt(r["Password"].ToString());
            //        lst.Add(obj);
            //    }
            //    model.lstTrad = lst;
            //}
            return View(model);
        }
        [HttpPost]
        [ActionName("RegistrationDetails")]
        [OnAction(ButtonName = "btnSearchCustomer")]
        public ActionResult AssociateListbyId(TraditionalAssociate model)
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
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    lst.Add(obj);
                }
                model.lstTrad = lst;
            }
            return View(model);
        }
        #endregion
        #region Customer
        public ActionResult CustomerList()
        {
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            return View();
        }
        [HttpPost]
        [ActionName("CustomerList")]
        [OnAction(ButtonName = "SearchCustomer")]
        public ActionResult CustomerListDetails(AssociateBooking model)
        {
            List<AssociateBooking> lstCustomer = new List<AssociateBooking>();
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            model.UserID = Session["Pk_userId"].ToString();
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;

            DataSet ds = model.GetListCustomer();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();
                    obj.Fk_SponsorId = r["FK_SponsorId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateID = r["AssociateId"].ToString();
                    obj.AssociateName = r["AssociateName"].ToString();
                    obj.UserID = r["PK_UserId"].ToString();
                    obj.SponsorID = r["SponsorId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.PaidAmount = r["TotalApprovedAmount"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.LastName = r["LastName"].ToString();
                    obj.Contact = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.City = r["City"].ToString();
                    obj.State = r["State"].ToString();
                    obj.Address = r["Address"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.BranchName = r["BranchName"].ToString();
                    obj.JoiningFromDate = r["JoiningDate"].ToString();
                    obj.EncryptKey = Crypto.Encrypt(r["PK_UserId"].ToString());
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    lstCustomer.Add(obj);
                }
                ViewBag.TotalPaidAmount = double.Parse(ds.Tables[0].Compute("sum(TotalApprovedAmount)", "").ToString()).ToString("n2");
                model.lstCustomer = lstCustomer;
            }
            return View(model);
        }
        #endregion

        public ActionResult ReturnBenefitReport(Reports model)
        {

            List<SelectListItem> Status = Common.Status();
            ViewBag.Status = Status;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst1 = new List<Reports>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds11 = model.ReturnBenefitAssociate();

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
                model.AddedBy = Session["Pk_userId"].ToString();
                DataSet dsRegistration = new DataSet();
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

                if (model.RegistrationType == "onlyRegistration")
                {
                    dsRegistration = model.CustomerRegistration();
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (model.Contact != null)
                        {
                            if (dsRegistration != null && dsRegistration.Tables.Count > 0)
                            {
                                if (dsRegistration.Tables[1] != null && dsRegistration.Tables[1].Rows.Count > 0)
                                {
                                    if (dsRegistration.Tables[1].Rows[0][0].ToString() == "1")
                                    {
                                        string body = "";
                                        try
                                        {
                                            body = "Dear " + model.FirstName + ' ' + model.LastName + ", You have been successfully registered as Customer in Blossom City. Your Login Id is " + dsRegistration.Tables[1].Rows[0]["LoginId"].ToString() + " and Password is " + Crypto.Decrypt(dsRegistration.Tables[1].Rows[0]["Password"].ToString());
                                            BLSMS.SendSMSNew(model.Contact, body);

                                            Session["Confirm_Name"] = dsRegistration.Tables[1].Rows[0]["Name"].ToString();
                                            Session["Confirm_LoginId"] = dsRegistration.Tables[1].Rows[0]["LoginId"].ToString();
                                            Session["Confirm_Password"] = Crypto.Decrypt(dsRegistration.Tables[1].Rows[0]["Password"].ToString());
                                            Session["Type"] = "Customer";
                                            //Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[2].Rows[0]["PKUserID"].ToString());
                                            TempData["Registration"] = "Registered Successfully !";
                                            FormName = "ConfirmRegistration";
                                            Controller = "AssociateDashboard";
                                        }
                                        catch (Exception ex)
                                        {
                                            FormName = "ConfirmRegistration";
                                            Controller = "AssociateDashboard";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    dsRegistration = model.CustomerRegistrationByAssociate();
                    if (dsRegistration.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        if (model.Contact != null)
                        {
                            string body = "";
                            try
                            {
                                body = "Dear " + model.FirstName + model.LastName + ", You have been successfully requested as Customer in Blossom City. After Some time you will get Login Id and Password.";
                                BLSMS.SendSMSNew(model.Contact, body);
                                TempData["Registration"] = "Registration Request Successful !";
                                FormName = "CustomerRegistration";
                                Controller = "AssociateDashboard";
                            }
                            catch (Exception ex)
                            {
                                FormName = "CustomerRegistration";
                                Controller = "AssociateDashboard";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }



            return RedirectToAction(FormName, Controller);
        }
        #endregion

        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                Common model = new Common();
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
                return View(ex.Message);
            }
        }
        public ActionResult LuckyDrawRequest()
        {
            Customer model = new Customer();
            Common obj = new Common();
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            List<SelectListItem> LuckyStatus = Common.RequestStatus();
            ViewBag.ddlstatus = LuckyStatus;
            model.UserID = Session["Pk_userId"].ToString();
            List<Customer> lst1 = new List<Customer>();
            DataSet ds11 = model.GetLuckyDrawRequestList();
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
                    Obj.CustomerLoginID = r["LoginId"].ToString();
                    Obj.CustomerName = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.Contact = r["MobileNo"].ToString();
                    Obj.AssociateID = r["AssociateId"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.CouponNo = r["CouponNo"].ToString();
                    Obj.status = r["Status"].ToString();
                    Obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    string afterFounder = "";
                    int pos = 4;
                    if (pos >= 0)
                    {
                        afterFounder = Obj.CouponNo.Remove(pos);
                    }
                    Obj.CouponNo = afterFounder + "******";
                    // Obj.Description = r["Description"].ToString();
                    lst1.Add(Obj);
                }
                ViewBag.TotalAmount = double.Parse(ds11.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                model.ListCust = lst1;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult LuckyDrawRequest(Customer model)
        {
            List<Customer> luckDrawRequest = new List<Customer>();
            Common obj = new Common();
            List<SelectListItem> Leg = Common.Leg();
            ViewBag.ddlleg = Leg;
            List<SelectListItem> LuckyStatus = Common.RequestStatus();
            ViewBag.ddlstatus = LuckyStatus;
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
            if (model.PaymentMode == "0")
            {
                model.PaymentMode = null;
            }
            #endregion
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            model.UserID = Session["Pk_userId"].ToString();
            DataSet ds = model.GetLuckyDrawRequestList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer Obj = new Customer();
                    Obj.CustomerLoginID = r["LoginId"].ToString();
                    Obj.CustomerName = r["Name"].ToString();
                    Obj.Email = r["Email"].ToString();
                    Obj.Contact = r["MobileNo"].ToString();
                    Obj.AssociateID = r["AssociateId"].ToString();
                    Obj.AssociateName = r["AssociateName"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.Leg = r["Leg"].ToString();
                    Obj.CouponNo = r["CouponNo"].ToString();
                    Obj.status = r["Status"].ToString();
                    Obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    string afterFounder = "";
                    int pos = 4;
                    if (pos >= 0)
                    {
                        afterFounder = Obj.CouponNo.Remove(pos);
                    }
                    Obj.CouponNo = afterFounder + "******";
                    luckDrawRequest.Add(Obj);
                }
                model.ListCust = luckDrawRequest;
                ViewBag.TotalAmount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }


        public ActionResult LuckyDrawList()
        {

            Customer model = new Customer();
            Common obj = new Common();
            model.UserID = Session["Pk_userId"].ToString();
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
                    // Obj.Description = r["Description"].ToString();
                    lst1.Add(Obj);
                }
                model.ListCust = lst1;

            }

            return View(model);
        }

        [HttpPost]
        public ActionResult LuckyDrawList(Customer model)
        {
            List<Customer> luckDrawRequest = new List<Customer>();
            Common obj = new Common();
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
            if (model.PaymentMode == "0")
            {
                model.PaymentMode = null;
            }
            model.JoiningFromDate = string.IsNullOrEmpty(model.JoiningFromDate) ? null : Common.ConvertToSystemDate(model.JoiningFromDate, "dd/MM/yyyy");
            model.JoiningToDate = string.IsNullOrEmpty(model.JoiningToDate) ? null : Common.ConvertToSystemDate(model.JoiningToDate, "dd/MM/yyyy");
            model.UserID = Session["Pk_userId"].ToString();
            DataSet ds = model.GetLuckyDrawCustomerList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Customer Obj = new Customer();
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
                    // Obj.Description = r["Description"].ToString();
                }
                model.ListCust = luckDrawRequest;
            }
            return View(model);
        }
        public ActionResult GetBookingNo(string ReferBy)
        {
            Common obj = new Common();
            DataSet ds = obj.GetBookingNo(ReferBy);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Result = "Already used. Please enter another booking no.";
            }
            else

            { obj.Result = "Yes"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

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

            return View(model);
        }
        [HttpPost]
        [ActionName("HoldPlot")]
        [OnAction(ButtonName = "Save")]
        public ActionResult HoldPlot(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_userId"].ToString();
                obj.Hold_From = DateTime.Now;
                if (obj.Amount != null && obj.Amount != "0")
                {
                    obj.Hold_To = obj.Hold_From.AddHours(480);
                }
                else
                {
                    obj.Hold_To = obj.Hold_From.AddHours(48);
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
                        try
                        {
                            string body = "Dear " + ds.Tables[0].Rows[0]["Name"].ToString() + ", You have been successfully registered as Customer in Blossom City. Your Login Id is " + ds.Tables[0].Rows[0]["LoginId"].ToString() + " and Password is " + Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            BLSMS.SendSMSNew(obj.Mobile, body);

                            Session["Confirm_Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                            Session["Confirm_LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Confirm_Password"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            Session["Type"] = "Associate";
                            //Session["PKUserID"] = Crypto.Encrypt(dsRegistration.Tables[2].Rows[0]["PKUserID"].ToString());
                            TempData["Plot"] = " Plot is on Hold !";
                            FormName = "ConfirmRegistration";
                            Controller = "AssociateDashboard";
                        }
                        catch (Exception ex)
                        {
                            TempData["Plot"] = ex.Message;
                            FormName = "HoldPlot";
                            Controller = "AssociateDashboard";
                        }
                    }
                    else
                    {
                        TempData["Plot"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "HoldPlot";
                        Controller = "AssociateDashboard";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
                FormName = "HoldPlot";
                Controller = "AssociateDashboard";
            }
            return RedirectToAction(FormName, Controller);
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

        public ActionResult GetCustomerDetails(string ReferBy)
        {
            Home obj = new Home();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetCustomerDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();

                obj.Result = "Yes";

            }
            else { obj.Result = "Invalid Customer Id"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
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
        public ActionResult TemporaryPlotHold(string PK_PlotHoldID)
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
        [ActionName("TemporaryPlotHold")]
        [OnAction(ButtonName = "Save")]
        public ActionResult TemporaryPlotHold(Plot obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.AddedBy = Session["Pk_userId"].ToString();

                obj.Hold_From = DateTime.Now;
                obj.Hold_To = obj.Hold_From.AddHours(48);
                DataSet ds = obj.SaveTemporaryPlotHold();
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
            FormName = "TemporaryPlotHold";
            Controller = "AssociateDashboard";

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
            model.AssociateID = Session["Pk_userId"].ToString();
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
                    //if (count1 == 0)
                    //{
                    //    ddlSite.Add(new SelectListItem { Text = "Select Site", Value = "0" });
                    //}
                    ddlSite.Add(new SelectListItem { Text = r["SiteName"].ToString(), Value = r["PK_SiteID"].ToString() });
                    count1 = count1 + 1;

                }
            }
            ViewBag.ddlSite = ddlSite;
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
            model.AddedBy = Session["Pk_userId"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetPlotHoldListAssociate();

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
            DataSet dsblock = model.GetBlockList();


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
            return View(model);
            #endregion
        }
        public ActionResult PrintPlotHold(string id)
        {
            Plot newdata = new Plot();
            newdata.EncryptKey = Crypto.Decrypt(id);
            newdata.AddedBy = Session["Pk_userId"].ToString();
            newdata.PK_PlotHoldID = Crypto.Decrypt(id);
            //  ViewBag.Name = Session["Name"].ToString();
            DataSet ds = newdata.GetPlotHoldListAssociate();

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

        public ActionResult ApprovedBookingList()
        {
            Plot model = new Plot();
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
        [ActionName("ApprovedBookingList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ApprovedBookingList(Plot model)
        {
            List<Plot> lst = new List<Plot>();
            model.AddedBy = Session["Pk_userId"].ToString();
            model.SiteID = model.SiteID == "0" ? null : model.SiteID;
            model.SectorID = model.SectorID == "0" ? null : model.SectorID;
            model.BlockID = model.BlockID == "0" ? null : model.BlockID;
            model.PaymentPlanID = model.PaymentPlanID == "0" ? null : model.PaymentPlanID;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");

            DataSet ds = model.GetBookingDetailsListAssociate();

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
                        obj.PlotRate = r["PlotRate"].ToString();
                        obj.Type = r["Type"].ToString();
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


            List<SelectListItem> ddlSector = new List<SelectListItem>();
            ddlSector.Add(new SelectListItem { Text = "Select Phase", Value = "0" });
            ViewBag.ddlSector = ddlSector;

            List<SelectListItem> ddlBlock = new List<SelectListItem>();
            ddlBlock.Add(new SelectListItem { Text = "Select Block", Value = "0" });
            ViewBag.ddlBlock = ddlBlock;
            return View(model);
        }
    }
}