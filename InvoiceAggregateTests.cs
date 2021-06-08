using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Timesheets.Models.Entities;

namespace TimesheetsTests
{
    public class InvoiceAggregateTests
    {
        private static Random rnd = new Random();
        public static int startIndex = rnd.Next(1, 8);

        public static IEnumerable<Sheet> testSheets = new List<Sheet>
        {
            Capacity = startIndex 
        };

        [Fact]
        public void InvoiceAggregate_CreateRequest()
        {
            var builder = new InvoiceAggregateBuilder();
            var sheet = builder.CreateTestObjectInvoiceAggregate();
            sheet.ContractID.Should().Be(builder.SheetContractId);
            sheet.DateStart.Should().BeExactly(TimeSpan.FromSeconds(DateTimeOffset.Now.ToUnixTimeSeconds()));
            sheet.DateEnd.Should().BeExactly(TimeSpan.FromSeconds(DateTimeOffset.Now.ToUnixTimeSeconds()));
        }

        public static IEnumerable<object[]> TestData()
        {

            yield return new object[]
            {
                testSheets
            };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void InvoiceAggregate_CalcTheTestSum(List<Sheet> testsheet)
        {
            var builder = new InvoiceAggregateBuilder();
            var sheet = builder.CreateTestObjectInvoiceAggregate();

            sheet.IncludeSheets(testsheet);
            sheet.Should().Be(testsheet);
        }
    }
}
