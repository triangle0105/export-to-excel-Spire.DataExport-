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
    
    public partial class ProductUsedNames
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.Guid> Product_Id { get; set; }
    
        public virtual Products Products { get; set; }
    }
}
