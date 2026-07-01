using Doccure.Web.UI.Dtos.OrderDtos;
using Doccure.Web.UI.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;

namespace Doccure.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _orderService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _orderService.CreateAsync(dto);
            TempData["Success"] = "Sifariş uğurla əlavə edildi!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteAsync(id);
            TempData["Success"] = "Sifariş uğurla silindi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            var value = await _orderService.GetByIdAsync(id);
            if (value == null)
            {
                TempData["Error"] = "Sifariş tapılmadı!";
                return RedirectToAction("Index");
            }

            var dto = new UpdateOrderDto
            {
                OrderId = value.OrderId,
                PatientName = value.PatientName,
                BlockNo = value.BlockNo,
                FloorNo = value.FloorNo,
                RoomNo = value.RoomNo,
                Status = value.Status
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _orderService.UpdateAsync(dto);
            TempData["Success"] = "Sifariş məlumatları uğurla yeniləndi!";
            return RedirectToAction("Index");
        }
    }
}
