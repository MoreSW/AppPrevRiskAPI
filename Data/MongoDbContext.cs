using MongoDB.Driver;

namespace appPrevencionRiesgos.Data
{
    public class MongoDbContext
    {
        public MongoClient client;
        public IMongoDatabase basicInformationDbContext;
        public IMongoDatabase UserDbContext;
        public IMongoDatabase LocationDbContext;

        public MongoDbContext()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nosimportas:0IPwUx72lVf85eDw@riesgo.wpl5wf1.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            client = new MongoClient(settings);
            //basicInformationDbContext = client.GetDatabase("BasicInformation");
            UserDbContext = client.GetDatabase("UserInfoAPI");
            LocationDbContext = client.GetDatabase("LocationInformationAPI");
        }
    }
}
