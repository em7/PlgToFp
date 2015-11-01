using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Converter;

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
            Assert.AreEqual("N25°00.0", result, "+25.00 should be converted to N25°00.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordWholeDegreeSmallTest()
        {
            double value = 5.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N05°00.0", result, "+05.00 should be converted to N05°00.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordMinuteTest()
        {
            double value = 25.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N25°15.0", result, "+25.25 should be converted to N25°15.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordSmallMinuteTest()
        {
            double value = 25.1;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N25°06.0", result, "+25.1 should be converted to N25°06.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordSecondTest()
        {
            double value = 25.26;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N25°15.6", result, "+25.26 should be converted to N25°15.6");
        }

        [TestMethod()]
        public void ConvertPositiveCoordSecondOnly()
        {
            double value = 0.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N00°15.0", result, "+00.25 should be converted to N00°15.0");
        }

        [TestMethod()]
        public void ConvertPositiveCoordMinuteOnly()
        {
            double value = 0.01;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("N00°00.6", result, "+00.01 should be converted to N00°00.6");
        }
        #endregion

        #region Negative coords
        [TestMethod()]
        public void ConvertNegativeCoordWholeDegreeTest()
        {
            double value = -25.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S25°00.0", result, "-25.00 should be converted to S25°00.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordWholeDegreeSmallTest()
        {
            double value = -5.0;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S05°00.0", result, "-05.00 should be converted to S05°00.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordMinuteTest()
        {
            double value = -25.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S25°15.0", result, "-25.25 should be converted to S25°15.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordSmallMinuteTest()
        {
            double value = -25.1;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S25°06.0", result, "-25.1 should be converted to S25°06.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordSecondTest()
        {
            double value = -25.26;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S25°15.6", result, "-25.26 should be converted to S25°15.6");
        }

        [TestMethod()]
        public void ConvertNegativeCoordSecondOnly()
        {
            double value = -0.25;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S00°15.0", result, "-00.25 should be converted to S00°15.0");
        }

        [TestMethod()]
        public void ConvertNegativeCoordMinuteOnly()
        {
            double value = -0.01;
            var result = _converter.Convert(value, typeof(string), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(result, typeof(string), "The converted value should be type of string");
            Assert.AreEqual("S00°00.6", result, "-00.01 should be converted to S00°00.6");
        }
        #endregion

        //#region ConvertBack
        //public void ConvertBack0Test()
        //{
        //    var coord = "N00°00.0";
        //}
        //#endregion

        //[TestMethod()]
        //public void ConvertBackTest()
        //{
        //    Assert.Fail();
        //}
    }
}