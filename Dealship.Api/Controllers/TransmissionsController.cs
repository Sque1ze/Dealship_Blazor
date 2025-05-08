using Dealship.Api.Services;
using Dealship.Shared;
using Microsoft.AspNetCore.Mvc;
using Dealship.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dealship.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransmissionsController : BaseCrudController<TransmissionDto, TransmissionDto>
    {
        public TransmissionsController(TransmissionService svc)
            : base(svc)
        {
        }
    }
}
