using Microsoft.EntityFrameworkCore;
using BackEnd.Models.Domains;
using BackEnd.Models.DAL;

namespace BackEnd.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;

        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            UserRepository = new Repository<User>(context);
            GuestRepository = new Repository<Guest>(context);
            ReservationRepository = new Repository<Reservation>(context);
            ReservationRoomRepository = new Repository<ReservationRoom>(context);
            RoomRepository = new Repository<Room>(context);
            RoomTypeRepository = new Repository<RoomType>(context);
            BillRepository = new Repository<Bill>(context);
            ServiceRepository = new Repository<Service>(context);
            GuestServiceRepository = new Repository<GuestService>(context);

        }

        public IRepository<User> UserRepository { get; set; }

        public IRepository<Guest> GuestRepository { get; set; }
        public IRepository<Reservation> ReservationRepository { get; set; }
        public IRepository<ReservationRoom> ReservationRoomRepository { get; set; }
        public IRepository<Room> RoomRepository { get; set; }
        public IRepository<RoomType> RoomTypeRepository { get; set; }
        public IRepository<Bill> BillRepository { get; set; }
        public IRepository<Service> ServiceRepository { get; set; }
        public IRepository<GuestService> GuestServiceRepository { get; set; }


        public async Task SaveChangesAsync()
        {
            await this._context.SaveChangesAsync();
        }

    }
}
