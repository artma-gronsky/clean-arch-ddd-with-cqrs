using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.MenuAggregate.ValueObjects;

namespace BuberDinner.Domain.MenuAggregate.Entities;

public class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _menuItems = new();

    public string Name { get; }

    public string Description { get; }

    public IReadOnlyList<MenuItem> Items => _menuItems.AsReadOnly();

    private MenuSection(MenuSectionId id, string description, string name, List<MenuItem>? items) : base(id)
    {
        Id = id;
        Description = description;
        Name = name;
        _menuItems = items;
    }

    public static MenuSection Create(
        string description,
        string name,
        List<MenuItem>? items)
    {
        return new(
            MenuSectionId.CreateUnique(),
            description,
            name,
            items);
    }
}