using Entity;

namespace Services.ItemSelection
{
    public interface IItemSelectionService
    {
        ItemEntity SelectedItem { get; }
    }
}