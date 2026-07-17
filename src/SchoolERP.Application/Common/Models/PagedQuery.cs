using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Common.Models
{
    public abstract record PagedQuery
    {
        public int PageNumber { get; init; } = 1;

        public int PageSize { get; init; } = 10;

        public string? SearchTerm { get; init; }

        public string? SortBy { get; init; }

        public string? SortDirection { get; init; } = "asc";
    }
}
