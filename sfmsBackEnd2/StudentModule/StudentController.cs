using Dapper;
using db;
using Microsoft.AspNetCore.Mvc;
using sfmsbackend2.StudentModule;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace sfmsBackEnd2.StudentModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        
        private readonly IDatabaseConnection databaseConnection;

        public StudentController(IDatabaseConnection databaseConnection){
            this.databaseConnection = databaseConnection;
        }

        [HttpGet]
        public IActionResult Get()
        {
            using var conn = databaseConnection.GetConnection();
            conn.Open();

            var students = conn.Query<Student>("SELECT * FROM Student");

            return Ok(students);

        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {

            using var conn = databaseConnection.GetConnection();
            conn.Open();

            var student = conn.Query<Student>(
                """
                    SELECT 
                    id,
                    first_name,
                    last_name,
                    standard,
                    parents_name,
                    parents_mobile,
                    comment,
                    school_name,
                    date_of_birth,
                    date_of_admission,
                    gender 
                    FROM Student WHERE id = @id
                """
                , new { id = id });

            return Ok(student);

        }

        [HttpPost]
        public IActionResult Create(Student student)
        {

            using var conn = databaseConnection.GetConnection();
            conn.Open();

            conn.Query<Student>(
                """
                INSERT INTO Student (
                    first_name,
                    last_name,
                    standard,
                    parents_name,
                    parents_mobile,
                    comment,
                    school_name,
                    date_of_birth,
                    date_of_admission,
                    gender,
                    fee_structure_id
                    ) 
                    VALUES (
                        @first_name,
                        @last_name,
                        @standard,
                        @parents_name,
                        @parents_mobile,
                        @comment,
                        @school_name,
                        @date_of_birth,
                        @date_of_admission,
                        @gender,
                        @fee_structure_id
                        );               
                """, student);

            return Ok(student);

        }

        [HttpPut]
        public IActionResult Update(Student student)
        {
            using var conn = databaseConnection.GetConnection();
            conn.Open();

            conn.Query<Student>(
                """
                UPDATE Student SET 
                    first_name = @first_name,
                    last_name = @last_name,
                    standard = @standard,
                    parents_name = @parents_name,
                    parents_mobile = @parents_mobile,
                    comment = @comment,
                    school_name = @school_name,
                    date_of_birth = @date_of_birth,
                    date_of_admission = @date_of_admission,
                    gender = @gender
                WHERE id = @id
                """, student);

            return Ok(student);

        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
           using var conn = databaseConnection.GetConnection();
            conn.Open();

            conn.Query<Student>("DELETE FROM Student WHERE id = @id", new { id = id });

            return Ok();

        }


    }
}
