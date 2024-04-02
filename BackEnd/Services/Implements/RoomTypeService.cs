using BackEnd.DataAccess;
using BackEnd.Models.Domains;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services.Implements
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        public RoomTypeService(IUnitOfWork unitOfWork) { 
            this.unitOfWork = unitOfWork;
        }

        public async Task<object> CreateRoomType(RoomType model)
        {
            RoomType check = await this.unitOfWork.RoomTypeRepository.GetSingleAsync(rt=>rt.Name == model.Name);
            if(check == null)
            {
                RoomType roomType = new RoomType();

                roomType.Name = model.Name;
                roomType.Description = model.Description;
                roomType.DailyPrice = model.DailyPrice;

                await this.unitOfWork.RoomTypeRepository.InsertAsync(roomType);
                await this.unitOfWork.SaveChangesAsync();
                return new
                {
                    EC = 0,
                    EM = "RoomType has been create",
                    DT = roomType,
                }; ;
            }
            return new
            {
                EC = 1,
                EM = "RoomType's name already exist",
                DT = "",
            }; ;
        }

        public async Task<object> DeleteRoomType(string id)
        {
          
            bool check = await this.unitOfWork.RoomTypeRepository.DeleteAsync(id);
            if (check)
            {
                await this.unitOfWork.SaveChangesAsync();
                return new
                {
                    EC = 0,
                    EM = "RoomType has been delete",
                    DT = "",
                };
            }
            return new
            {
                EC = 1,
                EM = "RoomType not found",
                DT = "",
            };
        }

        public async Task<object> GetAllRoomType()
        {
            List<RoomType> roomTypes= await this.unitOfWork.RoomTypeRepository.GetAsync();
            return new 
            {
                EC = 0,
                EM = "",
                DT = roomTypes,
            };
        }

        public async Task<object> GetRoomType(string id)
        {
            RoomType roomType = await this.unitOfWork.RoomTypeRepository.GetSingleAsync(id);
            return new
            {
                EC = 0,
                EM = "",
                DT = roomType,
            };
        }

        public async Task<object> UpdateRoomType(RoomType model)
        {
            RoomType roomType = await this.unitOfWork.RoomTypeRepository.GetSingleAsync(model.RoomTypeID);
            RoomType check = await this.unitOfWork.RoomTypeRepository.GetSingleAsync(rt => rt.Name == model.Name);
            if (roomType == null)
            {
                return new
                {
                    EC = 1,
                    EM = "RoomType not found",
                    DT = "",
                };
            }

            if(check!=null && model.Name!= roomType.Name)
            {
                return new
                {
                    EC = 1,
                    EM = "RoomType's name already exist",
                    DT = "",
                };
            }

            roomType.Name = model.Name;
            roomType.Description = model.Description;
            roomType.DailyPrice = model.DailyPrice;
            this.unitOfWork.RoomTypeRepository.Update(roomType);
            await this.unitOfWork.SaveChangesAsync();
            return new
            {
                EC = 0,
                EM = "RoomType has been updated",
                DT = "",
            };
        }
    }
}
