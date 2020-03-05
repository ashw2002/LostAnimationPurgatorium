using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Provides editor menu items.
/// </summary>
public class Menus : Editor
{
    /// <summary>
    /// Imports packages for User Reporting.
    /// </summary>
    [MenuItem("Window/User Reporting/Import Packages", false, 9999)]
    public static void ImportPackageMenuItem()
    {
        string[] packages = Directory.GetFiles("Assets/..", "UserReporting.unitypackage", SearchOption.AllDirectories);
        foreach (string package in packages)
        {
            AssetDatabase.ImportPackage(package, true);
        }
    }
}
