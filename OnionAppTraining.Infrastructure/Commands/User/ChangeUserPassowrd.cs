namespace OnionAppTraining.Infrastructure.Commands.User
{
    public class ChangeUserPassowrd : ICommand
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
