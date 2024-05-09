using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonCore.Entities.Order
{
    public class Address
    {
        public Address(string firstName, string lastNAme, string street, string city, string country)
        {
            FirstName = firstName;
            LastNAme = lastNAme;
            Street = street;
            City = city;
            Country = country;
        }
        public Address()
        {
            
        }

        public string FirstName { get; set; }
        public string LastNAme { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
