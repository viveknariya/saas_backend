using System;

namespace sfmsBackEnd2.FeeModule
{
    public class FeeStructure
    {
        public Guid id { get; set; }
        public string structure_name { get; set; }
        public double amount { get; set; }
        public string period { get; set; }
        public string offset { get; set; }
        public string comment { get; set; }

    }
}