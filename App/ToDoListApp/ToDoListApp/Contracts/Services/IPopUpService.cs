using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Contracts.Services
{
    public interface IPopUpService
    {
        void Initialize(Page page);
        void DisplayAlert(string title, string message, string cancel);
    }
}
