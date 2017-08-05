using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using Service;
using Service.EntityModel;
using System.Web.Http.Routing;
using System.Web.Mvc;

namespace Du_An_Y_Te.Controllers
{
    public class ChatRoomHub : Hub
    {
        NoiDungTinNhanPhongThaoLuan_Service NoiDungTinNhanPhongThaoLuanService = new NoiDungTinNhanPhongThaoLuan_Service();
        ChatUserDetailsRoom_Service ChatUserDetailsRoomService = new ChatUserDetailsRoom_Service();
        ThanhVien_Service ThanhVienService = new ThanhVien_Service();
        public class UserDetail
        {
            public int id_ThanhVien { get; set; }
            public string ConnectionId { get; set; }
            public string UserName { get; set; }
            public string CurrentMessage { get; set; }
            public string AnhDaiDien { get; set; }


        }
        public class MessageRoom
        {
            public UserDetail ThanhVien { get; set; }
            public string NoiDung { get; set; }
            public DateTime NgayTao { get; set; }
        }
        #region Connect
        public void Connect(int? id_ThanhVien, int id_Phong)
        {
            if (id_ThanhVien != null)
            {
                var id = Context.ConnectionId;

                var item = ChatUserDetailsRoomService.GetByIdThanhVien((int)id_ThanhVien);
                if (item != null)
                {
                    ChatUserDetailsRoomService.DeleteByIdThanhVien((int)id_ThanhVien);
                    // Disconnect
                    Clients.All.onUserDisconnectedExisting(item.ConnectionId, (int)id_ThanhVien, id_Phong);
                }

                var Users = ChatUserDetailsRoomService.GetById_Phong(id_Phong);
                var thanhvien = ThanhVienService.GetById((int)id_ThanhVien);
                var thanhvienChatSend = new UserDetail()
                {
                    id_ThanhVien = thanhvien.id,
                    ConnectionId = id,
                    UserName = thanhvien.TaiKhoan,
                    AnhDaiDien = thanhvien.Picture,

                };
                if (Users.Where(x => x.id == id_ThanhVien).ToList().Count == 0)
                {
                    var userdetails = new ChatUserDetailsRoom() { ConnectionId = id, id_PhongThaoLuan = id_Phong, id_ThanhVien = (int)id_ThanhVien, NgayTao = DateTime.Now, TrangThai = true };

                    ChatUserDetailsRoomService.Add(userdetails);

                    // send to caller
                    List<UserDetail> connectedUsers = new List<UserDetail>();
                    var allUserDetal = ChatUserDetailsRoomService.GetById_Phong(id_Phong);
                    for (int i = 0; i < allUserDetal.Count; i++)
                    {
                        var thv = ThanhVienService.GetById(allUserDetal[i].id_ThanhVien);
                        connectedUsers.Add(new UserDetail()
                        {
                            id_ThanhVien = thv.id,
                            ConnectionId = allUserDetal[i].ConnectionId,
                            UserName = thv.TaiKhoan,
                            AnhDaiDien = thv.Picture
                        });
                    }
                    var allMessage = NoiDungTinNhanPhongThaoLuanService.GetAll(id_Phong);
                    List<MessageRoom> CurrentMessage = new List<MessageRoom>();
                    foreach (var mess in allMessage)
                    {
                        var userdetail = new UserDetail() { id_ThanhVien = mess.id_ThanhVien, AnhDaiDien = mess.ThanhVien.Picture, UserName = mess.ThanhVien.TaiKhoan };
                        CurrentMessage.Add(new MessageRoom() { ThanhVien = userdetail, NoiDung = mess.NoiDung, NgayTao = mess.NgayTao.Value });
                    }
                    Clients.Caller.onConnected(id, thanhvienChatSend, connectedUsers, CurrentMessage, id_Phong);
                }

                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, thanhvienChatSend, id_Phong);
            }
            else
            {
                // send to caller
                List<UserDetail> connectedUsers = new List<UserDetail>();
                var allUserDetal = ChatUserDetailsRoomService.GetById_Phong(id_Phong);
                for (int i = 0; i < allUserDetal.Count; i++)
                {
                    var thv = ThanhVienService.GetById(allUserDetal[i].id_ThanhVien);
                    connectedUsers.Add(new UserDetail()
                    {
                        id_ThanhVien = thv.id,
                        ConnectionId = allUserDetal[i].ConnectionId,
                        UserName = thv.TaiKhoan,
                        AnhDaiDien = thv.Picture
                    });
                }
                var allMessage = NoiDungTinNhanPhongThaoLuanService.GetAll(id_Phong);
                List<MessageRoom> CurrentMessage = new List<MessageRoom>();
                foreach (var mess in allMessage)
                {
                    var userdetail = new UserDetail() { id_ThanhVien = mess.id_ThanhVien, AnhDaiDien = mess.ThanhVien.Picture, UserName = mess.ThanhVien.TaiKhoan };
                    CurrentMessage.Add(new MessageRoom() { ThanhVien = userdetail, NoiDung = mess.NoiDung, NgayTao = mess.NgayTao.Value });
                }
                Clients.Caller.getAllUser(connectedUsers, CurrentMessage,id_Phong);

            }




        }
        #endregion

        #region Disconnect
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            using (Service.EntityModel.DuAnYTeEntities dc = new Service.EntityModel.DuAnYTeEntities())
            {
                var item = dc.ChatUserDetailsRooms.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                if (item != null)
                {
                    //item.TrangThai = false;
                    dc.ChatUserDetailsRooms.Remove(item);
                    dc.SaveChanges();

                    var idConection = Context.ConnectionId;
                    Clients.All.onUserDisconnected(idConection, item.id_ThanhVien);
                }
            }
            return base.OnDisconnected(stopCalled);
        }
        #endregion


        #region gửi tin nhắn
        public void SendMessageToRoom(int id_thanhvien, string message, int id_Phong)
        {
            NoiDungTinNhanPhongThaoLuan noidungtinnhan = new NoiDungTinNhanPhongThaoLuan() { id_PhongThaoLuan = id_Phong, id_ThanhVien = id_thanhvien, NoiDung = message, NgayTao = DateTime.Now, TrangThai = true };
            NoiDungTinNhanPhongThaoLuanService.Add(noidungtinnhan);
            // Call the broadcastMessage method to update clients.
            var thanhvien = ThanhVienService.GetById((int)id_thanhvien);
            var thanhvienChatSend = new UserDetail()
            {
                id_ThanhVien = thanhvien.id,
                UserName = thanhvien.TaiKhoan,
                AnhDaiDien = thanhvien.Picture,
            };
            Clients.All.messageReceived(thanhvienChatSend, message, id_Phong, DateTime.Now);
        }
        #endregion
    }
}