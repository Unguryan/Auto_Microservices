using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UI.Models;

namespace UI.Extra
{
    public static class LoginUtilities
    {
        private static string Path = "LastUser.txt";

        public static AuthUserUI GetSavedUser()
        {
            return Deserialize();
        }

        public static void SaveUser(AuthUserUI auth)
        {
            if(string.IsNullOrEmpty(auth.UserName) || string.IsNullOrEmpty(auth.Password))
            {
                if (!File.Exists(Path))
                {
                    return;
                }

                File.Delete(Path);
                return;
            }

            Serialize(auth);
        }

        private static void Serialize(AuthUserUI user)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using Stream fStream = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(fStream, user);
        }

        private static AuthUserUI Deserialize()
        {
            if (!File.Exists(Path))
            {
                return null;
            }

            BinaryFormatter formatter = new BinaryFormatter();

            using Stream fStream = File.OpenRead(Path);
            return (AuthUserUI)formatter.Deserialize(fStream);
        }
    }
}
