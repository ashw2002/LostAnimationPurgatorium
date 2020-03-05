using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public static class ErrorLog {
    //makes a new file path for the ErrorLog class to write to everytime a new game starts
    public static string FileName = Application.persistentDataPath + "/logs/" + CleanInput(System.DateTime.UtcNow.ToString()) + "_Log.txt";

    public static void CreateDebugFile(string s) {
        //makes sure the logs directory exists before trying to write a file to it
        if (System.IO.Directory.Exists(Application.persistentDataPath + "/logs"))
        {
            CreateFile(s);
        }
        else
        {
            //if the directory doesn't exist then it makes the directory with the appropriate path
            //then creates the file
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/logs");
            CreateFile(s);
        }
    }
    //formats the debug with the info prefix and appends whatever is called through the function to the file
    public static void InfoDebugTxt(string s){
        System.IO.File.AppendAllText(FileName, "[" + System.DateTime.UtcNow.ToString() + "] " + "[INFO] " + s + Environment.NewLine);
    }
    //formats the debug with the error prefix and appends whatever is called through the function to the file
    public static void ErrorDebugTxt(string s){
        System.IO.File.AppendAllText(FileName, "[" + System.DateTime.UtcNow.ToString() + "] " + "[ERROR] " + s + Environment.NewLine);
    }
    //makes the file with the appropriate file path
    static void CreateFile(string s){
        FileName = Application.persistentDataPath + "/logs/" + CleanInput(System.DateTime.UtcNow.ToString()) + "_Log.txt";
        System.IO.File.WriteAllText(FileName, "["+ System.DateTime.UtcNow.ToString() + "]" + " [INFO] Game Start" + Environment.NewLine);
    }
    //removes invalid characters from the datetime string then returns it
    static string CleanInput(string strIn){
        string fixedString = Regex.Replace(strIn, @"[^0-9a-zA-Z]+" , "_");
        return fixedString;
    }
}