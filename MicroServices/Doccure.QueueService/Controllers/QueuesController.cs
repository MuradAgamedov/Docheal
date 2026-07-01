using Doccure.QueueService.Context;
using Doccure.QueueService.Entities;
using Doccure.QueueService.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Doccure.QueueService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuesController : ControllerBase
    {
        private readonly QueueContext _context;
        private readonly IHubContext<QueueHub> _hubContext;

        public QueuesController(QueueContext context, IHubContext<QueueHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetQueueList()
        {
            var values = _context.PatientQueues.OrderBy(x => x.QueueNumber).ToList();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateQueue(PatientQueue queue)
        {
            queue.Status = "Waiting";
            await _context.AddAsync(queue);
            await _context.SaveChangesAsync();
            return Ok("Pasiyent sıraya əlavə edildi");
        }


        [HttpPost("call/{id}")]


        public async Task<IActionResult> CallPatient(int id)
        {
            var patient = await _context.PatientQueues.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            patient.Status = "Called";
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync(

                "PatientCalled",
                    patient.QueueNumber,
                    patient.PatientName,
                    patient.BrancName,
                    patient.AppointmentTime
                );


            return Ok("Patient called");
        }


        [HttpPost("current")]
        public async Task<IActionResult> GetCurrentPatient(int id)
        {
            var currentPatient = _context.PatientQueues.Where(x => x.Status == "Called").OrderBy(x => x.QueueNumber).FirstOrDefault();

            return Ok(currentPatient);

        }
    }
}
