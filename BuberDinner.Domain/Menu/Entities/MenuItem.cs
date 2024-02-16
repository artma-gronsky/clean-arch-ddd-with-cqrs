using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities;

public class MenuItem : Entity<MenuItemId>
{
    public string Name { get; }

    public string Description { get; }

    private MenuItem(MenuItemId id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    private static MenuItem Create(
        string name,
        string description)
    {
        return new MenuItem(MenuItemId.CreateUnique(), name, description);
    }
}