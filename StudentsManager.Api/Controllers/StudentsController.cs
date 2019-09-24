using StudentsManager.Api.Models;
using StudentsManager.Core.DataTransferObjects;
using StudentsManager.Core.Infrastructure;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentsManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(
            int? gender = null,
            int page = 1, int pageSize = 25,
            string identifier = "",
            string firstName = "",
            string lastName = "",
            string middleName = "")
        {
            var students = await _studentService.GetStudents(
                new PaginationInfoDto
                {
                    PageNumber = page,
                    PageSize = pageSize
                }, identifier, firstName, lastName, middleName, gender
            );

            var pagedResult = _mapper.Adapt<PagedResultDto<StudentsListModel>>(students);

            return new JsonResult(pagedResult);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var dto = await _studentService.GetStudentById(id);

                var model = _mapper.Adapt<StudentModel>(dto);

                return new JsonResult(model);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<bool> DeleteStudent(int id)
        {
            var isSuccess = await _studentService.DeleteStudent(id);

            return isSuccess;
        }

        
        [HttpPost]
        public async Task<StudentModel> CreateStudent(StudentModel studentmodel)
        {
            var dto = await _studentService.CreateStudent(_mapper.Adapt<StudentDto>(studentmodel));

            var model = _mapper.Adapt<StudentModel>(dto);

            return model;
        }

        [HttpPut]
        public async Task<StudentModel> UpdateStudent(StudentModel studentmodel)
        {
            var dto = await _studentService.UpdateStudent(_mapper.Adapt<StudentDto>(studentmodel));

            var model = _mapper.Adapt<StudentModel>(dto);

            return model;
        }
    }
}











