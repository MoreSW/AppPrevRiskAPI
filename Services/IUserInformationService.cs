using appPrevencionRiesgos.Data.Entities;
using appPrevencionRiesgos.Model;
using appPrevencionRiesgos.Model.Security;

namespace appPrevencionRiesgos.Services
{
    public interface IUserInformationService
    {
        Task<IEnumerable<UserInformationModel>> GetAllUsersAsync();
        Task<UserInformationModel> CreateUser(UserInformationModel userInformation);
        Task<UserInformationModel> GetOneUserAsync(string userId);
        Task<UserInformationModel> UpdateUserAsync(string userId, UserInformationModel userInformation);
        Task DeleteUserAsync(string userId);
        Task<UserInformationModel> GetOneUserByEmailAsync(string uId);
        Task<UserInformationModel> UpdateUserByEmailAsync(string uId, UserInformationModel user);
        Task DeleteUserByEmailAsync(string uId);
    }
}
