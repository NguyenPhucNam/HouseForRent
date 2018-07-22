using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HouseForRent
{
    public partial class FrmHD : Form
    {
        public FrmHD()
        {
            InitializeComponent();
        }
        //Database
        House_for_RentEntities3 db = new House_for_RentEntities3();
        //Table Tenant vs House
        Tenant te = new Tenant();
        private void FrmHD_Load(object sender, EventArgs e)
        {
            nhatrong();
            List<string> list = new List<string>() { "Nữ", "Nam" };
            cbogt1.DataSource = list;
            cbogt2.DataSource = list;
            Enable();
        }
        private void Enable()
        {
            // Enable Ben A
            txtName.Enabled = false;
            txtNS.Enabled = false;
            txtQQ.Enabled = false;
            txtSđt.Enabled = false;
            cbogt1.Enabled = false;
            txtCMND.Enabled = false;
            txtDC.Enabled = false;
            pictureBox.Enabled = false;
            // Enable Ben B
            txtnam2.Enabled = false;
            txtngsinh.Enabled = false;
            txttt.Enabled = false;
            txtdcct.Enabled = false;
            txttt.Enabled = false;
            txtsdt.Enabled = false;
            cbogt2.Enabled = false;
        }
        private void Disable()
        {
            // Enable Ben A
            txtName.Enabled = true;
            txtNS.Enabled = true;
            txtQQ.Enabled = true;
            txtSđt.Enabled = true;
            cbogt1.Enabled = true;
            txtCMND.Enabled = true;
            txtDC.Enabled = true;
            pictureBox.Enabled = true;
            // Enable Ben B
            txtnam2.Enabled = true;
            txtngsinh.Enabled = true;
            txttt.Enabled = true;
            txtdcct.Enabled = true;
            txttt.Enabled = true;
            txtsdt.Enabled = false;
            cbogt2.Enabled = true;
        }
        private void nhatrong()
        {
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
                              Id1 = t.Id,
                              House_id = t.Id,
                              Hoten = t.Hoten,
                              Gioitinh = t.Gioitinh,
                              Quequan = t.Quequan,
                              SoCMND = t.SoCMND,
                              Ngaysinh = t.Ngaysinh,
                              Phone = t.Phone,
                              Dicchi = t.Dicchi,
                              Photo = t.Photo
                          }).ToList();
            dtgView1.DataSource = result;
            dtgView1.Refresh();
            dtgView1.Columns["Gioitinh"].Visible = false;
            dtgView1.Columns["Ngaysinh"].Visible = false;
            dtgView1.Columns["Quequan"].Visible = false;
            dtgView1.Columns["SoCMND"].Visible = false;
            dtgView1.Columns["Phone"].Visible = false;
            dtgView1.Columns["Photo"].Visible = false;
            dtgView1.Columns["Hoten"].Visible = false;
            dtgView1.Columns["Id"].Visible = false;
            dtgView1.Columns["Id1"].Visible = false;
            dtgView1.Columns["House_id"].Visible = false;
            dtgView1.Columns["Dicchi"].Visible = false;
            dtgView1.Columns["Date"].Visible = false;
            dtgView1.Columns["Price"].Visible = false;
            this.dtgView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dtgView1.MultiSelect = false;
        }

        private void dtgView1_MouseClick(object sender, MouseEventArgs e)
        {
            //Add value to Textbox, combobox from DatagridView
            lblnha.Text = dtgView1.SelectedRows[0].Cells[1].Value.ToString();
            lblgia.Text = dtgView1.SelectedRows[0].Cells[3].Value.ToString();
            txtName.Text = dtgView1.SelectedRows[0].Cells[7].Value.ToString();
            txtQQ.Text = dtgView1.SelectedRows[0].Cells[9].Value.ToString();
            cbogt1.Text = dtgView1.SelectedRows[0].Cells[8].Value.ToString();
            txtCMND.Text = dtgView1.SelectedRows[0].Cells[10].Value.ToString();
            txtDC.Text = dtgView1.SelectedRows[0].Cells[13].Value.ToString();
            txtSđt.Text = dtgView1.SelectedRows[0].Cells[12].Value.ToString();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Stringtv();
            Disable();

        }
        private void Stringtv()
        {
            txtName.Text = "N/A";
            txtNS.Text = "N/A";
            txtQQ.Text = "N/A";
            txtCMND.Text = "0";           
            txtDC.Text = "N/A";
            txtSđt.Text = "N/A";
            cbogt1.Text = "N/A";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtgView1.SelectedRows.Count == 1)
            {
                var row = dtgView1.SelectedRows[0];
                var cell = row.Cells["Id"];
                DateTime Id = (DateTime)cell.Value;
                te = db.Tenants.Single(p => p.House_id == Id);
                string name = this.txtName.Text, // get inputted value from textbox         
                    NS = txtngsinh.Text,
                    GT = cbogt1.Text,
                    QQ = txtQQ.Text,
                    CMND = txtCMND.Text,
                    DC = txtDC.Text,
                    Sdt = txtSđt.Text;
                DateTime time = dtPa.Value;
                DateTime NgSinh = System.Convert.ToDateTime(NS);// Convert String to Datetime
                int SoCMND = System.Convert.ToInt32(CMND);// Convert String to Integer
                te.House_id = Id;
                te.Hoten = name; // then add student object to database
                te.Gioitinh = GT;
                te.Ngaysinh = NgSinh;
                te.Quequan = QQ;
                te.SoCMND = SoCMND;
                te.Dicchi = DC;
                te.Phone = Sdt;
                te.Date = time;
                ImageConverter converter = new ImageConverter();
                byte[] image = (byte[])converter.ConvertTo(pictureBox.Image, typeof(byte[]));
                te.Photo = image;
                db.Entry(te).State = EntityState.Modified;
                db.SaveChanges(); // commit the command
                this.Close(); // close the window and show message
                MessageBox.Show("Successfully!");     
            }
        }
        private void txtCMND_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Disable();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            pictureBox.ImageLocation = open.FileName;
        }
    }
}
