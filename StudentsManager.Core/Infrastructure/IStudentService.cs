using StudentsManager.Core.BusinessObjects;
using StudentsManager.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentsManager.Core.Infrastructure
{
    public interface IStudentService
    {
        Task<StudentDto> GetStudentById(int id);

        Task<PagedResultDto<StudentDto>> GetStudents(
            PaginationInfoDto paginationInfo,
            string identifier = "",
            string firstName = "",
            string lastName = "",
            string middleName = "",
            int? gender = null);

        Task<bool> CreateStudent(StudentDto student);

        Task<bool> UpdateStudent(StudentDto student);

        Task<bool> DeleteStudent(int id);
    }
}
