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
        private IEnumerable<int> Unit_test_Range = Enumerable.Range(1, startIndex);
        
        public static Contract Unit_test_Contract = new Contract()
        {
            IsDeleted = false, DateStart = DateTime.Now, DateEnd = DateTime.Now,
            Description = "neque tellus, imperdiet non, vestibulum nec, euismod in,", Title = "quis"
        };
        public static Invoice Unit_test_Invoice = new Invoice() { ContractID = Guid.Parse("71A382FD-F695-8702-D9DC-C28174FE34D4") };
        public static Service Unit_test_Service = new Service() {Name = "Euismod"};
        public static Employee Unit_test_Employee= new Employee() {  UserID =Guid.Parse( "C160074B-8451-D8CC-D864-964730DCDAE5")};
        public static Sheet Unit_test_Sheets = new Sheet
        {
            Contract = Unit_test_Contract, Employee = Unit_test_Employee, Invoice = Unit_test_Invoice, Service = Unit_test_Service
        };

        public static IEnumerable<Sheet> TestSheets = new List<Sheet>() { };
        
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

            sheet.IncludeSheets( testSheets);
            sheet.Should().Be( testSheets);
        }
    }
}
