using AsterHeight.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AsterHeight.Controllers
{
    public class KYCController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage UploadAadhar()
        {
            Response resp = new Response();
            KYCDocument obj = new KYCDocument();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                if (HttpContext.Current.Request.Params["UserID"].ToString() == "" || HttpContext.Current.Request.Params["UserID"] == null)
                {
                    resp.Status = "1";
                    resp.Message = "Please Enter User Id";
                    return Request.CreateResponse(HttpStatusCode.OK,
                  resp);
                }
                if (HttpContext.Current.Request.Params["AdharNumber"] == "" || HttpContext.Current.Request.Params["AdharNumber"] == null)
                {
                    resp.Status = "1";
                    resp.Message = "Please Enter Aadhar Number.";
                    return Request.CreateResponse(HttpStatusCode.OK,
                  resp);
                }
                if (HttpContext.Current.Request.Files.Count == 0)
                {
                    resp.Status = "1";
                    resp.Message = "Please Choose any file";
                    return Request.CreateResponse(HttpStatusCode.OK,
                  resp);
                }
                obj.UserID =  HttpContext.Current.Request.Params["UserID"];
                obj.AdharNumber = HttpContext.Current.Request.Params["AdharNumber"];
                var File = HttpContext.Current.Request.Files[0];
                obj.AdharImage =  rn.Next(10, 100) + "_aadhar_" + DateTime.Now.ToString("ddmmyyhhmmss") + obj.UserID.ToString() + "_" + File.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/KYCDocuments/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                File.SaveAs(folderPath + obj.AdharImage);
                obj.AdharImage = "/KYCDocuments/" + obj.AdharImage;
                obj.ActionStatus = "Adhar";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        resp.Status = "0";
                        resp.Message = "Aadhar Uploaded successfully..";

                    }
                    else
                    {
                        resp.Status = "1";
                        resp.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                resp.Status = "1";
                resp.Message = ex.Message;

            }
            return Request.CreateResponse(HttpStatusCode.OK,
                   resp);
        }
        [HttpPost]
        public HttpResponseMessage UploadPan()
        {
            Response resp = new Response();
            KYCDocument obj = new KYCDocument();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                if (HttpContext.Current.Request.Params["UserID"].ToString() == "" || HttpContext.Current.Request.Params["UserID"] == null)
                {
                    resp.Status = "1";
                    resp.Message = "Please Enter User Id";
                    return Request.CreateResponse(HttpStatusCode.OK,
                  resp);
                }
                if (HttpContext.Current.Request.Params["PanNumber"] == "" || HttpContext.Current.Request.Params["PanNumber"] == null)
                {
                    resp.Status = "1";
                    resp.Message = "Please Enter PAN Number.";
                    return Request.CreateResponse(HttpStatusCode.OK,
                  resp);
                }
                if ( HttpContext.Current.Request.Files.Count == 0)
                {
                    resp.Status = "1";
                    resp.Message = "Please Choose any file";
                    return Request.CreateResponse(HttpStatusCode.OK,
                  resp);
                }
                obj.UserID = HttpContext.Current.Request.Params["UserID"];
                obj.PanNumber = HttpContext.Current.Request.Params["PanNumber"];
                var File = HttpContext.Current.Request.Files[0];
                obj.PanImage =  rn.Next(10, 100) + "_pan_" + DateTime.Now.ToString("ddmmyyhhmmss") + obj.UserID.ToString() + "_" + File.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/KYCDocuments/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                File.SaveAs(folderPath + obj.PanImage);
                obj.PanImage = "/KYCDocuments/" + obj.PanImage;
                obj.ActionStatus = "Pan";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        resp.Status = "0";
                        resp.Message = "PAN Uploaded successfully..";

                    }
                    else
                    {
                        resp.Status = "1";
                        resp.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                resp.Status = "1";
                resp.Message = ex.Message;

            }
            return Request.CreateResponse(HttpStatusCode.OK,
                   resp);
        }
        [HttpPost]
        public HttpResponseMessage UploadDocument()
        {
            Response resp = new Response();
            KYCDocument obj = new KYCDocument();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                if (HttpContext.Current.Request.Params["UserID"].ToString() == "" || HttpContext.Current.Request.Params["UserID"] == null)
                {
                    resp.Status = "1";
                    resp.Message = "Please Enter User Id";
                    return Request.CreateResponse(HttpStatusCode.OK,
                  resp);
                }
                if (HttpContext.Current.Request.Params["DocumentNumber"] == "" || HttpContext.Current.Request.Params["DocumentNumber"] == null)
                {
                    resp.Status = "1";
                    resp.Message = "Please Enter Document Number.";
                    return Request.CreateResponse(HttpStatusCode.OK,
                  resp);
                }
                if (HttpContext.Current.Request.Files.Count == 0)
                {
                    resp.Status = "1";
                    resp.Message = "Please Choose any file";
                    return Request.CreateResponse(HttpStatusCode.OK,
                  resp);
                }
                obj.UserID = HttpContext.Current.Request.Params["UserID"];
                obj.DocumentNumber = HttpContext.Current.Request.Params["DocumentNumber"];
                var File = HttpContext.Current.Request.Files[0];
                obj.DocumentImage =  rn.Next(10, 100) + "_doc_" + DateTime.Now.ToString("ddmmyyhhmmss") + obj.UserID.ToString() + "_" + File.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/KYCDocuments/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                File.SaveAs(folderPath + obj.DocumentImage);
                obj.DocumentImage = "/KYCDocuments/" + obj.DocumentImage;
                obj.ActionStatus = "Doc";
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        resp.Status = "0";
                        resp.Message = "Document Uploaded successfully..";

                    }
                    else
                    {
                        resp.Status = "1";
                        resp.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                resp.Status = "1";
                resp.Message = ex.Message;

            }
            return Request.CreateResponse(HttpStatusCode.OK,
                   resp);
        }
    }
}
