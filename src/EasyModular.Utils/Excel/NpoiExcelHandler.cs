using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace EasyModular.Utils
{
    /// <summary>
    /// 基于NPIO的EXCEL处理类
    /// </summary>
    public class NpoiExcelHandler : IExcelHandler
    {
        public byte[] Export<T>(IList<T> entities, string title) where T : class, new()
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet(string.IsNullOrEmpty(title) ? "sheet" : title);
            var rowIndex = 0; //行下标
            var colIndex = 0; //列下标
            var excludeCols = new List<int>(); //排除的列

            var titleStyle = ExcelStyleHelper.GetTitleStyle(workbook); //标题样式
            var dataStyle = ExcelStyleHelper.GetDataStyle(workbook);   //内容样式

            Type type = typeof(T);
            var pros = type.GetProperties();

            //标题
            if (!string.IsNullOrEmpty(title))
            {

                IRow titleRow = sheet.CreateRow(0);
                titleRow.CreateCell(0).SetCellValue(title);
                titleRow.Height = 400;
                //合并单元格
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, pros.Length - 1));
                rowIndex++;

                //设置合并后style
                var cell = sheet.GetRow(0).GetCell(0);
                cell.CellStyle = titleStyle;
            }

            //列名
            var headerRow = sheet.CreateRow(rowIndex);
            headerRow.Height = 300;
            for (int i = 0; i < pros.Length; i++)
            {
                var descAtr = (DescriptionAttribute[])pros[i].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descAtr.Length <= 0)
                {
                    excludeCols.Add(i);
                    continue;
                }

                var cell = headerRow.CreateCell(colIndex);
                cell.SetCellValue(descAtr[0].Description);
                cell.CellStyle = dataStyle;
                colIndex++;

            }
            rowIndex++;

            //数据
            foreach (var item in entities)
            {
                colIndex = 0;
                var row = sheet.CreateRow(rowIndex);
                row.Height = 300;
                for (int i = 0; i < pros.Length; i++)
                {
                    if (excludeCols.Contains(i))
                        continue;

                    var value = pros[i].GetValue(item);
                    var cell = row.CreateCell(colIndex);
                    cell.SetCellValue(value?.ToString());
                    cell.CellStyle = dataStyle;

                    colIndex++;
                }
                rowIndex++;
            }

            byte[] buffer = null;
            using var ms = new MemoryStream();
            workbook.Write(ms);
            buffer = ms.GetBuffer();
            ms.Close();

            return buffer;

        }

        public List<T> Import<T>(IFormFile formFile, bool hasTitle = false) where T : class, new()
        {
            var result = new List<T>();

            using var ms = new MemoryStream();
            formFile.CopyTo(ms);
            ms.Seek(0, SeekOrigin.Begin);

            var workbook = new XSSFWorkbook(ms);
            var sheet = workbook.GetSheetAt(0);

            var cellNum = sheet.GetRow(0);
            var cols = cellNum.LastCellNum; //列数
            var rowStartIndex = hasTitle ? 2 : 1; //数据起始行
            var headIndex = hasTitle ? 1 : 0; //列名行下标

            var pros = typeof(T).GetProperties();

            for (int i = rowStartIndex; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                var item = new T();
                for (int j = 0; j <= cols; j++)
                {
                    var dataVal = sheet.GetRow(i).GetCell(j)?.ToString();
                    var headCol = sheet.GetRow(headIndex).GetCell(j)?.ToString();
                    foreach (var pro in pros)
                    {
                        var descAtr = (DescriptionAttribute[])pro.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descAtr.Length <= 0)
                            continue;

                        if (descAtr[0].Description != headCol)
                            continue;

                        var dataType = pro.PropertyType.FullName;
                        switch (dataType)
                        {
                            case "System.String":
                                pro.SetValue(item, dataVal);
                                break;
                            case "System.DateTime":
                                pro.SetValue(item, Convert.ToDateTime(dataVal));
                                break;
                            case "System.Boolean":
                                pro.SetValue(item, Convert.ToBoolean(dataVal));
                                break;
                            case "System.Int16":
                                pro.SetValue(item, Convert.ToInt16(dataVal));
                                break;
                            case "System.Int32":
                                pro.SetValue(item, Convert.ToInt32(dataVal));
                                break;
                            case "System.Int64":
                                pro.SetValue(item, Convert.ToInt64(dataVal));
                                break;
                            case "System.Byte":
                                pro.SetValue(item, Convert.ToByte(dataVal));
                                break;
                        }
                        break;
                    }
                }

                result.Add(item);
            }

            return result;

        }

    }
}
