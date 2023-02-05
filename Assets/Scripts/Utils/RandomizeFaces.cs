using UnityEngine;

namespace GGJ23
{
    public class RandomizeFaces : MonoBehaviour
    {
        private MeshRenderer mesh;

        [SerializeField]
        private Material[] faces;

        private void Awake()
        {
            mesh = GetComponent<MeshRenderer>();
            mesh.material = faces[Random.Range(0, faces.Length)];
        }
    }
}
