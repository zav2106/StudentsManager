using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsManager.Core.DataTransferObjects
{
    public class PaginationInfoDto
    {
        public int PageSize { get; set; } = 25;

        public int PageNumber { get; set; } = 1;

        public long TotalItems { get; set; }
    }
}

