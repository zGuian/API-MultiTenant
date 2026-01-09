namespace GearCore.Monolith.Core.SalesCore.DTOs.Requests
{
    public class RealizeSaleRequest
    {
        public string UserId { get; set; } = string.Empty;
        public IEnumerable<RealizeSaleDto> Items { get; set; } = [];
    }
}
