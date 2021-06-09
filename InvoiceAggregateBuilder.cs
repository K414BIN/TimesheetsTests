using Timesheets.Services.Aggregates.InvoiceAggregate;
using Timesheets.Models.Dto;
using System;

namespace TimesheetsTests
{
    public class InvoiceAggregateBuilder
    {
        public Guid SheetContractId = Guid.Parse("fe48f122-c9bd-4a14-87d8-c71a77b9550b");

        public InvoiceAggregate CreateTestObjectInvoiceAggregate()
        {
            var testRequest = new InvoiceRequest()
            {
                ContractId = SheetContractId,
                DateStart = DateTime.Today,
                DateEnd = DateTime.Now
            };
            var result = InvoiceAggregate.Create(testRequest.ContractId,testRequest.DateStart,testRequest.DateEnd);
            return result;
        }
    }
}