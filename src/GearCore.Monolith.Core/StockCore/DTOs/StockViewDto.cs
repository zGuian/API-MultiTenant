namespace GearCore.Monolith.Core.StockCore.DTOs
{
    public class StockViewDto
    {
        public string Local { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public StockViewDto(string local, string? description, bool isActive)
        {
            Local = local;
            Description = description;
            IsActive = isActive;
        }
    }
}
