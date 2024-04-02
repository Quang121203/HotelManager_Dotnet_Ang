using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services.Implements
{
    public class GuestService : IGuestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IReservationService reservationService;
        public GuestService(IUnitOfWork unitOfWork, IReservationService reservationService)
        {
            this.unitOfWork = unitOfWork;
            this.reservationService = reservationService;
        }

        public async Task<object> CreateGuest(Guest model)
        {
            Guest guest = new Guest
            {
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Age = model.Age,
                Email = model.Email
            };
            await this.unitOfWork.GuestRepository.InsertAsync(guest);
            await this.unitOfWork.SaveChangesAsync();
            return new
            {
                EC = 0,
                EM = "Guest has been create",
                DT = "",
            };
        }

        public async Task<object> DeleteAllGuests()
        {
            List<Guest> guests = await this.unitOfWork.GuestRepository.GetAsync();
            foreach (var guest in guests)
            {
                await this.unitOfWork.GuestRepository.DeleteAsync(guest.GuestID);
            }
            await this.unitOfWork.SaveChangesAsync();
            return new
            {
                EC = 0,
                EM = "All Guests have been delete",
                DT = "",
            };
        }

        public async Task<object> DeleteGuest(string id)
        {
            bool check = await this.unitOfWork.GuestRepository.DeleteAsync(id);
            if (check)
            {
                await this.unitOfWork.SaveChangesAsync();
                return new
                {
                    EC = 0,
                    EM = "Guest has been delete",
                    DT = "",
                };
            }
            return new
            {
                EC = 1,
                EM = "Guest not found",
                DT = "",
            };
        }

        public async Task<object> GetAllGuests()
        {
            List<Guest> guests= await this.unitOfWork.GuestRepository.GetAsync();
            return new
            {
                EC = 0,
                EM = "",
                DT = guests,
            };
        }

        public async Task<object> GetGuest(string id)
        {
            Guest guest= await this.unitOfWork.GuestRepository.GetSingleAsync(id);
            return new
            {
                EC = 0,
                EM = "",
                DT = guest,
            };
        }

        public async Task<Guest> GetGuestByRoom(string RoomId)
        {
            Reservation reservation = await this.reservationService.GetReservationByRoom(RoomId);

            return await this.unitOfWork.GuestRepository.GetSingleAsync(reservation?.GuestID);
        }

        public async Task<object> UpdateGuest(Guest model)
        {
            Guest guest = await this.unitOfWork.GuestRepository.GetSingleAsync(model.GuestID);

            if (guest == null)
            {
                return new
                {
                    EC = 1,
                    EM = "Guest not found",
                    DT = "",
                };
            }

            guest.PhoneNumber = model.PhoneNumber;
            guest.Email = model.Email;
            guest.FullName = model.FullName;
            guest.Age = model.Age;


            this.unitOfWork.GuestRepository.Update(guest);
            await this.unitOfWork.SaveChangesAsync();

            return new
            {
                EC = 0,
                EM = "Guest has been update",
                DT = "",
            }; ;
        }
    }
}
