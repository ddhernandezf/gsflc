using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace Transporte.Report
{
    public static class Tools
    {
        public static byte[] XtraReportExport(XtraReport dxReport, Dictionary<string, object> parameters, XtraReportExportOptions doctype)
        {
            MemoryStream ms = new MemoryStream();

            try
            {
                if (dxReport == null)
                    throw new Exception("Archivo fuente de reporte no encontrado.");

                parameters.ToList().ForEach(x =>
                {
                    dxReport.Parameters[x.Key].Value = x.Value;
                });

                if (doctype == XtraReportExportOptions.XLS)
                {
                    dxReport.CreateDocument();
                    XlsxExportOptions opts = new XlsxExportOptions()
                    {
                        ExportMode = XlsxExportMode.SingleFile,
                        SheetName = "Reporte",
                        TextExportMode = TextExportMode.Value
                    };
                    dxReport.ExportToXlsx(ms, opts);

                    dxReport.ExportToXlsx(ms, opts);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                else
                {
                    dxReport.CreateDocument();
                    PdfExportOptions opts = new PdfExportOptions();
                    dxReport.ExportToPdf(ms, opts);

                    opts.ShowPrintDialogOnOpen = false;
                    dxReport.ExportToPdf(ms, opts);
                    ms.Seek(0, SeekOrigin.Begin);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }

            return ms.ToArray();
        }
    }
}
