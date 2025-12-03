using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Domain.Repository
{
    public interface IFileReaderRepository<TClass> where TClass : class
    {
        Task<IEnumerable<TClass>> ReadFileAsync(string filePath);
    }
}
