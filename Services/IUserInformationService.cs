using appPrevencionRiesgos.Data.Entities;
using appPrevencionRiesgos.Model;
using appPrevencionRiesgos.Model.Security;

namespace appPrevencionRiesgos.Services
{
    public interface IUserInformationService
    {
        Task<IEnumerable<UserInformationModel>> GetAllUsersAsync();
        Task<UserInformationModel> CreateUser(UserInformationModel userInformation);
        Task<UserConfidenceExtendedModel> AddUserConfidence(UserConfidenceExtendedModel email);
        Task<UserInformationModel> GetOneUserAsync(string userId);
        Task<UserInformationModel> UpdateUserAsync(string userId, UserInformationModel userInformation);
        Task DeleteUserAsync(string userId);
        Task<UserInformationModel> GetOneUserByUidAsync(string uId);
        Task<UserInformationModel> GetOneUserByEmailAsync(string email);
        Task<UserInformationModel> UpdateUserByEmailAsync(string uId, UserInformationModel user);
        Task DeleteUserByUidAsync(string uId);
        Task DeleteUserByEmailAsync(string email);
    }
}
