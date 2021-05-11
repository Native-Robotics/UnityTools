using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace NativeRobotics.Utils.Editor
{
    public class RobotGeneratorWindow : OdinEditorWindow
    {
        [ShowInInspector, FolderPath]
        private string savingPath = "Assets/Debug/Robot";

        [Header("Basic settings"), OnValueChanged("DOFChanged"), Range(1, 14), LabelText("DOF")]
        public int dof = 6;

        protected virtual string SavingPath
        {
            get => savingPath;
            set => savingPath = value;
        }

        public virtual void DOFChanged()
        {
        }

        /// <summary>
        /// Saving robot prefab
        /// </summary>
        /// <param name="prefab">Reference to robot prefab</param>
        protected void SavePrefab(GameObject prefab)
        {
            var prefabName = $"{prefab.name}.prefab";
            var path = Path.Combine(SavingPath, prefabName);
            PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, path, InteractionMode.AutomatedAction);
        }

        protected static class AngleHelper
        {
            public enum Action
            {
                SwitchSign,
                SetPi,
                SetHalfPi
            }

            public static float Help(Action action, float angle)
            {
                switch (action)
                {
                    case Action.SwitchSign:
                        return angle * -1;
                    case Action.SetPi:
                        return Mathf.PI;
                    case Action.SetHalfPi:
                        return Mathf.PI / 2;
                    default:
                        return angle;
                }
            }
        }
    }
}