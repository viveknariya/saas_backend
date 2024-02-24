using Dapper;
using db;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace sfmsBackEnd2.FeeModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeStructureController : ControllerBase
    {
        private readonly IDatabaseConnection databaseConnection;

        public FeeStructureController(IDatabaseConnection databaseConnection){
            this.databaseConnection = databaseConnection;
        }

        [HttpGet]
        public IActionResult Get()
        {
             using var conn = databaseConnection.GetConnection();


            conn.Open();

            var feeStructures = conn.Query<FeeStructure>(
                """
                SELECT
                    id,
                    structure_name,
                    amount,
                    period,
                    offset,
                    comment
                FROM
                    FeeStructure           
                """
            );

            return Ok(feeStructures);

        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {

             using var conn = databaseConnection.GetConnection();
            conn.Open();

            var feeStructure = conn.Query<FeeStructure>(
                """
                SELECT
                    id,
                    structure_name,
                    amount,
                    period,
                    offset,
                    comment
                FROM
                    FeeStructure   
                WHERE 
                    id = @id
                """, new { id = id });

            return Ok(feeStructure);

        }

        [HttpPost]
        public IActionResult Create(FeeStructure feeStructure)
        {

             using var conn = databaseConnection.GetConnection();
            conn.Open();

            conn.Query<FeeStructure>(
                """
                INSERT INTO FeeStructure
                	(id,
                    structure_name,
                    amount,
                    period,
                    offset,
                    comment)
                    VALUES
                	    (@id,
                        @structure_name,
                        @amount,
                        @period,
                        @offset,
                        @comment)         
                """, feeStructure);

            return Ok(feeStructure);

        }

        [HttpPut]
        public IActionResult Update(FeeStructure feeStructure)
        {

             using var conn = databaseConnection.GetConnection();
            conn.Open();

            conn.Query<FeeStructure>(
                """
                UPDATE FeeStructure SET
                	structure_name = @structure_name,
                    amount = @amount,
                    period = @period,
                    offset = @offset,
                    comment = @comment
                    WHERE
                	    id = @id
                """, feeStructure);

            return Ok(feeStructure);

        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {

            using var conn = databaseConnection.GetConnection();

            conn.Open();

            conn.Query<FeeStructure>("DELETE FROM FeeStructure WHERE id = @id", new { id = id });

            return Ok();

        }
        
    }
}
