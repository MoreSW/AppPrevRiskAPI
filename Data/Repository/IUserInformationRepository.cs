using appPrevencionRiesgos.Data.Entities;

namespace appPrevencionRiesgos.Data.Repository
{
    public interface IUserInformationRepository
    {
        Task<IEnumerable<UserInformationEntity>> GetAllUsersAsync();
        Task CreateUser(UserInformationEntity user);
        Task<UserInformationEntity> GetOneUserAsync(string userId);
        Task UpdateUserAsync(string userId, UserInformationEntity user);
        Task DeleteUserAsync(string userId);
        Task<UserInformationEntity> GetOneUserByUidAsync(string uId);
        Task UpdateUserByEmailAsync(string uId, UserInformationEntity user);
        Task DeleteUserByEmailAsync(string uId);
        Task UpdateUserConfidenceAsync(IUserConfidenceEntity userConfidenceEntity);
    }
}