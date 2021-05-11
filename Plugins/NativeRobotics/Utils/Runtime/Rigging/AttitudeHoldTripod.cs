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

        private Quaternion prevReferenceLocalRotation = Quaternion.identity;

        private void Update()
        {
            var referenceLocalRotation = reference.localRotation;
            if (prevReferenceLocalRotation == referenceLocalRotation) return;

            referenceLocalRotation.ToAngleAxis(out var referenceAngle, out var referenceAxis);
            transform.localRotation = Quaternion.AngleAxis(sign * (referenceAngle + offset), referenceAxis);

            prevReferenceLocalRotation = referenceLocalRotation;
        }
    }
}