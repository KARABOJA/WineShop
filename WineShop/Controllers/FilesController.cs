using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WineShop.Controllers
{
    public class FilesController : Controller
    {
        // GET: Files
        public FileResult Command(string num)
        {
            Microsoft.Reporting.WebForms.ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            rv.LocalReport.ReportPath = Server.MapPath("/Reports/ReportCustomerCommand.rdlc");

            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter vwCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandDetailsTableAdapter tbCustomerCommandDetailsTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandDetailsTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandDataTable vwCustomerCommands= vwCustomerCommandTableAdapter.GetDataByNumCommand(num);
                Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandRow row = (Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandRow)vwCustomerCommands.Rows[0];

            ReportDataSource rdsCustomerCommand = new ReportDataSource("DataSetCustomerCommand") { Value = vwCustomerCommands };
            ReportDataSource rdsCustomerCommandDetail = new ReportDataSource("DataSetCustomerCommandDetail") { Value = tbCustomerCommandDetailsTableAdapter.GetDataByUidCustomerCommand(row.Uid) };

            rv.LocalReport.DataSources.Add(rdsCustomerCommand);
            rv.LocalReport.DataSources.Add(rdsCustomerCommandDetail);

            rv.LocalReport.Refresh();

            byte[] bytes = rv.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string extension, out string[] streamids,out Warning[] warnings);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            return File(ms, "Application/pdf");
        }
        public FileResult Invoice(string num)
        {
            Microsoft.Reporting.WebForms.ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            rv.LocalReport.ReportPath = Server.MapPath("/Reports/ReportCustomerInvoice.rdlc");

            Datas.Ecommerce.DataSetCustomerInvoiceTableAdapters.vwCustomerInvoiceTableAdapter vwCustomerInvoiceTableAdapter = new Datas.Ecommerce.DataSetCustomerInvoiceTableAdapters.vwCustomerInvoiceTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetCustomerInvoiceTableAdapters.tbCustomerInvoiceDetailsTableAdapter tbCustomerInvoiceDetailsTableAdapter = new Datas.Ecommerce.DataSetCustomerInvoiceTableAdapters.tbCustomerInvoiceDetailsTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetCustomerInvoice.vwCustomerInvoiceDataTable vwCustomerInvoice = vwCustomerInvoiceTableAdapter.GetDataByNumInvoice(num);
            Datas.Ecommerce.DataSetCustomerInvoice.vwCustomerInvoiceRow row = (Datas.Ecommerce.DataSetCustomerInvoice.vwCustomerInvoiceRow)vwCustomerInvoice.Rows[0];

            ReportDataSource rdsCustomerInvoice = new ReportDataSource("DataSetCustomerInvoice") { Value = vwCustomerInvoice };
            ReportDataSource rdsCustomerInvoiceDetail = new ReportDataSource("DataSetCustomerInvoiceDetail") { Value = tbCustomerInvoiceDetailsTableAdapter.GetDataByUidCustomerInvoice(row.Uid) };

            rv.LocalReport.DataSources.Add(rdsCustomerInvoice);
            rv.LocalReport.DataSources.Add(rdsCustomerInvoiceDetail);

            rv.LocalReport.Refresh();

            byte[] bytes = rv.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string extension, out string[] streamids, out Warning[] warnings);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            return File(ms, "Application/pdf");
        }

        [HttpPost]
        public ActionResult UploadPhoto()
        {
            if (Session["CurrentCustomer"] == null) { return RedirectToAction("CustomerLogin", "Home"); }
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    string sFileName = Path.GetFileName(file.FileName);
                    BinaryReader reader = new BinaryReader(file.InputStream);
                    byte[] bytes = reader.ReadBytes((int)file.ContentLength);
                    Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter tbCustomerTableAdapter = new Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    if (tbCustomerTableAdapter.UpdatePhoto(
                        Tools.Security.Customer.GetCurrent().Uid
                        , bytes
                        , file.ContentType
                        , Tools.Security.Customer.GetCurrent().Uid).Equals(1))
                    {
                        ((Models.Customer)Session["CurrentCustomer"]).PhotoBytes = bytes;
                        ((Models.Customer)Session["CurrentCustomer"]).PhotoContentType = file.ContentType;
                    };
                }

            }
            return RedirectToAction("Profile", "Profile");
        }

        public ActionResult ImageProfile()
        {
            if (Tools.Security.Customer.GetCurrent().PhotoBytes != null)
            {
                byte[] imageByteData = Tools.Security.Customer.GetCurrent().PhotoBytes;
                return File(imageByteData, Tools.Security.Customer.GetCurrent().PhotoContentType);
            }
            else { return null; }
        }
    }
}