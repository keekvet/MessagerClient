﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MessengerClient.Constant
{
    class Constants
    {
        public const int PORT = 12345;
        public const string IP = "127.0.0.1";

        public const string DATABASE_FILENAME = "local_message_db.db";

        public const string DEFAULT_RECEIVER_TEXT = "none";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DATABASE_FILENAME);
            }
        }
    }
}