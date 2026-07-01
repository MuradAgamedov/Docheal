using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QueueController : Controller
    {
        private readonly Services.QueueServices.IQueueService _queueService;

        public QueueController(Services.QueueServices.IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _queueService.GetAllAsync();
            return View(values);
        }
    }
}
