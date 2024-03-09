using System;
using System.ComponentModel.DataAnnotations;

namespace sfmsBackEnd2.FeeModule
{
    public class FeeStructure
    {
        [Key]
        public int id { get; set; }
        public string structure_name { get; set; }
        public double amount { get; set; }
        public string period { get; set; }
        public string offset { get; set; }
        public string comment { get; set; }

    }
}