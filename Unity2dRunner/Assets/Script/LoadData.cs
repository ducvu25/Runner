using System.IO;
using UnityEngine;

public static class LoadData 
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/save/";
    public static readonly string FILE_EXT = ".json";

    public static void Save(string filename, string dataToSave)
    {
        if(!Directory.Exists(SAVE_FOLDER))
            Directory.CreateDirectory(SAVE_FOLDER);
        //Debug.Log(SAVE_FOLDER + filename + FILE_EXT);
        File.WriteAllText(SAVE_FOLDER + filename + FILE_EXT, dataToSave);
    }
    public static string Load(string filename)
    {
        string file = SAVE_FOLDER + filename + FILE_EXT;
        if (File.Exists(file))
        {
            string s = File.ReadAllText(file);
            return s;
        }
        return null;
    }
}
