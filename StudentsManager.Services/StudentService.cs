using Microsoft.EntityFrameworkCore;
using StudentsManager.Core.BusinessObjects;
using StudentsManager.Core.DataTransferObjects;
using StudentsManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace StudentsManager.Services
{
    public class StudentService : IStudentService
    {
        private readonly IApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public StudentService(IApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public async Task<bool> CreateStudent(StudentDto student)
        {
            var entity = _mapper.Adapt<Student>(student);
            await _applicationContext.Students.AddAsync(entity);

            await _applicationContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var entity = new Student
            {
                Id = id,
                IsDeleted = true
            };

            var entry = _applicationContext.Entry(entity);

            entry.Property(x => x.IsDeleted).IsModified = true;

            await _applicationContext.SaveChangesAsync();

            return true;
        }

        public async Task<StudentDto> GetStudentById(int id)
        {
            var entity = await _applicationContext
                .Students
                .SingleAsync(x => x.Id == id && x.IsDeleted == false);

            return _mapper.Adapt<StudentDto>(entity);
        }

        public async Task<PagedResultDto<StudentDto>> GetStudents(
            PaginationInfoDto paginationInfo,
            string identifier = "",
            string firstName = "",
            string lastName = "",
            string middleName = "",
            int? gender = null)
        {
            IQueryable<Student> rs = _applicationContext.Students.Where(r => !r.IsDeleted);

            if (!String.IsNullOrEmpty(identifier))
            {
                rs = rs.Where(r => r.Identifier.Contains(identifier));
            }

            if (!String.IsNullOrEmpty(firstName))
            {
                rs = rs.Where(r => r.FirstName.Contains(firstName));
            }

            if (!String.IsNullOrEmpty(lastName))
            {
                rs = rs.Where(r => r.LastName.Contains(identifier));
            }

            if (!String.IsNullOrEmpty(middleName))
            {
                rs = rs.Where(r => r.MiddleName.Contains(middleName));
            }

            if (gender.HasValue) 
            {
                rs = rs.Where(r => r.Gender.Equals(gender.Value));
            }
            

            return await GetDtos(paginationInfo, rs.OrderBy(x => x.Id));
        }

        public async Task<bool> UpdateStudent(StudentDto student)
        {
            var entity = await _applicationContext
                .Students
                .SingleAsync(x => x.Id == student.Id && x.IsDeleted == false);

            entity.Identifier = student.Identifier;
            entity.FirstName = student.FirstName;
            entity.LastName = student.LastName;
            entity.MiddleName = student.MiddleName;

            await _applicationContext.SaveChangesAsync();

            return true;
        }

        protected async Task<PagedResultDto<StudentDto>> GetDtos(PaginationInfoDto paginationInfo, IQueryable<Student> query)
        {
            var result = query.PageResult(paginationInfo.PageNumber, paginationInfo.PageSize);

            if (result.CurrentPage > result.PageCount && result.PageCount > 0)
            {
                result = query.PageResult(result.PageCount, paginationInfo.PageSize);
            }

            return new PagedResultDto<StudentDto>
            {
                Items = (await result.Queryable.ToArrayAsync()).Select(x => _mapper.Adapt<StudentDto>(x)).ToList(),
                TotalRows = result.RowCount,
                TotalPages = result.PageCount,
                CurrentPage = result.CurrentPage,
                ItemsPerPage = result.PageSize
            };
        }
    }
}
