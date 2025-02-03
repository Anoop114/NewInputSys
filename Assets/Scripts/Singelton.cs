using UnityEngine;

public class Singelton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = (T)FindObjectOfType(typeof(T));
            if (_instance != null) return _instance;
            var obj = new GameObject
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            _instance = obj.AddComponent<T>();
            return _instance;
        }
    }
}