using BackEnd.Models.Domains;

namespace BackEnd.Services.Interfaces
{
    public interface IBillService
    {
        Task<object> GetAllBill();
        Task<object> GetBillByGuestID(string id);

        Task<List<Bill>> GetBillsByStatus(bool status);
        Task<object> CreateBill(Bill model);
        Task<object> UpdateBill(Bill model);
        Task<bool> DeleteAllBills();
    }
}
