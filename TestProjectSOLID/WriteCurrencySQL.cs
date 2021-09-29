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
    class WriteCurrencySQL
    {
        private string XmlFileString { get; set; }
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private DataContext db;
        private Table<Currency> currencies;


        public bool flagExist = false;
        
        public WriteCurrencySQL(string xmlFileString)
        {
            XmlFileString = xmlFileString;
        }

        public void ToWriteCurrency()
        {
            db = new DataContext(connectionString);
            currencies = db.GetTable<Currency>();
            XDocument xDoc = XDocument.Parse(XmlFileString);
            foreach (XElement element in xDoc.Element("ValCurs").Elements("Valute"))
            {
                flagExist = true;
                if (!IsExistCurrency(element.Element("CharCode").Value.ToString()))
                {
                    ToAddNewCurrency(element);
                }
            }
        }

        private bool IsExistCurrency(string charCode)
        {
            var querry = from cur in db.GetTable<Currency>()
                         where cur.CharCode == charCode
                         select cur.CharCode;
            if (querry.Count() > 0)
                return true;
            return false;
        }

        private void ToAddNewCurrency(XElement element)
        {
            Currency currency = new Currency
            {
                ID = element.Attribute("ID").Value.ToString(),
                NumCode = element.Element("NumCode").Value.ToString(),
                CharCode = element.Element("CharCode").Value.ToString()
            };
            db.GetTable<Currency>().InsertOnSubmit(currency);
            db.SubmitChanges();
        }
    }
}
