using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TcpServer;
using TcpServer.Handlers;
using Excel = Microsoft.Office.Interop.Excel;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public partial class frm_Revenue : Form
    {
        private readonly RevenueHandler revenueHandler;
        private readonly ExcelExportService excelExportService;
        private const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QuanLyQuanNet;Integrated Security=True";

        public frm_Revenue()
        {
            InitializeComponent();
            revenueHandler = new RevenueHandler(CONNECTION_STRING);
            excelExportService = new ExcelExportService();
        }

        private void frm_Revenue_Load(object sender, EventArgs e)
        {
            cboMode.SelectedIndex = 0;
            UpdateDatePickerVisibility();
            ResetForm(); 
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDatePickerVisibility();
        }

        private void UpdateDatePickerVisibility()
        {
            dtWeekDate.Visible = cboMode.SelectedIndex <= 1; // Day or Week
            dtMonth.Visible = cboMode.SelectedIndex == 2; // Month
            dtYear.Visible = cboMode.SelectedIndex == 3; // Year
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = GetSelectedDate();
                var request = new RevenueFilterRequest
                {
                    Mode = cboMode.SelectedIndex,
                    SelectedDate = selectedDate
                };
                RevenueResponse response = revenueHandler.HandleRevenueFilter(request);
               
                dgvSales.DataSource = response.Details;
              
                UpdateChart(response);
               
                lblTotal.Text = response.TotalRevenue.ToString("N0") + " VND";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DateTime GetSelectedDate()
        {
            switch (cboMode.SelectedIndex)
            {
                case 0:
                case 1:
                    return dtWeekDate.Value;
                case 2:
                    return dtMonth.Value;
                case 3:
                    return dtYear.Value;
                default:
                    return DateTime.Now;
            }
        }

        private void UpdateChart(RevenueResponse response)
        {
            try
            {
                chartRevenue.Titles.Clear();
                chartRevenue.Titles.Add(response.ChartTitle);
                chartRevenue.Series.Clear();
                Series series = new Series("Tổng Doanh thu");
                series.IsValueShownAsLabel = true;
                switch (cboMode.SelectedIndex)
                {
                    case 0: // Day
                        series.ChartType = SeriesChartType.Column;
                        RenderDayChart(series, response.ChartData);
                        chartRevenue.ChartAreas[0].AxisX.Title = "Giờ";
                        break;
                    case 1: // Week
                        series.ChartType = SeriesChartType.Column;
                        RenderWeekChart(series, response.ChartData, response.StartDate);
                        chartRevenue.ChartAreas[0].AxisX.Title = "Ngày trong Tuần";
                        chartRevenue.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                        break;
                    case 2: // Month
                        series.ChartType = SeriesChartType.Line;
                        RenderMonthChart(series, response.ChartData, response.StartDate);
                        chartRevenue.ChartAreas[0].AxisX.Title = "Ngày trong Tháng";
                        chartRevenue.ChartAreas[0].AxisX.Interval = 1;
                        break;
                    case 3: // Year
                        series.ChartType = SeriesChartType.Column;
                        RenderYearChart(series, response.ChartData);
                        chartRevenue.ChartAreas[0].AxisX.Title = "Tháng";
                        break;
                }
                chartRevenue.Series.Add(series);
                chartRevenue.ChartAreas[0].AxisY.Title = "Số tiền (VND)";
                chartRevenue.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                chartRevenue.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo biểu đồ: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderDayChart(Series series, DataTable dt)
        {
            var chartData = ChartDataHelper.PrepareChartDataForDay(dt);
            foreach (var item in chartData)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
        }

        private void RenderWeekChart(Series series, DataTable dt, DateTime startOfWeek)
        {
            var chartData = ChartDataHelper.PrepareChartDataForWeek(dt, startOfWeek);
            foreach (var item in chartData)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
        }

        private void RenderMonthChart(Series series, DataTable dt, DateTime date)
        {
            var chartData = ChartDataHelper.PrepareChartDataForMonth(dt, date.Year, date.Month);
            foreach (var item in chartData.OrderBy(x => x.Key))
            {
                series.Points.AddXY(item.Key.ToString(), item.Value);
            }
        }

        private void RenderYearChart(Series series, DataTable dt)
        {
            var chartData = ChartDataHelper.PrepareChartDataForYear(dt);
            foreach (var item in chartData)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Lưu file Excel",
                Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls",
                DefaultExt = "xlsx"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    excelExportService.ExportToExcel(dgvSales, saveFileDialog.FileName);
                    MessageBox.Show("Xuất file thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xuất file: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            RevenueResponse response = revenueHandler.HandleRefresh();
            dgvSales.DataSource = response.Details;
            chartRevenue.Titles.Clear();
            chartRevenue.Series.Clear();
            chartRevenue.ChartAreas[0].AxisX.Title = "";
            chartRevenue.ChartAreas[0].AxisY.Title = "";
            lblTotal.Text = "0 VND";
            cboMode.SelectedIndex = 0;
            UpdateDatePickerVisibility();
            dtWeekDate.Value = DateTime.Today;
            dtMonth.Value = DateTime.Today;
            dtYear.Value = DateTime.Today;
        }

        private void RevenueChart(object sender, EventArgs e)
        {
           
        }

        private void dgvSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void chartRevenue_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            ResetForm();
        }
    }

    public class ExcelExportService
    {
        public void ExportToExcel(DataGridView dgv, string fileName)
        {
            var application = new Microsoft.Office.Interop.Excel.Application();
            application.Application.Workbooks.Add(Type.Missing);
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                application.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
            }
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    application.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value;
                }
            }
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(fileName);
            application.ActiveWorkbook.Saved = true;
            application.Quit();
        }
    }
}