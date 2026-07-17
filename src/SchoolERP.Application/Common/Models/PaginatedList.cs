using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolERP.Application.Common.Models
{
    public class PaginatedList<T>
    {
        public IReadOnlyList<T> Items { get; }

        public int PageNumber { get; }

        public int PageSize { get; }

        public int TotalRecords { get; }

        public int TotalPages =>
            (int)Math.Ceiling(
                TotalRecords / (double)PageSize
            );


        public PaginatedList(
            IReadOnlyList<T> items,
            int totalRecords,
            int pageNumber,
            int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

}
