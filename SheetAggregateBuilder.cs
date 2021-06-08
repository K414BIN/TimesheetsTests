using System;
using System.Collections.Generic;
using System.Text;
using Timesheets.Models.Dto;
using Timesheets.Services.Aggregates.SheetAggregate;

namespace TimesheetsTests
{
    public class SheetAggregateBuilder
    {
        public Guid SheetEmployeeId = Guid.Parse("50402246-dd7e-419f-b045-be80853866c1");
        public Guid SheetContractId = Guid.Parse("751edbf2-cbab-4f4f-977f-77e74582a025");
        public Guid SheetServiceId = Guid.Parse("ba2013f5-ffe6-41f5-831b-d2ecd4120a39");

        public  SheetAggregate CreateRandomSheet()
        {
            var sheetRequest = new SheetCreateRequest()
            {
                Amount = 8,
                ContractID = SheetContractId,
                Date = DateTime.Now,
                EmployeeID = SheetEmployeeId,
                ServiceID = SheetServiceId
            };
            var result =  SheetAggregate.CreateFromSheetRequest(sheetRequest);
            return result;
        }
    }
}
