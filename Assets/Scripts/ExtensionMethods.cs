using UnityEngine;

namespace GGJ23
{
    public static class ExtensionMethods
    {
        public static T GetGameController<T>(this GameObject gameObject)
        {
            return GameObject.FindGameObjectWithTag("GameController").GetComponent<T>();
        }

        public static T GetPlayer<T>(this GameObject gameObject)
        {
            return GameObject.FindGameObjectWithTag("Player").GetComponent<T>();
        }
    }
}
