using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NativeRobotics.Utils.Editor
{
    public enum RobotMeshType
    {
        FBX,
        DAE,
        Joints,
    }

    public enum Axis
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Backward
    }
    
    public static class RobotGeneratorMeshProcessor
    {
        public static List<Transform> ProcessMeshFBX(Transform meshRoot, int dof)
        {
            var joints = new List<Transform>(dof);

            for (var i = 0; i < dof; i++)
            {
                var jName = $"J{i + 1}";
                var j = FindChildByName(meshRoot, jName);

                if (j is null)
                {
                    jName = $"j{i + 1}";
                    j = FindChildByName(meshRoot, jName);
                }

                if (j is null)
                {
                    Debug.Log("Can't create joint parent transform");
                    break;
                }

                var jt = new GameObject($"JT{i + 1}").GetComponent<Transform>();

                jt.parent = j;
                jt.localPosition = Vector3.zero;
                jt.localRotation = Quaternion.identity;
                jt.localScale = Vector3.one;

                jt.parent = j.parent;
                j.parent = jt;

                joints.Add(j);
            }

            return joints;
        }

        public static void ProcessMeshDAE(Transform meshRoot, Vector3 baseRotation, Axis[] jointAxis, int dof,
            string name)
        {
            var baseJoint = FindChildByNamePart(meshRoot, "link");
            baseJoint.name = "Base";

            var jt = new GameObject($"Base{name}-Root").GetComponent<Transform>();

            jt.parent = baseJoint;
            jt.localPosition = Vector3.zero;
            jt.localRotation = Quaternion.identity;

            jt.parent = baseJoint.parent;
            baseJoint.parent = jt;
            baseJoint.rotation = Quaternion.Euler(baseRotation);

            var j = baseJoint;

            for (var i = 0; i < dof; i++)
            {
                j = FindChildByNamePart(j, "link");

                var jtRoot = new GameObject($"JT{i + 1}").GetComponent<Transform>();
                jt = new GameObject($"J{i + 1}").GetComponent<Transform>();

                jt.parent = jtRoot;

                jtRoot.parent = j;
                jtRoot.localPosition = Vector3.zero;
                jtRoot.localRotation = Quaternion.identity;
                jtRoot.localScale = Vector3.one;

                jtRoot.parent = j.parent;
                jtRoot.rotation = AxisToRotation(jointAxis[i]);

                j.parent = jt;
            }
        }

        private static Quaternion AxisToRotation(Axis a)
        {
            switch (a)
            {
                case Axis.Up:
                    return Quaternion.Euler(0, 0, 0);
                case Axis.Down:
                    return Quaternion.Euler(180, 0, 0);
                case Axis.Right:
                    return Quaternion.Euler(0, 0, -90);
                case Axis.Left:
                    return Quaternion.Euler(0, 0, 90);
                case Axis.Forward:
                    return Quaternion.Euler(90, 0, 0);
                case Axis.Backward:
                    return Quaternion.Euler(-90, 0, 0);
                default:
                    return Quaternion.identity;
            }
        }

        private static Transform FindChildByName(Transform parent, string name)
        {
            return parent.GetComponentsInChildren<Transform>().FirstOrDefault(t =>
                string.Equals(t.name, name, StringComparison.CurrentCultureIgnoreCase) && t != parent);
        }

        private static Transform FindChildByNamePart(Transform parent, string namePart)
        {
            return parent.GetComponentsInChildren<Transform>()
                .FirstOrDefault(t => t.name.StartsWith(namePart) && t != parent);
        }
    }
}