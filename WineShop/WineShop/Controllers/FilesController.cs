using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WineShop.Controllers
{
    public class FilesController : Controller
    {
        // GET: Files
        public FileResult Commands(string num)
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
        public FileResult Invoices(string num)
        {
            Microsoft.Reporting.WebForms.ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
            rv.LocalReport.ReportPath = Server.MapPath("/Reports/ReportCustomerCommand.rdlc");

            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter vwCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandDetailsTableAdapter tbCustomerCommandDetailsTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandDetailsTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandDataTable vwCustomerCommands = vwCustomerCommandTableAdapter.GetDataByNumCommand(num);
            Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandRow row = (Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandRow)vwCustomerCommands.Rows[0];

            ReportDataSource rdsCustomerCommand = new ReportDataSource("DataSetCustomerCommand") { Value = vwCustomerCommands };
            ReportDataSource rdsCustomerCommandDetail = new ReportDataSource("DataSetCustomerCommandDetail") { Value = tbCustomerCommandDetailsTableAdapter.GetDataByUidCustomerCommand(row.Uid) };

            rv.LocalReport.DataSources.Add(rdsCustomerCommand);
            rv.LocalReport.DataSources.Add(rdsCustomerCommandDetail);

            rv.LocalReport.Refresh();

            byte[] bytes = rv.LocalReport.Render("PDF", null, out string mimeType, out string encoding, out string extension, out string[] streamids, out Warning[] warnings);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            return File(ms, "Application/pdf");
        }
    }
}