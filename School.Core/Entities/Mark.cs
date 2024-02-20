using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Entities
{
    public class Mark
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int ClassId { get; set; }
        
        public int StudentId { get; set; }  
        [Range(0,100)]
        public decimal MarkInPercent { get; set; }
        [ForeignKey(nameof(MaterialId))]
        public Material Material { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }
        [ForeignKey(nameof(StudentId))] 
        public Student student { get; set; }   
    }
}
