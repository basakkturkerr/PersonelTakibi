using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WFAPersonelTakibi.Models
{

    public  class Employee
    {
        public Employee()
        {
            this.Id = new Guid();
        }
        public Guid Id { get; set; }

        [
            Required, 
            MaxLength(50)
        ]
        public string FirstName { get; set; }

        [
            Required, 
            MaxLength(50)
        ]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
        
        public string EMail { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public Department Department { get; set; }

        [
            Required, 
            MaxLength(24)
        ]
        public string Phone { get; set; }

        [
            Required,
            MaxLength(300)     
        ]
        public string Address { get; set; }

        [  
           MaxLength(150)
        ]
        public string ImageUrl { get; set; }



    }
}
