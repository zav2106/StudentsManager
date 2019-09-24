using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsManager.Core.BusinessObjects
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(16)]
        public string Identifier { get; set; }

        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(40)]
        public string LastName { get; set; }

        [StringLength(60)]
        public string MiddleName { get; set; }

        [Required]
        public int Gender { get; set; }

        public bool IsDeleted { get; set; }
    }
}
