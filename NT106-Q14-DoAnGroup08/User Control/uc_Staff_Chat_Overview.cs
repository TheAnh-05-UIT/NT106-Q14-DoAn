using Newtonsoft.Json;
using NT106_Q14_DoAnGroup08.ConnectionServser;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NT106_Q14_DoAnGroup08.Uc_Staff
{
    public partial class uc_Staff_Chat_Overview : UserControl
    {
        Action<string, string> tabCreateHandler;
        public uc_Staff_Chat_Overview(Action<string, string> tabCreateHandler)
        {
            InitializeComponent();
            this.tabCreateHandler = tabCreateHandler;
            listBox1.MouseDown += (s, e) =>
            {
                if (listBox1.IndexFromPoint(new Point(e.X, e.Y)) < 0) listBox1.ClearSelected();
            };
            LoadComputerList();
        }

        private void LoadComputerList()
        {
            try
            {
                var request = new { action = "GET_ALL_COMPUTERS" };
                string jsonResponse = ServerConnection.SendRequest(JsonConvert.SerializeObject(request));
                dynamic response = JsonConvert.DeserializeObject(jsonResponse);

                if (response.status == "success")
                {
                    DataTable dt = response.data.ToObject<DataTable>();
                    listBox1.Items.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        string computerName = row["ComputerName"].ToString();
                        string computerId = row["ComputerId"].ToString();
                        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
                        {
                            if (listBox1.SelectedItem == null) return;
                            if (listBox1.SelectedItem.ToString() == computerName)
                            {
                                tabCreateHandler?.Invoke(computerId, computerName);
                            }
                        }
                        listBox1.Items.Add(computerName);
                        listBox1.DoubleClick += new EventHandler(listBox1_SelectedIndexChanged);
                    }
                }
                else
                {
                    MessageBox.Show("Lỗi tải danh sách máy: " + response.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }
    }
}
