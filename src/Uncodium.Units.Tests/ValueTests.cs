﻿using NUnit.Framework;
using System;
using static Uncodium.Units.Physics;
using static Uncodium.Units.SI;

namespace Uncodium.Units.Tests
{
    [TestFixture]
    public class ValueTests
    {
        #region Add

        [Test]
        public void CanAddSameUnits()
        {
            var a = 1 * Meter;
            var b = 1 * Meter;
            var r = a + b;

            Assert.IsTrue((float)r == 2);
        }

        [Test]
        public void CanAddSameDimensionlessUnits()
        {
            var a = 1 * Radian;
            var b = 1 * Radian;
            var r = a + b;

            Assert.IsTrue((float)r == 2);
        }

        [Test]
        public void CanAddSameUnitsWithDifferentScales()
        {
            var a = 1 * Meter;
            var b = 1 * Centimeter;
            var r = a + b;

            Assert.IsTrue((double)r == 1.01);
        }

        [Test]
        public void CannotAddDifferentUnits()
        {
            var a = 1 * Meter;
            var b = 1 * Second;

            Assert.Catch<Exception>(() =>
            {
                var r = a + b;
            });
        }

        [Test]
        public void CannotAddDifferentDimensionlessUnits()
        {
            var a = 1 * Radian;
            var b = 1 * Steradian;

            Assert.Catch<Exception>(() =>
            {
                var r = a + b;
            });
        }

        #endregion

        #region Divide

        [Test]
        public void Divide1()
        {
            var d = 1 * Meter;
            var t = 1 * Second;
            var x = d / t;
        }

        [Test]
        public void Divide2()
        {
            var d = 1 * Kilometer;
            var t = 1 * Second;
            var x = d / t;
        }

        [Test]
        public void Divide3()
        {
            var d = 1 * Meter;
            var t = 1 * Hour;
            var x = d / t;
        }

        [Test]
        public void Divide4()
        {
            var d = 42.195 * Kilometer;
            var t = 2.5 * Hour;
            var x = d / t;
        }

        [Test]
        public void Divide5()
        {
            var d = 42.195 * Kilometer;
            var t = 2.5 * Hour;
            var x = d / t;
            var s = x.ToString();
        }

        [Test]
        public void Divide6()
        {
            var x = Millimeter / Second;
            var s = x.ToString();
        }

        [Test]
        public void Divide7()
        {
            var x = Meter / KilometersPerHour;
            var s = x.ToString();
        }

        #endregion

        #region Conversion

        [Test]
        public void KilometerPerHourToMph()
        {
            var d = 42.195 * Kilometer;
            var t = 2.5 * Hour;
            var x = d / t;
            var y = x.ConvertTo(MilesPerHour);
        }

        [Test]
        public void Marathon()
        {
            var x = ((42.195 * Kilometer) / (2.5 * Hour));
        }

        #endregion

        #region Reciprocal

        [Test]
        public void ReciprocalValue_1()
        {
            var a = 2 * Meter;
            var b = 1 / a;
            Assert.IsTrue(a.ToString() == "2 m");
            Assert.IsTrue(b.ToString() == "0.5 [m^-1]");
        }
        
        [Test]
        public void ReciprocalValue_2()
        {
            var a = 2 * Meter;
            var b = a.Inverse;
            Assert.IsTrue(a.ToString() == "2 m");
            Assert.IsTrue(b.ToString() == "0.5 [m^-1]");
        }

        #endregion

        #region Float

        [Test]
        public void Float1()
        {
            var x = 2.5 * Meter;
            var f = (double)x;
            Assert.IsTrue(f == 2.5);
        }

        [Test]
        public void Float1const()
        {
            var x = new Constant("foo", "foo", 2.5, Meter);
            var f = (double)x;
            Assert.IsTrue(f == 2.5);
        }

        [Test]
        public void Float2()
        {
            var x = 2.5 * Centimeter;
            var f = (double)x;
            Assert.IsTrue(f == 2.5);
        }

        [Test]
        public void Float2const()
        {
            var x = new Constant("foo", "foo", 2.5, Centimeter);
            var f = (double)x;
            Assert.IsTrue(f == 2.5);
        }

        #endregion

        #region Dimensions

        [Test]
        public void Dimensions_1()
        {
            var a = 1.0 * Meter / Second;
            var b = 3.6 * Kilometer / Hour;
            Assert.IsTrue(a == b);
        }

        [Test]
        public void Dimensions_2()
        {
            var a = 80 * Centimeter + 2 * Decimeter;
            Assert.IsTrue(a.Unit == Centimeter);
            Assert.IsTrue(a.X.Numerator == 100);
            Assert.IsTrue(a.X.Denominator == 1);
        }

        [Test]
        public void Dimensions_3()
        {
            var a = 2 * Decimeter + 80 * Centimeter;
            Assert.IsTrue(a.Unit == Decimeter);
            Assert.IsTrue(a.X.Numerator == 10);
            Assert.IsTrue(a.X.Denominator == 1);
        }

        #endregion

        #region Formatting

        [Test] public void Formatting_1() => Assert.IsTrue((80 * Centimeter + 2 * Decimeter).ToString() == "100 cm");
        [Test] public void Formatting_2() => Assert.IsTrue((2 * Decimeter + 80 * Centimeter).ToString() == "10 dm");
        [Test] public void Formatting_3() => Assert.IsTrue((1 * Meter / Second).ToString() == "1 m/s");
        [Test] public void Formatting_4() => Assert.IsTrue((1 * Centimeter / Second).ToString() == "1 cm/s");
        [Test] public void Formatting_5() => Assert.IsTrue((1 * MetersPerSecond).ToString() == "1 m/s");
        [Test] public void Formatting_6() => Assert.IsTrue((3.6 * KilometersPerHour).ToString() == "3.6 km/h");
        [Test] public void Formatting_7() => Assert.IsTrue((3.6 * Kilometer / Hour).ToString() == "3.6 km/h");
        [Test] public void Formatting_8() => Assert.IsTrue((1 * International.Foot / Second).ToString() == "1 ft/s");
        [Test] public void Formatting_9() => Assert.IsTrue((3.6 * Kilogram * KilometersPerHour).ToString() == "1 [kg^1][m^1][s^-1]");

        [Test]
        public void Formatting_10()
        {
            var a = 1 * SquareMeter;
            var b = 50 * SquareDecimeter;
            var r = a + b;
            Assert.IsTrue(r.ToString() == "1.5 m²");
        }

        [Test]
        public void Formatting_11()
        {
            var a = 50 * SquareDecimeter; 
            var b = 1 * SquareMeter;
            var r = a + b;
            Assert.IsTrue(r.ToString() == "150 dm²");
        }

        [Test]
        public void Formatting_12()
        {
            var x = Decimeter * Decimeter;
            var a = 50 * Decimeter * Decimeter;
            var s = a.ToString();
            Assert.IsTrue(s == "50 dm²");
        }

        [Test]
        public void Formatting_13()
        {
            var a = (88 * Kilowatt).ConvertTo(PS);
            Assert.IsTrue(a.X.ToFloat() > 119 && a.X.ToFloat() < 120);
        }

        #endregion
    }
}
