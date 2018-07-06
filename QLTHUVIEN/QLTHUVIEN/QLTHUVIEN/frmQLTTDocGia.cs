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
    public partial class frmQLTTDocGia : Form
    {
        SqlCommand cm;
        public frmQLTTDocGia()
        {
            InitializeComponent();
        }

        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select * from docgia";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSTTdocgia.DataSource = dt;
            cn.CloseConn();            
        }
        public void Loadmakhoa()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            cm = new SqlCommand("select makhoa from khoa", cn.con);
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cbmakhoa.DataSource = ds.Tables[0];
            cbmakhoa.ValueMember = "makhoa";
        }
        public DataTable danhsachdocgia()
        {
            Connection cn = new Connection();
            SqlDataAdapter da = new SqlDataAdapter("select MADOCGIA as 'Mã đọc giả',HOTEN as 'Họ tên',NGAYSINH as 'Ngày sinh',MAKHOA as 'Mã khoa',DIACHI as 'Địa chỉ',NGAYLAPTHE as 'Ngày lập thẻ',MATKHAU as 'Mật khẩu' from DOCGIA", cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtmdg.Clear();
            txthoten.Clear();
            txtdiachi.Clear();
            txtmatkhau.Clear();
            cbmakhoa.Text = "CNTT";
            dtpngaylapthe.Value = DateTime.Today;
            dtpNgaysinh.Value = DateTime.Today;
            frmQLTTDocGia_Load(sender, e);
        }

        private void frmQLTTDocGia_Load(object sender, EventArgs e)
        {
            hienthi();
            dtpngaylapthe.Value = DateTime.Today;
            dtpNgaysinh.Value = DateTime.Today;
            Loadmakhoa();
            dgvDSTTdocgia.DataSource = danhsachdocgia();
        }

        private void dgvDSTTdocgia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvDSTTdocgia.Rows[e.RowIndex];

                txtmdg.Text = row.Cells[0].Value.ToString();
                txthoten.Text = row.Cells[1].Value.ToString();
                dtpNgaysinh.Text = row.Cells[2].Value.ToString();
                cbmakhoa.Text = row.Cells[3].Value.ToString();
                txtdiachi.Text = row.Cells[4].Value.ToString();
                dtpngaylapthe.Text = row.Cells[5].Value.ToString();
                txtmatkhau.Text = row.Cells[6].Value.ToString();
            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string madocgia = txtmdg.Text;
            string hoten = txthoten.Text;
            DateTime ngaysinh = dtpNgaysinh.Value;
            string makhoa = cbmakhoa.Text;
            string diachi = txtdiachi.Text;
            DateTime ngaylapthe = dtpngaylapthe.Value;
            string matkhau = txtmatkhau.Text;

            DateTime nams = Convert.ToDateTime(dtpNgaysinh.Value.ToString());
            int Ngt = int.Parse(DateTime.Now.Year.ToString());
            int nn = int.Parse(nams.Year.ToString());
            int tuoi = Ngt - nn;

            cm = new SqlCommand("select madocgia from docgia where madocgia='" + madocgia + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if(madocgia=="" || makhoa == "" || diachi == ""|| matkhau == "")
            {
                MessageBox.Show("Thông tin đọc giả không được bỏ trống!!!");
            }
            if (tuoi < 18)
            {
                errorProvider1.SetError(dtpNgaysinh, "Đọc giả phải đủ 18 tuổi");
                return;
            }
            else
            if(tuoi > 100)
            {
                errorProvider1.SetError(dtpNgaysinh, "Đọc giả phải nho hon 100 tuổi");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }
            if (madocgia == ma)
            {
                MessageBox.Show("Trùng mã độc giả, thêm thất bại");
            }
            else
            if (ngaysinh >= ngaylapthe)
            {
                MessageBox.Show("Ngày lập thẻ phải sau ngay sinh");
            }
           

            else
            {
                string sqlthem = "insert into docgia values('" + madocgia + "',N'" + hoten + "','" + ngaysinh + "','" + makhoa + "',N'" + diachi + "','" + ngaylapthe + "','" + matkhau + "')";
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
            string madocgia = txtmdg.Text;
            string hoten = txthoten.Text;
            DateTime ngaysinh = dtpNgaysinh.Value;
            string makhoa = cbmakhoa.Text;
            string diachi = txtdiachi.Text;
            DateTime ngaylapthe = dtpngaylapthe.Value;
            string matkhau = txtmatkhau.Text;
            cm = new SqlCommand("select madocgia from docgia where madocgia='" + madocgia + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (ngaysinh >= ngaylapthe)
            {
                MessageBox.Show("Ngày lập thẻ phải sau ngay sinh");
            }
            else
            if (madocgia == ma)          
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update docgia set MADOCGIA='" + madocgia + "',HOTEN='" + hoten + "',NGAYSINH='" + ngaysinh + "',MAKHOA='" + makhoa + "',DIACHI='" + diachi + "',NGAYLAPTHE='" + ngaylapthe + "',MATKHAU='" + matkhau + "' where MADOCGIA ='" + madocgia + "'";
                    SqlCommand cmd = new SqlCommand(sqlsua, cn.con);
                    try
                    {
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
            }
            hienthi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string madocgia = txtmdg.Text;
            cm = new SqlCommand("select madocgia from docgia where madocgia='" + madocgia + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (madocgia == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete docgia where MADOCGIA ='" + madocgia + "'";
                    SqlCommand cmd = new SqlCommand(sqlxoa, cn.con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        cn.CloseConn();
                        MessageBox.Show("Xóa thành công");
                        btnLammoi_Click(sender, e);
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
                string sqltk = "select *from docgia where MADOCGIA ='" + madocgia + "'";
                SqlCommand cmd = new SqlCommand(sqltk, cn.con);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvDSTTdocgia.DataSource = dt;
            }
        }

        private void dtpNgaysinh_ValueChanged(object sender, EventArgs e)
        {

            //DateTime nams = Convert.ToDateTime(dtpNgaysinh.Value.ToString());
            //int Ngt=int.Parse(DateTime.Now.Year.ToString());
            //int nn = int.Parse(nams.Year.ToString());
            //int tuoi = Ngt - nn;
            //if(tuoi<18)
            //{
            //    errorProvider1.SetError(dtpNgaysinh, "Đọc giả phải đủ 18 tuổi");
            //    return;
            //}
            //else
            //{
            //    errorProvider1.Clear();
            //}
        }
    }
}
