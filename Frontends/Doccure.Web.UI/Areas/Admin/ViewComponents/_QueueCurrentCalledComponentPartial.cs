using Doccure.Web.UI.Services.QueueServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.ViewComponents
{
    public class _QueueCurrentCalledComponentPartial : ViewComponent
    {
        private readonly IQueueService _queueService;

        public _QueueCurrentCalledComponentPartial(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentCalled = await _queueService.GetCurrentPatientAsync();
            var values = await _queueService.GetAllAsync();
            var previousCalled = values?.LastOrDefault(x => x.Status == "Completed");

            return View(Tuple.Create(currentCalled, previousCalled));
        }
    }
}
