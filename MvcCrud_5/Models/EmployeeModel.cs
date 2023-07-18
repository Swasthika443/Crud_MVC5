using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCrud_5.Models
{
    public class EmployeeModel
    {
        [Key]
        public int id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
      

         public string Emp_Age { get; set; }
        
        public string Mobile { get; set; }
    }
}