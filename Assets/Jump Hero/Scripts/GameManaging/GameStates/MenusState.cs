using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Jump_Hero.Scripts.GameManaging.GameStates
{
    internal class MenusState : GameState
    {
        private SceneAsset _scene;

        public MenusState(SceneAsset scene) 
        {
            _scene = scene;
            MenusState = this;
            Debug.Log(scene);
        }

        public override void Enter()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_scene.name, LoadSceneMode.Additive);
            asyncOperation.completed += AsyncOperation_completed;
        }

        private void AsyncOperation_completed(AsyncOperation obj)
        {
            Exit(GameLevelState);
        }

        protected override void BeforeExit(GameState nextState)
        {
            SceneManager.UnloadSceneAsync(_scene.name);
        }
    }
}
