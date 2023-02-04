using UnityEngine;

namespace GGJ23
{
    public static class ExtensionMethods
    {
        public static T GetGameController<T>(this GameObject gameObject)
        {
            return GameObject.FindGameObjectWithTag("GameController").GetComponent<T>();
        }
    }
}
