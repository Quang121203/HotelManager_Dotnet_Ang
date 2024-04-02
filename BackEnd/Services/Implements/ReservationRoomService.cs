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
        public async Task<bool> CreateReservationRoom(ReservationRoom model)
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
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAllReservationRoom()
        {
            var reservationRooms = await GetAllReservationRoom();

            foreach (var reservationRoom in reservationRooms)
            {
                await this.unitOfWork.ReservationRoomRepository.DeleteAsync(reservationRoom.RoomID);
            }
       
            await this.unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<List<ReservationRoom>> GetAllReservationRoom()
        {
            return await this.unitOfWork.ReservationRoomRepository.GetAsync();
        }

        public async Task<List<ReservationRoom>> GetAllReservationRoomByReservationID(string ID)
        {
            return await this.unitOfWork.ReservationRoomRepository.GetAsync(d => d.ReservationID == ID);
        }

        public async Task<ReservationRoom> GetReservationRoomByRoomID(string ID)
        {
            return await this.unitOfWork.ReservationRoomRepository.GetSingleAsync(d => d.RoomID == ID);
        }
    }
}
