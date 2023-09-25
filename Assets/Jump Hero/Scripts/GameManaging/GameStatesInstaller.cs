using Assets.Jump_Hero.Scripts.GameManaging.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Jump_Hero.Scripts.GameManaging
{
    internal class GameStatesInstaller : MonoInstaller
    {
        [SerializeField] private SceneAsset _menusScene;
        [SerializeField] private SceneAsset _gameLevelScene;
        public override void InstallBindings()
        {
            Container.BindInstance(_menusScene).WithId("MenusState");
            Container.Bind<MenusState>().AsSingle().NonLazy();
            
            Container.BindInstance(_gameLevelScene).WithId("GameLevelState");
            Container.Bind<GameLevelState>().AsSingle().NonLazy();
        }
    }
}
