using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public class DrawColliderPreviewCube : DrawColliderPreview
    {
        protected override void DrawVolumeCollider()
        {
            base.DrawVolumeCollider();

            Gizmos.color = ColorWire;
            Gizmos.DrawWireCube(LocalPosition, LocalScale);
            Gizmos.color = ColorVolume;
            Gizmos.DrawCube(LocalPosition, LocalScale);
        }
    }
}
