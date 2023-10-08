using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public class SphereDrawColliderPreview : DrawColliderPreview
    {
        protected override void DrawVolumeCollider()
        {
            Color = Color.blue;

            base.DrawVolumeCollider();

            Gizmos.color = ColorVolume;
            Gizmos.DrawSphere(LocalPosition, CalculateAverageSphereRadius(LocalScale));
            Gizmos.color = ColorWire;
            Gizmos.DrawWireSphere(LocalPosition, CalculateAverageSphereRadius(LocalScale));
        }

        private float CalculateAverageSphereRadius(Vector3 radius) => ((radius.x + radius.y + radius.z) / 3) / 2;
    }
}
