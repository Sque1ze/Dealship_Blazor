using Dealship.Api.Services.Interfaces;
using Dealship.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dealship.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace Dealship.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseCrudController<TDto, TForm> : ControllerBase
    {
        protected readonly ICrudService<TDto, TForm> _svc;

        protected BaseCrudController(ICrudService<TDto, TForm> svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<TDto>>> Get([FromQuery] QueryParams qp)
        {
            var result = await _svc.GetAsync(qp);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TDto>> Get(int id)
        {
            var dto = await _svc.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<TDto>> Post([FromBody] TForm f)
        {
            var created = await _svc.CreateAsync(f);
            return Ok(created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] TForm f)
        {
            var updated = await _svc.UpdateAsync(id, f);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _svc.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
