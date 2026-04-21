using System.IO.Pipelines;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BT_NET.Helpers
{
    public class ExcelHelper
    {
        public List<T> ReadFromExcel<T>(IFormFile file, Func<IXLRangeRow, T> mapRow) where T : new()
        {
            var list = new List<T>();
            using var stream = file.OpenReadStream();
            using var worbook = new XLWorkbook(stream);

            var worsheet = worbook.Worksheet(1);
            var rows = worsheet.RangeUsed().RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                T item = mapRow(row);
                list.Add(item);
            }

            return list;

        }
    }
}