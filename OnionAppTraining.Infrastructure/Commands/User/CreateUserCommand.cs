namespace OnionAppTraining.Infrastructure.Commands.User
{
    public class CreateUserCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
    }
}
