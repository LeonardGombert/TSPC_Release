﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Networking;
using Sirenix.OdinInspector;
using Gameplay.VR;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public bool startGame;

        [Tooltip("Cannot Lose the Game")]
        public bool ghostMode;

        public int currentLevelIndex { get; set; }
        private GameScenes currentScene;

        public BoolVariable _isMobile;
        public BoolVariable _ghostingGame;

        public Transform UICanvas;

        [SerializeField] private CallableFunction _fadeTransition;
        [SerializeField] private CallableFunction _unfadeTransition;

        public AlertManager alertManager;

        [Title("Lose")]
        public Transform loseCanvas;
        public StringVariable _loseText;
        [SerializeField] private Sprite[] deathIcons;

        [Title("Win")]
        public Transform winCanvas;

        public static GameManager instance;
        void OnEnable() { if (instance == null) instance = this; }

        void Start()
        {
            _ghostingGame.Value = ghostMode;

            if (startGame)
            {
                currentScene = GameScenes.Null;
                LoadMainMenu();
            }
        }

        [Button]
        public void Lose(LoseType loseType)
        {
            if (ghostMode) return;

            // subject to change very soon
            if (_isMobile.Value) loseCanvas.gameObject.SetActive(true);
            Text loseText = loseCanvas.Find("ExplanationText").GetComponentInChildren<Text>();

            #region Set LoseText value
            switch (loseType)
            {
                case LoseType.PlayerSpottedByGuard:
                    loseText.text = _loseText.Value = "You were spotted by a Guard";
                    loseCanvas.transform.Find("DeathIcon").GetComponent<Image>().overrideSprite = deathIcons[0];
                    break;
                case LoseType.PlayerSpottedByCam:
                    loseText.text = _loseText.Value = "You were spotted by a Camera";
                    loseCanvas.transform.Find("DeathIcon").GetComponent<Image>().overrideSprite = deathIcons[1];
                    break;

                case LoseType.BodySpottedByCam:
                    loseText.text = _loseText.Value = "A dead body was spotted by a Camera";
                    loseCanvas.transform.Find("DeathIcon").GetComponent<Image>().overrideSprite = deathIcons[2];
                    break;
                case LoseType.BodySpottedByGuard:
                    loseText.text = _loseText.Value = "A dead body was spotted by a Guard";
                    loseCanvas.transform.Find("DeathIcon").GetComponent<Image>().overrideSprite = deathIcons[3];
                    break;

                case LoseType.PlayerHitTrap:
                    loseText.text = _loseText.Value = "You ran into a Hidden Trap !";
                    loseCanvas.transform.Find("DeathIcon").GetComponent<Image>().overrideSprite = deathIcons[4];
                    break;

                case LoseType.MissSymbols:
                    loseText.text = _loseText.Value = "You failed to enter the right Code !";
                    loseCanvas.transform.Find("DeathIcon").GetComponent<Image>().overrideSprite = deathIcons[5];
                    break;
            } 
            #endregion

            // Reset the symbols
            TransmitterManager.instance.symbolManager.isSymbolLoaded.Value = false;
        }

        [Button]
        public void Win()
        {
            if (_isMobile.Value) winCanvas.gameObject.SetActive(true);
        }

        IEnumerator WaitLoadScene(GameScenes scene)
        {
            _fadeTransition.Raise();
            yield return new WaitForSeconds(1.0f);

            if (currentScene != GameScenes.Null) SceneManager.UnloadSceneAsync((int)currentScene);

            yield return new WaitForEndOfFrame();

            SceneManager.LoadScene((int)scene, LoadSceneMode.Additive);
            currentScene = scene;
            
            yield return new WaitForSeconds(1.0f);
            _unfadeTransition.Raise();

            yield break;
        }

        public void LoadScene(GameScenes scene)
        {
            StartCoroutine(WaitLoadScene(scene));
        }

        public void LoadMainMenu()
        {
            winCanvas.gameObject.SetActive(false);
            loseCanvas.gameObject.SetActive(false);

            currentLevelIndex = 1;
            LoadScene(GameScenes.GameScene_MainMenu);
        }
    }
}
