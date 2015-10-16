using System.Threading.Tasks;

namespace PlgToFp.Windows.Module.FlightPlan.FlightPlan
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

        /// <summary>
        /// Async version of LoadPlanGFlightPlan
        /// </summary>
        Task<FlightPlanModel> LoadPlanGFlightPlanAsync(string path);
    }
}