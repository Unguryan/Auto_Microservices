using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using User_GrpcService.Models;

namespace User_GrpcService.Services
{
    public class AuthSerializer
    {
        private const string Path = "AuthUsers.txt";

        public void Serialize(IList<AuthUser> users)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using Stream fStream = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(fStream, users);
        }

        public IList<AuthUser> Deserialize()
        {
            if (!File.Exists(Path))
            { 
                return new List<AuthUser>();
            }

            BinaryFormatter formatter = new BinaryFormatter();

            using Stream fStream = File.OpenRead(Path);
            //fStream.Position = 0;
            var t = formatter.Deserialize(fStream);

            return (IList<AuthUser>)t;
        }
    }
}
