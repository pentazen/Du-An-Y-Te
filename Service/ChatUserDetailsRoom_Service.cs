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
    public class ChatUserDetailsRoom_Service : IService
    {
        public ChatUserDetailsRoom GetByIdThanhVien(int id)
        {
            return DuAnYTeEntitiesFramework.ChatUserDetailsRooms.FirstOrDefault(bs => bs.TrangThai != false && bs.id_ThanhVien == id);
        }
        public void Add(ChatUserDetailsRoom user)
        {
            DuAnYTeEntitiesFramework.ChatUserDetailsRooms.Add(user);
            DuAnYTeEntitiesFramework.SaveChanges();

        }
        public List<ChatUserDetailsRoom> GetAll()
        {
            return DuAnYTeEntitiesFramework.ChatUserDetailsRooms.Where(bs => bs.TrangThai != false).ToList();
        }
        public List<ChatUserDetailsRoom> GetById_Phong(int id_Phong)
        {
            return DuAnYTeEntitiesFramework.ChatUserDetailsRooms.Where(user => user.TrangThai != false&& user.id_PhongThaoLuan==id_Phong).ToList();
        }
        public object DeleteByIdThanhVien(int id)
        {
            try
            {
                var ds = DuAnYTeEntitiesFramework.ChatUserDetailsRooms.Where(bs => bs.id_ThanhVien == id).ToList();
                foreach (var item in ds)
                {
                    item.TrangThai = false;
                }
                DuAnYTeEntitiesFramework.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
