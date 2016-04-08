namespace CRM.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public static class EnumerableExtensions
    {
        public static void WriteHtmlTable<T>(this IEnumerable<T> data, TextWriter output)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    Table table = new Table();
                    TableRow row = new TableRow();
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                    foreach (PropertyDescriptor prop in props)
                    {
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Text = prop.Name;
                        row.Cells.Add(hcell);
                    }

                    table.Rows.Add(row);

                    foreach (T item in data)
                    {
                        row = new TableRow();
                        foreach (PropertyDescriptor prop in props)
                        {
                            TableCell cell = new TableCell();
                            cell.Text = prop.Converter.ConvertToString(prop.GetValue(item));
                            var hasUnpaidInvoices = bool.Parse(item.GetType().GetProperty("HasUnpaidInvoices").GetValue(item, null).ToString());
                            if (hasUnpaidInvoices)
                            {
                                cell.BackColor = System.Drawing.Color.Red;
                            }
                            row.Cells.Add(cell);
                        }
                        table.Rows.Add(row);
                    }

                    table.RenderControl(htw);

                    output.Write(sw.ToString());
                }
            }
        }

        public static void GenerateExcel<T>(this IEnumerable<T> data, TextWriter output)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (var item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
