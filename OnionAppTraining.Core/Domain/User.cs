using System;

namespace OnionAppTraining.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string UserName { get; protected set; }
        public string FullName { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public User(string email, string password, string username, string salt)//TODO weryfikacje
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Password = password;
            UserName = username;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
