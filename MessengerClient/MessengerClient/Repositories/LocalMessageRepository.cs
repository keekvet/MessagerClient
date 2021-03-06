﻿using MessageCore.Models;
using MessengerClient.Datasource;
using MessengerClient.DbModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengerClient.Repositories
{
    class LocalMessageRepository : SqliteDatasource
    {
        private static TableQuery<LocalMessage> savedMess = Connection.Table<LocalMessage>();

        public static int GetNextLocalId()
        {
            LocalMessage lastMessage = savedMess.LastOrDefault();
            if (lastMessage is null)
                return 1;
            return lastMessage.LocalId + 1;
        }

        public static void AddMessage(LocalMessage message)
        {
            if (message.LocalId == 0)
                message.LocalId = GetNextLocalId();
            if(message.Id == 0 || GetMessageById(message.Id) is null)
                Connection.Insert(message);
        }

        public static List<LocalMessage> GetMessagesWithUsers(string userName1, string userName2)
        {
            return savedMess.Where(m => 
            (m.Sender == userName1 && m.Receiver == userName2) ||
            (m.Sender == userName2 && m.Receiver == userName1)).ToList();
        }

        public static LocalMessage GetMessageByLocalIdAndSenderName(int localId, string senderName)
        {
            return savedMess.Where(m => m.LocalId == localId && m.Sender == senderName).First();
        }

        public static LocalMessage GetMessageById(int id)
        {
            return savedMess.Where(m => m.Id == id).FirstOrDefault();
        }

        public static void RemoveMessageByLocalIdIfWithoutId(int localId)
        {
            savedMess.Delete(m => m.LocalId == localId && m.Id == 0);
        }

        public static LocalMessage GetLastMessageWithUsers(string userName1, string userName2)
        {
            return savedMess.Where(m =>
            (m.Sender == userName1 && m.Receiver == userName2) ||
            (m.Sender == userName2 && m.Receiver == userName1))
                .DefaultIfEmpty(new LocalMessage("", DateTime.MinValue, userName1, userName2)).LastOrDefault();
        }

        public static List<LocalMessage> GetMessagesByUser(string userName)
        {
            return savedMess.Where(m => m.Receiver.Equals(userName) || m.Sender.Equals(userName)).ToList();
        }
        public static void RemoveMessagesByUser(string userName)
        {
            
            List<LocalMessage> userMessages = GetMessagesByUser(userName);
            savedMess.Delete(m => m.Receiver.Equals(userName) || m.Sender.Equals(userName));
        }
    }
}
