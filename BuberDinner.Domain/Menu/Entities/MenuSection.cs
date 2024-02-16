using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities;

public class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _menuItems = new();

    public string Name { get; }

    public string Description { get; }

    public IReadOnlyList<MenuItem> Items => _menuItems.AsReadOnly();

    private MenuSection(MenuSectionId id, string description, string name) : base(id)
    {
        Id = id;
        Description = description;
        Name = name;
    }

    public static MenuSection Create(
        string description,
        string name)
    {
        return new(
            MenuSectionId.CreateUnique(),
            description,
            name);
    }
}