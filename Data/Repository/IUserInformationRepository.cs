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
        Task<UserInformationEntity> GetOneUserByEmailAsync(string email);
        Task UpdateUserByEmailAsync(string uId, UserInformationEntity user);
        Task DeleteUserByUidAsync(string uId);
        Task DeleteUserByEmailAsync(string email);
        Task UpdateUserConfidenceAsync(IUserConfidenceEntity userConfidenceEntity);
    }
}