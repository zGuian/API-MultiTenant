
namespace GearCore.Monolith.Core.StockCore.Interfaces.Entities
{
    public interface IStock
    {
        Guid Id { get; set; }
        string Local { get; set; }
        string? Description { get; set; }
    }
}
