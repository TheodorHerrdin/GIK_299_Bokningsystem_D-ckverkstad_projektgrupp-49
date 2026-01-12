using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireBookingSystem
{
    public class VehicleInformation
    {
        public string RegistrationNumber { get; set; }

        public VehicleInformation(string registrationNumber)
        {
            RegistrationNumber = registrationNumber;
        }
    }
}
