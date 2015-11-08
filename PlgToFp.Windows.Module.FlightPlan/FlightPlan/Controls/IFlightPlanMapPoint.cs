using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Controls
{
    /// <summary>
    /// An interface for flight plan points to be displayed in FlightPlanMap
    /// </summary>
    public interface IFlightPlanMapPoint
    {
        double Longitude { get; }

        double Latitude { get; }
    }
}
