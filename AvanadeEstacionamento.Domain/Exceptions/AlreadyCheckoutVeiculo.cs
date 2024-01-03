using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvanadeEstacionamento.Domain.Exceptions
{
    public class AlreadyCheckoutVeiculo : Exception
    {
        public AlreadyCheckoutVeiculo(string error) : base(error) { }
    }
}
