using UnityEngine;
using System;
using System.Collections;

namespace Completed
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance = null;
        public BoardManager boardScript;
        

        private int level = 4;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            boardScript = GetComponent<BoardManager>();
            initGame();
        }

        void initGame()
        {
            boardScript.SetupScene(level);
        }

        void Update()
        {

        }
    }
}