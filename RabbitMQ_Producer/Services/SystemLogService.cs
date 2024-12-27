using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RabbitMQ_Producer.Infrastructor.RabbitMQ;
using RabbitMQ_Producer.Models.DTOs;
using RabbitMQ_Producer.Repositories;
using SharedLibrary;

namespace RabbitMQ_Producer.Services
{
    public class SystemLogService
    {
        private ApplicationDbContext _context;
        private readonly SystemLogRepository _systemLogRepository;
        private readonly IMapper _mapper;
        private readonly IRabbitMQProducer _rabbitMQProducer;
        public SystemLogService(SystemLogRepository systemLogRepo, IMapper mapper, ApplicationDbContext context, IRabbitMQProducer rabbitMQProducer)
        {
            _systemLogRepository = systemLogRepo;
            _mapper = mapper;
            _context = context;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<SytemLogRespDto> GetLogSystem(string Id)
        {
            var logEntity = await _systemLogRepository.GetLogSystemById(Id);
            return _mapper.Map<SytemLogRespDto>(logEntity);
        }

        public async Task<SytemLogRespDto> SendLogSystem(string Id)
        {
            var log = await _systemLogRepository.GetLogSystemById(Id);
            var result = _mapper.Map<SytemLogRespDto>(log);
            _rabbitMQProducer.SendWeatherMessage(result);
            return result;

        }

        public async Task<int> SendBatchLogSystem(int limit)
        {
            var log = await _systemLogRepository.GetLogWithPagination(limit);
            var result = _mapper.Map<List<SytemLogRespDto>>(log);

            if(result.Count() != 0)
            {
                for(var i=0; i< result.Count(); i++)
                {
                    _rabbitMQProducer.SendLogMessage(result[i]);
                }
            }

            //var logEntity = await _systemLogRepository.GetLogSystemById(Id);
            //var result = _mapper.Map<SytemLogRespDto>(logEntity);
            return result.Count();

        }
    }
}
