using UnityEditor;
using UnityEngine;

public class BuildScript
{
    [MenuItem("Build/Build Linux")]
    public static void BuildLinux()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = EditorBuildSettings.scenes;
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
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = EditorBuildSettings.scenes;
        buildPlayerOptions.locationPathName = "Builds/WindowsBuild/Game.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        
        Debug.Log("Starting Windows build...");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Windows build completed!");
    }
}