using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;

namespace CarConnect.BusinessLayer.interfaces
{
    public interface IReservationRepository
    {
        void Add(Reservation reservation);
        void Delete(int reservationId);
        IEnumerable<Reservation> GetAllReservations();
        Reservation GetById(int reservationId);
        IEnumerable<Reservation> GetReservationsByCustomerId(int customerId);
        void Update(Reservation reservation);
    }
}
