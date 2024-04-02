using BackEnd.Models.Domains;

namespace BackEnd.Services.Interfaces
{
    public interface IBillService
    {
        Task<List<Bill>> GetAllBill();
        Task<List<Bill>> GetBillByGuestID(string id);

        Task<List<Bill>> GetBillsByStatus(bool status);
        Task<bool> CreateBill(Bill model);
        Task<bool> UpdateBill(Bill model);
        Task<bool> DeleteAllBills();
    }
}
