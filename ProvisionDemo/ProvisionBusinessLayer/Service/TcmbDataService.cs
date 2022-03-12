using ProvisionBusinessLayer.ViewModels;
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
    public class TcmbDataService
    {
        public static string Add(SystemContext db,string code)
        {
            List<TcmbData> listModel = new List<TcmbData>();
            DateTime todayTime = DateTime.Now;
            DateTime twoMonth = todayTime.AddMonths(-2);
            TimeSpan totalDaysTime = todayTime - twoMonth;
            DateTime date;
            code = code.ToUpper();
            int totalDays = Convert.ToInt32(totalDaysTime.TotalDays);
            string year, month, day;

            for (int i = 0; i < totalDays; i++)
            {
                try
                {
                    date = todayTime.AddDays(-i);
                    year = date.Year.ToString();
                    month = date.Month.ToString();
                    day = date.Day.ToString();
                    if (Convert.ToInt32(month) < 10)
                    {
                        month = "0" + month;
                    }
                    if (Convert.ToInt32(day) < 10)
                    {
                        day = "0" + day;
                    }
                    string exchangeRate = " https://www.tcmb.gov.tr/kurlar/" + year + month + "/" + day + month + year + ".xml";
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(exchangeRate);


                    TcmbData model = new TcmbData();

                    model.Code = code;
                    model.Date = date;
                    model.Unit = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='" + code + "']/Unit").InnerXml;
                    model.Isim = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='" + code + "']/Isim").InnerXml;
                    model.CurrencyName = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='" + code + "']/CurrencyName").InnerXml;
                    model.ForexBuying = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='" + code + "']/ForexBuying").InnerXml;
                    model.ForexSelling = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='" + code + "']/ForexSelling").InnerXml;
                    model.BanknoteBuying = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='" + code + "']/BanknoteBuying").InnerXml;
                    model.BanknoteSelling = xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='" + code + "']/BanknoteSelling").InnerXml;

                    var status = db.TcmbDatas.Where(x => x.Code == code && x.ForexBuying == model.ForexBuying && x.ForexSelling == model.ForexSelling && x.BanknoteBuying == model.BanknoteBuying && x.BanknoteSelling == model.BanknoteSelling).FirstOrDefault();
                    if (status == null)
                    {
                        db.TcmbDatas.Add(model);
                        db.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    int errorMsg = i;
                }
            }
            return ("Geçmiş İki Aylık Veri Çekildi");
        }

        public static List<TCMBViewModel> List(SystemContext db,string code)
        {
            List<TCMBViewModel> listModel = new List<TCMBViewModel>();
            DateTime zaman = DateTime.Now.AddMonths(-2);
            var list = db.TcmbDatas.Where(x => x.Code == code && x.Date > zaman).ToList();
            foreach (var item in list)
            {
                TCMBViewModel model = new TCMBViewModel();
                model.BanknoteBuying = item.BanknoteBuying;
                model.BanknoteSelling = item.BanknoteSelling;
                model.CurrencyName = item.CurrencyName;
                model.ForexSelling = item.ForexSelling;
                model.ForexBuying = item.ForexBuying;
                model.Date = item.Date.ToString("yyyy-MM-dd");
                listModel.Add(model);
            }

            return listModel;
        }
    }
}
