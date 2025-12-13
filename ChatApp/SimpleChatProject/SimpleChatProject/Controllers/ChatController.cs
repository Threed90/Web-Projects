using Microsoft.AspNetCore.Mvc;
using SimpleChatProject.Models;

namespace SimpleChatProject.Controllers
{
    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> messages = new();
        public IActionResult Index()
        {
            if (messages.Count < 1)
            {
                return View(new ChatViewModel());
            }

            var allMessages = new ChatViewModel
            {
                Messages = messages
                    .Select(m => new MessageViewModel
                    {
                        Sender = m.Key,
                        Message = m.Value
                    })
                    .ToList()
            };

            return View(allMessages);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chatViewModel)
        {
            if (chatViewModel.CurrentMessage != null)
            {
                messages.Add(new KeyValuePair<string, string>(
                    chatViewModel.CurrentMessage.Sender,
                    chatViewModel.CurrentMessage.Message));
            }
            return RedirectToAction("Index");
        }
    }
}
