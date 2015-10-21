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
    public class CoordsLonNumToFmcConverterTests
    {
        private readonly CoordsLonNumToFmcConverter _converter;

        public CoordsLonNumToFmcConverterTests()
        {
            _converter = new CoordsLonNumToFmcConverter();
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
        public void ConvertPositiveCoordWholeDegreeLargeTest()
        {
            double value = 125.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("E125°00.0", result, "+125.00 should be converted to E125°00.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordWholeDegreeTest()
        {
            double value = 25.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("E025°00.0", result, "+025.00 should be converted to E025°00.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordWholeDegreeSmallTest()
        {
            double value = 5.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("E005°00.0", result, "+005.00 should be converted to E005°00.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordMinuteTest()
        {
            double value = 25.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("E025°15.0", result, "+025.25 should be converted to E025°15.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordSmallMinuteTest()
        {
            double value = 25.1;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("E025°06.0", result, "+025.1 should be converted to E025°06.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordSecondTest()
        {
            double value = 25.26;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("E025°15.6", result, "+025.26 should be converted to E025°15.6");
        }

        [TestMethod()]
        public void ConvertPositiveCoordSecondOnly()
        {
            double value = 0.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("E000°15.0", result, "+000.25 should be converted to E000°15.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordMinuteOnly()
        {
            double value = 0.01;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("E000°00.6", result, "+000.01 should be converted to E000°00.6");
        }
        #endregion

        #region Negative coords
        [TestMethod()]
        public void ConvertNegativeCoordWholeDegreeTest()
        {
            double value = -25.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("W025°00.0", result, "-025.00 should be converted to W025°00.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordWholeDegreeSmallTest()
        {
            double value = -5.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("W005°00.0", result, "-005.00 should be converted to W005°00.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordMinuteTest()
        {
            double value = -25.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("W025°15.0", result, "-025.25 should be converted to W025°15.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordSmallMinuteTest()
        {
            double value = -25.1;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("W025°06.0", result, "-025.1 should be converted to W025°06.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordSecondTest()
        {
            double value = -25.26;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("W025°15.6", result, "-025.26 should be converted to W025°15.6");
        }

        [TestMethod()]
        public void ConvertNegativeCoordSecondOnly()
        {
            double value = -0.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("W000°15.0", result, "-000.25 should be converted to W000°15.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordMinuteOnly()
        {
            double value = -0.01;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("W000°00.6", result, "-000.01 should be converted to W000°00.6");
        }
        #endregion

        //[TestMethod()]
        //public void ConvertBackTest()
        //{
        //    Assert.Fail();
        //}
    }
}