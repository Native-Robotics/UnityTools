using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public class MeshGenerator : MonoBehaviour, IMeshGenerator
    {
        private MeshFilter MeshFilter { get; set; }
        protected Mesh Mesh { get; private set; }
        protected Vector3 Size { get; } = new(1f, 1f, 1f);

        protected void Init()
        {
            MeshFilter = gameObject.AddComponent<MeshFilter>();
            Mesh = new Mesh();
            MeshFilter.mesh = Mesh;
        }

        public void GenerateMesh() => Init();
    }
}
