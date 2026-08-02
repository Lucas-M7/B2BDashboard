namespace B2BDashboard.Application.Common;

public record PagedResult<T>(IReadOnlyList<T> Items, 
    int TotalCount, int Page, int PageSize)
{
    // Math.Ceiling - arrendonda pra cima
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}