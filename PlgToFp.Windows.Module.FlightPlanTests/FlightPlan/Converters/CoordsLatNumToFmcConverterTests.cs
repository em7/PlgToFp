using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Converters.Tests
{
    [TestClass()]
    public class CoordsLatNumToFmcConverterTests
    {
        private readonly CoordsLatNumToFmcConverter _converter;

        public CoordsLatNumToFmcConverterTests()
        {
            _converter = new CoordsLatNumToFmcConverter();
        }

        #region Positive coords
        [TestMethod()]
        public void ConvertNonDoubleTest()
        {
            var value = new Object();
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsNull(result, "The result when converting non double value should be null.");
        }

        [TestMethod()]
        public void ConvertPositiveCoordWholeDegreeTest()
        {
            double value = 25.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N2500.0", result, "+25.00 should be converted to N2500.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordWholeDegreeSmallTest()
        {
            double value = 5.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N0500.0", result, "+05.00 should be converted to N0500.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordMinuteTest()
        {
            double value = 25.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N2515.0", result, "+25.25 should be converted to N2515.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordSmallMinuteTest()
        {
            double value = 25.1;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N2506.0", result, "+25.1 should be converted to N2506.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordSecondTest()
        {
            double value = 25.26;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N2515.6", result, "+25.26 should be converted to N2515.6");
        }

        [TestMethod()]
        public void ConvertPositiveCoordSecondOnly()
        {
            double value = 0.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N0015.0", result, "+0.25 should be converted to N0015.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordMinuteOnly()
        {
            double value = 0.01;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N0000.6", result, "+0.01 should be converted to N0000.6");
        }
        #endregion

        #region Negative coords
        [TestMethod()]
        public void ConvertNegativeCoordWholeDegreeTest()
        {
            double value = -25.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S2500.0", result, "-25.00 should be converted to S2500.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordWholeDegreeSmallTest()
        {
            double value = -5.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S0500.0", result, "-05.00 should be converted to S0500.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordMinuteTest()
        {
            double value = -25.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S2515.0", result, "-25.25 should be converted to S2515.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordSmallMinuteTest()
        {
            double value = -25.1;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S2506.0", result, "-25.1 should be converted to S2506.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordSecondTest()
        {
            double value = -25.26;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S2515.6", result, "-25.26 should be converted to S2515.6");
        }

        [TestMethod()]
        public void ConvertNegativeCoordSecondOnly()
        {
            double value = -0.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S0015.0", result, "-0.25 should be converted to S0015.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordMinuteOnly()
        {
            double value = -0.01;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S0000.6", result, "-0.01 should be converted to S0000.6");
        }
        #endregion

        //[TestMethod()]
        //public void ConvertBackTest()
        //{
        //    Assert.Fail();
        //}
    }
}