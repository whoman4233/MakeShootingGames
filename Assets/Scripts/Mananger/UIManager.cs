using Chapter.Event;
using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        PlayerEventBus playerEventBus;
        GameEventBus gameEventBus;

        public void Initalize()
        {
            playerEventBus = GameManager.Instance.playerEventBus;
            playerEventBus.Subscribe(PlayerEventType.OnDead, GameOver);
            gameEventBus = GameManager.Instance.gameEventBus;
            gameEventBus.Subscribe(GameEventType.End, StageClear);
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
            gameEventBus = GameManager.Instance.gameEventBus;
            gameEventBus.Subscribe(GameEventType.Start, ShowStageText);
            Debug.Log("LobbyInit");
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
            StageText.text = "Stage : " + GameManager.Instance.NowStage.ToString();
            Debug.Log("ShowStageText");
        }

        public void GameOver()
        {
            GameOverPanel.SetActive(true);
        }

        public void StageClearButtonClick()
        {
            GameManager.Instance.StageClearButtonClick();
        }

        public void StageClear()
        {
            GameClearPanel.SetActive(true);
        }

        public void ReGame()
        {
            GameOverPanel.SetActive(false);
            GameManager.Instance.ReGameButtonClick();
        }

        public void OnLeftButtonClick()
        {
            GameManager.Instance.OnBackButtonClick();
        }

        public void OnRightButtonClick()
        {
            GameManager.Instance.OnNextButtonClick();
        }

        public void GameStartButtonClick()
        {
            GameManager.Instance.OnGameStartButtonClick();
        }

    }
}
