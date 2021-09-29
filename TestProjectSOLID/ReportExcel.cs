using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ClosedXML.Excel;
using System.Xml.Linq;
using System.Data.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace TestProjectSOLID
{
    /*
     * Класс предназначен для выгрузки данных в Excel
     * Получаем дату, на которую надо выгрузить
     * Выгружаем сначала рубли, т.к. их нет в файле, далее остальные валюты.
     */
    class ReportExcel
    {
        private string Date { get; set; }
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private DataContext db;

        public bool flagException = false;

        public ReportExcel(string date)
        {
            Date = date;
        }

        public void ToReportExcelFile()
        {
            try
            {
                db = new DataContext(connectionString);
                DataSet tableInfo = GetElementsAboutThisDataRub();
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("RUB");

                foreach (DataTable dt in tableInfo.Tables)
                {
                    worksheet.Cell(1, 1).InsertTable(dt);
                    worksheet.Columns().AdjustToContents();
                }

                List<string> charCodeList;
                List<Int16> nominalList;
                List<decimal> valueList;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    GetMasWithParam(out charCodeList, out nominalList, out valueList);
                    for (int i = 0; i < charCodeList.Count; i++)
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter("select c.CharCode as 'Аббревиатура валюты', r.Nominal as 'Номинал', cast(r.Value / @val as decimal(18, 4)) as 'Значение' " +
                                        "from currency c join rate r on c.CurrencyID = r.CurrencyID where r.Date = @date and c.CharCode != @code ", con))
                        {
                            adapter.SelectCommand.Parameters.AddWithValue("@date", Date);
                            adapter.SelectCommand.Parameters.AddWithValue("@code", charCodeList[i]);
                            adapter.SelectCommand.Parameters.AddWithValue("@val", valueList[i]);

                            DataSet ds = new DataSet();
                            adapter.Fill(ds);

                            var workSheetAdd = workbook.Worksheets.Add(charCodeList[i]);

                            foreach (DataTable dt in ds.Tables)
                            {
                                workSheetAdd.Cell(1, 1).InsertTable(dt);
                                workSheetAdd.Columns().AdjustToContents();
                            }
                        }
                    }


                }



                string fileName = String.Format("{0}{1}{2}.xlsx", Date.Split('.')[2], Date.Split('.')[1], Date.Split('.')[0]);

                workbook.SaveAs(fileName);
            }
            catch
            {
                flagException = true;
            }
            
        }

        private DataSet GetElementsAboutThisDataRub()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter("select c.CharCode as 'Аббревиатура валюты', r.Nominal as 'Номинал', r.Value as 'Значение' " +
                    "from currency c join rate r on c.CurrencyID = r.CurrencyID where r.Date = @date " +
                    "order by c.CurrencyID", con))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@date", Date);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    return ds;
                }
            }
        }

        private void GetMasWithParam(out List<string> charCodeList, out List<Int16> nominalList, out List<decimal> valueList)
        {
            charCodeList = new List<string>();
            nominalList = new List<Int16>();
            valueList = new List<decimal>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("select c.CharCode, r.Nominal, r.Value " +
                    "from currency c join rate r on c.CurrencyID = r.CurrencyID where r.Date = @date " +
                    "order by c.CurrencyID", con))
                {
                    com.Parameters.AddWithValue("@date", Date);

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                charCodeList.Add(reader.GetString(0));
                                nominalList.Add((Int16)reader.GetInt16(1));
                                valueList.Add((decimal)reader.GetDecimal(2));
                            }
                        }
                    }
                }
            }
            
        }


    }
}
