using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyModular.Utils
{
    /// <summary>
    /// Excel样式辅助类（NPOI）
    /// </summary>
    public  class ExcelStyleHelper
    {
        public static ICellStyle GetTitleStyle(XSSFWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            //边框  
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            //水平对齐  
            style.Alignment = HorizontalAlignment.Center;

            //垂直对齐  
            style.VerticalAlignment = VerticalAlignment.Center;

            //设置字体
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 10;
            font.FontName = "微软雅黑";
            font.Boldweight = (short)FontBoldWeight.Bold;
            style.SetFont(font);

            //背景颜色
            style.FillForegroundColor = 43;
            style.FillPattern = FillPattern.SolidForeground;

            return style;
        }

        public static ICellStyle GetDataStyle(XSSFWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            //边框  
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            //文本居中
            style.VerticalAlignment = VerticalAlignment.Center;
            //换行
            style.WrapText = true;

            return style;
        }

        public static void SetRegionBorder(int border, CellRangeAddress region, ISheet sheet, IWorkbook workbook)
        {
            RegionUtil.SetBorderLeft(border, region, sheet, workbook);
            RegionUtil.SetBorderRight(border, region, sheet, workbook);
            RegionUtil.SetBorderTop(border, region, sheet, workbook);
            RegionUtil.SetBorderBottom(border, region, sheet, workbook);
        }
    }
}
