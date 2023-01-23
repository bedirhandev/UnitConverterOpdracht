using System;
using System.Collections.Generic;

namespace UnitConverterLibrary
{
    /// <summary>
    /// Enumeration of the different unit types.
    /// </summary>
    public enum UnitType
    {
        Meter,
        Kilometer,
        MetersPerSecond,
        KilometersPerHour,
        Second,
        Hour
    }

    /// <summary>
    /// Abstract class representing a unit of measurement.
    /// </summary>
    public abstract class Unit
    {
        private double _quantity;
        private UnitType _type;

        /// <summary>
        /// Gets or sets the value of the unit.
        /// </summary>
        public double Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        /// <summary>
        /// Gets or sets the type of unit.
        /// </summary>
        public UnitType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="type">The type of unit.</param>
        public Unit(UnitType type)
        {
            _type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="quantity">The value of the unit.</param>
        /// <param name="type">The type of unit.</param>
        public Unit(double quantity, UnitType type)
        {
            if (!IsValidUnitValue(quantity))
            {
                throw new ArgumentException("Invalid unit value. Value must be greater than 0 and not NaN or Infinity.");
            }

            _quantity = quantity;
            _type = type;
        }

        /// <summary>
        /// Returns a string representation of the unit.
        /// </summary>
        /// <returns>The string representation of the unit.</returns>
        public override string ToString()
        {
            return $"{_quantity} {_type}";
        }

        /// <summary>
        /// Determines if a unit value is valid.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the value is valid, false otherwise.</returns>
        private bool IsValidUnitValue(double value)
        {
            return !(double.IsNaN(value) ||
                double.IsInfinity(value) ||
                value.Equals(null) ||
                double.IsNegativeInfinity(value) ||
                value <= 0);
        }
    }

    /// <summary>
    /// Class representing a meter unit of measurement.
    /// </summary>
    public class Meter : Unit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class with a value.
        /// </summary>
        /// <param name="value">The value of the unit meter.</param>
        public Meter(double value) : base(value, UnitType.Meter) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        public Meter() : base(UnitType.Meter) { }
    }

    /// <summary>
    /// Represents a unit of distance in kilometers
    /// </summary>
    public class Kilometer : Unit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Kilometer"/> class with a value.
        /// </summary>
        /// <param name="value">The value of the unit kilometer.</param>
        public Kilometer(double value) : base(value, UnitType.Kilometer) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Kilometer"/> class.
        /// </summary>
        public Kilometer() : base(UnitType.Kilometer) { }
    }

    /// <summary>
    /// Represents a unit of velocity in meters per second
    /// </summary>
    public class MetersPerSecond : Unit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetersPerSecond"/> class with a value.
        /// </summary>
        /// <param name="value">The value of the unit  meters per second.</param>
        public MetersPerSecond(double value) : base(value, UnitType.MetersPerSecond) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetersPerSecond"/> class.
        /// </summary>
        public MetersPerSecond() : base(UnitType.MetersPerSecond) { }
    }

    /// <summary>
    /// Represents a unit of velocity in kilometers per hour
    /// </summary>
    public class KilometersPerHour : Unit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KilometersPerHour"/> class with a value.
        /// </summary>
        /// <param name="value">The value of the unit kilometers per hour.</param>
        public KilometersPerHour(double value) : base(value, UnitType.KilometersPerHour) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="KilometersPerHour"/> class.
        /// </summary>
        public KilometersPerHour() : base(UnitType.KilometersPerHour) { }
    }

    /// <summary>
    /// Represents a unit of time in seconds
    /// </summary>
    public class Second : Unit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Second"/> class with a value.
        /// </summary>
        /// <param name="value">The value of the unit second.</param>
        public Second(double value) : base(value, UnitType.Second) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Second"/> class.
        /// </summary>
        public Second() : base(UnitType.Second) { }
    }

    /// <summary>
    /// Represents a unit of time in hours
    /// </summary>
    public class Hour : Unit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hour"/> class with a value.
        /// </summary>
        /// <param name="value">The value of the unit hour.</param>
        public Hour(double value) : base(value, UnitType.Hour) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Hour"/> class.
        /// </summary>
        public Hour() : base(UnitType.Hour) { }
    }

    /// <summary>
    /// Utility class for performing unit conversions
    /// </summary>
    public class UnitConversionUtility
    {
        /// <summary>
        /// Dictionary containing conversion factors between unit types
        /// </summary>
        private static readonly Dictionary<Tuple<UnitType, UnitType>, double> ConversionFactors = new Dictionary<Tuple<UnitType, UnitType>, double>
        {
            { Tuple.Create(UnitType.Meter, UnitType.Kilometer), 1.0 / 1000.0 },
            { Tuple.Create(UnitType.Kilometer, UnitType.Meter), 1000 },
            { Tuple.Create(UnitType.MetersPerSecond, UnitType.KilometersPerHour), 3.6 },
            { Tuple.Create(UnitType.KilometersPerHour, UnitType.MetersPerSecond), 1.0 / 3.6 },
            { Tuple.Create(UnitType.Second, UnitType.Hour), 1.0 / 3600.0 },
            { Tuple.Create(UnitType.Hour, UnitType.Second), 3600 }
        };

        /// <summary>
        /// Determines if the conversion from fromUnitType to toUnitType is allowed
        /// </summary>
        /// <param name="fromUnitType">The type of unit to convert from</param>
        /// <param name="toUnitType">The type of unit to convert to</param>
        /// <returns>True if the conversion is allowed, false otherwise</returns>
        public static bool IsCompatible(UnitType fromUnitType, UnitType toUnitType)
        {
            return ConversionFactors.ContainsKey(Tuple.Create(fromUnitType, toUnitType));
        }

        /// <summary>
        /// Gets the conversion factor between the specified unit types
        /// </summary>
        /// <param name="fromUnitType">The type of unit to convert from</param>
        /// <param name="toUnitType">The type of unit to convert to</param>
        /// <returns>The conversion factor between the specified unit</returns>
        public static double GetConversionFactor(UnitType fromUnitType, UnitType toUnitType)
        {
            return ConversionFactors[Tuple.Create(fromUnitType, toUnitType)];
        }

        /// <summary>
        /// Converts a quantity from one unit to another
        /// </summary>
        /// <param name="fromUnitType">The unit type to convert from</param>
        /// <param name="toUnitType">The unit type to convert to</param>
        /// <param name="conversionFactor">The conversion factor used to convert the quantity</param>
        public static void Convert(Unit fromUnitType, Unit toUnitType, double conversionFactor)
        {
            toUnitType.Quantity = fromUnitType.Quantity * conversionFactor;
        }

        /// <summary>
        /// Creates an instance of a unit class
        /// </summary>
        /// <param name="fromUnitType">The type of unit to create</param>
        /// <param name="value">The value of the unit</param>
        /// <returns>A new instance of a unit class</returns>
        public static Unit Create(UnitType fromUnitType, double value)
        {
            string className = fromUnitType.ToString();
            Type unitClass = Type.GetType("UnitConverterLibrary." + className);
            Unit unit = (Unit)Activator.CreateInstance(unitClass, value);
            return unit;
        }
    }

    /// <summary>
    /// A class that provides methods for converting units
    /// </summary>
    public static class UnitConverter
    {
        /// <summary>
        /// Converts a quantity from one unit to another
        /// </summary>
        /// <param name="fromUnitType">The unit type to convert from</param>
        /// <param name="toUnitType">The unit type to convert to</param>
        public static void Convert(Unit fromUnitType, Unit toUnitType)
        {
            if (!UnitConversionUtility.IsCompatible(fromUnitType.Type, toUnitType.Type))
            {
                throw new ArgumentException("Conversion between these units is not allowed");
            }

            var conversionFactor = UnitConversionUtility.GetConversionFactor(fromUnitType.Type, toUnitType.Type);
            UnitConversionUtility.Convert(fromUnitType, toUnitType, conversionFactor);
        }
    }
}