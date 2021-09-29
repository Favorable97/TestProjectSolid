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
    /*
     * Запись в БД информации о валюте
     * На входе XML файл, который парсится
     * Также проверка на существование валюты в БД
     * Если такая валюта есть, то ничего не делается
     * Иначе валюта записывается в БД
     */
    class WriteCurrencySQL
    {
        private string XmlFileString { get; set; }
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private DataContext db;
        private Table<Currency> currencies;


        public bool flagExist = false;
        public bool flagException = false;
        
        public WriteCurrencySQL(string xmlFileString)
        {
            XmlFileString = xmlFileString;
        }

        public void ToWriteCurrency()
        {
            try
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
            } catch
            {
                flagException = true;
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
