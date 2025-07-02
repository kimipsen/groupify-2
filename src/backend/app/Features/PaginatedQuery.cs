using app.Domain.Types;

namespace app.Features;

public class PaginatedQuery<TIdType>
{
    public int PageSize { get; set; } = 10;
    public SearchTerm SearchTerm { get; set; } = SearchTerm.Empty;
    public virtual TIdType LastId { get; set; } = default!;
}