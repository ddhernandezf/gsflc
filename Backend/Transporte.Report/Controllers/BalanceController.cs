using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Transporte.API;
using Transporte.BL;
using Transporte.Model.Report;
using BalanceBL = Transporte.BL.Report.Balance;
using BalanceModel = Transporte.Model.Report.Balance;
using BalanceReport = Transporte.Report.Reports.Balance;
using BalanceSingleReport = Transporte.Report.Reports.BalanceSingle;

namespace Transporte.Report.Controllers
{
    [Route("Balance")]
    [ApiController]
    public class BalanceController : CustomController
    {
        private BalanceBL bl { get; } = new BalanceBL(Tool.configuration);

        [HttpPost("Tipo/{doctype}/Opcion/{option}")]
        public IActionResult General(XtraReportExportOptions? doctype, BalanceOptions option, [FromBody] BalanceFilter filter)
        {
            try
            {
                if (doctype == null)
                    throw new Exception("Debe expecificar el formato del archivo");

                if (filter == null)
                    throw new Exception("No existen opciones de filtrado");

                List<BalanceModel> data = new List<BalanceModel>() { bl.General(filter, option) };

                string reportTitle = "";
                switch (option)
                {
                    case BalanceOptions.BALANCE:
                        reportTitle = "REPORTE DE BALANCE DE SERVICIOS Y GASTOS";
                        break;
                    case BalanceOptions.SERVICE:
                        reportTitle = "REPORTE DE SERVICIOS";
                        break;
                    case BalanceOptions.EXPENSE:
                        reportTitle = "REPORTE DE GASTOS";
                        break;
                }

                string reportSubTitle = "";
                if (filter.monthAndYear)
                    reportSubTitle = $"{GetMonthName((byte)filter.month)} {filter.year}";
                else
                {
                    if (filter.dateRange)
                    {
                        string start = ((DateTime)filter.startDate).ToString("dd/MM/yyyy");
                        string end = ((DateTime)filter.endDate).ToString("dd/MM/yyyy");
                        reportSubTitle = $"Del {start} al {end}";
                    }
                    else
                        reportSubTitle = ((DateTime)filter.startDate).ToString("dd/MM/yyyy");
                }

                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("rptTitle", reportTitle);
                parameters.Add("rptSubTitle", reportSubTitle);
                parameters.Add("rptUser", userName);

                string contentType = doctype == XtraReportExportOptions.XLS ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" : "application/pdf";
                string extFile = doctype == XtraReportExportOptions.XLS ? "xlsx" : "pdf";
                byte[] byteReport;
                switch (option)
                {
                    case BalanceOptions.BALANCE:
                        BalanceReport rptBalance = new BalanceReport() { DataSource = data };
                        byteReport = Tools.XtraReportExport(rptBalance, parameters, (XtraReportExportOptions)doctype);

                        if (doctype == XtraReportExportOptions.BYTE)
                            return Ok(rptBalance);

                        if (byteReport.Length > 0)
                            return File(byteReport, contentType, $"Reporte balance {DateTime.Now:yyyyMMddHHmmss}.{extFile}");

                        return Ok();
                    default:
                        BalanceSingleReport rptSingle = new BalanceSingleReport() { DataSource = data };
                        byteReport = Tools.XtraReportExport(rptSingle, parameters, (XtraReportExportOptions)doctype);
                        string fileName = option == BalanceOptions.SERVICE ? "Reporte servicios" : "Reporte gastos";

                        if (doctype == XtraReportExportOptions.BYTE)
                            return Ok(rptSingle);

                        if (byteReport.Length > 0)
                            return File(byteReport, contentType, $"{fileName} {DateTime.Now:yyyyMMddHHmmss}.{extFile}");

                        return Ok();
                }
            }
            catch (Exception ex)
            {
                //string error = ex.CustomResponse();
                //Log.ErrorToLog(userName, this, MethodBase.GetCurrentMethod(), error);
                //return BadRequest(error);
                return BadRequest(ex);
            }
        }
    }
}