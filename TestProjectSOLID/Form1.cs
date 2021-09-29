using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Configuration;
using System.IO;
using ClosedXML.Excel;

namespace TestProjectSOLID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void GetCurrencyRateBtn_Click(object sender, EventArgs e)
        {

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Лист1");

            //создадим заголовки у столбцов
            worksheet.Cell("A" + 1).Value = "Имя";
            worksheet.Cell("B" + 1).Value = "Фамиля";
            worksheet.Cell("C" + 1).Value = "Отчество";
            worksheet.Cell("D" + 1).Value = "Возраст";

            // 

            worksheet.Cell("A" + 2).Value = "Иван";
            worksheet.Cell("B" + 2).Value = "Иванов";
            worksheet.Cell("C" + 2).Value = "Иванович";
            worksheet.Cell("D" + 2).Value = 18;
            //пример изменения стиля ячейки
            worksheet.Cell("B" + 2).Style.Fill.BackgroundColor = XLColor.Red;

            // пример создания сетки в диапазоне
            var rngTable = worksheet.Range("A1:G" + 10);
            rngTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;

            worksheet.Columns().AdjustToContents(); //ширина столбца по содержимому

            workbook.SaveAs("Report.xlsx");


            /*string date = DateSearch.Value.ToShortDateString();
            GetXMLFile getXML = new GetXMLFile(date);
            string xmlFileStr = getXML.ToGetXMLFile();

            WriteCurrencySQL writeCurrencySQL = new WriteCurrencySQL(xmlFileStr);
            writeCurrencySQL.ToWriteCurrency();
            if (!writeCurrencySQL.flagExist)
            {
                // Вывод на экран, что на введённую дату нет данных
                return;
            } else
            {
                WriteRateSQL writeRateSQL = new WriteRateSQL(xmlFileStr, date);
                writeRateSQL.ToWriteRateNewDate();
            }*/


        }
    }
}
