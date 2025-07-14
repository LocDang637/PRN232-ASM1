using SmokeQuit.Services.LocDPX;
using SmokeQuit.SoapAPIServices.LocDPX.SoapModels;
using System.ServiceModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmokeQuit.SoapAPIServices.LocDPX.SoapServices
{
    [ServiceContract]
    public interface IChatsLocDpxSoapService
    {
        [OperationContract]
        Task<List<ChatsLocDpx>> GetAllAsync();
        [OperationContract]
        Task<ChatsLocDpx> GetByIdAsync(int userId);
        [OperationContract]
        Task<int> CreateAsync(ChatsLocDpx chat);
        [OperationContract]
        Task<ChatsLocDpx> UpdateAsync(ChatsLocDpx chat);
        [OperationContract]
        Task<int> DeleteAsync(int chatId);

    }
    public class ChatsLocDpxSoapService : IChatsLocDpxSoapService
    {
        private readonly IServiceProviders _serviceProviders;

        public ChatsLocDpxSoapService(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders ?? throw new ArgumentNullException(nameof(serviceProviders));
        }
        public async Task<int> CreateAsync(ChatsLocDpx chat)
        {
            try
            {
                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

                var ChatJsonString = JsonSerializer.Serialize(chat, opt);
                var ChatRMO = JsonSerializer.Deserialize<SmokeQuit.Repository.LocDPX.Models.ChatsLocDpx>(ChatJsonString, opt);

                var result = await _serviceProviders.ChatsService.CreateAsync(ChatRMO);

                return result;
            } 
            catch (Exception ex) { }

            return 0;

        }

        public Task<int> DeleteAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChatsLocDpx>> GetAllAsync()
        {
            try
            {
                var chats = await _serviceProviders.ChatsService.GetAllAsync();

                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

                var cashDepositSlipJsonString = JsonSerializer.Serialize(chats, opt);

                var result = JsonSerializer.Deserialize<List<ChatsLocDpx>>(cashDepositSlipJsonString, opt);

                return result;
            }
            catch (Exception ex) { }

            return new List<ChatsLocDpx>();
        }

        public Task<ChatsLocDpx> GetByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ChatsLocDpx> UpdateAsync(ChatsLocDpx chat)
        {
            throw new NotImplementedException();
        }
    }
}
