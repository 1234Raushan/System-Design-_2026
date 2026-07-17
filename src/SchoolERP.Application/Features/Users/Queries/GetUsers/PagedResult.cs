namespace SchoolERP.Application.Features.Users.Queries.GetUsers;

public sealed class PagedResult<T>
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public int TotalCount { get; init; }

    public int TotalPages { get; init; }

    public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
}
