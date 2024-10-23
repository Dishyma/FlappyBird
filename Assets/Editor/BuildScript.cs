using UnityEditor;
using UnityEngine;
using System.Linq;

public class BuildScript
{
    [MenuItem("Build/Build Linux")]
    public static void BuildLinux()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = "Builds/LinuxBuild/Game.x86_64";
        buildPlayerOptions.target = BuildTarget.StandaloneLinux64;
        buildPlayerOptions.options = BuildOptions.None;
        
        Debug.Log("Starting Linux build...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Linux build completed!");
    }

    [MenuItem("Build/Build Windows")]
    public static void BuildWindows()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = "Builds/WindowsBuild/Game.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        
        Debug.Log("Starting Windows build...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Windows build completed!");
    }
}