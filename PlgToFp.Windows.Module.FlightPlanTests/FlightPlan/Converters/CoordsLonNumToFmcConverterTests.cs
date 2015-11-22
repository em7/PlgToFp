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

        #region Positive ConvertBack
        [TestMethod()]
        public void ConvertBackPositive0Test()
        {
            var coord = "E000°00.0";
            var coordNoDeg = "E00000.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0d, (double)res1, "E000°00.0 should be converted to 0d");
            Assert.AreEqual(0d, (double)res2, "E000°00.0 should be converted to 0d");
        }

        [TestMethod()]
        public void ConvertBackNullTest()
        {
            object coord = null;

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0d, (double)res1, "null should be converted to 0d");
        }

        [TestMethod()]
        public void ConvertBackEmptyTest()
        {
            object coord = "";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0d, (double)res1, "empty string should be converted to 0d");
        }

        [TestMethod()]
        public void ConvertBackPositiveSecondTest()
        {
            var coord = "E000°00.6";
            var coordNoDeg = "E00000.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0.01d, (double)res1, "E000°00.6 should be converted to 0.01d");
            Assert.AreEqual(0.01d, (double)res2, "E00000.6 should be converted to 0.01d");
        }

        [TestMethod()]
        public void ConvertBackPositiveMinuteTest()
        {
            var coord = "E000°06.0";
            var coordNoDeg = "E00006.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0.1d, (double)res1, "E000°06.0 should be converted to 0.1d");
            Assert.AreEqual(0.1d, (double)res2, "E00006.0 should be converted to 0.1d");
        }

        [TestMethod()]
        public void ConvertBackPositiveDegreeTest()
        {
            var coord = "E025°00.0";
            var coordNoDeg = "E02500.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(25d, (double)res1, "E025°00.0 should be converted to 25d");
            Assert.AreEqual(25d, (double)res2, "E02500.0 should be converted to 25d");
        }

        [TestMethod()]
        public void ConvertBackPositiveDegMinTest()
        {
            var coord = "E025°00.6";
            var coordNoDeg = "E02500.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(25.01d, (double)res1, "E025°00.6 should be converted to 25.01d");
            Assert.AreEqual(25.01d, (double)res2, "E02500.6 should be converted to 25.01d");
        }

        [TestMethod()]
        public void ConvertBackPositiveMinSecTest()
        {
            var coord = "E000°15.6";
            var coordNoDeg = "E00015.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0.26d, (double)res1, "E025°00.0 should be converted to 0.26d");
            Assert.AreEqual(0.26d, (double)res2, "E02500.0 should be converted to 0.26d");
        }

        [TestMethod()]
        public void ConvertBackPositiveCoordsest()
        {
            var coord = "E025°15.6";
            var coordNoDeg = "E02515.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(25.26d, (double)res1, "E025°15.6 should be converted to 25.26d");
            Assert.AreEqual(25.26d, (double)res2, "E02515.6 should be converted to 25.26d");
        }
        #endregion

        #region Negative ConvertBack
        [TestMethod()]
        public void ConvertBackNegative0Test()
        {
            var coord = "W000°00.0";
            var coordNoDeg = "W00000.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0d, (double)res1, "W000°00.0 should be converted to 0d");
            Assert.AreEqual(0d, (double)res2, "W000°00.0 should be converted to 0d");
        }

        [TestMethod()]
        public void ConvertBackNegativeSecondTest()
        {
            var coord = "W000°00.6";
            var coordNoDeg = "W00000.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-0.01d, (double)res1, "W000°00.6 should be converted to -0.01d");
            Assert.AreEqual(-0.01d, (double)res2, "W00000.6 should be converted to -0.01d");
        }

        [TestMethod()]
        public void ConvertBackNegativeMinuteTest()
        {
            var coord = "W000°06.0";
            var coordNoDeg = "W00006.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-0.1d, (double)res1, "W000°06.0 should be converted to -0.1d");
            Assert.AreEqual(-0.1d, (double)res2, "W00006.0 should be converted to -0.1d");
        }

        [TestMethod()]
        public void ConvertBackNegativeDegreeTest()
        {
            var coord = "W025°00.0";
            var coordNoDeg = "W02500.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-25d, (double)res1, "W025°00.0 should be converted to -25d");
            Assert.AreEqual(-25d, (double)res2, "W02500.0 should be converted to -25d");
        }

        [TestMethod()]
        public void ConvertBackNegativeDegMinTest()
        {
            var coord = "W025°00.6";
            var coordNoDeg = "W02500.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-25.01d, (double)res1, "W025°00.6 should be converted to -25.01d");
            Assert.AreEqual(-25.01d, (double)res2, "W02500.6 should be converted to -25.01d");
        }

        [TestMethod()]
        public void ConvertBackNegativeMinSecTest()
        {
            var coord = "W000°15.6";
            var coordNoDeg = "W00015.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-0.26d, (double)res1, "W025°00.0 should be converted to -0.26d");
            Assert.AreEqual(-0.26d, (double)res2, "W02500.0 should be converted to -0.26d");
        }

        [TestMethod()]
        public void ConvertBackNegativeCoordsest()
        {
            var coord = "W025°15.6";
            var coordNoDeg = "W02515.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-25.26d, (double)res1, "W025°15.6 should be converted to -25.26d");
            Assert.AreEqual(-25.26d, (double)res2, "W02515.6 should be converted to -25.26d");
        }
        #endregion

        //[TestMethod()]
        //public void ConvertBackTest()
        //{
        //    Assert.Fail();
        //}
    }
}