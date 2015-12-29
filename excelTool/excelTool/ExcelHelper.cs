using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;

namespace excelTool
{
    public class ExcelHelper
    {
        public static Stream RenderDataTableToExcel(DataTable sourceTable)
        {
            var workbook = new HSSFWorkbook();
            var ms = new MemoryStream();
            var sheet = (HSSFSheet)workbook.CreateSheet();
            var headerRow = (HSSFRow)sheet.CreateRow(0);

            // handling header. 
            foreach (DataColumn column in sourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value. 
            int rowIndex = 1;

            foreach (DataRow row in sourceTable.Rows)
            {
                var dataRow = (HSSFRow)sheet.CreateRow(rowIndex);

                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }

                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }
        public static DataTable RenderDataTableFromExcel(Stream excelFileStream, int sheetIndex, int headerRowIndex)
        {
            var workbook = new HSSFWorkbook(excelFileStream);
            var sheet = (HSSFSheet)workbook.GetSheetAt(sheetIndex);

            var table = new DataTable();

            var headerRow = (HSSFRow)sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                var column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                var row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)

                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            excelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }
    }
}
