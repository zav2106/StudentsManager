using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsManager.Core.DataTransferObjects
{
    public class StudentDto
    {
        public int Id { get; set; }

        public string Identifier { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public int Gender { get; set; }

        public bool IsDeleted { get; set; }
    }
}
