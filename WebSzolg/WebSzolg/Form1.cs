using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using WebSzolg.Entities;
using WebSzolg.MnbServiceReference;

namespace WebSzolg
{
    public partial class Form1 : Form
    {
        BindingList<RateData> rates = new BindingList<RateData>();
        BindingList<string> currencies = new BindingList<string>();
        
        public Form1()
        {
            InitializeComponent();
            
            RefreshData();
        
            dataGridView1.DataSource = rates.ToList();
            chartRateData.DataSource = rates;
            comboBox1.DataSource = currencies.ToList();
        }
        public void RefreshData()
        {
            rates.Clear();
            MNBArfolyamServiceSoapClient mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()

            };
            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rd = new RateData();
                rates.Add(rd);
                rd.Date = DateTime.Parse(element.GetAttribute("date"));

                var akarmi = (XmlElement)element.ChildNodes[0];
                rd.Currency = akarmi.GetAttribute("curr");

                var alapegyseg = decimal.Parse(akarmi.GetAttribute("unit"));
                var value = decimal.Parse(akarmi.InnerText);
                if (alapegyseg != 0) rd.Value = value / alapegyseg;
            }
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            var legend = chartRateData.Legends[0];
            legend.Enabled = false;
            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
