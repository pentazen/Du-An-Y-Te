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
   public class ChatUserDetails_Service : IService<ChatUserDetail>
    {
        public ChatUserDetail GetById(int id)
        {
            return DbContext.ChatUserDetails.FirstOrDefault(bs => bs.TrangThai != false && bs.id == id);
        }
        public void Add(ChatUserDetail user)
        {
             DbContext.ChatUserDetails.Add(user);
            DbContext.SaveChanges();

        }
        public List<ChatUserDetail> GetAll()
        {
            return DbContext.ChatUserDetails.Where(bs => bs.TrangThai != false).ToList();
        }
        public object DeleteById(int id)
        {
            try
            {
                DbContext.ChatUserDetails.FirstOrDefault(bs => bs.id == id).TrangThai = false;
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
