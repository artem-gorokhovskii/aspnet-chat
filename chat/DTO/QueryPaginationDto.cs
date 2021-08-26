using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chat.DTO
{
    public class QueryPaginationDto
    {
        public QueryPaginationDto()
        {
            Take = 20;
            Skip = 0;
        }

        [Range(1, 100)]
        public int Take { get; set; }

        [Range(0, int.MaxValue)]
        public int Skip { get; set; }
    }
}
