using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public class MeshGeneratorPreviewCube : MeshGeneratorPreview
    {
        protected override void GenerateMesh()
        {
            base.GenerateMesh();

            var size = Size;

            Vector3[] vertices =
            {
                new Vector3(-size.x / 2, -size.y / 2, -size.z / 2),
                new Vector3(size.x / 2, -size.y / 2, -size.z / 2),
                new Vector3(size.x / 2, -size.y / 2, size.z / 2),
                new Vector3(-size.x / 2, -size.y / 2, size.z / 2),
                new Vector3(-size.x / 2, size.y / 2, -size.z / 2),
                new Vector3(size.x / 2, size.y / 2, -size.z / 2),
                new Vector3(size.x / 2, size.y / 2, size.z / 2),
                new Vector3(-size.x / 2, size.y / 2, size.z / 2),
            };

            int[] triangles =
            {
                0, 2, 1, 0, 3, 2,
                4, 5, 6, 4, 6, 7,
                0, 1, 5, 0, 5, 4,
                1, 2, 6, 1, 6, 5,
                2, 3, 7, 2, 7, 6,
                3, 0, 4, 3, 4, 7
            };

            Mesh.vertices = vertices;
            Mesh.triangles = triangles;
        }
    }
}
