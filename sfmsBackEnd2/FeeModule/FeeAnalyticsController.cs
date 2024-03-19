using Dapper;
using db;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace sfmsBackEnd2.FeeModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeAnalyticsController : ControllerBase
    {
        private readonly IDatabaseConnection databaseConnection;

        public FeeAnalyticsController(IDatabaseConnection databaseConnection){
            this.databaseConnection = databaseConnection;
        }

        [HttpGet]
        public IActionResult Get()
        {
             using var conn = databaseConnection.GetConnection();


            conn.Open();

            string query = """
                SELECT s.first_name,s.last_name,s.standard,fee_analytics.* FROM Student AS s LEFT JOIN 
                (
                	SELECT fa.id,fa.student_id,fa.total_fee,fa.paid_fee,last_transection2.date_of_transection FROM FeeAnalytics AS fa LEFT JOIN
                	(
                		SELECT ft.student_id,ft.date_of_transection FROM FeeTransection AS ft LEFT JOIN 
                		( 
                			SELECT MAX(id) last_transaction_id
                			FROM FeeTransection
                			GROUP BY student_id
                		) last_trtansection
                		ON ft.id = last_trtansection.last_transaction_id
                	) last_transection2
                	ON fa.student_id = last_transection2.student_id
                ) fee_analytics
                ON s.id = fee_analytics.student_id;

                SELECT SUM(total_fee) AS total_fee ,SUM(paid_fee) as paid_fee  FROM FeeAnalytics;
                """;

            var multiResult = conn.QueryMultiple(query);
            var result1 = multiResult.Read();
            var result2 = multiResult.Read();

            return Ok(new { result1, result2 });

        }
    }
}
