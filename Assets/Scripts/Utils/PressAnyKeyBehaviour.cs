using UnityEngine;
using UnityEngine.Events;

namespace GGJ23
{
    public class PressAnyKeyBehaviour : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onKeyPressBehaviour;

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                onKeyPressBehaviour.Invoke();
            }
        }
    }
}
