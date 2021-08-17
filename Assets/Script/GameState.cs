using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GoGooseGo
{
    public class GameState
    {
        public ReactiveProperty<bool> isStop = new ReactiveProperty<bool>();

        public void Restart()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}