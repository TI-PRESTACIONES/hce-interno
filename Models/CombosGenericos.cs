using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Ext.Net.MVC;

namespace AppSaludMVC.Models
{
    public class CombosGenericos
    {
        [Field(FieldType = typeof(Ext.Net.Hidden))]
        public int ID { get; set; }
        public string Name { get; set; }


        public static List<CombosGenericos> GetEstadoRegistro(string estadosReg)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/Models/xml/tablas.xml"));
            var dataList = new List<CombosGenericos>();
            var datamodel = new CombosGenericos();
            foreach (XmlNode cityNode in xmlDoc.SelectNodes(string.Concat("CombosRegistro/ComboEstado[@code='", estadosReg, "']/campos")))
            {
                datamodel = new CombosGenericos();
                string id = cityNode.SelectSingleNode("id").InnerText;
                string name = cityNode.SelectSingleNode("name").InnerText;
                datamodel.ID = Convert.ToInt32(id);
                datamodel.Name = name.Trim();
                dataList.Add(datamodel);
               // data.Add(new { Id = id, Name = name });
            }

          


            return dataList;
        }
        //public class uspCombosGenericos
        //{
        //    public int ID { get; set; }
        //    public string Name { get; set; }

        //    public static List<FormPanelDepartment> getComboGeneral()
        //    {
        //        return new List<FormPanelDepartment>
        //    {
        //        new FormPanelDepartment { ID = 1, Name = "Department A" },
        //        new FormPanelDepartment { ID = 2, Name = "Department B" },
        //        new FormPanelDepartment { ID = 3, Name = "Department C" }
        //    };
        //    }
        //}

 
    }
}