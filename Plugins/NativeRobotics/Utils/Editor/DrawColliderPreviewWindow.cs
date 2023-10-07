using System.Collections.Generic;
using NativeRobotics.Utils.ColliderPreview;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace NativeRobotics.Utils.Editor
{
    public class DrawColliderPreviewWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Native Robotics/Draw Collider Preview")]
        private static void OpenWindow() => GetWindow<DrawColliderPreviewWindow>().Show();

        [PropertyOrder(0)]
        [LabelText("Clear collider preview")]
        [Button(ButtonSizes.Large), GUIColor(1f, 0.27f, 0.22f)]
        private void OnClearButtonClicked()
        {
            ClearColliderPreview(colliderCube);
            ClearColliderPreview(colliderSphere);
        }

        [PropertyOrder(1)]
        [LabelText("Cube collider")]
        [GUIColor(1f, 1f, 1f)]
        [SerializeField] private List<GameObject> colliderCube;

        [PropertyOrder(2)]
        [LabelText("Cube collider preview")]
        [HorizontalGroup("Cube", 0.9f)]
        [Button(ButtonSizes.Large), GUIColor(0.18f, 0.81f, 0.34f)]
        private void OnCubeColliderPreviewButtonClicked() => DrawColliderPreviewCube(colliderCube);

        [PropertyOrder(3)]
        [LabelText("X")]
        [HorizontalGroup("Cube", 0.1f)]
        [Button(ButtonSizes.Large), GUIColor(1f, 0.27f, 0.22f)]
        private void OnClearCubeColliderPreviewButtonClicked() => ClearColliderPreview(colliderCube);

        [PropertyOrder(4)]
        [LabelText("Sphere collider")]
        [GUIColor(1f, 1f, 1f)]
        [SerializeField] private List<GameObject> colliderSphere;

        [PropertyOrder(5)]
        [LabelText("Sphere collider preview")]
        [HorizontalGroup("Sphere", 0.9f)]
        [Button(ButtonSizes.Large), GUIColor(0.18f, 0.81f, 0.34f)]
        private void OnSphereColliderPreviewButtonClicked() => DrawColliderPreviewSphere(colliderSphere);

        [PropertyOrder(6)]
        [LabelText("X")]
        [HorizontalGroup("Sphere", 0.1f)]
        [Button(ButtonSizes.Large), GUIColor(1f, 0.27f, 0.22f)]
        private void OnClearSphereColliderPreviewButtonClicked() => ClearColliderPreview(colliderSphere);

        private void DrawColliderPreviewCube(List<GameObject> items)
        {
            ClearColliderPreview(colliderCube);

            foreach (var item in items)
            {
                if (!item.GetComponent<DrawColliderPreviewCube>())
                {
                    item.AddComponent<DrawColliderPreviewCube>();
                    item.AddComponent<MeshGeneratorCube>();
                    item.GetComponent<MeshGeneratorCube>().GenerateMesh();
                }
            }
        }

        private void DrawColliderPreviewSphere(List<GameObject> items)
        {
            ClearColliderPreview(colliderSphere);

            foreach (var item in items)
            {
                if (!item.GetComponent<DrawColliderPreviewSphere>())
                {
                    item.AddComponent<DrawColliderPreviewSphere>();
                    item.AddComponent<MeshGeneratorSphere>();
                    item.GetComponent<MeshGeneratorSphere>().GenerateMesh();
                }
            }
        }

        private void ClearColliderPreview(List<GameObject> items)
        {
            foreach (var item in items)
            {
                if (item.GetComponent<DrawColliderPreviewCube>())
                    DestroyImmediate(item.GetComponent<DrawColliderPreviewCube>());
                if (item.GetComponent<DrawColliderPreviewSphere>())
                    DestroyImmediate(item.GetComponent<DrawColliderPreviewSphere>());
                if (item.GetComponent<MeshGeneratorCube>())
                    DestroyImmediate(item.GetComponent<MeshGeneratorCube>());
                if (item.GetComponent<MeshGeneratorSphere>())
                    DestroyImmediate(item.GetComponent<MeshGeneratorSphere>());
                if (item.GetComponent<MeshFilter>())
                    DestroyImmediate(item.GetComponent<MeshFilter>());
            }
        }
    }
}
