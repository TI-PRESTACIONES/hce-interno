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
using SoluccionSalud.Componentes;


namespace AppSaludMVC.Reportes
{
    using SvcAuditoriaImpresion = SoluccionSalud.Service.AuditoriaImpresionService.SvcAuditoriaImpresion;
    using System.Data.SqlClient;
    using System.Configuration;


    public partial class VisorReportesHCE : System.Web.UI.Page
    {
        #region Variables Publicas
        public DataTable objTabla1 = new DataTable(); //Recibe Datos para armar el reporte
        public PrintDocument Prd = new PrintDocument();
        public ReportDocument Rpt = new ReportDocument();//crystal report
        public DataSet dsRptViewer = new DataSet();//Para Crear el xml de los reportes
        public string imgIzquierda = "";//Logos  
        public string imgDolor = "";//Logos
        public string imgDerecha = "";//Logos
        public string imgDF = "";//Diagnostico funcional
        public string imgValor = "";
        public string imgEstado = "";
        public string imgValoracionSocio = " ";
        public string firma = "";//firma digital
        public string imgFirma = "";
        #endregion

        /***CONSTANTES**/
        #region Constantes de Form VERSION I REPORTES


        static string FORM_REPORT_EXPORT = "pdf";

        static string FORM_0000 = AppSaludMVC.Controllers.UTILES_MENSAJES.FORM_ANAMNESIS_EA_F1;
        static string FORM_0001 = "CCEP0306";
        static string FORM_0002 = "CCEP0102";
        static string FORM_0003 = "CCEP0304";
        static string FORM_0004 = "CCEP0104";
        static string FORM_0005 = "CCEP2010";
        static string FORM_0006 = "CCEP0055";
        static string FORM_0007 = "CCEP0253";
        static string FORM_0008 = "CCEP0004";
        static string FORM_0009 = "CCEP0313";
        static string FORM_0010 = "CCEP0311";
        static string FORM_0011 = "CCEP0315";
        static string FORM_0012 = "CCEP0302";


        /* Formularios Extras*/

        static string FORMFE_0001 = "CCEPF012";
        static string FORMFE_0002 = "CCEPF013";
        static string FORMFE_0003 = "CCEP00F3";
        static string FORMFE_0004 = "CCEP00F4";
        static string FORMFE_0005CAB = "CCEPF006";
        static string FORMFE_0005DET = "CCEPF006";
        static string FORMFE_0006 = "CCEPF002";
        static string FORMFE_0007 = "CCEPF014";
        static string FORMFE_0008 = "CCEPF015";
        static string FORMFE_0009 = "CCEPF016";
        static string FORMFE_0010 = "CCEPF017";
        static string FORMFE_0011 = "CCEPF018";
        static string FORMFE_0012 = "CCEP0F90";
        static string FORMFE_0013 = "CCEPF150";
        static string FORMFE_0014 = "CCEPF151";
        static string FORMFE_0015 = "CCEPF152";
        static string FORMFE_0016 = "CCEPF154";
        static string FORMFE_0017 = "CCEPF###";
        static string FORMFE_0018DET1 = "CCEPF100";
        static string FORMFE_0018DET2 = "CCEPF100";
        static string FORMFE_0019 = "CCEPF101";
        static string FORMFE_0019DET1 = "CCEPF101";
        static string FORMFE_0019DET2 = "CCEPF101";
        static string FORMFE_0019DET3 = "CCEPF101"; //Firmas(Imprimir todos)
        static string FORMFE_0020 = "CCEPF300";
        static string FORMFE_0021 = "CCEPF080";

        /* Formularios FED*/
        static string FORMFE_0030 = "CCEPF###";
        static string FORMFE_0031 = "CCEPF###";
        static string FORMFE_0032 = "CCEPF###";
        static string FORMFE_0033 = "CCEPF###";
        static string FORMFE_0034 = "CCEPF###";
        static string FORMFE_0035 = "CCEPF###";
        static string FORMFE_0036 = "CCEPF###";
        static string FORMFE_0037 = "CCEPF###";
        static string FORMFE_0038 = "CCEPF440";
        static string FORMFE_0039 = "CCEPF441";
        static string FORMFE_0040 = "CCEPF442";
        static string FORMFE_0041 = "CCEPF445";
        static string FORMFE_0042 = "CCEPF447";
        static string FORMFE_0043 = "CCEPF204";
        static string FORMFE_0044 = "CCEPF051";
        static string FORMFE_0045 = "CCEPF001";
        static string FORMFE_0046 = "CCEPF###";

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            // ReportViewer.ReportSource = ViewData["ReportData"];

            /*
             * Agregado
             * */

            if (Request.QueryString["ReportID"] != null)
            {
                // Agregados: REPORTE INICIAL
                string reportID = Request.QueryString["ReportID"].ToString();
                string Visor = Request.QueryString["Visor"].ToString();
                // AGREGADOS : REPORTE GENERICOS
                string unidRep = Request.QueryString["UR"];
                string epiClinico = Request.QueryString["EC"];
                string epiAten = Request.QueryString["EA"];
                string paciente = Request.QueryString["PA"];
                string formatos = Request.QueryString["FOR"];
                string usuario = Request.QueryString["US"];
                string nombreFileExp = Request.QueryString["FI"];

                //string usuarioActual = (ENTITY_GLOBAL.Instance != null ? ENTITY_GLOBAL.Instance.USUARIO : usuario);
                string usuarioActual = usuario;

                //CCEP0102    

                switch (reportID)
                {
                    case "CCEP0003": GenerarReporterptViewAnamnesisEA(Visor);
                        break;
                    case "CCEP0055": GenerarReporterptViewAnamnesisAF(Visor);
                        break;
                    case "CCEP0004": GenerarReporterptViewAnamnesisAP(Visor);
                        break;
                    case "CCEP0102": GenerarReporterptViewExamenTriajeFisico(Visor);
                        break;
                    case "CCEP0104": GenerarReporterptViewExamenFisicoRegional(Visor);
                        break;
                    case "CCEP0253": GenerarReporterptViewDiagnostico(Visor);
                        break;
                    case "CCEP2010": GenerarReporterptViewEvolucionObjetiva(Visor);
                        break;
                    case "CCEP0306": GenerarReporterptViewExamenSolicitado(Visor);
                        break;
                    case "CCEP0304": GenerarReporterptViewMedicamento(Visor);
                        break;
                    case "CCEP0313": GenerarReporterptViewEmitirDescansoMedico(Visor);
                        break;
                    case "CCEP0311": GenerarReporterptViewProximaAtencion(Visor);
                        break;

                    case "CCEP0315": GenerarReporterptViewSolicitarReferencia(Visor);
                        break;

                    case "CCEP0302": GenerarReporterptViewCuidadosPreventivos(Visor);
                        break;
                    // *** FORMULARIOS MASIVO ***
                    case "TOTALHC": GenerarReporterptViewTotalHCE(Visor);
                        break;
                    // *** FORMULARIOS PARA ERP ***
                    case "GENERICO_HCE": GenerarReporterptViewGeneralHCE(Visor, unidRep, epiClinico, epiAten, paciente, formatos, usuarioActual, nombreFileExp);
                        break;
                    case "ReportExamenes": GenerarReporteReceta("");
                        break;

                    // *** FORMULARIOS (EXTRAS) ***
                    case "CCEP0F90": GenerarReporterptViewDiagnostico_FE(Visor);
                        break;
                    case "CCEPF012": GenerarReporterptViewInmunizacionNinio_FE(Visor);
                        break;
                    case "CCEPF300": GenerarReporterptViewEmitirDescansoMedico_FE(Visor);
                        break;
                    case "CCEP00F2": GenerarReporterptViewAlergia_FE(Visor);
                        break;
                    case "CCEPF015": GenerarReporterptViewValoracionFuncionalAM_FE(Visor);
                        break;
                    case "CCEPF152": GenerarReporterptViewProximaAtencion_FE(Visor);
                        break;
                    case "CCEPF013": GenerarReporterptViewInmunizacionAdulto_FE(Visor);
                        break;
                    case "CCEPF154": GenerarReporterptViewApoyoDiagnostico_FE(Visor);
                        break;
                    case "CCEPF202": GenerarReporterptViewReferencia_FE(Visor);
                        break;
                    case "CCEPF150": GenerarReporterptViewExamenApoyoDiagnostico_FE(Visor);
                        break;
                    case "CCEPF018": GenerarReporterptValoracionAM_FE(Visor);
                        break;
                    case "CCEPF016": GenerarReporteValoracionMentalAM_FE(Visor);
                        break;
                    case "CCEPF080": GenerarReporteEvolucuionMedica_FE(Visor);
                        break;
                    case "CCEPF101": GenerarReporteMedicamentos_Fe(Visor);
                        break;
                    case "CCEPF151": GenerarReporteInterconsulta_FE(Visor);
                        break;
                    case "CCEPF017": GenerarReporteValoracionSocioFamAM_FE(Visor);
                        break;
                    case "CCEPF014": GenerarReporteAnamnesis_ANTFAM_FE(Visor);
                        break;
                    case "CCEPF203b": GenerarReporteContrarReferencia_FE(Visor);
                        break;
                    case "CCEPF301b": GenerarReporteSolicitudTransfusional_FE(Visor);
                        break;
                    case "CCEPF100": GenerarReporteDieta_FE(Visor);
                        break;
                    case "CCEP9918": GenerarReporterptViewSolicitudProducto(Visor);
                        break;
                    case "CCEP00F3": GenerarReporterptViewAntecedentesPersonalesFisiologico(Visor);
                        break;
                    case "CCEPF004": GenerarReporteAntFisiologicoPediatrico_FE(Visor);
                        break;
                    case "CCEPF006": GenerarReporteAntecedentesGeneralesPatologicos_FE(Visor);
                        break;
                    // Nuevos Formulario
                    case "CCEPF461": GenerarReporterptViewSeguridadCirugiaEntrada_FE(Visor);
                        break;
                    case "CCEPF463": GenerarReporterptViewSeguridadCirugiaSalida_FE(Visor);
                        break;
                    case "CCEPF462": GenerarReporterptViewSeguridadCirugiaPausa_FE(Visor);
                        break;
                    case "CCEPF464": GenerarReporterptViewEscalaAltaCirugiaAmbulatoria_FE(Visor);
                        break;
                    case "CCEPF444": GenerarReporterptViewEscalaAldrete_FE(Visor);
                        break;
                    case "CCEPF435": GenerarReporterptViewGradoDependencia_FE(Visor);
                        break;
                    case "CCEPF448": GenerarReporterptViewEscalaSedacionRass_FE(Visor);
                        break;
                    case "CCEPF440": GenerarReporterptViewEscalaGlasgow_FE(Visor);
                        break;
                    case "CCEPF441": GenerarReporterptViewEscalaGlasgowPreEscolar_FE(Visor);
                        break;
                    case "CCEPF442": GenerarReporterptViewEscalaGlasgowLactante_FE(Visor);
                        break;
                    case "CCEPF445": GenerarReporterptViewEscalaStewart_FE(Visor);
                        break;
                    case "CCEPF447": GenerarReporterptViewEscalaRamsay_FE(Visor);
                        break;
                    case "CCEPF204": GenerarReporterptViewRetiroVoluntario_FE(Visor);
                        break;
                    case "CCEPF446": GenerarReporterptViewEscalaBromage_FE(Visor);
                        break;
                    case "CCEPF431": GenerarReporterptViewDolorEvaAdulto_FE(Visor);
                        break;
                    case "CCEPF432": GenerarReporterptViewDolorEvaNinios_FE(Visor);
                        break;
                    case "CCEPF051": GenerarReporterptViewFuncionesVitales_FE(Visor);
                        break;
                    case "CCEPF001": GenerarReporterptViewEnfermedadActual_FE(Visor);
                        break;
                    case "CCEPF501": GenerarReporteEvolucionObstetricaPuerperio_FE(Visor);
                        break;
                    case "CCEPF425": GenerarReporteVigilanciaDispositivos_FE(Visor);
                        break;
                    case "CCEPF201": GenerarReporteInformeAlta_FE(Visor);
                        break;
                    case "CCEPF402": GenerarReporteBalanceHidroElectrolitico_FE(Visor, 1); //EN NEO
                        break;
                    case "CCEPF401": GenerarReporteBalanceHidroElectrolitico_FE(Visor, 2);  //EN SOP
                        break;
                    case "CCEPF403": GenerarReporteBalanceHidroElectrolitico_FE(Visor, 3);  //PEDIATRICO
                        break;
                    case "CCEPF400": GenerarReporteBalanceHidroElectrolitico_FE(Visor, 4);  //NORMAL
                        break;
                    // ***  FIN FORMULARIOS (EXTRAS) ***

                }

                Rpt.ExportToStream(ExportFormatType.PortableDocFormat);
                Response.ContentType = "application/pdf";

                var rutac = "C:/PDF/";

                if (!Directory.Exists(rutac))
                {
                    DirectoryInfo di = Directory.CreateDirectory(rutac);
                }

                Rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, "C:/PDF/Reporte.pdf");

                var Ruta = "C:/PDF/Reporte.pdf";

                System.IO.FileInfo fi = new System.IO.FileInfo(Ruta);



                var NombreServidor = fi.Name;
                var rutaServidor = Server.MapPath("../resources/DocumentosAdjuntos/");
                if (!Directory.Exists(rutaServidor))
                {
                    DirectoryInfo di = Directory.CreateDirectory(rutaServidor);
                }
                var PathServidor = rutaServidor + NombreServidor;
                System.IO.File.Copy(Ruta, PathServidor, true);
                //System.IO.FileInfo fiServidor = new System.IO.FileInfo(PathServidor);
                var PathOri = "../resources/DocumentosAdjuntos/" + NombreServidor;
                //   dtMes.Accion = PathOri;

                Response.Redirect(PathOri);
                //       Response.BinaryWrite(oStream.ToArray());
                Response.End();

            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            //  ((AllJobsSummaryReportLayout)ViewData["ReportData"]).Close();
            //  ((AllJobsSummaryReportLayout)ViewData["ReportData"]).Dispose();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // if (!Page.IsPostBack)
            //  {


            // }
        }
        private void GenerarReporteReceta(String tipoVista)
        {

            if (Request.QueryString["ReportID"] != null)
            {
                // Agregados
                string reportID = Request.QueryString["ReportID"].ToString();
                string Visor = Request.QueryString["Visor"].ToString();

                //CCEP0102    

                switch (reportID)
                {
                    case "CCEP0003": GenerarReporterptViewAnamnesisEA(Visor);
                        break;
                    case "CCEP0005": GenerarReporterptViewAnamnesisAF(Visor);
                        break;
                    case "CCEP0102": GenerarReporterptViewExamenTriajeFisico(Visor);
                        break;
                    case "CCEP0104": GenerarReporterptViewExamenFisicoRegional(Visor);
                        break;
                    case "CCEP0253": GenerarReporterptViewDiagnostico(Visor);
                        break;
                    case "CCEP2010": GenerarReporterptViewEvolucionObjetiva(Visor);
                        break;
                    case "CCEP0306": GenerarReporterptViewExamenSolicitado(Visor);
                        break;

                    case "CCEP0311": GenerarReporterptViewEmitirDescansoMedico(Visor);
                        break;

                    case "CCEP0314": GenerarReporterptViewSolicitarReferencia(Visor);
                        break;

                    case "ReportExamenes": GenerarReporteReceta("");
                        break;

                }



            }
            /* Rpt.Load(Server.MapPath("rptReports/rptViewAnamnesisEA.rpt"));

             List<rptViewAnamnesisEA> rptViewAnamnesisEAList = new List<rptViewAnamnesisEA>();
             SS_HC_Anamnesis_EA objAnamnesisEA = new SS_HC_Anamnesis_EA();
             objAnamnesisEA.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
             objAnamnesisEA.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
             objAnamnesisEA.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
             objAnamnesisEA.IdEpisodioAtencion = (int)ENTITY_GLOBAL.Instance.EpisodioAtencion; 
              objAnamnesisEA.Accion = "REPORTEA";
             rptViewAnamnesisEAList = ServiceReportes.ReporteAnamnesisEA(objAnamnesisEA, 0, 0);

             objTabla1 = new System.Data.DataTable();
             List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit>();
             SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit objRPT;
             if (rptViewAnamnesisEAList != null) {
                 foreach (rptViewAnamnesisEA objReport in rptViewAnamnesisEAList) // Loop through List with foreach.
                 { 
                         objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit();
                         objRPT.NombreCompleto = objReport.NombreCompleto;
                         listaRPT.Add(objRPT);  
                 }
             }
            
             //DataSet obj = new DataSet();
             //dsRptViewer.Tables.Add(objTabla1.Copy());
             //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
             Rpt.SetDataSource(listaRPT);
             if (rptViewAnamnesisEAList.Count == 0)
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
             }
             else
             {
                 if (tipoVista == "I")
                 {
                     CrystalReportViewer1.ReportSource = Rpt;
                     CrystalReportViewer1.DataBind();
                 }
                 else
                 {
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
        /**************/


        private void GenerarReporterptViewAnamnesisEA(String tipoVista)
        {
            String nombreRpt = "Enfermedad_Actual";
            string tura = Server.MapPath("rptReports/rptViewAnamnesisEA.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewAnamnesisEA.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit>();

            listaRPT = getDatarptViewAnamnesisEA("REPORTEA");

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, nombreRpt);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit>

        getDatarptViewAnamnesisEA(String accion)
        {

            List<rptViewAnamnesisEA> rptViewListaDB = new List<rptViewAnamnesisEA>();
            SS_HC_Anamnesis_EA objRptFiltro = new SS_HC_Anamnesis_EA();
            objRptFiltro.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
            objRptFiltro.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
            objRptFiltro.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
            objRptFiltro.IdEpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;
            objRptFiltro.Accion = "REPORTEA";
            rptViewListaDB = ServiceReportes.ReporteAnamnesisEA(objRptFiltro, 0, 0);

            objTabla1 = new System.Data.DataTable();
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit>();
            SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit objRPT;
            if (rptViewListaDB != null)
            {
                if (rptViewListaDB.Count > 0)
                {
                    ///////////////////////////////                    
                    //PARA LA AUDITORIA DE IMPRESION                    
                    setDataImpresionAuditoria(accion, 0, null, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
                    ///////////////////////////////            
                }

                foreach (rptViewAnamnesisEA objReport in rptViewListaDB) // Loop through List with foreach.
                {
                    objRPT = getObjetoReporteAnamnesisEA(objReport);
                    //objRPT.NombreCompleto = objReport.NombreCompleto;
                    //objRPT.Accion = "~/resources/images/logohce.png";
                    listaRPT.Add(objRPT);
                }
            }
            return listaRPT;
        }

        private void GenerarReporterptViewAnamnesisAF(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewAnamnesisAF.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewAnamnesisAF.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAFEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAFEdit>();

            listaRPT = getDatarptViewAnamnesisAF("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {

                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "ANAMNESISAF");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAFEdit>

        getDatarptViewAnamnesisAF(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario
            )
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAFEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAFEdit>();

            List<rptViewAnamnesisAF> rptViewAnamnesisAF = new List<rptViewAnamnesisAF>();
            SS_HC_Anamnesis_AF objAnamnesisAF = new SS_HC_Anamnesis_AF();
            objAnamnesisAF.UnidadReplicacion = unidadReplicacion;
            objAnamnesisAF.IdPaciente = idPaciente;
            objAnamnesisAF.EpisodioClinico = epiClinico;
            objAnamnesisAF.IdEpisodioAtencion = idEpiAtencion;
            objAnamnesisAF.Accion = "REPORTEA";
            rptViewAnamnesisAF = ServiceReportes.ReporteAnamnesisAF(objAnamnesisAF, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAFEdit objRPT;
            if (rptViewAnamnesisAF != null)
            {
                foreach (rptViewAnamnesisAF objReport in rptViewAnamnesisAF) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAFEdit();

                    objRPT.Expr103 = objReport.Expr103;
                    objRPT.Expr101 = objReport.Expr101;
                    objRPT.Expr3 = objReport.Expr3;

                    objRPT.Descripcion = objReport.Descripcion;
                    objRPT.Observaciones = objReport.Observaciones;


                    //GENERALES
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.Edad != null)
                    {
                        objRPT.Edad = Convert.ToInt32(objReport.Edad);
                    }
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    objRPT.Expr104 = objReport.Expr104;
                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewAnamnesisAF.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }

        private void GenerarReporterptViewExamenTriajeFisico(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewExamenTriajeFisico.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewExamenTriajeFisico.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenTriajeFisicoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenTriajeFisicoEdit>();
            listaRPT = getDatarptViewExamenTriajeFisico("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "TRIAJE");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }


        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenTriajeFisicoEdit>

        getDatarptViewExamenTriajeFisico(
            String tipoVista, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenTriajeFisicoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenTriajeFisicoEdit>();

            List<rptViewExamenTriajeFisico> rptViewExamenTriajeFisico = new List<rptViewExamenTriajeFisico>();
            SS_HC_ExamenFisico_Triaje objExamenTriajeFisico = new SS_HC_ExamenFisico_Triaje();
            objExamenTriajeFisico.UnidadReplicacion = unidadReplicacion;
            objExamenTriajeFisico.IdPaciente = idPaciente;
            objExamenTriajeFisico.EpisodioClinico = epiClinico;
            objExamenTriajeFisico.IdEpisodioAtencion = idEpiAtencion;
            objExamenTriajeFisico.Accion = "REPORTEA";
            rptViewExamenTriajeFisico = ServiceReportes.ReporteExamenFisico_Triaje(objExamenTriajeFisico, 0, 0);

            objTabla1 = new System.Data.DataTable();
            SoluccionSalud.RepositoryReport.Entidades.rptViewExamenTriajeFisicoEdit objRPT;
            if (rptViewExamenTriajeFisico != null)
            {
                foreach (rptViewExamenTriajeFisico objReport in rptViewExamenTriajeFisico) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewExamenTriajeFisicoEdit();

                    if (objReport.PresionMinima != null)
                    {
                        objRPT.PresionMinima = Convert.ToInt32(objReport.PresionMinima);
                    }


                    if (objReport.PresionMaxima != null)
                    {
                        objRPT.PresionMaxima = Convert.ToInt32(objReport.PresionMaxima);
                    }



                    if (objReport.FrecuenciaRespiratoria != null)
                    {
                        objRPT.FrecuenciaRespiratoria = Convert.ToInt32(objReport.FrecuenciaRespiratoria);
                    }

                    if (objReport.FrecuenciaCardiaca != null)
                    {
                        objRPT.FrecuenciaCardiaca = Convert.ToInt32(objReport.FrecuenciaCardiaca);
                    }

                    if (objReport.Temperatura != null)
                    {
                        objRPT.Temperatura = Convert.ToDecimal(objReport.Temperatura);
                    }

                    if (objReport.Peso != null)
                    {
                        objRPT.Peso = Convert.ToDecimal(objReport.Peso);
                    }

                    if (objReport.Talla != null)
                    {
                        objRPT.Talla = Convert.ToDecimal(objReport.Talla);
                    }

                    if (objReport.IndiceMasaCorporal != null)
                    {
                        objRPT.IndiceMasaCorporal = Convert.ToDecimal(objReport.IndiceMasaCorporal);
                    }


                    objRPT.Expr02 = objReport.Expr02;
                    objRPT.Expr01 = objReport.Expr01;
                    objRPT.Expr03 = objReport.Expr03;
                    objRPT.Expr04 = objReport.Expr04;
                    objRPT.Expr102 = objReport.Expr102;

                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;

                    objRPT.Expr104 = objReport.Expr104;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewExamenTriajeFisico.Count > 0)
                {
                    setDataImpresionAuditoria(tipoVista, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;
        }

        private void GenerarReporterptViewExamenFisicoRegional(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewExamenFisicoRegional.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewExamenFisicoRegional.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenFisicoRegionalEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenFisicoRegionalEdit>();
            listaRPT = getDatarptViewExamenFisicoRegional("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "REGIONAL");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenFisicoRegionalEdit>
        getDatarptViewExamenFisicoRegional(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenFisicoRegionalEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenFisicoRegionalEdit>();

            List<rptViewExamenFisicoRegional> rptViewExamenFisicoRegional = new List<rptViewExamenFisicoRegional>();
            SS_HC_ExamenFisico_Regional objExamenFisicoRegional = new SS_HC_ExamenFisico_Regional();
            objExamenFisicoRegional.UnidadReplicacion = unidadReplicacion;
            objExamenFisicoRegional.IdPaciente = idPaciente;
            objExamenFisicoRegional.EpisodioClinico = epiClinico;
            objExamenFisicoRegional.IdEpisodioAtencion = idEpiAtencion;
            objExamenFisicoRegional.Accion = "REPORTEA";
            rptViewExamenFisicoRegional = ServiceReportes.ReporteExamenFisicoRegional(objExamenFisicoRegional, 0, 0);

            objTabla1 = new System.Data.DataTable();
            SoluccionSalud.RepositoryReport.Entidades.rptViewExamenFisicoRegionalEdit objRPT;
            if (rptViewExamenFisicoRegional != null)
            {
                foreach (rptViewExamenFisicoRegional objReport in rptViewExamenFisicoRegional) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewExamenFisicoRegionalEdit();
                    objRPT.CuerpoHumanoDesc = objReport.CuerpoHumanoDesc;
                    objRPT.Comentarios = objReport.Comentarios;

                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    objRPT.Expr104 = objReport.Expr104;

                    listaRPT.Add(objRPT);


                }

                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewExamenFisicoRegional.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }


        private void GenerarReporterptViewEvolucionObjetiva(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewEvolucionObjetiva.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewEvolucionObjetiva.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionObjetivaEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionObjetivaEdit>();
            listaRPT = getDatarptViewEvolucionObjetiva("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "EVOLUCION");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionObjetivaEdit>

        getDatarptViewEvolucionObjetiva(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionObjetivaEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionObjetivaEdit>();

            List<rptViewEvolucionObjetiva> rptViewEvolucionObjetiva = new List<rptViewEvolucionObjetiva>();
            SS_HC_EvolucionObjetiva objEvolucionObjetiva = new SS_HC_EvolucionObjetiva();
            objEvolucionObjetiva.UnidadReplicacion = unidadReplicacion;
            objEvolucionObjetiva.IdPaciente = idPaciente;
            objEvolucionObjetiva.EpisodioClinico = epiClinico;
            objEvolucionObjetiva.IdEpisodioAtencion = idEpiAtencion;
            objEvolucionObjetiva.Accion = "REPORTEA";
            rptViewEvolucionObjetiva = ServiceReportes.ReporteEvolucionObjetiva(objEvolucionObjetiva, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionObjetivaEdit objRPT;
            if (rptViewEvolucionObjetiva != null)
            {
                foreach (rptViewEvolucionObjetiva objReport in rptViewEvolucionObjetiva) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionObjetivaEdit();

                    objRPT.FechaIngreso = Convert.ToDateTime(objReport.FechaIngreso);

                    objRPT.FechaAtencion = Convert.ToDateTime(objReport.FechaAtencion);

                    objRPT.EvolucionObjetiva = objReport.EvolucionObjetiva;

                    objRPT.Expr104 = objReport.Expr104;

                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewEvolucionObjetiva.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }





        private void GenerarReporterptViewEmitirDescansoMedico(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewEmitirDescansoMedico.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewEmitirDescansoMedico.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoEdit>();
            listaRPT = getDatarptViewEmitirDescansoMedico("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }











        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoEdit>

        getDatarptViewEmitirDescansoMedico(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoEdit>();

            List<rptViewEmitirDescansoMedico> rptViewEmitirDescansoMedico = new List<rptViewEmitirDescansoMedico>();
            SS_HC_DescansoMedico objEmitirDescansoMedico = new SS_HC_DescansoMedico();
            objEmitirDescansoMedico.UnidadReplicacion = unidadReplicacion;
            objEmitirDescansoMedico.IdPaciente = idPaciente;
            objEmitirDescansoMedico.EpisodioClinico = epiClinico;
            objEmitirDescansoMedico.IdEpisodioAtencion = idEpiAtencion;
            objEmitirDescansoMedico.Accion = "REPORTEA";
            rptViewEmitirDescansoMedico = ServiceReportes.ReporteEmitirDescansoMedico(objEmitirDescansoMedico, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoEdit objRPT;
            if (rptViewEmitirDescansoMedico != null)
            {
                foreach (rptViewEmitirDescansoMedico objReport in rptViewEmitirDescansoMedico) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoEdit();

                    objRPT.Observacion = objReport.Observacion;

                    objRPT.FechaInicioDescanso = Convert.ToDateTime(objReport.FechaInicioDescanso);

                    objRPT.FechaFinDescanso = Convert.ToDateTime(objReport.FechaFinDescanso);

                    objRPT.Dias = Convert.ToInt32(objReport.Dias);

                    objRPT.Expr104 = objReport.Expr104;

                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewEmitirDescansoMedico.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }


        private void GenerarReporterptViewProximaAtencion(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewProximaAtencion.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewProximaAtencion.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencionEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencionEdit>();
            listaRPT = getDatarptViewProximaAtencion("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));



            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PROXIMACITA");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencionEdit>

        getDatarptViewProximaAtencion(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencionEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencionEdit>();

            List<rptViewProximaAtencion> rptViewProximaAtencion = new List<rptViewProximaAtencion>();
            SS_HC_ProximaAtencion objProximaAtencion = new SS_HC_ProximaAtencion();
            objProximaAtencion.UnidadReplicacion = unidadReplicacion;
            objProximaAtencion.IdPaciente = idPaciente;
            objProximaAtencion.EpisodioClinico = epiClinico;
            objProximaAtencion.IdEpisodioAtencion = idEpiAtencion;
            objProximaAtencion.Accion = "REPORTEA";
            rptViewProximaAtencion = ServiceReportes.ReporteProximaAtencion(objProximaAtencion, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencionEdit objRPT;
            if (rptViewProximaAtencion != null)
            {
                foreach (rptViewProximaAtencion objReport in rptViewProximaAtencion) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencionEdit();



                    objRPT.FechaSolicitada = Convert.ToDateTime(objReport.FechaSolicitada);
                    objRPT.EspecialidadDesc = objReport.EspecialidadDesc;
                    objRPT.IdPersonalSalud = Convert.ToInt32(objReport.IdPersonalSalud);
                    objRPT.Observacion = objReport.Observacion;
                    objRPT.CMP = objReport.CMP;
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;

                    objRPT.Expr104 = objReport.Expr104;

                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewProximaAtencion.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;


        }



        private void GenerarReporterptViewCuidadosPreventivos(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewCuidadosPreventivos.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewCuidadosPreventivos.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewCuidadosPreventivoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewCuidadosPreventivoEdit>();
            listaRPT = getDatarptViewCuidadosPreventivo("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "CUIDADOPREVENTIVO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewCuidadosPreventivoEdit>

         getDatarptViewCuidadosPreventivo(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewCuidadosPreventivoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewCuidadosPreventivoEdit>();

            List<rptViewCuidadosPreventivo> rptViewCuidadosPreventivo = new List<rptViewCuidadosPreventivo>();
            SS_HC_SeguimientoRiesgo objCuidadosPreventivo = new SS_HC_SeguimientoRiesgo();
            objCuidadosPreventivo.UnidadReplicacion = unidadReplicacion;
            objCuidadosPreventivo.IdPaciente = idPaciente;
            objCuidadosPreventivo.EpisodioClinico = epiClinico;
            objCuidadosPreventivo.IdEpisodioAtencion = idEpiAtencion;
            objCuidadosPreventivo.Accion = "REPORTEA";
            rptViewCuidadosPreventivo = ServiceReportes.ReporteCuidadosPreventivo(objCuidadosPreventivo, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewCuidadosPreventivoEdit objRPT;
            if (rptViewCuidadosPreventivo != null)
            {
                foreach (rptViewCuidadosPreventivo objReport in rptViewCuidadosPreventivo) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewCuidadosPreventivoEdit();



                    objRPT.FechaAtencion = Convert.ToDateTime(objReport.FechaAtencion);
                    objRPT.FechaSeguimiento = Convert.ToDateTime(objReport.FechaSeguimiento);
                    objRPT.EspecialidadDesc = objReport.EspecialidadDesc;
                    objRPT.IdPersonalSalud = Convert.ToInt32(objReport.IdPersonalSalud);
                    objRPT.Descripcion = objReport.Descripcion;
                    objRPT.Nombre = objReport.Nombre;
                    objRPT.Comentario = objReport.Comentario;
                    objRPT.Nombre = objReport.Nombre;
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;
                    objRPT.IdCuidadoPreventivo = Convert.ToInt32(objReport.IdCuidadoPreventivo);
                    objRPT.Expr104 = objReport.Expr104;
                    objRPT.Secuencia = objReport.Secuencia;
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;

                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewCuidadosPreventivo.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;


        }

        private void GenerarReporterptViewSolicitarReferencia(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewSolicitarReferencia.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewSolicitarReferencia.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitarReferenciaEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitarReferenciaEdit>();
            listaRPT = getDatarptViewSolicitarReferencia("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "REFERENCIA");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }


        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitarReferenciaEdit>

        getDatarptViewSolicitarReferencia(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitarReferenciaEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitarReferenciaEdit>();

            List<rptViewSolicitarReferencia> rptViewSolicitarReferencia = new List<rptViewSolicitarReferencia>();
            SS_HC_ProximaAtencion objSolicitarReferencia = new SS_HC_ProximaAtencion();
            objSolicitarReferencia.UnidadReplicacion = unidadReplicacion;
            objSolicitarReferencia.IdPaciente = idPaciente;
            objSolicitarReferencia.EpisodioClinico = epiClinico;
            objSolicitarReferencia.IdEpisodioAtencion = idEpiAtencion;
            objSolicitarReferencia.Accion = "REPORTEA";
            rptViewSolicitarReferencia = ServiceReportes.ReporteSolicitarReferencia(objSolicitarReferencia, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitarReferenciaEdit objRPT;
            if (rptViewSolicitarReferencia != null)
            {
                foreach (rptViewSolicitarReferencia objReport in rptViewSolicitarReferencia) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitarReferenciaEdit();



                    objRPT.FechaSolicitada = Convert.ToDateTime(objReport.FechaSolicitada);
                    objRPT.EspecialidadDesc = objReport.EspecialidadDesc;
                    //  objRPT.IdPersonalSalud = Convert.ToInt32(objReport.IdPersonalSalud);
                    objRPT.Observacion = objReport.Observacion;
                    objRPT.EstablecimientoDesc = objReport.EstablecimientoDesc;
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;

                    objRPT.Expr104 = objReport.Expr104;

                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewSolicitarReferencia.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;


        }







        private void GenerarReporterptViewDiagnostico(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewDiagnostico.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewDiagnostico.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit>();

            listaRPT = getDatarptViewDiagnostico("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {

                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DIAGNOSTICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }








        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit>

        getDatarptViewDiagnostico(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit>();

            List<rptViewDiagnostico> rptViewDiagnostico = new List<rptViewDiagnostico>();
            SS_HC_Diagnostico objDiagnostico = new SS_HC_Diagnostico();
            objDiagnostico.UnidadReplicacion = unidadReplicacion;
            objDiagnostico.IdPaciente = idPaciente;
            objDiagnostico.EpisodioClinico = epiClinico;
            objDiagnostico.IdEpisodioAtencion = idEpiAtencion;
            objDiagnostico.Accion = "REPORTEA";
            rptViewDiagnostico = ServiceReportes.ReporteDiagnostico(objDiagnostico, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit objRPT;
            if (rptViewDiagnostico != null)
            {
                foreach (rptViewDiagnostico objReport in rptViewDiagnostico) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit();


                    objRPT.DiagnosticoDesc = objReport.DiagnosticoDesc;
                    objRPT.DeterminacionDiagnosticaDesc = objReport.DeterminacionDiagnosticaDesc;
                    objRPT.DiagnosticoPrincipalDesc = objReport.DiagnosticoPrincipalDesc;
                    objRPT.GradoAfeccion = objReport.GradoAfeccion;
                    objRPT.TipoAntecedenteDesc = objReport.TipoAntecedenteDesc;
                    objRPT.IndicadorPreExistenciaDesc = objReport.IndicadorPreExistenciaDesc;
                    objRPT.IndicadorCronicoDesc = objReport.IndicadorCronicoDesc;
                    objRPT.IndicadorNuevoDesc = objReport.IndicadorNuevoDesc;
                    objRPT.Observacion = objReport.Observacion;

                    //AUX
                    //objRPT.Expr101 = objReport.Expr101;
                    //GENERALES
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    //objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;                    
                    objRPT.Expr104 = objReport.Expr104;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewDiagnostico.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }

        private void GenerarReporterptViewExamenSolicitado(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewExamenSolicitado.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewExamenSolicitado.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenSolicitadoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenSolicitadoEdit>();
            listaRPT = getDatarptViewExamenSolicitado("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "EXAMEN");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenSolicitadoEdit>

        getDatarptViewExamenSolicitado(
            String tipoVista, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenSolicitadoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenSolicitadoEdit>();

            List<rptViewExamenSolicitado> rptViewExamenSolicitado = new List<rptViewExamenSolicitado>();
            SS_HC_ExamenSolicitado objExamenSolicitado = new SS_HC_ExamenSolicitado();
            objExamenSolicitado.UnidadReplicacion = unidadReplicacion;
            objExamenSolicitado.IdPaciente = idPaciente;
            objExamenSolicitado.EpisodioClinico = epiClinico;
            objExamenSolicitado.IdEpisodioAtencion = idEpiAtencion;
            objExamenSolicitado.Accion = "REPORTEA";
            rptViewExamenSolicitado = ServiceReportes.ReporteExamenSolicitado(objExamenSolicitado, 0, 0);

            objTabla1 = new System.Data.DataTable();
            SoluccionSalud.RepositoryReport.Entidades.rptViewExamenSolicitadoEdit objRPT;
            if (rptViewExamenSolicitado != null)
            {
                foreach (rptViewExamenSolicitado objReport in rptViewExamenSolicitado) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewExamenSolicitadoEdit();

                    objRPT.ProcedimientoMedicoDesc = objReport.ProcedimientoMedicoDesc;

                    objRPT.FechaSolitada = Convert.ToDateTime(objReport.FechaSolitada);

                    objRPT.CodigoComponente = objReport.CodigoComponente;

                    objRPT.IdTipoExamen = Convert.ToInt32(objReport.IdTipoExamen);

                    objRPT.Cantidad = Convert.ToInt32(objReport.Cantidad);

                    objRPT.Observacion = objReport.Observacion;

                    objRPT.NombreCompleto = objReport.NombreCompleto;

                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;

                    objRPT.CodigoOA = objReport.CodigoOA;

                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;

                    objRPT.Expr104 = objReport.Expr104;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewExamenSolicitado.Count > 0)
                {
                    setDataImpresionAuditoria(tipoVista, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }

        private void GenerarReporterptViewMedicamento(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewMedicamento.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewMedicamento.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentoEdit>();
            listaRPT = getDatarptViewMedicamento("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "MEDICAMENTOS");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentoEdit>

        getDatarptViewMedicamento(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentoEdit>();

            List<rptViewMedicamento> rptViewMedicamento = new List<rptViewMedicamento>();
            SS_HC_Medicamento objMedicamento = new SS_HC_Medicamento();
            objMedicamento.UnidadReplicacion = unidadReplicacion;
            objMedicamento.IdPaciente = idPaciente;
            objMedicamento.EpisodioClinico = epiClinico;
            objMedicamento.IdEpisodioAtencion = idEpiAtencion;
            objMedicamento.Accion = "REPORTEA";
            rptViewMedicamento = ServiceReportes.ReporteMedicamento(objMedicamento, 0, 0);

            objTabla1 = new System.Data.DataTable();
            SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentoEdit objRPT;
            if (rptViewMedicamento != null)
            {
                foreach (rptViewMedicamento objReport in rptViewMedicamento) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentoEdit();

                    objRPT.GrupoMed = objReport.GrupoMed;
                    objRPT.MED_DCI = objReport.MED_DCI;
                    objRPT.UnidMedDesc = objReport.UnidMedDesc;
                    objRPT.Dosis = Convert.ToDecimal(objReport.Dosis);
                    objRPT.ViaDesc = objReport.ViaDesc;
                    objRPT.Cantidad = Convert.ToInt32(objReport.Cantidad);
                    objRPT.Frecuencia = Convert.ToInt32(objReport.Frecuencia);
                    objRPT.DiasTratamiento = Convert.ToInt32(objReport.DiasTratamiento);
                    objRPT.TipoRegistroMedDesc = objReport.TipoRegistroMedDesc;
                    objRPT.IndicadorRecetaDesc = objReport.IndicadorRecetaDesc;
                    objRPT.IndicacionesDesc = objReport.IndicacionesDesc;

                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    objRPT.Expr104 = objReport.Expr104;

                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewMedicamento.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                ///////////////////////////////  
            }
            return listaRPT;
        }

        private void GenerarReporterptViewcasoprueba(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewcasoprueba.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewcasoprueba.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit>();
            listaRPT = getDatarptViewcasoprueba("REPORTEA");

            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PRUEBA");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit>
        getDatarptViewcasoprueba(String accion)
        {

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit>();

            List<rptViewDiagnostico> rptViewDiagnostico = new List<rptViewDiagnostico>();
            SS_HC_Diagnostico objDiagnostico = new SS_HC_Diagnostico();
            objDiagnostico.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
            objDiagnostico.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
            objDiagnostico.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
            objDiagnostico.IdEpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;
            objDiagnostico.Accion = "REPORTEA";
            rptViewDiagnostico = ServiceReportes.ReporteDiagnostico(objDiagnostico, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit objRPT;
            if (rptViewDiagnostico != null)
            {
                foreach (rptViewDiagnostico objReport in rptViewDiagnostico) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit();


                    objRPT.DiagnosticoDesc = objReport.DiagnosticoDesc;
                    objRPT.DeterminacionDiagnosticaDesc = objReport.DeterminacionDiagnosticaDesc;
                    objRPT.DiagnosticoPrincipalDesc = objReport.DiagnosticoPrincipalDesc;
                    objRPT.GradoAfeccion = objReport.GradoAfeccion;
                    objRPT.TipoAntecedenteDesc = objReport.TipoAntecedenteDesc;
                    objRPT.IndicadorPreExistenciaDesc = objReport.IndicadorPreExistenciaDesc;
                    objRPT.IndicadorCronicoDesc = objReport.IndicadorCronicoDesc;
                    objRPT.IndicadorNuevoDesc = objReport.IndicadorNuevoDesc;

                    //AUX
                    objRPT.Expr101 = objReport.Expr101;
                    //GENERALES
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    //objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;

                    objRPT.Expr102 = objReport.Expr102;
                    listaRPT.Add(objRPT);
                }
            }
            return listaRPT;
        }

        private SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit getObjetoReporteAnamnesisEA(rptViewAnamnesisEA objReport)
        {
            SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit objRPT
                = new SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit();


            objRPT.MotivoConsulta = objReport.MotivoConsulta;
            objRPT.Expr5 = objReport.Expr5;
            objRPT.Expr6 = objReport.Expr6;
            objRPT.TiempoEnfermedad = objReport.TiempoEnfermedad;
            objRPT.Descripcion = objReport.Descripcion;
            objRPT.RelatoCronologico = objReport.RelatoCronologico;
            objRPT.Exprapetito = objReport.Exprapetito;
            objRPT.Exprsed = objReport.Exprsed;
            objRPT.Exprorina = objReport.Exprorina;
            objRPT.Deposiciones = objReport.Deposiciones;
            objRPT.IdCIAP2 = Convert.ToInt32(objReport.IdCIAP2);
            objRPT.Sueno = objReport.Sueno;
            objRPT.PesoAnterior = Convert.ToDecimal(objReport.PesoAnterior);
            objRPT.Infancia = objReport.Infancia;
            objRPT.EvaluacionAlimentacionActual = objReport.EvaluacionAlimentacionActual;
            objRPT.NombreCompleto = objReport.NombreCompleto;
            objRPT.Sexo = objReport.Sexo;
            objRPT.edad = objReport.edad != null ? (int)objReport.edad : 0;
            objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
            objRPT.CodigoOA = objReport.CodigoOA;
            objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
            objRPT.ServicioExtra = objReport.ServicioExtra;








            return objRPT;
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
                string Visor = "P";

                //CCEP0102    

                switch (reportID)
                {
                    case "CCEP0003": GenerarReporterptViewAnamnesisEA(Visor);
                        break;
                    case "CCEP0005": GenerarReporterptViewAnamnesisAF(Visor);
                        break;
                    case "CCEP0102": GenerarReporterptViewExamenTriajeFisico(Visor);
                        break;
                    case "CCEP0104": GenerarReporterptViewExamenFisicoRegional(Visor);
                        break;
                    case "CCEP0253": GenerarReporterptViewDiagnostico(Visor);
                        break;
                    case "CCEP2010": GenerarReporterptViewEvolucionObjetiva(Visor);
                        break;
                    case "CCEP0306": GenerarReporterptViewExamenSolicitado(Visor);
                        break;

                    case "ReportExamenes": GenerarReporteReceta("");
                        break;

                }


            }
        }
        protected void GenerarRecetaPDF_Click(object sender, EventArgs e)
        {

        }
        private void GenerarReporteMasivo(String tipoVista)
        {
            /* ReportDocument cryRpt = new ReportDocument();
             cryRpt.Load("C:/MainReport.rpt");
             cryRpt.DataSourceConnections.Clear();
             cryRpt.SetDataSource(ds.Tables[0]);
             cryRpt.Subreports[0].DataSourceConnections.Clear();
             cryRpt.Subreports[0].SetDataSource(ds.Tables[0]);
             crystalReportViewer1.ReportSource = cryRpt;
             crystalReportViewer1.Refresh();
             string tura = Server.MapPath("rptReports/ViewAdjuntos.rpt");
             Rpt.Load(Server.MapPath("rptReports/ViewAdjuntos.rpt"));*/


        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit> GrupalReporterptViewAnamnesisEA(
            String tipoVista, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario

            )
        {

            List<rptViewAnamnesisEA> rptViewListaDB = new List<rptViewAnamnesisEA>();
            SS_HC_Anamnesis_EA objRptFiltro = new SS_HC_Anamnesis_EA();
            /*
            objRptFiltro.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
            objRptFiltro.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
            objRptFiltro.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
            objRptFiltro.IdEpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;
            */
            objRptFiltro.UnidadReplicacion = unidadReplicacion;
            objRptFiltro.IdPaciente = idPaciente;
            objRptFiltro.EpisodioClinico = epiClinico;
            objRptFiltro.IdEpisodioAtencion = idEpiAtencion;
            objRptFiltro.Accion = "REPORTEA";
            rptViewListaDB = ServiceReportes.ReporteAnamnesisEA(objRptFiltro, 0, 0);

            objTabla1 = new System.Data.DataTable();
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit>();
            SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit objRPT;
            if (rptViewListaDB != null)
            {


                foreach (rptViewAnamnesisEA objReport in rptViewListaDB) // Loop through List with foreach.
                {
                    objRPT = getObjetoReporteAnamnesisEA(objReport);
                    //objRPT.NombreCompleto = objReport.NombreCompleto;
                    //objRPT.Accion = "~/resources/images/logohce.png";
                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewListaDB.Count > 0)
                {
                    setDataImpresionAuditoria(tipoVista, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }
        /*****/
        private void GenerarReporterptViewAnamnesisAP(String tipoVista)
        {
            String nombreRpt = "Enfermedad_Actual";
            string tura = Server.MapPath("rptReports/rptViewAnamnesisAP.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewAnamnesisAP.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAPEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAPEdit>();

            listaRPT = getDatarptViewAnamnesisAP("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, nombreRpt);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAPEdit>
        getDatarptViewAnamnesisAP(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAPEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAPEdit>();

            List<rptViewAnamnesisAP> rptViewAnamnesisAP = new List<rptViewAnamnesisAP>();
            SS_HC_Anamnesis_AP objRptFiltro = new SS_HC_Anamnesis_AP();
            objRptFiltro.UnidadReplicacion = unidadReplicacion;
            objRptFiltro.IdPaciente = idPaciente;
            objRptFiltro.EpisodioClinico = epiClinico;
            objRptFiltro.IdEpisodioAtencion = idEpiAtencion;
            objRptFiltro.Accion = "REPORTEA";
            rptViewAnamnesisAP = ServiceReportes.ReporteAnamnesisAP(objRptFiltro, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAPEdit objRPT;
            if (rptViewAnamnesisAP != null)
            {
                foreach (rptViewAnamnesisAP objReport in rptViewAnamnesisAP) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAPEdit();
                    //ADD DATOS DEL REPORTE

                    objRPT.GrupoTipoDiagnostico = objReport.GrupoTipoDiagnostico;
                    objRPT.GrupoTipoDiagnosticoDesc = objReport.GrupoTipoDiagnosticoDesc;
                    objRPT.DiagnosticoDesc = objReport.DiagnosticoDesc;
                    objRPT.Alcohol = objReport.Alcohol;
                    objRPT.Tabaco = objReport.Tabaco;
                    objRPT.Drogas = objReport.Drogas;
                    objRPT.ActividadFisica = objReport.ActividadFisica;
                    objRPT.ConsumoVerduras = objReport.ConsumoVerduras;
                    objRPT.ConsumoFrutas = objReport.ConsumoFrutas;
                    objRPT.Medicamentos = objReport.Medicamentos;
                    objRPT.Alimentos = objReport.Alimentos;
                    objRPT.SustanciasEnElAmbiente = objReport.SustanciasEnElAmbiente;
                    objRPT.SustanciasContactoConPiel = objReport.SustanciasContactoConPiel;
                    objRPT.CrianzaAnimalesDomesticos = objReport.CrianzaAnimalesDomesticos;
                    objRPT.AguaPotable = objReport.AguaPotable;
                    objRPT.DisposicionExcretas = objReport.DisposicionExcretas;


                    //GENERALES

                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    objRPT.Expr104 = objReport.Expr104;
                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewAnamnesisAP.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }


        /**GENERAL::*/
        private void GenerarReporterptViewGeneralHCE(String tipoVista,
                String unidRep, String epiClinico,
                String epiAten, String paciente,
                String formatos, String usuarioActual, String nombreFileExp)
        {

            /***** TRATAMIENTOS DE PARÁMETROS ******/
            String unidadReplicacion = unidRep;
            int idPaciente = (paciente != null ? (paciente != "" ? Convert.ToInt32(paciente) : 0) : 0);
            int episodioClinico = (epiClinico != null ? (epiClinico != "" ? Convert.ToInt32(epiClinico) : 0) : 0); ;
            long idEpisodioAtencion = (epiAten != null ? (epiAten != "" ? Convert.ToInt64(epiAten) : 0) : 0); ; ;


            string ruta = Server.MapPath("rptReports/ViewAdjuntos.rpt");
            Rpt.Load(Server.MapPath("rptReports/ViewAdjuntos.rpt"));

            //string varPath = Server.MapPath("rptReports/ViewAdjuntosFE.rpt");
            //Rpt.Load(Server.MapPath("rptReports/ViewAdjuntosFE.rpt"));

            #region AgrupadorReporte

            /**LISTAR DATOS GENERALES DEL REPORTES EN 'rptViewAgrupador'*/
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit> ListrptViewAgrupador = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit>();
            SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad = new SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit();
            objEntidad.UnidadReplicacion = unidadReplicacion;
            objEntidad.IdPaciente = idPaciente;
            objEntidad.EpisodioClinico = episodioClinico;
            objEntidad.IdEpisodioAtencion = idEpisodioAtencion;
            Boolean existeDataHC = false;
            List<rptViewAgrupador> ListrptViewAgrupadorOrig = new List<rptViewAgrupador>();
            rptViewAgrupador objEntidadOrig = new rptViewAgrupador();
            objEntidadOrig.UnidadReplicacion = unidadReplicacion;
            objEntidadOrig.IdPaciente = idPaciente;
            objEntidadOrig.EpisodioClinico = episodioClinico;
            objEntidadOrig.EpisodioAtencion = idEpisodioAtencion;
            ListrptViewAgrupadorOrig = ServiceReportes.ReporteViewAgrupador(objEntidadOrig);
            if (ListrptViewAgrupadorOrig.Count > 0)
            {
                existeDataHC = true;
                objEntidad.NombreCompleto = ListrptViewAgrupadorOrig[0].NombreCompleto;
                objEntidad.TipoTrabajadorDesc = ListrptViewAgrupadorOrig[0].TipoTrabajadorDesc;
                objEntidad.ServicioExtra = ListrptViewAgrupadorOrig[0].ServicioExtra;
                objEntidad.Sexo = ListrptViewAgrupadorOrig[0].Sexo;
                objEntidad.CodigoOA = ListrptViewAgrupadorOrig[0].CodigoOA;
                objEntidad.edad = (ListrptViewAgrupadorOrig[0].edad != null ? Convert.ToInt32(ListrptViewAgrupadorOrig[0].edad) : 0);
                objEntidad.UnidadServicioDesc = ListrptViewAgrupadorOrig[0].UnidadServicioDesc;
            }

            /************LISTAR DATA DE CADA SUBREPORTE (DESCARTAR LISTADOS DE ACUERDO A PARAM de FORMATOS)***********************/
            string formatosRecibidos = null;
            formatosRecibidos = formatos;
            string FOMR_VACIO = "000";
            //formatos = FOMR_VACIO + "-";
            formatos = "";
            //PARA EL REGSITRO DE AUDITORÍA
            int idImpresionLog = setDataImpresionAuditoria("HEADER", 0, objEntidad, null, usuarioActual);



            #endregion

            // FORMULARIO EXTRAS
            #region FORMULARIOEXTRAS

            //LISTADO FORMFE_0012
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit> listaRPTrptViewDiagnostico_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0012))
            {
                listaRPTrptViewDiagnostico_FE = getDatarptViewDiagnostico_FE("MASIVO",
                     unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                     objEntidad, idImpresionLog, FORMFE_0012, usuarioActual);
                if (listaRPTrptViewDiagnostico_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0012, FOMR_VACIO);
                    formatos = formatos + FORMFE_0012 + "-";
                }
            }

            #endregion

            // FORMULARIO EXTRAS
            #region FORMULARIOEXTRAS_GETDATA

            //LISTADO FORMFE_0001   
            DataTable listaRPTInmunizacionNinio = new DataTable();
            if (formatosRecibidos.Contains("" + FORMFE_0001))
            {
                listaRPTInmunizacionNinio = rptVistas_FE("rptViewInmunizacionNinio_FE",
                    unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                    null, 0, FORMFE_0001, usuarioActual);
                if (listaRPTInmunizacionNinio.Rows.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0001, FOMR_VACIO);
                    formatos = formatos + FORMFE_0001 + "-";
                }
            }

            //LISTADO FORMFE_0002         
            DataTable listaRPTInmunizacionAdulto = new DataTable();
            if (formatosRecibidos.Contains("" + FORMFE_0002))
            {
                listaRPTInmunizacionAdulto = rptVistas_FE("rptViewInmunizacionAdulto_FE"
                    , unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion
                    , null, 0, FORMFE_0002, usuarioActual);
                if (listaRPTInmunizacionAdulto.Rows.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0002, FOMR_VACIO);
                    formatos = formatos + FORMFE_0002 + "-";
                }
            }

            //LISTADO FORMFE_0003        
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit> listaRPTrptViewAntPerFisiologico_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0003))
            {
                listaRPTrptViewAntPerFisiologico_FE = getDatarptViewAntecedenteFisiologico_FE("MASIVO",
                         unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                         objEntidad, idImpresionLog, FORMFE_0003, usuarioActual);
                if (listaRPTrptViewAntPerFisiologico_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0003, FOMR_VACIO);
                    formatos = formatos + FORMFE_0003 + "-";
                }
            }

            //LISTADO FORMFE_0004 
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit> listaRPTrptViewAntFisiologicoPediatrico_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0004))
            {
                listaRPTrptViewAntFisiologicoPediatrico_FE = getDatarptViewAntFisiologicoPediatrico_FE("MASIVO",
                    unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                    objEntidad, idImpresionLog, FORMFE_0004, usuarioActual);
                if (listaRPTrptViewAntFisiologicoPediatrico_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0004, FOMR_VACIO);
                    formatos = formatos + FORMFE_0004 + "-";
                }
            }

            //LISTADO FORM_0005
            DataTable listaRPTrptAntGenerales_FE = new DataTable();
            DataTable listaRPTrptAntGeneralesDetalle_FE = new DataTable();
            if (formatosRecibidos.Contains("" + FORMFE_0005CAB))
            {

                listaRPTrptAntGenerales_FE = rptVistas_FE("rptViewAntecedentesPersonalesPatologicosGenerales_FE",
                         unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion
                        , null, 0, FORMFE_0005CAB, usuarioActual);
                if (listaRPTrptAntGenerales_FE.Rows.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0005CAB, FOMR_VACIO);
                    formatos = formatos + FORMFE_0005CAB + "-";
                }

                listaRPTrptAntGeneralesDetalle_FE = rptVistas_FE("rptViewAntecedentesPersonalesPatologicosGenerales_FE",
                          unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion
                        , null, 0, FORMFE_0005DET, usuarioActual);
                if (listaRPTrptAntGeneralesDetalle_FE.Rows.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0005DET, FOMR_VACIO);
                    formatos = formatos + FORMFE_0005DET + "-";
                }
            }

            //LISTADO FORM_0006
            DataTable listarptViewAlergias_FE = new DataTable();
            if (formatosRecibidos.Contains("" + FORMFE_0006))
            {
                listarptViewAlergias_FE = rptVistas_FE("rptViewAlergias_FE",
                        unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion
                        , null, 0, FORMFE_0006, usuarioActual);
                if (listarptViewAlergias_FE.Rows.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0006, FOMR_VACIO);
                    formatos = formatos + FORMFE_0006 + "-";
                }
            }

            //LISTADO FORM_0007
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit> listarptAnt_Familiares = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0007))
            {
                listarptAnt_Familiares = getDatarptViewAnamnesis_ANTFAM_FE("MASIVO",
                      unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                     objEntidad, idImpresionLog, FORMFE_0007, usuarioActual);
                if (listarptAnt_Familiares.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0007, FOMR_VACIO);
                    formatos = formatos + FORMFE_0007 + "-";
                }
            }

            //LISTADO FORMFE_0008
            List<SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit> listarptView_ValoracionFuncionalAM_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0008))
            {
                listarptView_ValoracionFuncionalAM_FE = getDatarptViewValoracionFuncionalAM_FE("MASIVO",
                   unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                   objEntidad, idImpresionLog, FORMFE_0008, usuarioActual);
                if (listarptView_ValoracionFuncionalAM_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0008, FOMR_VACIO);
                    formatos = formatos + FORMFE_0008 + "-";
                }
            }

            //LISTADO FORMFE_0009
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit> listarptViewValoracionMentalAM_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0009))
            {
                listarptViewValoracionMentalAM_FE = getDatarptViewValoracionMentalAM_FE("MASIVO",
                   unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                   objEntidad, idImpresionLog, FORMFE_0009, usuarioActual);
                if (listarptViewValoracionMentalAM_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0009, FOMR_VACIO);
                    formatos = formatos + FORMFE_0009 + "-";
                }
            }

            //LISTADO FORMFE_0010
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit> listaRPTrptViewValoracionSocioFamAM_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0010))
            {
                listaRPTrptViewValoracionSocioFamAM_FE = getDatarptViewValoracionSocioFamAM_FE("MASIVO",
                   unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                   objEntidad, idImpresionLog, FORMFE_0010, usuarioActual);
                if (listaRPTrptViewValoracionSocioFamAM_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0010, FOMR_VACIO);
                    formatos = formatos + FORMFE_0010 + "-";
                }
            }

            //LISTADO FORMFE_0011
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit> listaRPTrptViewValoracionAM_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0011))
            {
                listaRPTrptViewValoracionAM_FE = getDatarptViewValoracionAM_FE("MASIVO",
                   unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                   objEntidad, idImpresionLog, FORMFE_0011, usuarioActual);
                if (listaRPTrptViewValoracionAM_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0011, FOMR_VACIO);
                    formatos = formatos + FORMFE_0011 + "-";
                }
            }

            //LISTADO FORMFE_0013
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit> listaRPTrptViewExamenApoyo_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0013))
            {
                listaRPTrptViewExamenApoyo_FE = getDatarptViewExamenApoyoDiagnostico_FE("MASIVO",
                   unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                   objEntidad, idImpresionLog, FORMFE_0013, usuarioActual);
                if (listaRPTrptViewExamenApoyo_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0013, FOMR_VACIO);
                    formatos = formatos + FORMFE_0013 + "-";
                }
            }

            //LISTADO FORMFE_0014
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit> listaRPTrptViewInterconsulta_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0014))
            {
                listaRPTrptViewInterconsulta_FE = getDatarptViewInterconsulta_FE("MASIVO",
                       unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       objEntidad, idImpresionLog, FORMFE_0014, usuarioActual);
                if (listaRPTrptViewInterconsulta_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0014, FOMR_VACIO);
                    formatos = formatos + FORMFE_0014 + "-";
                }
            }

            //LISTADO FORMFE_0015
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit> listaRPTrptViewProximaAtencion_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0015))
            {
                listaRPTrptViewProximaAtencion_FE = getDatarptViewProximaAtencion_FE("MASIVO",
                       unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       objEntidad, idImpresionLog, FORMFE_0015, usuarioActual);
                if (listaRPTrptViewProximaAtencion_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0015, FOMR_VACIO);
                    formatos = formatos + FORMFE_0015 + "-";
                }
            }


            //LISTADO FORMFE_0016
            DataTable listaRPTApoyoDiagnostico = new DataTable();
            if (formatosRecibidos.Contains("" + FORMFE_0016))
            {
                listaRPTApoyoDiagnostico = rptVistas_FE("rptViewApoyoDiagnostico_FE",
                       unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       null, idImpresionLog, FORMFE_0016, usuarioActual);

                if (listaRPTApoyoDiagnostico.Rows.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0016, FOMR_VACIO);
                    formatos = formatos + FORMFE_0016 + "-";
                }
            }

            //LISTADO FORMFE_0017

            //LISTADO FORMFE_0018
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit> listaRPTrptViewDieta1_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>();
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit> listaRPTrptViewDieta2_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0018DET1))
            {
                int a = 0;
                listaRPTrptViewDieta1_FE = getDatarptViewDieta_FE("MASIVO",
                   unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       null, idImpresionLog, FORMFE_0018DET1, usuarioActual, 2);

                if (listaRPTrptViewDieta1_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0018DET1, FOMR_VACIO);
                    a = 1;
                }

                listaRPTrptViewDieta2_FE = getDatarptViewDieta_FE("MASIVO",
                    unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       null, idImpresionLog, FORMFE_0018DET2, usuarioActual, 3);

                if (listaRPTrptViewDieta2_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0018DET2, FOMR_VACIO);
                    a = a + 1;
                }

                if (a > 0)
                {
                    formatos = formatos + FORMFE_0018DET1 + "-";
                }
            }

            //LISTADO FORMFE_0019
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit> listaRPTrptViewMedicamentos1_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>();
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit> listaRPTrptViewMedicamentos2_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0019DET1))
            {
                int a = 0;

                //Subreporte 1
                listaRPTrptViewMedicamentos1_FE = getDatarptViewMedicamentos_FE("MASIVO",
                       unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       objEntidad, idImpresionLog, FORMFE_0019DET1, usuarioActual, 1);

                if (listaRPTrptViewMedicamentos1_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0019DET1, FOMR_VACIO);
                    a = 1;
                }
                //Subreporte 2
                listaRPTrptViewMedicamentos2_FE = getDatarptViewMedicamentos_FE("MASIVO",
                       unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       objEntidad, idImpresionLog, FORMFE_0019DET2, usuarioActual, 4);


                if (listaRPTrptViewMedicamentos2_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0019DET2, FOMR_VACIO);
                    a = a + 1;
                }
                //Subreporte 3
                DataTable listaRPTPac_Med = new DataTable();
                listaRPTPac_Med = rptDatosPacienteMedico_FE("rptViewDatosPaciente_Medico",
                       unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       null, idImpresionLog, FORMFE_0019DET3, usuarioActual);

                if (listaRPTPac_Med.Rows.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0019DET3, FOMR_VACIO);
                    a = a + 1;
                }

                if (a > 0)
                {
                    formatos = formatos + FORMFE_0019DET1 + "-";
                }
            }

            //LISTADO FORMFE_0020
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit> listaRPTrptViewDescansoMedicoFE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0020))
            {
                listaRPTrptViewDescansoMedicoFE = getDatarptViewEmitirDescansoMedicoFE("MASIVO",
                       unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       objEntidad, idImpresionLog, FORMFE_0020, usuarioActual);
                if (listaRPTrptViewDescansoMedicoFE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0020, FOMR_VACIO);
                    formatos = formatos + FORMFE_0020 + "-";
                }
            }
            //LISTADO FORMFE_0021
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit> listaRPTrptViewEvolucionMedica_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit>();
            if (formatosRecibidos.Contains("" + FORMFE_0021))
            {
                listaRPTrptViewEvolucionMedica_FE = getDatarptViewEvolucionMedica_FE("MASIVO",
                                  unidadReplicacion, idPaciente, episodioClinico, idEpisodioAtencion,
                       objEntidad, idImpresionLog, FORMFE_0021, usuarioActual);
                if (listaRPTrptViewEvolucionMedica_FE.Count > 0)
                {
                    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                    formatosRecibidos = formatosRecibidos.Replace(FORMFE_0021, FOMR_VACIO);
                    formatos = formatos + FORMFE_0021 + "-";
                }
            }

            #endregion
            /**ADD DATOS GENERALES DEL REPORTES EN 'ListrptViewAgrupador'*/
            //OBS: AUX TipoEpisodio:  usado para la fórmula de mostrar o no un subreporte de acuerdo al FORMATO que contenga
            objEntidad.TipoEpisodio = formatos;

            ListrptViewAgrupador.Add(objEntidad);
            Rpt.DataSourceConnections.Clear();
            Rpt.SetDataSource(ListrptViewAgrupador);
            /********************************/

            int cantidadSubReport = Rpt.Subreports.Count;

            try
            {
                if (cantidadSubReport > 0)
                {
                    //ADD FORMFE_0012 (ok)
                    if (listaRPTrptViewDiagnostico_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewDiagnostico_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewDiagnostico_FEsubrep.rpt"].SetDataSource(listaRPTrptViewDiagnostico_FE);
                    }

                    #region FORMULARIOEXTRAS

                    //ADD FORMFE_0001 (ok)
                    if (listaRPTInmunizacionNinio.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewInmunizacionNinio_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewInmunizacionNinio_FEsubrep.rpt"].SetDataSource(listaRPTInmunizacionNinio);
                    }

                    //ADD FORMFE_0002
                    if (listaRPTInmunizacionAdulto.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewInmunizacionAdultoRep_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewInmunizacionAdultoRep_FEsubrep.rpt"].SetDataSource(listaRPTInmunizacionAdulto);
                    }

                    //ADD FORMFE_0003
                    if (listaRPTrptViewAntPerFisiologico_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewAntecedentesPersonalesFisiologicos_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAntecedentesPersonalesFisiologicos_FEsubrep.rpt"].SetDataSource(listaRPTrptViewAntPerFisiologico_FE);
                    }

                    //ADD FORMFE_0004
                    if (listaRPTrptViewAntFisiologicoPediatrico_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewAntFisiologicoPediatricoFEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAntFisiologicoPediatricoFEsubrep.rpt"].SetDataSource(listaRPTrptViewAntFisiologicoPediatrico_FE);
                    }

                    //ADD FORMFE_0005
                    if (listaRPTrptAntGenerales_FE.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewAntecedentesPersonalesPatologicosGenerales_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAntecedentesPersonalesPatologicosGenerales_FEsubrep.rpt"].SetDataSource(listaRPTrptAntGenerales_FE);
                    }
                    if (listaRPTrptAntGeneralesDetalle_FE.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewAntecedentesPatologicosGeneralesdetalle.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAntecedentesPatologicosGeneralesdetalle.rpt"].SetDataSource(listaRPTrptAntGeneralesDetalle_FE);
                    }


                    //ADD FORMFE_0006 (ok)
                    if (listarptViewAlergias_FE.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewAlergia_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAlergia_FEsubrep.rpt"].SetDataSource(listarptViewAlergias_FE);
                    }

                    //ADD FORMFE_0007
                    if (listarptAnt_Familiares.Count > 0)
                    {
                        Rpt.Subreports["rptViewAnamnesis_ANTFAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAnamnesis_ANTFAM_FEsubrep.rpt"].SetDataSource(listarptAnt_Familiares);
                    }

                    //ADD FORMFE_0008
                    if (listarptView_ValoracionFuncionalAM_FE.Count > 0)
                    {
                        Rpt.Subreports["rptView_ValoracionFuncionalAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptView_ValoracionFuncionalAM_FEsubrep.rpt"].SetDataSource(listarptView_ValoracionFuncionalAM_FE);
                    }

                    //ADD FORMFE_0009
                    if (listarptViewValoracionMentalAM_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewValoracionMentalAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewValoracionMentalAM_FEsubrep.rpt"].SetDataSource(listarptViewValoracionMentalAM_FE);
                    }

                    //ADD FORMFE_0010
                    if (listaRPTrptViewValoracionSocioFamAM_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewValoracionSocioFamAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewValoracionSocioFamAM_FEsubrep.rpt"].SetDataSource(listaRPTrptViewValoracionSocioFamAM_FE);
                    }


                    //ADD FORMFE_0011
                    if (listaRPTrptViewValoracionAM_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewValoracionAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewValoracionAM_FEsubrep.rpt"].SetDataSource(listaRPTrptViewValoracionAM_FE);
                    }

                    //ADD FORMFE_0013
                    if (listaRPTrptViewExamenApoyo_FE.Count > 0)
                    {
                        Rpt.Subreports["ptViewExamenApoyo_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["ptViewExamenApoyo_FEsubrep.rpt"].SetDataSource(listaRPTrptViewExamenApoyo_FE);
                    }


                    //ADD FORMFE_0014  
                    if (listaRPTrptViewInterconsulta_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewInterconsulta_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewInterconsulta_FEsubrep.rpt"].SetDataSource(listaRPTrptViewInterconsulta_FE);
                    }

                    //ADD FORMFE_0015       
                    if (listaRPTrptViewProximaAtencion_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewProximaAtencion_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewProximaAtencion_FEsubrep.rpt"].SetDataSource(listaRPTrptViewProximaAtencion_FE);
                    }

                    //ADD FORMFE_0016             
                    if (listaRPTApoyoDiagnostico.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewApoyoDiagnosticoRep_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewApoyoDiagnosticoRep_FEsubrep.rpt"].SetDataSource(listaRPTApoyoDiagnostico);
                    }
                    //ADD FORMFE_0017

                    //ADD FORMFE_0018    
                    if (listaRPTrptViewDieta1_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewDieta_FEDetalle1.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewDieta_FEDetalle1.rpt"].SetDataSource(listaRPTrptViewDieta1_FE);
                    }
                    if (listaRPTrptViewDieta2_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewDieta_FEDetalle2.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewDieta_FEDetalle2.rpt"].SetDataSource(listaRPTrptViewDieta2_FE);
                    }

                    //ADD FORMFE_0019  (ok)    
                    if (listaRPTrptViewMedicamentos1_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewMedicamentos_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewMedicamentos_FEsubrep.rpt"].SetDataSource(listaRPTrptViewMedicamentos1_FE);
                    }
                    if (listaRPTrptViewMedicamentos2_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewMedicamentos_FEsubrep2.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewMedicamentos_FEsubrep2.rpt"].SetDataSource(listaRPTrptViewMedicamentos2_FE);
                    }

                    //ADD FORMFE_0020     (ok)
                    if (listaRPTrptViewDescansoMedicoFE.Count > 0)
                    {
                        Rpt.Subreports["rptViewEmitirDescansoMedicoFEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewEmitirDescansoMedicoFEsubrep.rpt"].SetDataSource(listaRPTrptViewDescansoMedicoFE);
                    }
                    //ADD FORMFE_0021  
                    if (listaRPTrptViewEvolucionMedica_FE.Count > 0)
                    {
                        try
                        {
                            Rpt.Subreports["rptViewEvolucionMedica_FEsubrep.rpt"].DataSourceConnections.Clear();
                            Rpt.Subreports["rptViewEvolucionMedica_FEsubrep.rpt"].SetDataSource(listaRPTrptViewEvolucionMedica_FE);

                        }
                        catch (Exception)
                        {
                            Response.Write("<script language=javascript>alert('No se encuentra el subreporte rptViewEvolucionMedica_FEsubrep');</script>");
                            //throw;
                        }
                    }

                    #endregion

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            /**ADD PARÁMETROS*/
            #region FORMULARIOINICALES_SETPARAMETER
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            imgFirma = Server.MapPath((string)Session["FIRMA_DIGITAL"]);
            Rpt.SetParameterValue("imgFirma", imgFirma);

            Rpt.SetParameterValue("FORM_0000", FORM_0000);
            Rpt.SetParameterValue("FORM_0001", FORM_0001);
            Rpt.SetParameterValue("FORM_0002", FORM_0002);
            Rpt.SetParameterValue("FORM_0003", FORM_0003);
            Rpt.SetParameterValue("FORM_0004", FORM_0004);
            Rpt.SetParameterValue("FORM_0005", FORM_0005);
            Rpt.SetParameterValue("FORM_0006", FORM_0006);
            Rpt.SetParameterValue("FORM_0007", FORM_0007);
            Rpt.SetParameterValue("FORM_0008", FORM_0008);
            Rpt.SetParameterValue("FORM_0009", FORM_0009);
            Rpt.SetParameterValue("FORM_0010", FORM_0010);
            Rpt.SetParameterValue("FORM_0011", FORM_0011);
            Rpt.SetParameterValue("FORM_0012", FORM_0012);
            #endregion

            #region FORMULARIOEXTRAS_SETPARAMETER

            Rpt.SetParameterValue("FORMFE_0001", FORMFE_0001);
            Rpt.SetParameterValue("FORMFE_0002", FORMFE_0002);
            Rpt.SetParameterValue("FORMFE_0003", FORMFE_0003);
            Rpt.SetParameterValue("FORMFE_0004", FORMFE_0004);
            Rpt.SetParameterValue("FORMFE_0005CAB", FORMFE_0005CAB);
            Rpt.SetParameterValue("FORMFE_0005DET", FORMFE_0005DET);
            Rpt.SetParameterValue("FORMFE_0006", FORMFE_0006);
            Rpt.SetParameterValue("FORMFE_0007", FORMFE_0007);
            Rpt.SetParameterValue("FORMFE_0008", FORMFE_0008);
            Rpt.SetParameterValue("FORMFE_0009", FORMFE_0009);
            Rpt.SetParameterValue("FORMFE_0010", FORMFE_0010);
            Rpt.SetParameterValue("FORMFE_0011", FORMFE_0011);
            Rpt.SetParameterValue("FORMFE_0012", FORMFE_0012);
            Rpt.SetParameterValue("FORMFE_0013", FORMFE_0013);
            Rpt.SetParameterValue("FORMFE_0014", FORMFE_0014);
            Rpt.SetParameterValue("FORMFE_0015", FORMFE_0015);
            Rpt.SetParameterValue("FORMFE_0016", FORMFE_0016);
            Rpt.SetParameterValue("FORMFE_0017", FORMFE_0017);
            Rpt.SetParameterValue("FORMFE_0018DET1", FORMFE_0018DET1);
            Rpt.SetParameterValue("FORMFE_0018DET2", FORMFE_0018DET2);
            Rpt.SetParameterValue("FORMFE_0019", FORMFE_0019);
            Rpt.SetParameterValue("FORMFE_0019DET1", FORMFE_0019DET1);
            Rpt.SetParameterValue("FORMFE_0019DET2", FORMFE_0019DET2);
            Rpt.SetParameterValue("FORMFE_0019DET3", FORMFE_0019DET3);
            Rpt.SetParameterValue("FORMFE_0020", FORMFE_0020);
            Rpt.SetParameterValue("FORMFE_0021", FORMFE_0021);

            #endregion

            #region FORMULARIOFED_SETPARAMETER
            Rpt.SetParameterValue("FORMFE_0038", FORMFE_0038);
            Rpt.SetParameterValue("FORMFE_0039", FORMFE_0039);
            Rpt.SetParameterValue("FORMFE_0040", FORMFE_0040);
            Rpt.SetParameterValue("FORMFE_0041", FORMFE_0041);
            Rpt.SetParameterValue("FORMFE_0042", FORMFE_0042);
            Rpt.SetParameterValue("FORMFE_0043", FORMFE_0043);
            Rpt.SetParameterValue("FORMFE_0044", FORMFE_0044);
            Rpt.SetParameterValue("FORMFE_0045", FORMFE_0045);
            #endregion

            /******************/

            if (!existeDataHC)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.SetParameterValue("imgFirma", imgFirma);

                        Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "EXAMEN");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                    Rpt.SetParameterValue("imgFirma", imgFirma);

                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetParameterValue("imgFirma", imgFirma);


        }

        private void DatosGenerales()
        {

            /**LISTAR DATOS GENERALES DEL REPORTES EN 'rptViewAgrupador'*/
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit> ListrptViewAgrupador = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit>();
            SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad = new SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit();
            objEntidad.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
            objEntidad.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
            objEntidad.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
            objEntidad.IdEpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;
            Boolean existeDataHC = false;
            List<rptViewAgrupador> ListrptViewAgrupadorOrig = new List<rptViewAgrupador>();
            rptViewAgrupador objEntidadOrig = new rptViewAgrupador();
            objEntidadOrig.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
            objEntidadOrig.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
            objEntidadOrig.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
            objEntidadOrig.IdEpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;
            ListrptViewAgrupadorOrig = ServiceReportes.ReporteViewAgrupador(objEntidadOrig);
            if (ListrptViewAgrupadorOrig.Count > 0)
            {
                existeDataHC = true;
                objEntidad.NombreCompleto = ListrptViewAgrupadorOrig[0].NombreCompleto;
                objEntidad.TipoTrabajadorDesc = ListrptViewAgrupadorOrig[0].TipoTrabajadorDesc;
                objEntidad.ServicioExtra = ListrptViewAgrupadorOrig[0].ServicioExtra;
                objEntidad.Sexo = ListrptViewAgrupadorOrig[0].Sexo;
                objEntidad.CodigoOA = ListrptViewAgrupadorOrig[0].CodigoOA;
                objEntidad.edad = (ListrptViewAgrupadorOrig[0].edad != null ? Convert.ToInt32(ListrptViewAgrupadorOrig[0].edad) : 0);
                objEntidad.UnidadServicioDesc = ListrptViewAgrupadorOrig[0].UnidadServicioDesc;
            }
        }





        private void DatosGenerales1()
        {

            /**LISTAR DATOS GENERALES DEL REPORTES EN 'rptViewAgrupador'*/
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit> ListrptViewAgrupador = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit>();
            SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad = new SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit();
            objEntidad.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
            objEntidad.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
            objEntidad.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
            objEntidad.IdEpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;
            Boolean existeDataHC = false;
            List<rptViewAgrupador> ListrptViewAgrupadorOrig = new List<rptViewAgrupador>();
            rptViewAgrupador objEntidadOrig = new rptViewAgrupador();
            objEntidadOrig.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
            objEntidadOrig.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
            objEntidadOrig.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
            objEntidadOrig.IdEpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;
            ListrptViewAgrupadorOrig = ServiceReportes.ReporteViewAgrupador(objEntidadOrig);
            if (ListrptViewAgrupadorOrig.Count > 0)
            {
                existeDataHC = true;
                objEntidad.NombreCompleto = ListrptViewAgrupadorOrig[0].NombreCompleto;
                objEntidad.TipoTrabajadorDesc = ListrptViewAgrupadorOrig[0].TipoTrabajadorDesc;
                objEntidad.ServicioExtra = ListrptViewAgrupadorOrig[0].ServicioExtra;
                objEntidad.Sexo = ListrptViewAgrupadorOrig[0].Sexo;
                objEntidad.CodigoOA = ListrptViewAgrupadorOrig[0].CodigoOA;
                objEntidad.edad = (ListrptViewAgrupadorOrig[0].edad != null ? Convert.ToInt32(ListrptViewAgrupadorOrig[0].edad) : 0);
                objEntidad.UnidadServicioDesc = ListrptViewAgrupadorOrig[0].UnidadServicioDesc;
            }


            if (ListrptViewAgrupadorOrig.Count > 0)
            {
                Rpt.Subreports["DatosGeneralesFE.rpt"].DataSourceConnections.Clear();
                Rpt.Subreports["DatosGeneralesFE.rpt"].SetDataSource(ListrptViewAgrupadorOrig);
            }


        }




        /*******************************************************   TOTAL HCE ******************************************************************/
        private void GenerarReporterptViewTotalHCE(String tipoVista)
        {
            string ruta = Server.MapPath("rptReports/ViewAdjuntos.rpt");
            Rpt.Load(Server.MapPath("rptReports/ViewAdjuntos.rpt"));

            //string varPath = Server.MapPath("rptReports/ViewAdjuntosFE.rpt");
            //Rpt.Load(Server.MapPath("rptReports/ViewAdjuntosFE.rpt"));

            #region AgrupadorReporte

            /**LISTAR DATOS GENERALES DEL REPORTES EN 'rptViewAgrupador'*/
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit> ListrptViewAgrupador = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit>();
            SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad = new SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit();
            objEntidad.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
            objEntidad.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
            objEntidad.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
            objEntidad.IdEpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;
            Boolean existeDataHC = false;
            List<rptViewAgrupador> ListrptViewAgrupadorOrig = new List<rptViewAgrupador>();
            rptViewAgrupador objEntidadOrig = new rptViewAgrupador();
            objEntidadOrig.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
            objEntidadOrig.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
            objEntidadOrig.EpisodioClinico = (int)ENTITY_GLOBAL.Instance.EpisodioClinico;
            /* objEntidadOrig.IdEpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;*/
            objEntidadOrig.EpisodioAtencion = (long)ENTITY_GLOBAL.Instance.EpisodioAtencion;
            ListrptViewAgrupadorOrig = ServiceReportes.ReporteViewAgrupador(objEntidadOrig);
            if (ListrptViewAgrupadorOrig.Count > 0)
            {
                existeDataHC = true;
                objEntidad.NombreCompleto = ListrptViewAgrupadorOrig[0].NombreCompleto;
                objEntidad.TipoTrabajadorDesc = ListrptViewAgrupadorOrig[0].TipoTrabajadorDesc;
                objEntidad.ServicioExtra = ListrptViewAgrupadorOrig[0].ServicioExtra;
                objEntidad.Sexo = ListrptViewAgrupadorOrig[0].Sexo;
                objEntidad.CodigoOA = ListrptViewAgrupadorOrig[0].CodigoOA;
                objEntidad.edad = (ListrptViewAgrupadorOrig[0].edad != null ? Convert.ToInt32(ListrptViewAgrupadorOrig[0].edad) : 0);
                objEntidad.UnidadServicioDesc = ListrptViewAgrupadorOrig[0].UnidadServicioDesc;
            }

            /************LISTAR DATA DE CADA SUBREPORTE (DESCARTAR LISTADOS DE ACUERDO A PARAM de FORMATOS)***********************/
            string FOMR_VACIO = "000";
            string formatos = FOMR_VACIO + "-";
            //PARA EL REGSITRO DE AUDITORÍA
            int idImpresionLog = setDataImpresionAuditoria("HEADER", 0, objEntidad, null, ENTITY_GLOBAL.Instance.USUARIO);

            //LISTADO FORM_0000
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit> listaRPTrptViewAnamnesisEAEdit = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisEAEdit>();
            listaRPTrptViewAnamnesisEAEdit = GrupalReporterptViewAnamnesisEA("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0000, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewAnamnesisEAEdit.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0000 + "-";
            }

            #endregion

            // FORMULARIO INICIALES
            #region FORMULARIOINICIALES_GETDATA

            //LISTADO FORM_0001
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenSolicitadoEdit> listaRPTrptViewExamenSolicitadoEdit = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenSolicitadoEdit>();
            listaRPTrptViewExamenSolicitadoEdit = getDatarptViewExamenSolicitado("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0001, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewExamenSolicitadoEdit.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0001 + "-";
            }

            //LISTADO FORM_0002
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenTriajeFisicoEdit> listaRPTrptViewExamenTriajeEdit = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenTriajeFisicoEdit>();
            listaRPTrptViewExamenTriajeEdit = getDatarptViewExamenTriajeFisico("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0002, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewExamenTriajeEdit.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0002 + "-";
            }

            //LISTADO FORM_0003
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentoEdit> listaRPTrptViewMedicamentoEdit = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentoEdit>();
            listaRPTrptViewMedicamentoEdit = getDatarptViewMedicamento("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0003, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewMedicamentoEdit.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0003 + "-";
            }

            //LISTADO FORM_0004
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenFisicoRegionalEdit> listaRPTrptViewExamenRegionalEdit = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenFisicoRegionalEdit>();
            listaRPTrptViewExamenRegionalEdit = getDatarptViewExamenFisicoRegional("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0004, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewExamenRegionalEdit.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0004 + "-";
            }

            //LISTADO FORM_0005
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionObjetivaEdit> listaRPTrptViewEvolucionObjetivaEdit = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionObjetivaEdit>();
            listaRPTrptViewEvolucionObjetivaEdit = getDatarptViewEvolucionObjetiva("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0005, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewEvolucionObjetivaEdit.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0005 + "-";
            }

            //LISTADO FORM_0006
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAFEdit> listaRPTrptViewAnamnesisAFEdit = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAFEdit>();
            listaRPTrptViewAnamnesisAFEdit = getDatarptViewAnamnesisAF("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0006, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewAnamnesisAFEdit.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0006 + "-";
            }

            //LISTADO FORM_0007
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit> listaRPTrptViewDiagnosticoEdit = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnosticoEdit>();
            listaRPTrptViewDiagnosticoEdit = getDatarptViewDiagnostico("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0007, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewDiagnosticoEdit.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0007 + "-";
            }

            //LISTADO FORM_0008
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAPEdit> listaRPTrptViewAnamnesisAPEdit = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesisAPEdit>();
            listaRPTrptViewAnamnesisAPEdit = getDatarptViewAnamnesisAP("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0008, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewAnamnesisAPEdit.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0008 + "-";
            }

            //LISTADO FORM_0009
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoEdit> listaRPTrptViewEmitirDescansoMedico = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoEdit>();
            listaRPTrptViewEmitirDescansoMedico = getDatarptViewEmitirDescansoMedico("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0009, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewEmitirDescansoMedico.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0009 + "-";
            }

            //LISTADO FORM_0010
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencionEdit> listaRPTrptViewProximaAtencion = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencionEdit>();
            listaRPTrptViewProximaAtencion = getDatarptViewProximaAtencion("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0010, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewProximaAtencion.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0010 + "-";
            }

            //LISTADO FORM_0011
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitarReferenciaEdit> listaRPTrptViewSolicitarReferencia = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitarReferenciaEdit>();
            listaRPTrptViewSolicitarReferencia = getDatarptViewSolicitarReferencia("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0011, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewSolicitarReferencia.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0011 + "-";
            }


            //LISTADO FORM_0012
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewCuidadosPreventivoEdit> listaRPTrptViewCuidadosPreventivo = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewCuidadosPreventivoEdit>();
            listaRPTrptViewCuidadosPreventivo = getDatarptViewCuidadosPreventivo("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORM_0012, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewCuidadosPreventivo.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORM_0012 + "-";
            }

            #endregion

            // FORMULARIO EXTRAS
            #region FORMULARIOEXTRAS_GETDATA

            //LISTADO FORMFE_0001




            DataTable listarptAgrupador_FE = new DataTable();


            listarptAgrupador_FE = rptAgrupador_FE("rptViewAgrupador", ENTITY_GLOBAL.Instance.UnidadReplicacion,
                         (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                         (long)ENTITY_GLOBAL.Instance.EpisodioAtencion,
                         null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);






            DataTable listaRPTInmunizacionNinio = new DataTable();
            listaRPTInmunizacionNinio = rptVistas_FE("rptViewInmunizacionNinio_FE"
                        , ENTITY_GLOBAL.Instance.UnidadReplicacion
                        , (int)ENTITY_GLOBAL.Instance.PacienteID
                        , (int)ENTITY_GLOBAL.Instance.EpisodioClinico
                        , (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                        , null
                        , 0
                        , ENTITY_GLOBAL.Instance.CONCEPTO
                        , ENTITY_GLOBAL.Instance.USUARIO);

            if (listaRPTInmunizacionNinio.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0001 + "-";
            }


            //LISTADO FORMFE_0002         
            DataTable listaRPTInmunizacionAdulto = new DataTable();
            listaRPTInmunizacionAdulto = rptVistas_FE("rptViewInmunizacionAdulto_FE"
                        , ENTITY_GLOBAL.Instance.UnidadReplicacion
                        , (int)ENTITY_GLOBAL.Instance.PacienteID
                        , (int)ENTITY_GLOBAL.Instance.EpisodioClinico
                        , (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                        , null
                        , 0
                        , ENTITY_GLOBAL.Instance.CONCEPTO
                        , ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTInmunizacionAdulto.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0002 + "-";
            }

            //LISTADO FORMFE_0003        
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit> listaRPTrptViewAntPerFisiologico_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit>();
            listaRPTrptViewAntPerFisiologico_FE = getDatarptViewAntecedenteFisiologico_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0003, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewAntPerFisiologico_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0003 + "-";
            }

            //LISTADO FORMFE_0004 
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit> listaRPTrptViewAntFisiologicoPediatrico_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit>();
            listaRPTrptViewAntFisiologicoPediatrico_FE = getDatarptViewAntFisiologicoPediatrico_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0004, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewAntFisiologicoPediatrico_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0004 + "-";
            }

            //LISTADO FORM_0005
            DataTable listaRPTrptAntGenerales_FE = new DataTable();
            DataTable listaRPTrptAntGeneralesDetalle_FE = new DataTable();
            listaRPTrptAntGenerales_FE = rptVistas_FE("rptViewAntecedentesPersonalesPatologicosGenerales_FE",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptAntGenerales_FE.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0005CAB + "-";
            }


            listaRPTrptAntGeneralesDetalle_FE = rptVistas_FE("rptViewAntecedentesPersonalesPatologicosGenerales_FE",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptAntGeneralesDetalle_FE.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0005DET + "-";
            }

            //LISTADO FORM_0006

            DataTable listarptViewAlergias_FE = new DataTable();
            listarptViewAlergias_FE = rptVistas_FE("rptViewAlergias_FE",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO);
            if (listarptViewAlergias_FE.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0006 + "-";
            }

            //List<SoluccionSalud.RepositoryReport.Entidades.rptViewAlergias_FEEdit> listarptViewAlergias_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAlergias_FEEdit>();
            //listarptViewAlergias_FE = getDatarptViewAlergias_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
            //    , objEntidad, idImpresionLog, FORMFE_0006, ENTITY_GLOBAL.Instance.USUARIO);
            //if (listarptViewAlergias_FE.Count > 0)
            //{
            //    //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
            //    formatos = formatos + FORMFE_0006 + "-";
            //}

            //LISTADO FORM_0007
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit> listarptAnt_Familiares = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit>();
            listarptAnt_Familiares = getDatarptViewAnamnesis_ANTFAM_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0007, ENTITY_GLOBAL.Instance.USUARIO);
            if (listarptAnt_Familiares.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0007 + "-";
            }

            //LISTADO FORMFE_0008
            List<SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit> listarptView_ValoracionFuncionalAM_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit>();
            listarptView_ValoracionFuncionalAM_FE = getDatarptViewValoracionFuncionalAM_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0008, ENTITY_GLOBAL.Instance.USUARIO);
            if (listarptView_ValoracionFuncionalAM_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0008 + "-";
            }


            //LISTADO FORMFE_0009
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit> listarptViewValoracionMentalAM_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit>();
            listarptViewValoracionMentalAM_FE = getDatarptViewValoracionMentalAM_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0009, ENTITY_GLOBAL.Instance.USUARIO);
            if (listarptViewValoracionMentalAM_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0009 + "-";
            }

            //LISTADO FORMFE_0010
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit> listaRPTrptViewValoracionSocioFamAM_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit>();
            listaRPTrptViewValoracionSocioFamAM_FE = getDatarptViewValoracionSocioFamAM_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0010, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewValoracionSocioFamAM_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0010 + "-";
            }

            //LISTADO FORMFE_0011
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit> listaRPTrptViewValoracionAM_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit>();
            listaRPTrptViewValoracionAM_FE = getDatarptViewValoracionAM_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0011, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewValoracionAM_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0011 + "-";
            }

            //LISTADO FORMFE_0012
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit> listaRPTrptViewDiagnostico_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit>();
            listaRPTrptViewDiagnostico_FE = getDatarptViewDiagnostico_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0012, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewDiagnostico_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0012 + "-";
            }

            //LISTADO FORMFE_0013
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit> listaRPTrptViewExamenApoyo_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit>();
            listaRPTrptViewExamenApoyo_FE = getDatarptViewExamenApoyoDiagnostico_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0013, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewExamenApoyo_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0013 + "-";
            }

            //LISTADO FORMFE_0014
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit> listaRPTrptViewInterconsulta_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit>();
            listaRPTrptViewInterconsulta_FE = getDatarptViewInterconsulta_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0014, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewInterconsulta_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0014 + "-";
            }

            //LISTADO FORMFE_0015
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit> listaRPTrptViewProximaAtencion_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit>();
            listaRPTrptViewProximaAtencion_FE = getDatarptViewProximaAtencion_FE("MASIVO", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , objEntidad, idImpresionLog, FORMFE_0015, ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewProximaAtencion_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0015 + "-";
            }


            //LISTADO FORMFE_0016
            DataTable listaRPTApoyoDiagnostico = new DataTable();
            listaRPTApoyoDiagnostico = rptVistas_FE("rptViewApoyoDiagnostico_FE",
                        ENTITY_GLOBAL.Instance.UnidadReplicacion,
                        (int)ENTITY_GLOBAL.Instance.PacienteID,
                        (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                        (long)ENTITY_GLOBAL.Instance.EpisodioAtencion,
                        null, 0,
                        ENTITY_GLOBAL.Instance.CONCEPTO,
                        ENTITY_GLOBAL.Instance.USUARIO);

            if (listaRPTApoyoDiagnostico.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0016 + "-";
            }

            //LISTADO FORMFE_0017

            //LISTADO FORMFE_0018
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit> listaRPTrptViewDieta1_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>();
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit> listaRPTrptViewDieta2_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>();
            listaRPTrptViewDieta1_FE = getDatarptViewDieta_FE("MASIVO",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, idImpresionLog, FORMFE_0018DET1,
                ENTITY_GLOBAL.Instance.USUARIO, 2);
            if (listaRPTrptViewDieta1_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0018DET1 + "-";
            }

            listaRPTrptViewDieta2_FE = getDatarptViewDieta_FE("MASIVO",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, idImpresionLog, FORMFE_0018DET2,
                ENTITY_GLOBAL.Instance.USUARIO, 3);
            if (listaRPTrptViewDieta2_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0018DET2 + "-";
            }

            //LISTADO FORMFE_0019
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit> listaRPTrptViewMedicamentos1_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>();
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit> listaRPTrptViewMedicamentos2_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>();
            //Subreporte 1
            listaRPTrptViewMedicamentos1_FE = getDatarptViewMedicamentos_FE("MASIVO"
                                                , ENTITY_GLOBAL.Instance.UnidadReplicacion
                                                , (int)ENTITY_GLOBAL.Instance.PacienteID
                                                , (int)ENTITY_GLOBAL.Instance.EpisodioClinico
                                                , (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                                , objEntidad
                                                , idImpresionLog
                // , FORMFE_0019
                                               , FORMFE_0019DET1
                                                , ENTITY_GLOBAL.Instance.USUARIO, 1);
            //Rpt.Subreports["rptViewMedicamentos_FEDetalle1.rpt"].SetDataSource(listaRPTrptViewMedicamentos1_FE);
            //Rpt.SetDataSource(listaRPTrptViewMedicamentos1_FE);
            if (listaRPTrptViewMedicamentos1_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                //  formatos = formatos + FORMFE_0019 + "-";
                formatos = formatos + FORMFE_0019DET1 + "-";
            }
            //Subreporte 2
            listaRPTrptViewMedicamentos2_FE = getDatarptViewMedicamentos_FE("MASIVO",
                                                 ENTITY_GLOBAL.Instance.UnidadReplicacion
                                                , (int)ENTITY_GLOBAL.Instance.PacienteID
                                                , (int)ENTITY_GLOBAL.Instance.EpisodioClinico
                                                , (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                                , objEntidad
                                                , idImpresionLog
                // , FORMFE_0019
                                               , FORMFE_0019DET2
                                                , ENTITY_GLOBAL.Instance.USUARIO, 4);

            //Rpt.Subreports["rptViewMedicamentos_FEDetalle2.rpt"].SetDataSource(listaRPTrptViewMedicamentos2_FE);
            //Rpt.SetDataSource(listaRPTrptViewMedicamentos2_FE);
            if (listaRPTrptViewMedicamentos2_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0019DET2 + "-";
            }
            //Subreporte 3
            DataTable listaRPTPac_Med = new DataTable();
            listaRPTPac_Med = rptDatosPacienteMedico_FE("rptViewDatosPaciente_Medico",
                            ENTITY_GLOBAL.Instance.UnidadReplicacion,
                            (int)ENTITY_GLOBAL.Instance.PacienteID,
                            (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                            (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                            , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                            ENTITY_GLOBAL.Instance.USUARIO);

            if (listaRPTPac_Med.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0019DET3 + "-";
            }

            //LISTADO FORMFE_0020
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit> listaRPTrptViewDescansoMedicoFE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit>();
            listaRPTrptViewDescansoMedicoFE = getDatarptViewEmitirDescansoMedicoFE("MASIVO"
                                                , ENTITY_GLOBAL.Instance.UnidadReplicacion
                                                , (int)ENTITY_GLOBAL.Instance.PacienteID
                                                , (int)ENTITY_GLOBAL.Instance.EpisodioClinico
                                                , (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                                , objEntidad
                                                , idImpresionLog
                                                , FORMFE_0020
                                                , ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewDescansoMedicoFE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0020 + "-";
            }
            //LISTADO FORMFE_0021
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit> listaRPTrptViewEvolucionMedica_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit>();
            listaRPTrptViewEvolucionMedica_FE = getDatarptViewEvolucionMedica_FE("MASIVO"
                                                , ENTITY_GLOBAL.Instance.UnidadReplicacion
                                                , (int)ENTITY_GLOBAL.Instance.PacienteID
                                                , (int)ENTITY_GLOBAL.Instance.EpisodioClinico
                                                , (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                                , objEntidad
                                                , idImpresionLog
                                                , FORMFE_0021
                                                , ENTITY_GLOBAL.Instance.USUARIO);
            if (listaRPTrptViewEvolucionMedica_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0021 + "-";
            }

            #endregion

            // FORMULARIO FED
            #region FORMULARIOSFED_GETDATA

            //LISTADO FORMFE_0038
            DataTable listarptVistasGlasgow_FE = new DataTable();

            //listarptVistasGlasgow_FE = rptVistasGlasgow_FE(
            //          "rptViewEscalaGlasgow_FE"
            //        , ENTITY_GLOBAL.Instance.UnidadReplicacion
            //        , (int)ENTITY_GLOBAL.Instance.PacienteID
            //        , (int)ENTITY_GLOBAL.Instance.EpisodioClinico
            //        , (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
            //        , null
            //        , 0
            //        , ENTITY_GLOBAL.Instance.CONCEPTO
            //        , ENTITY_GLOBAL.Instance.USUARIO
            //        , "EG");

            if (listarptVistasGlasgow_FE.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0038 + "-";
            }



            //LISTADO FORMFE_0039
            DataTable listarptVistasGlasgowPreEscolar_FE = new DataTable();

            //listarptVistasGlasgowPreEscolar_FE = rptVistasGlasgow_FE("rptViewEscalaGlasgow_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
            //   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, "GP");

            if (listarptVistasGlasgowPreEscolar_FE.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0039 + "-";
            }

            //LISTADO FORMFE_0040
            DataTable listarptVistasGlasgowLactante_FE = new DataTable();

            //listarptVistasGlasgowLactante_FE = rptVistasGlasgow_FE("rptViewEscalaGlasgow_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
            //   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, "GL");

            if (listarptVistasGlasgowLactante_FE.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0040 + "-";
            }

            //LISTADO FORMFE_0041
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEscalaStewart_FEEdit> listarptVistasStewart_FE = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEscalaStewart_FEEdit>();
            listarptVistasStewart_FE = getDatarptViewEscalaStewart_FE("MASIVO"
                                                , ENTITY_GLOBAL.Instance.UnidadReplicacion
                                                , (int)ENTITY_GLOBAL.Instance.PacienteID
                                                , (int)ENTITY_GLOBAL.Instance.EpisodioClinico
                                                , (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                                , objEntidad
                                                , idImpresionLog
                                                , FORMFE_0041
                                                , ENTITY_GLOBAL.Instance.USUARIO);

            if (listarptVistasStewart_FE.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0041 + "-";
            }

            //LISTADO FORMFE_0042

            DataTable listarptVistasRamsay_FE = new DataTable();

            listarptVistasRamsay_FE = rptVistasEscalaRamsay_FE("rptViewEscalaRamsay_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID
                                   , (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);


            if (listarptVistasRamsay_FE.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0042 + "-";
            }



            //LISTADO FORMFE_0043
            DataTable listarptVistasRetiroVoluntario_FE1 = new DataTable();
            DataTable listarptVistasRetiroVoluntario_FE2 = new DataTable();

            string varVistaEntidad = "rptViewRetiroVoluntario_FE"; // Entidad Vista
            listarptVistasRetiroVoluntario_FE1 = rptVistasRetiroVoluntario_FE(varVistaEntidad, ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID
                                   , (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            listarptVistasRetiroVoluntario_FE2 = rptVistasRetiroVoluntario_FE(varVistaEntidad
                                , ""
                                , 0
                                , 0
                                , 0
                                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);


            // Recorrer y asignar valores

            foreach (DataRow ht_fila in listarptVistasRetiroVoluntario_FE1.AsEnumerable())
            {

                DataRow rw = listarptVistasRetiroVoluntario_FE2.NewRow();

                rw["UnidadReplicacion"] = ht_fila["UnidadReplicacion"];
                rw["IdEpisodioAtencion"] = ht_fila["IdEpisodioAtencion"];
                rw["IdPaciente"] = ht_fila["IdPaciente"];
                rw["EpisodioClinico"] = ht_fila["EpisodioClinico"];
                rw["IdRetiroVoluntario"] = ht_fila["IdRetiroVoluntario"];
                rw["FechaIngreso"] = ht_fila["FechaIngreso"];
                rw["HoraIngreso"] = ht_fila["HoraIngreso"];
                rw["RepresentanteLegal"] = ht_fila["RepresentanteLegal"];
                rw["IdPersonalSalud"] = ht_fila["IdPersonalSalud"];
                rw["UsuarioCreacion"] = ht_fila["UsuarioCreacion"];
                rw["FechaCreacion"] = ht_fila["FechaCreacion"];
                rw["UsuarioModificacion"] = ht_fila["UsuarioModificacion"];
                rw["FechaModificacion"] = ht_fila["FechaModificacion"];
                rw["Accion"] = ht_fila["Accion"];
                rw["Version"] = ht_fila["Version"];
                rw["ApellidoPaterno"] = ht_fila["ApellidoPaterno"];
                rw["ApellidoMaterno"] = ht_fila["ApellidoMaterno"];
                rw["Nombres"] = ht_fila["Nombres"];
                rw["NombreCompleto"] = ht_fila["NombreCompleto"];
                rw["Busqueda"] = ht_fila["Busqueda"];
                rw["TipoDocumento"] = ht_fila["TipoDocumento"];
                rw["Documento"] = ht_fila["Documento"];
                rw["FechaNacimiento"] = ht_fila["FechaNacimiento"];
                rw["Sexo"] = ht_fila["Sexo"];
                rw["EstadoCivil"] = ht_fila["EstadoCivil"];
                rw["PersonaEdad"] = ht_fila["PersonaEdad"];
                rw["IdOrdenAtencion"] = ht_fila["IdOrdenAtencion"];
                rw["CodigoOA"] = ht_fila["CodigoOA"];
                rw["LineaOrdenAtencion"] = ht_fila["LineaOrdenAtencion"];
                rw["TipoOrdenAtencion"] = ht_fila["TipoOrdenAtencion"];
                rw["TipoAtencion"] = ht_fila["TipoAtencion"];
                rw["TipoTrabajador"] = ht_fila["TipoTrabajador"];
                rw["IdEstablecimientoSalud"] = ht_fila["IdEstablecimientoSalud"];
                rw["IdUnidadServicio"] = ht_fila["IdUnidadServicio"];
                rw["FechaRegistro"] = ht_fila["FechaRegistro"];
                rw["FechaAtencion"] = ht_fila["FechaAtencion"];
                rw["IdEspecialidad"] = ht_fila["IdEspecialidad"];
                rw["IdTipoOrden"] = ht_fila["IdTipoOrden"];
                rw["estadoEpiAtencion"] = ht_fila["estadoEpiAtencion"];
                rw["ObservacionProximaEpiAtencion"] = ht_fila["ObservacionProximaEpiAtencion"];
                rw["TipoAtencionDesc"] = ht_fila["TipoAtencionDesc"];
                rw["TipoTrabajadorDesc"] = ht_fila["TipoTrabajadorDesc"];
                rw["EstablecimientoCodigo"] = ht_fila["EstablecimientoCodigo"];
                rw["EstablecimientoDesc"] = ht_fila["EstablecimientoDesc"];
                rw["UnidadServicioCodigo"] = ht_fila["UnidadServicioCodigo"];
                rw["UnidadServicioDesc"] = ht_fila["UnidadServicioDesc"];
                rw["NombreCompletoPerSalud"] = ht_fila["NombreCompletoPerSalud"];
                rw["CMP"] = ht_fila["CMP"];
                rw["CodigoHC"] = ht_fila["CodigoHC"];
                rw["Cama"] = ENTITY_GLOBAL.Instance.CAMA;


                listarptVistasRetiroVoluntario_FE2.Rows.Add(rw);

            }


            if (listarptVistasRetiroVoluntario_FE2.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0043 + "-";
            }

            //LISTADO FORMFE_0044

            DataTable listarptVistasFuncionesVitales_FE = new DataTable();

            listarptVistasFuncionesVitales_FE = rptVistas_FE("rptViewFuncionesVitales_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID
                                   , (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);


            if (listarptVistasFuncionesVitales_FE.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0044 + "-";
            }

            //LISTADO FORMFE_0045

            DataTable listarptVistasEnfermedadActual_FE = new DataTable();

            listarptVistasEnfermedadActual_FE = rptVistas_FE("rptViewEnfermedadActual_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID
                                   , (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);


            if (listarptVistasEnfermedadActual_FE.Rows.Count > 0)
            {
                //DE AYUDA A LA FÓRMULA DE SUPRIMIR EN LA PLANTILLA
                formatos = formatos + FORMFE_0045 + "-";
            }

            #endregion

            /**ADD DATOS GENERALES DEL REPORTES EN 'ListrptViewAgrupador'*/
            //OBS: AUX TipoEpisodio:  usado para la fórmula de mostrar o no un subreporte de acuerdo al FORMATO que contenga
            objEntidad.TipoEpisodio = formatos;

            ListrptViewAgrupador.Add(objEntidad);
            Rpt.DataSourceConnections.Clear();
            Rpt.SetDataSource(ListrptViewAgrupador);
            /********************************/

            int cantidadSubReport = Rpt.Subreports.Count;

            try
            {
                if (cantidadSubReport > 0)
                {

                    #region FORMULARIOINICIALES_SETDATASOURCE
                    //ADD 00
                    //Rpt.Subreports[0].DataSourceConnections.Clear();
                    //Rpt.Subreports[0].SetDataSource(listaRPTrptViewAnamnesisEAEdit);

                    ////ADD FORM_0001
                    //Rpt.Subreports[1].DataSourceConnections.Clear();
                    //Rpt.Subreports[1].SetDataSource(listaRPTrptViewExamenSolicitadoEdit);


                    ////ADD FORM_0002
                    //Rpt.Subreports[2].DataSourceConnections.Clear();
                    //Rpt.Subreports[2].SetDataSource(listaRPTrptViewExamenTriajeEdit);

                    ////ADD FORM_0003
                    //Rpt.Subreports[3].DataSourceConnections.Clear();
                    //Rpt.Subreports[3].SetDataSource(listaRPTrptViewMedicamentoEdit);

                    ////ADD FORM_0004
                    //Rpt.Subreports[4].DataSourceConnections.Clear();
                    //Rpt.Subreports[4].SetDataSource(listaRPTrptViewExamenRegionalEdit);

                    ////ADD FORM_0005
                    //Rpt.Subreports[5].DataSourceConnections.Clear();
                    //Rpt.Subreports[5].SetDataSource(listaRPTrptViewEvolucionObjetivaEdit);

                    ////ADD FORM_0006
                    //Rpt.Subreports[6].DataSourceConnections.Clear();
                    //Rpt.Subreports[6].SetDataSource(listaRPTrptViewAnamnesisAFEdit);


                    ////ADD FORM_0007
                    //Rpt.Subreports[7].DataSourceConnections.Clear();
                    //Rpt.Subreports[7].SetDataSource(listaRPTrptViewDiagnosticoEdit);


                    ////ADD FORM_0008
                    //Rpt.Subreports[8].DataSourceConnections.Clear();
                    //Rpt.Subreports[8].SetDataSource(listaRPTrptViewAnamnesisAPEdit);

                    ////ADD FORM_0009
                    //Rpt.Subreports[9].DataSourceConnections.Clear();
                    //Rpt.Subreports[9].SetDataSource(listaRPTrptViewEmitirDescansoMedico);


                    ////ADD FORM_0010
                    //Rpt.Subreports[10].DataSourceConnections.Clear();
                    //Rpt.Subreports[10].SetDataSource(listaRPTrptViewProximaAtencion);


                    ////ADD FORM_0011
                    //Rpt.Subreports[11].DataSourceConnections.Clear();
                    //Rpt.Subreports[11].SetDataSource(listaRPTrptViewSolicitarReferencia);


                    ////ADD FORM_0012
                    //Rpt.Subreports[12].DataSourceConnections.Clear();
                    //Rpt.Subreports[12].SetDataSource(listaRPTrptViewCuidadosPreventivo);

                    #endregion

                    #region FORMULARIOEXTRAS_SETDATASOURCE

                    //ADD FORMFE_0001 (ok)
                    if (listaRPTInmunizacionNinio.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewInmunizacionNinio_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewInmunizacionNinio_FEsubrep.rpt"].SetDataSource(listaRPTInmunizacionNinio);
                    }


                    //ADD FORMFE_0002
                    if (listaRPTInmunizacionAdulto.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewInmunizacionAdultoRep_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewInmunizacionAdultoRep_FEsubrep.rpt"].SetDataSource(listaRPTInmunizacionAdulto);
                    }

                    //ADD FORMFE_0003
                    if (listaRPTrptViewAntPerFisiologico_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewAntecedentesPersonalesFisiologicos_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAntecedentesPersonalesFisiologicos_FEsubrep.rpt"].SetDataSource(listaRPTrptViewAntPerFisiologico_FE);
                    }

                    //ADD FORMFE_0004
                    if (listaRPTrptViewAntFisiologicoPediatrico_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewAntFisiologicoPediatricoFEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAntFisiologicoPediatricoFEsubrep.rpt"].SetDataSource(listaRPTrptViewAntFisiologicoPediatrico_FE);
                    }

                    //ADD FORMFE_0005
                    if (listaRPTrptAntGenerales_FE.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewAntecedentesPersonalesPatologicosGenerales_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAntecedentesPersonalesPatologicosGenerales_FEsubrep.rpt"].SetDataSource(listaRPTrptAntGenerales_FE);
                    }
                    if (listaRPTrptAntGeneralesDetalle_FE.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewAntecedentesPatologicosGeneralesdetalle.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAntecedentesPatologicosGeneralesdetalle.rpt"].SetDataSource(listaRPTrptAntGeneralesDetalle_FE);
                    }


                    //ADD FORMFE_0006 (ok)
                    if (listarptViewAlergias_FE.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewAlergia_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAlergia_FEsubrep.rpt"].SetDataSource(listarptViewAlergias_FE);
                    }



                    //ADD FORMFE_0007
                    if (listarptAnt_Familiares.Count > 0)
                    {
                        Rpt.Subreports["rptViewAnamnesis_ANTFAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewAnamnesis_ANTFAM_FEsubrep.rpt"].SetDataSource(listarptAnt_Familiares);
                    }

                    //ADD FORMFE_0008
                    if (listarptView_ValoracionFuncionalAM_FE.Count > 0)
                    {
                        Rpt.Subreports["rptView_ValoracionFuncionalAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptView_ValoracionFuncionalAM_FEsubrep.rpt"].SetDataSource(listarptView_ValoracionFuncionalAM_FE);
                    }

                    //ADD FORMFE_0009
                    if (listarptViewValoracionMentalAM_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewValoracionMentalAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewValoracionMentalAM_FEsubrep.rpt"].SetDataSource(listarptViewValoracionMentalAM_FE);
                    }

                    //ADD FORMFE_0010
                    if (listaRPTrptViewValoracionSocioFamAM_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewValoracionSocioFamAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewValoracionSocioFamAM_FEsubrep.rpt"].SetDataSource(listaRPTrptViewValoracionSocioFamAM_FE);
                    }


                    //ADD FORMFE_0011
                    if (listaRPTrptViewValoracionAM_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewValoracionAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewValoracionAM_FEsubrep.rpt"].SetDataSource(listaRPTrptViewValoracionAM_FE);
                    }

                    //ADD FORMFE_0012 (ok)
                    if (listaRPTrptViewDiagnostico_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewDiagnostico_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewDiagnostico_FEsubrep.rpt"].SetDataSource(listaRPTrptViewDiagnostico_FE);
                    }

                    //ADD FORMFE_0013
                    if (listaRPTrptViewExamenApoyo_FE.Count > 0)
                    {
                        Rpt.Subreports["ptViewExamenApoyo_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["ptViewExamenApoyo_FEsubrep.rpt"].SetDataSource(listaRPTrptViewExamenApoyo_FE);
                    }


                    //ADD FORMFE_0014  
                    if (listaRPTrptViewInterconsulta_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewInterconsulta_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewInterconsulta_FEsubrep.rpt"].SetDataSource(listaRPTrptViewInterconsulta_FE);
                    }

                    //ADD FORMFE_0015       
                    if (listaRPTrptViewProximaAtencion_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewProximaAtencion_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewProximaAtencion_FEsubrep.rpt"].SetDataSource(listaRPTrptViewProximaAtencion_FE);
                    }

                    //ADD FORMFE_0016             
                    if (listaRPTApoyoDiagnostico.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewApoyoDiagnosticoRep_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewApoyoDiagnosticoRep_FEsubrep.rpt"].SetDataSource(listaRPTApoyoDiagnostico);
                    }
                    //ADD FORMFE_0017

                    //ADD FORMFE_0018    
                    if (listaRPTrptViewDieta1_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewDieta_FEDetalle1.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewDieta_FEDetalle1.rpt"].SetDataSource(listaRPTrptViewDieta1_FE);
                    }
                    if (listaRPTrptViewDieta2_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewDieta_FEDetalle2.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewDieta_FEDetalle2.rpt"].SetDataSource(listaRPTrptViewDieta2_FE);
                    }

                    //ADD FORMFE_0019  (ok)    
                    if (listaRPTrptViewMedicamentos1_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewMedicamentos_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewMedicamentos_FEsubrep.rpt"].SetDataSource(listaRPTrptViewMedicamentos1_FE);
                    }
                    if (listaRPTrptViewMedicamentos2_FE.Count > 0)
                    {
                        Rpt.Subreports["rptViewMedicamentos_FEsubrep2.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewMedicamentos_FEsubrep2.rpt"].SetDataSource(listaRPTrptViewMedicamentos2_FE);
                    }
                    if (listaRPTPac_Med.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewMedicamentos_FEsubrepFirmas.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewMedicamentos_FEsubrepFirmas.rpt"].SetDataSource(listaRPTPac_Med);
                    }
                    //ADD FORMFE_0020     (ok)
                    if (listaRPTrptViewDescansoMedicoFE.Count > 0)
                    {
                        Rpt.Subreports["rptViewEmitirDescansoMedicoFEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewEmitirDescansoMedicoFEsubrep.rpt"].SetDataSource(listaRPTrptViewDescansoMedicoFE);
                    }
                    //ADD FORMFE_0021  
                    if (listaRPTrptViewEvolucionMedica_FE.Count > 0)
                    {
                        try
                        {
                            Rpt.Subreports["rptViewEvolucionMedica_FEsubrep.rpt"].DataSourceConnections.Clear();
                            Rpt.Subreports["rptViewEvolucionMedica_FEsubrep.rpt"].SetDataSource(listaRPTrptViewEvolucionMedica_FE);

                        }
                        catch (Exception)
                        {
                            Response.Write("<script language=javascript>alert('No se encuentra el subreporte rptViewEvolucionMedica_FEsubrep');</script>");
                            //throw;
                        }
                    }

                    #endregion

                    #region FORMULARIOFED_SETDATASOURCE_ADJUNTO

                    //ADD FORMFE_0038     
                    if (listarptVistasGlasgow_FE.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewEscalaGlasgow_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewEscalaGlasgow_FEsubrep.rpt"].SetDataSource(listarptVistasGlasgow_FE);
                    }


                    //ADD FORMFE_0038     
                    if (listarptVistasGlasgowPreEscolar_FE.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewEscalaGlasgowPreEscolar_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewEscalaGlasgowPreEscolar_FEsubrep.rpt"].SetDataSource(listarptVistasGlasgowPreEscolar_FE);
                    }

                    //ADD FORMFE_0040     
                    if (listarptVistasGlasgowLactante_FE.Rows.Count > 0)
                    {
                        Rpt.Subreports["rptViewEscalaGlasgowLactante_FEsubrep.rpt"].DataSourceConnections.Clear();
                        Rpt.Subreports["rptViewEscalaGlasgowLactante_FEsubrep.rpt"].SetDataSource(listarptVistasGlasgowLactante_FE);
                    }

                    //ADD FORMFE_0041  
                    if (listarptVistasStewart_FE.Count > 0)
                    {
                        try
                        {
                            Rpt.Subreports["rptViewEscalaStewart_FEsubrep.rpt"].DataSourceConnections.Clear();
                            Rpt.Subreports["rptViewEscalaStewart_FEsubrep.rpt"].SetDataSource(listarptVistasStewart_FE);

                        }
                        catch (Exception)
                        {

                            //Response.Write("<script language=javascript>alert('No se encuentra el subreporte rptViewEscalaStewart_FEsubrep');</script>");
                            //throw;
                        }
                    }

                    //ADD FORMFE_0042
                    if (listarptVistasRamsay_FE.Rows.Count > 0)
                    {
                        try
                        {
                            Rpt.Subreports["rptViewEscalaRamsay_FEsubrep.rpt"].DataSourceConnections.Clear();
                            Rpt.Subreports["rptViewEscalaRamsay_FEsubrep.rpt"].SetDataSource(listarptVistasRamsay_FE);
                        }
                        catch (Exception)
                        {
                        }

                    }

                    //ADD FORMFE_0043
                    if (listarptVistasRetiroVoluntario_FE2.Rows.Count > 0)
                    {
                        try
                        {
                            Rpt.Subreports["rptViewRetiroVoluntario_FEsubrep.rpt"].DataSourceConnections.Clear();
                            Rpt.Subreports["rptViewRetiroVoluntario_FEsubrep.rpt"].SetDataSource(listarptVistasRetiroVoluntario_FE2);

                        }
                        catch (Exception)
                        {
                        }
                    }

                    //ADD FORMFE_0044
                    if (listarptVistasFuncionesVitales_FE.Rows.Count > 0)
                    {
                        try
                        {
                            Rpt.Subreports["rptViewFuncionesVitale_FEsubrep.rpt"].DataSourceConnections.Clear();
                            Rpt.Subreports["rptViewFuncionesVitale_FEsubrep.rpt"].SetDataSource(listarptVistasFuncionesVitales_FE);

                        }
                        catch (Exception)
                        {
                        }
                    }

                    //ADD FORMFE_0045
                    if (listarptVistasEnfermedadActual_FE.Rows.Count > 0)
                    {
                        try
                        {
                            Rpt.Subreports["rptViewEnfermedadActual_FEsubrep.rpt"].DataSourceConnections.Clear();
                            Rpt.Subreports["rptViewEnfermedadActual_FEsubrep.rpt"].SetDataSource(listarptVistasEnfermedadActual_FE);

                        }
                        catch (Exception)
                        {
                        }
                    }

                    #endregion

                    #region FORMULARIOEXTRAS_SETDATASOURCE_ADJUNTOFE

                    //if (listarptAgrupador_FE.Rows.Count > 0)
                    //{
                    //    Rpt.Subreports["rptDatosGeneralesFE.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptDatosGeneralesFE.rpt"].SetDataSource(listarptAgrupador_FE);
                    //}


                    ////ADD FORMFE_0001 (ok)
                    //if (listaRPTInmunizacionNinio.Rows.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewInmunizacionNinio_FE.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewInmunizacionNinio_FE.rpt"].SetDataSource(listaRPTInmunizacionNinio);
                    //}


                    ////ADD FORMFE_0002
                    //if (listaRPTInmunizacionAdulto.Rows.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewInmunizacionAdultoRep_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewInmunizacionAdultoRep_FEsubrep.rpt"].SetDataSource(listaRPTInmunizacionAdulto);
                    //}

                    ////ADD FORMFE_0003
                    //if (listaRPTrptViewAntPerFisiologico_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewAntecedentesPersonalesFisiologicos_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewAntecedentesPersonalesFisiologicos_FEsubrep.rpt"].SetDataSource(listaRPTrptViewAntPerFisiologico_FE);
                    //}

                    ////ADD FORMFE_0004
                    //if (listaRPTrptViewAntFisiologicoPediatrico_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewAntFisiologicoPediatricoFEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewAntFisiologicoPediatricoFEsubrep.rpt"].SetDataSource(listaRPTrptViewAntFisiologicoPediatrico_FE);
                    //}

                    ////ADD FORMFE_0005
                    //if (listaRPTrptAntGenerales_FE.Rows.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewAntecedentesPersonalesPatologicosGenerales_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewAntecedentesPersonalesPatologicosGenerales_FEsubrep.rpt"].SetDataSource(listaRPTrptAntGenerales_FE);
                    //}
                    //if (listaRPTrptAntGeneralesDetalle_FE.Rows.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewAntecedentesPatologicosGeneralesdetalle.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewAntecedentesPatologicosGeneralesdetalle.rpt"].SetDataSource(listaRPTrptAntGeneralesDetalle_FE);
                    //}


                    ////ADD FORMFE_0006 (ok)
                    //if (listarptViewAlergias_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewAlergia_FE.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewAlergia_FE.rpt"].SetDataSource(listarptViewAlergias_FE);
                    //}



                    ////ADD FORMFE_0007
                    //if (listarptAnt_Familiares.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewAnamnesis_ANTFAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewAnamnesis_ANTFAM_FEsubrep.rpt"].SetDataSource(listarptAnt_Familiares);
                    //}

                    ////ADD FORMFE_0008
                    //if (listarptView_ValoracionFuncionalAM_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptView_ValoracionFuncionalAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptView_ValoracionFuncionalAM_FEsubrep.rpt"].SetDataSource(listarptView_ValoracionFuncionalAM_FE);
                    //}

                    ////ADD FORMFE_0009
                    //if (listarptViewValoracionMentalAM_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewValoracionMentalAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewValoracionMentalAM_FEsubrep.rpt"].SetDataSource(listarptViewValoracionMentalAM_FE);
                    //}

                    ////ADD FORMFE_0010
                    //if (listaRPTrptViewValoracionSocioFamAM_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewValoracionSocioFamAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewValoracionSocioFamAM_FEsubrep.rpt"].SetDataSource(listaRPTrptViewValoracionSocioFamAM_FE);
                    //}


                    ////ADD FORMFE_0011
                    //if (listaRPTrptViewValoracionAM_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewValoracionAM_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewValoracionAM_FEsubrep.rpt"].SetDataSource(listaRPTrptViewValoracionAM_FE);
                    //}

                    ////ADD FORMFE_0012 (ok)
                    //if (listaRPTrptViewDiagnostico_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewDiagnostico_FE.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewDiagnostico_FE.rpt"].SetDataSource(listaRPTrptViewDiagnostico_FE);
                    //}

                    ////ADD FORMFE_0013
                    //if (listaRPTrptViewExamenApoyo_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["ptViewExamenApoyo_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["ptViewExamenApoyo_FEsubrep.rpt"].SetDataSource(listaRPTrptViewExamenApoyo_FE);
                    //}


                    ////ADD FORMFE_0014  
                    //if (listaRPTrptViewInterconsulta_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewInterconsulta_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewInterconsulta_FEsubrep.rpt"].SetDataSource(listaRPTrptViewInterconsulta_FE);
                    //}

                    ////ADD FORMFE_0015       
                    //if (listaRPTrptViewProximaAtencion_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewProximaAtencion_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewProximaAtencion_FEsubrep.rpt"].SetDataSource(listaRPTrptViewProximaAtencion_FE);
                    //}

                    ////ADD FORMFE_0016             
                    //if (listaRPTApoyoDiagnostico.Rows.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewApoyoDiagnosticoRep_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewApoyoDiagnosticoRep_FEsubrep.rpt"].SetDataSource(listaRPTApoyoDiagnostico);
                    //}
                    ////ADD FORMFE_0017

                    ////ADD FORMFE_0018    
                    //if (listaRPTrptViewDieta1_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewDieta_FEDetalle1.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewDieta_FEDetalle1.rpt"].SetDataSource(listaRPTrptViewDieta1_FE);
                    //}
                    //if (listaRPTrptViewDieta2_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewDieta_FEDetalle2.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewDieta_FEDetalle2.rpt"].SetDataSource(listaRPTrptViewDieta2_FE);
                    //}

                    ////ADD FORMFE_0019  (ok)    
                    //if (listaRPTrptViewMedicamentos_FE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewMedicamentos_FE.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewMedicamentos_FE.rpt"].SetDataSource(listaRPTrptViewMedicamentos_FE);
                    //}

                    ////ADD FORMFE_0020     (ok)
                    //if (listaRPTrptViewDescansoMedicoFE.Count > 0)
                    //{
                    //    Rpt.Subreports["rptViewEmitirDescansoMedicoFE.rpt"].DataSourceConnections.Clear();
                    //    Rpt.Subreports["rptViewEmitirDescansoMedicoFE.rpt"].SetDataSource(listaRPTrptViewDescansoMedicoFE);
                    //}
                    ////ADD FORMFE_0021  
                    //if (listaRPTrptViewEvolucionMedica_FE.Count > 0)
                    //{
                    //    try
                    //    {
                    //        Rpt.Subreports["rptViewEvolucionMedica_FEsubrep.rpt"].DataSourceConnections.Clear();
                    //        Rpt.Subreports["rptViewEvolucionMedica_FEsubrep.rpt"].SetDataSource(listaRPTrptViewEvolucionMedica_FE);

                    //    }
                    //    catch (Exception)
                    //    {
                    //        Response.Write("<script language=javascript>alert('No se encuentra el subreporte rptViewEvolucionMedica_FEsubrep');</script>");
                    //        //throw;
                    //    }
                    //}


                    #endregion




                }
            }
            catch (Exception ex)
            {
                throw;
            }

            /**ADD PARÁMETROS*/
            #region FORMULARIOINICALES_SETPARAMETER
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            imgFirma = Server.MapPath((string)Session["FIRMA_DIGITAL"]);
            Rpt.SetParameterValue("imgFirma", imgFirma);

            Rpt.SetParameterValue("FORM_0000", FORM_0000);
            Rpt.SetParameterValue("FORM_0001", FORM_0001);
            Rpt.SetParameterValue("FORM_0002", FORM_0002);
            Rpt.SetParameterValue("FORM_0003", FORM_0003);
            Rpt.SetParameterValue("FORM_0004", FORM_0004);
            Rpt.SetParameterValue("FORM_0005", FORM_0005);
            Rpt.SetParameterValue("FORM_0006", FORM_0006);
            Rpt.SetParameterValue("FORM_0007", FORM_0007);
            Rpt.SetParameterValue("FORM_0008", FORM_0008);
            Rpt.SetParameterValue("FORM_0009", FORM_0009);
            Rpt.SetParameterValue("FORM_0010", FORM_0010);
            Rpt.SetParameterValue("FORM_0011", FORM_0011);
            Rpt.SetParameterValue("FORM_0012", FORM_0012);
            #endregion

            #region FORMULARIOEXTRAS_SETPARAMETER

            Rpt.SetParameterValue("FORMFE_0001", FORMFE_0001);
            Rpt.SetParameterValue("FORMFE_0002", FORMFE_0002);
            Rpt.SetParameterValue("FORMFE_0003", FORMFE_0003);
            Rpt.SetParameterValue("FORMFE_0004", FORMFE_0004);
            Rpt.SetParameterValue("FORMFE_0005CAB", FORMFE_0005CAB);
            Rpt.SetParameterValue("FORMFE_0005DET", FORMFE_0005DET);
            Rpt.SetParameterValue("FORMFE_0006", FORMFE_0006);
            Rpt.SetParameterValue("FORMFE_0007", FORMFE_0007);
            Rpt.SetParameterValue("FORMFE_0008", FORMFE_0008);
            Rpt.SetParameterValue("FORMFE_0009", FORMFE_0009);
            Rpt.SetParameterValue("FORMFE_0010", FORMFE_0010);
            Rpt.SetParameterValue("FORMFE_0011", FORMFE_0011);
            Rpt.SetParameterValue("FORMFE_0012", FORMFE_0012);
            Rpt.SetParameterValue("FORMFE_0013", FORMFE_0013);
            Rpt.SetParameterValue("FORMFE_0014", FORMFE_0014);
            Rpt.SetParameterValue("FORMFE_0015", FORMFE_0015);
            Rpt.SetParameterValue("FORMFE_0016", FORMFE_0016);
            Rpt.SetParameterValue("FORMFE_0017", FORMFE_0017);
            Rpt.SetParameterValue("FORMFE_0018DET1", FORMFE_0018DET1);
            Rpt.SetParameterValue("FORMFE_0018DET2", FORMFE_0018DET2);
            Rpt.SetParameterValue("FORMFE_0019", FORMFE_0019);
            Rpt.SetParameterValue("FORMFE_0019DET1", FORMFE_0019DET1);
            Rpt.SetParameterValue("FORMFE_0019DET2", FORMFE_0019DET2);
            Rpt.SetParameterValue("FORMFE_0019DET3", FORMFE_0019DET3);
            Rpt.SetParameterValue("FORMFE_0020", FORMFE_0020);
            Rpt.SetParameterValue("FORMFE_0021", FORMFE_0021);

            #endregion

            #region FORMULARIOFED_SETPARAMETER
            Rpt.SetParameterValue("FORMFE_0038", FORMFE_0038);
            Rpt.SetParameterValue("FORMFE_0039", FORMFE_0039);
            Rpt.SetParameterValue("FORMFE_0040", FORMFE_0040);
            Rpt.SetParameterValue("FORMFE_0041", FORMFE_0041);
            Rpt.SetParameterValue("FORMFE_0042", FORMFE_0042);
            Rpt.SetParameterValue("FORMFE_0043", FORMFE_0043);
            Rpt.SetParameterValue("FORMFE_0044", FORMFE_0044);
            Rpt.SetParameterValue("FORMFE_0045", FORMFE_0045);
            #endregion

            /******************/

            if (!existeDataHC)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.SetParameterValue("imgFirma", imgFirma);

                        Rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "EXAMEN");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                    Rpt.SetParameterValue("imgFirma", imgFirma);

                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetParameterValue("imgFirma", imgFirma);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }


        /******Registros de AUDITORÍA IMPRESIÓN********/

        public int setDataImpresionAuditoria(String tipo, int idImpresionLog,
            SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objActoInfoEpiAtencion,
            string codigoFormato,
            string codigoUsuario
            )
        {
            int result = 0;
            try
            {
                if (tipo == "REPORTEA") //INDIVIDUAL
                {
                    ///////////////////////////////                    
                    //PARA LA AUDITORIA DE IMPRESION
                    if (Session["VW_ATENCIONPACIENTE_GEN_SELECC"] != null)
                    {
                        VW_ATENCIONPACIENTE_GENERAL objSelecc = (VW_ATENCIONPACIENTE_GENERAL)Session["VW_ATENCIONPACIENTE_GEN_SELECC"];
                        setDataImpresionAuditoriaIndividual(objSelecc, codigoFormato);
                    }
                }
                else if (tipo == "MASIVO") //MASIVO
                {
                    if (objActoInfoEpiAtencion != null && idImpresionLog > 0)
                    {
                        ///////////////////////////////      
                        SS_HC_ImpresionHC_Detalle objDetalle = new SS_HC_ImpresionHC_Detalle();

                        SS_HC_ImpresionHC obj = new SS_HC_ImpresionHC();
                        obj.UnidadReplicacion = objActoInfoEpiAtencion.UnidadReplicacion;
                        obj.IdPaciente = objActoInfoEpiAtencion.IdPaciente;
                        obj.HostImpresion = UtilMVC.ObtenerIP();//MOVIDO 14/04/16
                        obj.UsuarioImpresion = codigoUsuario;
                        obj.FechaImpresion = DateTime.Now;
                        obj.Accion = "INSERT_DETALLE";

                        //
                        objDetalle.IdImpresion = idImpresionLog;
                        objDetalle.IdPaciente = objActoInfoEpiAtencion.IdPaciente;
                        objDetalle.IdEpisodioAtencion = objActoInfoEpiAtencion.IdEpisodioAtencion;
                        objDetalle.EpisodioClinico = objActoInfoEpiAtencion.EpisodioClinico;
                        objDetalle.EpisodioAtencion = objActoInfoEpiAtencion.EpisodioAtencion;
                        objDetalle.IdOpcion = ENTITY_GLOBAL.Instance.IDOPCION_ACTUAL != null ? ((int)ENTITY_GLOBAL.Instance.IDOPCION_ACTUAL) : 0;
                        objDetalle.CodigoOpcion = codigoFormato;
                        objDetalle.IdFormato = ENTITY_GLOBAL.Instance.IDFORMATO != null ? ((int)ENTITY_GLOBAL.Instance.IDFORMATO) : 0;

                        objDetalle.TipoAtencion = (int)Session["TIPOATENCION_ACTUAL"];
                        objDetalle.IdUnidadServicio = objActoInfoEpiAtencion.IdUnidadServicio;
                        objDetalle.IdPersonalSalud = objActoInfoEpiAtencion.IdPersonalSalud;
                        //
                        objDetalle.HostImpresion = UtilMVC.ObtenerIP();//MOVIDO 14/04/16
                        objDetalle.UsuarioImpresion = codigoUsuario;
                        objDetalle.FechaImpresion = DateTime.Now;
                        objDetalle.Accion = "INSERT_DETALLE";
                        result = SvcAuditoriaImpresion.save_ChangesTraking(obj, objDetalle, "SINGLE");
                    }
                }
                else if (tipo == "HEADER") //MASIVO
                {
                    ///////////////////////////////      
                    SS_HC_ImpresionHC_Detalle objDetalle = new SS_HC_ImpresionHC_Detalle();

                    SS_HC_ImpresionHC obj = new SS_HC_ImpresionHC();
                    obj.UnidadReplicacion = objActoInfoEpiAtencion.UnidadReplicacion;
                    obj.IdPaciente = objActoInfoEpiAtencion.IdPaciente;
                    obj.HostImpresion = UtilMVC.ObtenerIP();//MOVIDO 14/04/16
                    obj.UsuarioImpresion = codigoUsuario;
                    obj.FechaImpresion = DateTime.Now;
                    obj.Accion = "INSERT";
                    result = SvcAuditoriaImpresion.save_ChangesTraking(obj, objDetalle, "SINGLE");
                }

            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public void setDataImpresionAuditoriaIndividual(VW_ATENCIONPACIENTE_GENERAL objActoMedicoSelecc,
            string codigoFormato
            )
        {
            try
            {
                SS_HC_ImpresionHC_Detalle objDetalle = new SS_HC_ImpresionHC_Detalle();
                SS_HC_ImpresionHC obj = new SS_HC_ImpresionHC();
                obj.UnidadReplicacion = ENTITY_GLOBAL.Instance.UnidadReplicacion;
                obj.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
                obj.HostImpresion = ENTITY_GLOBAL.Instance.HostName;
                obj.UsuarioImpresion = ENTITY_GLOBAL.Instance.USUARIO;
                obj.FechaImpresion = DateTime.Now;
                obj.Accion = "INSERT_TOTAL";
                //
                objDetalle.IdPaciente = (int)ENTITY_GLOBAL.Instance.PacienteID;
                objDetalle.IdEpisodioAtencion = (int)ENTITY_GLOBAL.Instance.EpisodioAtencion;
                objDetalle.EpisodioClinico = ENTITY_GLOBAL.Instance.EpisodioClinico;
                objDetalle.EpisodioAtencion = ENTITY_GLOBAL.Instance.EpisodioAtencionPrim;
                objDetalle.IdOpcion = ENTITY_GLOBAL.Instance.IDOPCION_ACTUAL != null ? ((int)ENTITY_GLOBAL.Instance.IDOPCION_ACTUAL) : 0;
                objDetalle.CodigoOpcion = codigoFormato;
                objDetalle.IdFormato = ENTITY_GLOBAL.Instance.IDFORMATO != null ? ((int)ENTITY_GLOBAL.Instance.IDFORMATO) : 0;
                objDetalle.TipoAtencion = objActoMedicoSelecc.TipoAtencion;
                objDetalle.IdUnidadServicio = objActoMedicoSelecc.IdUnidadServicio;
                objDetalle.IdPersonalSalud = objActoMedicoSelecc.IdPersonalSalud;
                //
                objDetalle.HostImpresion = ENTITY_GLOBAL.Instance.HostName;
                objDetalle.UsuarioImpresion = ENTITY_GLOBAL.Instance.USUARIO;
                objDetalle.FechaImpresion = DateTime.Now;
                objDetalle.Accion = "INSERT_TOTAL";
                int result = SvcAuditoriaImpresion.save_ChangesTraking(obj, objDetalle, "SINGLE");
            }
            catch (Exception ex)
            {
            }

        }


        // *** FORMULARIOS (EXTRAS) ***

        private void GenerarReporterptViewDiagnostico_FE(string tipovista)
        {
            string tura = Server.MapPath("rptReports/rptViewDiagnostico_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewDiagnostico_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit>();
            listaRPT = getDatarptViewDiagnostico_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
              , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);

            }
            else
            {
                if (tipovista == "I")
                {
                    if (tipovista == "I")
                    {
                        ReportViewer.ReportSource = Rpt;
                        ReportViewer.DataBind();
                    }
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {

                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DIAGNOSTICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit>
        getDatarptViewDiagnostico_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
        , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
        string codFormato, string codUsuario)
        {

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit>();
            List<rptViewDiagnostico_FE> rptViewDiagnostico_FE = new List<rptViewDiagnostico_FE>();
            SS_HC_Diagnostico_FE objDiagnostico_FE = new SS_HC_Diagnostico_FE();
            objDiagnostico_FE.UnidadReplicacion = unidadReplicacion;
            objDiagnostico_FE.IdPaciente = idPaciente;
            objDiagnostico_FE.EpisodioClinico = epiClinico;
            objDiagnostico_FE.IdEpisodioAtencion = idEpiAtencion;
            objDiagnostico_FE.Accion = "REPORTEA";
            rptViewDiagnostico_FE = ServiceReportes.ReporteDiagnostico_FE(objDiagnostico_FE, 0, 0);

            objTabla1 = new System.Data.DataTable();
            SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit objRPT;

            if (rptViewDiagnostico_FE != null)
            {
                foreach (rptViewDiagnostico_FE objReport in rptViewDiagnostico_FE)
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewDiagnostico_FEEdit();

                    objRPT.DiagnosticoDesc = objReport.DiagnosticoDesc;
                    objRPT.DeterminacionDiagnosticaDesc = objReport.DeterminacionDiagnosticaDesc;
                    objRPT.GradoAfeccionDesc = objReport.GradoAfeccionDesc;
                    objRPT.DiagnosticoPrincipalDesc = objReport.DiagnosticoPrincipalDesc;
                    objRPT.Observacion = objReport.Observacion;
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.Sexo = objReport.Sexo;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.Expr104 = objReport.Expr104;
                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewDiagnostico_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 

            }
            return listaRPT;
        }


        private void GenerarReporterptViewInmunizacionNinio_FE(string tipovista)
        {
            string tura = Server.MapPath("rptReports/rptViewInmunizacionNinio_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewInmunizacionNinio_FE.rpt"));


            DataTable listaRPT = new DataTable();

            listaRPT = rptVistas_FE("rptViewInmunizacionNinio_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion,
                        (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                        (long)ENTITY_GLOBAL.Instance.EpisodioAtencion,
                        null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);



            //Datos generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {

                if (tipovista == "I")
                {

                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DIAGNOSTICO");


                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);



                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewImnunizaionNinio_FEEdit>
        getDatarptViewImnunizaionNinio_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
           SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, String codFormato, String codUsuario)
        {

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewImnunizaionNinio_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewImnunizaionNinio_FEEdit>();
            List<rptViewInmunizacionNinio_FE> rptViewImnunizaionNinio = new List<rptViewInmunizacionNinio_FE>();
            SS_HC_AntecedentesPersonalesIN_FE objImnunizaionNinio = new SS_HC_AntecedentesPersonalesIN_FE();
            objImnunizaionNinio.UnidadReplicacion = unidadReplicacion;
            objImnunizaionNinio.IdPaciente = idPaciente;
            objImnunizaionNinio.EpisodioClinico = epiClinico;
            objImnunizaionNinio.IdEpisodioAtencion = idEpiAtencion;
            objImnunizaionNinio.Accion = "REPORTEA";

            rptViewImnunizaionNinio = ServiceReportes.ReporteInmunizacionNinio_FE(objImnunizaionNinio, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewImnunizaionNinio_FEEdit objRPT;

            if (rptViewImnunizaionNinio != null)
            {
                foreach (rptViewInmunizacionNinio_FE objReport in rptViewImnunizaionNinio)
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewImnunizaionNinio_FEEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;

                    objRPT.NO = objReport.NO;
                    objRPT.SI = objReport.SI;

                    objRPT.BCG_RN = objReport.BCG_RN;
                    objRPT.BCG_NoRecuerda = objReport.BCG_NoRecuerda;

                    objRPT.HepatitisBRN_RN = objReport.HepatitisBRN_RN;
                    objRPT.HepatitisBRN_NoRecuerda = objReport.HepatitisBRN_NoRecuerda;

                    objRPT.DPT_1era = objReport.DPT_1era;
                    objRPT.DPT_2da = objReport.DPT_2da;
                    objRPT.DPT_3era = objReport.DPT_3era;
                    objRPT.DPT_1erRef = objReport.DPT_1erRef;
                    objRPT.DPT_2doRef = objReport.DPT_2doRef;
                    objRPT.DPT_NoRecuerda = objReport.DPT_NoRecuerda;

                    objRPT.POLIO_1era = objReport.POLIO_1era;
                    objRPT.POLIO_2da = objReport.POLIO_2da;
                    objRPT.POLIO_3era = objReport.POLIO_3era;
                    objRPT.POLIO_1erRef = objReport.POLIO_1erRef;
                    objRPT.POLIO_2doRef = objReport.POLIO_2doRef;
                    objRPT.POLIO_NoRecuerda = objReport.POLIO_NoRecuerda;

                    objRPT.HiB_1era = objReport.HiB_1era;
                    objRPT.HiB_2da = objReport.HiB_2da;
                    objRPT.HiB_3era = objReport.HiB_3era;
                    objRPT.HiB_1erRef = objReport.HiB_1erRef;
                    objRPT.HiB_2doRef = objReport.HiB_2doRef;
                    objRPT.HiB_NoRecuerda = objReport.HiB_NoRecuerda;

                    objRPT.HEPATITISB_1era = objReport.HEPATITISB_1era;
                    objRPT.HEPATITISB_2da = objReport.HEPATITISB_2da;
                    objRPT.HEPATITISB_3era = objReport.HEPATITISB_3era;
                    objRPT.HEPATITISB_1erRef = objReport.HEPATITISB_1erRef;
                    objRPT.HEPATITISB_NoRecuerda = objReport.HEPATITISB_NoRecuerda;

                    objRPT.ROTAVIRUS_1era = objReport.ROTAVIRUS_1era;
                    objRPT.ROTAVIRUS_2da = objReport.ROTAVIRUS_2da;
                    objRPT.ROTAVIRUS_3era = objReport.ROTAVIRUS_3era;
                    objRPT.ROTAVIRUS_1erRef = objReport.ROTAVIRUS_1erRef;
                    objRPT.ROTAVIRUS_2doRef = objReport.ROTAVIRUS_2doRef;
                    objRPT.ROTAVIRUS_NoRecuerda = objReport.ROTAVIRUS_NoRecuerda;

                    objRPT.SRP_1era = objReport.SRP_1era;
                    objRPT.SRP_2da = objReport.SRP_2da;
                    objRPT.SRP_NoRecuerda = objReport.SRP_NoRecuerda;

                    objRPT.VARICELA_1era = objReport.VARICELA_1era;
                    objRPT.VARICELA_2da = objReport.VARICELA_2da;
                    objRPT.VARICELA_NoRecuerda = objReport.VARICELA_NoRecuerda;

                    objRPT.HEPATITISA_1era = objReport.HEPATITISA_1era;
                    objRPT.HEPATITISA_2da = objReport.HEPATITISA_2da;
                    objRPT.HEPATITISA_NoRecuerda = objReport.HEPATITISA_NoRecuerda;

                    objRPT.NEUMOCOCO_1era = objReport.NEUMOCOCO_1era;
                    objRPT.NEUMOCOCO_2da = objReport.NEUMOCOCO_2da;
                    objRPT.NEUMOCOCO_3era = objReport.NEUMOCOCO_3era;
                    objRPT.NEUMOCOCO_1erRef = objReport.NEUMOCOCO_1erRef;
                    objRPT.NEUMOCOCO_NoRecuerda = objReport.NEUMOCOCO_NoRecuerda;
                    objRPT.INFLUENZA = Convert.ToDateTime(objReport.INFLUENZA);
                    objRPT.Secuencia = Convert.ToInt32(objReport.Secuencia);
                    objRPT.OtrasVacunas = objReport.OtrasVacunas;


                    //if (objReport.PersonaEdad != null)
                    //{
                    //    objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    //}


                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewImnunizaionNinio.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }


        private void GenerarReporterptViewEmitirDescansoMedico_FE(String tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewEmitirDescansoMedicoFE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewEmitirDescansoMedicoFE.rpt"));


            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit>();
            listaRPT = getDatarptViewEmitirDescansoMedicoFE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);


            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));


            //--------------------------------------------------------------------------------//
            //  var FIRMAS = Session["FIRMA_DIGITAL"];

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            imgFirma = Server.MapPath((string)Session["FIRMA_DIGITAL"]);
            Rpt.SetParameterValue("imgFirma", imgFirma);

            //firma = Server.MapPath();
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.SetParameterValue("imgFirma", imgFirma);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                    Rpt.SetParameterValue("imgFirma", imgFirma);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetParameterValue("imgFirma", imgFirma);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);
        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit>

        getDatarptViewEmitirDescansoMedicoFE(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit>();

            List<rptViewEmitirDescansoMedico_FE> rptViewEmitirDescansoMedico = new List<rptViewEmitirDescansoMedico_FE>();
            SS_HC_DescansoMedico_FE objEmitirDescansoMedico = new SS_HC_DescansoMedico_FE();
            objEmitirDescansoMedico.UnidadReplicacion = unidadReplicacion;
            objEmitirDescansoMedico.IdPaciente = idPaciente;
            objEmitirDescansoMedico.EpisodioClinico = epiClinico;
            objEmitirDescansoMedico.IdEpisodioAtencion = idEpiAtencion;
            objEmitirDescansoMedico.Accion = "REPORTEA";

            rptViewEmitirDescansoMedico = ServiceReportes.ReporteEmitirDescansoMedico_FE(objEmitirDescansoMedico, 0, 0);

            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit objRPT;
            if (rptViewEmitirDescansoMedico != null)
            {
                foreach (rptViewEmitirDescansoMedico_FE objReport in rptViewEmitirDescansoMedico) // Loop through List with foreach.
                {


                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewEmitirDescansoMedicoFEEdit();

                    objRPT.Observacion = objReport.Observacion;

                    objRPT.FechaInicioDescanso = Convert.ToDateTime(objReport.FechaInicioDescanso);

                    objRPT.FechaFinDescanso = Convert.ToDateTime(objReport.FechaFinDescanso);

                    objRPT.Dias = Convert.ToInt32(objReport.Dias);
                    objRPT.Expr102 = objReport.Expr102;
                    objRPT.Expr104 = objReport.Expr104;

                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Sexo = objReport.Sexo;
                    if (objReport.PersonaEdad != null)
                    {
                        objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    }
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    objRPT.estadoEpiAtencion = Convert.ToInt32(objReport.estadoEpiAtencion);
                    objRPT.DiagnosticoDesc = objReport.DiagnosticoDesc;
                    objRPT.FechaAtencion = Convert.ToDateTime(objReport.FechaAtencion);
                    objRPT.Expr103 = objReport.Expr103;
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;
                    objRPT.EspecialidadDesc = objReport.EspecialidadDesc;

                    if (objReport.UnidadServicioCodigo != null && objReport.UnidadServicioCodigo != "")
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(objReport.UnidadServicioCodigo);

                        if (fi.Exists)
                        {

                            var NombreServidor = fi.Name;
                            var rutaServidor = Server.MapPath("../resources/DocumentosAdjuntos/firmas/");
                            if (!Directory.Exists(rutaServidor))
                            {
                                DirectoryInfo di = Directory.CreateDirectory(rutaServidor);
                            }
                            var PathServidor = rutaServidor + NombreServidor;
                            System.IO.File.Copy(objReport.UnidadServicioCodigo, PathServidor, true);
                            //System.IO.FileInfo fiServidor = new System.IO.FileInfo(PathServidor);
                            var PathOri = "../resources/DocumentosAdjuntos/firmas/" + NombreServidor;
                            //objRPT.Accion = PathOri;
                            Session["FIRMA_DIGITAL"] = PathOri;

                        }


                    }
                    else
                    {
                        Session["FIRMA_DIGITAL"] = "";
                    }

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                    
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewEmitirDescansoMedico.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }
        private void GenerarReporterptViewAlergia_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewAlergiaFE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewAlergiaFE.rpt"));
            DataTable listaRPT = new DataTable();

            listaRPT = rptVistas_FE("rptViewAlergias_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            DataSet obj = new DataSet();
            dsRptViewer.Tables.Add(objTabla1.Copy());
            dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));

            //List<SoluccionSalud.RepositoryReport.Entidades.rptViewAlergias_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAlergias_FEEdit>();
            //listaRPT = getDatarptViewAlergias_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
            //    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);



            //datos generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {

                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);


        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAlergias_FEEdit>
        getDatarptViewAlergias_FE(
            String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
            , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
            string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAlergias_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAlergias_FEEdit>();

            List<rptViewAlergias_FE> rptViewAlergia_FE = new List<rptViewAlergias_FE>();
            SS_HC_Alergia_FE objEmitirDescansoMedico = new SS_HC_Alergia_FE();
            objEmitirDescansoMedico.UnidadReplicacion = unidadReplicacion;
            objEmitirDescansoMedico.IdPaciente = idPaciente;
            objEmitirDescansoMedico.EpisodioClinico = epiClinico;
            objEmitirDescansoMedico.IdEpisodioAtencion = idEpiAtencion;
            objEmitirDescansoMedico.Accion = "REPORTEA";
            rptViewAlergia_FE = ServiceReportes.ReporteAlergia_FE(objEmitirDescansoMedico, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewAlergias_FEEdit objRPT;
            if (rptViewAlergia_FE != null)
            {
                foreach (rptViewAlergias_FE objReport in rptViewAlergia_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewAlergias_FEEdit();

                    objRPT.Observaciones = objReport.Observaciones;

                    objRPT.SI = objReport.SI;

                    objRPT.NO = objReport.NO;

                    objRPT.TipoAlergiaDesc = objReport.TipoAlergiaDesc;

                    objRPT.Nombre = objReport.Nombre;

                    objRPT.DesdeCuando = objReport.DesdeCuando;
                    objRPT.EstudioAlergista = objReport.EstudioAlergista;



                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewAlergia_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }



        private void GenerarReporterptViewValoracionFuncionalAM_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptView_ValoracionFuncionalAM_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptView_ValoracionFuncionalAM_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit>();
            listaRPT = getDatarptViewValoracionFuncionalAM_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));



            //datos generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            imgDF = Server.MapPath("imagenes/leyendaValoracion.JPG");
            Rpt.SetParameterValue("imgDF", imgDF);

            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.SetParameterValue("imgDF", imgDF);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                    Rpt.SetParameterValue("imgDF", imgDF);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetParameterValue("imgDF", imgDF);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);


        }
        private List<SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit>
        getDatarptViewValoracionFuncionalAM_FE(
          String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
          , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
          string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit>();

            List<rptView_ValoracionFuncionalAM_FE> rptViewValoracionFuncionalAM = new List<rptView_ValoracionFuncionalAM_FE>();
            SS_HC_ValoracionAM_FE objValoracionAM_FE = new SS_HC_ValoracionAM_FE();
            objValoracionAM_FE.UnidadReplicacion = unidadReplicacion;
            objValoracionAM_FE.IdPaciente = idPaciente;
            objValoracionAM_FE.EpisodioClinico = epiClinico;
            objValoracionAM_FE.IdEpisodioAtencion = idEpiAtencion;
            objValoracionAM_FE.Accion = "REPORTEA";
            rptViewValoracionFuncionalAM = ServiceReportes.ReporteValoracionAM_FE(objValoracionAM_FE, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit objRPT;
            if (rptViewValoracionFuncionalAM != null)
            {
                foreach (rptView_ValoracionFuncionalAM_FE objReport in rptViewValoracionFuncionalAM) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptView_ValoracionFuncionalAM_FEEdit();

                    objRPT.LD = objReport.LD;
                    objRPT.LI = objReport.LI;
                    objRPT.VD = objReport.VD;
                    objRPT.VI = objReport.VI;
                    objRPT.SHD = objReport.SHD;
                    objRPT.SHI = objReport.SHI;
                    objRPT.MD = objReport.MD;
                    objRPT.MI = objReport.MI;
                    objRPT.CD = objReport.CD;
                    objRPT.CI = objReport.CI;
                    objRPT.AD = objReport.AD;
                    objRPT.AI = objReport.AI;
                    objRPT.DiagnosticoFuncional = objReport.DiagnosticoFuncional;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewValoracionFuncionalAM.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }


        private void GenerarReporterptViewProximaAtencion_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewProximaAtencion_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewProximaAtencion_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit>();
            listaRPT = getDatarptViewProximaAtencion_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));

            //Datos Generales
            setDatosGenerales();


            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.SetParameterValue("imgDF", imgDF);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                    Rpt.SetParameterValue("imgDF", imgDF);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);


        }
        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit>
        getDatarptViewProximaAtencion_FE(
        String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
        , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
        string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit>();

            List<rptViewProximaAtencion_FE> rptViewProximaAtencion_FE = new List<rptViewProximaAtencion_FE>();
            SS_HC_ProximaAtencion_FE objValoracionAM_FE = new SS_HC_ProximaAtencion_FE();
            objValoracionAM_FE.UnidadReplicacion = unidadReplicacion;
            objValoracionAM_FE.IdPaciente = idPaciente;
            objValoracionAM_FE.EpisodioClinico = epiClinico;
            objValoracionAM_FE.IdEpisodioAtencion = idEpiAtencion;
            objValoracionAM_FE.Accion = "REPORTEA";
            rptViewProximaAtencion_FE = ServiceReportes.ReporteProximaAtencion_FE(objValoracionAM_FE, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit objRPT;
            if (rptViewProximaAtencion_FE != null)
            {
                foreach (rptViewProximaAtencion_FE objReport in rptViewProximaAtencion_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewProximaAtencion_FEEdit();

                    objRPT.FechaSolicitada = Convert.ToDateTime(objReport.FechaSolicitada);
                    objRPT.Descripcion = objReport.Descripcion;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewProximaAtencion_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }



        private void GenerarReporterptViewInmunizacionAdulto_FE(string tipovista)
        {
            // Reporte
            string tura = Server.MapPath("rptReports/rptViewInmunizacionAdultoRep_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewInmunizacionAdultoRep_FE.rpt"));

            DataTable listaRPT = new DataTable();

            listaRPT = rptVistas_FE("rptViewInmunizacionAdulto_FE",
                        ENTITY_GLOBAL.Instance.UnidadReplicacion,
                        (int)ENTITY_GLOBAL.Instance.PacienteID,
                        (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                        (long)ENTITY_GLOBAL.Instance.EpisodioAtencion,
                        null, 0,
                        ENTITY_GLOBAL.Instance.CONCEPTO,
                        ENTITY_GLOBAL.Instance.USUARIO);


            //Datos Generales
            setDatosGenerales();


            //List<SoluccionSalud.RepositoryReport.Entidades.rptViewInmunizacionAdulto_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewInmunizacionAdulto_FEEdit>();
            //listaRPT = getDatarptViewImnunizaionAdulto_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
            //    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipovista == "I")
                {

                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {

                        Rpt.ExportToHttpResponse
                       (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DIAGNOSTICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }



        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewInmunizacionAdulto_FEEdit>
        getDatarptViewImnunizaionAdulto_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
           SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, String codFormato, String codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewInmunizacionAdulto_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewInmunizacionAdulto_FEEdit>();
            List<rptViewInmunizacionAdulto_FE> rptViewImnunizaionAdulto = new List<rptViewInmunizacionAdulto_FE>();
            SS_HC_AntecedentesPersonalesIAdul_FE objImnunizaionAdulto = new SS_HC_AntecedentesPersonalesIAdul_FE();
            objImnunizaionAdulto.UnidadReplicacion = unidadReplicacion;
            objImnunizaionAdulto.IdPaciente = idPaciente;
            objImnunizaionAdulto.EpisodioClinico = epiClinico;
            objImnunizaionAdulto.IdEpisodioAtencion = idEpiAtencion;
            objImnunizaionAdulto.Accion = "REPORTEA";

            //Servicio
            rptViewImnunizaionAdulto = ServiceReportes.ReporteInmunizacionAdulto_FE(objImnunizaionAdulto, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewInmunizacionAdulto_FEEdit objRPT;

            if (rptViewImnunizaionAdulto != null)
            {
                foreach (rptViewInmunizacionAdulto_FE objReport in rptViewImnunizaionAdulto)
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewInmunizacionAdulto_FEEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;

                    objRPT.NO = objReport.NO;
                    objRPT.SI = objReport.SI;

                    objRPT.DPT_1era = objReport.DPT_1era;
                    objRPT.DPT_2da = objReport.DPT_2da;
                    objRPT.DPT_3era = objReport.DPT_3era;
                    objRPT.DPT_NoRecuerda = objReport.DPT_NoRecuerda;

                    objRPT.SRP_1era = objReport.SRP_1era;
                    objRPT.SRP_NoRecuerda = objReport.SRP_NoRecuerda;

                    objRPT.VARICELA_1era = objReport.VARICELA_1era;
                    objRPT.VARICELA_NoRecuerda = objReport.VARICELA_NoRecuerda;

                    objRPT.HEPATITISB_1era = objReport.HEPATITISB_1era;
                    objRPT.HEPATITISB_2da = objReport.HEPATITISB_2da;
                    objRPT.HEPATITISB_3era = objReport.HEPATITISB_3era;
                    objRPT.HEPATITISB_1erRef = objReport.HEPATITISB_1erRef;
                    objRPT.HEPATITISB_NoRecuerda = objReport.HEPATITISB_NoRecuerda;


                    objRPT.HEPATITISA_1era = objReport.HEPATITISA_1era;
                    objRPT.HEPATITISA_2da = objReport.HEPATITISA_2da;
                    objRPT.HEPATITISA_NoRecuerda = objReport.HEPATITISA_NoRecuerda;

                    objRPT.NEUMOCOCO_1era = objReport.NEUMOCOCO_1era;
                    objRPT.NEUMOCOCO_2da = objReport.NEUMOCOCO_2da;
                    objRPT.NEUMOCOCO_3era = objReport.NEUMOCOCO_3era;
                    objRPT.NEUMOCOCO_NoRecuerda = objReport.NEUMOCOCO_NoRecuerda;

                    objRPT.Antitetanica_1era = objReport.Antitetanica_1era;
                    objRPT.Antitetanica_2da = objReport.Antitetanica_2da;
                    objRPT.Antitetanica_3era = objReport.Antitetanica_3era;
                    objRPT.Antitetanica_NoRecuerda = objReport.Antitetanica_NoRecuerda;

                    objRPT.Papiloma_1era = objReport.Papiloma_1era;
                    objRPT.Papiloma_2da = objReport.Papiloma_2da;
                    objRPT.Papiloma_3era = objReport.Papiloma_3era;
                    objRPT.Papiloma_NoRecuerda = objReport.Papiloma_NoRecuerda;

                    objRPT.INFLUENZA = Convert.ToDateTime(objReport.INFLUENZA);

                    objRPT.Secuencia = Convert.ToInt32(objReport.Secuencia);
                    objRPT.OtrasVacunas = objReport.OtrasVacunas;




                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewImnunizaionAdulto.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }


        private void GenerarReporterptViewApoyoDiagnostico_FE(string tipovista)
        {
            // Reporte
            string tura = Server.MapPath("rptReports/rptViewApoyoDiagnosticoRep_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewApoyoDiagnosticoRep_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewApoyoDiagnostico_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewApoyoDiagnostico_FEEdit>();
            listaRPT = getDatarptViewApoyoDiagnostico_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);


            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");


            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);

            }
            else
            {
                if (tipovista == "I")
                {
                    if (tipovista == "I")
                    {
                        ReportViewer.ReportSource = Rpt;
                        ReportViewer.DataBind();
                    }
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {

                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "ApoyoDiagnostico");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }


        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewApoyoDiagnostico_FEEdit>
        getDatarptViewApoyoDiagnostico_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
           SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, String codFormato, String codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewApoyoDiagnostico_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewApoyoDiagnostico_FEEdit>();
            List<rptViewApoyoDiagnostico_FE> rptViewApoyoDiagnostico = new List<rptViewApoyoDiagnostico_FE>();
            SS_HC_ApoyoDiagnostico_FE objApoyoDiagnostico = new SS_HC_ApoyoDiagnostico_FE();
            objApoyoDiagnostico.UnidadReplicacion = unidadReplicacion;
            objApoyoDiagnostico.IdPaciente = idPaciente;
            objApoyoDiagnostico.EpisodioClinico = epiClinico;
            objApoyoDiagnostico.IdEpisodioAtencion = idEpiAtencion;
            objApoyoDiagnostico.Accion = "REPORTEA";

            //Servicio
            rptViewApoyoDiagnostico = ServiceReportes.ReporteApoyoDiagnostico_FE(objApoyoDiagnostico, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewApoyoDiagnostico_FEEdit objRPT;

            if (rptViewApoyoDiagnostico != null)
            {
                foreach (rptViewApoyoDiagnostico_FE objReport in rptViewApoyoDiagnostico)
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewApoyoDiagnostico_FEEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;

                    //                objRPT.Secuencia = Convert.ToInt32(objReport.Secuencia);
                    objRPT.NroInforme = objReport.NroInforme;
                    objRPT.FechaSolicitada = Convert.ToDateTime(objReport.FechaSolicitada);



                    objRPT.IdEspecialidad = Convert.ToInt32(objReport.IdEspecialidad);
                    objRPT.IdProcedimiento = Convert.ToInt32(objReport.IdProcedimiento);
                    objRPT.TipoOrdenAtencion = Convert.ToInt32(objReport.TipoOrdenAtencion);
                    objRPT.CodigoComponente = objReport.CodigoComponente;
                    //objRPT.IdDiagnostico = Convert.ToInt32(objReport.IdDiagnostico);
                    //objRPT.ProcedimientoText = objReport.ProcedimientoText;
                    //objRPT.DiagnosticoText = objReport.DiagnosticoText;
                    objRPT.Observacion = objReport.Observacion;

                    objRPT.Accion = objReport.Accion;
                    objRPT.Version = objReport.Version;
                    objRPT.Estado = Convert.ToInt32(objReport.Estado);
                    objRPT.UsuarioCreacion = objReport.UsuarioCreacion;
                    //objRPT.FechaCreacion = Convert.ToDateTime(objReport.FechaCreacion);
                    //objRPT.UsuarioModificacion = objReport.UsuarioModificacion;
                    //objRPT.FechaModificacion = Convert.ToDateTime(objReport.FechaModificacion);

                    objRPT.Nombre = objReport.Nombre;
                    objRPT.RutaInforme = objReport.RutaInforme;
                    objRPT.Diagnostico = objReport.Diagnostico;


                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewApoyoDiagnostico.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }

        //rptReferencia_FE
        private void GenerarReporterptViewReferencia_FE(string tipovista)
        {
            // Reporte
            string tura = Server.MapPath("rptReports/rptViewReferencia_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewReferencia_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewReferencia_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewReferencia_FEEdit>();

            listaRPT = getDatarptViewReferencia_FE("REPORTEA",
                        ENTITY_GLOBAL.Instance.UnidadReplicacion,
                        (int)ENTITY_GLOBAL.Instance.PacienteID,
                        (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                        (long)ENTITY_GLOBAL.Instance.EpisodioAtencion,
                        null, 0,
                        ENTITY_GLOBAL.Instance.CONCEPTO,
                        ENTITY_GLOBAL.Instance.USUARIO);

            //Datos Generales
            setDatosGenerales();


            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);

            }
            else
            {
                if (tipovista == "I")
                {
                    if (tipovista == "I")
                    {
                        ReportViewer.ReportSource = Rpt;
                        ReportViewer.DataBind();
                    }
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {

                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DIAGNOSTICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }
        //fin rpt referencia
        //inicio  < private List> referencia
        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewReferencia_FEEdit>
        getDatarptViewReferencia_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
        SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, String codFormato, String codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewReferencia_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewReferencia_FEEdit>();
            List<rptViewReferencia_FE> rptViewReferencia = new List<rptViewReferencia_FE>();
            SS_HC_Referencia_FE objReferencia = new SS_HC_Referencia_FE();
            objReferencia.UnidadReplicacion = unidadReplicacion;
            objReferencia.IdPaciente = idPaciente;
            objReferencia.EpisodioClinico = epiClinico;
            objReferencia.IdEpisodioAtencion = idEpiAtencion;
            objReferencia.Accion = "REPORTEA";

            //Servicio
            rptViewReferencia = ServiceReportes.ReporteReferencia_FE(objReferencia, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewReferencia_FEEdit objRPT;

            if (rptViewReferencia != null)
            {
                foreach (rptViewReferencia_FE objReport in rptViewReferencia)
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewReferencia_FEEdit();
                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.NroReferencia = objReport.NroReferencia;
                    objRPT.P_UNO = objReport.P_UNO;
                    objRPT.P_DOS = objReport.P_DOS;
                    objRPT.P_TRES = objReport.P_TRES;
                    objRPT.P_CUATRO = objReport.P_CUATRO;
                    objRPT.EstablecimientoOri = objReport.EstablecimientoOri;
                    objRPT.ServicioOri = objReport.ServicioOri;
                    objRPT.IdentificacionUsu = objReport.IdentificacionUsu;
                    objRPT.Anamnesis = objReport.Anamnesis;
                    objRPT.EstadoGeneral = objReport.EstadoGeneral;
                    objRPT.Glasgow = objReport.Glasgow;
                    objRPT.FV_T = objReport.FV_T;
                    objRPT.FV_PA = objReport.FV_PA;
                    objRPT.FV_FR = objReport.FV_FR;
                    objRPT.FV_FC = objReport.FV_FC;
                    objRPT.Exam_aux = objReport.Exam_aux;
                    objRPT.Motivo = objReport.Motivo;
                    objRPT.DR_EMERGENCIA = objReport.DR_EMERGENCIA;
                    objRPT.DR_CONSULTA_EXTERNA = objReport.DR_CONSULTA_EXTERNA;
                    objRPT.DR_HOSPITALIZACION = objReport.DR_HOSPITALIZACION;
                    objRPT.FechaReferencia = objReport.FechaReferencia;
                    objRPT.HoraReferencia = objReport.HoraReferencia;
                    objRPT.PersonaAtiende = objReport.PersonaAtiende;
                    objRPT.CS_ESTABLE = objReport.CS_ESTABLE;
                    objRPT.CS_INESTABLE = objReport.CS_INESTABLE;
                    objRPT.MedicoSanna = objReport.MedicoSanna;
                    objRPT.MedicoAtencion = objReport.MedicoAtencion;
                    objRPT.Respon_ref = objReport.Respon_ref;
                    objRPT.Colegiatura_ref = objReport.Colegiatura_ref;
                    objRPT.Respon_serv = objReport.Respon_serv;
                    objRPT.Colegiatura_ser = objReport.Colegiatura_ser;
                    objRPT.Respon_acomp = objReport.Respon_acomp;
                    objRPT.Colegiatura_acomp = objReport.Colegiatura_acomp;
                    objRPT.Respon_recibe = objReport.Respon_recibe;
                    objRPT.Colegiatura_recib = objReport.Colegiatura_recib;
                    objRPT.CLL_ESTABLE = objReport.CLL_ESTABLE;
                    objRPT.CLL_INESTABLE = objReport.CLL_INESTABLE;
                    objRPT.CLL_FALLECIDO = objReport.CLL_FALLECIDO;
                    objRPT.DIAGNOSTICO = objReport.DIAGNOSTICO;
                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewReferencia.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }

        //fin private List referencia
        private void GenerarReporterptViewExamenApoyoDiagnostico_FE(string tipovista)
        {
            // Reporte
            string tura = Server.MapPath("rptReports/rptViewExamenApoyoDiagnostico_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewExamenApoyoDiagnostico_FE.rpt"));
            //List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit>();
            //listaRPT = getDatarptViewExamenApoyoDiagnostico_FE("REPORTEA",
            //            ENTITY_GLOBAL.Instance.UnidadReplicacion,
            //            (int)ENTITY_GLOBAL.Instance.PacienteID,
            //            (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
            //            (long)ENTITY_GLOBAL.Instance.EpisodioAtencion,
            //            null, 0,
            //            ENTITY_GLOBAL.Instance.CONCEPTO,
            //            ENTITY_GLOBAL.Instance.USUARIO);

            DataTable listaRPT = new DataTable();
            listaRPT = rptVistas_FE("rptViewExamenApoyoDiagnostico_FE",
                         ENTITY_GLOBAL.Instance.UnidadReplicacion,
                         (int)ENTITY_GLOBAL.Instance.PacienteID,
                         (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                         (long)ENTITY_GLOBAL.Instance.EpisodioAtencion,
                         null, 0,
                         ENTITY_GLOBAL.Instance.CONCEPTO,
                         ENTITY_GLOBAL.Instance.USUARIO);

            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);

            }
            else
            {
                if (tipovista == "I")
                {
                    if (tipovista == "I")
                    {
                        ReportViewer.ReportSource = Rpt;
                        ReportViewer.DataBind();
                    }
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {

                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DIAGNOSTICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }
        //




        private void GenerarReporterptValoracionAM_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewValoracionAM_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewValoracionAM_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit>();
            listaRPT = getDatarptViewValoracionAM_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));


            //Datos Generales
            setDatosGenerales();


            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.SetParameterValue("imgDF", imgDF);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                    Rpt.SetParameterValue("imgDF", imgDF);
                }

            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);


        }

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit>
   getDatarptViewExamenApoyoDiagnostico_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
   SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, String codFormato, String codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit>();
            List<rptViewExamenApoyoDiagnostico_FE> rptViewExamenApoyoDiagnostico = new List<rptViewExamenApoyoDiagnostico_FE>();  //modificar
            SS_HC_ExamenSolicitadoFE objExApoyoDiag = new SS_HC_ExamenSolicitadoFE();   //modificar
            objExApoyoDiag.UnidadReplicacion = unidadReplicacion;
            objExApoyoDiag.IdPaciente = idPaciente;
            objExApoyoDiag.EpisodioClinico = epiClinico;
            objExApoyoDiag.IdEpisodioAtencion = idEpiAtencion;
            objExApoyoDiag.Accion = "REPORTEA";

            //Servicio
            rptViewExamenApoyoDiagnostico = ServiceReportes.ReporteExamenApoyoDiagnostico_FE(objExApoyoDiag, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit objRPT;

            if (rptViewExamenApoyoDiagnostico != null)
            {
                foreach (rptViewExamenApoyoDiagnostico_FE objReport in rptViewExamenApoyoDiagnostico)  //
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewExamenApoyoDiagnostico_FEEdit();
                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdPaciente = Convert.ToInt32(objReport.IdPaciente);
                    objRPT.EpisodioClinico = Convert.ToInt32(objReport.EpisodioClinico);
                    objRPT.IdEpisodioAtencion = Convert.ToInt32(objReport.IdEpisodioAtencion);
                    objRPT.Secuencia = Convert.ToInt32(objReport.Secuencia);
                    objRPT.Motivo = objReport.Motivo;
                    objRPT.FechaSolitada = Convert.ToDateTime(objReport.FechaSolicitada);
                    //   if (rptViewExamenApoyoDiagnostico != null){objRPT.IdTipoExamen = objReport.IdTipoExamen;}else{objRPT.IdTipoExamen = 0;}
                    objRPT.TipoExamen = objReport.TipoExamen;
                    objRPT.Examen = objReport.Examen;
                    objRPT.Cantidad = Convert.ToInt32(objReport.Cantidad);
                    objRPT.Especificaciones = objReport.Especificaciones;
                    objRPT.Observacion = objReport.Observacion;
                    objRPT.Diagnostico = objReport.Diagnostico;
                    objRPT.Resumen = objReport.Resumen;
                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewExamenApoyoDiagnostico.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }


        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit>
       getDatarptViewValoracionAM_FE(
      String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
      , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
      string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit>();

            List<rptViewValoracionAM_FE> rptViewValoracionAM_FE = new List<rptViewValoracionAM_FE>();
            SS_HC_ValoracionAM_FE objValoracionAM_FE = new SS_HC_ValoracionAM_FE();
            objValoracionAM_FE.UnidadReplicacion = unidadReplicacion;
            objValoracionAM_FE.IdPaciente = idPaciente;
            objValoracionAM_FE.EpisodioClinico = epiClinico;
            objValoracionAM_FE.IdEpisodioAtencion = idEpiAtencion;
            objValoracionAM_FE.Accion = "REPORTEA";
            rptViewValoracionAM_FE = ServiceReportes.rptViewValoracionAM_FE(objValoracionAM_FE, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit objRPT;
            if (rptViewValoracionAM_FE != null)
            {
                foreach (rptViewValoracionAM_FE objReport in rptViewValoracionAM_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionAM_FEEdit();

                    objRPT.S = objReport.S;
                    objRPT.E = objReport.E;
                    objRPT.F = objReport.F;
                    objRPT.G = objReport.G;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewValoracionAM_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }

        private void GenerarReporteValoracionMentalAM_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewValoracionMentalAM_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewValoracionMentalAM_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit>();
            listaRPT = getDatarptViewValoracionMentalAM_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));

            //Datos Generales
            setDatosGenerales();


            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            imgValor = Server.MapPath("imagenes/LeyendaValoracionCongnitivo.JPG");
            Rpt.SetParameterValue("imgValor", imgValor);



            imgEstado = Server.MapPath("imagenes/LeyendaEstadoAfectivo.JPG");
            Rpt.SetParameterValue("imgEstado", imgEstado);





            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.SetParameterValue("imgValor", imgValor);
                        Rpt.SetParameterValue("imgEstado", imgEstado);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                    Rpt.SetParameterValue("imgValor", imgValor);
                    Rpt.SetParameterValue("imgEstado", imgEstado);

                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetParameterValue("imgValor", imgValor);
            Rpt.SetParameterValue("imgEstado", imgEstado);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }



        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit>
       getDatarptViewValoracionMentalAM_FE(
      String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
      , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
      string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit>();

            List<rptViewValoracionMentalAM_FE> rptViewValoracionMentalAM_FE = new List<rptViewValoracionMentalAM_FE>();
            SS_HC_ValoracionMentalAM_FE objValoracionMAM_FE = new SS_HC_ValoracionMentalAM_FE();
            objValoracionMAM_FE.UnidadReplicacion = unidadReplicacion;
            objValoracionMAM_FE.IdPaciente = idPaciente;
            objValoracionMAM_FE.EpisodioClinico = epiClinico;
            objValoracionMAM_FE.IdEpisodioAtencion = idEpiAtencion;
            objValoracionMAM_FE.Accion = "REPORTEA";
            rptViewValoracionMentalAM_FE = ServiceReportes.rptViewValoracionMentalAM_FE(objValoracionMAM_FE, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit objRPT;
            if (rptViewValoracionMentalAM_FE != null)
            {
                foreach (rptViewValoracionMentalAM_FE objReport in rptViewValoracionMentalAM_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionMentalAM_FEEdit();

                    objRPT.Cualfecha = Convert.ToInt32(objReport.Cualfecha);
                    objRPT.QueDiasemana = Convert.ToInt32(objReport.QueDiasemana);
                    objRPT.EnquelugarEstamos = Convert.ToInt32(objReport.EnquelugarEstamos);
                    objRPT.CualNumerotelefono = Convert.ToInt32(objReport.CualNumerotelefono);
                    objRPT.CuantosAniostiene = Convert.ToInt32(objReport.CuantosAniostiene);
                    objRPT.DondeNacio = Convert.ToInt32(objReport.DondeNacio);
                    objRPT.NombrePresidente = Convert.ToInt32(objReport.NombrePresidente);
                    objRPT.NombrePresidenteAnterior = Convert.ToInt32(objReport.NombrePresidenteAnterior);
                    objRPT.ApellidoMadre = Convert.ToInt32(objReport.ApellidoMadre);
                    objRPT.Restar = Convert.ToInt32(objReport.Restar);
                    objRPT.DesganoDes = objReport.DesganoDes;
                    objRPT.ImpotenteDes = objReport.ImpotenteDes;
                    objRPT.MemoriaDes = objReport.MemoriaDes;
                    objRPT.SatisfechoDes = objReport.SatisfechoDes;
                    objRPT.ValorCognitiva = objReport.ValorCognitiva;
                    objRPT.EstadoAfectivo = objReport.EstadoAfectivo;
                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewValoracionMentalAM_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }


        private void GenerarReporteEvolucuionMedica_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewEvolucionMedica_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewEvolucionMedica_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit>();
            listaRPT = getDatarptViewEvolucionMedica_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));


            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }



        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit>
   getDatarptViewEvolucionMedica_FE(
  String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
  , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
  string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit>();

            List<rptViewEvolucionMedica_FE> rptViewEvolucionMedica_FE = new List<rptViewEvolucionMedica_FE>();
            SS_HC_EvolucionMedica_FE ObjEvolucionMedica_FE = new SS_HC_EvolucionMedica_FE();
            ObjEvolucionMedica_FE.UnidadReplicacion = unidadReplicacion;
            ObjEvolucionMedica_FE.IdPaciente = idPaciente;
            ObjEvolucionMedica_FE.EpisodioClinico = epiClinico;
            ObjEvolucionMedica_FE.IdEpisodioAtencion = idEpiAtencion;
            ObjEvolucionMedica_FE.Accion = "REPORTEA";
            rptViewEvolucionMedica_FE = ServiceReportes.rptViewEvolucionMedica_FE(ObjEvolucionMedica_FE, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit objRPT;
            if (rptViewEvolucionMedica_FE != null)
            {
                foreach (rptViewEvolucionMedica_FE objReport in rptViewEvolucionMedica_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewEvolucionMedica_FEEdit();

                    objRPT.FechaIngreso = Convert.ToDateTime(objReport.FechaIngreso);
                    objRPT.Hora = Convert.ToDateTime(objReport.Hora);
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.EvolucionObjetiva = objReport.EvolucionObjetiva;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewEvolucionMedica_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }




        private void GenerarReporteMedicamentos_Fe(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewMedicamentos_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewMedicamentos_FE.rpt"));
            DataTable listaRPT = new DataTable();
            DataTable listaRPT_Pac_Med = new DataTable();
            listaRPT = rptVistas_FE("rptViewMedicamentos_FE",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                ENTITY_GLOBAL.Instance.USUARIO);
            /*Datos Paciente -Medico*/
            listaRPT_Pac_Med = rptDatosPacienteMedico_FE("rptViewDatosPaciente_Medico",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                ENTITY_GLOBAL.Instance.USUARIO);

            Rpt.SetDataSource(listaRPT);
            DataSet obj = new DataSet();
            dsRptViewer.Tables.Add(objTabla1.Copy());
            dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit> listaRPT_1 = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>();
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit> listaRPT_2 = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>();

            listaRPT_1 = getDatarptViewMedicamentos_FE("REPORTEA",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                ENTITY_GLOBAL.Instance.USUARIO, 1);
            Rpt.Subreports["rptViewMedicamentos_FEDetalle1.rpt"].SetDataSource(listaRPT_1);
            //    Rpt.SetDataSource(listaRPT_1);

            listaRPT_2 = getDatarptViewMedicamentos_FE("REPORTEA",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                ENTITY_GLOBAL.Instance.USUARIO, 4);
            Rpt.Subreports["rptViewMedicamentos_FEDetalle2.rpt"].SetDataSource(listaRPT_2);

            Rpt.Subreports["rptViewMedicamentos_FEsubrepFirmas.rpt"].SetDataSource(listaRPT_Pac_Med);

            Rpt.SetDataSource(listaRPT);


            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            if (listaRPT_1.Count == 0 && listaRPT_2.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }


        //private void GenerarReporteMedicamentos_Fe_Detalle1(string tipoVista)
        //{
        //    string tura = Server.MapPath("rptReports/rptViewMedicamentos_FEDetalle1.rpt");
        //    Rpt.Load(Server.MapPath("rptReports/rptViewMedicamentos_FEDetalle1.rpt"));

        //    List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>();
        //    listaRPT = getDatarptViewMedicamentos_FE("REPORTEA",
        //        ENTITY_GLOBAL.Instance.UnidadReplicacion,
        //        (int)ENTITY_GLOBAL.Instance.PacienteID,
        //        (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
        //        (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
        //        , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
        //        ENTITY_GLOBAL.Instance.USUARIO,1);
        //    Rpt.SetDataSource(listaRPT);
        //    if (listaRPT.Count == 0)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true); }
        //    else
        //    {
        //        if (tipoVista == "I")
        //        {
        //            ReportViewer.ReportSource = Rpt;
        //            ReportViewer.DataBind();
        //        }
        //        else
        //        {
        //            Response.Buffer = false;
        //            Response.ClearContent();
        //            Response.ClearHeaders();
        //            try
        //            {
        //                Rpt.ExportToHttpResponse
        //                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AnteFamiliar");
        //            }
        //            catch (Exception ex)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    GenerarReporteMedicamentos_Fe_Detalle1(tipoVista);

        //}

        //private void GenerarReporteMedicamentos_Fe_Detalle2(string tipoVista)
        //{
        //    string tura = Server.MapPath("rptReports/rptViewMedicamentos_FEDetalle2.rpt");
        //    Rpt.Load(Server.MapPath("rptReports/rptViewMedicamentos_FEDetalle2.rpt"));

        //    List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>();
        //    listaRPT = getDatarptViewMedicamentos_FE("REPORTEA",
        //            ENTITY_GLOBAL.Instance.UnidadReplicacion,
        //            (int)ENTITY_GLOBAL.Instance.PacienteID,
        //            (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
        //            (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
        //            , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
        //            ENTITY_GLOBAL.Instance.USUARIO, 4);
        //    Rpt.SetDataSource(listaRPT);
        //    if (listaRPT.Count == 0)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true); }
        //    else
        //    {
        //        if (tipoVista == "I")
        //        {
        //            ReportViewer.ReportSource = Rpt;
        //            ReportViewer.DataBind();
        //        }
        //        else
        //        {
        //            Response.Buffer = false;
        //            Response.ClearContent();
        //            Response.ClearHeaders();
        //            try
        //            {
        //                Rpt.ExportToHttpResponse
        //                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AnteFamiliar");
        //            }
        //            catch (Exception ex)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    GenerarReporteMedicamentos_Fe_Detalle2(tipoVista);
        //}

        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>
   getDatarptViewMedicamentos_FE(
  String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
  , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
  string codFormato, string codUsuario, int tipomedicamento)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit>();

            List<rptViewMedicamentos_FE> rptViewMedicamentos_FE = new List<rptViewMedicamentos_FE>();
            SS_HC_Medicamento_FE ObjMedicamento_FE = new SS_HC_Medicamento_FE();
            ObjMedicamento_FE.UnidadReplicacion = unidadReplicacion;
            ObjMedicamento_FE.IdPaciente = idPaciente;
            ObjMedicamento_FE.EpisodioClinico = epiClinico;
            ObjMedicamento_FE.IdEpisodioAtencion = idEpiAtencion;
            ObjMedicamento_FE.Accion = "REPORTEA";
            rptViewMedicamentos_FE = ServiceReportes.rptViewMedicamentos_FE(ObjMedicamento_FE, 0, 0, tipomedicamento);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit objRPT;
            if (rptViewMedicamentos_FE != null)
            {
                foreach (rptViewMedicamentos_FE objReport in rptViewMedicamentos_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewMedicamentos_FEEdit();
                    objRPT.IdUnidadMedida = Convert.ToInt32(objReport.IdUnidadMedida);

                    if (objReport.Frecuencia != null)
                    {
                        objRPT.Frecuencia =/* Convert.ToInt32(*/(decimal)objReport.Frecuencia/*)*/;
                    }
                    else
                    {
                        objRPT.Frecuencia = Convert.ToInt32(objReport.Frecuencia);
                    }
                    objRPT.MED_DCI = objReport.MED_DCI;
                    objRPT.Comentario = objReport.Comentario;
                    objRPT.Presentacion = objReport.Presentacion;
                    objRPT.Dosis = objReport.Dosis;
                    objRPT.UnidMedDesc = objReport.UnidMedDesc;
                    objRPT.TipoComida = Convert.ToInt32(objReport.TipoComida);
                    objRPT.UndTiempoFre = objReport.UndTiempoFre;
                    objRPT.Periodo = objReport.Periodo;
                    objRPT.UndTiempoPeri = objReport.UndTiempoPeri;
                    objRPT.ViaDesc = objReport.ViaDesc;
                    objRPT.Cantidad = Convert.ToInt32(objReport.Cantidad);
                    objRPT.TipRecetaDes = objReport.TipRecetaDes;
                    objRPT.Indicacion = objReport.Indicacion;
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;
                    objRPT.Sexo = objReport.Sexo;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.DescripcionLarga = objReport.DescripcionLarga;
                    objRPT.DescripcionLocal = objReport.DescripcionLocal;
                    objRPT.DireccionComun = objReport.DireccionComun;
                    objRPT.DocumentoFiscal = objReport.DocumentoFiscal;
                    objRPT.FechaCreacion = Convert.ToDateTime(objReport.FechaCreacion);
                    objRPT.TITULAR = objReport.TITULAR;
                    objRPT.VIGENCIA = objReport.VIGENCIA;
                    objRPT.POLIZA = objReport.POLIZA;
                    objRPT.ASEGURADORA = objReport.ASEGURADORA;
                    objRPT.EMPLEADORA = objReport.EMPLEADORA;
                    objRPT.DCI = objReport.DCI;
                    objRPT.Nombre = objReport.Nombre;
                    objRPT.DiagnosticoDesc = objReport.DiagnosticoDesc;
                    objRPT.UsuarioAuditoria = objReport.UsuarioAuditoria;
                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewMedicamentos_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }


        private void GenerarReporteInterconsulta_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewInterconsulta_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewInterconsulta_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit>();
            listaRPT = getDatarptViewInterconsulta_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));

            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }



        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit>
   getDatarptViewInterconsulta_FE(
 String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
  , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
  string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit>();

            List<rptViewInterconsulta_FE> rptViewInterconsulta_FE = new List<rptViewInterconsulta_FE>();
            SS_HC_InterConsulta_FE ObjMedicamento_FE = new SS_HC_InterConsulta_FE();
            ObjMedicamento_FE.UnidadReplicacion = unidadReplicacion;
            ObjMedicamento_FE.IdPaciente = idPaciente;
            ObjMedicamento_FE.EpisodioClinico = epiClinico;
            ObjMedicamento_FE.IdEpisodioAtencion = idEpiAtencion;
            ObjMedicamento_FE.Accion = "REPORTEA";
            rptViewInterconsulta_FE = ServiceReportes.rptViewInterconsulta_FE(ObjMedicamento_FE, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit objRPT;
            if (rptViewInterconsulta_FE != null)
            {
                foreach (rptViewInterconsulta_FE objReport in rptViewInterconsulta_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewInterconsulta_FEEdit();


                    objRPT.diagnostico = objReport.diagnostico;
                    objRPT.DiagnosticoText = objReport.DiagnosticoText;
                    objRPT.FechaSolicitada = Convert.ToDateTime(objReport.FechaSolicitada);
                    objRPT.EspecialidadDesc = objReport.EspecialidadDesc;
                    objRPT.FechaPlaneada = Convert.ToDateTime(objReport.FechaPlaneada);
                    objRPT.CodigoComponente = objReport.CodigoComponente;
                    objRPT.Observacion = objReport.Observacion;
                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewInterconsulta_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }





        private void GenerarReporteValoracionSocioFamAM_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewValoracionSocioFamAM_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewValoracionSocioFamAM_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit>();
            listaRPT = getDatarptViewValoracionSocioFamAM_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));

            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            imgValoracionSocio = Server.MapPath("Imagen/LeyendaEstadoSocio.JPG");
            Rpt.SetParameterValue("imgValoracionSocio", imgValoracionSocio);

            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.SetParameterValue("imgValoracionSocio", imgValoracionSocio);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                    Rpt.SetParameterValue("imgValoracionSocio", imgValoracionSocio);

                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetParameterValue("imgValoracionSocio", imgValoracionSocio);
            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }



        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit>
        getDatarptViewValoracionSocioFamAM_FE(
        String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
        , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
        string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit>();

            List<rptViewValoracionSocioFamAM_FE> rptViewValoracionSocioFamAM_FE = new List<rptViewValoracionSocioFamAM_FE>();
            SS_HC_ValoracionSocioFamAM_FE ObjValoracionSocioAM_FE = new SS_HC_ValoracionSocioFamAM_FE();
            ObjValoracionSocioAM_FE.UnidadReplicacion = unidadReplicacion;
            ObjValoracionSocioAM_FE.IdPaciente = idPaciente;
            ObjValoracionSocioAM_FE.EpisodioClinico = epiClinico;
            ObjValoracionSocioAM_FE.IdEpisodioAtencion = idEpiAtencion;
            ObjValoracionSocioAM_FE.Accion = "REPORTEA";
            rptViewValoracionSocioFamAM_FE = ServiceReportes.rptViewValoracionSocioFamAM_FE(ObjValoracionSocioAM_FE, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit objRPT;
            if (rptViewValoracionSocioFamAM_FE != null)
            {
                foreach (rptViewValoracionSocioFamAM_FE objReport in rptViewValoracionSocioFamAM_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewValoracionSocioFamAM_FEEdit();


                    objRPT.V1 = objReport.V1;
                    objRPT.V2 = objReport.V2;
                    objRPT.V3 = objReport.V3;
                    objRPT.V4 = objReport.V4;
                    objRPT.V5 = objReport.V5;

                    objRPT.ARS1 = objReport.ARS1;
                    objRPT.ARS2 = objReport.ARS2;
                    objRPT.ARS3 = objReport.ARS3;
                    objRPT.ARS4 = objReport.ARS4;
                    objRPT.ARS5 = objReport.ARS5;

                    objRPT.RS1 = objReport.RS1;
                    objRPT.RS2 = objReport.RS2;
                    objRPT.RS3 = objReport.RS3;
                    objRPT.RS4 = objReport.RS4;
                    objRPT.RS5 = objReport.RS5;


                    objRPT.SE1 = objReport.SE1;
                    objRPT.SE2 = objReport.SE2;
                    objRPT.SE3 = objReport.SE3;
                    objRPT.SE4 = objReport.SE4;
                    objRPT.SE5 = objReport.SE5;


                    objRPT.SS1 = objReport.SS1;
                    objRPT.SS2 = objReport.SS2;
                    objRPT.SS3 = objReport.SS3;
                    objRPT.SS4 = objReport.SS4;
                    objRPT.SS5 = objReport.SS5;

                    objRPT.Valoracion = objReport.Valoracion;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewValoracionSocioFamAM_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }


        private void GenerarReporteAnamnesis_ANTFAM_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewAnamnesis_ANTFAM_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewAnamnesis_ANTFAM_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit>();

            listaRPT = getDatarptViewAnamnesis_ANTFAM_FE("REPORTEA",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                ENTITY_GLOBAL.Instance.USUARIO);

            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {

                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AnteFamiliar");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);




        }



        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit>
        getDatarptViewAnamnesis_ANTFAM_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
            SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit>();
            List<rptViewAnamnesis_AFAM_FE> rptViewAnamnesis_AFAM = new List<rptViewAnamnesis_AFAM_FE>();
            SS_HC_Anamnesis_AFAM_CAB_FE ObjAnamnesis_AFAM_CAB = new SS_HC_Anamnesis_AFAM_CAB_FE();
            ObjAnamnesis_AFAM_CAB.UnidadReplicacion = unidadReplicacion;
            ObjAnamnesis_AFAM_CAB.IdPaciente = idPaciente;
            ObjAnamnesis_AFAM_CAB.EpisodioClinico = epiClinico;
            ObjAnamnesis_AFAM_CAB.IdEpisodioAtencion = idEpiAtencion;
            ObjAnamnesis_AFAM_CAB.Accion = "REPORTEA";

            //Servicio
            rptViewAnamnesis_AFAM = ServiceReportes.rptViewAnamnesis_AFAM_FE(ObjAnamnesis_AFAM_CAB, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit objRPT;
            if (rptViewAnamnesis_AFAM != null)
            {
                foreach (rptViewAnamnesis_AFAM_FE objReport in rptViewAnamnesis_AFAM) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewAnamnesis_AFAM_FEEdit();


                    objRPT.AntecedenteFami_flag = objReport.AntecedenteFami_flag;
                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    //    objRPT.IdTipoParentesco = Convert.ToInt32(objReport.IdTipoParentesco);                    
                    if (objReport.Edad != null)
                    {
                        objRPT.Edad = Convert.ToInt32(objReport.Edad);
                    }

                    //    objRPT.IdVivo = objReport.IdVivo;
                    //objRPT.Estado = objReport.Estado;
                    objRPT.UsuarioCreacion = objReport.UsuarioCreacion;
                    //objRPT.FechaCreacion = objReport.FechaCreacion;
                    //objRPT.UsuarioModificacion = objReport.UsuarioModificacion;
                    //objRPT.FechaModificacion = objReport.FechaModificacion;
                    objRPT.Accion = objReport.Accion;
                    objRPT.Version = objReport.Version;
                    objRPT.Expr1 = Convert.ToInt32(objReport.Expr1);
                    objRPT.Expr103 = objReport.Expr103; // Grupo
                    objRPT.Secuencia = objReport.Secuencia;
                    objRPT.IdDiagnostico = objReport.IdDiagnostico;
                    objRPT.Observaciones = objReport.Observaciones;
                    objRPT.IDAntecedentePat = Convert.ToInt32(objReport.IDAntecedentePat);
                    objRPT.CodigoAntecedentePat = objReport.CodigoAntecedentePat;
                    objRPT.Descripcion = objReport.Descripcion;
                    objRPT.Adicional1 = objReport.Adicional1;
                    objRPT.Adicional2 = objReport.Adicional2;
                    //objRPT.Expr2 = objReport.Expr2;
                    //objRPT.Expr4 = objReport.Expr4;
                    //objRPT.Expr5 = objReport.Expr5;
                    //objRPT.Expr6 = objReport.Expr6;
                    //objRPT.IdDiagnosticoPadre = objReport.IdDiagnosticoPadre;
                    //objRPT.Orden = objReport.Orden;
                    objRPT.Expr2 = Convert.ToInt32(objReport.Expr2);
                    //objRPT.Nivel = objReport.Nivel;
                    objRPT.Expr5 = Convert.ToDateTime(objReport.Expr5);
                    //objRPT.IndicadorPermitido = objReport.IndicadorPermitido;
                    //objRPT.tipoFolder = objReport.tipoFolder;
                    objRPT.Expr6 = objReport.Expr6;
                    objRPT.ApellidoPaterno = objReport.ApellidoPaterno;
                    objRPT.ApellidoMaterno = objReport.ApellidoMaterno;
                    objRPT.Nombres = objReport.Nombres;
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.Busqueda = objReport.Busqueda;
                    objRPT.TipoDocumento = objReport.TipoDocumento;
                    objRPT.Documento = objReport.Documento;
                    //objRPT.FechaNacimiento = objReport.FechaNacimiento;
                    objRPT.Sexo = objReport.Sexo;
                    objRPT.EstadoCivil = objReport.EstadoCivil;
                    //objRPT.PersonaEdad = objReport.PersonaEdad;
                    //objRPT.IdOrdenAtencion = objReport.IdOrdenAtencion;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    //objRPT.LineaOrdenAtencion = objReport.LineaOrdenAtencion;
                    //objRPT.TipoOrdenAtencion = objReport.TipoOrdenAtencion;
                    //objRPT.TipoAtencion = objReport.TipoAtencion;
                    objRPT.TipoTrabajador = objReport.TipoTrabajador;
                    //objRPT.IdEstablecimientoSalud = objReport.IdEstablecimientoSalud;
                    //objRPT.IdUnidadServicio = objReport.IdUnidadServicio;
                    //objRPT.IdPersonalSalud = objReport.IdPersonalSalud;
                    //objRPT.FechaRegistro = objReport.FechaRegistro;
                    //objRPT.FechaAtencion = objReport.FechaAtencion;
                    //objRPT.IdEspecialidad = objReport.IdEspecialidad;
                    //objRPT.IdTipoOrden = objReport.IdTipoOrden;
                    //objRPT.estadoEpiAtencion = objReport.estadoEpiAtencion;
                    objRPT.Expr102 = objReport.Expr102;
                    objRPT.TipoAtencionDesc = objReport.TipoAtencionDesc;
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;
                    objRPT.EstablecimientoCodigo = objReport.EstablecimientoCodigo;
                    objRPT.EstablecimientoDesc = objReport.EstablecimientoDesc;
                    objRPT.UnidadServicioCodigo = objReport.UnidadServicioCodigo;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.PersMedicoNombreDocumento = objReport.PersMedicoNombreDocumento;
                    objRPT.Expr104 = objReport.Expr104; //Especialiadad
                    objRPT.EspecialidadCodigo = objReport.EspecialidadCodigo;
                    objRPT.EspecialidadDesc = objReport.EspecialidadDesc;
                    objRPT.Expr101 = objReport.Expr101; // Parentesco
                    objRPT.Expr3 = objReport.Expr3;
                    objRPT.PersonaEdad = Convert.ToInt32(objReport.PersonaEdad);

                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewAnamnesis_AFAM.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 

            }
            return listaRPT;

        }
        private void GenerarReporteContrarReferencia_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewContrarReferencia_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewContrarReferencia_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewContrarReferencia_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewContrarReferencia_FEEdit>();
            listaRPT = getDatarptViewContrarReferencia_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));

            //Datos Generales
            setDatosGenerales();


            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }



        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewContrarReferencia_FEEdit>
   getDatarptViewContrarReferencia_FE(
 String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
  , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
  string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewContrarReferencia_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewContrarReferencia_FEEdit>();

            List<rptViewContrarReferencia_FE> rptViewContrarReferencia_FE = new List<rptViewContrarReferencia_FE>();
            SS_HC_CONTRARREFERENCIA_FE ObjValoracionSocioAM_FE = new SS_HC_CONTRARREFERENCIA_FE();
            ObjValoracionSocioAM_FE.UnidadReplicacion = unidadReplicacion;
            ObjValoracionSocioAM_FE.IdPaciente = idPaciente;
            ObjValoracionSocioAM_FE.EpisodioClinico = epiClinico;
            ObjValoracionSocioAM_FE.IdEpisodioAtencion = idEpiAtencion;
            ObjValoracionSocioAM_FE.Accion = "REPORTEA";
            rptViewContrarReferencia_FE = ServiceReportes.rptViewContrarReferencia_FE(ObjValoracionSocioAM_FE, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewContrarReferencia_FEEdit objRPT;
            if (rptViewContrarReferencia_FE != null)
            {
                foreach (rptViewContrarReferencia_FE objReport in rptViewContrarReferencia_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewContrarReferencia_FEEdit();


                    objRPT.CalificacionJ = objReport.CalificacionJ;
                    objRPT.CalificacionNJ = objReport.CalificacionNJ;
                    objRPT.CMP = objReport.CMP;
                    objRPT.CPA = objReport.CPA;
                    objRPT.CPC = objReport.CPC;
                    objRPT.CPD = objReport.CPD;
                    objRPT.CPF = objReport.CPF;
                    objRPT.CPM = objReport.CPM;
                    objRPT.CPR = objReport.CPR;
                    objRPT.DiagnosticoEG = objReport.DiagnosticoEG;
                    objRPT.DiagnosticoIN = objReport.DiagnosticoIN;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.EspecialidadDesc = objReport.EspecialidadDesc;
                    objRPT.EstablecimientoDestino = objReport.EstablecimientoDestino;
                    objRPT.establecimientoOrigen = objReport.establecimientoOrigen;
                    objRPT.FechaContrarreferencia = Convert.ToDateTime(objReport.FechaContrarreferencia);
                    objRPT.FechaEgreso = Convert.ToDateTime(objReport.FechaEgreso);
                    objRPT.FechaIngreso = Convert.ToDateTime(objReport.FechaIngreso);
                    objRPT.HoraContrarreferencia = Convert.ToDateTime(objReport.HoraContrarreferencia);
                    objRPT.IdentificacionUsuario = objReport.IdentificacionUsuario;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.NroContrarreferencia = Convert.ToInt64(objReport.NroContrarreferencia);
                    objRPT.OrigenA = objReport.OrigenA;
                    objRPT.OrigenD = objReport.OrigenD;
                    objRPT.OrigenE = objReport.OrigenE;
                    objRPT.ProcedimientosRealizados = objReport.ProcedimientosRealizados;
                    objRPT.Recomendaciones = objReport.Recomendaciones;
                    objRPT.ServicioDestino = objReport.ServicioDestino;
                    objRPT.servicioOrigen = objReport.servicioOrigen;
                    objRPT.TratamientoRealizados = objReport.TratamientoRealizados;
                    objRPT.UPSA = objReport.UPSA;
                    objRPT.UPSC = objReport.UPSC;
                    objRPT.UPSE = objReport.UPSE;
                    objRPT.UPSH = objReport.UPSH;




                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewContrarReferencia_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }


        private void GenerarReporteSolicitudTransfusional_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewSolucitud_Transfusional_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewSolucitud_Transfusional_FE.rpt"));
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudTransfusional_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudTransfusional_FEEdit>();
            listaRPT = getDatarptViewSolicitudTransfusional_FE("REPORTEA",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO);


            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {

                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AnteFamiliar");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);




        }



        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudTransfusional_FEEdit>
        getDatarptViewSolicitudTransfusional_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
            SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudTransfusional_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudTransfusional_FEEdit>();
            List<rptViewSolicitudTransfusional_FE> rptViewSolicitudTranfusional = new List<rptViewSolicitudTransfusional_FE>();
            SS_HC_SolucitudTransfusional_FE ObjSolicitudTransfucional = new SS_HC_SolucitudTransfusional_FE();
            ObjSolicitudTransfucional.UnidadReplicacion = unidadReplicacion;
            ObjSolicitudTransfucional.IdPaciente = idPaciente;
            ObjSolicitudTransfucional.EpisodioClinico = epiClinico;
            ObjSolicitudTransfucional.IdEpisodioAtencion = idEpiAtencion;
            ObjSolicitudTransfucional.Accion = "REPORTEA";

            //Servicio
            rptViewSolicitudTranfusional = ServiceReportes.rptViewSolicitudTranfusional_FE(ObjSolicitudTransfucional, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudTransfusional_FEEdit objRPT;
            if (rptViewSolicitudTranfusional != null)
            {
                foreach (rptViewSolicitudTransfusional_FE objReport in rptViewSolicitudTranfusional) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudTransfusional_FEEdit();
                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.FechaSolicitud = Convert.ToDateTime(objReport.FechaSolicitud);
                    objRPT.HoraRecepcion = objReport.HoraRecepcion;
                    objRPT.Nombres_Paciente = objReport.Nombres_Paciente;
                    objRPT.Sexo_Paciente = objReport.Sexo_Paciente;
                    objRPT.Edad_paciente = Convert.ToInt32(objReport.Edad_paciente);
                    objRPT.CodigoHC = objReport.CodigoHC;
                    objRPT.Nro_cama = objReport.Nro_cama;
                    objRPT.UnidadServicioCodigo = objReport.UnidadServicioCodigo;
                    objRPT.UnidadServicioDesc = objReport.UnidadServicioDesc;
                    objRPT.TransfusionesPrevias = objReport.TransfusionesPrevias;
                    objRPT.ReaccionesTransfusionalesAnteriores = objReport.ReaccionesTransfusionalesAnteriores;
                    objRPT.EmbarazosPrevios = objReport.EmbarazosPrevios;
                    objRPT.EmbarazosPreviosEspecificar = objReport.EmbarazosPreviosEspecificar;
                    objRPT.Abortos = objReport.Abortos;
                    objRPT.AbortosEspecificar = objReport.AbortosEspecificar;
                    objRPT.IncompatMaternoFetal = objReport.IncompatMaternoFetal;
                    objRPT.IncompatMaternoFetalEspecificar = objReport.IncompatMaternoFetalEspecificar;
                    objRPT.DiagnosticoEnfermedad = objReport.DiagnosticoEnfermedad;
                    objRPT.Hb = Convert.ToDecimal(objReport.Hb);
                    objRPT.Hcto = Convert.ToDecimal(objReport.Hcto);
                    objRPT.Plaquetas = Convert.ToDecimal(objReport.Plaquetas);
                    objRPT.SangreTotalFlag = objReport.SangreTotalFlag;
                    objRPT.SangreTotalCantidad = Convert.ToDecimal(objReport.SangreTotalCantidad);
                    objRPT.FraccionPediatricasCantidad = Convert.ToDecimal(objReport.FraccionPediatricasCantidad);
                    objRPT.FraccionPediatricasFlag = objReport.FraccionPediatricasFlag;
                    objRPT.PaqueteGlobularFlag = objReport.PaqueteGlobularFlag;
                    objRPT.PaqueteGlobularCantidad = Convert.ToDecimal(objReport.PaqueteGlobularCantidad);
                    objRPT.RequerimientoEspecialFlag = objReport.RequerimientoEspecialFlag;
                    objRPT.PlasmaFrescoCongeladoFlag = objReport.PlasmaFrescoCongeladoFlag;
                    objRPT.PlasmaFrescoCongeladoCantidad = Convert.ToDecimal(objReport.PlasmaFrescoCongeladoCantidad);
                    objRPT.DesleucocitadoCantidad = Convert.ToDecimal(objReport.DesleucocitadoCantidad);
                    objRPT.DesleucocitadoFlag = objReport.RequerimientoEspecialFlag;
                    objRPT.CrioprecipitadoFlag = objReport.CrioprecipitadoFlag;
                    objRPT.CrioprecipitadoCantidad = Convert.ToDecimal(objReport.CrioprecipitadoCantidad);
                    objRPT.IrradiadoCantidad = Convert.ToDecimal(objReport.IrradiadoCantidad);
                    objRPT.IrradiadoFlag = objReport.IrradiadoFlag;
                    objRPT.PlaquetasFlag = objReport.PlaquetasFlag;
                    objRPT.PlaquetasCantidad = Convert.ToDecimal(objReport.PlaquetasCantidad);
                    objRPT.OtrosCantidad = Convert.ToDecimal(objReport.OtrosCantidad);
                    objRPT.OtrosEspecificar = objReport.OtrosEspecificar;
                    objRPT.OtrosFlag = objReport.OtrosFlag;
                    objRPT.Requisito = objReport.Requisito;
                    objRPT.PersonaBanco = objReport.PersonaBanco;
                    objRPT.FechaRecepcion = objReport.FechaRecepcion;
                    objRPT.HoraRecepcion = objReport.HoraRecepcion;
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.Expr104 = objReport.Expr104;
                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewSolicitudTranfusional.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }

        private void GenerarReporteDieta_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewDieta_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewDieta_FE.rpt"));
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit> listaRPT1 = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>();
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit> listaRPT2 = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>();
            listaRPT1 = getDatarptViewDieta_FE("REPORTEA",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO, 2);
            Rpt.Subreports["rptViewDieta_FEDetalle1.rpt"].SetDataSource(listaRPT1);
            Rpt.SetDataSource(listaRPT1);

            listaRPT2 = getDatarptViewDieta_FE("REPORTEA",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO, 3);
            Rpt.SetDataSource(listaRPT2);
            Rpt.Subreports["rptViewDieta_FEDetalle2.rpt"].SetDataSource(listaRPT2);

            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);



            if (listaRPT1.Count == 0 && listaRPT2.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AnteFamiliar");
                    }
                    catch (Exception ex) { throw; }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }

        private void GenerarReporteDieta_FEDetalle(string tipoVista)
        {


            string tura = Server.MapPath("rptReports/rptViewDieta_FEDetalle2.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewDieta_FEDetalle2.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>();
            listaRPT = getDatarptViewDieta_FE("REPORTEA",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO, 3);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true); }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AnteFamiliar");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            GenerarReporteDieta_FE(tipoVista);

        }

        private void GenerarReporteDieta_FEDetalle2(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewDieta_FEDetalle1.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewDieta_FEDetalle1.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>();
            listaRPT = getDatarptViewDieta_FE("REPORTEA",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO, 2);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true); }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AnteFamiliar");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            GenerarReporteDieta_FE(tipoVista);
        }


        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>
  getDatarptViewDieta_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
      SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, string codFormato, string codUsuario, int tipomedicamento)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit>();
            List<rptViewDieta_FE> rptViewDieta_FE = new List<rptViewDieta_FE>();
            SS_HC_Medicamento_FE ObjDieta = new SS_HC_Medicamento_FE();
            ObjDieta.UnidadReplicacion = unidadReplicacion;
            ObjDieta.IdPaciente = idPaciente;
            ObjDieta.EpisodioClinico = epiClinico;
            ObjDieta.IdEpisodioAtencion = idEpiAtencion;
            ObjDieta.Accion = "REPORTEA";

            // Servicio
            rptViewDieta_FE = ServiceReportes.rptViewDieta_FE(ObjDieta, 0, 0, tipomedicamento);

            objTabla1 = new System.Data.DataTable();
            SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit objRPT;
            if (rptViewDieta_FE != null)
            {
                foreach (rptViewDieta_FE objReport in rptViewDieta_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewDieta_FEEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.Presentacion = objReport.Presentacion;
                    objRPT.DescripcionLarga = objReport.DescripcionLarga;
                    objRPT.DescripcionLocal = objReport.DescripcionLocal;
                    objRPT.DireccionComun = objReport.DireccionComun;
                    objRPT.DocumentoFiscal = objReport.DocumentoFiscal;
                    objRPT.DietaMedicamento = objReport.DietaMedicamento;
                    objRPT.ViaDescDieta = objReport.ViaDescDieta;
                    objRPT.VolumenDia = objReport.VolumenDia;
                    objRPT.FrecuenciaToma = objReport.FrecuenciaToma;
                    objRPT.Hora = Convert.ToDateTime(objReport.Hora);
                    //    objRPT.HoraInicio = objReport.HoraInicio;
                    objRPT.Comentario = objReport.Comentario;
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.EspecialidadDesc = objReport.EspecialidadDesc;
                    objRPT.CMP = objReport.CMP;
                    objRPT.RNE = objReport.RNE;
                    objRPT.Documento = objReport.Documento;
                    objRPT.ComentarioDieta = objReport.ComentarioDieta;

                    //detalle2

                    objRPT.DosisComplementoDieta = objReport.DosisComplementoDieta;
                    objRPT.MicroNutriente = objReport.MicroNutriente;
                    objRPT.DCI = objReport.DCI;
                    objRPT.ViaDesc = objReport.ViaDesc;
                    objRPT.ComentarioComplementoDieta = objReport.ComentarioComplementoDieta;

                    //detalle1 - Dieta
                    objRPT.PadreDescripcion = objReport.PadreDescripcion;
                    objRPT.HijoDescripcion = objReport.HijoDescripcion;

                    listaRPT.Add(objRPT);
                }


                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewDieta_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }




        /*************************************************************************************************/


        private void GenerarReporterptViewSolicitudProducto(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewSolicitudProducto.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewSolicitudProducto.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudProductoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudProductoEdit>();
            listaRPT = getDatarptViewSolicitudProducto("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }





        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudProductoEdit>
   getDatarptViewSolicitudProducto(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
  , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
  string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudProductoEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudProductoEdit>();

            List<rptViewSolicitudProducto> rptViewSolicitudProducto = new List<rptViewSolicitudProducto>();
            SS_FA_SolicitudProducto ObjSolicitudProducto = new SS_FA_SolicitudProducto();
            ObjSolicitudProducto.UnidadReplicacion = unidadReplicacion;
            ObjSolicitudProducto.IdPaciente = idPaciente;
            ObjSolicitudProducto.EpisodioClinico = epiClinico;
            ObjSolicitudProducto.IdEpisodioAtencion = idEpiAtencion;
            ObjSolicitudProducto.Accion = "REPORTEA";
            rptViewSolicitudProducto = ServiceReportes.ReporteSolicitudProducto(ObjSolicitudProducto, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudProductoEdit objRPT;
            if (rptViewSolicitudProducto != null)
            {
                foreach (rptViewSolicitudProducto objReport in rptViewSolicitudProducto) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewSolicitudProductoEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.IdSolicitudProducto = objReport.IdSolicitudProducto;
                    objRPT.NumeroDocumento = objReport.NumeroDocumento;
                    objRPT.Observacion = objReport.Observacion;
                    objRPT.Secuencia = objReport.Secuencia;
                    objRPT.Cantidad = Convert.ToDecimal(objReport.Cantidad);
                    objRPT.Linea = objReport.Linea;
                    objRPT.Familia = objReport.Familia;
                    objRPT.SubFamilia = objReport.SubFamilia;
                    objRPT.CodigoComponente = objReport.CodigoComponente;
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.CodigoOA = objReport.CodigoOA;
                    objRPT.FechaCreacion = Convert.ToDateTime(objReport.FechaCreacion);
                    objRPT.PersMedicoNombreCompleto = objReport.PersMedicoNombreCompleto;
                    objRPT.TipoTrabajadorDesc = objReport.TipoTrabajadorDesc;
                    objRPT.Medicamento = objReport.Medicamento;


                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewSolicitudProducto.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }



        //***ANT.PER. FISIO
        private void GenerarReporterptViewAntecedentesPersonalesFisiologico(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewAntecedentesPersonalesFisiologicos_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewAntecedentesPersonalesFisiologicos_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit>();
            listaRPT = getDatarptViewAntecedenteFisiologico_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);


            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }
        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit>
        getDatarptViewAntecedenteFisiologico_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
       , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
       string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit>();

            List<rptViewAntecedentesPersonalesFisiologicos_FE> rptViewAnteFisiologico = new List<rptViewAntecedentesPersonalesFisiologicos_FE>();
            SS_HC_AntecedentesPersonalesFisiologicos_FE ObjAnteFisiologico = new SS_HC_AntecedentesPersonalesFisiologicos_FE();
            ObjAnteFisiologico.UnidadReplicacion = unidadReplicacion;
            ObjAnteFisiologico.IdPaciente = idPaciente;
            ObjAnteFisiologico.EpisodioClinico = epiClinico;
            ObjAnteFisiologico.IdEpisodioAtencion = idEpiAtencion;
            ObjAnteFisiologico.Accion = "REPORTEA";
            rptViewAnteFisiologico = ServiceReportes.rptViewAntecedentesFisiologicos_FE(ObjAnteFisiologico, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit objRPT;
            if (rptViewAnteFisiologico != null)
            {
                foreach (rptViewAntecedentesPersonalesFisiologicos_FE objReport in rptViewAnteFisiologico) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesFisiologicos_FEEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.IdSecuencia = objReport.IdSecuencia;
                    objRPT.GrupoSanguineo = objReport.GrupoSanguineo;
                    objRPT.FactorRH = objReport.FactorRH;
                    objRPT.AlimentacionA_flag = objReport.AlimentacionA_flag;
                    objRPT.Alcohol = objReport.Alcohol;
                    objRPT.Alcohol_EspecificarCantidad = objReport.Alcohol_EspecificarCantidad;
                    objRPT.Tabaco_flag = objReport.Tabaco_flag;
                    objRPT.Tabaco_NroCigarrillos = objReport.Tabaco_NroCigarrillos;
                    objRPT.TiempoConsumo = objReport.TiempoConsumo;
                    objRPT.Drogas_flag = objReport.Drogas_flag;
                    objRPT.Drogas_Especificar = objReport.Drogas_Especificar;
                    objRPT.Cafe_flag = objReport.Cafe_flag;
                    objRPT.Otros = objReport.Otros;
                    objRPT.ActividadFisica_flag = objReport.ActividadFisica_flag;
                    objRPT.ActividadFisica_subflag = objReport.ActividadFisica_subflag;
                    objRPT.ConsumoVerduras_flag = objReport.ConsumoVerduras_flag;
                    objRPT.ConsumoVerduras_subflag = objReport.ConsumoVerduras_subflag;
                    objRPT.ConsumoFrutas_flag = objReport.ConsumoFrutas_flag;
                    objRPT.ConsumoFrutas_subflag = objReport.ConsumoFrutas_subflag;
                    objRPT.InmunizacionesAdultoObservaciones = objReport.InmunizacionesAdultoObservaciones;
                    objRPT.Accion = objReport.Accion;
                    objRPT.Version = objReport.Version;
                    objRPT.Estado = objReport.Estado;
                    objRPT.UsuarioCreacion = objReport.UsuarioCreacion;
                    objRPT.FechaCreacion = Convert.ToDateTime(objReport.FechaCreacion);
                    objRPT.UsuarioModificacion = objReport.UsuarioModificacion;
                    objRPT.FechaModificacion = Convert.ToDateTime(objReport.FechaModificacion);

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewAnteFisiologico.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }

        //***FIN ANT.PER. FISIO 


        private void GenerarReporteAntFisiologicoPediatrico_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewAntFisiologicoPediatricoFE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewAntFisiologicoPediatricoFE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit>();
            listaRPT = getDatarptViewAntFisiologicoPediatrico_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);


            //Datos Generales
            setDatosGenerales();


            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }



        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit>
  getDatarptViewAntFisiologicoPediatrico_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
 , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
 string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit>();

            List<rptViewAntFisiologicoPediatricoFE> rptViewAntFisiologicoPediatricoFE = new List<rptViewAntFisiologicoPediatricoFE>();
            SS_HC_Ant_Fisiologico_Pediatrico_FE ObjSolicitudProducto = new SS_HC_Ant_Fisiologico_Pediatrico_FE();
            ObjSolicitudProducto.UnidadReplicacion = unidadReplicacion;
            ObjSolicitudProducto.IdPaciente = idPaciente;
            ObjSolicitudProducto.EpisodioClinico = epiClinico;
            ObjSolicitudProducto.IdEpisodioAtencion = idEpiAtencion;
            ObjSolicitudProducto.Accion = "REPORTEA";

            rptViewAntFisiologicoPediatricoFE = ServiceReportes.rptViewAntFisiologicoPediatricoFE(ObjSolicitudProducto, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit objRPT;
            if (rptViewAntFisiologicoPediatricoFE != null)
            {
                foreach (rptViewAntFisiologicoPediatricoFE objReport in rptViewAntFisiologicoPediatricoFE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewAntFisiologicoPediatricoFEEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.IdAntFiPediatrico = objReport.EpisodioClinico;
                    objRPT.EdadMaterna = Convert.ToInt32(objReport.EdadMaterna);
                    objRPT.Paridad_1 = Convert.ToInt32(objReport.Paridad_1);
                    objRPT.Paridad_2 = Convert.ToInt32(objReport.Paridad_2);
                    objRPT.Paridad_3 = Convert.ToInt32(objReport.Paridad_3);
                    objRPT.Paridad_4 = Convert.ToInt32(objReport.Paridad_4);
                    objRPT.Gravidez = Convert.ToInt32(objReport.Gravidez);
                    objRPT.ControlPrenatal = Convert.ToInt32(objReport.ControlPrenatal);
                    objRPT.Complicaciones = objReport.Complicaciones;
                    objRPT.TipoParto = Convert.ToInt32(objReport.TipoParto);
                    objRPT.MotivoCesarea = objReport.MotivoCesarea;
                    objRPT.LugarNacimiento = objReport.LugarNacimiento;
                    objRPT.Peso = Convert.ToDecimal(objReport.Peso);
                    objRPT.PesoNR = Convert.ToInt32(objReport.PesoNR);
                    objRPT.Talla = Convert.ToDecimal(objReport.Talla);
                    objRPT.TallaNR = Convert.ToInt32(objReport.TallaNR);
                    objRPT.PCNacer = Convert.ToDecimal(objReport.PCNacer);
                    objRPT.PCNacerNR = Convert.ToInt32(objReport.PCNacerNR);
                    objRPT.APGAR = Convert.ToInt32(objReport.APGAR);
                    objRPT.Reanimacion = Convert.ToInt32(objReport.Reanimacion);
                    objRPT.Lactancia = Convert.ToInt32(objReport.Lactancia);
                    objRPT.InicioAblactansia = Convert.ToDateTime(objReport.InicioAblactansia);
                    objRPT.AlimentosActuales = objReport.AlimentosActuales;
                    objRPT.Vigilancia = Convert.ToInt32(objReport.Vigilancia);
                    objRPT.Psicomotor = Convert.ToInt32(objReport.Psicomotor);
                    objRPT.DetallarPsicomotor = objReport.DetallarPsicomotor;

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewAntFisiologicoPediatricoFE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }

            return listaRPT;

        }


        private void GenerarReporteAntecedentesGeneralesPatologicos_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewAntecedentesPersonalesPatologicosGenerales_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewAntecedentesPersonalesPatologicosGenerales_FE.rpt"));
            //List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit> listaRPT1 = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit>();
            //List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit> listaRPT2 = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit>();
            DataTable listaRPT1 = new DataTable();
            DataTable listaRPT2 = new DataTable();

            listaRPT1 = rptVistas_FE("rptViewAntecedentesPersonalesPatologicosGenerales_FE",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO);
            Rpt.Subreports["rptViewAntecedentesPatologicosGeneralesdetalle.rpt"].SetDataSource(listaRPT1);
            Rpt.SetDataSource(listaRPT1);

            //Datos Generales
            setDatosGenerales();

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            if (listaRPT1.Rows.Count == 0 && listaRPT2.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AnteFamiliar");
                    }
                    catch (Exception ex) { throw; }
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
        }


        //


        //detalle
        private void GenerarReportePatologicosGeneralDetalle(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewAntecedentesPatologicosGeneralesdetalle.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewAntecedentesPatologicosGeneralesdetalle.rpt"));

            //List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit>();
            DataTable listaRPT = new DataTable();

            listaRPT = rptVistas_FE("rptViewAntecedentesPersonalesPatologicosGenerales_FE",
                    ENTITY_GLOBAL.Instance.UnidadReplicacion,
                    (int)ENTITY_GLOBAL.Instance.PacienteID,
                    (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                    (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                    , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO,
                    ENTITY_GLOBAL.Instance.USUARIO);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Rows.Count == 0)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true); }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();
                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AnteFamiliar");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            GenerarReporteDieta_FE(tipoVista);

        }





        public static DataTable rptVistas_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " ORDER BY Accion ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "Patologicos");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }


        public static DataTable rptVigilanciaDrenaje_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " ORDER BY Accion ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "Drenajes");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }

        public static DataTable rptInformeAlta_MED_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " ORDER BY Accion ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "Drenajes");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }

        public static DataTable rptAgrupador_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                /* string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion;*/
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and EpisodioAtencion=" + EpisodioAtencion; //ADD 05.06.2017 ORLANDO
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "Agrupador");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }

        public static DataTable rptDatosPacienteMedico_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                /* string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion;*/
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and EpisodioAtencion=" + EpisodioAtencion;//ADD 07.06.2017 OES Motivo
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "PacienteMedico");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }

        public static DataTable rptVistasGlasgow_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario, string TipoEscala)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " and TipoEscala='" + TipoEscala + "' ORDER BY Accion ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "Glasgow");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }

        public static DataTable rptVistasBalanceHidroElectro_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario, int TipoBalance)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " and TipoBalance='" + TipoBalance + "' ORDER BY Accion ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "Balance");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }
        public static DataTable rptVistasBalanceHidroElectroDetalles_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario, int TipoBalance, int TipoGrid)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " and TipoBalance=" + TipoBalance + "and Tipo=" + TipoGrid + " ORDER BY Accion ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "BalanceDetalles");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }

        //
        //private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit>
        // getDatarptViewPatologicosGenerales_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
        //    SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, string codFormato, string codUsuario)
        //{
        //    List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit>();
        //    List<rptViewAntecedentesPersonalesPatologicosGenerales_FE> rptViewDieta_FE = new List<rptViewAntecedentesPersonalesPatologicosGenerales_FE>();
        //    SS_HC_Anam_AP_PatologicosGenerales_FE ObjDieta = new SS_HC_Anam_AP_PatologicosGenerales_FE();
        //    ObjDieta.UnidadReplicacion = unidadReplicacion;
        //    ObjDieta.IdPaciente = idPaciente;
        //    ObjDieta.EpisodioClinico = epiClinico;
        //    ObjDieta.IdEpisodioAtencion = idEpiAtencion;
        //    ObjDieta.Accion = "REPORTEA";

        //    //Servicio
        //    //rptViewDieta_FE = ServiceReportes.rptViewPatologicosGenerales_FE(ObjDieta, 0, 0);

        //    rptViewDieta_FE = rptViewPatologicosBaseDatos_FE(ObjDieta, 0, 0);
        //    objTabla1 = new System.Data.DataTable();
        //    SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit objRPT;
        //    if (rptViewDieta_FE != null)
        //    {
        //        foreach (rptViewAntecedentesPersonalesPatologicosGenerales_FE objReport in rptViewDieta_FE) // Loop through List with foreach.
        //        {
        //            objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit();
        //            objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
        //            objRPT.IdPaciente = objReport.IdPaciente;
        //            objRPT.EpisodioClinico = objReport.EpisodioClinico;
        //            objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;

        //            objRPT.EnfermedadesAnteriores_rb = objReport.EnfermedadesAnteriores_rb;
        //            objRPT.HipertensionSeleccion_ckb = objReport.HipertensionSeleccion_ckb;
        //            objRPT.HipertensionTiempoenfermedad_list = objReport.HipertensionTiempoenfermedad_list;
        //            objRPT.HipertensionMedicacion_rb = objReport.HipertensionMedicacion_rb;
        //            objRPT.HipertensionMedicacion_txt = objReport.HipertensionMedicacion_txt;
        //            objRPT.HipertensionTipoDiagn_list = objReport.HipertensionTipoDiagn_list;
        //            objRPT.DiabetesSeleccion_ckb = objReport.DiabetesSeleccion_ckb;
        //            objRPT.DiabetesTiempoenfermedad_list = objReport.DiabetesTiempoenfermedad_list;
        //            objRPT.DiabetesMedicacion_rb = objReport.DiabetesMedicacion_rb;
        //            objRPT.DiabetesMedicacion_txt = objReport.DiabetesMedicacion_txt;
        //            objRPT.DiabetesTipoDiagn_list = objReport.DiabetesTipoDiagn_list;
        //            objRPT.AsmaSeleccion_ckb = objReport.AsmaSeleccion_ckb;
        //            objRPT.AsmaTiempoenfermedad_list = objReport.AsmaTiempoenfermedad_list;
        //            objRPT.AsmaMedicacion_rb = objReport.AsmaMedicacion_rb;
        //            objRPT.AsmaMedicacion_txt = objReport.AsmaMedicacion_txt;
        //            objRPT.AsmaTipoDiagn_list = objReport.AsmaTipoDiagn_list;
        //            objRPT.SindromeCSeleccion_ckb = objReport.SindromeCSeleccion_ckb;
        //            objRPT.SindromeCTiempoenfermedad_list = objReport.SindromeCTiempoenfermedad_list;
        //            objRPT.SindromeCMedicacion_rb = objReport.SindromeCMedicacion_rb;
        //            objRPT.SindromeCMedicacion_txt = objReport.SindromeCMedicacion_txt;
        //            objRPT.SindromeCTipoDiagn_list = objReport.SindromeCTipoDiagn_list;
        //            objRPT.SindromeRSeleccion_ckb = objReport.SindromeRSeleccion_ckb;
        //            objRPT.SindromeRTiempoenfermedad_list = objReport.SindromeRTiempoenfermedad_list;
        //            objRPT.SindromeRMedicacion_rb = objReport.SindromeRMedicacion_rb;
        //            objRPT.SindromeRMedicacion_txt = objReport.SindromeRMedicacion_txt;
        //            objRPT.SindromeRTipoDiagn_list = objReport.SindromeRTipoDiagn_list;
        //            objRPT.GastritisSeleccion_ckb = objReport.GastritisSeleccion_ckb;
        //            objRPT.GastritisTiempoenfermedad_list = objReport.GastritisTiempoenfermedad_list;
        //            objRPT.GastritisMedicacion_rb = objReport.GastritisMedicacion_rb;
        //            objRPT.GastritisMedicacion_txt = objReport.GastritisMedicacion_txt;
        //            objRPT.GastritisTipoDiagn_list = objReport.GastritisTipoDiagn_list;
        //            objRPT.ArritmiaSeleccion_ckb = objReport.ArritmiaSeleccion_ckb;
        //            objRPT.ArritmiaTiempoenfermedad_list = objReport.ArritmiaTiempoenfermedad_list;
        //            objRPT.ArritmiaMedicacion_rb = objReport.ArritmiaMedicacion_rb;
        //            objRPT.ArritmiaMedicacion_txt = objReport.ArritmiaMedicacion_txt;
        //            objRPT.ArritmiaTipoDiagn_list = objReport.ArritmiaTipoDiagn_list;
        //            objRPT.HepatitisSeleccion_ckb = objReport.HepatitisSeleccion_ckb;
        //            objRPT.HepatitisTiempoenfermedad_list = objReport.HepatitisTiempoenfermedad_list;
        //            objRPT.HepatitisMedicacion_rb = objReport.HepatitisMedicacion_rb;
        //            objRPT.HepatitisMedicacion_txt = objReport.HepatitisMedicacion_txt;
        //            objRPT.HepatitisTipoDiagn_list = objReport.HepatitisTipoDiagn_list;
        //            objRPT.TuberculosisSeleccion_ckb = objReport.TuberculosisSeleccion_ckb;
        //            objRPT.TuberculosisTiempoenfermedad_list = objReport.TuberculosisTiempoenfermedad_list;
        //            objRPT.TuberculosisMedicacion_rb = objReport.TuberculosisMedicacion_rb;
        //            objRPT.TuberculosisMedicacion_txt = objReport.TuberculosisMedicacion_txt;
        //            objRPT.TuberculosisTipoDiagn_list = objReport.TuberculosisTipoDiagn_list;
        //            objRPT.HipertiroidismoSeleccion_ckb = objReport.HipertiroidismoSeleccion_ckb;
        //            objRPT.HipertiroidismoTiempoenfermedad_list = objReport.HipertiroidismoTiempoenfermedad_list;
        //            objRPT.HipertiroidismoMedicacion_rb = objReport.HipertiroidismoMedicacion_rb;
        //            objRPT.HipertiroidismoMedicacion_txt = objReport.HipertiroidismoMedicacion_txt;
        //            objRPT.HipertiroidismoTipoDiagn_list = objReport.HipertiroidismoTipoDiagn_list;
        //            objRPT.HipotiroidismoSeleccion_ckb = objReport.HipotiroidismoSeleccion_ckb;
        //            objRPT.HipotiroidismoTiempoenfermedad_list = objReport.HipotiroidismoTiempoenfermedad_list;
        //            objRPT.HipotiroidismoMedicacion_rb = objReport.HipotiroidismoMedicacion_rb;
        //            objRPT.HipotiroidismoMedicacion_txt = objReport.HipotiroidismoMedicacion_txt;
        //            objRPT.HipotiroidismoTipoDiagn_list = objReport.HipotiroidismoTipoDiagn_list;
        //            objRPT.InfeccionSeleccion_ckb = objReport.InfeccionSeleccion_ckb;
        //            objRPT.InfeccionTiempoenfermedad_list = objReport.InfeccionTiempoenfermedad_list;
        //            objRPT.InfeccionMedicacion_rb = objReport.InfeccionMedicacion_rb;
        //            objRPT.InfeccionMedicacion_txt = objReport.InfeccionMedicacion_txt;
        //            objRPT.InfeccionTipoDiagn_list = objReport.InfeccionTipoDiagn_list;
        //            objRPT.CardiopatiasSeleccion_ckb = objReport.CardiopatiasSeleccion_ckb;
        //            objRPT.CardiopatiasTiempoenfermedad_list = objReport.CardiopatiasTiempoenfermedad_list;
        //            objRPT.CardiopatiasMedicacion_rb = objReport.CardiopatiasMedicacion_rb;
        //            objRPT.CardiopatiasMedicacion_txt = objReport.CardiopatiasMedicacion_txt;
        //            objRPT.CardiopatiasTipoDiagn_list = objReport.CardiopatiasTipoDiagn_list;
        //            objRPT.EtransmisionSSeleccion_ckb = objReport.EtransmisionSSeleccion_ckb;
        //            objRPT.EtransmisionSTiempoenfermedad_list = objReport.EtransmisionSTiempoenfermedad_list;
        //            objRPT.EtransmisionSMedicacion_rb = objReport.EtransmisionSMedicacion_rb;
        //            objRPT.EtransmisionSMedicacion_txt = objReport.EtransmisionSMedicacion_txt;
        //            objRPT.EtransmisionSTipoDiagn_list = objReport.EtransmisionSTipoDiagn_list;
        //            objRPT.DShipoacusiaSeleccion_ckb = objReport.DShipoacusiaSeleccion_ckb;
        //            objRPT.DShipoacusiaTiempoenfermedad_list = objReport.DShipoacusiaTiempoenfermedad_list;
        //            objRPT.DShipoacusiaMedicacion_rb = objReport.DShipoacusiaMedicacion_rb;
        //            objRPT.DShipoacusiaMedicacion_txt = objReport.DShipoacusiaMedicacion_txt;
        //            objRPT.DShipoacusiaTipoDiagn_list = objReport.DShipoacusiaTipoDiagn_list;
        //            objRPT.DScegueraSeleccion_ckb = objReport.DScegueraSeleccion_ckb;
        //            objRPT.DScegueraTiempoenfermedad_list = objReport.DScegueraTiempoenfermedad_list;
        //            objRPT.DScegueraMedicacion_rb = objReport.DScegueraMedicacion_rb;
        //            objRPT.DScegueraMedicacion_txt = objReport.DScegueraMedicacion_txt;
        //            objRPT.DScegueraTipoDiagn_list = objReport.DScegueraTipoDiagn_list;
        //            objRPT.DSSordoMudoSeleccion_ckb = objReport.DSSordoMudoSeleccion_ckb;
        //            objRPT.DSSordoMudoTiempoenfermedad_list = objReport.DSSordoMudoTiempoenfermedad_list;
        //            objRPT.DSSordoMudoMedicacion_rb = objReport.DSSordoMudoMedicacion_rb;
        //            objRPT.DSSordoMudoMedicacion_txt = objReport.DSSordoMudoMedicacion_txt;
        //            objRPT.DSSordoMudoTipoDiagn_list = objReport.DSSordoMudoTipoDiagn_list;
        //            objRPT.DSMiopiaAltaSeleccion_ckb = objReport.DSMiopiaAltaSeleccion_ckb;
        //            objRPT.DSMiopiaAltaTiempoenfermedad_list = objReport.DSMiopiaAltaTiempoenfermedad_list;
        //            objRPT.DSMiopiaAltaMedicacion_rb = objReport.DSMiopiaAltaMedicacion_rb;
        //            objRPT.DSMiopiaAltaMedicacion_txt = objReport.DSMiopiaAltaMedicacion_txt;
        //            objRPT.DSMiopiaAltaTipoDiagn_list = objReport.DSMiopiaAltaTipoDiagn_list;
        //            objRPT.CancerSeleccion_ckb = objReport.CancerSeleccion_ckb;
        //            objRPT.CancerTiempoenfermedad_list = objReport.CancerTiempoenfermedad_list;
        //            objRPT.CancerMedicacion_rb = objReport.CancerMedicacion_rb;
        //            objRPT.CancerMedicacion_txt = objReport.CancerMedicacion_txt;
        //            objRPT.CancerTipoDiagn_list = objReport.CancerTipoDiagn_list;

        //            objRPT.Secuencia = Convert.ToInt32(objReport.Secuencia);
        //            objRPT.OtrasEnfermedades = objReport.OtrasEnfermedades;



        //            //objRPT.IdDiagnostico = Convert.ToInt32(objReport.IdDiagnostico);
        //            objRPT.DiagnosticoText = objReport.DiagnosticoText;
        //            objRPT.TiempoEnfermedad_list = objReport.TiempoEnfermedad_list;
        //            objRPT.DiagnosticoText = objReport.DiagnosticoText;
        //            //objRPT.TipoDiagn_list = objReport.TipoDiagn_list;

        //            objRPT.Descripcion = objReport.Descripcion;




        //            objRPT.Adicional1 = objReport.Adicional1;
        //            objRPT.Adicional2 = objReport.Adicional2;
        //            objRPT.Medicacion_txt = objReport.Medicacion_txt;


        //            listaRPT.Add(objRPT);
        //        }
        //    }
        //    return listaRPT;
        //}
        ////FIN


        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit>
        getDatarptViewAntecedentesPersonalesPatologicosGenerales_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion,
           SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog, String codFormato, String codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit>();
            List<rptViewAntecedentesPersonalesPatologicosGenerales_FE> rptViewAntePatologicoGeneral = new List<rptViewAntecedentesPersonalesPatologicosGenerales_FE>();

            SS_HC_Anam_AP_PatologicosGenerales_FE objPatologicos = new SS_HC_Anam_AP_PatologicosGenerales_FE();
            objPatologicos.UnidadReplicacion = unidadReplicacion;
            objPatologicos.IdPaciente = idPaciente;
            objPatologicos.EpisodioClinico = epiClinico;
            objPatologicos.IdEpisodioAtencion = idEpiAtencion;
            objPatologicos.IdPatologicosGenerales = 1;
            objPatologicos.Accion = "REPORTEA";

            //Servicio
            rptViewAntePatologicoGeneral = ServiceReportes.rptViewPatologicosGenerales_FE(objPatologicos, 0, 0);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit objRPT;

            if (rptViewAntePatologicoGeneral != null)
            {
                foreach (rptViewAntecedentesPersonalesPatologicosGenerales_FE objReport in rptViewAntePatologicoGeneral)
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewAntecedentesPersonalesPatologicosGenerales_FEEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;

                    objRPT.EnfermedadesAnteriores_rb = objReport.EnfermedadesAnteriores_rb;
                    objRPT.HipertensionSeleccion_ckb = objReport.HipertensionSeleccion_ckb;
                    objRPT.HipertensionTiempoenfermedad_list = objReport.HipertensionTiempoenfermedad_list;
                    objRPT.HipertensionMedicacion_rb = objReport.HipertensionMedicacion_rb;
                    objRPT.HipertensionMedicacion_txt = objReport.HipertensionMedicacion_txt;
                    objRPT.HipertensionTipoDiagn_list = objReport.HipertensionTipoDiagn_list;
                    objRPT.DiabetesSeleccion_ckb = objReport.DiabetesSeleccion_ckb;
                    objRPT.DiabetesTiempoenfermedad_list = objReport.DiabetesTiempoenfermedad_list;
                    objRPT.DiabetesMedicacion_rb = objReport.DiabetesMedicacion_rb;
                    objRPT.DiabetesMedicacion_txt = objReport.DiabetesMedicacion_txt;
                    objRPT.DiabetesTipoDiagn_list = objReport.DiabetesTipoDiagn_list;
                    objRPT.AsmaSeleccion_ckb = objReport.AsmaSeleccion_ckb;
                    objRPT.AsmaTiempoenfermedad_list = objReport.AsmaTiempoenfermedad_list;
                    objRPT.AsmaMedicacion_rb = objReport.AsmaMedicacion_rb;
                    objRPT.AsmaMedicacion_txt = objReport.AsmaMedicacion_txt;
                    objRPT.AsmaTipoDiagn_list = objReport.AsmaTipoDiagn_list;
                    objRPT.SindromeCSeleccion_ckb = objReport.SindromeCSeleccion_ckb;
                    objRPT.SindromeCTiempoenfermedad_list = objReport.SindromeCTiempoenfermedad_list;
                    objRPT.SindromeCMedicacion_rb = objReport.SindromeCMedicacion_rb;
                    objRPT.SindromeCMedicacion_txt = objReport.SindromeCMedicacion_txt;
                    objRPT.SindromeCTipoDiagn_list = objReport.SindromeCTipoDiagn_list;
                    objRPT.SindromeRSeleccion_ckb = objReport.SindromeRSeleccion_ckb;
                    objRPT.SindromeRTiempoenfermedad_list = objReport.SindromeRTiempoenfermedad_list;
                    objRPT.SindromeRMedicacion_rb = objReport.SindromeRMedicacion_rb;
                    objRPT.SindromeRMedicacion_txt = objReport.SindromeRMedicacion_txt;
                    objRPT.SindromeRTipoDiagn_list = objReport.SindromeRTipoDiagn_list;
                    objRPT.GastritisSeleccion_ckb = objReport.GastritisSeleccion_ckb;
                    objRPT.GastritisTiempoenfermedad_list = objReport.GastritisTiempoenfermedad_list;
                    objRPT.GastritisMedicacion_rb = objReport.GastritisMedicacion_rb;
                    objRPT.GastritisMedicacion_txt = objReport.GastritisMedicacion_txt;
                    objRPT.GastritisTipoDiagn_list = objReport.GastritisTipoDiagn_list;
                    objRPT.ArritmiaSeleccion_ckb = objReport.ArritmiaSeleccion_ckb;
                    objRPT.ArritmiaTiempoenfermedad_list = objReport.ArritmiaTiempoenfermedad_list;
                    objRPT.ArritmiaMedicacion_rb = objReport.ArritmiaMedicacion_rb;
                    objRPT.ArritmiaMedicacion_txt = objReport.ArritmiaMedicacion_txt;
                    objRPT.ArritmiaTipoDiagn_list = objReport.ArritmiaTipoDiagn_list;
                    objRPT.HepatitisSeleccion_ckb = objReport.HepatitisSeleccion_ckb;
                    objRPT.HepatitisTiempoenfermedad_list = objReport.HepatitisTiempoenfermedad_list;
                    objRPT.HepatitisMedicacion_rb = objReport.HepatitisMedicacion_rb;
                    objRPT.HepatitisMedicacion_txt = objReport.HepatitisMedicacion_txt;
                    objRPT.HepatitisTipoDiagn_list = objReport.HepatitisTipoDiagn_list;
                    objRPT.TuberculosisSeleccion_ckb = objReport.TuberculosisSeleccion_ckb;
                    objRPT.TuberculosisTiempoenfermedad_list = objReport.TuberculosisTiempoenfermedad_list;
                    objRPT.TuberculosisMedicacion_rb = objReport.TuberculosisMedicacion_rb;
                    objRPT.TuberculosisMedicacion_txt = objReport.TuberculosisMedicacion_txt;
                    objRPT.TuberculosisTipoDiagn_list = objReport.TuberculosisTipoDiagn_list;
                    objRPT.HipertiroidismoSeleccion_ckb = objReport.HipertiroidismoSeleccion_ckb;
                    objRPT.HipertiroidismoTiempoenfermedad_list = objReport.HipertiroidismoTiempoenfermedad_list;
                    objRPT.HipertiroidismoMedicacion_rb = objReport.HipertiroidismoMedicacion_rb;
                    objRPT.HipertiroidismoMedicacion_txt = objReport.HipertiroidismoMedicacion_txt;
                    objRPT.HipertiroidismoTipoDiagn_list = objReport.HipertiroidismoTipoDiagn_list;
                    objRPT.HipotiroidismoSeleccion_ckb = objReport.HipotiroidismoSeleccion_ckb;
                    objRPT.HipotiroidismoTiempoenfermedad_list = objReport.HipotiroidismoTiempoenfermedad_list;
                    objRPT.HipotiroidismoMedicacion_rb = objReport.HipotiroidismoMedicacion_rb;
                    objRPT.HipotiroidismoMedicacion_txt = objReport.HipotiroidismoMedicacion_txt;
                    objRPT.HipotiroidismoTipoDiagn_list = objReport.HipotiroidismoTipoDiagn_list;
                    objRPT.InfeccionSeleccion_ckb = objReport.InfeccionSeleccion_ckb;
                    objRPT.InfeccionTiempoenfermedad_list = objReport.InfeccionTiempoenfermedad_list;
                    objRPT.InfeccionMedicacion_rb = objReport.InfeccionMedicacion_rb;
                    objRPT.InfeccionMedicacion_txt = objReport.InfeccionMedicacion_txt;
                    objRPT.InfeccionTipoDiagn_list = objReport.InfeccionTipoDiagn_list;
                    objRPT.CardiopatiasSeleccion_ckb = objReport.CardiopatiasSeleccion_ckb;
                    objRPT.CardiopatiasTiempoenfermedad_list = objReport.CardiopatiasTiempoenfermedad_list;
                    objRPT.CardiopatiasMedicacion_rb = objReport.CardiopatiasMedicacion_rb;
                    objRPT.CardiopatiasMedicacion_txt = objReport.CardiopatiasMedicacion_txt;
                    objRPT.CardiopatiasTipoDiagn_list = objReport.CardiopatiasTipoDiagn_list;
                    objRPT.EtransmisionSSeleccion_ckb = objReport.EtransmisionSSeleccion_ckb;
                    objRPT.EtransmisionSTiempoenfermedad_list = objReport.EtransmisionSTiempoenfermedad_list;
                    objRPT.EtransmisionSMedicacion_rb = objReport.EtransmisionSMedicacion_rb;
                    objRPT.EtransmisionSMedicacion_txt = objReport.EtransmisionSMedicacion_txt;
                    objRPT.EtransmisionSTipoDiagn_list = objReport.EtransmisionSTipoDiagn_list;
                    objRPT.DShipoacusiaSeleccion_ckb = objReport.DShipoacusiaSeleccion_ckb;
                    objRPT.DShipoacusiaTiempoenfermedad_list = objReport.DShipoacusiaTiempoenfermedad_list;
                    objRPT.DShipoacusiaMedicacion_rb = objReport.DShipoacusiaMedicacion_rb;
                    objRPT.DShipoacusiaMedicacion_txt = objReport.DShipoacusiaMedicacion_txt;
                    objRPT.DShipoacusiaTipoDiagn_list = objReport.DShipoacusiaTipoDiagn_list;
                    objRPT.DScegueraSeleccion_ckb = objReport.DScegueraSeleccion_ckb;
                    objRPT.DScegueraTiempoenfermedad_list = objReport.DScegueraTiempoenfermedad_list;
                    objRPT.DScegueraMedicacion_rb = objReport.DScegueraMedicacion_rb;
                    objRPT.DScegueraMedicacion_txt = objReport.DScegueraMedicacion_txt;
                    objRPT.DScegueraTipoDiagn_list = objReport.DScegueraTipoDiagn_list;
                    objRPT.DSSordoMudoSeleccion_ckb = objReport.DSSordoMudoSeleccion_ckb;
                    objRPT.DSSordoMudoTiempoenfermedad_list = objReport.DSSordoMudoTiempoenfermedad_list;
                    objRPT.DSSordoMudoMedicacion_rb = objReport.DSSordoMudoMedicacion_rb;
                    objRPT.DSSordoMudoMedicacion_txt = objReport.DSSordoMudoMedicacion_txt;
                    objRPT.DSSordoMudoTipoDiagn_list = objReport.DSSordoMudoTipoDiagn_list;
                    objRPT.DSMiopiaAltaSeleccion_ckb = objReport.DSMiopiaAltaSeleccion_ckb;
                    objRPT.DSMiopiaAltaTiempoenfermedad_list = objReport.DSMiopiaAltaTiempoenfermedad_list;
                    objRPT.DSMiopiaAltaMedicacion_rb = objReport.DSMiopiaAltaMedicacion_rb;
                    objRPT.DSMiopiaAltaMedicacion_txt = objReport.DSMiopiaAltaMedicacion_txt;
                    objRPT.DSMiopiaAltaTipoDiagn_list = objReport.DSMiopiaAltaTipoDiagn_list;
                    objRPT.CancerSeleccion_ckb = objReport.CancerSeleccion_ckb;
                    objRPT.CancerTiempoenfermedad_list = objReport.CancerTiempoenfermedad_list;
                    objRPT.CancerMedicacion_rb = objReport.CancerMedicacion_rb;
                    objRPT.CancerMedicacion_txt = objReport.CancerMedicacion_txt;
                    objRPT.CancerTipoDiagn_list = objReport.CancerTipoDiagn_list;

                    objRPT.Secuencia = Convert.ToInt32(objReport.Secuencia);
                    objRPT.OtrasEnfermedades = objReport.OtrasEnfermedades;



                    //objRPT.IdDiagnostico = Convert.ToInt32(objReport.IdDiagnostico);
                    objRPT.DiagnosticoText = objReport.DiagnosticoText;
                    objRPT.TiempoEnfermedad_list = objReport.TiempoEnfermedad_list;
                    objRPT.DiagnosticoText = objReport.DiagnosticoText;
                    //objRPT.TipoDiagn_list = objReport.TipoDiagn_list;

                    objRPT.Descripcion = objReport.Descripcion;




                    objRPT.Adicional1 = objReport.Adicional1;
                    objRPT.Adicional2 = objReport.Adicional2;
                    objRPT.Medicacion_txt = objReport.Medicacion_txt;

                    listaRPT.Add(objRPT);
                }

                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewAntePatologicoGeneral.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                /////////////////////////////// 
            }
            return listaRPT;
        }



        //*** Inicio CIRUGIA ENTRADA
        private void GenerarReporterptViewSeguridadCirugiaEntrada_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewSeguridadCirugiaEntrada_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewSeguridadCirugiaEntrada_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit>();
            listaRPT = getDatarptViewSeguridadCirugia("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, 1);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }


        //***FIN CIRUGIA ENTRADA


        #region CCEPF463_REPORTE


        private void GenerarReporterptViewSeguridadCirugiaSalida_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewSeguridadCirugiaSalida_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewSeguridadCirugiaSalida_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit>();
            listaRPT = getDatarptViewSeguridadCirugia("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, 3);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }
        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit>
        getDatarptViewSeguridadCirugia(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
       , SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit objEntidad, int idImpresionLog,
       string codFormato, string codUsuario, int tipocirugia)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit>();

            List<rptViewSeguridadCirugia_FE> rptViewAnteFisiologico = new List<rptViewSeguridadCirugia_FE>();
            SS_HC_SeguridadCirugia_FE ObjAnteFisiologico = new SS_HC_SeguridadCirugia_FE();
            ObjAnteFisiologico.UnidadReplicacion = unidadReplicacion;
            ObjAnteFisiologico.IdPaciente = idPaciente;
            ObjAnteFisiologico.EpisodioClinico = epiClinico;
            ObjAnteFisiologico.IdEpisodioAtencion = idEpiAtencion;
            ObjAnteFisiologico.IdEpisodioAtencion = 1;
            ObjAnteFisiologico.Accion = "REPORTEA";


            //Reporte-Service
            rptViewAnteFisiologico = ServiceReportes.rptViewSeguridadCirugia_FE(ObjAnteFisiologico, 0, 0, tipocirugia);

            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit objRPT;

            if (rptViewAnteFisiologico != null)
            {
                foreach (rptViewSeguridadCirugia_FE objReport in rptViewAnteFisiologico) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.NombreCompleto = objReport.NombreCompleto;
                    objRPT.IntervencionQ = objReport.IntervencionQ;
                    objRPT.FechaRegistro = Convert.ToDateTime(objReport.FechaRegistro);
                    objRPT.Respuesta1 = Convert.ToInt32(objReport.Respuesta1);
                    objRPT.Respuesta2 = Convert.ToInt32(objReport.Respuesta2);
                    objRPT.Respuesta3 = Convert.ToInt32(objReport.Respuesta3);
                    objRPT.Respuesta4 = Convert.ToInt32(objReport.Respuesta4);
                    objRPT.Respuesta5 = Convert.ToInt32(objReport.Respuesta5);
                    objRPT.Respuesta6 = Convert.ToInt32(objReport.Respuesta6);
                    objRPT.Respuesta7 = Convert.ToInt32(objReport.Respuesta7);
                    objRPT.Respuesta8 = Convert.ToInt32(objReport.Respuesta8);
                    objRPT.Respuesta9 = Convert.ToInt32(objReport.Respuesta9);
                    objRPT.Respuesta10 = Convert.ToInt32(objReport.Respuesta10);
                    objRPT.Respuesta11 = Convert.ToInt32(objReport.Respuesta11);
                    objRPT.Respuesta12 = Convert.ToInt32(objReport.Respuesta12);
                    objRPT.Respuesta13 = Convert.ToInt32(objReport.Respuesta13);
                    objRPT.Respuesta14 = Convert.ToInt32(objReport.Respuesta14);
                    objRPT.Antibiotico = objReport.Antibiotico;
                    objRPT.UsuarioCreacion = objReport.UsuarioCreacion;
                    objRPT.FechaCreacion = Convert.ToDateTime(objReport.FechaCreacion);
                    objRPT.UsuarioModificacion = objReport.UsuarioModificacion;
                    objRPT.FechaModificacion = Convert.ToDateTime(objReport.FechaModificacion);

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                //if (rptViewAntecedentesPersonalesFisiologicos_FE.Count > 0)
                //{
                //    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                //}
                /////////////////////////////// 
            }

            return listaRPT;

        }
        #endregion








        #region CCEPF462_REPORTE


        private void GenerarReporterptViewSeguridadCirugiaPausa_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewSeguridadCirugiaPausa_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewSeguridadCirugiaPausa_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewSeguridadCirugia_FEEdit>();
            listaRPT = getDatarptViewSeguridadCirugia("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, 2);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }


        #endregion




        #region CCEPF444_REPORTE

        private void GenerarReporterptViewEscalaAldrete_FE(string tipoVista)
        {

            Rpt.Load(Server.MapPath("rptReports/rptViewEscalaAldrete_FE.rpt")); // Crystal Report
            string tura = Server.MapPath("rptReports/rptViewEscalaAldrete_FE.rpt");

            DataTable listaRPT = new DataTable();
            string varVistaEntidad = "rptViewEscalaAldrete_FE"; // Entidad Vista
            listaRPT = rptVistasEscalaAldrete_FE(varVistaEntidad, ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID
                                   , (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "EscalaAldrete");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }




        }
        public static DataTable rptVistasEscalaAldrete_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " ORDER BY Accion ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "EscalaAldrete");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }

        #endregion // fin CCEPF444_REPORTE




        #region CCEPF445_REPORTE
        //***ESCALA STEWART
        private void GenerarReporterptViewEscalaStewart_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewEscalaStewart_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewEscalaStewart_FE.rpt"));

            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEscalaStewart_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEscalaStewart_FEEdit>();
            listaRPT = getDatarptViewEscalaStewart_FE("REPORTEA", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
            }
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


        }
        private List<SoluccionSalud.RepositoryReport.Entidades.rptViewEscalaStewart_FEEdit>
        getDatarptViewEscalaStewart_FE(String accion, String unidadReplicacion, int idPaciente, int epiClinico, long idEpiAtencion
       , SoluccionSalud.RepositoryReport.Entidades.rptViewAgrupadorEdit objEntidad, int idImpresionLog,
       string codFormato, string codUsuario)
        {
            List<SoluccionSalud.RepositoryReport.Entidades.rptViewEscalaStewart_FEEdit> listaRPT = new List<SoluccionSalud.RepositoryReport.Entidades.rptViewEscalaStewart_FEEdit>();

            List<rptViewEscalaStewart_FE> rptViewEscalaStewart_FE = new List<rptViewEscalaStewart_FE>();
            SS_HC_EscalaStewart_FE ObjAnteFisiologico = new SS_HC_EscalaStewart_FE();
            ObjAnteFisiologico.UnidadReplicacion = unidadReplicacion;
            ObjAnteFisiologico.IdPaciente = idPaciente;
            ObjAnteFisiologico.EpisodioClinico = epiClinico;
            ObjAnteFisiologico.IdEpisodioAtencion = idEpiAtencion;
            ObjAnteFisiologico.IdEpisodioAtencion = 1;
            ObjAnteFisiologico.Accion = "REPORTEA";
            rptViewEscalaStewart_FE = ServiceReportes.rptViewEscalaStewart_FE(ObjAnteFisiologico, 0, 0);
            //AAAA
            objTabla1 = new System.Data.DataTable();

            SoluccionSalud.RepositoryReport.Entidades.rptViewEscalaStewart_FEEdit objRPT;
            if (rptViewEscalaStewart_FE != null)
            {
                foreach (rptViewEscalaStewart_FE objReport in rptViewEscalaStewart_FE) // Loop through List with foreach.
                {
                    objRPT = new SoluccionSalud.RepositoryReport.Entidades.rptViewEscalaStewart_FEEdit();

                    objRPT.UnidadReplicacion = objReport.UnidadReplicacion;
                    objRPT.IdEpisodioAtencion = objReport.IdEpisodioAtencion;
                    objRPT.IdPaciente = objReport.IdPaciente;
                    objRPT.EpisodioClinico = objReport.EpisodioClinico;
                    objRPT.IdEscalaStewart = objReport.IdEscalaStewart;
                    objRPT.FechaIngreso = Convert.ToDateTime(objReport.FechaIngreso);
                    objRPT.HoraIngreso = Convert.ToDateTime(objReport.HoraIngreso);
                    objRPT.FlagParametro1 = Convert.ToInt32(objReport.FlagParametro1);
                    objRPT.FlagParametro2 = Convert.ToInt32(objReport.FlagParametro2);
                    objRPT.FlagParametro3 = Convert.ToInt32(objReport.FlagParametro3);
                    objRPT.FlagParametro4 = Convert.ToInt32(objReport.FlagParametro4);
                    objRPT.FlagParametro5 = Convert.ToInt32(objReport.FlagParametro5);
                    objRPT.FlagParametro6 = Convert.ToInt32(objReport.FlagParametro6);
                    objRPT.FlagParametro7 = Convert.ToInt32(objReport.FlagParametro7);
                    objRPT.FlagParametro8 = Convert.ToInt32(objReport.FlagParametro8);
                    objRPT.FlagParametro9 = Convert.ToInt32(objReport.FlagParametro9);
                    objRPT.Adicional1 = Convert.ToInt32(objReport.Adicional1);
                    objRPT.Adicional2 = objReport.Adicional2;
                    objRPT.Total = Convert.ToInt32(objReport.Total);
                    objRPT.Estado = Convert.ToInt32(objReport.Estado);
                    objRPT.UsuarioCreacion = objReport.UsuarioCreacion;
                    objRPT.FechaCreacion = Convert.ToDateTime(objReport.FechaCreacion);
                    objRPT.UsuarioModificacion = objReport.UsuarioModificacion;
                    objRPT.FechaModificacion = Convert.ToDateTime(objReport.FechaModificacion);

                    listaRPT.Add(objRPT);
                }
                ///////////////////////////////                     
                //PARA LA AUDITORIA DE IMPRESION
                if (rptViewEscalaStewart_FE.Count > 0)
                {
                    setDataImpresionAuditoria(accion, idImpresionLog, objEntidad, codFormato, codUsuario);
                }
                ///////////////////////////////  
            }

            return listaRPT;

        }

        //***FIN ESCALA STEWART
        #endregion


        #region CCEPF464_REPORTE
        private void GenerarReporterptViewEscalaAltaCirugiaAmbulatoria_FE(string tipoVista)
        {
            Rpt.Load(Server.MapPath("rptReports/rptViewEscalaAltaCirugiaAmbulatoria_FE.rpt"));
            DataTable listaRPT = new DataTable();
            listaRPT = rptVistas_FE("rptViewEscalaAltaCirugiaAmbulatoria_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
               , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion


        #region CCEPF435_REPORTE
        private void GenerarReporterptViewGradoDependencia_FE(string tipoVista)
        {
            Rpt.Load(Server.MapPath("rptReports/rptViewGradoDependencia_FE.rpt"));
            DataTable listaRPT = new DataTable();
            listaRPT = rptVistas_FE("rptViewGradoDependencia_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
               , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);



            Rpt.SetDataSource(listaRPT);
            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }

        }

        #endregion

        #region CCEPF448_REPORTE
        private void GenerarReporterptViewEscalaSedacionRass_FE(string tipoVista)
        {
            Rpt.Load(Server.MapPath("rptReports/rptViewEscalaSedacionRass_FE.rpt"));
            DataTable listaRPT = new DataTable();
            listaRPT = rptVistas_FE("rptViewEscalaSedacionRass_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
               , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion

        #region CCEPF440_REPORTE
        private void GenerarReporterptViewEscalaGlasgow_FE(string tipoVista)
        {
            Rpt.Load(Server.MapPath("rptReports/rptViewEscalaGlasgow_FE.rpt"));
            DataTable listaRPT = new DataTable();
            listaRPT = rptVistasGlasgow_FE("rptViewEscalaGlasgow_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
               , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, "EG");
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }

        }

        #endregion

        #region CCEPF441_REPORTE
        private void GenerarReporterptViewEscalaGlasgowPreEscolar_FE(string tipoVista)
        {
            Rpt.Load(Server.MapPath("rptReports/rptViewEscalaGlasgowPreEscolar_FE.rpt"));
            DataTable listaRPT = new DataTable();
            listaRPT = rptVistasGlasgow_FE("rptViewEscalaGlasgow_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
               , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, "GP");
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion

        #region CCEPF442_REPORTE
        private void GenerarReporterptViewEscalaGlasgowLactante_FE(string tipoVista)
        {
            Rpt.Load(Server.MapPath("rptReports/rptViewEscalaGlasgowLactante_FE.rpt"));
            DataTable listaRPT = new DataTable();
            listaRPT = rptVistasGlasgow_FE("rptViewEscalaGlasgow_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
               , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, "GL");
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        ;
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "SolicitudMedicamento");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion

        #region CCEPF447_REPORTE

        private void GenerarReporterptViewEscalaRamsay_FE(string tipoVista)
        {

            Rpt.Load(Server.MapPath("rptReports/rptViewEscalaRamsay_FE.rpt")); // Crystal Report
            string tura = Server.MapPath("rptReports/rptViewEscalaRamsay_FE.rpt");

            DataTable listaRPT = new DataTable();
            string varVistaEntidad = "rptViewEscalaRamsay_FE"; // Entidad Vista
            listaRPT = rptVistasEscalaRamsay_FE(varVistaEntidad, ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID
                                   , (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "EscalaRamsay");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }





        }
        public static DataTable rptVistasEscalaRamsay_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " ORDER BY Accion ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "EscalaRamsay");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }

        #endregion // fin CCEPF447_REPORTE

        #region CCEPF204_REPORTE

        private void GenerarReporterptViewRetiroVoluntario_FE(string tipoVista)
        {

            Rpt.Load(Server.MapPath("rptReports/rptViewRetiroVoluntario_FE.rpt")); // Crystal Report
            string tura = Server.MapPath("rptReports/rptViewRetiroVoluntario_FE.rpt");

            DataTable DataTableRPT = new DataTable();
            DataTable DataTableRPTNew = new DataTable();

            string varVistaEntidad = "rptViewRetiroVoluntario_FE"; // Entidad Vista
            DataTableRPT = rptVistasRetiroVoluntario_FE(varVistaEntidad, ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID
                                   , (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            DataTableRPTNew = rptVistasRetiroVoluntario_FE(varVistaEntidad
                                , ""
                                , 0
                                , 0
                                , 0
                                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            // Recorrer y asignar valores

            foreach (DataRow ht_fila in DataTableRPT.AsEnumerable())
            {

                DataRow rw = DataTableRPTNew.NewRow();

                rw["UnidadReplicacion"] = ht_fila["UnidadReplicacion"];
                rw["IdEpisodioAtencion"] = ht_fila["IdEpisodioAtencion"];
                rw["IdPaciente"] = ht_fila["IdPaciente"];
                rw["EpisodioClinico"] = ht_fila["EpisodioClinico"];
                rw["IdRetiroVoluntario"] = ht_fila["IdRetiroVoluntario"];
                rw["FechaIngreso"] = ht_fila["FechaIngreso"];
                rw["HoraIngreso"] = ht_fila["HoraIngreso"];
                rw["RepresentanteLegal"] = ht_fila["RepresentanteLegal"];
                rw["IdPersonalSalud"] = ht_fila["IdPersonalSalud"];
                rw["UsuarioCreacion"] = ht_fila["UsuarioCreacion"];
                rw["FechaCreacion"] = ht_fila["FechaCreacion"];
                rw["UsuarioModificacion"] = ht_fila["UsuarioModificacion"];
                rw["FechaModificacion"] = ht_fila["FechaModificacion"];
                rw["Accion"] = ht_fila["Accion"];
                rw["Version"] = ht_fila["Version"];
                rw["ApellidoPaterno"] = ht_fila["ApellidoPaterno"];
                rw["ApellidoMaterno"] = ht_fila["ApellidoMaterno"];
                rw["Nombres"] = ht_fila["Nombres"];
                rw["NombreCompleto"] = ht_fila["NombreCompleto"];
                rw["Busqueda"] = ht_fila["Busqueda"];
                rw["TipoDocumento"] = ht_fila["TipoDocumento"];
                rw["Documento"] = ht_fila["Documento"];
                rw["FechaNacimiento"] = ht_fila["FechaNacimiento"];
                rw["Sexo"] = ht_fila["Sexo"];
                rw["EstadoCivil"] = ht_fila["EstadoCivil"];
                rw["PersonaEdad"] = ht_fila["PersonaEdad"];
                rw["IdOrdenAtencion"] = ht_fila["IdOrdenAtencion"];
                rw["CodigoOA"] = ht_fila["CodigoOA"];
                rw["LineaOrdenAtencion"] = ht_fila["LineaOrdenAtencion"];
                rw["TipoOrdenAtencion"] = ht_fila["TipoOrdenAtencion"];
                rw["TipoAtencion"] = ht_fila["TipoAtencion"];
                rw["TipoTrabajador"] = ht_fila["TipoTrabajador"];
                rw["IdEstablecimientoSalud"] = ht_fila["IdEstablecimientoSalud"];
                rw["IdUnidadServicio"] = ht_fila["IdUnidadServicio"];
                rw["FechaRegistro"] = ht_fila["FechaRegistro"];
                rw["FechaAtencion"] = ht_fila["FechaAtencion"];
                rw["IdEspecialidad"] = ht_fila["IdEspecialidad"];
                rw["IdTipoOrden"] = ht_fila["IdTipoOrden"];
                rw["estadoEpiAtencion"] = ht_fila["estadoEpiAtencion"];
                rw["ObservacionProximaEpiAtencion"] = ht_fila["ObservacionProximaEpiAtencion"];
                rw["TipoAtencionDesc"] = ht_fila["TipoAtencionDesc"];
                rw["TipoTrabajadorDesc"] = ht_fila["TipoTrabajadorDesc"];
                rw["EstablecimientoCodigo"] = ht_fila["EstablecimientoCodigo"];
                rw["EstablecimientoDesc"] = ht_fila["EstablecimientoDesc"];
                rw["UnidadServicioCodigo"] = ht_fila["UnidadServicioCodigo"];
                rw["UnidadServicioDesc"] = ht_fila["UnidadServicioDesc"];
                rw["NombreCompletoPerSalud"] = ht_fila["NombreCompletoPerSalud"];
                rw["CMP"] = ht_fila["CMP"];
                rw["CodigoHC"] = ht_fila["CodigoHC"];
                rw["Cama"] = ENTITY_GLOBAL.Instance.CAMA;


                DataTableRPTNew.Rows.Add(rw);

            }
            //



            Rpt.SetDataSource(DataTableRPTNew);

            if (DataTableRPTNew.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "RetiroVoluntario");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }




        }
        public static DataTable rptVistasRetiroVoluntario_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " ORDER BY Accion ASC";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();

                adapter.Fill(ds_Result, "RetiroVoluntario");

                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }

                return ds_Result.Tables[0];

            }
        }

        #endregion // fin CCEPF447_REPORTE




        #region CCEPF446_REPORTE

        private void GenerarReporterptViewEscalaBromage_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewEscalaBromage_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewEscalaBromage_FE.rpt"));
            DataTable listaRPT = new DataTable();

            listaRPT = rptVistas_FE("rptViewEscalaBromage_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion



        #region CCEPF431_REPORTE

        private void GenerarReporterptViewDolorEvaAdulto_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewDolorEvaAdulto_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewDolorEvaAdulto_FE.rpt"));
            DataTable listaRPT = new DataTable();

            listaRPT = rptVistas_FE("rptViewDolorEvaAdulto_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);

            imgDolor = Server.MapPath("resources/images/CCEPF431.JPG");
            Rpt.SetParameterValue("imgDolor", imgDolor);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.SetParameterValue("imgDolor", imgDolor);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    Rpt.SetParameterValue("imgDolor", imgDolor);
                    Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgDolor", imgDolor);
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


            //Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            //Rpt.SetParameterValue("imgDerecha", imgDerecha);


        }
        #endregion

        #region CCEPF432_REPORTE

        private void GenerarReporterptViewDolorEvaNinios_FE(string tipoVista)
        {

            Rpt.Load(Server.MapPath("rptReports/rptViewDolorEvaNinios_FE.rpt")); // Crystal Report
            string tura = Server.MapPath("rptReports/rptViewDolorEvaNinios_FE.rpt");

            DataTable DataTableRPT = new DataTable();
            string varVistaEntidad = "rptViewDolorEvaNinios_FE"; // Entidad Vista

            DataTableRPT = rptVistasDolorEvaNinios_FE(varVistaEntidad, ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID
                                   , (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                                   , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(DataTableRPT);

            if (DataTableRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "EscalaRamsay");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }




        }
        public static DataTable rptVistasDolorEvaNinios_FE(string Reporte, string UnidadReplicacion, int PacienteID, int EpisodioClinico, long EpisodioAtencion, string var, int va, string CONCEPTO, string Usuario)
        {

            using (SqlConnection conx = new SqlConnection(ConfigurationManager.ConnectionStrings["ConexionReportes"].ToString()))
            {
                conx.Open();
                string sql = @"SELECT * FROM " + Reporte + "  where UnidadReplicacion='" + UnidadReplicacion + "' and IdPaciente=" + PacienteID + " and  EpisodioClinico= " + EpisodioClinico + " and IdEpisodioAtencion=" + EpisodioAtencion + " ORDER BY Accion ASC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conx);
                DataSet ds_Result = new DataSet();
                adapter.Fill(ds_Result, "DolorEvaNinios");
                if (ds_Result == null || ds_Result.Tables.Count == 0)
                {
                    return null;
                }
                return ds_Result.Tables[0];

            }
        }

        #endregion // fin CCEPF432_REPORTE

        #region CCEPF051_REPORTE

        private void GenerarReporterptViewFuncionesVitales_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewFuncionesVitale_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewFuncionesVitale_FE.rpt"));
            DataTable listaRPT = new DataTable();

            listaRPT = rptVistas_FE("rptViewFuncionesVitales_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);
            //DataSet obj = new DataSet();
            //dsRptViewer.Tables.Add(objTabla1.Copy());
            //dsRptViewer.WriteXmlSchema((Server.MapPath("Xmls/xmlViewAnamnesisEA.xml")));
            //Datos Generales
            setDatosGenerales();
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion

        #region CCEPF001_REPORTE

        private void GenerarReporterptViewEnfermedadActual_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewEnfermedadActual_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewEnfermedadActual_FE.rpt"));
            DataTable listaRPT = new DataTable();

            listaRPT = rptVistas_FE("rptViewEnfermedadActual_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            setDatosGenerales();
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion

        #region CCEPF501_REPORTE

        private void GenerarReporteEvolucionObstetricaPuerperio_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewEvolucionObstetricaPuerperio_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewEvolucionObstetricaPuerperio_FE.rpt"));
            DataTable listaRPT = new DataTable();

            listaRPT = rptVistas_FE("rptViewEvolucionObstetricaPuerperio_FE", ENTITY_GLOBAL.Instance.UnidadReplicacion, (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico, (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            setDatosGenerales();
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion

        #region CCEPF425_REPORTE

        private void GenerarReporteVigilanciaDispositivos_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewVigilanciaDispositivos_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewVigilanciaDispositivos_FE.rpt"));
            DataTable listaRPT = new DataTable();
            DataTable listaRPTDrenajes = new DataTable();

            listaRPT = rptVistas_FE("rptViewVigilancia_Dispositivos_FE",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            listaRPTDrenajes = rptVigilanciaDrenaje_FE("rptViewVigilancia_Dispositivos_FE",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            Rpt.Subreports["rptViewVigilanciaDispositivosDrenajes_FE.rpt"].SetDataSource(listaRPTDrenajes);

            setDatosGenerales();
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion


        #region CCEPF200_REPORTE

        private void GenerarReporteInformeAlta_FE(string tipoVista)
        {
            string tura = Server.MapPath("rptReports/rptViewInformeAlta_FE.rpt");
            Rpt.Load(Server.MapPath("rptReports/rptViewInformeAlta_FE.rpt"));
            DataTable listaRPT = new DataTable();
            DataTable listaRPT_Med = new DataTable();
            DataTable listaRPT_Mat = new DataTable();
            DataTable listaRPT_proxCita = new DataTable();

            listaRPT = rptVistas_FE("rptViewInformeAlta_DatosGenerales_FE",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            listaRPT_Med = rptInformeAlta_MED_FE("rptViewInformeAlta_Med",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            listaRPT_Mat = rptInformeAlta_MED_FE("rptViewInformeAlta_Mat",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            listaRPT_proxCita = rptInformeAlta_MED_FE("rptViewInformeAlta_proxCita",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);

            Rpt.Subreports["rptViewInformeAlta_med.rpt"].SetDataSource(listaRPT_Med);
            Rpt.Subreports["rptViewInformeAlta_mat.rpt"].SetDataSource(listaRPT_Mat);
            Rpt.Subreports["rptViewInformeAlta_proxCita.rpt"].SetDataSource(listaRPT_proxCita);

            setDatosGenerales();
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion

        #region CCEPF402_REPORTE

        private void GenerarReporteBalanceHidroElectrolitico_FE(string tipoVista, int TipoBalance)
        {
            //NEO
            if (TipoBalance == 1)
            {
                string tura = Server.MapPath("rptReports/rptViewBalanceHidroElectrolitico_FE.rpt");
                Rpt.Load(Server.MapPath("rptReports/rptViewBalanceHidroElectrolitico_FE.rpt"));
            }
            //SOP
            else if (TipoBalance == 2)
            {
                string tura = Server.MapPath("rptReports/rptViewBalanceHidroElectrolitico_SOP_FE.rpt");
                Rpt.Load(Server.MapPath("rptReports/rptViewBalanceHidroElectrolitico_SOP_FE.rpt"));
            }
            //PEDIATRICO
            else if (TipoBalance == 3)
            {
                string tura = Server.MapPath("rptReports/rptViewBalanceHidroElectrolitico_PEDIATRICO_FE.rpt");
                Rpt.Load(Server.MapPath("rptReports/rptViewBalanceHidroElectrolitico_PEDIATRICO_FE.rpt"));
            }
            //NORMAL
            else if (TipoBalance == 4)
            {
                string tura = Server.MapPath("rptReports/rptViewBalanceHidroElectrolitico_NORMAL_FE.rpt");
                Rpt.Load(Server.MapPath("rptReports/rptViewBalanceHidroElectrolitico_NORMAL_FE.rpt"));
            }

            DataTable listaRPT = new DataTable();
            DataTable listaRPT_detalle1 = new DataTable();
            DataTable listaRPT_detalle2 = new DataTable();

            listaRPT = rptVistasBalanceHidroElectro_FE("rptViewBalanceHidroElectrolitico_FE",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, TipoBalance);

            listaRPT_detalle1 = rptVistasBalanceHidroElectroDetalles_FE("rptViewBalanceHidroElectroliticoDetalle1_FE",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, TipoBalance, 1);

            listaRPT_detalle2 = rptVistasBalanceHidroElectroDetalles_FE("rptViewBalanceHidroElectroliticoDetalle2_FE",
                ENTITY_GLOBAL.Instance.UnidadReplicacion,
                (int)ENTITY_GLOBAL.Instance.PacienteID,
                (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                (long)ENTITY_GLOBAL.Instance.EpisodioAtencion
                , null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO, TipoBalance, 2);

            Rpt.Subreports["rptViewBalanceHidroElectroliticoDetalle1.rpt"].SetDataSource(listaRPT_detalle1);
            Rpt.Subreports["rptViewBalanceHidroElectroliticoDetalle2.rpt"].SetDataSource(listaRPT_detalle2);
            setDatosGenerales();
            imgIzquierda = Server.MapPath("Imagen/Logo.png");
            Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            Rpt.SetDataSource(listaRPT);

            if (listaRPT.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", string.Format("Mensaje('{0}');", "NO HAY INFORMACION"), true);
                return;
            }
            else
            {
                if (tipoVista == "I")
                {
                    ReportViewer.ReportSource = Rpt;
                    ReportViewer.DataBind();

                }
                else
                {
                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();
                    try
                    {
                        Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
                        Rpt.ExportToHttpResponse
                        (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DESCANSOMEDICO");
                    }
                    catch (Exception ex)
                    {
                        throw;
                    } Rpt.SetParameterValue("imgIzquierda", imgIzquierda);


                }
                Rpt.SetParameterValue("imgIzquierda", imgIzquierda);
            }


        }

        #endregion
        //Datos Generales (Pie de Pagina)


        public void setDatosGenerales()
        {


            DataTable listarptAgrupador_FE = new DataTable();


            listarptAgrupador_FE = rptAgrupador_FE("rptViewAgrupador", ENTITY_GLOBAL.Instance.UnidadReplicacion,
                         (int)ENTITY_GLOBAL.Instance.PacienteID, (int)ENTITY_GLOBAL.Instance.EpisodioClinico,
                         (long)ENTITY_GLOBAL.Instance.EpisodioAtencion,
                         null, 0, ENTITY_GLOBAL.Instance.CONCEPTO, ENTITY_GLOBAL.Instance.USUARIO);



            if (listarptAgrupador_FE.Rows.Count > 0)
            {
                Rpt.Subreports["rptDatosGeneralesFE.rpt"].DataSourceConnections.Clear();
                Rpt.Subreports["rptDatosGeneralesFE.rpt"].SetDataSource(listarptAgrupador_FE);
            }
        }
        // ***  FIN FORMULARIOS (EXTRAS) ***


    }

}