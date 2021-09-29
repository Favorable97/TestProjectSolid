using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestProjectSOLID
{
    class WriteRateSQL
    {
        private string XmlFileString { get; set; }
        private string Date { get; set; }
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private DataContext db;

        public WriteRateSQL(string xmlFileString, string date)
        {
            XmlFileString = xmlFileString;
            Date = date;
        }

        public void ToWriteRateNewDate()
        {
            db = new DataContext(connectionString);
            XDocument xDoc = XDocument.Parse(XmlFileString);
            foreach (XElement element in xDoc.Element("ValCurs").Elements("Valute"))
            {
                if (!IsExistRateInThisDate(element))
                {
                    ToAddNewRateOnThisDate(element);
                } else
                {
                    ToUpdateRateOnThisDate(element);
                }
            }
        }

        private bool IsExistRateInThisDate(XElement element)
        {
            IEnumerable<Rate> rates = db.ExecuteQuery<Rate>(
                "SELECT * " +
                "FROM Currency c join Rate r on c.CurrencyID = r.CurrencyID WHERE convert(char, r.Date, 104) = {0} " +
                "and c.CharCode = {1}", Date, element.Element("CharCode").Value.ToString());
            if (rates.Count() > 0)
                return true;
            return false;
        }

        private void ToAddNewRateOnThisDate(XElement element)
        {
            Rate rate = new Rate
            {
                CurrencyID = GetIDCurrency(element.Element("CharCode").Value.ToString()),
                Date = DateTime.Parse(Date),
                Nominal = Convert.ToInt16(element.Element("Nominal").Value.ToString()),
                Value = Convert.ToDecimal(element.Element("Value").Value.ToString())
            };
            db.GetTable<Rate>().InsertOnSubmit(rate);
            db.SubmitChanges();
        }

        private void ToUpdateRateOnThisDate(XElement element)
        {
            IEnumerable<Rate> rates = db.ExecuteQuery<Rate>("SELECT r.RateID, r.CurrencyID, r.Date, r.Nominal, r.Value " +
                "FROM Currency c join Rate r on c.CurrencyID = r.CurrencyID " +
                "WHERE convert(char, r.Date, 104) = {0} and c.CharCode = {1}", Date, element.Element("CharCode").Value.ToString());
            Rate rate = rates.FirstOrDefault();

            rate.Nominal = Convert.ToInt16(element.Element("Nominal").Value.ToString());
            rate.Value = Convert.ToDecimal(element.Element("Value").Value.ToString());

            db.SubmitChanges();
        }

        private int GetIDCurrency(string charCode)
        {
            var querry = from cur in db.GetTable<Currency>()
                         where cur.CharCode == charCode
                         select cur.CurrencyID;

            return Convert.ToInt32(querry.FirstOrDefault());
        }
    }
}
