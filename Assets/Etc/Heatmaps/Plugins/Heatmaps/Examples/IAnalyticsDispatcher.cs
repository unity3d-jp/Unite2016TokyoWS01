/// <summary>
/// A possible interface for any Analytics dispatcher
/// </summary>
using UnityEngine.EventSystems;

namespace UnityAnalyticsHeatmap
{
	public interface IAnalyticsDispatcher : IEventSystemHandler
    {

        void DisableAnalytics();

        void EnableAnalytics();
    }
}
