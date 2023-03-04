using EasyAgenda.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EasyAgenda.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleDAL _peopleRepository;
        public PeopleController(IPeopleDAL peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        [Route("v1/people/{id}"), HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var people = await _peopleRepository.Get(id);
                return Ok(people);
            }
            catch (KeyNotFoundException error)
            {
                return NotFound(error.Message);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }
        [Route("v1/people"), HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var people = await _peopleRepository.GetAll();
                return Ok(people);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }
    }
}
