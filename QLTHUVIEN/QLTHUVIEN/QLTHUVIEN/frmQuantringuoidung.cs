using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace QLTHUVIEN
{
    public partial class frmQuantringuoidung : Form
    {
        SqlCommand cm;

        public frmQuantringuoidung()
        {
            InitializeComponent();
        }

       // string name = "toEncrypt" ;
        //public static string Encrypt(string toEncrypt)
        //{
        //    string key = "2giotoitaigoccayda";
        //    bool useHashing = true;
        //    byte[] keyArray;
        //    byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        //    if (useHashing)
        //    {
        //        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        //        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //    }
        //    else
        //        keyArray = UTF8Encoding.UTF8.GetBytes(key);

        //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //    tdes.Key = keyArray;
        //    tdes.Mode = CipherMode.ECB;
        //    tdes.Padding = PaddingMode.PKCS7;

        //    ICryptoTransform cTransform = tdes.CreateEncryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        //}
        //public static string Decrypt(string toDecrypt)
        //{
        //    string key = "2giotoitaigoccayda";
        //    bool useHashing = true;
        //    byte[] keyArray;
        //    byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

        //    if (useHashing)
        //    {
        //        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        //        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //    }
        //    else
        //        keyArray = UTF8Encoding.UTF8.GetBytes(key);

        //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //    tdes.Key = keyArray;
        //    tdes.Mode = CipherMode.ECB;
        //    tdes.Padding = PaddingMode.PKCS7;

        //    ICryptoTransform cTransform = tdes.CreateDecryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        //    return UTF8Encoding.UTF8.GetString(resultArray);
        //}

        //public string GetMD5(string chuoi)
        //{
        //    string str_md5 = "";
        //    byte[] mang = System.Text.Encoding.UTF8.GetBytes(chuoi);

        //    MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
        //    mang = my_md5.ComputeHash(mang);

        //    foreach (byte b in mang)
        //    {
        //        str_md5 += b.ToString("X2");
        //    }

        //        return str_md5;
        //}
        private void hienthi()
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string sqlht = "select * from nhanvien";
            SqlCommand cmd = new SqlCommand(sqlht, cn.con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dgvDSnhanvien.DataSource = dt;
            cn.CloseConn();
        }
        public DataTable danhsachnhanvien()
        {
            Connection cn = new Connection();
            SqlDataAdapter da = new SqlDataAdapter("select MANHANVIEN as 'Mã nhân viên',HOTEN as 'Họ tên',DIACHI as 'Địa chỉ',TENDANGNHAP as 'Tên đăng nhập',MATKHAU as 'Mật khẩu',QUYENHAN as 'Quyền hạn' from nhanvien", cn.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private void btnLammoi_Click(object sender, EventArgs e)
        {
            txtmnv.Clear();
            txthoten.Clear();
            txtmatkhau.Clear();
            txttendn.Clear();
            txtdiachi.Clear();
        }

        private void frmQuantringuoidung_Load(object sender, EventArgs e)
        {
            hienthi();
            dgvDSnhanvien.DataSource = danhsachnhanvien();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            xuly xl = new xuly();
            Connection cn = new Connection();
            cn.OpenConn();
            string manhanvien = txtmnv.Text;
            string hoten = txthoten.Text;
            string diachi = txtdiachi.Text;
            string tendangnhap = txttendn.Text;
            string matkhau = txtmatkhau.Text;
            string quyenhan = cbquyenhan.Text;
            cm = new SqlCommand("select manhanvien from nhanvien where manhanvien='" + manhanvien + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (manhanvien == "" || hoten == "" || diachi == "" || tendangnhap == "" || matkhau == "" || quyenhan == "")
            {
                MessageBox.Show("Thông tin nhân viên không được bỏ trống!!!");
            }
            else
            if (manhanvien == ma)
            {
                MessageBox.Show("Trùng mã nhân viên, thêm thất bại");
            }
            else
            {
                string sqlthem = "insert into nhanvien values('" + manhanvien + "',N'" + hoten + "',N'" + diachi + "','" + tendangnhap + "','" + matkhau + "','" + quyenhan + "')";
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
            string manhanvien = txtmnv.Text;
            string hoten = txthoten.Text;
            string diachi = txtdiachi.Text;
            string tendangnhap = txttendn.Text;
            string matkhau = txtmatkhau.Text;
            string quyenhan = cbquyenhan.Text;
            cm = new SqlCommand("select manhanvien from nhanvien where manhanvien='" + manhanvien + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (manhanvien == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn sửa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlsua = "update nhanvien set MANHANVIEN='" + manhanvien + "',HOTEN ='" + hoten + "',DIACHI='" + diachi + "',TENDANGNHAP='" + tendangnhap + "',MATKHAU='" + matkhau + "', QUYENHAN='" + quyenhan + "' where  MANHANVIEN='" + manhanvien + "'";
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
                //if(manhanvien != ma)
                //MessageBox.Show("Không được thay đổi mã nhân viên!");
            
            hienthi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Connection cn = new Connection();
            cn.OpenConn();
            string manhanvien = txtmnv.Text;
            cm = new SqlCommand("select manhanvien from nhanvien where manhanvien='" + manhanvien + "'", cn.con);
            string ma = cm.ExecuteScalar() as string;
            if (manhanvien == ma)
            {
                DialogResult dlr = MessageBox.Show("Bạn có chắc muốn xóa?", "THÔNG BÁO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (DialogResult.OK == dlr)
                {
                    string sqlxoa = "delete nhanvien where manhanvien='" + manhanvien + "'";
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
                string manhanvien = txtTK.Text;
                string sqltk = "select *from nhanvien where manhanvien ='" + manhanvien + "'";
                SqlCommand cmd = new SqlCommand(sqltk, cn.con);
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dgvDSnhanvien.DataSource = dt;
            }
        }

        private void dgvDSnhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvDSnhanvien.Rows[e.RowIndex];

                txtmnv.Text = row.Cells[0].Value.ToString();
                txthoten.Text = row.Cells[1].Value.ToString();
                txtdiachi.Text = row.Cells[2].Value.ToString();
                txttendn.Text = row.Cells[3].Value.ToString();
                txtmatkhau.Text = row.Cells[4].Value.ToString();
                cbquyenhan.Text = row.Cells[5].Value.ToString();
            }
        }

    }
}
