using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ_Producer.Repositories;
using RabbitMQ_Producer.Models.DTOs;
using AutoMapper;

namespace RabbitMQ_Producer.Services
{
    public class ScheduledTaskService
    {
        private readonly Timer _timer;
        private readonly SystemLogRepository _systemLogRepository;

        public ScheduledTaskService()
        {
            // ตั้งเวลาทำงานทุก 1 นาที (60000 มิลลิวินาที)
            _timer = new Timer(ExecuteTask, null, Timeout.Infinite, Timeout.Infinite);
            //_systemLogRepository = systemLogRepository;
        }

        public void Start()
        {
            //var result = _systemLogRepository.GetLogSystemById("B3DBC3A8-62D9-4D87-810C-27354462E8FA");
            //Console.WriteLine($"result : {result}");

            // เริ่ม Service โดยตั้งเวลาเริ่มใน 1 วินาที และทำซ้ำทุก 1 นาที
            _timer.Change(1000, 6000);
            Console.WriteLine("Scheduled Task Service Started.");
        }

        public void Stop()
        {
            // หยุด Timer
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            Console.WriteLine("Scheduled Task Service Stopped.");
        }

        private void ExecuteTask(object state)
        {
            // งานที่จะทำงานตามเวลาที่กำหนด
            Console.WriteLine($"Task executed at: {DateTime.Now}");
        }
    }

    //public class ScheduledService
    //{
    //    private Timer _timer;
    //    private readonly SystemLogRepository _systemLogRepository;
    //    private readonly IMapper _mapper;
    //    private bool _isExecuting;

    //    public ScheduledService(SystemLogRepository systemLogRepo, IMapper mapper)
    //    {
    //        _systemLogRepository = systemLogRepo;
    //        _mapper = mapper;
    //    }

    //    public void Start()
    //    {
    //        _timer = new Timer(async _ => await ExecuteTaskAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
    //    }

    //    public void Stop()
    //    {
    //        _timer?.Change(Timeout.Infinite, 0);
    //    }

    //    private async Task ExecuteTaskAsync()
    //    {
    //        if (_isExecuting) return; // ป้องกันการทำงานซ้อนกัน
    //        _isExecuting = true;

    //        try
    //        {
    //            // ตัวอย่างการดึงข้อมูลจาก Repository
    //            var logEntity = await _systemLogRepository.GetLogSystemById("B3DBC3A8-62D9-4D87-810C-27354462E8FA");
    //            var result = _mapper.Map<SytemLogRespDto>(logEntity);

    //            Console.WriteLine($"Task executed at: {DateTime.Now}");
    //            Console.WriteLine($"Log Data: {result}");
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Error: {ex.Message}");
    //        }
    //        finally
    //        {
    //            _isExecuting = false;
    //        }
    //    }
    //}
}
