using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Repositories
{
    public interface IOpinionExtractor
    {
        Task<IEnumerable<Opinion>> ExtractAsync(CancellationToken cancellationToken = default);
    }
}
