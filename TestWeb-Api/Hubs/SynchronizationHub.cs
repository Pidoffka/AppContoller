using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeb_Api.Models;
using System.Web;
using TestWeb_Api.Models_for_Metods;

namespace TestWeb_Api.Hubs
{
    public class SynchronizationHub: Hub
    {
        //public async Task SendMessage_Server(SendMessageModel message)
        //{
        //    string client_sender_id = message.Id_User_Sender.ToString();
        //    string client_receiver_id = message.Id_User_Receiver.ToString();
        //    await Clients.Client(client_receiver_id).SendAsync("SendMessage", message);
        //    await Clients.Client(client_sender_id).SendAsync("SendMessage", message);
        //}
        //public async Task ReadMessage_Server(ReadMessageModel message)
        //{
        //    string client_sender_id = message.Id_User_Sender.ToString();
        //    string client_receiver_id = message.Id_User_Reader.ToString();
        //    await Clients.Client(client_receiver_id).SendAsync("ReadMessage", message);
        //    await Clients.Client(client_sender_id).SendAsync("ReadMessage", message);
        //}
        //public async Task DeleteMessage_Server(DeleteMessageModel message)
        //{
        //    string client_sender_id = message.Id_User_Sender.ToString();
        //    string client_receiver_id = message.Id_User_Reader.ToString();
        //    await Clients.Client(client_receiver_id).SendAsync("DeleteMessage", message);
        //    await Clients.Client(client_sender_id).SendAsync("DeleteMessage", message);
        //}

        //public async Task Add_Friend_Server(Friend_test_add_friend friends)
        //{
        //    using(var context = new AppContext())
        //    {
        //        var User_Sender = context.Users.First(x => x.Phone_Number == friends.phone_number_sender);
        //        var User_Receiver = context.Users.First(x => x.Phone_Number == friends.phone_number_receiver);
        //        string user_receiver_id = User_Receiver.Id_User.ToString();
        //        string user_sender_id = User_Sender.Id_User.ToString();
        //        await Clients.Client(user_receiver_id).SendAsync("Add_friend", friends);
        //        await Clients.Client(user_sender_id).SendAsync("Add_friend", friends);
        //    }
        //}

        //public async Task Check_Friend_Server(Friend_test friends)
        //{
        //    using (var context = new AppContext())
        //    {
        //        var User_Sender = context.Users.First(x => x.Phone_Number == friends.phone_number_sender);
        //        var User_Receiver = context.Users.First(x => x.Phone_Number == friends.phone_number_receiver);
        //        string user_receiver_id = User_Receiver.Id_User.ToString();
        //        string user_sender_id = User_Sender.Id_User.ToString();
        //        await Clients.Client(user_receiver_id).SendAsync("Check_friend", friends);
        //        await Clients.Client(user_sender_id).SendAsync("Check_friend", friends);
        //    }
        //}

        //public async Task Show_Friends_Server(User user)
        //{
        //    string user_id = user.Id_User.ToString();
        //    await Clients.Client(user_id).SendAsync("Show_friends", user);
        //}
    }
}
