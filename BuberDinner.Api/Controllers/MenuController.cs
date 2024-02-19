using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menu;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("hosts/{hostId}/menus")]
public class MenuController(IMapper _mapper, ISender _mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateMenu(CreateMenuRequest request, string hostId)
    {
        var menuResult = await _mediator.Send(_mapper.Map<CreateMenuCommand>((request, hostId)));

        return menuResult.Match(
            menu => Ok(_mapper.Map<MenuResponse>(menu)),
            Problem
        );
    }
}