using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting; // Añadimos esta línea
using System.Linq;
using System.Collections.Generic;

public class BuildScript
{
    [MenuItem("Build/Build Linux")]
    public static void BuildLinux()
    {
        Debug.Log("Starting Linux build...");
        
        try
        {
            string[] scenes = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();

            if (scenes.Length == 0)
            {
                Debug.LogError("No scenes found in build settings!");
                EditorApplication.Exit(1);
                return;
            }

            Debug.Log($"Building {scenes.Length} scenes: {string.Join(", ", scenes)}");

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = "build/StandaloneLinux64/Game.x86_64",
                target = BuildTarget.StandaloneLinux64,
                options = BuildOptions.Development
            };

            Debug.Log("Build configuration prepared, starting build...");
            
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;
            
            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build completed successfully!");
            }
            else
            {
                Debug.LogError($"Build failed with result: {summary.result}");
                Debug.LogError($"Total errors: {summary.totalErrors}");
                EditorApplication.Exit(1);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Build failed with exception: {ex}");
            EditorApplication.Exit(1);
        }
    }

    [MenuItem("Build/Build Windows")]
    public static void BuildWindows()
    {
        Debug.Log("Starting Windows build...");
        
        try
        {
            string[] scenes = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();

            if (scenes.Length == 0)
            {
                Debug.LogError("No scenes found in build settings!");
                EditorApplication.Exit(1);
                return;
            }

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = "build/StandaloneWindows64/Game.exe",
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.Development
            };

            Debug.Log("Build configuration prepared, starting build...");
            
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;
            
            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build completed successfully!");
            }
            else
            {
                Debug.LogError($"Build failed with result: {summary.result}");
                Debug.LogError($"Total errors: {summary.totalErrors}");
                EditorApplication.Exit(1);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Build failed with exception: {ex}");
            EditorApplication.Exit(1);
        }
    }
}