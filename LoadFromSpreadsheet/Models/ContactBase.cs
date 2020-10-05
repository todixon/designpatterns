using System;
using System.Collections.Generic;
using System.Text;

namespace LoadFromSpreadsheet.Models
{
    public class ContactBase
    {
        public Guid ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IFG_ExternalId { get; set; }
        public string IFG_NPN { get; set; }
    }
}
