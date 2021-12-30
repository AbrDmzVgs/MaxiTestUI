﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiService.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DoB { get; set; }
        public int EmployeeNumber { get; set; }
        public string Curp { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
    }
}
