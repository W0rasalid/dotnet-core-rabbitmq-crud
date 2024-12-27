using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models.DTOs
{
    public class SytemLogRequestDto
    {
        public string Id { get; set; }
    }

    public class SytemLogRespDto
    {
        public DateTime? LogDate { get; set; }
        //public string? Request { get; set; }
        public string? Id { get; set; }
    }
}
