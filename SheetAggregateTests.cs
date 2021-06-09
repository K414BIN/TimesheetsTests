using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace TimesheetsTests
{
    public class SheetAggregateTests
    {
         public static Guid EmployeeId1 = Guid.Parse("192675bf-2fc7-4e5e-b1b9-1cbc3f23bf32");

        [Fact]
        public void SheetAggregate_CreateRandomFromSheetRequest()
        {

            var builder = new SheetAggregateBuilder();

            var sheet =  builder.CreateRandomSheet();
            sheet.Amount.Should().Be(8);
            sheet.ContractID.Should().Be(builder.SheetContractId);
            sheet.ServiceID.Should().Be(builder.SheetServiceId);
            sheet.EmployeeID.Should().Be(builder.SheetEmployeeId);
     
            sheet.Date.Should().BeExactly(TimeSpan.FromSeconds(DateTimeOffset.Now.ToUnixTimeSeconds()));
        }

        [Fact]
        public void SheetAggregate_WhenApproved_IsApprovedIsTrue()
        {

            var builder = new SheetAggregateBuilder();

            var sheet =  builder.CreateRandomSheet();
             
            sheet.ApproveSheet();
            sheet.IsApproved.Should().BeTrue();
          
            sheet.ApprovedDate.Should().BeExactly(TimeSpan.FromSeconds(DateTimeOffset.Now.ToUnixTimeSeconds()));
     
        }

        public static  IEnumerable<object[]> TestData()
        {

            yield return new object[] { EmployeeId1 };
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void SheetAggregate_Changing_EmployeeId( Guid newEmployeeId)
        {
            // arrange
            var builder = new SheetAggregateBuilder();
            var sheet =  builder.CreateRandomSheet();

            // act
            sheet.ChangeEmployee(newEmployeeId);

            // assert
            sheet.EmployeeID.Should().Be(newEmployeeId);
        }

    }
}