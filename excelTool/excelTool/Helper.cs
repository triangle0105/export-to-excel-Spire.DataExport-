using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace excelTool
{
    public static class Helper
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            System.Type propertyType = prop.PropertyType;
                            System.TypeCode typeCode = System.Type.GetTypeCode(propertyType);
                            switch (typeCode)
                            {
                                case TypeCode.Int32:
                                    prop.SetValue(obj, Convert.ToInt32(row[prop.Name]), null);
                                    break;
                                case TypeCode.Int64:
                                    prop.SetValue(obj, Convert.ToInt64(row[prop.Name]), null);
                                    break;
                                case TypeCode.String:
                                    prop.SetValue(obj, row[prop.Name], null);
                                    break;
                                case TypeCode.Object:
                                    if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                                    {
                                        prop.SetValue(obj, Guid.Parse(row[prop.Name].ToString()), null);
                                    }
                                    else if (propertyType == typeof(DateTime?))
                                    {
                                        //var date = (DateTime?)prop.GetValue(string.IsNullOrEmpty(row[prop.Name].ToString()) ? (DateTime?)null : row[prop.Name], null);
                                        //if (row[prop.Name].ToString() != "")
                                        //{
                                        //    try
                                        //    {
                                        //        propertyInfo.SetValue(
                                        //            obj,
                                        //            (DateTime?)DateTime.ParseExact(row[prop.Name].ToString(), "MM/dd/yyyy HH:mm:ss", null), null);
                                        //    }
                                        //    catch
                                        //    {
                                        //        propertyInfo.SetValue(obj, (DateTime?)DateTime.ParseExact(row[prop.Name].ToString(), "MM/dd/yyyy", null), null);
                                        //    }
                                        //}
                                        //else
                                        //    propertyInfo.SetValue(obj, null, null);


                                        //var date = string.IsNullOrEmpty(row[prop.Name].ToString()) ? (DateTime?)null :(DateTime?) DateTime.ParseExact(row[prop.Name].ToString(),"MM/dd/yyyy HH:mm:ss",null);
                                        var date = string.IsNullOrEmpty(row[prop.Name].ToString()) ? (DateTime?)null : (DateTime?)Convert.ChangeType(row[prop.Name], typeof(DateTime));
                                        if (date.HasValue)
                                        {
                                            DateTime? newDate = DateTime.SpecifyKind(date.Value, DateTimeKind.Utc);
                                            prop.SetValue(obj, newDate, null);
                                        }
                                        
                                            
                                    }
                                    break;
                                default:
                                    propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                                    break;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }

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
