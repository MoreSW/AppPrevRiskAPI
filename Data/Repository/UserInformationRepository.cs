using appPrevencionRiesgos.Data.Entities;
using Microsoft.SharePoint.Client;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace appPrevencionRiesgos.Data.Repository
{
    public class UserInformationRepository : IUserInformationRepository
    {
        internal MongoDbContext _mongoRepository = new MongoDbContext();
        private IMongoCollection<UserInformationEntity> collection;
        public UserInformationRepository()
        {
            collection = _mongoRepository.UserDbContext.GetCollection<UserInformationEntity>("UserInfoAPI");
        }
        public async Task CreateUser(UserInformationEntity user)
        {
            await collection.InsertOneAsync(user);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var userToDelete = Builders<UserInformationEntity>.Filter.Eq(i => i.Id, new ObjectId(userId));
            await collection.DeleteOneAsync(userToDelete);
        }

        public async Task DeleteUserByEmailAsync(string uId)
        {
            var userToDelete = Builders<UserInformationEntity>.Filter.Eq(i => i.UserId, uId);
            await collection.DeleteOneAsync(userToDelete);
        }

        public async Task<IEnumerable<UserInformationEntity>> GetAllUsersAsync()
        {
            return await collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<UserInformationEntity> GetOneUserAsync(string userId)
        {
            return await collection.FindAsync(new BsonDocument { { "_id", new ObjectId(userId) } }).Result.FirstAsync();
        }

        public async Task<UserInformationEntity> GetOneUserByEmailAsync(string uId)
        {
            return await collection.FindAsync(new BsonDocument { { "UserId", uId } }).Result.FirstAsync();
        }

        public async Task UpdateUserAsync(string userId, UserInformationEntity user)
        {
            var userToUpdate = Builders<UserInformationEntity>.Filter.Eq(i => i.Id, user.Id);
            await collection.ReplaceOneAsync(userToUpdate, user);
        }

        public async Task UpdateUserByEmailAsync(string uId, UserInformationEntity user)
        {
            var userToUpdate = Builders<UserInformationEntity>.Filter.Eq(i => i.UserId, user.UserId);
            await collection.ReplaceOneAsync(userToUpdate, user);
        }
    }
}
