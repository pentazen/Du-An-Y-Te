
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Thư viện
using Service.EntityModel;
using System.Security.Cryptography;
#endregion


namespace Service
{
    public class ThanhVien_Service : IService
    {
        public IList<ThanhVien> GetAll()
        {
            //for (int i = 1; i <= 100; i++)
            //{
            //    //DangKyTaiKhoan("taikhoan" + i.ToString(), "email" + i.ToString() + "@gmail.com", "123456" + i.ToString());
            //    if (i % 2 == 0)
            //    {
            //        var tam = DuAnYTeEntitiesFramework.ThanhViens.FirstOrDefault(tv => tv.id == i);
            //        if (tam != null)
            //            tam.id_LoaiThanhVien = 2;

            //    }
            //}
            //foreach (var item in DuAnYTeEntitiesFramework.ThanhViens)
            //{
            //    item.TrangThai = true;
            //}
            //DuAnYTeEntitiesFramework.SaveChanges();
            return DuAnYTeEntitiesFramework.ThanhViens.Where(tv => tv.TrangThai != false).ToList();
        }
        public ThanhVien GetByTaiKhoan(string taikhoan)
        {
            return DuAnYTeEntitiesFramework.ThanhViens.AsEnumerable().FirstOrDefault(tv => tv.TaiKhoan == taikhoan);
        }
        public object TaoTaiKhoan(ThanhVien thanhvien)
        {
            var kt = KiemTraDangKy(thanhvien.TaiKhoan, thanhvien.Email);
            try
            {
                if ((bool)kt)
                {
                    string ma;
                    do
                    {
                        ma = CreateCapcha();
                    }
                    while (DuAnYTeEntitiesFramework.ThanhViens.FirstOrDefault(user => user.MaKichHoat == ma) != null);
                    thanhvien.MaKichHoat = ma;
                    thanhvien.TrangThai = true;
                    thanhvien.NgayTao = DateTime.Now;
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hash = GetMd5Hash(md5Hash, thanhvien.MatKhau);
                        thanhvien.MatKhauMaHoa = hash;
                    }
                    DuAnYTeEntitiesFramework.ThanhViens.Add(thanhvien);
                    DuAnYTeEntitiesFramework.SaveChanges();
                    return thanhvien;
                }
                return false;

            }
            catch (Exception ex)
            {
                return kt;
            }
        }
        public ThanhVien KiemTraDangNhap(string taikhoan, string matkhau)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, matkhau);
                return DuAnYTeEntitiesFramework.ThanhViens.AsEnumerable().FirstOrDefault(tv => tv.TaiKhoan == taikhoan && tv.MatKhau == matkhau &&tv.MatKhauMaHoa== hash && tv.TrangThai == true);
            }
            
        }
        public ThanhVien KiemTraDangNhapVaoTrangAdmin(string taikhoan, string matkhau)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, matkhau);
                return DuAnYTeEntitiesFramework.ThanhViens.AsEnumerable().FirstOrDefault(tv => tv.TaiKhoan == taikhoan && tv.MatKhau == matkhau&&tv.MatKhauMaHoa== hash && tv.TrangThai == true && tv.id_LoaiThanhVien != 4);
            }
        }
        public object KiemTraDangKy(string taikhoan, string email)
        {
            ThanhVien Kiemtra = DuAnYTeEntitiesFramework.ThanhViens.AsEnumerable().FirstOrDefault(tv => tv.TaiKhoan == taikhoan || tv.Email == email);
            if (Kiemtra != null)
            {
                string TraVe = "";
                bool ktTaiKhoan = (DuAnYTeEntitiesFramework.ThanhViens.AsEnumerable().FirstOrDefault(tv => tv.TaiKhoan == taikhoan) != null);
                bool ktEmail = (DuAnYTeEntitiesFramework.ThanhViens.AsEnumerable().FirstOrDefault(tv => tv.Email == email) != null);
                if (ktTaiKhoan && ktEmail)
                {
                    TraVe += "Tài khoản và Email này đã tồn tại!";
                }
                else
                {
                    if (ktTaiKhoan)
                    {
                        TraVe += "Tài khoản này đã tồn tại!";
                    }
                    if (ktEmail)
                    {
                        TraVe += "Email này đã tồn tại! \n";
                    }
                }
                return TraVe;
            }
            return true;
        }
        private string CreateCapcha()
        {
            string strString = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            int randomCharIndex = 0;
            char randomChar;
            string captcha = "";
            for (int i = 0; i < 15; i++)
            {
                randomCharIndex = random.Next(0, strString.Length);
                randomChar = strString[randomCharIndex];
                captcha += Convert.ToString(randomChar);
            }
            return captcha;
        }
        public object DangKyTaiKhoan(string taikhoan, string email, string matkhau, int loaiTaiKhoan = 4)
        {
            var kt = KiemTraDangKy(taikhoan, email);
            try
            {
                if ((bool)kt)
                {
                    ThanhVien tv = new ThanhVien() { TaiKhoan = taikhoan, Email = email, MatKhau = matkhau };
                    string ma;
                    do
                    {
                        ma = CreateCapcha();
                    }
                    while (DuAnYTeEntitiesFramework.ThanhViens.FirstOrDefault(user => user.MaKichHoat == ma) != null);
                    tv.MaKichHoat = ma;
                    tv.id_LoaiThanhVien = loaiTaiKhoan;
                    tv.TrangThai = false;
                    tv.NgayTao = DateTime.Now;
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hash = GetMd5Hash(md5Hash, matkhau);
                        tv.MatKhauMaHoa = hash;
                    }
                    DuAnYTeEntitiesFramework.ThanhViens.Add(tv);
                    DuAnYTeEntitiesFramework.SaveChanges();
                    return tv;
                }
                return false;

            }
            catch (Exception ex)
            {
                return kt;
            }
        }
        public bool KichHoatTaiKhoan(int id, string code)
        {
            ThanhVien user = DuAnYTeEntitiesFramework.ThanhViens.FirstOrDefault(tv => tv.id == id && tv.MaKichHoat == code && tv.TrangThai == false);
            if (user != null)
            {
                user.TrangThai = true;
                user.MaKichHoat = CreateCapcha();
                DuAnYTeEntitiesFramework.ThanhViens.Attach(user);
                DuAnYTeEntitiesFramework.Entry(user).State = System.Data.Entity.EntityState.Modified;
                DuAnYTeEntitiesFramework.SaveChanges();
                return true;
            }
            return false;
        }
        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public bool CapNhatThongTinCaNhan(ThanhVien thanhvien)
        {
            try
            {
                ThanhVien thanhvien_old = DuAnYTeEntitiesFramework.ThanhViens.FirstOrDefault(tdc => tdc.id == thanhvien.id);
                //thanhvien.TaiKhoan = thanhvien_old.TaiKhoan;
                //thanhvien.MatKhau = thanhvien_old.MatKhau;
                thanhvien_old.NgaySua = DateTime.Now;
                //thanhvien.TrangThai = true;
                thanhvien_old.Ho = thanhvien.Ho;
                thanhvien_old.Ten = thanhvien.Ten;
                thanhvien_old.NTNS = thanhvien.NTNS;
                thanhvien_old.Email = thanhvien.Email;
                thanhvien_old.CMND = thanhvien.CMND;
                thanhvien_old.SoDienThoai = thanhvien.SoDienThoai;
                thanhvien_old.DiaChi = thanhvien.DiaChi;
                thanhvien_old.ChieuCao = thanhvien.ChieuCao;
                thanhvien_old.CanNang = thanhvien.CanNang;
                thanhvien_old.MaNhomMau = thanhvien.MaNhomMau;
                if (!string.IsNullOrEmpty(thanhvien.Picture))
                    thanhvien_old.Picture = thanhvien.Picture;
                thanhvien_old.id_LoaiThanhVien = thanhvien.id_LoaiThanhVien;
                thanhvien_old.TrangThai = true;
                //DuAnYTeEntitiesFramework.ThanhViens.Attach(thanhvien_old);

                DuAnYTeEntitiesFramework.Entry(thanhvien_old).State = System.Data.Entity.EntityState.Modified;
                DuAnYTeEntitiesFramework.SaveChanges();
                //using (var dbCtx = new DuAnYTeEntities())
                //{
                //    //3. Mark entity as modified
                //    dbCtx.Entry(thanhvien_old).State = System.Data.Entity.EntityState.Modified;

                //    //4. call SaveChanges
                //    dbCtx.SaveChanges();
                //}
                //thanhvien.NgaySua = DateTime.Now;
                //DuAnYTeEntitiesFramework.ThanhViens.Attach(thanhvien);
                //var entry = DuAnYTeEntitiesFramework.Entry(thanhvien);
                ////entry.Property(e => e.Email).IsModified = true;
                //entry.Property(e => e.Ho).IsModified = true;
                //entry.Property(e => e.Ten).IsModified = true;
                //entry.Property(e => e.NTNS).IsModified = true;
                //entry.Property(e => e.cm).IsModified = true;
                //entry.Property(e => e.Email).IsModified = true;
                //entry.Property(e => e.Email).IsModified = true;
                //entry.Property(e => e.Email).IsModified = true;
                //entry.Property(e => e.Email).IsModified = true;
                //entry.Property(e => e.Email).IsModified = true;
                //entry.Property(e => e.Email).IsModified = true;
                //entry.Property(e => e.Email).IsModified = true;

                //// other changed properties
                //DuAnYTeEntitiesFramework.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ThanhVien GetById(int id)
        {
            return DuAnYTeEntitiesFramework.ThanhViens.FirstOrDefault(tt => tt.id == id);
        }

        public bool DeleteById(int id)
        {
            try
            {
                DuAnYTeEntitiesFramework.ThanhViens.FirstOrDefault(tt => tt.id == id).TrangThai = false;
                DuAnYTeEntitiesFramework.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public ThanhVien KiemTraTonTaiBangTaiKhoan(string taikhoan)
        {
            ThanhVien thanhvien = DuAnYTeEntitiesFramework.ThanhViens.AsEnumerable().FirstOrDefault(tv => tv.TaiKhoan == taikhoan);
            if(thanhvien!=null)
            {
                thanhvien.TrangThai = false;
                DuAnYTeEntitiesFramework.ThanhViens.Attach(thanhvien);
                DuAnYTeEntitiesFramework.Entry(thanhvien).State = System.Data.Entity.EntityState.Modified;
                DuAnYTeEntitiesFramework.SaveChanges();
                return thanhvien;
            }
            return null;
        }


        public bool VoHieuHoaTaiKhoan(string taikhoan)
        {
            return (DuAnYTeEntitiesFramework.ThanhViens.AsEnumerable().FirstOrDefault(tv => tv.TaiKhoan == taikhoan) != null);
        }
    }
}
