using System.Collections.Generic;
using NativeRobotics.Utils.ColliderPreview;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace NativeRobotics.Utils.Editor
{
    public class ColliderPreviewWindow : OdinEditorWindow
    {
        private const string TheCollectionIsEmptyMessage = "The collection is EMPTY";
        private const string TheCollectionHasNullMessage = "The collection has NULL";
        private const string CubeCollectionMessage = ", please add cube colliders and try it again";
        private const string SphereCollectionMessage = ", please add sphere colliders and try it again";

        [MenuItem("Tools/Native Robotics/Collider Preview")]
        private static void OpenWindow() => GetWindow<ColliderPreviewWindow>().Show();

        [PropertyOrder(0)]
        [LabelText("Clear all collider preview")]
        [HorizontalGroup("Clear", 0.9f)]
        [GUIColor(1f, 0.27f, 0.22f)]
        [Button(ButtonSizes.Large)]
        private void OnClearButtonClicked()
        {
            ClearColliderPreview(colliderCube);
            ClearColliderPreview(colliderSphere);
        }

        [PropertyOrder(1)]
        [LabelText("X")]
        [HorizontalGroup("Clear", 0.1f)]
        [GUIColor(1f, 0.27f, 0.22f)]
        [Button(ButtonSizes.Large)]
        private void OnButtonClearCollection() => ClearCollection();

        [PropertyOrder(2)]
        [HideLabel]
        [DisplayAsString]
        [GUIColor(1f, 1f, 0f)]
        public string cubeMessage = string.Empty;

        [PropertyOrder(3)]
        [LabelText("Cube collection")]
        [GUIColor(0.18f, 0.51f, 0.34f)]
        [SerializeField]
        private List<GameObject> colliderCube;

        [PropertyOrder(4)]
        [LabelText("Cube collider preview")]
        [HorizontalGroup("Cube", 0.9f)]
        [GUIColor(0.18f, 0.81f, 0.34f)]
        [Button(ButtonSizes.Large)]
        private void OnCubeColliderPreviewButtonClicked()
        {
            cubeMessage = CheckItemsCollection(colliderCube, TheCollectionIsEmptyMessage + CubeCollectionMessage);
            DrawColliderPreviewCube(colliderCube);
        }

        [PropertyOrder(5)]
        [LabelText("X")]
        [HorizontalGroup("Cube", 0.1f)]
        [GUIColor(1f, 0.27f, 0.22f)]
        [Button(ButtonSizes.Large)]
        private void OnClearCubeColliderPreviewButtonClicked() => ClearColliderPreview(colliderCube);

        [PropertyOrder(6)]
        [HideLabel]
        [DisplayAsString]
        [GUIColor(1f, 1f, 0f)]
        public string sphereMessage = string.Empty;

        [PropertyOrder(7)]
        [LabelText("Sphere collection")]
        [GUIColor(0.03f, 0.517f, 1f)]
        [SerializeField]
        private List<GameObject> colliderSphere;

        [PropertyOrder(8)]
        [LabelText("Sphere collider preview")]
        [HorizontalGroup("Sphere", 0.9f)]
        [GUIColor(0.03f, 0.517f, 1f)]
        [Button(ButtonSizes.Large)]
        private void OnSphereColliderPreviewButtonClicked()
        {
            sphereMessage = CheckItemsCollection(colliderSphere, TheCollectionIsEmptyMessage + SphereCollectionMessage);
            DrawColliderPreviewSphere(colliderSphere);
        }

        [PropertyOrder(9)]
        [LabelText("X")]
        [HorizontalGroup("Sphere", 0.1f)]
        [GUIColor(1f, 0.27f, 0.22f)]
        [Button(ButtonSizes.Large)]
        private void OnClearSphereColliderPreviewButtonClicked() => ClearColliderPreview(colliderSphere);

        private void ClearCollection()
        {
            colliderCube.Clear();
            colliderSphere.Clear();
        }

        private void DrawColliderPreviewCube(List<GameObject> collections)
        {
            ClearColliderPreview(colliderCube);

            foreach (var item in collections)
            {
                if (item == null)
                {
                    cubeMessage = TheCollectionHasNullMessage + CubeCollectionMessage;
                }
                else
                {
                    if (!item.GetComponent<CubeDrawColliderPreview>())
                    {
                        var cubeGizmo = item.AddComponent<CubeDrawColliderPreview>();
                        var cubeMesh = item.AddComponent<CubeMeshGenerator>();
                        CreateMesh(cubeMesh);
                        DrawGizmo(cubeGizmo);
                    }
                }
            }
        }

        private void DrawColliderPreviewSphere(List<GameObject> collections)
        {
            ClearColliderPreview(colliderSphere);

            foreach (var item in collections)
            {
                if (item == null)
                {
                    sphereMessage = TheCollectionHasNullMessage + SphereCollectionMessage;
                }
                else
                {
                    var sphereGizmo = item.AddComponent<SphereDrawColliderPreview>();
                    var sphereMesh = item.AddComponent<SphereMeshGenerator>();
                    CreateMesh(sphereMesh);
                    DrawGizmo(sphereGizmo);
                }
            }
        }

        private void ClearColliderPreview(List<GameObject> collections)
        {
            foreach (var item in collections)
            {
                if (item == null)
                {
                    Debug.Log(TheCollectionHasNullMessage);
                }
                else
                {
                    var components = item.GetComponents<Component>();

                    foreach (var component in components)
                    {
                        if (component.GetType() != typeof(Transform))
                        {
                            DestroyImmediate(component);
                        }
                    }
                }
            }
        }

        private string CheckItemsCollection(List<GameObject> collections, string message)
        {
            if (collections.Count == 0)
            {
                return message;
            }

            return string.Empty;
        }

        private void CreateMesh(MeshGenerator meshGenerator) => meshGenerator.GenerateMesh();
        private void DrawGizmo(DrawColliderPreview drawColliderPreview) => drawColliderPreview.DrawGizmo();
    }
}
