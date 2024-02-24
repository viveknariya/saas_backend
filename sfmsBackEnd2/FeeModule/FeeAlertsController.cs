using Dapper;
using db;
using Microsoft.AspNetCore.Mvc;

namespace sfmsBackEnd2.FeeModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeAlertsController : ControllerBase
    {
        private readonly IDatabaseConnection databaseConnection;

        public FeeAlertsController(IDatabaseConnection databaseConnection){
            this.databaseConnection = databaseConnection;
        }

        [HttpGet]
        public IActionResult Get()
        {


            using var conn = databaseConnection.GetConnection();
            conn.Open();

            var feeAlerts = conn.Query<FeeAlerts>(
                """
                SELECT
                    id,
                    student_id,
                    fee_alert_date,
                    fee_alert_status,
                    identity_ref
                FROM
                    FeeAlerts           
                """
            );

            return Ok(feeAlerts);

        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            using var conn = databaseConnection.GetConnection();

            conn.Open();

            var feeAlerts = conn.Query<FeeAlerts>(
                """
                SELECT
                    id,
                    student_id,
                    fee_alert_date,
                    fee_alert_status,
                    identity_ref
                FROM
                    FeeAlerts   
                WHERE 
                    id = @id
                """, new { id = id });

            return Ok(feeAlerts);

        }

        [HttpPost]
        public IActionResult Create(FeeAlerts feeAlerts)
        {


            using var conn = databaseConnection.GetConnection();
            conn.Open();

            conn.Query<FeeAlerts>(
                """
                INSERT INTO FeeAlerts
                	(id,
                    student_id,
                    fee_alert_date,
                    fee_alert_status)
                    VALUES
                	    (@id,
                        @student_id,
                        @fee_alert_date,
                        @fee_alert_status)         
                """, feeAlerts);

            return Ok(feeAlerts);

        }

        [HttpPut]
        public IActionResult Update(FeeAlerts feeAlerts)
        {

            using var conn = databaseConnection.GetConnection();
            conn.Open();

            conn.Query<FeeAlerts>(
                """
                UPDATE FeeAlerts SET
                	student_id = @student_id,
                    fee_alert_date = @fee_alert_date,
                    fee_alert_status = @fee_alert_status
                    WHERE
                	    id = @id
                """, feeAlerts);

            return Ok(feeAlerts);

        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {


            using var conn = databaseConnection.GetConnection();
            conn.Open();

            conn.Query<FeeAlerts>("DELETE FROM FeeAlerts WHERE id = @id", new { id = id });

            return Ok();

        }
    }
}
