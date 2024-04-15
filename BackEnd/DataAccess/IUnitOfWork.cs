namespace BackEnd.DataAccess
{
    using Models.Domains;
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; set; }
        IRepository<Guest> GuestRepository { get; set; }
        IRepository<Reservation> ReservationRepository { get; set; }
        IRepository<ReservationRoom> ReservationRoomRepository { get; set; }
        IRepository<Room> RoomRepository { get; set; }
        IRepository<RoomType> RoomTypeRepository { get; set; }
        IRepository<Bill> BillRepository { get; set; }
        IRepository<Token> TokenRepository { get; set; }

        Task SaveChangesAsync();
    }
}
