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
    
    public partial class Employees
    {
        public Employees()
        {
            this.AspNetUsers = new HashSet<AspNetUsers>();
            this.Comments = new HashSet<Comments>();
            this.Doctors = new HashSet<Doctors>();
            this.EmployeeProvinces = new HashSet<EmployeeProvinces>();
            this.EmployeeRolesEmployees = new HashSet<EmployeeRolesEmployees>();
            this.Employees1 = new HashSet<Employees>();
            this.EmployeeTerritories = new HashSet<EmployeeTerritories>();
            this.SalesFlowPermissionRules = new HashSet<SalesFlowPermissionRules>();
            this.SalesFlowPermissionRules1 = new HashSet<SalesFlowPermissionRules>();
            this.SalesFlowReviews = new HashSet<SalesFlowReviews>();
            this.SalesFlows = new HashSet<SalesFlows>();
            this.SalesFlowsFiles = new HashSet<SalesFlowsFiles>();
            this.Distributors = new HashSet<Distributors>();
            this.LeftNavigations = new HashSet<LeftNavigations>();
        }
    
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string CellPhoneNumber { get; set; }
        public Nullable<System.DateTime> JoinDate { get; set; }
        public Nullable<System.DateTime> LeaveDate { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.Guid> Department_Id { get; set; }
        public Nullable<System.Guid> Parent_Id { get; set; }
        public string WorkPhoneNumber { get; set; }
    
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual Departments Departments { get; set; }
        public virtual ICollection<Doctors> Doctors { get; set; }
        public virtual ICollection<EmployeeProvinces> EmployeeProvinces { get; set; }
        public virtual ICollection<EmployeeRolesEmployees> EmployeeRolesEmployees { get; set; }
        public virtual ICollection<Employees> Employees1 { get; set; }
        public virtual Employees Employees2 { get; set; }
        public virtual ICollection<EmployeeTerritories> EmployeeTerritories { get; set; }
        public virtual ICollection<SalesFlowPermissionRules> SalesFlowPermissionRules { get; set; }
        public virtual ICollection<SalesFlowPermissionRules> SalesFlowPermissionRules1 { get; set; }
        public virtual ICollection<SalesFlowReviews> SalesFlowReviews { get; set; }
        public virtual ICollection<SalesFlows> SalesFlows { get; set; }
        public virtual ICollection<SalesFlowsFiles> SalesFlowsFiles { get; set; }
        public virtual ICollection<Distributors> Distributors { get; set; }
        public virtual ICollection<LeftNavigations> LeftNavigations { get; set; }
    }
}