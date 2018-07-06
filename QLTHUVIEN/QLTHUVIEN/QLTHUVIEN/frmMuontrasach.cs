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

namespace QLTHUVIEN
{
    public partial class frmMuontrasach : Form
    {
        SqlCommand cm;
        public frmMuontrasach()
        {
            InitializeComponent();
        }
        public void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select * from phieumuon";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvmuontra.DataSource = dt;
            cn.CloseConn();
        }
        public void Loadmasach()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select masach from sach", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmasach.DataSource = ds.Tables[0];
            cbmasach.ValueMember = "masach";
        }
        public void Loadmanhanvien()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select manhanvien from nhanvien", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmanhanvien.DataSource = ds.Tables[0];
            cbmanhanvien.ValueMember = "manhanvien";
        }
        public void Loadmamadg()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select madocgia from docgia", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmadg.DataSource = ds.Tables[0];
            cbmadg.ValueMember = "madocgia";
        }
        public DataTable danhsachphieumuon()
        {
            Connection cn = new Connection();
            SqlDataAdapter da = new SqlDataAdapter("select madocgia as 'Mã đọc giả',ngaymuon as 'Ngày mượn',masach as 'Mã sách',manhanvien as 'Mã nhân viên',soluong as 'Số lượng',trangthai as 'Trạng thái',ngaytra as 'Ngày trả' from PHIEUMUON", cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void btnLammoi_Click(object sender, EventArgs e)
        {
            dtpngaynuon.Value = DateTime.Today;
            cbmanhanvien.Text = "";
            cbmasach.Text = "";
          //  txtsoluong.Clear();
            cbmtrangthai.Text = "ĐANG MUON";
            dtpNgaytra.Value = DateTime.Today;
            frmMuontrasach_Load(sender, e);
        }

        private void frmMuontrasach_Load(object sender, EventArgs e)
        {
            hienthi();
            dtpngaynuon.Value = DateTime.Today;
            dtpNgaytra.Value = DateTime.Today;
            cbmtrangthai.Text = "ĐANG MUON";
            Loadmanhanvien();
            Loadmasach();
            Loadmamadg();
            dgvmuontra.DataSource = danhsachphieumuon();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string madocgia = cbmadg.Text;
            DateTime ngaymuon = dtpngaynuon.Value;
            string masach = cbmasach.Text;
            string manhanvien = cbmanhanvien.Text;
            string soluong = txtsoluong.Text;
            string trangthai = cbmtrangthai.Text;
            DateTime ngaytra = dtpNgaytra.Value;
           // cm = new SqlCommand("select madocgia from phieumuon where madocgia='" + madocgia + "'", cn.con);
            //string ma = cm.ExecuteScalar() as string;
            if (madocgia == "" || masach == "" || manhanvien == "" || soluong == "" || trangthai == "")
            {
                MessageBox.Show("Thông tin mượn trả không được bỏ trống!!!");
            }
            else
            if (ngaymuon > ngaytra)
            {
                MessageBox.Show("Ngày trả phải sau ngày mượn");
            }
            //else
            //if (madocgia == ma)
            //{
            //    MessageBox.Show("Trùng mã độc giả, thêm thất bại!!!");
            //}
            else
            {
                string sqlthem = "insert into phieumuon values('" + madocgia + "','" + ngaymuon + "','" + masach + "','" + manhanvien + "','" + soluong + "','" + trangthai + "','" + ngaytra + "')";
                SqlCommand cmd = new SqlCommand(sqlthem, cn.con);
                try
                {
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thành công");
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            hienthi();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string madocgia = cbmadg.Text;
            DateTime ngaymuon = dtpngaynuon.Value;
            string masach = cbmasach.Text;
            string manhanvien = cbmanhanvien.Text;
            string soluong = txtsoluong.Text;
            string trangthai = cbmtrangthai.Text;
            DateTime ngaytra = dtpNgaytra.Value;
            cm = new SqlCommand("select madocgia from phieumuon where madocgia='" + madocgia + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (ngaymuon >= ngaytra)
            {
                MessageBox.Show("Ngày trả phải sau ngày mượn");
            }
            else
            if (madocgia == ma)
            {
                string sqlsua = "update phieumuon set MADOCGIA = '" + madocgia + "',NGAYMUON = '" + ngaymuon + "',MASACH = '" + masach + "',MANHANVIEN = '" + manhanvien + "',SOLUONG = '" + soluong + "',TRANGTHAI = '" + trangthai + "',NGAYTRA = '" + ngaytra + "'where MADOCGIA = '" + madocgia + "'";
                SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                try
                {
                    DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (DialogResult.OK == dlr)
                        cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Sửa thành công");
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Sửa thất bại!");
                }
            }
            else
                MessageBox.Show("Không trùng mã loại!");
            hienthi();
        }

        private void dgvmuontra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvmuontra.Rows[e.RowIndex];

                cbmadg.Text = row.Cells[0].Value.ToString();
                dtpngaynuon.Text = row.Cells[1].Value.ToString();
                cbmasach.Text = row.Cells[2].Value.ToString();
                cbmanhanvien.Text = row.Cells[3].Value.ToString();
                txtsoluong.Text = row.Cells[4].Value.ToString();
                cbmtrangthai.Text = row.Cells[5].Value.ToString();
                dtpNgaytra.Text = row.Cells[6].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string madocgia = cbmadg.Text;
            cm = new SqlCommand("select madocgia from phieumuon where madocgia='" + madocgia + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (madocgia == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete phieumuon where MADOCGIA = '" + madocgia + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        btnLammoi_Click( sender,  e);
                    }
                    catch
                    {
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thất bại!");
                    }
                }
            }
            hienthi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            if (txtTK.Text == "")
            {
                MessageBox.Show("Chưa nhập thông tin cần tìm!");
            }
            else
            {
                Connection cn = new Connection();
                cn.OpenConn();
                string madocgia = txtTK.Text;
                string sqltk = "select *from phieumuon where MADOCGIA ='" + madocgia + "'";
                SqlCommand cmd = new SqlCommand(sqltk, cn.con);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvmuontra.DataSource = dt;
            }
        }

        private void btnGiahan_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string madocgia = cbmadg.Text;
            DateTime ngaymuon = dtpngaynuon.Value;
            string masach = cbmasach.Text;
            string manhanvien = cbmanhanvien.Text;
            string soluong = txtsoluong.Text;
            string trangthai = cbmtrangthai.Text;
            DateTime ngaytra = dtpNgaytra.Value;
            string giahan = numgiahan.Text;
            cm = new SqlCommand("select madocgia from phieumuon where madocgia='" + madocgia + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (ngaymuon >= ngaytra)
            {
                MessageBox.Show("Ngày trả phải sau ngày mượn");
            }
            else
            if (madocgia == ma)
            {
                string sqlgh = "update phieumuon set ngaytra = DATEADD(day, " + Int32.Parse(giahan) + ",ngaytra) where MADOCGIA = '" + madocgia + "' and MANHANVIEN = '" + manhanvien + "'";
                SqlCommand cmd = new SqlCommand(sqlgh, cn.con);
                try
                {
                    frmMuontrasach_Load( sender,  e);
                    DialogResult dlr = MessageBox.Show("Bạn có chắc muốn gia hạn?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (DialogResult.OK == dlr)
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Gia hạn thành công");
                    
                }
                catch
                {
                    cmd.Dispose();
                    cn.CloseConn();
                    MessageBox.Show("Gia hạn thất bại!");
                }
            }
            else
                MessageBox.Show("Không trùng mã độc giả!");
            hienthi();
        }
    }
}
