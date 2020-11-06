using AdministracionMinisterio.Models;
using AdministracionMinisterio.Reportes;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AdministracionMinisterio.Services
{
    public class ExportarPdfService
    {
        Entities db = new Entities();

        public byte[] Ministerios(List<Ministerio> ministerios)
        {
            // creando-completando el dataset
            MinisteriosDataSet ministerio = new MinisteriosDataSet();

            string[] fecha = new string[] {
                DateTime.Now.ToString()
                };
            ministerio.Fecha.Rows.Add(fecha);

            // agregando una lista de datos al detallecomprobante
            foreach (var item in ministerios)
            {
                string[] ministerioFila = new string[] {
                item.Nombre.ToString(),
                item.Localidad.ToString(),
                item.Direccion.ToString()
                };
                // agregar a la tabla detalleMateriales del dataSet la fila que creamos en la linea 34
                ministerio.Ministerios.Rows.Add(ministerioFila);
            }
            return CreatePDF(ministerio.Ministerios, ministerio.Fecha);
        } // fin LiquidacionCompra

        private byte[] CreatePDF(DataTable dt1, DataTable dt2)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;

            viewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(string.Format("~/Reportes/ReporteMinisterios.rdlc"));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("Ministerios", dt1));
            viewer.LocalReport.DataSources.Add(new ReportDataSource("Fecha", dt2));

            viewer.LocalReport.Refresh();
            byte[] bytes = viewer.LocalReport.Render("Pdf", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            return bytes;
        }
    }
}