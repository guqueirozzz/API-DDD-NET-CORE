using Domain.Interfaces;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    internal class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _Imessage;

        public ServiceMessage(IMessage message)
        {
            _Imessage = message;   
        }
    }
}
