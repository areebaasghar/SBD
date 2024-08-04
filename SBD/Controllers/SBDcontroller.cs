using SBDDAL;
using SBDModelClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Data.SqlClient;



namespace SBDApi.Controllers
{
   
    [ApiController]
    [Route("SBDApi/[controller]")]
public class SBDcontroller  : ControllerBase
    {
        [HttpGet(Name = "getdata")]
        public async Task<IActionResult> GetData()
        {
            try
            {
                List<EntUserInfo> list = await CRUD.ReadDataAsync<EntUserInfo>("sp_GetUserInfo");
                var serializedData = JsonSerializer.Serialize(list);
                return Ok(serializedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error during getting data");
            }
        }

        [HttpPost(Name = "savedata")]
        public async Task<IActionResult> SaveData([FromBody] EntUserInfo ei)
        {
            if (ei == null)
            {
                return BadRequest("Invalid ID!");
            }

            await CRUD.CUDAsync<EntUserInfo>(ei, "sp_SaveInfo");

            return Ok("Data Saved successfully.");
        }

        [HttpDelete(Name = "deletedata")]
        public async Task<IActionResult> DeleteData([FromBody] EntUserInfo ei)
        {
            if (ei == null)
            {
                return BadRequest("Invalid ID!");
            }

            await CRUD.CUDAsync<EntUserInfo>(ei, "sp_deleteInfo");

            return Ok("Data deleted successfully.");
        }


        [HttpPut(Name = "updatedata")]
        public async Task<IActionResult> UpdateData([FromBody] EntUserInfo ei)
        {
            if (ei == null)
            {
                return BadRequest("Invalid ID!");
            }
            await CRUD.CUDAsync<EntUserInfo>(ei, "sp_updateInfo");

            return Ok("User updated successfully.");
        }

    }
}