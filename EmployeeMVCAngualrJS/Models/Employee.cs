using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeMVCAngualrJS.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column(TypeName = "date"), Required]
        public DateTime JoiningDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [ForeignKey("Gender")]
        public int GenderId { get; set; }

        public Gender Gender { get; set; }
        [NotMapped]
        public string GName { get; set; }
    }
}