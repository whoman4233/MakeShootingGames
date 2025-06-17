using Chapter.Event;
using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Chapter.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject GameClearPanel;
        [SerializeField] private GameObject GameOverPanel;
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private Transform HeartParent;
        [SerializeField] private Text StageText;
        private List<GameObject> hearts = new List<GameObject>();

        private void Awake()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if(currentSceneName == "LobbyScene 1")
            {
                LobbyInit();
            }
            else if(currentSceneName == "InGameScene")
            {
                Initalize();
            }
        }

        public void Initalize()
        {
            EventBusManager.Instance.playerEventBus.Subscribe(PlayerEventType.OnDead, GameOver);
            EventBusManager.Instance.gameEventBus.Subscribe(GameEventType.End, StageClear);
            if(GameOverPanel != null)
            {
                GameOverPanel.SetActive(false);
            }
            if (GameClearPanel != null)
            {
                GameClearPanel.SetActive(false);
            }
        }

        public void LobbyInit()
        {
            EventBusManager.Instance.gameEventBus.Subscribe(GameEventType.Start, ShowStageText);
        }



        public void SetMaxHp(int maxHp)
        {
            // 기존 하트 제거
            foreach (Transform t in HeartParent)    
                Destroy(t.gameObject);
            hearts.Clear();

            // 새 하트 생성
            for (int i = 0; i < maxHp; i++)
            {
                GameObject heart = Instantiate(heartPrefab, HeartParent);
                hearts.Add(heart);
            }
        }

        public void UpdateHp(int currentHp)
        {
            for (int i = 0; i < hearts.Count; i++)
            {
                hearts[i].SetActive(i < currentHp);
            }
        }

        public void ShowStageText()
        {
            Debug.Log("UIManager.ShowStageText()");
            StageText.text = "Stage : " + StageManager.Instance.NowStage.ToString();
        }

        public void GameOver()
        {
            GameOverPanel.SetActive(true);
        }

        public void StageClearButtonClick()
        {
            EventBusManager.Instance.gameEventBus.Publish(GameEventType.UIClearButton);
        }

        public void StageClear()
        {
            GameClearPanel.SetActive(true);
        }

        public void ReGame()
        {
            GameOverPanel.SetActive(false);
            EventBusManager.Instance.gameEventBus.Publish(GameEventType.UIRegameButton);
        }

        public void OnLeftButtonClick()
        {
            EventBusManager.Instance.gameEventBus.Publish(GameEventType.UIBackButton);
        }

        public void OnRightButtonClick()
        {
            EventBusManager.Instance.gameEventBus.Publish(GameEventType.UINextButton);
        }

        public void GameStartButtonClick()
        {
            EventBusManager.Instance.gameEventBus.Publish(GameEventType.UIStartButton);
        }

    }
}
