using Sirenix.OdinInspector;
using UnityEngine;

namespace NativeRobotics.Utils.RobotView
{
    public class MeshRobotView : MonoBehaviour, IRobotView
    {
        [Header("References"), SerializeField]
        private Transform[] joints = new Transform[0];

        [SerializeField]
        private int[] signs = new int[0];

        [SerializeField]
        private float[] offsets = new float[0];

        [SerializeField]
        private float smoothingFactor = 1.0f;

        [Header("Debug"), SerializeField, Range(-Mathf.PI, Mathf.PI), OnValueChanged("DebugStateChanged", true)]
        public float[] debugState;

        private void DebugStateChanged()
        {
            if (debugState.Length == joints.Length)
            {
                UpdateView(debugState);
            }
        }

        public void Init(Transform[] joints, int[] signs, float[] offsets)
        {
            this.joints = joints;
            this.signs = signs;
            this.offsets = offsets;

            debugState = new float[joints.Length];

            for (var i = 0; i < joints.Length; i++)
            {
                signs[i] = 1;
                offsets[i] = 0;
                debugState[i] = 0;
            }
        }

        public void UpdateView(float[] state)
        {
            for (var i = 0; i < joints.Length; i++)
            {
                var joint = joints[i];
                var targetAngle = state[i] * signs[i] * Mathf.Rad2Deg + offsets[i];
                var displayedAngle = Mathf.LerpAngle(joint.localEulerAngles.y, targetAngle, smoothingFactor);
                joint.localEulerAngles = new Vector3(0.0f, displayedAngle, 0.0f);
            }
        }
    }
}