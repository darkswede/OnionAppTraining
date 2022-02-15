namespace OnionAppTraining.Infrastructure.Commands.User
{
    public class ChangeUserPassowrdCommand : ICommand
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
