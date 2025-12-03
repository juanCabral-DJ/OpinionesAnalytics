using OpinionesAnalytics.Domain.Csv;
using OpinionesAnalyticsAPI.DATA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Dtos
{
    public class DimDtos
    {
        public string? SurveysCsvPath { get; set; } 
        public string? ClientsCsvPath { get; set; }
        public string? ProductsCsvPath { get; set; }
        public IEnumerable<Social_Comments>? Social_Comments { get; set; }  
        public IEnumerable<WebReviews>? WebReviews { get; set; }
        public List<Productos> products { get; set; }
        public List<Clientes> clients { get; set; }
        public List<surveys> surveys { get; set; }
    }
}
