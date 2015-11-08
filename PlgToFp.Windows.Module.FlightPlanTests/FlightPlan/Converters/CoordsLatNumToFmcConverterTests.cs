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

        #region Positive ConvertBack
        [TestMethod()]
        public void ConvertBackPositive0Test()
        {
            var coord = "N00°00.0";
            var coordNoDeg = "N0000.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0d, (double)res1, "N00°00.0 should be converted to 0d");
            Assert.AreEqual(0d, (double)res2, "N00°00.0 should be converted to 0d");
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
            var coord = "N00°00.6";
            var coordNoDeg = "N0000.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0.01d, (double)res1, "N00°00.6 should be converted to 0.01d");
            Assert.AreEqual(0.01d, (double)res2, "N0000.6 should be converted to 0.01d");
        }

        [TestMethod()]
        public void ConvertBackPositiveMinuteTest()
        {
            var coord = "N00°06.0";
            var coordNoDeg = "N0006.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0.1d, (double)res1, "N00°06.0 should be converted to 0.1d");
            Assert.AreEqual(0.1d, (double)res2, "N0006.0 should be converted to 0.1d");
        }

        [TestMethod()]
        public void ConvertBackPositiveDegreeTest()
        {
            var coord = "N25°00.0";
            var coordNoDeg = "N2500.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(25d, (double)res1, "N25°00.0 should be converted to 25d");
            Assert.AreEqual(25d, (double)res2, "N2500.0 should be converted to 25d");
        }

        [TestMethod()]
        public void ConvertBackPositiveDegMinTest()
        {
            var coord = "N25°00.6";
            var coordNoDeg = "N2500.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(25.01d, (double)res1, "N25°00.6 should be converted to 25.01d");
            Assert.AreEqual(25.01d, (double)res2, "N2500.6 should be converted to 25.01d");
        }

        [TestMethod()]
        public void ConvertBackPositiveMinSecTest()
        {
            var coord = "N00°15.6";
            var coordNoDeg = "N0015.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0.26d, (double)res1, "N25°00.0 should be converted to 0.26d");
            Assert.AreEqual(0.26d, (double)res2, "N2500.0 should be converted to 0.26d");
        }

        [TestMethod()]
        public void ConvertBackPositiveCoordsest()
        {
            var coord = "N25°15.6";
            var coordNoDeg = "N2515.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(25.26d, (double)res1, "N25°15.6 should be converted to 25.26d");
            Assert.AreEqual(25.26d, (double)res2, "N2515.6 should be converted to 25.26d");
        }
        #endregion

        #region Negative ConvertBack
        [TestMethod()]
        public void ConvertBackNegative0Test()
        {
            var coord = "S00°00.0";
            var coordNoDeg = "S0000.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(0d, (double)res1, "S00°00.0 should be converted to 0d");
            Assert.AreEqual(0d, (double)res2, "S00°00.0 should be converted to 0d");
        }

        [TestMethod()]
        public void ConvertBackNegativeSecondTest()
        {
            var coord = "S00°00.6";
            var coordNoDeg = "S0000.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-0.01d, (double)res1, "S00°00.6 should be converted to -0.01d");
            Assert.AreEqual(-0.01d, (double)res2, "S0000.6 should be converted to -0.01d");
        }

        [TestMethod()]
        public void ConvertBackNegativeMinuteTest()
        {
            var coord = "S00°06.0";
            var coordNoDeg = "S0006.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-0.1d, (double)res1, "S00°06.0 should be converted to -0.1d");
            Assert.AreEqual(-0.1d, (double)res2, "S0006.0 should be converted to -0.1d");
        }

        [TestMethod()]
        public void ConvertBackNegativeDegreeTest()
        {
            var coord = "S25°00.0";
            var coordNoDeg = "S2500.0";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-25d, (double)res1, "S25°00.0 should be converted to -25d");
            Assert.AreEqual(-25d, (double)res2, "S2500.0 should be converted to -25d");
        }

        [TestMethod()]
        public void ConvertBackNegativeDegMinTest()
        {
            var coord = "S25°00.6";
            var coordNoDeg = "S2500.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-25.01d, (double)res1, "S25°00.6 should be converted to -25.01d");
            Assert.AreEqual(-25.01d, (double)res2, "S2500.6 should be converted to -25.01d");
        }

        [TestMethod()]
        public void ConvertBackNegativeMinSecTest()
        {
            var coord = "S00°15.6";
            var coordNoDeg = "S0015.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-0.26d, (double)res1, "S25°00.0 should be converted to -0.26d");
            Assert.AreEqual(-0.26d, (double)res2, "S2500.0 should be converted to -0.26d");
        }

        [TestMethod()]
        public void ConvertBackNegativeCoordsest()
        {
            var coord = "S25°15.6";
            var coordNoDeg = "S2515.6";

            var res1 = _converter.ConvertBack(coord, typeof(double), null, CultureInfo.InvariantCulture);
            var res2 = _converter.ConvertBack(coordNoDeg, typeof(double), null, CultureInfo.InvariantCulture);

            Assert.IsInstanceOfType(res1, typeof(double), "The converted value should be type of double");
            Assert.IsInstanceOfType(res2, typeof(double), "The converted value should be type of double");

            Assert.AreEqual(-25.26d, (double)res1, "S25°15.6 should be converted to -25.26d");
            Assert.AreEqual(-25.26d, (double)res2, "s2515.6 should be converted to -25.26d");
        }
        #endregion
        //[TestMethod()]
        //public void ConvertBackTest()
        //{
        //    Assert.Fail();
        //}
    }
}