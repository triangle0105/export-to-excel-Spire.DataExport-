//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace excelTool
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeRolesEmployees
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> Employee_Id { get; set; }
        public Nullable<System.Guid> EmployeeRole_Id { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual EmployeeRoles EmployeeRoles { get; set; }
        public virtual Employees Employees { get; set; }
    }
}
