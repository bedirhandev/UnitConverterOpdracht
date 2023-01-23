using System;
using UnitConverterLibrary;

namespace UnitConverterApp
{
    class Program
    {
        // A demo of the assignment: conversion of units time, distance and velocity.
        static void Main(string[] args)
        {
            // Conversion from seconds to hours
            double quantity = 3600;
            Second second = new Second(quantity);
            Hour hour = new Hour();
            UnitConverter.Convert(second, hour);
            Console.WriteLine($"{ second } = { hour }");

            // Conversion from hours to seconds
            quantity = 1;
            hour = new Hour(quantity);
            second = new Second();
            UnitConverter.Convert(hour, second);
            Console.WriteLine($"{ hour } = { second }");

            // Conversion from meters to kilometers
            quantity = 1000;
            Meter meter = new Meter(quantity);
            Kilometer kilometer = new Kilometer();
            UnitConverter.Convert(meter, kilometer);
            Console.WriteLine($"{ meter } = { kilometer }");

            // Conversion from kilometers to meters
            quantity = 1;
            kilometer = new Kilometer(quantity);
            meter = new Meter();
            UnitConverter.Convert(kilometer, meter);
            Console.WriteLine($"{ kilometer } = { meter }");

            // Conversion from meters per second to kilometers per hour
            quantity = 1;
            MetersPerSecond meterPerSecond = new MetersPerSecond(quantity);
            KilometersPerHour kilometerPerHour = new KilometersPerHour();
            UnitConverter.Convert(meterPerSecond, kilometerPerHour);
            Console.WriteLine($"{ meterPerSecond } = { kilometerPerHour }");

            // Conversion from kilometers per hour to meters per second
            quantity = 3.6;
            kilometerPerHour = new KilometersPerHour(quantity);
            meterPerSecond = new MetersPerSecond();
            UnitConverter.Convert(kilometerPerHour, meterPerSecond);
            Console.WriteLine($"{ kilometerPerHour } = { meterPerSecond }");
        }
    }
}