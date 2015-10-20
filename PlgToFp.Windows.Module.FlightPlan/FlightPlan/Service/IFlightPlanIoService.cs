using System.Threading.Tasks;
using PlgToFp.Windows.Module.FlightPlan.FlightPlan.Model;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan.Service
{
    /// <summary>
    /// Service for importing/exporting flight plans
    /// </summary>
    public interface IFlightPlanIoService
    {
        /// <summary>
        /// Loads the Plan-G flight plan
        /// </summary>
        /// <param name="path">path to file</param>
        /// <returns>loaded flight plan</returns>
        /// <exception cref="ArgumentNullException">path is null</exception>
        FlightPlanModel LoadPlanGFlightPlan(string path);
    }
}