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

namespace HouseForRent
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        House_for_RentEntities3 db = new House_for_RentEntities3();
        House hou;
        public void ketnoi()
        {
            //Cach1
            //SqlConnection kn = new SqlConnection(@"Data Source=DESKTOP-1V99NP7\SQLEXPRESS;Initial Catalog=House_for_Rent;Integrated Security=True");
            //SqlDataAdapter cmd = new SqlDataAdapter(@"Select h.*,t.* from House h, Tenant t where  h.id = t.House_id", kn);
            //kn.Open();
            //DataTable dt = new DataTable();
            //cmd.Fill(dt);
            //dtgView.DataSource = dt;

            //Ket noi database lay ra 2 bang cho vao DatagridView
            var result = (from h in db.Houses
                          join t in db.Tenants
                          on h.Id equals t.House_id
                          select new
                          {
                              Id = h.Id,
                              House = h.House1,
                              Style = h.Style,
                              Price = h.Price,  
                              Date = t.Date,
                              Elec = h.Elec,
                              Water = h.Water,
                              Wifi = h.Wifi,
                              Other = h.Wifi,
                              Total = h.Total,
                              Id1 = t.Id,
                              House_id = t.Id,
                              Hoten = t.Hoten,
                              Gioitinh = t.Gioitinh,
                              Quequan = t.Quequan,
                              SoCMND = t.SoCMND,
                              Ngaysinh = t.Ngaysinh,
                              Phone = t.Phone,
                              Dicchi = t.Dicchi,
                              Photo = t.Photo }).ToList();
            dtgView.DataSource = result;
            dtgView.Refresh();
            dtgView.Columns["Total"].Visible = false;
            dtgView.Columns["Elec"].Visible = false;
            dtgView.Columns["Water"].Visible = false;
            dtgView.Columns["Wifi"].Visible = false;
            dtgView.Columns["Other"].Visible = false;
            dtgView.Columns["Gioitinh"].Visible = false;
            dtgView.Columns["Ngaysinh"].Visible = false;
            dtgView.Columns["Quequan"].Visible = false;
            dtgView.Columns["SoCMND"].Visible = false;
            dtgView.Columns["Phone"].Visible = false;
            dtgView.Columns["Photo"].Visible = false;
            dtgView.Columns["Hoten"].Visible = false;
            dtgView.Columns["Id"].Visible = false;
            dtgView.Columns["Id1"].Visible = false;
            dtgView.Columns["House_id"].Visible = false;
            dtgView.Columns["Dicchi"].Visible = false;
            // Khong duoc chon nhieu cot
            this.dtgView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dtgView.MultiSelect = false;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có thật sự muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            Application.Exit();
        }

        private void mniCreate_Click(object sender, EventArgs e)
        {
            FrmHD HD = new FrmHD();
            HD.ShowDialog();
            FrmMain_Load(null, null);
            ketnoi();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Enable();
            ketnoi();
        }

        private void mniInfo_Click(object sender, EventArgs e)
        {
            FrmHelp help = new FrmHelp();
            help.ShowDialog();
            FrmMain_Load(null, null);
        }

        private void dtgView_SelectionChanged(object sender, EventArgs e)
        {

        }
        private void ButtonDis()
        {
            btnedit.Enabled = true;
            btnRe.Enabled = true;
        }
        private void Disable()
        {
            btnsave.Enabled = true;     
            txtgia.Enabled = true;
            txtnuoc.Enabled = true;
            txtdien.Enabled = true;
            txtwifi.Enabled = true;
            txtother.Enabled = true;
        }
        private void Enable()
        {
            btnedit.Enabled = false;
            btnsave.Enabled = false;
            btnRe.Enabled = false;
            txtgia.Enabled = false;
            txtnuoc.Enabled = false;
            txtdien.Enabled = false;
            txtwifi.Enabled = false;
            txtother.Enabled = false;
            txttotal.Enabled = false;
            txtgia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtnuoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtdien.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtwifi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txtother.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txttotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }
       
        private void ShowTenant()
        {
            if (dtgView.SelectedRows.Count == 1) // if select only one row
            {
                var row = dtgView.SelectedRows[0]; // get the selected row
                var cell = row.Cells["Id"]; // get the id cell of the row
                DateTime id = (DateTime)cell.Value; // get the id value from the cell
                dtgView.RefreshEdit(); // refresh the list after editing
                this.ketnoi();
            }
        }

        private void dtgView_MouseClick(object sender, MouseEventArgs e)
        {
            if (dtgView.SelectedRows.Count == 1) // if select only one row
            {
                var row = dtgView.SelectedRows[0]; // get the selected row
                var cell = row.Cells["Id"]; // get the id cell of the row
                DateTime id = (DateTime)cell.Value; // get the id value from the cell
                dtgView.RefreshEdit(); // refresh the list after editing
                //Select each row
                lblt001.Text = dtgView.SelectedRows[0].Cells[1].Value.ToString();
                lblloai.Text = dtgView.SelectedRows[0].Cells[2].Value.ToString();
                txtgia.Text = dtgView.SelectedRows[0].Cells[3].Value.ToString();
                txtdien.Text = dtgView.SelectedRows[0].Cells[5].Value.ToString();
                txtnuoc.Text = dtgView.SelectedRows[0].Cells[6].Value.ToString();
                txtwifi.Text = dtgView.SelectedRows[0].Cells[7].Value.ToString();
                txtother.Text = dtgView.SelectedRows[0].Cells[8].Value.ToString();
                txttotal.Text = dtgView.SelectedRows[0].Cells[9].Value.ToString();
                lbltenant2.Text = dtgView.SelectedRows[0].Cells[12].Value.ToString();
                lblname.Text = dtgView.SelectedRows[0].Cells[12].Value.ToString();
                lblgt.Text = dtgView.SelectedRows[0].Cells[13].Value.ToString();
                lblcmnd.Text = dtgView.SelectedRows[0].Cells[15].Value.ToString();
                lblsdt.Text = dtgView.SelectedRows[0].Cells[17].Value.ToString();
                lblqq.Text = dtgView.SelectedRows[0].Cells[14].Value.ToString();
                lbltt.Text = dtgView.SelectedRows[0].Cells[18].Value.ToString();
                //pictureBox.Image = (Image)dtgView.SelectedRows[0].Cells[18].Value;
                ////Tenant upload = (Tenant)dtgView.SelectedRows[0].DataBoundItem;
                //ImageConverter objImageConverter = new ImageConverter();
                //pictureBox.Image = (Image)objImageConverter.ConvertFrom(te.Photo);
                //pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Enable();
                ButtonDis();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var row = dtgView.SelectedRows[0];
                var cell = row.Cells["Id"];
                DateTime Id = (DateTime)cell.Value;
                hou = db.Houses.Single(p => p.Id == Id);
                string strNha = lblnha.Text;
            txtnuoc.Text = "0";
            txtdien.Text = "0";
            txtwifi.Text = "0";
            txtother.Text = "0";
            string pri = txtgia.Text;
            int f_pri = int.Parse(pri);
            //Refresh
            int f_elec = int.Parse(txtdien.Text);

            int f_water = int.Parse(txtnuoc.Text);

            int f_wifi = int.Parse(txtwifi.Text);

            int f_other = int.Parse(txtother.Text);
            int f_total = (f_elec + f_water + f_wifi + f_other + f_pri);
            txttotal.Text = f_total.ToString();
            String strhou = lblnha.Text;
            hou.House1 = strhou;
            hou.Elec = f_elec;
            hou.Price = f_pri;
            hou.Water = f_water;
            hou.Wifi = f_wifi;
            hou.Other = f_other;
            hou.Total = f_total;
            db.Entry(hou).State = EntityState.Modified;
            db.SaveChanges();
            MessageBox.Show("Refresh successfully!");
            ShowTenant();
        }
        private void btnedit_Click(object sender, EventArgs e)
        {
            if (dtgView.SelectedRows.Count == 1)
            {
                Disable();
                var row = dtgView.SelectedRows[0];
                var cell = row.Cells["Id"];
                DateTime Id = (DateTime)cell.Value;
                hou = db.Houses.Single(p => p.Id == Id);
                string strNha = lblnha.Text;
        }
    }
        private void btnsave_Click(object sender, EventArgs e)
        {
            Enable();
            btnRe.Enabled = true;
            string pri = txtgia.Text;
            int f_pri = int.Parse(pri);
            string elec = txtdien.Text;
            int f_elec = int.Parse(elec);
            string water = txtnuoc.Text;
            int f_water = int.Parse(water);
            string wifi = txtwifi.Text;
            int f_wifi = int.Parse(wifi);
            string other = txtother.Text;
            int f_other = int.Parse(other);
            int f_total = (f_elec + f_water + f_wifi + f_other + f_pri);
            txttotal.Text = f_total.ToString();
            String strhou = lblnha.Text;
            hou.House1 = strhou;
            hou.Elec = f_elec;
            hou.Price = f_pri;
            hou.Water = f_water;
            hou.Wifi = f_wifi;
            hou.Other = f_other;
            hou.Total = f_total;
            db.Entry(hou).State = EntityState.Modified;
            db.SaveChanges();
            MessageBox.Show("Edit successfully!");
            ShowTenant();
        }
        private void txtnuoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtdien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtwifi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtother_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void dtgView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}

