using System;

namespace OnionAppTraining.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Role { get; set; }
        public string Salt { get; protected set; }
        public string UserName { get; protected set; }
        public string FullName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public User(Guid guid, string email, string password, string role, string username, string salt)//TODO weryfikacje
        {
            Id = guid;
            Email = email.ToLowerInvariant();
            Password = password;
            Role = role;
            UserName = username;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
