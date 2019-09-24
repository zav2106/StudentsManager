using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsManager.Core.DataTransferObjects
{
    public class PagedResultDto<TItem>
    {
        public IEnumerable<TItem> Items { get; set; }

        public int TotalPages { get; set; }

        public int TotalRows { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
