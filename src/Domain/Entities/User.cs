using System;

namespace Domain.Entities
{
    public class User 
    {
        public Guid Id { get; set; }
        public ulong UserId { get; set; }
        public string UserName { get; set; }

        public User(ulong userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}