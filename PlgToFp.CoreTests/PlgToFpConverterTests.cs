using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlpToFp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FlpToFp.Core.Tests
{
    [TestClass()]
    public class PlgToFpConverterTests
    {
        [TestMethod()]
        public void ConvertLkmtLktbTest()
        {
            var plg = XDocument.Load(@"./plg/lkmtlktb.plg");
            var fp = XDocument.Load(@"./fp/lkmtlktb.fp"); ;
            var converter = new PlgToFpConverter();
            var fpConverted = converter.Convert(plg);
            Assert.IsNotNull(fpConverted, "The converted flight plan should not be null");
            Assert.IsTrue(XNode.DeepEquals(fpConverted.FirstNode, fp.FirstNode), "The converted flight plan should be correct");
        }
    }
}