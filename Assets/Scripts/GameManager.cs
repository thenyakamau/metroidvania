using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace namespace1
{
    public class GameManager : MonoBehaviour
    {
        public GameObject startGamePanel, pauseGamePanel;

        public bool isOnPause;

        private PlayerMovement playerMovementScript;

        void Start()
        {
            playerMovementScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        void Update()
        {
            if (playerMovementScript.gameOver)
                startGamePanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape))
                CheckPaused();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        void CheckPaused()
        {
            if (!isOnPause)
            {
                isOnPause = true;
                pauseGamePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                isOnPause = false;
                pauseGamePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
