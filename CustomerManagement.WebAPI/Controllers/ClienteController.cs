using System;
using System.Net;
using System.Threading.Tasks;
using CustomerManagement.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] InsertClienteCommand command)
        {
            await _mediator.Send(command);
            return Ok("Criação realizada com sucesso.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClienteCommand command)
        {
            if (await _mediator.Send(command))
                return Ok("Atualização realizada com sucesso.");
            else
                return this.StatusCode((int)HttpStatusCode.Forbidden, new ApplicationException("O cliente referente a chave informada não existe na base de dados."));
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetClienteCommand());
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteClienteCommand command)
        {
            if (await _mediator.Send(command))
                return Ok("Remoção realizada com sucesso.");
            else
                return this.StatusCode((int)HttpStatusCode.Forbidden, new ApplicationException("O cliente referente a chave informada não existe na base de dados."));
        }
    }
}