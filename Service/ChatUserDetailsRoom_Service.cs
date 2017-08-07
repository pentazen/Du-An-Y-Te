using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Thư viện
using Service.EntityModel;
using System.Security.Cryptography;
using Blog_Guitar.Service;
#endregion
namespace Service
{
    public class ChatUserDetailsRoom_Service : IService<ChatUserDetailsRoom>
    {
        public ChatUserDetailsRoom GetByIdThanhVien(int id)
        {
            return DbContext.ChatUserDetailsRooms.FirstOrDefault(bs => bs.TrangThai != false && bs.id_ThanhVien == id);
        }
        public void Add(ChatUserDetailsRoom user)
        {
            DbContext.ChatUserDetailsRooms.Add(user);
            DbContext.SaveChanges();

        }
        public List<ChatUserDetailsRoom> GetAll()
        {
            return DbContext.ChatUserDetailsRooms.Where(bs => bs.TrangThai != false).ToList();
        }
        public List<ChatUserDetailsRoom> GetById_Phong(int id_Phong)
        {
            return DbContext.ChatUserDetailsRooms.Where(user => user.TrangThai != false&& user.id_PhongThaoLuan==id_Phong).ToList();
        }
        public object DeleteByIdThanhVien(int id)
        {
            try
            {
                var ds = DbContext.ChatUserDetailsRooms.Where(bs => bs.id_ThanhVien == id).ToList();
                foreach (var item in ds)
                {
                    item.TrangThai = false;
                }
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
