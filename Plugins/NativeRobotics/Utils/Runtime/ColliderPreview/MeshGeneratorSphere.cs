using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public class MeshGeneratorSphere : MeshGenerator, IMeshGenerator
    {
        public void GenerateMesh()
        {
            Init();

            var radius = ((Size.x + Size.y + Size.z) / 3) / 2;
            var latitudeSegments = 16;
            var longitudeSegments = 16;

            int numVertices = (latitudeSegments + 1) * (longitudeSegments + 1);
            Vector3[] vertices = new Vector3[numVertices];
            int[] triangles = new int[latitudeSegments * longitudeSegments * 6];

            int vertexIndex = 0;
            int triangleIndex = 0;

            for (int lat = 0; lat <= latitudeSegments; lat++)
            {
                float theta = lat * Mathf.PI / latitudeSegments;
                float sinTheta = Mathf.Sin(theta);
                float cosTheta = Mathf.Cos(theta);

                for (int lon = 0; lon <= longitudeSegments; lon++)
                {
                    float phi = lon * 2 * Mathf.PI / longitudeSegments;
                    float sinPhi = Mathf.Sin(phi);
                    float cosPhi = Mathf.Cos(phi);

                    float x = cosPhi * sinTheta;
                    float y = cosTheta;
                    float z = sinPhi * sinTheta;

                    vertices[vertexIndex] = new Vector3(x, y, z) * radius;

                    if (lat < latitudeSegments && lon < longitudeSegments)
                    {
                        int nextVertex = vertexIndex + longitudeSegments + 1;
                        triangles[triangleIndex++] = vertexIndex;
                        triangles[triangleIndex++] = nextVertex + 1;
                        triangles[triangleIndex++] = nextVertex;
                        triangles[triangleIndex++] = vertexIndex;
                        triangles[triangleIndex++] = vertexIndex + 1;
                        triangles[triangleIndex++] = nextVertex + 1;
                    }

                    vertexIndex++;
                }
            }

            Mesh.vertices = vertices;
            Mesh.triangles = triangles;
        }
    }
}
