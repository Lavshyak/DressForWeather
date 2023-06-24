using Microsoft.AspNetCore.Mvc;
using DressForWeather.Models;
namespace DressForWeather.WebAPI.Controllers
{
    public class UserController : Controller
    {


        public PhysicalData physicalData { get; set; } = default!;
        public Info userWeather { get; set; } = default!
        public IEnumerable<Clotch> ListClotch { get; set; } = default!;
        [HttpPost(Name = "CreateListCloth")]
        public Task<ActionResult<IEnumerable<Clotch>>> CreateListCloth(User user,IEnumerable<Clotch> clotches) 
        {
            if (user == null || clotches == null)
            {
                BadRequest(string.Empty);
            }
            else 
            {
              
                /*
                 * Добовление в бд
                 */
                return Ok(user);
            }
            
        }
}
}
