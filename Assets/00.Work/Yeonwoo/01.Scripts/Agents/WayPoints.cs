using UnityEngine;

namespace _00.Work.Yeonwoo._01.Scripts.Agents
{
    public class WayPoints : MonoBehaviour
    {
        [SerializeField] private WayPoint[]  wayPoints;

        private int _currentIndex;

        public Vector3 GetNextWayPoint()
        {
            Debug.Assert(wayPoints.Length >= 1, "There must be at least one waypoint");
            _currentIndex = (_currentIndex + 1)  % wayPoints.Length;
            return wayPoints[_currentIndex].Position;
        }
    }
}