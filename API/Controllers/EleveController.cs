using Microsoft.AspNetCore.Mvc;
using Models.ModelAPI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.ServicesAPI;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EleveController : ControllerBase
    {
        private readonly IEleveServiceAPI _eleveServices;

        public EleveController(IEleveServiceAPI eleveServices)
        {
            _eleveServices = eleveServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EleveAPI>>> GetEleves()
        {
            var eleves = await _eleveServices.GetElevesAsync();
            return Ok(eleves);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EleveAPI>> GetEleve(int id)
        {
            var eleve = await _eleveServices.GetEleveByIdAsync(id);
            if (eleve == null)
            {
                return NotFound();
            }
            return Ok(eleve);
        }

        [HttpPost]
        public async Task<ActionResult<EleveAPI>> PostEleve(EleveAPI eleve)
        {
            var newEleve = await _eleveServices.AddEleveAsync(eleve);
            return CreatedAtAction(nameof(GetEleve), new { id = newEleve.Id }, newEleve);
        }

        [HttpPut("{nom}")]
        public async Task<IActionResult> PutEleveByName(string nom, EleveAPI updatedEleve)
        {
            var result = await _eleveServices.UpdateEleveByNameAsync(nom, updatedEleve);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEleveById(int id)
        {
            var result = await _eleveServices.DeleteEleveByIdAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

