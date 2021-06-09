using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Timesheets.Models.Entities;
using System.Linq;

namespace TimesheetsTests
{
    public class InvoiceAggregateTests
    {
        private static Random rnd = new Random();
        public static int startIndex = rnd.Next(1, 8);
        private IEnumerable<int> testRange = Enumerable.Range(1, startIndex);

        public static IEnumerable<Sheet> TestSheets = new List<Sheet>()
        {
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
                TestSheets
            };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void InvoiceAggregate_CalcTheTestSum( IEnumerable<Sheet> testSheets)
        {
            var builder = new InvoiceAggregateBuilder();
            var sheet = builder.CreateTestObjectInvoiceAggregate();

            sheet.IncludeSheets(testSheets);
            sheet.Should().Be(testSheets);
        }
    }
}
