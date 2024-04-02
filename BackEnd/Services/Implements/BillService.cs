using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services.Implements
{
    public class BillService : IBillService
    {
        private readonly IUnitOfWork unitOfWork;

        public BillService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateBill(Bill model)
        {
            Guest guest = await this.unitOfWork.GuestRepository.GetSingleAsync(model.IDGuest);
            if (guest == null)
            {
                return false;
            }
            Bill bill = new Bill{
                Sum = model.Sum,
                Status = model.Status,
                IDGuest = model.IDGuest
            };
           

            await this.unitOfWork.BillRepository.InsertAsync(bill);
            await this.unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllBills()
        {
            List<Bill> bills = await this.unitOfWork.BillRepository.GetAsync();

            foreach (var bill in bills)
            {
                await this.unitOfWork.BillRepository.DeleteAsync(bill.ID);
            }

            await this.unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<Bill>> GetAllBill()
        {
            return await this.unitOfWork.BillRepository.GetAsync();
        }

        public async Task<List<Bill>> GetBillByGuestID(string id)
        {
            return await this.unitOfWork.BillRepository.GetAsync(d => d.IDGuest == id);
        }

        public async Task<List<Bill>> GetBillsByStatus(bool status)
        {
            return await this.unitOfWork.BillRepository.GetAsync(d => d.Status == status);
        }

        public async Task<bool> UpdateBill(Bill model)
        {
            var bill = await this.unitOfWork.BillRepository.GetSingleAsync(model.ID);

            if (bill == null)
            {
                throw new Exception("Bill not found");
            }


            var guest = await this.unitOfWork.GuestRepository.GetSingleAsync(model.IDGuest);
            if (guest == null)
            {
                return false;
            }

            bill.Sum = model.Sum;
            bill.Status = model.Status;
            bill.IDGuest = model.IDGuest;

            this.unitOfWork.BillRepository.Update(bill);
            await this.unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
