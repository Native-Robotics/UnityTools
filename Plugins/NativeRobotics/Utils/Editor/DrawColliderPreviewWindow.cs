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
        private const string TheListIsEmptyMessage = "The list is empty";
        private const string HasNullMessage = "Has null";
        private const string CubeListMessage = ", please add Cube colliders and try it again";
        private const string SphereListMessage = ", please add Sphere colliders and try it again";

        [MenuItem("Tools/Native Robotics/Draw Collider Preview")]
        private static void OpenWindow() => GetWindow<DrawColliderPreviewWindow>().Show();

        [PropertyOrder(0)]
        [LabelText("Clear all collider preview")]
        [Button(ButtonSizes.Large), GUIColor(1f, 0.27f, 0.22f)]
        private void OnClearButtonClicked()
        {
            ClearColliderPreview(colliderCube);
            ClearColliderPreview(colliderSphere);
        }

        [PropertyOrder(1)]
        [HideLabel]
        [DisplayAsString]
        [GUIColor(1f, 1f, 0f)]
        public string cubeMessage = string.Empty;

        [PropertyOrder(2)]
        [LabelText("Cube collider")]
        [GUIColor(1f, 1f, 1f)]
        [SerializeField]
        private List<GameObject> colliderCube;

        [PropertyOrder(3)]
        [LabelText("Cube collider preview")]
        [HorizontalGroup("Cube", 0.9f)]
        [GUIColor(0.18f, 0.81f, 0.34f)]
        [Button(ButtonSizes.Large)]
        private void OnCubeColliderPreviewButtonClicked()
        {
            cubeMessage = CheckItemsList(colliderCube, TheListIsEmptyMessage + CubeListMessage);
            DrawColliderPreviewCube(colliderCube);
        }

        [PropertyOrder(4)]
        [LabelText("X")]
        [HorizontalGroup("Cube", 0.1f)]
        [GUIColor(1f, 0.27f, 0.22f)]
        [Button(ButtonSizes.Large)]
        private void OnClearCubeColliderPreviewButtonClicked() => ClearColliderPreview(colliderCube);

        [PropertyOrder(5)]
        [HideLabel]
        [DisplayAsString]
        [GUIColor(1f, 1f, 0f)]
        public string sphereMessage = string.Empty;

        [PropertyOrder(6)]
        [LabelText("Sphere collider")]
        [GUIColor(1f, 1f, 1f)]
        [SerializeField]
        private List<GameObject> colliderSphere;

        [PropertyOrder(7)]
        [LabelText("Sphere collider preview")]
        [HorizontalGroup("Sphere", 0.9f)]
        [GUIColor(0.18f, 0.81f, 0.34f)]
        [Button(ButtonSizes.Large)]
        private void OnSphereColliderPreviewButtonClicked()
        {
            sphereMessage = CheckItemsList(colliderSphere, TheListIsEmptyMessage + SphereListMessage);
            DrawColliderPreviewSphere(colliderSphere);
        }

        [PropertyOrder(7)]
        [LabelText("X")]
        [HorizontalGroup("Sphere", 0.1f)]
        [GUIColor(1f, 0.27f, 0.22f)]
        [Button(ButtonSizes.Large)]
        private void OnClearSphereColliderPreviewButtonClicked() => ClearColliderPreview(colliderSphere);

        private void DrawColliderPreviewCube(List<GameObject> items)
        {
            ClearColliderPreview(colliderCube);

            foreach (var item in items)
            {
                if (item == null)
                {
                    cubeMessage = HasNullMessage + CubeListMessage;
                    return;
                }

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
                if (item == null)
                {
                    sphereMessage = HasNullMessage + SphereListMessage;
                    return;
                }

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
                if (item == null) return;

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

        private string CheckItemsList(List<GameObject> items, string message)
        {
            if (items.Count == 0)
                return message;
            return string.Empty;
        }
    }
}
