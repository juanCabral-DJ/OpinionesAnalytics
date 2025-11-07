using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Repositories
{
    public interface IOpinionExtractor<Tentity> where Tentity : class
    {
        Task<IEnumerable<Tentity>> ExtractAsync(CancellationToken cancellationToken = default);
    }
}
