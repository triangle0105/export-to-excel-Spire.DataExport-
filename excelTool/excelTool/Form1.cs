using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;

namespace excelTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mapper.Reset();
            Mapper.CreateMap<Employees, EmployeeModel>();
            using (var db = new SMCSFEEntities())
            {
                var employeeList = db.Employees.Select(Mapper.Map<Employees, EmployeeModel>).ToList();
                var cellExport = new Spire.DataExport.XLS.CellExport();
                var worksheet1 = new Spire.DataExport.XLS.WorkSheet
                {
                    DataSource = Spire.DataExport.Common.ExportSource.DataTable,
                    DataTable = ListToDataTable(employeeList),
                    StartDataCol = ((System.Byte) (0))
                };
                cellExport.Sheets.Add(worksheet1);
                cellExport.ActionAfterExport = Spire.DataExport.Common.ActionType.OpenView;
                cellExport.SaveToFile("20110223.xls");
            }
        }
        ///  
        /// 将List集合类转换成DataTable 
        ///  
        /// 集合 
        ///  
        public static DataTable ListToDataTable(IList list)
        {
            var result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    //获取类型
                    Type colType = pi.PropertyType;
                    //当类型为Nullable<>时
                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }
                    result.Columns.Add(pi.Name, colType);
                }
                foreach (object t in list)
                {
                    var tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(t, null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
    }
}
