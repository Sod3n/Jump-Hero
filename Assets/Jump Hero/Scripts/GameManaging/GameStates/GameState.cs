using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Jump_Hero.Scripts.GameManaging.GameStates
{
    internal abstract class GameState
    {
        public static MenusState MenusState;
        public static GameLevelState GameLevelState;

        public abstract void Enter();
        public void Exit(GameState nextState)
        {
            BeforeExit(nextState);

            nextState.Enter();
        }
        protected abstract void BeforeExit(GameState nextState);
    }
}
