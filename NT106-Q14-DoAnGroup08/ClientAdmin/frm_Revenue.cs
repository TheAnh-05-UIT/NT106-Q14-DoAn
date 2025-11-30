using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;

namespace NT106_Q14_DoAnGroup08.ClientAdmin
{
    public class RevenueDataResponse
    {
        public DataTable Details { get; set; }
        public DataTable ChartData { get; set; }
        public string ChartTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalRevenue { get; set; }
    }
    public partial class frm_Revenue : Form
    {
        private readonly ExcelExportService excelExportService;

        public frm_Revenue()
        {
            InitializeComponent();
            excelExportService = new ExcelExportService();
        }

        private void frm_Revenue_Load(object sender, EventArgs e)
        {
            cboMode.SelectedIndex = 0;
            UpdateDatePickerVisibility();
            ResetForm();
        }

        private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDatePickerVisibility();
        }

        private void UpdateDatePickerVisibility()
        {
            dtWeekDate.Visible = cboMode.SelectedIndex <= 1; 
            dtMonth.Visible = cboMode.SelectedIndex == 2; 
            dtYear.Visible = cboMode.SelectedIndex == 3; 
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = GetSelectedDate();
                var request = new
                {
                    action = "FILTER_REVENUE",
                    data = new
                    {
                        Mode = cboMode.SelectedIndex,
                        SelectedDate = selectedDate
                    }
                };

                string jsonRequest = JsonConvert.SerializeObject(request);
                string jsonResponse = ServerConnection.SendRequest(jsonRequest);
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);
                if (response.status == "success")
                {
                    var responseData = response.data.ToObject<RevenueDataResponse>();

                    dgvSales.DataSource = responseData.Details;

                    UpdateChart(responseData);

                    lblTotal.Text = responseData.TotalRevenue.ToString("N0") + " VND";
                }
                else
                {
                    MessageBox.Show($"Lỗi từ server: {response.message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        private void UpdateChart(RevenueDataResponse response)
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
            var result = new Dictionary<string, int>();
            var groupedData = dt.AsEnumerable()
                                .GroupBy(row => row.Field<DateTime>("Ngày").Hour)
                                .Select(g => new {
                                    Gio = g.Key,
                                    TongTien = g.Sum(row => Convert.ToInt32(row["Số tiền (VND)"]))
                                })
                                .OrderBy(x => x.Gio);
            foreach (var item in groupedData)
            {
                result.Add($"{item.Gio}h", item.TongTien);
            }
            if (!result.Any())
            {
                result.Add("Không có dữ liệu", 0);
            }
            foreach (var item in result)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
        }

        private void RenderWeekChart(Series series, DataTable dt, DateTime startOfWeek)
        {
            var result = new Dictionary<string, int>();
            string[] dayNames = { "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy", "Chủ Nhật" };

            for (int i = 0; i < 7; i++)
            {
                DateTime date = startOfWeek.AddDays(i);
                result.Add($"{dayNames[i]} ({date.Day}/{date.Month})", 0);
            }

            foreach (DataRow row in dt.Rows)
            {
                DateTime date = Convert.ToDateTime(row["Ngày"]);
                int revenue = Convert.ToInt32(row["Tổng tiền"]);
                int dayIndex = ((int)date.DayOfWeek - 1 + 7) % 7;
                string key = $"{dayNames[dayIndex]} ({date.Day}/{date.Month})";
                if (result.ContainsKey(key))
                {
                    result[key] = revenue;
                }
            }
            foreach (var item in result)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
        }

        private void RenderMonthChart(Series series, DataTable dt, DateTime date)
        {
            var result = new Dictionary<int, int>();
            int daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            for (int i = 1; i <= daysInMonth; i++)
            {
                result.Add(i, 0);
            }

            foreach (DataRow row in dt.Rows)
            {
                int day = Convert.ToInt32(row["Ngày"]);
                int revenue = Convert.ToInt32(row["Tổng tiền"]);
                if (result.ContainsKey(day))
                {
                    result[day] = revenue;
                }
            }
            foreach (var item in result.OrderBy(x => x.Key))
            {
                series.Points.AddXY(item.Key.ToString(), item.Value);
            }
        }

        private void RenderYearChart(Series series, DataTable dt)
        {
            var result = new Dictionary<string, int>();
            string[] monthNames = { "", "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                                   "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };

            for (int i = 1; i <= 12; i++)
            {
                result.Add(monthNames[i], 0);
            }

            foreach (DataRow row in dt.Rows)
            {
                int month = Convert.ToInt32(row["Tháng"]);
                int revenue = Convert.ToInt32(row["Tổng tiền"]);
                result[monthNames[month]] = revenue;
            }
            foreach (var item in result)
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
            dgvSales.DataSource = null;
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
        private void RevenueChart(object sender, EventArgs e) { }
        private void dgvSales_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void chartRevenue_Click(object sender, EventArgs e) { }
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