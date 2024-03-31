using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services.Implements
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork unitOfWork;

        public RoomService(IUnitOfWork unitOfWork) { 
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> CheckRoom(DateTime StartTime, DateTime EndTime, RoomType RoomType, int NumberRoomWantReser)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckRoomByRoomNumber(string roomNumber)
        {
            Room room = await this.unitOfWork.RoomRepository.GetSingleAsync(d => d.RoomNumber == roomNumber);
            if (room != null) return false;
            return true;
        }

        public async Task<bool> CreateRoom(Room model)
        {
            var check = await CheckRoomByRoomNumber(model.RoomNumber);

            if (!check)
            {
                throw new Exception("Room number already exists");

            }
            var roomTypeID = await this.unitOfWork.RoomTypeRepository.GetSingleAsync(model.RoomTypeID);
            if (roomTypeID != null)
            {
                var room = new Room
                {
                    RoomNumber = model.RoomNumber,
                    RoomTypeID = model.RoomTypeID,
                    IsAvaiable = true,
                };
                await this.unitOfWork.RoomRepository.InsertAsync(room);
                await this.unitOfWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAllRoom()
        {
            List<Room> rooms = await GetAllRoom();

            foreach (var room in rooms)
            {
                await this.unitOfWork.RoomRepository.DeleteAsync(room.RoomID);
            }

            await this.unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteRoom(string roomId)
        {
            bool check = await this.unitOfWork.RoomRepository.DeleteAsync(roomId);
            if (check)
            {
                await this.unitOfWork.SaveChangesAsync();
            }
            return check;
        }

        public async Task<List<Room>> GetAllRoom()
        {
            return await this.unitOfWork.RoomRepository.GetAsync();
        }

        public async Task<List<Room>> GetRoomByIsAvailable(bool status)
        {
            return await this.unitOfWork.RoomRepository.GetAsync(d => d.IsAvaiable == status);
        }

        public async Task<List<Room>> GetRoomByRoomTypeId(string roomTypeId)
        {
            var rooms = await this.unitOfWork.RoomRepository.GetAsync(d => d.RoomTypeID == roomTypeId);
            return rooms;
        }

        public async Task<List<Room>> GetRoomByRoomTypeName(string roomTypeName)
        {
            var rooms = await this.unitOfWork.RoomRepository.GetAsync(d => d.RoomType.Name == roomTypeName);
            return rooms;
        }

        public Task<List<Room>> GetRoomIsReser(DateTime StartTime, DateTime EndTime, RoomType RoomType)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomNotReser(DateTime StartTime, DateTime EndTime, RoomType RoomType)
        {
            throw new NotImplementedException();
        }

        public async Task<Room> GetRoomById(string roomId)
        {
            Room room = await this.unitOfWork.RoomRepository.GetSingleAsync(roomId);
            return room;
        }

        public async Task<bool> UpdateRoom(Room model)
        {
            var room = await this.unitOfWork.RoomRepository.GetSingleAsync(model.RoomID);
            var check = await CheckRoomByRoomNumber(model.RoomNumber);

            if (!check && model.RoomNumber != room.RoomNumber)
            {
                throw new Exception("Room number already exists");
            }

            if (room == null)
            {
                throw new Exception("Room not found");
            }
            var roomTypeID = await this.unitOfWork.RoomTypeRepository.GetSingleAsync(model.RoomTypeID);

            if (roomTypeID == null)
            {
                throw new Exception("RoomType not found");
            }

            room.RoomNumber = model.RoomNumber;
            room.RoomTypeID = model.RoomTypeID;
            room.IsAvaiable = model.IsAvaiable;

            this.unitOfWork.RoomRepository.Update(room);
            await this.unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
