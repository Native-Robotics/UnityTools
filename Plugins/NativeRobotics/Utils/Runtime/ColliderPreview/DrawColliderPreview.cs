using UnityEngine;

namespace NativeRobotics.Utils.ColliderPreview
{
    public abstract class DrawColliderPreview : MonoBehaviour
    {
        protected Color Color { get; set; } = Color.green;
        private bool IsDrawGizmo { get; set; }
        private readonly float _colorAlpha = 0.1f;
        protected Color ColorVolume { get; private set; }
        protected Color ColorWire { get; private set; }
        protected Vector3 LocalPosition { get; private set; }
        protected Vector3 LocalScale { get; private set; }

        private void OnDrawGizmos()
        {
            if (IsDrawGizmo)
                DrawVolumeCollider();
        }

        protected void Init()
        {
            var transforms = transform;

            ColorVolume = new Color(Color.r, Color.g, Color.b, _colorAlpha);
            ColorWire = Color;
            LocalPosition = transforms.localPosition;
            LocalScale = transforms.localScale;
        }

        protected abstract void DrawVolumeCollider();

        public void DrawGizmo() => IsDrawGizmo = true;
    }
}
