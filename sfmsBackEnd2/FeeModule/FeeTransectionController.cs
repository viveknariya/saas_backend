using Dapper;
using db;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace sfmsBackEnd2.FeeModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeTransectionController : ControllerBase
    {
        private readonly IDatabaseConnection databaseConnection;

        public FeeTransectionController(IDatabaseConnection databaseConnection){
            this.databaseConnection = databaseConnection;
        }
        
        [HttpGet]
        public IActionResult Get()
        {

             using var conn = databaseConnection.GetConnection();


            conn.Open();

            var feeTransections = conn.Query<FeeTransection>(
                """
                SELECT
                	id,
                    amount,
                    student_id,
                    comment,
                    date_of_transection,
                    mode_of_transection
                FROM
                	FeeTransection     
                """
            );

            return Ok(feeTransections);

        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {

             using var conn = databaseConnection.GetConnection();

            conn.Open();

            var feeTransections = conn.Query<FeeTransection>(
                """
                SELECT
                	id,
                    amount,
                    student_id,
                    comment,
                    date_of_transection,
                    mode_of_transection
                FROM
                	FeeTransection
                WHERE
                    id = @id 
                """, new { id = id });

            return Ok(feeTransections);

        }
        [HttpGet("{skip}/{take}")]
        public IActionResult GetPage(int skip, int take)
        {
             using var conn = databaseConnection.GetConnection();

            conn.Open();

            var feeTransections = conn.Query<FeeTransection>(
                """
                SELECT
                    id,
                    amount,
                    student_id,
                    comment,
                    date_of_transection,
                    mode_of_transection
                FROM 
                    FeeTransection 
                ORDER BY 
                    identity_ref 
                OFFSET 
                    @skip ROWS 
                FETCH NEXT 
                    @take ROWS ONLY;              
                """, new { skip = skip, take = take });

            return Ok(feeTransections);

        }

        [HttpPost]
        public IActionResult Create(FeeTransection feeTransection)
        {
             using var conn = databaseConnection.GetConnection();

            conn.Open();

            conn.Query<FeeTransection>(
                """
                INSERT INTO FeeTransection
                	(id,
                    amount,
                    student_id,
                    comment,
                    date_of_transection,
                    mode_of_transection)
                VALUES
                	(@id,
                    @amount,
                    @student_id,
                    @comment,
                    @date_of_transection,
                    @mode_of_transection)         
                """, feeTransection);

            return Ok(feeTransection);

        }

        [HttpPut]
        public IActionResult Update(FeeTransection feeTransection)
        {


             using var conn = databaseConnection.GetConnection();

            conn.Open();

            var students = conn.Query<FeeTransection>(
                """
                UPDATE FeeTransection SET
                    amount = @amount,
                    student_id = @student_id,
                    comment = @comment,
                    date_of_transection = @date_of_transection,
                    mode_of_transection = @mode_of_transection
                WHERE
                	id = @id
                """, feeTransection);

            return Ok(feeTransection);

        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {

             using var conn = databaseConnection.GetConnection();


            conn.Open();

            conn.Query<FeeTransection>("DELETE FROM FeeTransection WHERE id = @id", new { id = id });

            return Ok();

        }
    }
}
