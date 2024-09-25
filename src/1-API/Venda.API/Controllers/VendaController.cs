using Microsoft.AspNetCore.Mvc;
using Venda.API.DTO;
using Venda.Domain.Entidades;
using Venda.Domain.Interfaces.V1.Servicos;

namespace Venda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CriarVenda([FromServices]IVendaServico<Domain.Entidades.Venda> _vendaService,  [FromBody] VendaDTO vendaDto)
        {
            var venda = MapearDtoParaEntidade(vendaDto);
            await _vendaService.CriarVendaAsync(venda);
            return CreatedAtAction(nameof(ObterVenda), new { id = venda.Id }, venda);
        }

        [HttpGet()]
        public async Task<IActionResult> ObterVendas([FromServices] IVendaServico<Domain.Entidades.Venda> _vendaService)
        {
            var venda = await _vendaService.BuscarTodasAsVendasAsync();
            if (venda == null) return NotFound();
            return Ok(venda);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterVenda([FromServices] IVendaServico<Domain.Entidades.Venda> _vendaService, Guid id)
        {
            var venda = await _vendaService.ObterVendaPorIdAsync(id);
            if (venda == null) return NotFound();
            return Ok(venda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarVenda([FromServices] IVendaServico<Domain.Entidades.Venda> _vendaService, Guid id, [FromBody] VendaDTO vendaDto)
        {
            var venda = MapearDtoParaEntidade(vendaDto);
            await _vendaService.AlterarVendaAsync(venda);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelarVenda([FromServices] IVendaServico<Domain.Entidades.Venda> _vendaService, Guid id)
        {
            await _vendaService.CancelarVendaAsync(id);
            return NoContent();
        }

        private Domain.Entidades.Venda MapearDtoParaEntidade(VendaDTO vendaDto)
        {
            var cliente = new Cliente(vendaDto.Cliente.Id, vendaDto.Cliente.Nome);
            var venda = new Domain.Entidades.Venda(vendaDto.Data, cliente, vendaDto.Filial);
            foreach (var itemDto in vendaDto.Itens)
            {
                var item = new ItemVenda(itemDto.Produto, itemDto.Quantidade, itemDto.ValorUnitario, itemDto.Desconto);
                venda.AdicionarItem(item);
            }
            return venda;
        }
    }
}
