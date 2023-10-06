using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public class MeshGeneratorPreview : MonoBehaviour
    {
        private MeshFilter MeshFilter { get; set; }
        protected Mesh Mesh { get; private set; }
        protected Vector3 Size { get; } = new(1f, 1f, 1f);
        private void Reset() => GenerateMesh();

        protected virtual void GenerateMesh()
        {
            MeshFilter = gameObject.AddComponent<MeshFilter>();
            Mesh = new Mesh();
            MeshFilter.mesh = Mesh;
        }
    }
}
