namespace OnionAppTraining.Infrastructure.MongoDB
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string UserCollection { get; set; }
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
