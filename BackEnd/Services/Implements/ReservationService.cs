using BackEnd.DataAccess;
using BackEnd.Models.DAL;
using BackEnd.Models.Domains;
using BackEnd.Models.DTOS;
using BackEnd.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.Implements
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRoomService roomService;
        private readonly IBillService billService;
        private readonly AppDBContext dbContext;
        public ReservationService(IUnitOfWork unitOfWork, IRoomService roomService, IBillService billService, AppDBContext dbContext)
        {
            this.unitOfWork = unitOfWork;
            this.roomService = roomService;
            this.billService = billService;
            this.dbContext = dbContext;
        }
        public async Task<object> Cancel(string ReservationId)
        {
            var reservation = await this.unitOfWork.ReservationRepository.GetSingleAsync(ReservationId);
            if (reservation == null || reservation.IsConfirmed == true)
            {
                return new
                {
                    EC = 1,
                    EM = "Reservation not found",
                    DT = "",
                };
            }
            var bill = await this.unitOfWork.BillRepository.GetSingleAsync(d => d.Status == false && d.IDGuest == reservation.GuestID);

            //delete bill
            await this.unitOfWork.BillRepository.DeleteAsync(bill);
            await this.unitOfWork.SaveChangesAsync();

            //delete reservationRoom
            var reservationRooms = await this.unitOfWork.ReservationRoomRepository.GetAsync(d => (d.ReservationID == ReservationId));
            foreach (var reservationRoom in reservationRooms)
            {
                this.dbContext.Set<ReservationRoom>().Remove(reservationRoom);
                await this.dbContext.SaveChangesAsync();
            }

            //delete Reservation
            await this.unitOfWork.ReservationRepository.DeleteAsync(reservation.ReservationID);
            await this.unitOfWork.SaveChangesAsync();

            return new
            {
                EC = 0,
                EM = "Reservation has been cancel",
                DT = "",
            };
        }

        public async Task<object> CheckIn(string ReservationId)
        {
            var reservation = await this.unitOfWork.ReservationRepository.GetSingleAsync(ReservationId);
            if (reservation == null)
            {
                return new
                {
                    EC = 1,
                    EM = "Reservation not found",
                    DT = "",
                };
            }

            reservation.IsConfirmed = true;
            reservation.ConfirmationTime = DateTime.Now;
            await UpdateReservation(reservation);
            await this.unitOfWork.SaveChangesAsync();

            var reservationRooms = await this.unitOfWork.ReservationRoomRepository.GetAsync(d => d.ReservationID == ReservationId);

            //update room
            foreach (var reservationRoom in reservationRooms)
            {
                var room = await this.unitOfWork.RoomRepository.GetSingleAsync(reservationRoom.RoomID);
                room.IsAvaiable = false;
                await this.roomService.UpdateRoom(room);
                await this.unitOfWork.SaveChangesAsync();
            }
            return new
            {
                EC = 0,
                EM = "Reservation has been check in",
                DT = "",
            };
        }

        public async Task<object> CheckOut(string ReservationId)
        {
            var reservation = await this.unitOfWork.ReservationRepository.GetSingleAsync(ReservationId);
            if (reservation == null || reservation.IsConfirmed == false)
            {
                return new
                {
                    EC = 1,
                    EM = "Reservation not found",
                    DT = "",
                };
            }

            //update bill
            Bill bill = await this.unitOfWork.BillRepository.GetSingleAsync(d => (d.IDGuest == reservation.GuestID && d.Status == false));
            bill.Status = true;
            await this.billService.UpdateBill(bill);
            await this.unitOfWork.SaveChangesAsync();

            //update room
            var reservationRooms = await this.unitOfWork.ReservationRoomRepository.GetAsync(d => d.ReservationID == ReservationId);
            foreach (var reservationRoom in reservationRooms)
            {
                var room = await this.unitOfWork.RoomRepository.GetSingleAsync(reservationRoom.RoomID);
                room.IsAvaiable = true;
                await this.roomService.UpdateRoom(room);
                await this.unitOfWork.SaveChangesAsync();
            }

            //delete reservationroom
            var ReservationRooms = await this.unitOfWork.ReservationRoomRepository.GetAsync(d => d.ReservationID == reservation.ReservationID);
            foreach (var ReservationRoom in ReservationRooms)
            {
                dbContext.Set<ReservationRoom>().Remove(ReservationRoom);
                await dbContext.SaveChangesAsync();
            }

            //delete reservation
            await this.unitOfWork.ReservationRepository.DeleteAsync(reservation.ReservationID);
            await this.unitOfWork.SaveChangesAsync();


            return new
            {
                EC = 0,
                EM = "Reservation has been check out",
                DT = "",
            };
        }

        public async Task<object> CreateReservation(Reservation model)
        {
            var guestId = await this.unitOfWork.GuestRepository.GetSingleAsync(model.GuestID);

            if (guestId != null)
            {
                var reservation = new Reservation
                {
                    GuestID = model.GuestID,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    IsConfirmed = false,

                };
                await this.unitOfWork.ReservationRepository.InsertAsync(reservation);
                await this.unitOfWork.SaveChangesAsync();
                return new
                {
                    EC =0 ,
                    EM = "Reservation has been create",
                    DT = reservation,
                };
            }
            return new
            {
                EC = 1,
                EM = "Guest not found",
                DT = "",
            };
        }

        public async Task<object> GetAllReservation()
        {
            List<Reservation> reservations= await this.unitOfWork.ReservationRepository.GetAsync();
            return new
            {
                EC = 0,
                EM = "",
                DT = reservations,
            };
        }

        public async Task<List<Reservation>> GetReservationByDate(DateTime StartTime, DateTime EndTime)
        {
            return await this.unitOfWork.ReservationRepository.GetAsync(d => (DateTime.Compare(StartTime, d.StartTime) > 0 && DateTime.Compare(d.StartTime, StartTime) > 0) || (DateTime.Compare(EndTime, d.EndTime) > 0 && DateTime.Compare(d.EndTime, EndTime) > 0) || (DateTime.Compare(d.StartTime, StartTime) >= 0 && DateTime.Compare(d.EndTime, EndTime) <= 0));

        }

        public async Task<object> GetReservationByGuestID(string GuestId)
        {
            Reservation reservation= await this.unitOfWork.ReservationRepository.GetSingleAsync(d => d.GuestID == GuestId);
            return new
            {
                EC = 0,
                EM = "",
                DT = reservation,
            };
        }

        public async Task<object> GetReservationByID(string id)
        {
            Reservation reservation= await this.unitOfWork.ReservationRepository.GetSingleAsync(id);
            return new
            {
                EC = 0,
                EM = "",
                DT = reservation
            };
        }

        public async Task<Reservation> GetReservationByRoom(string RoomId)
        {
            ReservationRoom reservationRoom = await this.unitOfWork.ReservationRoomRepository.GetSingleAsync(d => d.RoomID == RoomId);
            if (reservationRoom == null)
            {
                return null;
            }
            return await this.unitOfWork.ReservationRepository.GetSingleAsync(d => (d.ReservationID == reservationRoom.ReservationID && d.IsConfirmed == true));
        }

        public async Task<object> GetReservationByWasConfirm(bool isConfirm)
        {
            List<Reservation> reservations= await this.unitOfWork.ReservationRepository.GetAsync(d => d.IsConfirmed == isConfirm);
            return new
            {
                EC = 0,
                EM = "",
                DT = reservations,
            };
        }

        public async Task<object> ReserveRooms(ReservationVM reservationvm)
        {
            RoomType rt = await this.unitOfWork.RoomTypeRepository.GetSingleAsync(d => d.RoomTypeID == reservationvm.RoomTypeId);
            var check = await this.roomService.CheckRoom(reservationvm.StartTime, reservationvm.EndTime, rt, reservationvm.NumberOfRooms);
            if (!check)
            {
                return new
                {
                    EC = 1,
                    EM = "Failed to reserve rooms. Not enough available rooms of the specified type",
                    DT = "",
                };
            }
            //Add guest
            //if exist reservation , not add

            var checkEmail = await this.unitOfWork.GuestRepository.GetSingleAsync(d => d.Email == reservationvm.GuestEmail);
            var guest = new Guest();

            if (checkEmail == null)
            {
                guest.FullName = reservationvm.GuestFullName;
                guest.PhoneNumber = reservationvm.GuestPhoneNumber;
                guest.Email = reservationvm.GuestEmail;
                await this.unitOfWork.GuestRepository.InsertAsync(guest);
                await this.unitOfWork.SaveChangesAsync();
            }
            else
            {
                guest.GuestID = checkEmail.GuestID;
                guest.FullName = reservationvm.GuestFullName;
                guest.PhoneNumber = reservationvm.GuestPhoneNumber;
                guest.Email = reservationvm.GuestEmail;
            }

            //Add reservation
            //if exist reservation , not add
            var checkReservation = await this.unitOfWork.ReservationRepository.GetSingleAsync(d => d.GuestID == guest.GuestID);
            var reservation = new Reservation();
            if (checkReservation == null)
            {
                reservation.GuestID = guest.GuestID;
                reservation.IsConfirmed = false;
                reservation.StartTime = reservationvm.StartTime;
                reservation.EndTime = reservationvm.EndTime;
                await this.unitOfWork.ReservationRepository.InsertAsync(reservation);
                await this.unitOfWork.SaveChangesAsync();
            }
            else
            {
                reservation.ReservationID = checkReservation.ReservationID;
            }

            // Create ReservationRoom records
            var rooms = await this.roomService.GetRoomNotReser(reservationvm.StartTime, reservationvm.EndTime, rt);

            for (int i = 0; i < reservationvm.NumberOfRooms; i++)
            {
                var reservationRoom = new ReservationRoom
                {
                    RoomID = rooms[i].RoomID,
                    ReservationID = reservation.ReservationID
                };
                await this.unitOfWork.ReservationRoomRepository.InsertAsync(reservationRoom);
            }

            await this.unitOfWork.SaveChangesAsync();



            //Bill
            double differenceInDays = (reservationvm.EndTime - reservationvm.StartTime).TotalDays;

            var checkBill = await this.unitOfWork.BillRepository.GetSingleAsync(d => (d.IDGuest == guest.GuestID && d.Status == false));

            if (checkBill == null)
            {
                Bill b = new Bill();
                b.IDGuest = guest.GuestID;
                b.Sum = reservationvm.NumberOfRooms * rt.DailyPrice * differenceInDays;
                b.Status = false;
                await this.unitOfWork.BillRepository.InsertAsync(b);
                await this.unitOfWork.SaveChangesAsync();
            }
            else
            {
                checkBill.Sum = checkBill.Sum + reservationvm.NumberOfRooms * rt.DailyPrice * differenceInDays;             
                await this.billService.UpdateBill(checkBill);
            }

            return new
            {
                EC = 0,
                EM = "Rooms reserved successfully",
                DT = "",
            };
        }

        public async Task<object> UpdateReservation(Reservation model)
        {
            var reservation = await this.unitOfWork.ReservationRepository.GetSingleAsync(model.ReservationID);

            if (reservation == null)
            {
                return new
                {
                    EC = 1,
                    EM = "Reservation not found",
                    DT = "",
                };
            }

            var guestId = await this.unitOfWork.GuestRepository.GetSingleAsync(model.GuestID);

            if (guestId == null)
            {
                return new
                {
                    EC = 1,
                    EM = "Guest not found",
                    DT = "",
                };
            }

            reservation.GuestID = model.GuestID;

            reservation.StartTime = model.StartTime;
            reservation.EndTime = model.EndTime;
            reservation.IsConfirmed = model.IsConfirmed;
            reservation.ConfirmationTime = model.ConfirmationTime;

            this.unitOfWork.ReservationRepository.Update(reservation);
            await this.unitOfWork.SaveChangesAsync();
            return new
            {
                EC = 0,
                EM = "Reservation has been update",
                DT = "",
            };
        }
        
        public async Task<object> DeleteReservation(string id)
        {
            //delete reservationroom
            var ReservationRooms = await this.unitOfWork.ReservationRoomRepository.GetAsync(d => d.ReservationID == id);
            foreach (var ReservationRoom in ReservationRooms)
            {
                dbContext.Set<ReservationRoom>().Remove(ReservationRoom);
                await dbContext.SaveChangesAsync();
            }

            //delete reservation
            bool check = await this.unitOfWork.ReservationRepository.DeleteAsync(id);
            if (check)
            {
                await this.unitOfWork.SaveChangesAsync();
                return new
                {
                    EC = 0,
                    EM = "Reservation has been delete",
                    DT = "",
                };
            }

            return new
            {
                EC = 1,
                EM = "Reservation not found",
                DT = "",
            };
        }
    }
}
