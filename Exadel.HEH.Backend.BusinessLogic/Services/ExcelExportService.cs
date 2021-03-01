using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNet.OData.Query;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class ExcelExportService : IExportService
    {
        private const string ExcelTemplatesPath = "ExcelTemplates";
        private const string FileName = "Statistics.xlsx";
        private const string SheetName = "Statistics";

        private readonly IStatisticsService _statisticsService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        private readonly IVendorService _vendorService;
        private readonly ILocationService _locationService;

        public ExcelExportService(
            IStatisticsService statisticsService,
            ICategoryService categoryService,
            ITagService tagService,
            IVendorService vendorService,
            ILocationService locationService)
        {
            _statisticsService = statisticsService;
            _categoryService = categoryService;
            _tagService = tagService;
            _vendorService = vendorService;
            _locationService = locationService;
        }

        public async Task<MemoryStream> GetFileAsync(ODataQueryOptions<DiscountStatisticsDto> options,
            string searchText = default, DateTime startDate = default, DateTime endDate = default)
        {
            var statistics = await _statisticsService.GetStatisticsAsync(options, searchText, startDate, endDate);

            await using var fileStream = File.OpenRead(Path.Combine(ExcelTemplatesPath, FileName));
            var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(memoryStream);

            var workSheet = package.Workbook.Worksheets[SheetName];
            workSheet.Cells[4, 2].Value = DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ssZ");

            var statisticsList = statistics.ToList();
            var rowInd = 7;
            ConfigureRangeStyles(workSheet, rowInd, statisticsList.Count);

            foreach (var statisticsItem in statisticsList)
            {
                await FillStatisticsRowAsync(workSheet, rowInd, statisticsItem);

                rowInd++;
            }

            workSheet.Column(1).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(7).AutoFit();
            workSheet.Column(8).AutoFit();

            await package.SaveAsync();

            return new MemoryStream(await package.GetAsByteArrayAsync()) { Position = 0 };
        }

        private void ConfigureRangeStyles(ExcelWorksheet workSheet, int rowInd, int count)
        {
            var range = workSheet.Cells[$"A{rowInd}:J{rowInd + count - 1}"];

            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

            var borderColor = Color.FromArgb(7, 128, 249);
            range.Style.Border.Top.Color.SetColor(borderColor);
            range.Style.Border.Bottom.Color.SetColor(borderColor);
            range.Style.Border.Left.Color.SetColor(borderColor);
            range.Style.Border.Right.Color.SetColor(borderColor);

            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 235, 247));

            range.Style.Font.Color.SetColor(Color.FromArgb(2, 52, 102));
        }

        private async Task FillStatisticsRowAsync(ExcelWorksheet workSheet, int rowInd,
            DiscountStatisticsDto statisticsItem)
        {
            var category = (await _categoryService.GetByIdAsync(statisticsItem.CategoryId)).Name;
            var tags = (await _tagService.GetByIdsAsync(statisticsItem.TagsIds))
                .Select(t => t.Name)
                .ToList();

            var discountAddresses = statisticsItem.Addresses.ToList();
            var countriesIds = discountAddresses.Select(a => a.CountryId).Distinct();
            var locations = (await _locationService.GetByIdsAsync(countriesIds))
                .ToDictionary(l => l.Id, l => l);

            var addresses = new List<string>();

            foreach (var discountAddress in discountAddresses)
            {
                var country = locations[discountAddress.CountryId].Country;
                var city = discountAddress.CityId.HasValue
                    ? locations[discountAddress.CountryId].Cities
                        .First(c => c.Id == discountAddress.CityId.Value).Name
                    : string.Empty;

                if (city == string.Empty)
                {
                    addresses.Add(country);
                    continue;
                }

                var street = discountAddress.Street;

                if (street == string.Empty)
                {
                    addresses.Add($"{country}, {city}");
                    continue;
                }

                addresses.Add(string.Join(", ", country, city, street));
            }

            var phones = (await _vendorService.GetByIdAsync(statisticsItem.VendorId)).Phones
                .Where(p => statisticsItem.PhonesIds.Contains(p.Id))
                .Select(p => p.Number)
                .ToList();

            workSheet.Cells[rowInd, 1].Value = statisticsItem.VendorName;
            workSheet.Cells[rowInd, 2].Value = statisticsItem.Conditions;
            workSheet.Cells[rowInd, 3].Value = statisticsItem.ViewsAmount;
            workSheet.Cells[rowInd, 4].Value = statisticsItem.PromoCode;
            workSheet.Cells[rowInd, 5].Value = category;
            workSheet.Cells[rowInd, 6].Value = tags.Any() ? string.Join("\n", tags) : "-";
            workSheet.Cells[rowInd, 7].Value = statisticsItem.StartDate.ToString("dd.MM.yyyy");
            workSheet.Cells[rowInd, 8].Value = statisticsItem.EndDate.HasValue
                ? statisticsItem.EndDate.Value.ToString("dd.MM.yyyy")
                : "-";
            workSheet.Cells[rowInd, 9].Value = string.Join(";\n", addresses);
            workSheet.Cells[rowInd, 10].Value = phones.Any() ? string.Join(";\n", phones) : "-";
        }
    }
}