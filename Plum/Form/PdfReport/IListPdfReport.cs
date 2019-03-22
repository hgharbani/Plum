using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfRpt.Core.Contracts;
using Plum.Data;
using Plum.Model.Model.MAterial;

namespace Plum.Form.PdfReport
{
  public  class IListPdfReport
    {
        public IPdfReportData CreatePdfReport()
        {
            return new PdfRpt.FluentInterface.PdfReport().DocumentPreferences(doc =>
            {
                doc.RunDirection(PdfRunDirection.RightToLeft);
                doc.Orientation(PageOrientation.Portrait);
                doc.PageSize(PdfPageSize.A4);
                doc.DocumentMetadata(new DocumentMetadata { Author = "Vahid", Application = "PdfRpt", Keywords = "IList Rpt.", Subject = "Test Rpt", Title = "Test" });
                doc.Compression(new CompressionSettings
                {
                    EnableCompression = true,
                    EnableFullCompression = true
                });
                doc.PrintingPreferences(new PrintingPreferences
                {
                    ShowPrintDialogAutomatically = true
                });
            })
            .DefaultFonts(fonts =>
            {
                fonts.Path(System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\IRANSans.ttf"),
                           System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "fonts\\verdana.ttf"));
                fonts.Size(9);
                fonts.Color(System.Drawing.Color.Black);
            })
            .PagesFooter(footer =>
            {
                footer.DefaultFooter(DateTime.Now.ToString("MM/dd/yyyy"));
            })
            .PagesHeader(header =>
            {
                header.CacheHeader(cache: true); // It's a default setting to improve the performance.
                header.DefaultHeader(defaultHeader =>
                {
                    defaultHeader.RunDirection(PdfRunDirection.RightToLeft);
                
                    defaultHeader.Message("گزارش مواد لازم یک غذا");
                });
            })
            .MainTableTemplate(template =>
            {
                template.BasicTemplate(BasicTemplate.BlackAndBlue1Template);
            })
            .MainTablePreferences(table =>
            {
                table.ColumnsWidthsType(TableColumnWidthType.Relative);
                table.NumberOfDataRowsPerPage(5);
            })
            .MainTableDataSource(dataSource =>
            {
                var listOfRows = new List<FoodMaterialModel>
                {
                    new FoodMaterialModel {Id = 0, MaterialName = "گوشت°", Quantity = 100, Price = 1}
                };

                for (var i = 1; i <= 2; i++)
                {
                    listOfRows.Add(new FoodMaterialModel { Id = i, MaterialName = "گوشت°"+i.ToString(), Quantity = 100, Price = 1+i });
                }
                dataSource.StronglyTypedList(listOfRows);
            })
            .MainTableSummarySettings(summarySettings =>
            {
                summarySettings.OverallSummarySettings("جمع کل");
                summarySettings.PreviousPageSummarySettings("نقل از صفحه قبل");
                summarySettings.PageSummarySettings("جمع صفحه");

            })
            .MainTableColumns(columns =>
            {
                columns.AddColumn(column =>
                {
                    column.PropertyName("rowNo");
                    column.IsRowNumber(true);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(0);
                    column.Width(1);
                    column.HeaderCell("#");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<FoodMaterialModel>(x => x.Id);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(1);
                    column.Width(2);
                    column.HeaderCell("Id");
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<FoodMaterialModel>(x => x.MaterialName);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                    column.IsVisible(true);
                    column.Order(2);
                    column.Width(3);
                    column.HeaderCell("Name", horizontalAlignment: HorizontalAlignment.Left);
                    column.Font(font =>
                    {
                        font.Size(10);
                        font.Color(System.Drawing.Color.Brown);
                    });
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<FoodMaterialModel>(x => x.Quantity);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                    column.IsVisible(true);
                    column.Order(3);
                    column.Width(3);
                    column.HeaderCell("Last Name");
                    column.PaddingLeft(25);
                });

                columns.AddColumn(column =>
                {
                    column.PropertyName<FoodMaterialModel>(x => x.Price);
                    column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                    column.IsVisible(true);
                    column.Order(4);
                    column.Width(2);
                    column.HeaderCell("Balance");
                    column.ColumnItemsTemplate(template =>
                    {
                        template.TextBlock();
                        template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                    column.AggregateFunction(aggregateFunction =>
                    {
                        aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                        aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                            ? string.Empty : string.Format("{0:n0}", obj));
                    });
                });

            })
                .MainTableDataSource(dataSource =>
                {
                    var listOfRows = new List<FoodSurplusPrice>
                    {
                        new FoodSurplusPrice {Id = 0, CostTitle = "jsjd°", Price = 100}
                    };

                    for (var i = 1; i <= 2; i++)
                    {
                        listOfRows.Add(new FoodSurplusPrice { Id = i, CostTitle = "تستی°" + i.ToString(), Price = 1 + i });
                    }
                    dataSource.StronglyTypedList(listOfRows);
                })
                 .MainTableColumns(columns =>
                 {
                     columns.AddColumn(column =>
                     {
                         column.PropertyName("rowNo");
                         column.IsRowNumber(true);
                         column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                         column.IsVisible(true);
                         column.Order(0);
                         column.Width(1);
                         column.HeaderCell("#");
                     });

                     columns.AddColumn(column =>
                     {
                         column.PropertyName<FoodSurplusPrice>(x => x.Id);
                         column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                         column.IsVisible(true);
                         column.Order(1);
                         column.Width(2);
                         column.HeaderCell("Id");
                     });

                     columns.AddColumn(column =>
                     {
                         column.PropertyName<FoodSurplusPrice>(x => x.CostTitle);
                         column.CellsHorizontalAlignment(HorizontalAlignment.Center);
                         column.IsVisible(true);
                         column.Order(2);
                         column.Width(3);
                         column.HeaderCell("Name", horizontalAlignment: HorizontalAlignment.Left);
                         column.Font(font =>
                         {
                             font.Size(10);
                             font.Color(System.Drawing.Color.Brown);
                         });
                     });

                    
                     columns.AddColumn(column =>
                     {
                         column.PropertyName<FoodMaterialModel>(x => x.Price);
                         column.CellsHorizontalAlignment(HorizontalAlignment.Left);
                         column.IsVisible(true);
                         column.Order(4);
                         column.Width(2);
                         column.HeaderCell("Balance");
                         column.ColumnItemsTemplate(template =>
                         {
                             template.TextBlock();
                             template.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                                 ? string.Empty : string.Format("{0:n0}", obj));
                         });
                         column.AggregateFunction(aggregateFunction =>
                         {
                             aggregateFunction.NumericAggregateFunction(AggregateFunction.Sum);
                             aggregateFunction.DisplayFormatFormula(obj => obj == null || string.IsNullOrEmpty(obj.ToString())
                                                                 ? string.Empty : string.Format("{0:n0}", obj));
                         });
                     });

                 })
                .MainTableEvents(events =>
                {
                    events.DataSourceIsEmpty(message: "رکوردی یافت نشد.");
                })
                .Export(export =>
                {
                    export.ToExcel();
                    export.ToCsv();
                    export.ToXml();
                })
                .Generate(data => data.AsPdfFile(AppPath.ApplicationPath + "\\Pdf\\RptIListSample.pdf"));
        }
    }
}