using System;

namespace User_GrpcService.Models
{
    [Serializable]
    public class AuthUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
