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
   public class ChatUserDetails_Service : IService
    {
        public ChatUserDetail GetById(int id)
        {
            return DuAnYTeEntitiesFramework.ChatUserDetails.FirstOrDefault(bs => bs.TrangThai != false && bs.id == id);
        }
        public void Add(ChatUserDetail user)
        {
             DuAnYTeEntitiesFramework.ChatUserDetails.Add(user);
            DuAnYTeEntitiesFramework.SaveChanges();

        }
        public List<ChatUserDetail> GetAll()
        {
            return DuAnYTeEntitiesFramework.ChatUserDetails.Where(bs => bs.TrangThai != false).ToList();
        }
        public object DeleteById(int id)
        {
            try
            {
                DuAnYTeEntitiesFramework.ChatUserDetails.FirstOrDefault(bs => bs.id == id).TrangThai = false;
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
