using System.ComponentModel;

namespace RabbitMQ_Producer.Models.DTOs
{
    public class SytemLogRequestDto
    {
        public string Id { get; set; }
    }

    public class SytemLogBatchRequestDto
    {
        [DefaultValue(10)]
        public int limit { get; set; }
    }

    public class SytemLogRespDto
    {
        public DateTime? LogDate { get; set; }
        //public string? Request { get; set; }
        public string? Id { get; set; } 
    }
}
