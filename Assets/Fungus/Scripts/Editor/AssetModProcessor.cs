// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.IO;
using UnityEngine;

namespace Fungus.EditorUtils
{
    /// <summary>
    /// Prevents saving of selected blocks and commands to avoid version control conflicts.
    /// </summary>
    public class AssetModProcessor : UnityEditor.AssetModificationProcessor
    {
        public static string[] OnWillSaveAssets(string[] paths)
        {
            string sceneName = "";
            
            foreach (string path in paths)
            {
                if (path.Contains(".unity"))
                {
                    sceneName = Path.GetFileNameWithoutExtension(path);
                }
            }
            
            if (sceneName.Length == 0)
            {
                return paths;
            }

            // Reset these variables before save so that they won't cause a git conflict
            Flowchart[] flowcharts = UnityEngine.Object.FindObjectsByType<Flowchart>(FindObjectsSortMode.None);
            foreach (Flowchart f in flowcharts)
            {
                if (!f.SaveSelection)
                {
                    f.SelectedBlock = null;
                    f.ClearSelectedCommands();
                }
            }

            return paths;
        }
    }
}