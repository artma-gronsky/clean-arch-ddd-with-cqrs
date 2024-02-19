namespace BuberDinner.Contracts.Menu
{
    public record CreateMenuRequest(
        string Name,
        string Description,
        List<MenuSections> Sections);

    public record MenuSections(
        string Name,
        string Description,
        List<MenuItem> Items);

    public record MenuItem(
        string Name,
        string Description);
}