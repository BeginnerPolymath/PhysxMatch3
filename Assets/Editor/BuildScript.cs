using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class BuildScript
{

    [MenuItem("File/Build All")]
    static void BuildAll()
    {
        var scenes = EditorBuildSettings.scenes;
        BuildPipeline.BuildPlayer(scenes, "./Builds/Windows/Shift (Alpha).exe", BuildTarget.StandaloneWindows, BuildOptions.None);
        BuildPipeline.BuildPlayer(scenes, "./Builds/Android/Shift (Alpha).apk", BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("File/Build Windows")]
    static void BuildWindows()
    {
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "./Builds/Windows/Shift (Alpha).exe", BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    [MenuItem("File/Build Android")]
    static void BuildAndroid()
    {
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "./Builds/Android/Shift (Alpha).apk", BuildTarget.Android, BuildOptions.None);
    }

    static void PerformAssetBundleBuild()
    {
        BuildPipeline.BuildAssetBundles("../AssetBundles/", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
        BuildPipeline.BuildAssetBundles("../AssetBundles/", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
    }
}