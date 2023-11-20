using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public class SphereMeshGenerator : MeshGenerator
    {
        public override void GenerateMesh()
        {
            Init();

            var radius = ((Size.x + Size.y + Size.z) / 3) / 2;
            var latitudeSegments = 6;
            var longitudeSegments = 6;

            var numVertices = (latitudeSegments + 1) * (longitudeSegments + 1);
            var vertices = new Vector3[numVertices];
            var triangles = new int[latitudeSegments * longitudeSegments * 6];

            var vertexIndex = 0;
            var triangleIndex = 0;

            for (var lat = 0; lat <= latitudeSegments; lat++)
            {
                var theta = lat * Mathf.PI / latitudeSegments;
                var sinTheta = Mathf.Sin(theta);
                var cosTheta = Mathf.Cos(theta);

                for (var lon = 0; lon <= longitudeSegments; lon++)
                {
                    var phi = lon * 2 * Mathf.PI / longitudeSegments;
                    var sinPhi = Mathf.Sin(phi);
                    var cosPhi = Mathf.Cos(phi);

                    var x = cosPhi * sinTheta;
                    var y = cosTheta;
                    var z = sinPhi * sinTheta;

                    vertices[vertexIndex] = new Vector3(x, y, z) * radius;

                    if (lat < latitudeSegments && lon < longitudeSegments)
                    {
                        var nextVertex = vertexIndex + longitudeSegments + 1;
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
