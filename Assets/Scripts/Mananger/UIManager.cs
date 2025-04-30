using Chapter.Event;
using Chapter.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject GameOverPanel;
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private Transform HeartParent;
        private List<GameObject> hearts = new List<GameObject>();
        PlayerEventBus playerEventBus;


        public void Initalize()
        {
            playerEventBus = GameManager.Instance.playerEventBus;
            playerEventBus.Subscribe(PlayerEventType.OnDead, GameOver);
            GameOverPanel.SetActive(false);
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

        public void GameOver()
        {
            GameOverPanel.SetActive(true);
        }

        public void ReGame()
        {
            GameOverPanel.SetActive(false);
            GameManager.Instance.ReGameButtonClick();
        }

    }
}
