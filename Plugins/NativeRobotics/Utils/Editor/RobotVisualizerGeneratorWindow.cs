using System;
using System.IO;
using NativeRobotics.Utils.RobotView;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace NativeRobotics.Utils.Editor
{
    public class RobotVisualizerGeneratorWindow : RobotGeneratorWindow
    {
        [MenuItem("Tools/Native Robotics/Robot Importer/Visualizer Prefab Generator")]
        private static void OpenWindow()
        {
            var window = GetWindow<RobotVisualizerGeneratorWindow>();
            window.Show();
        }

        [PreviewField(ObjectFieldAlignment.Left, Height = 100), LabelText("Robot mesh")]
        public GameObject robotMesh;

        [Button, PropertyOrder(-1)]
        public virtual void SetSavingPathAsMeshPath()
        {
            if (robotMesh == null) return;

            var assetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(robotMesh);
            savingPath = Path.GetDirectoryName(assetPath);
        }

        [Button(ButtonSizes.Large), PropertySpace(SpaceBefore = 20)]
        private void BuildVisualizerPrefab()
        {
            try
            {
                var robotMeshInstance = Instantiate(robotMesh, Vector3.zero, Quaternion.identity);
                var joints = RobotGeneratorMeshProcessor.ProcessMeshFBX(robotMeshInstance.transform, dof);
                
                // Adjusting prefab name
                var meshName = robotMeshInstance.name;

                if (meshName.EndsWith("(Clone)"))
                {
                    meshName = meshName.Substring(0, meshName.LastIndexOf("(Clone)", StringComparison.Ordinal));
                }

                meshName = meshName.Replace(' ', '_');
                meshName += "_Visualizer";
                
                robotMeshInstance.name = meshName;

                // Adding Mesh Robot View component
                var view = robotMeshInstance.AddComponent<MeshRobotView>();
                view.Init(joints.ToArray(), new int[dof], new float[dof]);
                
                // var visualizer = robotMeshInstance.AddComponent<RobotVisualizer>();
                // visualizer.dof = dof;
                // visualizer.joints = joints.Count == 0 ? new Transform[dof] : joints.ToArray();
                // visualizer.offsets = new float[dof];
                // visualizer.signs = new int[dof];
                // visualizer.useMeshRobot = true;
                // visualizer.useStabRobot = false;

                SavePrefab(robotMeshInstance);
            }
            catch
            {
                Debug.LogError("Can't process provided FBX model. Check hierarchy and naming.");
            }
        }
    }
}