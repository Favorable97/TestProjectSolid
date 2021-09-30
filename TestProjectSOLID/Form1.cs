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
            string date = DateSearch.Value.ToShortDateString();
            GetXMLFile getXML = new GetXMLFile(date);
            string xmlFileStr = getXML.ToGetXMLFile();

            if (xmlFileStr == "-1")
            {
                MessageBox.Show("Произошла ошибка во время получения файла с ресурса");
                return;
            }

            WriteCurrencySQL writeCurrencySQL = new WriteCurrencySQL(xmlFileStr);
            writeCurrencySQL.ToWriteCurrency();
            if (!writeCurrencySQL.flagExist)
            {
                MessageBox.Show("На выбранную дату нет данных!");
                return;
            } else
            {
                if (writeCurrencySQL.flagException)
                {
                    MessageBox.Show("Возникла ошибка во время записи валют в базу данных!");
                    return;
                }
                WriteRateSQL writeRateSQL = new WriteRateSQL(xmlFileStr, date);
                writeRateSQL.ToWriteRateNewDate();
                if (writeRateSQL.flagException)
                {
                    MessageBox.Show("Возникла ошибка во время записи котировок в базу данных!");
                    return;
                }
                
                ReportExcel report = new ReportExcel(date);
                report.ToReportExcelFile();
                if (report.flagException)
                {
                    MessageBox.Show("Возникла ошибка во время создания Excel файла!");
                    return;
                }
                else
                {
                    MessageBox.Show("Файл сформирован");
                }
            }
        }
    }
}
