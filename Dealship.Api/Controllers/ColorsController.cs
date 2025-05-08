using Dealship.Api.Services;
using Dealship.Shared;
using Dealship.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dealship.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorsController : BaseCrudController<ColorDto, ColorDto>
    {
        public ColorsController(ColorService svc)
            : base(svc)
        {
        }
    }
}
