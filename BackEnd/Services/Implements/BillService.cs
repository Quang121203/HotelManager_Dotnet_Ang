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
        public async Task<object> CreateBill(Bill model)
        {
            Guest guest = await this.unitOfWork.GuestRepository.GetSingleAsync(model.IDGuest);
            if (guest == null)
            {
                return new
                {
                    EC = 1,
                    EM = "Guest not found",
                    DT = "",
                };
            }
            Bill bill = new Bill{
                Sum = model.Sum,
                Status = model.Status,
                IDGuest = model.IDGuest
            };
           

            await this.unitOfWork.BillRepository.InsertAsync(bill);
            await this.unitOfWork.SaveChangesAsync();
            return new
            {
                EC = 0,
                EM = "Bill has been create",
                DT = bill,
            };
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

        public async Task<object> GetAllBill()
        {
            List<Bill> bills= await this.unitOfWork.BillRepository.GetAsync();
            return new
            {
                EC = 0,
                EM = "",
                DT = bills,
            };
        }

        public async Task<object> GetBillByGuestID(string id)
        {
            List<Bill> bills= await this.unitOfWork.BillRepository.GetAsync(d => d.IDGuest == id);
            return new
            {
                EC = 0,
                EM = "",
                DT = bills,
            };
        }

        public async Task<List<Bill>> GetBillsByStatus(bool status)
        {
            return await this.unitOfWork.BillRepository.GetAsync(d => d.Status == status);
        }

        public async Task<object> UpdateBill(Bill model)
        {
            var bill = await this.unitOfWork.BillRepository.GetSingleAsync(model.ID);

            if (bill == null)
            {
                return new
                {
                    EC = 1,
                    EM = "Bill not found",
                    DT = "",
                };
            }


            var guest = await this.unitOfWork.GuestRepository.GetSingleAsync(model.IDGuest);
            if (guest == null)
            {
                return new
                {
                    EC = 1,
                    EM = "Guest not found",
                    DT = "",
                };
            }

            bill.Sum = model.Sum;
            bill.Status = model.Status;
            bill.IDGuest = model.IDGuest;

            this.unitOfWork.BillRepository.Update(bill);
            await this.unitOfWork.SaveChangesAsync();

            return new
            {
                EC = 0,
                EM = "Bill has been update",
                DT = "",
            };
        }
    }
}
