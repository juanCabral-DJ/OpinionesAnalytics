using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpinionesAnalytics.Application.Exceptions
{
    public class OpinionesHandlerException : Exception
        {
        public OpinionesHandlerException(string message) : base(message)
        {
            
        }
 
        public OpinionesHandlerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    }
 
