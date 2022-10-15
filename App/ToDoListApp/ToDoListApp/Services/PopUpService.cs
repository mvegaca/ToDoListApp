using ToDoListApp.Contracts.Services;

namespace ToDoListApp.Services
{
    public class PopUpService : IPopUpService
    {
        private Page _page;
        
        public void Initialize(Page page)
        {
            _page = page;
        }

        public void DisplayAlert(string title, string message, string cancel)
        {
            _page.DisplayAlert(title, message, cancel);
        }
    }
}
