using UnityEngine;

namespace NativeRobotics.Utils.Rigging
{
    public class RodBellow : MonoBehaviour
    {
        [Header("Transforms")]
        public Transform a;
        public Transform b;
        
        [Header("Distances")]
        public float distanceConstant = 0.1036497f;
        public Vector3 stretchAxis = Vector3.one;

        private float initialDistance;
        private float currentDistance;

        private Vector3 prevPosA = Vector3.zero;
        private Vector3 prevPosB = Vector3.zero;
        private Vector3 constantScale = Vector3.one;

        private void Awake()
        {
            initialDistance = Vector3.Distance(a.position, b.position) - distanceConstant;
            constantScale = Vector3.Scale(Vector3.one, Vector3.one - stretchAxis);
        }

        private void Update()
        {
            if (a.position == prevPosA && b.position == prevPosB) return;

            var positionA = a.position;
            var positionB = b.position;

            currentDistance = Vector3.Distance(positionA, positionB);
            var scaleFactor = (currentDistance - distanceConstant) / initialDistance;

            transform.localScale = constantScale + stretchAxis * scaleFactor;
            
            prevPosA = positionA;
            prevPosB = positionB;
        }
    }
}
