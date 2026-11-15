using Microsoft.AspNetCore.Mvc;
using MYAcademy_Portfolio.Context;
using MYAcademy_Portfolio.Entities;

namespace MYAcademy_Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult MessageList()
        {
            var values = _context.Messages
                .OrderByDescending(x => x.SendDate)
                .ToList();
            return View(values);
        }

        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }

        // Modal açıldığında çağrılır, mesajı okundu olarak işaretler
        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            var value = _context.Messages.Find(id);
            if (value == null)
            {
                return NotFound();
            }

            value.IsRead = true;
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            if (value != null)
            {
                _context.Messages.Remove(value);
                _context.SaveChanges();
                TempData["Success"] = "Mesaj silindi.";
            }
            return RedirectToAction("MessageList");
        }
    }
}
