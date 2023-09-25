using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Jump_Hero.Scripts.Editor
{
    internal class BootLoaderWindow : EditorWindow
    {
        void OnGUI()
        {
            EditorSceneManager.playModeStartScene = (SceneAsset)EditorGUILayout.ObjectField(new GUIContent("Boot Scene"), EditorSceneManager.playModeStartScene, typeof(SceneAsset), false);
            EditorPrefs.SetString("ActiveScene", EditorSceneManager.GetActiveScene().path);
        }

        [MenuItem("Window/BootLoaderSettings")]
        static void Open()
        {
            GetWindow<BootLoaderWindow>();
        }
    }
}
