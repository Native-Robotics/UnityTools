using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public class CubeDrawColliderPreview : DrawColliderPreview
    {
        protected override void DrawVolumeCollider()
        {
            Init();

            Gizmos.color = ColorWire;
            Gizmos.DrawWireCube(LocalPosition, LocalScale);
            Gizmos.color = ColorVolume;
            Gizmos.DrawCube(LocalPosition, LocalScale);
        }
    }
}
