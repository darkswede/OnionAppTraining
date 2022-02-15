namespace OnionAppTraining.Infrastructure.Settings
{
    public interface IGeneralSettings
    {
        public string Name { get; set; }
        public bool SeedData { get; set; }
    }
}
