using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.Implements
{
    public class ReservationRoomService : IReservationRoomService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReservationRoomService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<object> CreateReservationRoom(ReservationRoom model)
        {
            var reservationID = await this.unitOfWork.ReservationRepository.GetSingleAsync(model.ReservationID);
            var roomID = await this.unitOfWork.RoomRepository.GetSingleAsync(model.RoomID);
            if (reservationID != null && roomID != null)
            {
                var reservationRoom = new ReservationRoom
                {
                    ReservationID = model.ReservationID,
                    RoomID = model.RoomID,
                };
                await this.unitOfWork.ReservationRoomRepository.InsertAsync(reservationRoom);
                await this.unitOfWork.SaveChangesAsync();
                return new
                {
                    EC = 0,
                    EM = "ReservationRoom has been create",
                    DT = reservationRoom,
                };
            }
            return new
            {
                EC = 1,
                EM = "Reservation or Room not found",
                DT = "",
            };
        }

        public async Task<object> DeleteAllReservationRoom()
        {
            List<ReservationRoom> reservationRooms = await this.unitOfWork.ReservationRoomRepository.GetAsync();

            foreach (var reservationRoom in reservationRooms)
            {
                await this.unitOfWork.ReservationRoomRepository.DeleteAsync(reservationRoom.RoomID);
            }
       
            await this.unitOfWork.SaveChangesAsync();

            return new
            {
                EC = 0,
                EM = "All ReservationRooms have been delete",
                DT = "",
            };
        }

        public async Task<object> GetAllReservationRoom()
        {
            List<ReservationRoom> reservationRooms= await this.unitOfWork.ReservationRoomRepository.GetAsync();
            return new
            {
                EC = 0,
                EM = "",
                DT = reservationRooms,
            };
        }

        public async Task<object> GetAllReservationRoomByReservationID(string ID)
        {
            List<ReservationRoom> reservationRooms= await this.unitOfWork.ReservationRoomRepository.GetAsync(d => d.ReservationID == ID);
            return new
            {
                EC = 0,
                EM = "",
                DT = reservationRooms,
            };
        }

        public async Task<ReservationRoom> GetReservationRoomByRoomID(string ID)
        {
            return await this.unitOfWork.ReservationRoomRepository.GetSingleAsync(d => d.RoomID == ID);
        }
    }
}
