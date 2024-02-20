using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Entities
{
    public class Student:Person
    {
        public int StudentId { get; set; }
        [Range(5, 100)]
        public int Age { get; set; }
        public int ClassId { get; set; }    
   
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }

    }
}
