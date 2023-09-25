using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Jump_Hero.Scripts.GameManaging.GameStates
{
    internal class GameLevelState : GameState
    {
        private SceneAsset _scene;

        public GameLevelState(SceneAsset scene)
        {
            _scene = scene;
            GameLevelState = this;
        }

        public override void Enter()
        {
            SceneManager.LoadSceneAsync(_scene.name, LoadSceneMode.Additive);
        }

        protected override void BeforeExit(GameState nextState)
        {
            SceneManager.UnloadSceneAsync(_scene.name);
        }
    }
}
