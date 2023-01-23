using NUnit.Framework;
using System;
using System.Reflection;
using UnitConverterLibrary;

[TestFixture]
public class UnitConverterTests
{
    [Test]
    [TestCase(1, UnitType.Meter, UnitType.Kilometer, 0.001)]
    [TestCase(1, UnitType.Kilometer, UnitType.Meter, 1000)]
    [TestCase(1, UnitType.MetersPerSecond, UnitType.KilometersPerHour, 3.6)]
    [TestCase(1, UnitType.KilometersPerHour, UnitType.MetersPerSecond, 0.277777777777778)]
    [TestCase(1, UnitType.Second, UnitType.Hour, 0.000277777777777778)]
    [TestCase(1, UnitType.Hour, UnitType.Second, 3600)]
    public void TestUnitConversion(double value, UnitType fromUnitType, UnitType toUnitType, double expected)
    {
        //Arrange
        var unit = UnitConversionUtility.Create(fromUnitType, value);

        //Act
        bool result = UnitConversionUtility.IsCompatible(fromUnitType, toUnitType);
        double conversionFactor = UnitConversionUtility.GetConversionFactor(fromUnitType, toUnitType);
        double convertedValue = unit.Quantity * conversionFactor;

        //Assert
        Assert.IsTrue(result);
        Assert.AreEqual(expected, conversionFactor, 0.0001);
        Assert.AreEqual(expected * value, convertedValue, 0.0001);
    }

    [TestCase(UnitType.Meter, UnitType.Kilometer, true)]
    [TestCase(UnitType.Meter, UnitType.Second, false)]
    [TestCase(UnitType.KilometersPerHour, UnitType.MetersPerSecond, true)]
    public void TestIsCompatible(UnitType fromUnitType, UnitType toUnitType, bool expected)
    {
        // Act
        bool result = UnitConversionUtility.IsCompatible(fromUnitType, toUnitType);

        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    [TestCase(1, UnitType.Meter, "1 Meter")]
    [TestCase(0.001, UnitType.Kilometer, "0,001 Kilometer")]
    [TestCase(3.6, UnitType.MetersPerSecond, "3,6 MetersPerSecond")]
    public void TestToString(double value, UnitType type, string expected)
    {
        // Arrange
        Unit unit = UnitConversionUtility.Create(type, value);

        // Act
        string result = unit.ToString();

        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(double.NaN)]
    [TestCase(double.NegativeInfinity)]
    [TestCase(double.PositiveInfinity)]
    public void TestInvalidUnitValue(double invalidValue)
    {
        //Arrange
        UnitType unitType = UnitType.Meter;
        string expectedMessage = "Invalid unit value. Value must be greater than 0 and not NaN or Infinity.";

        //Act & Assert
        TargetInvocationException ex = Assert.Throws<TargetInvocationException>(() => UnitConversionUtility.Create(unitType, invalidValue));
        ArgumentException argEx = ex.InnerException as ArgumentException;
        Assert.AreEqual(expectedMessage, argEx.Message);
    }
}
