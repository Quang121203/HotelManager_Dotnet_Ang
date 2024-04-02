using BackEnd.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BackEnd.Models.DAL
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            #region RoomType - Room's (1-n) relationship:
            modelBuilder.Entity<Room>()
            .HasOne(op => op.RoomType)
            .WithMany(au => au.Rooms)
            .HasForeignKey(op => op.RoomTypeID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
            .HasOne(op => op.Guest)
            .WithMany(au => au.Reservations)
            .HasForeignKey(op => op.GuestID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bill>()
                .HasOne(g => g.Guest)
                .WithMany(b => b.Bills)
                .HasForeignKey(b => b.IDGuest)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Rersevation - Room's (n-n) relationship:
            //ReservationRoom
            modelBuilder.Entity<ReservationRoom>()
                .HasKey(rr => new { rr.RoomID, rr.ReservationID });


            modelBuilder.Entity<ReservationRoom>()
                .HasOne(rr => rr.Room)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(rr => rr.RoomID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservationRoom>()
                .HasOne(rr => rr.Reservation)
                .WithMany(res => res.ReservationRooms)
                .HasForeignKey(rr => rr.ReservationID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}
