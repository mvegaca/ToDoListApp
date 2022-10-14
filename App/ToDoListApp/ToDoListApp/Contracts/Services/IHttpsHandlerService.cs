using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Contracts.Services
{
    public interface IHttpsHandlerService
    {
        HttpMessageHandler GetPlatformMessageHandler();
    }
}
