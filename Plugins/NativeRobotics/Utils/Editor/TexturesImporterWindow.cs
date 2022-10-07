using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace NativeRobotics.Utils.Editor
{
    public class TexturesImporterWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Native Robotics/Textures Importer")]
        private static void OpenWindow()
        {
            GetWindow<TexturesImporterWindow>().Show();
        }

        private const int RES_4K = 4096;

        public enum PlatformType : uint
        {
            Standalone,
            Web,
            iPhone,
            Android,
            WebGL,
            WindowsStoreApps,
            PS4,
            XboxOne,
            Nintendo3DS,
            tvOS
        }

        private string[] platformOptions = new string[]
        {
            "Standalone",
            "Web",
            "iPhone",
            "Android",
            "WebGL",
            "Windows Store Apps",
            "PS4",
            "XboxOne",
            "Nintendo 3DS",
            "tvOS"
        };

        public List<PlatformType> platforms = new List<PlatformType>() { PlatformType.Standalone, PlatformType.iPhone, PlatformType.WebGL };

        [Space]

        [LabelText("Streaming Mip Maps:")]
        public bool streaming = false;

        [Space]

        [AssetsOnly]
        [PreviewField(ObjectFieldAlignment.Left)]
        [LabelText("AlbedoTransparency:")]
        [SuffixLabel("$aName", false)]
        public Texture a = null;
        private string aName => (a == null ? "-" : a.name);

        [AssetsOnly]
        [PreviewField(ObjectFieldAlignment.Left)]
        [LabelText("AO:")]
        [SuffixLabel("$aoName", false)]
        public Texture ao = null;
        private string aoName => (ao == null ? "-" : ao.name);

        [AssetsOnly]
        [PreviewField(ObjectFieldAlignment.Left)]
        [LabelText("MetallicSmoothness:")]
        [SuffixLabel("$msName", false)]
        public Texture ms = null;
        private string msName => (ms == null ? "-" : ms.name);

        [AssetsOnly]
        [PreviewField(ObjectFieldAlignment.Left)]
        [LabelText("Normal:")]
        [SuffixLabel("$nrName", false)]
        public Texture nr = null;
        private string nrName => (nr == null ? "-" : nr.name);

        [AssetsOnly]
        [PreviewField(ObjectFieldAlignment.Left)]
        [LabelText("Emission:")]
        [SuffixLabel("$emName", false)]
        public Texture em = null;
        private string emName => (em == null ? "-" : em.name);

        [HorizontalGroup("Size", LabelWidth = 30), PropertySpace(20)]
        [LabelText("AT:")]
        public int aSize = RES_4K;
        [HorizontalGroup("Size", LabelWidth = 30), PropertySpace(20)]
        [LabelText("AO:")]
        public int aoSize = RES_4K;
        [HorizontalGroup("Size", LabelWidth = 30), PropertySpace(20)]
        [LabelText("MS:")]
        public int msSize = RES_4K;
        [HorizontalGroup("Size", LabelWidth = 30), PropertySpace(20)]
        [LabelText("NR:")]
        public int nrSize = RES_4K;
        [HorizontalGroup("Size", LabelWidth = 30), PropertySpace(20)]
        [LabelText("EM:")]
        public int emSize = RES_4K;

        [HorizontalGroup("Presets", LabelWidth = 20)]
        [Button(ButtonSizes.Medium, Name = "4096"), PropertySpace(20)]
        public void Set4K()
        {
            aSize = RES_4K;
            emSize = RES_4K / 4;
            aoSize = RES_4K / 4;
            msSize = RES_4K;
            nrSize = RES_4K;
        }

        [HorizontalGroup("Presets", LabelWidth = 20), PropertySpace(20)]
        [Button(ButtonSizes.Medium, Name = "2048")]
        public void Set2K()
        {
            aSize = RES_4K / 2;
            emSize = RES_4K / 8;
            aoSize = RES_4K / 8;
            msSize = RES_4K / 2;
            nrSize = RES_4K / 2;
        }

        [HorizontalGroup("Presets", LabelWidth = 20), PropertySpace(20)]
        [Button(ButtonSizes.Medium, Name = "1024")]
        public void Set1K()
        {
            aSize = RES_4K / 4;
            emSize = RES_4K / 16;
            aoSize = RES_4K / 16;
            msSize = RES_4K / 4;
            nrSize = RES_4K / 4;
        }

        [HorizontalGroup("Presets", LabelWidth = 20), PropertySpace(20)]
        [Button(ButtonSizes.Medium, Name = "512")]
        public void Set05K()
        {
            aSize = RES_4K / 8;
            emSize = RES_4K / 32;
            aoSize = RES_4K / 32;
            msSize = RES_4K / 8;
            nrSize = RES_4K / 8;
        }

        [HorizontalGroup("Presets", LabelWidth = 20)]
        [Button(ButtonSizes.Medium, Name = "256"), PropertySpace(20)]
        public void EnhanceAlbedo()
        {
            aSize = RES_4K / 16;
            emSize = RES_4K / 64;
            aoSize = RES_4K / 64;
            msSize = RES_4K / 16;
            nrSize = RES_4K / 16;
        }

        [GUIColor(52.0f / 255, 199.0f / 255, 89.0f / 255)]
        [Button(ButtonSizes.Large), PropertySpace(20)]
        private void ProcessTextures()
        {
            TextureImporterFormat defaultFormat;
            TextureImporterFormat ambientEmissionFormat;
            TextureImporterFormat normalsFormat;

            foreach (var platform in platforms)
            {
                switch (platform)
                {
                    case PlatformType.iPhone:
                        defaultFormat = TextureImporterFormat.ASTC_10x10;
                        ambientEmissionFormat = TextureImporterFormat.ASTC_10x10;
                        normalsFormat = TextureImporterFormat.ASTC_6x6;
                        break;
                    case PlatformType.Standalone:
                        defaultFormat = TextureImporterFormat.DXT5;
                        ambientEmissionFormat = TextureImporterFormat.DXT1;
                        normalsFormat = TextureImporterFormat.BC7;
                        break;
                    case PlatformType.WebGL:
                        defaultFormat = TextureImporterFormat.DXT5;
                        ambientEmissionFormat = TextureImporterFormat.DXT1;
                        normalsFormat = TextureImporterFormat.DXT5;
                        break;
                    default:
                        defaultFormat = TextureImporterFormat.DXT5;
                        ambientEmissionFormat = TextureImporterFormat.DXT1;
                        normalsFormat = TextureImporterFormat.BC7;
                        break;

                }

                string platformString = platformOptions[(int)platform];

                EditorUtility.DisplayProgressBar($"Reimport Progress | {platformString}", "Number of processed assets", 0.0f / 5);
                if (a != null)
                    ReimportTexture(a, aSize, defaultFormat, platformString);
                EditorUtility.DisplayProgressBar($"Reimport Progress | {platformString}", "Number of processed assets", 1.0f / 5);
                if (ao != null)
                    ReimportTexture(ao, aoSize, ambientEmissionFormat, platformString);
                EditorUtility.DisplayProgressBar($"Reimport Progress | {platformString}", "Number of processed assets", 2.0f / 5);
                if (ms != null)
                    ReimportTexture(ms, msSize, defaultFormat, platformString);
                EditorUtility.DisplayProgressBar($"Reimport Progress | {platformString}", "Number of processed assets", 3.0f / 5);
                if (nr != null)
                    ReimportTexture(nr, nrSize, normalsFormat, platformString);
                EditorUtility.DisplayProgressBar($"Reimport Progress | {platformString}", "Number of processed assets", 4.0f / 5);
                if (em != null)
                    ReimportTexture(em, emSize, ambientEmissionFormat, platformString);
                EditorUtility.ClearProgressBar();

                EditorApplication.Beep();
            }
        }

        [GUIColor(255.0f / 255, 59.0f / 255, 48.0f / 255)]
        [Button(ButtonSizes.Large), PropertySpace(20)]
        private void Reset()
        {
            a = null;
            ao = null;
            ms = null;
            nr = null;
            em = null;
        }

        private void ReimportTexture(Object texture, int size, TextureImporterFormat format, string platform)
        {
            var tPath = AssetDatabase.GetAssetPath(texture);
            var ti = (TextureImporter)AssetImporter.GetAtPath(tPath);
            var tips = ti.GetPlatformTextureSettings(platform);
            tips.overridden = true;
            tips.maxTextureSize = size;
            tips.format = format;
            ti.mipmapEnabled = true;
            ti.streamingMipmaps = streaming;
            ti.SetPlatformTextureSettings(tips);

            ti.SaveAndReimport();
        }
    }
}
