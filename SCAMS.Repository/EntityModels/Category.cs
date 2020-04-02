using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SCAMS.Repository.EntityModels
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string Title { get; set; }
    }
}
