using FlightSystem.Core.Enums;
using FlightSystem.Core.Models;

namespace FlightSystem.Test.Core;

public class FlightSystemCoreTests
{
    [Fact]
    public void FlightStatus_EnumValues_ShouldBeCorrect()
    {
        // Test FlightStatus enum values
        Assert.Equal("Scheduled", FlightStatus.Scheduled.ToString());
        Assert.Equal("CheckinOpen", FlightStatus.CheckinOpen.ToString());
        Assert.Equal("Boarding", FlightStatus.Boarding.ToString());
        Assert.Equal("Departed", FlightStatus.Departed.ToString());
        Assert.Equal("Delayed", FlightStatus.Delayed.ToString());
        Assert.Equal("Cancelled", FlightStatus.Cancelled.ToString());
    }

    [Fact]
    public void PassengerType_EnumValues_ShouldBeCorrect()
    {
        // Test PassengerType enum values
        Assert.Equal("Adult", PassengerType.Adult.ToString());
        Assert.Equal("Child", PassengerType.Child.ToString());
        Assert.Equal("Infant", PassengerType.Infant.ToString());
    }

    [Fact]
    public void SeatClass_EnumValues_ShouldBeCorrect()
    {
        // Test SeatClass enum values
        Assert.Equal("Economy", SeatClass.Economy.ToString());
        Assert.Equal("Business", SeatClass.Business.ToString());
        Assert.Equal("First", SeatClass.First.ToString());
    }

    [Fact]
    public void EmployeeRole_EnumValues_ShouldBeCorrect()
    {
        // Test EmployeeRole enum values
        Assert.Equal("Admin", EmployeeRole.Admin.ToString());
        Assert.Equal("CheckinAgent", EmployeeRole.CheckinAgent.ToString());
        Assert.Equal("Supervisor", EmployeeRole.Supervisor.ToString());
    }

    [Fact]
    public void FlightStatus_FromString_ShouldWorkCorrectly()
    {
        // Test converting string to FlightStatus
        Assert.True(Enum.TryParse<FlightStatus>("CheckinOpen", out var status1));
        Assert.Equal(FlightStatus.CheckinOpen, status1);

        Assert.True(Enum.TryParse<FlightStatus>("Boarding", out var status2));
        Assert.Equal(FlightStatus.Boarding, status2);
    }

    [Fact]
    public void PassengerType_FromString_ShouldWorkCorrectly()
    {
        // Test converting string to PassengerType
        Assert.True(Enum.TryParse<PassengerType>("Adult", out var type1));
        Assert.Equal(PassengerType.Adult, type1);

        Assert.True(Enum.TryParse<PassengerType>("Child", out var type2));
        Assert.Equal(PassengerType.Child, type2);
    }

    [Fact]
    public void SeatClass_FromString_ShouldWorkCorrectly()
    {
        // Test converting string to SeatClass
        Assert.True(Enum.TryParse<SeatClass>("Economy", out var seatClass1));
        Assert.Equal(SeatClass.Economy, seatClass1);

        Assert.True(Enum.TryParse<SeatClass>("Business", out var seatClass2));
        Assert.Equal(SeatClass.Business, seatClass2);
    }

    [Fact]
    public void EmployeeRole_FromString_ShouldWorkCorrectly()
    {
        // Test converting string to EmployeeRole
        Assert.True(Enum.TryParse<EmployeeRole>("Admin", out var role1));
        Assert.Equal(EmployeeRole.Admin, role1);

        Assert.True(Enum.TryParse<EmployeeRole>("CheckinAgent", out var role2));
        Assert.Equal(EmployeeRole.CheckinAgent, role2);

        Assert.True(Enum.TryParse<EmployeeRole>("Supervisor", out var role3));
        Assert.Equal(EmployeeRole.Supervisor, role3);
    }

    [Fact]
    public void FlightStatus_AllValues_ShouldBeUnique()
    {
        // Test that all FlightStatus values are unique
        var values = Enum.GetValues<FlightStatus>();
        var uniqueValues = values.Distinct();
        Assert.Equal(values.Length, uniqueValues.Count());
    }

    [Fact]
    public void PassengerType_AllValues_ShouldBeUnique()
    {
        // Test that all PassengerType values are unique
        var values = Enum.GetValues<PassengerType>();
        var uniqueValues = values.Distinct();
        Assert.Equal(values.Length, uniqueValues.Count());
    }
}
