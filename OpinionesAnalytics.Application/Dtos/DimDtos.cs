using OpinionesAnalytics.Domain.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Dtos
{
    public class DimDtos
    {
        public string? fileData { get; set; }
        public List<Social_CommentsDto> Social_CommentsDtos { get; set; }
        public List<surveys> surveys { get; set; }
    }
}
