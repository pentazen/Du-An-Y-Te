using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Service;
using Du_An_Y_Te.Models;
using System.Web.Mvc;
using System.Net.Mail;
using System.IO;
using System.Web.Script.Serialization;

namespace Du_An_Y_Te.Controllers.API
{
    public class AccountController : ApiController
    {
        Service.ThanhVien_Service ThanhVienService = new ThanhVien_Service();
        [System.Web.Http.HttpPost]
        public Service.EntityModel.ThanhVien Login(AccountViewModels.LoginViewModel TaiKhoan)
        {
            return ThanhVienService.KiemTraDangNhap(TaiKhoan.TaiKhoan, TaiKhoan.MatKhau);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Account/SignUp")]
        //[Authorize] Dùng để kiểm tra đăng nhập chưa
        //[System.Web.Http.AllowAnonymous] // Cái này dùng để cho phép người dùng chưa đăng nhập sử dụng
        [ValidateAntiForgeryToken] // Cần Token để Access (Tạm thời chưa hoạt động ở API cần Chỉnh sửa )
        public object SignUp(AccountViewModels.SignUpViewModel Account)
        {
            if (ModelState.IsValid)
            {
                Service.EntityModel.ThanhVien check = ThanhVienService.DangKyTaiKhoan(Account.TaiKhoan, Account.Email, Account.MatKhau) as Service.EntityModel.ThanhVien;
                try
                {
                    if (check != null)
                    {
                        SendMailToUserAccessAccount((Service.EntityModel.ThanhVien)check);
                        return Json(new { KetQua = true, Message = string.Format("Click vào đường dẫn này để kích hoạt tài khoản <br> <a target=\"_blank\" href='http://45.119.81.22/Account/Access?id={0}&&code={1}'>http://45.119.81.22/Account/Access?id={0}&&code={1}</a>", check.id, check.MaKichHoat) });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { KetQua = false, Message = check });
                }
            }
            return ModelState.Values;
        }

        public bool Access(int id, string code)
        {
            return ThanhVienService.KichHoatTaiKhoan(id, code);
        }
        private void SendMailToUserAccessAccount(Service.EntityModel.ThanhVien User)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("zzpentazenzz@gmail.com", "Dịch Vụ Chăm Sóc Sức Khỏe");
                message.To.Add(new MailAddress(User.Email));
                //message.CC.Add(new MailAddress("zzpentazenzz@gmail.com"));
                message.Subject = "Email xác nhận";
                message.Body = "Vui lòng nhấn vào đường dẫn dưới đây để xác nhận đăng ký tại website : \n";
                string m = Request.RequestUri.Host;
                //message.Body += "http://www.abc?code=" + Session["rdnCode"].ToString();
                //message.Body += "http://localhost:49833/Account/Access?id=" + "s";
                //message.Body += string.Format("http://45.119.81.22/Account/Access?id={0}&&code={1}", User.id, User.MaKichHoat);

                message.Body = " <html>";
                message.Body += "<head>";
                message.Body += "       <title>Chào mừng bạn đã đến với dịch vụ sức khỏe của Tanico</title> <meta charset=\"utf - 8\" />";
                message.Body += "</head>";
                message.Body += "<body>";
                message.Body += "  <h1>Cám ơn đã tham gia cộng đồng sức khỏe của chúng tôi</h1>";
                message.Body += "  <h2>Dưới đây là thông tin tài khoản của bạn</h2>";
                message.Body += " <table cellspacing=\"0\" style=\"border: 2px dashed #FB4314; width: 800px; height: 500px;\">";
                message.Body += "   <tr>";
                message.Body += "       <th>Tài Khoản :</th><td>" + User.TaiKhoan + " </td>";
                message.Body += "   </tr>";
                message.Body += "   <tr style=\"background-color: #e0e0e0;\">";
                message.Body += "       <th>Mật Khẩu:</th><td>" + User.MatKhau + "</td>";
                message.Body += "   </tr>";
                message.Body += "   <tr>";
                message.Body += string.Format("<th>Hãy Kích Hoạt Tài Khoản Bằng Đường Link Này :</th><td><a href='http://45.119.81.22/Account/Access?id={0}&&code={1}'>http://45.119.81.22/Account/Access?id={0}&&code={1}</a></td>", User.id, User.MaKichHoat);
                message.Body += "    </tr>";
                message.Body += "          </table>";
                message.Body += " </body>";
                message.Body += " </html>';";
                message.IsBodyHtml = true;
                //SmtpClient client = new SmtpClient("localhost", 49833);

                //System.Net.NetworkCredential auth = new System.Net.NetworkCredential("zzpentazenzz@gmail.com", "0908129509");
                //client.EnableSsl = false;
                //client.Credentials = auth;
                //client.Credentials = CredentialCache.DefaultNetworkCredentials;
                //client.Send(message);



                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new System.Net.NetworkCredential("zzpentazenzz@gmail.com", "0908129509");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            catch (Exception ex)
            {

            }
        }

        public bool CapNhatThongTinCaNhan(Service.EntityModel.ThanhVien User)
        {
            return ThanhVienService.CapNhatThongTinCaNhan(User);
        }

        public object GetById(int id)
        {
            return ThanhVienService.GetById(id);
        }
        [System.Web.Http.Route("api/Account/ForgetPassword")]
        public object ForgetPassword(AccountViewModels.ForgetPasswordViewModel taikhoan)
        {
            Service.EntityModel.ThanhVien thanhvien = ThanhVienService.KiemTraTonTaiBangTaiKhoan(taikhoan.TaiKhoan);
            if (thanhvien!=null)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress("zzpentazenzz@gmail.com", "Dịch Vụ Chăm Sóc Sức Khỏe");
                    message.To.Add(new MailAddress(thanhvien.Email));
                    //message.CC.Add(new MailAddress("zzpentazenzz@gmail.com"));
                    message.Subject = "Email lấy mật khẩu mới";
                    message.Body = "Vui lòng nhấn vào đường dẫn dưới đây để kích hoạt lại tài khoản tại website : \n";
                    string m = Request.RequestUri.Host;

                    message.Body = " <html>";
                    message.Body += "<head>";
                    message.Body += "       <title>Chào mừng bạn đã đến với dịch vụ sức khỏe của Tanico</title> <meta charset=\"utf - 8\" />";
                    message.Body += "</head>";
                    message.Body += "<body>";
                    message.Body += "  <h1>Thông tin mật khẩu và mã kích hoạt lại tài khoản của bạn</h1>";
                    message.Body += "  <h2>Dưới đây là thông tin tài khoản của bạn</h2>";
                    message.Body += " <table cellspacing=\"0\" style=\"border: 2px dashed #FB4314; width: 800px; height: 500px;\">";
                    message.Body += "   <tr>";
                    message.Body += "       <th>Tài Khoản :</th><td>" + thanhvien.TaiKhoan + " </td>";
                    message.Body += "   </tr>";
                    message.Body += "   <tr style=\"background-color: #e0e0e0;\">";
                    message.Body += "       <th>Mật Khẩu:</th><td>" + thanhvien.MatKhau + "</td>";
                    message.Body += "   </tr>";
                    message.Body += "   <tr>";
                    message.Body += string.Format("<th>Hãy Kích Hoạt Tài Khoản Bằng Đường Link Này :</th><td><a href='http://45.119.81.22/Account/Access?id={0}&&code={1}'>http://45.119.81.22/Account/Access?id={0}&&code={1}</a></td>", thanhvien.id, thanhvien.MaKichHoat);
                    message.Body += "    </tr>";
                    message.Body += "          </table>";
                    message.Body += " </body>";
                    message.Body += " </html>';";
                    message.IsBodyHtml = true;



                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential("zzpentazenzz@gmail.com", "0908129509");
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }
                catch (Exception ex)
                {

                }
                return new JavaScriptSerializer().Serialize(new { KetQua = true, Message = "Vui lòng vào Email của bạn để kích hoạt và nhận mật khẩu mới!" });
            }
            return new JavaScriptSerializer().Serialize(new { KetQua = false, Message = "Tài khoản không tồn tại! \n Vui lòng nhập chính xác tài khoản!"});
        }

    }
}
