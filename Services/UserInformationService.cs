using appPrevencionRiesgos.Data.Entities;
using appPrevencionRiesgos.Data.Repository;
using appPrevencionRiesgos.Exceptions;
using appPrevencionRiesgos.Model;
using appPrevencionRiesgos.Model.Security;
using AutoMapper;
using Microsoft.SharePoint.Client;
using Microsoft.VisualBasic;
using MongoDB.Bson;

namespace appPrevencionRiesgos.Services
{
    public class UserInformationService : IUserInformationService
    {
        private IUserInformationRepository _userRepository;
        private IMapper _mapper;
        public UserInformationService(IUserInformationRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserInformationModel> CreateUser(UserInformationModel userInformation)
        {
            var userEntity = _mapper.Map<UserInformationEntity>(userInformation);
            await _userRepository.CreateUser(userEntity);
            if (true)
            {
                return _mapper.Map<UserInformationModel>(userEntity);
            }
            throw new Exception("Database Error.");
        }

        public async Task DeleteUserAsync(string userId)
        {
            var result = await GetOneUserAsync(userId);
            await _userRepository.DeleteUserAsync(userId);
            if (result == null)
            {
                throw new Exception("Database Error.");
            }
        }

        public async Task DeleteUserByEmailAsync(string uId)
        {
            var result = await GetOneUserByEmailAsync(uId);
            await _userRepository.DeleteUserByEmailAsync(uId);
            if (result == null)
            {
                throw new Exception("Database Error.");
            }
        }

        public async Task<IEnumerable<UserInformationModel>> GetAllUsersAsync()
        {
            var informationEntityList = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserInformationModel>>(informationEntityList);
        }

        public async Task<UserInformationModel> GetOneUserAsync(string userId)
        {
            var user = await _userRepository.GetOneUserAsync(userId);

            if (user == null)
                throw new NotFoundElementException($"Information with id:{userId} does not exists.");

            return _mapper.Map<UserInformationModel>(user);
        }

        public async Task<UserInformationModel> GetOneUserByEmailAsync(string uId)
        {
            var user = await _userRepository.GetOneUserByEmailAsync(uId);

            if (user == null)
                throw new NotFoundElementException($"Information with userId: {uId} does not exists.");

            return _mapper.Map<UserInformationModel>(user);
        }

        public async Task<UserInformationModel> UpdateUserAsync(string userId, UserInformationModel userInformation)
        {
            var result = await GetOneUserAsync(userId);
            var informationEntity = _mapper.Map<UserInformationEntity>(userInformation);
            informationEntity.Id = new ObjectId(userId);
            await _userRepository.UpdateUserAsync(userId, informationEntity);

            if (result != null)
            {
                return _mapper.Map<UserInformationModel>(informationEntity);
            }

            throw new Exception("Database Error.");
        }

        public async Task<UserInformationModel> UpdateUserByEmailAsync(string uId, UserInformationModel user)
        {
            var result = await GetOneUserByEmailAsync(uId);
            var informationEntity = _mapper.Map<UserInformationEntity>(user);
            informationEntity.UserId = uId;
            await _userRepository.UpdateUserByEmailAsync(uId, informationEntity);

            if (result != null)
            {
                return _mapper.Map<UserInformationModel>(informationEntity);
            }

            throw new Exception("Database Error.");
        }
    }
}
