using Assets.Jump_Hero.Scripts.GameManaging.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Jump_Hero.Scripts.GameManaging.Bootstrapper
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private SceneContext _sceneContext;

        public void Awake()
        {
#if UNITY_EDITOR
            //SceneManager.LoadScene(EditorPrefs.GetString("ActiveScene"), LoadSceneMode.Additive);
#endif
            _sceneContext.Run();
            GameState.MenusState.Enter();
        }

        
    }
}

