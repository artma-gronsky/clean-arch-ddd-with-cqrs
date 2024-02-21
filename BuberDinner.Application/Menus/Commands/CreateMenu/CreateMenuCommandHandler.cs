using System.ComponentModel.Design;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Hosts.ValueObjects;
using BuberDinner.Domain.Menus;
using BuberDinner.Domain.Menus.Entities;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        // Create menu
        await Task.CompletedTask;

        var menu = Menu.Create(
            hostId: HostId.Create(Guid.Parse(request.HostId)),
            name: request.Name,
            description: request.Description,
            sections: request.Sections.ConvertAll(sections => MenuSection.Create(
                name: sections.Name,
                description: sections.Description,
                items: sections.Items.ConvertAll(items => MenuItem.Create(
                    name: items.Name,
                    description: items.Description)))));

        //Persist Menu
        _menuRepository.Add(menu);

        // Return menu
        return menu;
    }
}