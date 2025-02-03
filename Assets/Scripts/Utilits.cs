using UnityEngine;

namespace DefaultNamespace
{
    public class Utilits : MonoBehaviour
    {
        public static Vector3 ScreenPointToWorldPoint(Vector3 screenPoint, Camera camera)
        {
            screenPoint.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(screenPoint);
        }
    }
}