using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityTools.Plugins.NativeRobotics.Utils.Runtime;

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
        private JointType[] types =
        {
            JointType.Revolute, JointType.Revolute, JointType.Revolute,
            JointType.Revolute, JointType.Revolute, JointType.Revolute
        };

        [SerializeField]
        private float smoothingFactor = 1.0f;

        [Header("Debug"), SerializeField, Range(-Mathf.PI, Mathf.PI), OnValueChanged("DebugStateChanged", true)]
        public float[] debugState;

        protected void DebugStateChanged()
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
        }

        public virtual void UpdateView(float[] state)
        {
            for (var i = 0; i < joints.Length; i++)
            {
                var joint = joints[i];
                switch (types[i])
                {
                    case JointType.Revolute:
                        var targetAngle = state[i] * signs[i] * Mathf.Rad2Deg + offsets[i];
                        var displayedAngle = Mathf.LerpAngle(joint.localEulerAngles.y, targetAngle, smoothingFactor);
                        joint.localEulerAngles = new Vector3(0.0f, displayedAngle, 0.0f);
                        break;
                    case JointType.Prismatic:
                        var targetPosition = state[i] * signs[i] + offsets[i];
                        var displayedPosition = Mathf.Lerp(joint.localPosition.y, targetPosition, smoothingFactor);
                        joint.localPosition = new Vector3(0.0f, displayedPosition, 0.0f);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}