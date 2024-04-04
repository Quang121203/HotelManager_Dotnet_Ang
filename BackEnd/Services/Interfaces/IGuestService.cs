using BackEnd.Models.Domains;

namespace BackEnd.Services.Interfaces
{
    public interface IGuestService
    {
        Task<object> GetAllGuests();
        Task<object> GetGuest(string id);
        Task<object> CreateGuest(Guest model);
        Task<object> UpdateGuest(Guest model);
        Task<object> DeleteGuest(string id);
        Task<object> DeleteAllGuests();
        Task<object> GetGuestByRoom(string RoomId);
    }
}
