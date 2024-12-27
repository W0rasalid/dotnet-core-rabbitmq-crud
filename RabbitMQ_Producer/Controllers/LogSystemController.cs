using Microsoft.AspNetCore.Mvc;
using RabbitMQ_Producer.Infrastructor.RabbitMQ;
using RabbitMQ_Producer.Models.DTOs;
using RabbitMQ_Producer.Services;

namespace RabbitMQ_Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogSystemController : ControllerBase
    {
        private readonly IRabbitMQProducer _rabbitMQProducer;
        private readonly SystemLogService _systemLogService;
        public LogSystemController(ILogger<LogSystemController> logger, SystemLogService systemLogService, IRabbitMQProducer rabbitMQProducer)
        {
            _rabbitMQProducer = rabbitMQProducer;
            _systemLogService = systemLogService ?? throw new ArgumentNullException(nameof(systemLogService));
        }


        [HttpGet("GetLogSystem/{id}")]
        public async Task<IActionResult> GetLog(string id)
        {
            var log = await _systemLogService.GetLogSystem(id);
            return Ok(log);
        }

        [HttpPost("SendLog")]
        public async Task<IActionResult> SendLog([FromBody] SytemLogRequestDto body)
        {
            var req = body.Id;
            var log = await _systemLogService.SendLogSystem(body.Id);
            _rabbitMQProducer.SendWeatherMessage(log);
            return Ok(log);
        }

        [HttpPut("SendLogBatch")]
        public async Task<IActionResult> SendLogBatch([FromBody] SytemLogBatchRequestDto body)
        {
            var log = await _systemLogService.SendBatchLogSystem(body.limit);
            return Ok(log);
        }
    }
}
