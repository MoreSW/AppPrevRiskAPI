using appPrevencionRiesgos.Data.Entities;
using appPrevencionRiesgos.Data.Repository;
using appPrevencionRiesgos.Exceptions;
using appPrevencionRiesgos.Model;
using appPrevencionRiesgos.Model.Security;
using AutoMapper;
using Microsoft.SharePoint.Client;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using System.Security;

namespace appPrevencionRiesgos.Services
{
    public class UserInformationService : IUserInformationService
    {
        private IUserInformationRepository _userRepository;
        private IMapper _mapper;
        Dictionary<string, bool> hasException = new Dictionary<string, bool>()
            {
                { "Request not Found" , true },
                { "Accepting request" , false },
                { "Already accepted request" , true },
                { "Duplicated request" , true }
            };
        public UserInformationService(IUserInformationRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserConfidenceExtendedModel> AddUserConfidence(UserConfidenceExtendedModel userConfidenceInformation)
        {
            if(userConfidenceInformation.EmailFrom == userConfidenceInformation.EmailTo)
                throw new AlreadyAddedElementException("Failed to add confidence user to list. The email were repeited.");

            var senderUser = await _userRepository.GetOneUserByEmailAsync(userConfidenceInformation.EmailFrom);
            var receiverUser = await _userRepository.GetOneUserByEmailAsync(userConfidenceInformation.EmailTo);

            var confidenceUserPosition = UserConfidenceStatus(senderUser, receiverUser.Email);
            if (hasException[confidenceUserPosition.Item1])
            {
                throw new AlreadyAddedElementException($"Failed to add confidence user to list. {confidenceUserPosition.Item1}.");
            }

            if (confidenceUserPosition.Item1 == "Accepting request")
            {
                senderUser.ConfidenceUsers[confidenceUserPosition.Item2]["email"] = receiverUser.Email;
                senderUser.ConfidenceUsers[confidenceUserPosition.Item2]["status"] = "accepted";
                receiverUser.ConfidenceUsers[confidenceUserPosition.Item2]["email"] = senderUser.Email;
                receiverUser.ConfidenceUsers[confidenceUserPosition.Item2]["status"] = "accepted";

                await _userRepository.UpdateUserAsync(senderUser.UserId, senderUser);
                await _userRepository.UpdateUserAsync(receiverUser.UserId, receiverUser);
            }

            if (confidenceUserPosition.Item1 == "Request not Found")
            {
                var userConfidenceSender = new UserConfidenceSenderEntity(senderUser.Email, receiverUser.Email);
                var userConfidenceReceiver = new UserConfidenceReceiverEntity(receiverUser.Email, senderUser.Email);

                await _userRepository.AddUserConfidenceToListAsync(userConfidenceReceiver);
                await _userRepository.AddUserConfidenceToListAsync(userConfidenceSender);
            }

            return userConfidenceInformation;
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

        public async Task DeleteUserByUidAsync(string uId)
        {
            var result = await GetOneUserByUidAsync(uId);
            await _userRepository.DeleteUserByUidAsync(uId);
            if (result == null)
            {
                throw new Exception("Database Error.");
            }
        }

        public async Task DeleteUserByEmailAsync(string email)
        {
            var result = await GetOneUserByEmailAsync(email);
            await _userRepository.DeleteUserByEmailAsync(email);
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

        public async Task<UserInformationModel> GetOneUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetOneUserByEmailAsync(email);

            if (user == null)
                throw new NotFoundElementException($"Information with email: {email} does not exists.");

            return _mapper.Map<UserInformationModel>(user);
        }

        public async Task<UserInformationModel> GetOneUserByUidAsync(string uId)
        {
            var user = await _userRepository.GetOneUserByUidAsync(uId);

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
            var result = await GetOneUserByUidAsync(uId);
            var informationEntity = _mapper.Map<UserInformationEntity>(user);
            informationEntity.UserId = uId;
            await _userRepository.UpdateUserByEmailAsync(uId, informationEntity);

            if (result != null)
            {
                return _mapper.Map<UserInformationModel>(informationEntity);
            }

            throw new Exception("Database Error.");
        }

        public Tuple<string, int> UserConfidenceStatus (UserInformationEntity userConfidenceEntity, string userConfidenceEmail)
        {
            int i = 0;
            for (i = 0; i < userConfidenceEntity.ConfidenceUsers.Count && userConfidenceEntity.ConfidenceUsers[i]["email"] != userConfidenceEmail; i++) { }
            if (i == userConfidenceEntity.ConfidenceUsers.Count)
                return Tuple.Create("Request not Found", -1);
            if (userConfidenceEntity.ConfidenceUsers[i]["status"] != "sent")
                return Tuple.Create("Accepting request", i);
            if (userConfidenceEntity.ConfidenceUsers[i]["status"] != "accepted")
                return Tuple.Create("Already accepted request", i);
            return Tuple.Create("Duplicated request", i);
        }
    }
}
