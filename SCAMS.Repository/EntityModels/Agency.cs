using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SCAMS.Repository.EntityModels
{
    public class Agency
    {
        [Key]
        public int AgencyID {get;set;}
        public string AgencyName { get; set; }
        public string  OLO { get; set; }
    }
}
