using OpinionesAnalytics.Application.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Interfaces
{
    public interface IOpinionesHandlerServices
    {
        Task<ServicesResult> ProcessOpinionesDataAsync();
    }
}
