using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RabbitMQ_Consumer.Repositories;
using RabbitMQ_Producer;
using RabbitMQ_Producer.Infrastructor.RabbitMQ;
using RabbitMQ_Producer.Models.DTOs;
using RabbitMQ_Producer.Repositories;
using SharedLibrary;

namespace RabbitMQ_Consumer.Services
{
    public class LogSystemService
    {
        private  ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private IRabbitMQConsumer _rabbitMQConsumer;
        private readonly LogSystemRepository _logSystemRepository;


        public LogSystemService(ApplicationDbContext context, IMapper mapper, LogSystemRepository logSystemRepository)
        {
            _mapper = mapper;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logSystemRepository = logSystemRepository;
        }

        public void SetRabbitMQConsumer(IRabbitMQConsumer rabbitMQConsumer)
        {
            _rabbitMQConsumer = rabbitMQConsumer ?? throw new ArgumentNullException(nameof(rabbitMQConsumer));
        }

        public async Task<SytemLogRespDto> GetLogSystem(string Id)
        {
            var logEntity = await _logSystemRepository.GetLogSystemById(Id);
            var result = _mapper.Map<SytemLogRespDto>(logEntity);
            return result;

        }

        public async void UpdateLogSystem(string Id)
        {
            var result = await _logSystemRepository.UpdateLogSystem(Id);
            Console.WriteLine($"Record Id : {result}");
        }

    }

}