using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiContentHub.Models
{
    public class HomeProduct
    {
        //public String ProductName { get; set; }
        public String Address { get; set; }
        public Decimal SqFt { get; set; }
        //public String ProductNumber { get; set; }
        //public String City { get; set; }
        public Int32 NoOfBedrooms { get; set; }
        public String NoOfBathrooms { get; set; }
        //public Int32 BuildYear { get; set; }
        //public Decimal LotSize { get; set; }
        //public Decimal SftPrice { get; set; }
        //public String Construction { get; set; }
        //public String Roof { get; set; }
        //public String Exterior { get; set; }
        //public Int32 Stories { get; set; }
        //public String DiningRoom { get; set; }
        //public String FamilyRoom { get; set; }
        //public Int32 Garage { get; set; }
        public Decimal Price { get; set; }
        public String ProductFamily { get; set; }
        public String MasterImageURL { get; set; }
    }
}
