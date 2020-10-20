using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IntegrationService.Models.UpackageViewModel;
using IntegrationService.Service;

namespace IntegrationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpackageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public UpackageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        // POST: api/Upackage
        [HttpPost]
        public async Task<ActionResult<ResponseUpackageViewModel>> PostUpackage(CreateUpackageViewModel viewModel)
        {
            var responseUpackageViewModel = _messageService.PostUpackage(viewModel); 
            return CreatedAtAction("GetUpackage", new { id = responseUpackageViewModel.RequestId }, responseUpackageViewModel);
        }
        // GET: api/Upackage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UpackageCurrentStatusViewModel>> GetUpackage(long id)
        {    
            var currentStatusView = _messageService.GetUpackageLastStatus(id);
            if (currentStatusView == null)
            {
                return NotFound();
            }
            return currentStatusView;
        }
    }
}
