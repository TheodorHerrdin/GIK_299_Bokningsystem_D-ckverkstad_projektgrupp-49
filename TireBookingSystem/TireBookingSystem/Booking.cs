using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireBookingSystem
{

    //Enum för att definiera de olika tjänsterna som verkstaden erbjuder
    public enum ServiceType 
    {
        TireChange = 1,
        Balancing,
        Tirestorage,
        Puncturerepair
    }

    //Huvudklassen för allt som rör bokningarna
    public class Booking
    {
        //Egenskaper som varje bokning måste innehålla
        public CustomerName Name { get; set; }
        public VehicleInformation Vehicle { get; set; }
        public DateTime BookingDate { get; set; }
        public ServiceType Service { get; set; }

        //Konstruktorn körs varje gång en ny bokning skapas
        public Booking(CustomerName name, VehicleInformation vehicle, DateTime date, ServiceType service)
        {
            Name = name;
            Vehicle = vehicle;
            BookingDate = date;
            Service = service;
        }
    }
}
