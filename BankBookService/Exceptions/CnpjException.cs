using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAgendaService.Exceptions
{
    internal class CnpjException: OperationException
    {
        public CnpjException(string message) : base(message)
        {

        }
    }
}
