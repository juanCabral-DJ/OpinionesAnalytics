using OpinionesAnalytics.Domain.Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Repositories
{
    public interface IOpinionReader : IOpinionExtractor<surveys>
    {
    }
}
