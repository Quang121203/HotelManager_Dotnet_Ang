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

        public async Task<bool> CreateRoomType(RoomType model)
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
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRoomType(string id)
        {
            bool check = await this.unitOfWork.RoomTypeRepository.DeleteAsync(id);
            if (check)
            {
                await this.unitOfWork.SaveChangesAsync();
            }
            return check;
        }

        public async Task<List<RoomType>> GetAllRoomType()
        {
            return await this.unitOfWork.RoomTypeRepository.GetAsync();
        }

        public async Task<RoomType> GetRoomType(string id)
        {
            return await this.unitOfWork.RoomTypeRepository.GetSingleAsync(id);
        }

        public async Task<bool> UpdateRoomType(RoomType model)
        {
            RoomType roomType = await this.unitOfWork.RoomTypeRepository.GetSingleAsync(model.RoomTypeID);

            if (roomType == null)
            {
                return false;
            }

            roomType.Name = model.Name;
            roomType.Description = model.Description;
            roomType.DailyPrice = model.DailyPrice;
            this.unitOfWork.RoomTypeRepository.Update(roomType);
            await this.unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
