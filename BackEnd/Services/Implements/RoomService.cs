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

        public async Task<bool> CheckRoom(DateTime StartTime, DateTime EndTime, RoomType RoomType, int NumberRoomWantReser)
        {
            var roomNotResers = await GetRoomNotReser(StartTime, EndTime, RoomType);
            if (NumberRoomWantReser > roomNotResers.Count)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CheckRoomByRoomNumber(string roomNumber)
        {
            Room room = await this.unitOfWork.RoomRepository.GetSingleAsync(d => d.RoomNumber == roomNumber);
            if (room != null) return false;
            return true;
        }

        public async Task<object> CreateRoom(Room model)
        {
            var check = await CheckRoomByRoomNumber(model.RoomNumber);

            if (!check)
            {
                return new
                {
                    EC = 1,
                    EM = "Room's number already exist",
                    DT = "",
                };

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
                return new
                {
                    EC = 0,
                    EM = "Room has been create",
                    DT = room,
                };
            }

            return new
            {
                EC = 1,
                EM = "RoomType not found",
                DT = "",
            };
        }

        public async Task<object> DeleteAllRoom()
        {
            List<Room> rooms = await this.unitOfWork.RoomRepository.GetAsync();

            foreach (var room in rooms)
            {
                await this.unitOfWork.RoomRepository.DeleteAsync(room.RoomID);
            }

            await this.unitOfWork.SaveChangesAsync();

            return new
            {
                EC = 0,
                EM = "All rooms have been delete",
                DT = "",
            }; ;
        }

        public async Task<object> DeleteRoom(string roomId)
        {
            bool check = await this.unitOfWork.RoomRepository.DeleteAsync(roomId);
            if (check)
            {
                await this.unitOfWork.SaveChangesAsync();
                return new
                {
                    EC = 0,
                    EM = "Room has been delete",
                    DT = "",
                };
            }
            return new
            {
                EC = 1,
                EM = "Room not found",
                DT = "",
            };
        }

        public async Task<object> GetAllRoom()
        {
            List<Room> rooms = await this.unitOfWork.RoomRepository.GetAsync();
            return new
            {
                EC = 0,
                EM = "",
                DT = rooms,
            };
        }

        public async Task<object> GetRoomByIsAvailable(bool status)
        {
            List<Room> rooms= await this.unitOfWork.RoomRepository.GetAsync(d => d.IsAvaiable == status);
            return new
            {
                EC = 0,
                EM = "",
                DT = rooms,
            };
        }

        public async Task<object> GetRoomByRoomTypeId(string roomTypeId)
        {
            List<Room> rooms = await this.unitOfWork.RoomRepository.GetAsync(d => d.RoomTypeID == roomTypeId);
            return new
            {
                EC = 0,
                EM = "",
                DT = rooms,
            };
        }

        public async Task<object> GetRoomByRoomTypeName(string roomTypeName)
        {
            List<Room> rooms = await this.unitOfWork.RoomRepository.GetAsync(d => d.RoomType.Name == roomTypeName);
            return new
            {
                EC = 0,
                EM = "",
                DT = rooms,
            };
        }

        public async Task<List<Room>> GetRoomIsReser(DateTime StartTime, DateTime EndTime, RoomType RoomType)
        {
            var reservations = await this.unitOfWork.ReservationRepository.GetAsync(d => ((DateTime.Compare(StartTime, d.StartTime) > 0 && DateTime.Compare(StartTime, d.EndTime) < 0) || (DateTime.Compare(EndTime, d.StartTime) > 0 && DateTime.Compare(EndTime, d.EndTime) < 0) || (DateTime.Compare(d.StartTime, StartTime) >= 0 && DateTime.Compare(d.EndTime, EndTime) <= 0)));

            List<Room> rooms = new List<Room>();
            foreach (var reservation in reservations)
            {
                var reservationRooms = await this.unitOfWork.ReservationRoomRepository.GetAsync(d => d.ReservationID == reservation.ReservationID);
                foreach (var reservationRoom in reservationRooms)
                {
                    Room room = await this.unitOfWork.RoomRepository.GetSingleAsync(reservationRoom.RoomID);
                    if (room.RoomTypeID == RoomType.RoomTypeID)
                    {
                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }

        public async Task<List<Room>> GetRoomNotReser(DateTime StartTime, DateTime EndTime, RoomType RoomType)
        {
            List<Room> rooms =  await this.unitOfWork.RoomRepository.GetAsync(d => d.RoomTypeID == RoomType.RoomTypeID); ;
            List<Room> roomIsResers = await GetRoomIsReser(StartTime, EndTime, RoomType);
            List<Room> roomNotResers = rooms.Except(roomIsResers).ToList();
            return roomNotResers;
        }

        public async Task<object> GetRoomById(string roomId)
        {
            Room room = await this.unitOfWork.RoomRepository.GetSingleAsync(roomId);
            return new
            {
                EC = 0,
                EM = "",
                DT = room,
            };
        }

        public async Task<object> UpdateRoom(Room model)
        {
            var room = await this.unitOfWork.RoomRepository.GetSingleAsync(model.RoomID);
            var check = await CheckRoomByRoomNumber(model.RoomNumber);

            if (room == null)
            {
                return new
                {
                    EC = 1,
                    EM = "Room not found",
                    DT = "",
                };
            }

            if (!check && model.RoomNumber != room.RoomNumber)
            {
                return new
                {
                    EC = 1,
                    EM = "Room number already exists",
                    DT = "",
                };
            }

            
            var roomTypeID = await this.unitOfWork.RoomTypeRepository.GetSingleAsync(model.RoomTypeID);

            if (roomTypeID == null)
            {
                return new
                {
                    EC = 1,
                    EM = "RoomType not found",
                    DT = "",
                };
            }

            room.RoomNumber = model.RoomNumber;
            room.RoomTypeID = model.RoomTypeID;
            room.IsAvaiable = model.IsAvaiable;

            this.unitOfWork.RoomRepository.Update(room);
            await this.unitOfWork.SaveChangesAsync();
            return new
            {
                EC = 0,
                EM = "Room has been updated",
                DT = "",
            };
        }
    }
}
