using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
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

        private void button2_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog { Filter = "test|*.xls;*.xlsx", InitialDirectory = "D:\\" };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = fileDialog.FileName;//得到文件所在位置。
                using (var excelhelper = new NPOIExcelHelper(fileName))
                {
                    DataTable dt = excelhelper.ExcelToDataTable("Sheet 1", true);
                    //DataTable dt = excelhelper.ExcelToDataTable(0, 0);
                    //var enu = dt.AsEnumerable();
                    var list = dt.DataTableToList<EmployeeModel>();
                    var a = list.Where(n => true).ToList();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mapper.Reset();
            Mapper.CreateMap<Employees, EmployeeModel>();
            using (var db = new SMCSFEEntities())
            {
                var employeeList = db.Employees.Select(Mapper.Map<Employees, EmployeeModel>).ToList();
                var excelHelper = new NPOIExcelHelper("D://123.xlsx");
                excelHelper.DataTableToExcel(ListToDataTable(employeeList), "123", true);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var path = "D://123.xlsx";
            string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
            using (var conn = new OleDbConnection(connstring))
            {
                conn.Open();
                DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });  //得到所有sheet的名字 
                string firstSheetName = sheetsName.Rows[0][2].ToString();   //得到第一个sheet的名字    
                string sql = string.Format("SELECT * FROM [{0}]","Sheet 1");  //查询字符串 
                var ada = new OleDbDataAdapter(sql, connstring);
                var set = new DataSet();
                ada.Fill(set);
                var list= set.Tables[0];
                var type = list.GetType();
            }
        }
    }
}
