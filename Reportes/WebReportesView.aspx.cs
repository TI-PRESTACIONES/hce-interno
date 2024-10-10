using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;

using System.Data;
using System.Drawing.Printing;
using SoluccionSalud.Entidades.Entidades;

using System.IO;
using SoluccionSalud.RepositoryReport;
using SoluccionSalud.RepositoryReport.Reportes_Service;

namespace AppSaludMVC.Reportes
{  
    
   
 
  
        public partial class WebReportesView : System.Web.UI.Page
        {


            #region Variables Publicas

            public DataTable objTabla1 = new DataTable(); //Recibe Datos para armar el reporte
            public PrintDocument Prd = new PrintDocument();
            public ReportDocument Rpt = new ReportDocument();//crystal report
            public DataSet dsRptViewer = new DataSet();//Para Crear el xml de los reportes
            public string imgIzquierda = "";//Logos
            public string imgDerecha = "";//Logos

            #endregion

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!Page.IsPostBack)
                {

                    if (Request.QueryString["ReportID"] != null)
                    {
                        // Agregados
                        string reportID = Request.QueryString["ReportID"].ToString();
                        string Visor = Request.QueryString["Visor"].ToString();
                        

                        switch (reportID)
                        {
                            case "CCEP0003": GenerarReporteReceta(Visor);
                                break;
                            case "ReportExamenes": GenerarReporteReceta("");
                                break;
                        }
                    }
                }
            }
            private void GenerarReporteReceta(String tipoVista)
            {
               /* Rpt.Load(Server.MapPath("rptReports/rptViewAnamnesisEA.rpt"));

                List<rptViewAnamnesisEA> rptViewAnamnesisEAList = new List<rptViewAnamnesisEA>();
                SS_HC_Anamnesis_EA objAnamnesisEA = new SS_HC_Anamnesis_EA();

                objAnamnesisEA.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
                objAnamnesisEA.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
                objAnamnesisEA.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
                objAnamnesisEA.IdEpisodioAtencion = (int)ENTITY_GLOBAL.Instance.EpisodioAtencion;
                objAnamnesisEA.Accion = "REPORTE_RECETA";
                rptViewAnamnesisEAList = ServiceReportes.ReporteAnamnesisEA(objAnamnesisEA, 0, 0);

                objTabla1 = new System.Data.DataTable();
                DataSet obj = new DataSet();

                //dsRptViewer.Tables.Add(objTabla1.Copy());
                //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/ListadoIngresantes.xml")));
                Rpt.SetDataSource(rptViewAnamnesisEAList);
                if (rptViewAnamnesisEAList.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                }
                else
                {
                    if (tipoVista == "VISTA")
                    {
                        visorReportes.ReportSource = Rpt;
                        visorReportes.DataBind();
                    }
                    else {
                        Response.Buffer = false;
                        Response.ClearContent();
                        Response.ClearHeaders();
                        try
                        {
                            Rpt.ExportToHttpResponse
                            (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "RECETA");
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                    
                }*/
                //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                //Rpt.SetParameterValue("imgDerecha", imgDerecha);
            }
            protected void CrystalReportViewer1_Navigate(object source, CrystalDecisions.Web.NavigateEventArgs e)
            {
                GenerarReporteReceta("");
            }
            private DataTable ConvertListToDataTable(List<VW_ATENCIONPACIENTE> list)
            {
                DataTable table = new DataTable();

                return table;
            }
            protected void Button1_Click(object sender, EventArgs e)
            {
                if (Request.QueryString["ReportID"] != null)
                {
                    // Agregados
                    string reportID = Request.QueryString["ReportID"].ToString();
                    switch (reportID)
                    {
                        case "ReportReceta": GenerarRecetaPDF_Click(sender, e);
                            break;
                        case "ReportExamenes": GenerarRecetaPDF_Click(sender, e);
                            break;
                    }
                }
            }
            protected void GenerarRecetaPDF_Click(object sender, EventArgs e)
            { 

            }
       
        }
    }
