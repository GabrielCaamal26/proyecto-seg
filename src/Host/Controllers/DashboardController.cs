using ApplicationCore.Commands;
using ApplicationCore.Commands.LogsR;
using ApplicationCore.DTOs.Logs;
using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Host.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        private readonly IMediator _mediator;
        public DashboardController(IDashboardService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }

        [HttpGet()]
      
        //public async Task<IActionResult> GastoPendienteArea()
        //{
        //    var origin = Request.Headers["origin"];
        //    return Ok("test");
        //}

        [Route("getData")]
        [HttpGet()]
        public async Task<IActionResult> GetUsuarios()
        {
            var result = await _service.GetData();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Response<int>>> Create(CreateUserCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok("result");
        }

        [HttpGet("GetIp")]
        public async Task<IActionResult> GetIp()
        {
            var result = await _service.GetIp();
            return Ok(result);
        }

        [HttpPost("createlog")]
        public async Task<ActionResult<Response<int>>> createlog([FromBody] LogsDto request)
        {
            var result = await _service.CreateLog(request);
            return Ok(result);
        }





















    }
}
