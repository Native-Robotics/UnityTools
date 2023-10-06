using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public class DrawColliderPreview : MonoBehaviour
    {
        private Color Color { get; } = Color.green;
        private readonly float _colorAlpha = 0.1f;
        protected Color ColorVolume { get; private set; }
        protected Color ColorWire { get; private set; }
        protected Vector3 LocalPosition { get; private set; }
        protected Vector3 LocalScale { get; private set; }
        private void OnDrawGizmos() => DrawVolumeCollider();

        protected virtual void DrawVolumeCollider()
        {
            var transforms = transform;

            ColorVolume = new Color(Color.r, Color.g, Color.b, _colorAlpha);
            ColorWire = Color;
            LocalPosition = transforms.localPosition;
            LocalScale = transforms.localScale;
        }
    }
}
