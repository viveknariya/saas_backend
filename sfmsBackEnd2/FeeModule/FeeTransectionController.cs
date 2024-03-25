using Dapper;
using db;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using sfmsbackend2.StudentModule;
using System.Transactions;

namespace sfmsBackEnd2.FeeModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeTransectionController : ControllerBase
    {
        private readonly IDatabaseConnection databaseConnection;

        public FeeTransectionController(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        [HttpGet]
        public IActionResult Get()
        {

            using var conn = databaseConnection.GetConnection();


            conn.Open();

            var feeTransections = conn.Query(
                """
                SELECT
                   	ft.id,
                    s.first_name,
                    s.last_name,
                    s.standard,
                    ft.is_credit,
                    ft.amount,
                    ft.student_id,
                    ft.comment,
                    ft.date_of_transection,
                    ft.mode_of_transection
                FROM
                   	FeeTransection AS ft
                LEFT JOIN 
                    Student AS s 
                ON 
                    ft.student_id = s.id;
                """
            );

            return Ok(feeTransections);

        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
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

        [HttpGet("student/{id}")]
        public IActionResult GetByStudentID(int id)
        {

            using var conn = databaseConnection.GetConnection();

            conn.Open();

            var feeTransections = conn.Query<FeeTransection>(
                """
                SELECT
                    *
                FROM
                	FeeTransection
                WHERE
                    student_id = @id 
                """, new { id = id });

            return Ok(feeTransections);

        }

        [HttpPost]
        public IActionResult Create(FeeTransection feeTransection)
        {
            
            using var conn = databaseConnection.GetConnection();

            conn.Open();

            using (var scope = new TransactionScope())
            {
                conn.Query<FeeTransection>(
                    """
                        INSERT INTO FeeTransection
                        (id,
                        amount,
                        student_id,
                        comment,
                        date_of_transection,
                        mode_of_transection,
                        is_credit,
                        type_of_fee
                        )
                        VALUES
                        (@id,
                        @amount,
                        @student_id,
                        @comment,
                        @date_of_transection,
                        @mode_of_transection,
                        @is_credit,
                        @type_of_fee
                        );
                     """, feeTransection);

                if(feeTransection.is_credit == true)
                {
                    conn.Execute("UPDATE FeeAnalytics SET paid_fee = paid_fee + @amount WHERE student_id = @student_id;", feeTransection);
                }
                else
                {
                    conn.Execute("UPDATE FeeAnalytics SET total_fee = total_fee + @amount WHERE student_id = @student_id;", feeTransection);
                }

                scope.Complete();
            }

            return Ok(feeTransection);
            
        }
    }
}
