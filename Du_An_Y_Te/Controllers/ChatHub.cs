using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using Service;
using Service.EntityModel;

namespace Du_An_Y_Te.Controllers
{
    public class ChatHub : Hub
    {
        #region Custom
        //public HubConnectionContext Clients { get; set; }
        //public List<UserDetail> ConnectedUsers { get; private set; }
        //public HubCallerContext Context { get; set; }
        //public IGroupManager Groups { get; set; }
        //public object CurrentMessage { get; private set; }
        public static string emailIDLoaded = "";
        #region Class
        public class UserDetail
        {
            public string ConnectionId { get; set; }
            public string UserName { get; set; }
            public string CurrentMessage { get; set; }
            public string AnhDaiDien { get; set; }


        }
        public class PrivateChatMessage
        {
            public string userName { get; set; }
            public string message { get; set; }
        }
        #endregion

        #region Method

        #region Connect
        public void Connect(string userName, string email)
        {
            emailIDLoaded = email;
            var id = Context.ConnectionId;
            using (Service.EntityModel.DuAnYTeEntities dc = new Service.EntityModel.DuAnYTeEntities())
            {
                var thanhvien = dc.ThanhViens.FirstOrDefault(x => x.Email == email);
                var item = dc.ChatUserDetails.FirstOrDefault(x => x.id_ThanhVien == thanhvien.id);
                //var k = dc.ThanhViens.FirstOrDefault(x => x.Email == email);
                if (item != null)
                {
                    dc.ChatUserDetails.Remove(item);
                    dc.SaveChanges();

                    // Disconnect
                    Clients.All.onUserDisconnectedExisting(item.ConnectionId, item.id_ThanhVien);
                }

                var Users = dc.ChatUserDetails.ToList();
                if (Users.Where(x => x.id_ThanhVien == thanhvien.id).ToList().Count == 0)
                {
                    var userdetails = new ChatUserDetail
                    {
                        ConnectionId = id,
                        id_ThanhVien = thanhvien.id
                    };
                    dc.ChatUserDetails.Add(userdetails);
                    dc.SaveChanges();

                    // send to caller
                    var connectedUsers = dc.ChatUserDetails.ToList();
                    //var CurrentMessage = dc.ChatMessageDetails.ToList();
                    //Clients.Caller.onConnected(id, userName, connectedUsers, CurrentMessage);
                    List<ChatUserDetail> final = new List<ChatUserDetail>();
                    foreach (var itemtemp in connectedUsers)
                    {
                        ChatUserDetail temp = new ChatUserDetail();
                        temp.id = itemtemp.id;
                        temp.id_ThanhVien = itemtemp.id_ThanhVien;
                        temp.ConnectionId = itemtemp.ConnectionId;
                        final.Add(temp);
                    }
                    //Clients.Caller.onConnected(id, userName, connectedUsers);
                    Clients.Caller.onConnected(id, userName, final);
                }

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName, email);
            }
        }
        #endregion

        #region Disconnect
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            using (Service.EntityModel.DuAnYTeEntities dc = new Service.EntityModel.DuAnYTeEntities())
            {
                var item = dc.ChatUserDetails.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (item != null)
                {
                    dc.ChatUserDetails.Remove(item);
                    dc.SaveChanges();

                    var id = Context.ConnectionId;
                    Clients.All.onUserDisconnected(id, item.id_ThanhVien);
                }
            }
            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region Private_Messages
        public void SendPrivateMessage(string toUserId, string message, string status)
        {
            string fromUserId = Context.ConnectionId;
            using (Service.EntityModel.DuAnYTeEntities dc = new Service.EntityModel.DuAnYTeEntities() )
            {
                int thisid = int.Parse(toUserId);
                //var toUser = dc.ChatUserDetails.FirstOrDefault(x => x.id_ThanhVien == 1);
                var toUser = dc.ChatUserDetails.FirstOrDefault(x => x.id_ThanhVien == thisid);
                var fromUser = dc.ChatUserDetails.FirstOrDefault(x => x.ConnectionId == fromUserId);
                if (toUser != null && fromUser != null)
                {
                    //if (status == "Click")
                    //    AddPrivateMessageinCache(fromUser.EmailID, toUser.EmailID, fromUser.UserName, message);

                    // send to 
                    //Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message, fromUser.EmailID, toUser.EmailID, status, fromUserId);
                    fromUser.ThanhVien = dc.ThanhViens.FirstOrDefault(x => x.id == fromUser.id_ThanhVien);
                    toUser.ThanhVien = dc.ThanhViens.FirstOrDefault(x => x.id == toUser.id_ThanhVien);
                    string to = dc.ChatUserDetails.FirstOrDefault(x => x.id_ThanhVien == toUser.id_ThanhVien).ConnectionId;
                    //Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.id_ThanhVien, message, fromUser.ThanhVien.Email, toUser.ThanhVien.Email, status, fromUserId);
                    Clients.Client(to).sendPrivateMessage(fromUserId, fromUser.id_ThanhVien, message, fromUser.ThanhVien.Email, toUser.ThanhVien.Email, status, fromUserId, fromUser.ThanhVien.id);

                    // send to caller user
                    Clients.Caller.sendPrivateMessage(toUserId, fromUser.id_ThanhVien, message, fromUser.ThanhVien.Email, toUser.ThanhVien.Email, status, fromUserId, thisid);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromid"></param>
        /// <param name="toid"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public List<PrivateChatMessage> GetPrivateMessage(string fromid, string toid, int take)
        {
            using (Service.EntityModel.DuAnYTeEntities dc = new Service.EntityModel.DuAnYTeEntities())
            {
                List<PrivateChatMessage> msg = new List<PrivateChatMessage>();

                var v = (from a in dc.ThanhViens.ToList()
                         join b in dc.ChatPrivateMessageDetails.ToList() on a.id equals b.id_ThanhVien_Gui into cc
                         from c in cc
                         where (c.id_ThanhVien_Gui.Equals(fromid) && c.id_ThanhVien_Nhan.Equals(toid)) || (c.id_ThanhVien_Gui.Equals(toid) && c.id_ThanhVien_Nhan.Equals(fromid))
                         orderby c.id descending
                         select new
                         {
                             UserName = a.TaiKhoan,
                             Message = c.NoiDung,
                             ID = c.id
                         }).Take(take).ToList();
                v = v.OrderBy(s => s.ID).ToList();

                foreach (var a in v)
                {
                    var res = new PrivateChatMessage()
                    {
                        userName = a.UserName,
                        message = a.Message
                    };
                    msg.Add(res);
                }
                return msg;
            }
        }

        private int takeCounter = 0;
        private int skipCounter = 0;
        public List<PrivateChatMessage> GetScrollingChatData(string fromid, string toid, int start = 10, int length = 1)
        {
            takeCounter = (length * start); // 20
            skipCounter = ((length - 1) * start); // 10

            using (Service.EntityModel.DuAnYTeEntities dc = new Service.EntityModel.DuAnYTeEntities())
            {
                List<PrivateChatMessage> msg = new List<PrivateChatMessage>();
                var v = (from a in dc.ThanhViens
                         join b in dc.ChatPrivateMessageDetails on a.id equals b.id_ThanhVien_Gui into cc
                         from c in cc
                         where (c.id_ThanhVien_Gui.Equals(fromid) && c.id_ThanhVien_Nhan.Equals(toid)) || (c.id_ThanhVien_Gui.Equals(toid) && c.id_ThanhVien_Nhan.Equals(fromid))
                         orderby c.id descending
                         select new
                         {
                             UserName = a.TaiKhoan,
                             Message = c.NoiDung,
                             ID = c.id
                         }).Take(takeCounter).Skip(skipCounter).ToList();
                v = v.OrderBy(s => s.ID).ToList();

                foreach (var a in v)
                {
                    var res = new PrivateChatMessage()
                    {
                        userName = a.UserName,
                        message = a.Message
                    };
                    msg.Add(res);
                }
                return msg;
            }
        }
        #endregion

        #region Save_Cache
        //private void AddAllMessageinCache(string userName, string message)
        //{
        //    using (Service.EntityModel.DuAnYTeEntities dc = new Service.EntityModel.DuAnYTeEntities())
        //    {
        //        var messageDetail = new ChatMessageDetail
        //        {
        //            UserName = userName,
        //            Message = message,
        //            EmailID = emailIDLoaded
        //        };
        //        dc.ChatMessageDetails.Add(messageDetail);
        //        dc.SaveChanges();
        //    }
        //}

        private void AddPrivateMessageinCache(string fromEmail, string chatToEmail, string userName, string message)
        {
            using (Service.EntityModel.DuAnYTeEntities dc = new Service.EntityModel.DuAnYTeEntities())
            {
                // Save master
                //var master = dc.ThanhViens.ToList().Where(a => a.Email.Equals(fromEmail)).ToList();
                //if (master.Count == 0)
                //{
                //    var result = new ChatPrivateMessageMaster
                //    {
                //        EmailID = fromEmail,
                //        UserName = userName
                //    };
                //    dc.ChatPrivateMessageMasters.Add(result);
                //    dc.SaveChanges();
                //}

                // Save details
                var thanhviengui = dc.ThanhViens.FirstOrDefault(x => x.Email == fromEmail);
                var thanhviennhan = dc.ThanhViens.FirstOrDefault(x => x.Email == chatToEmail);
                var resultDetails = new ChatPrivateMessageDetail
                {
                    id_ThanhVien_Gui = thanhviengui.id,
                    id_ThanhVien_Nhan = thanhviennhan.id,
                    NoiDung = message
                };
                dc.ChatPrivateMessageDetails.Add(resultDetails);
                dc.SaveChanges();
            }
        }
        #endregion

        #endregion

        #endregion

        public void Hello()
        {
            Clients.All.hello();
        }
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}