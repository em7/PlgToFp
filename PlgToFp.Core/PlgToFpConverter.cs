using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FlpToFp.Core
{
    /// <summary>
    /// Converter for Plan-G (plg) format to iFMS (fp) format
    /// </summary>
    public class PlgToFpConverter
    {
        

        /// <summary>
        /// Converts the flight plans in memory
        /// </summary>
        /// <param name="plg">the plg format loaded in memory</param>
        /// <returns>the fp format in memory</returns>
        /// <exception cref="ArgumentNullException">plg argument null</exception>
        /// <exception cref="FlightPlanConvertException">convertion fails, usually due to bad input data</exception>
        public XDocument Convert(XDocument plg)
        {
            if (plg == null) throw new ArgumentNullException("plg");

            var plgParser = new PlgParser();
            var flightPlan = plgParser.ParsePlg(plg);
            
            return null;
        }

        
    }
}
