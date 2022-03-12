using ProvisionDataLayer.Context;
using ProvisionDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProvisionBusinessLayer.Service
{
    public class CurrencyCodeService
    {
        public static string Add()
        {
            using (var db = new SystemContext())
            {


                string address = "https://www.tcmb.gov.tr/kurlar/today.xml";
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(address);

                XmlNodeList list = xmlDocument.SelectNodes("Tarih_Date/Currency[@Kod]");
                List<ProvisionDataLayer.Models.CurrencyCode> paramList = new List<ProvisionDataLayer.Models.CurrencyCode>(); ;
                for (int i = 0; i < list.Count; i++)
                {
                    string param = list[i].OuterXml.Split(@"Kod=")[1].Substring(1, 3);
                    var status = db.CurrencyCodes.Where(x => x.Code == param).FirstOrDefault();
                    if (status == null)
                    {
                        ProvisionDataLayer.Models.CurrencyCode model = new ProvisionDataLayer.Models.CurrencyCode();
                        model.Code = param;
                        db.Add(model);
                        db.SaveChanges();
                    }
                }
                return ("Para Birimleri Aktarıldı");
            }
        }

        public static List<CurrencyCode> List()
        {
            using (var db=new SystemContext())
            {
                var list = db.CurrencyCodes.ToList();
                return list;
            }
        }
    }
}
