using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace excelTool
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string CellPhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? LeaveDate { get; set; }
        public DateTime? UpdateTime { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? Department_Id { get; set; }
        public Guid? Parent_Id { get; set; }
        public string WorkPhoneNumber { get; set; }
    }
}
