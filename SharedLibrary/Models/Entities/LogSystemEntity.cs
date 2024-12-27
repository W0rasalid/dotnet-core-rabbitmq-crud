using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models.Entities
{
    [Table("LogSystem")]
    public class LogSystemEntity
    {

        public DateTime? LogDate { get; set; }
        public int? MainMenu { get; set; }
        public int? SubMenu { get; set; }
        public int? EmpCode { get; set; }
        public string? CompanyCode { get; set; }
        public int? DepartmentId { get; set; }
        public string? Message { get; set; }
        public string? Request { get; set; }
        public string? Operation { get; set; }
        public string? Reference { get; set; }
        public bool? IsSuccess { get; set; }
        [Key] // ระบุว่าเป็น Primary Key
        public string Id { get; set; }
    }
}
