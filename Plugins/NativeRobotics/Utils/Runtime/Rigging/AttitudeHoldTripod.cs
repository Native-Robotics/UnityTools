using System;
using UnityEngine;

namespace NativeRobotics.Utils.Rigging
{
    public class AttitudeHoldTripod : MonoBehaviour
    {
        [Header("Reference settings")] 
        public Transform reference;

        [Header("Tripod settings")]
        public int sign;

        public float offset;

        private void Update()
        {
            reference.localRotation.ToAngleAxis(out var angle, out var axis);
            transform.localRotation = Quaternion.AngleAxis(sign * (angle + offset), axis);
        }
    }
}