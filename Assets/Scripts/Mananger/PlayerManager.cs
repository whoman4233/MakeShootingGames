using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Singleton;
using Chapter.Event;
using Chapter.Data;
using Chapter.Strategy;
using Chapter.Base;

namespace Chapter.Manager
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public string PlayerID => $"P0{PlayerIndex + 1}"; // 예: index 0 -> "P01"
        public int PlayerIndex { get; private set; }

        public int PlayerHP { get; private set; }
        public float Damage { get; private set; }
        public float ShootSpeed { get; private set; }
        public float MoveSpeed { get; private set; }

        private const int DefaultMaxHp = 3;

        private UIManager uiManager;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            uiManager = FindObjectOfType<UIManager>();
            EventBusManager.Instance.playerEventBus.Subscribe(PlayerEventType.OnHit, OnPlayerHit);
        }

        public void SetPlayerIndex(int index)
        {
            PlayerIndex = index;
        }
        public void InitializePlayer()
        {
            LoadStatsFromData();
            uiManager = FindObjectOfType<UIManager>();
            uiManager.SetMaxHp(PlayerHP);
            uiManager.UpdateHp(PlayerHP);
        }

        private void LoadStatsFromData()
        {
            var data = DataManager.Instance.GetPlayer(PlayerID);
            if (data == null)
            {
                Debug.LogWarning($"Player data not found for ID: {PlayerID}, using default values.");
                PlayerHP = DefaultMaxHp;
                Damage = 5;
                ShootSpeed = 1f;
                MoveSpeed = 5f;
            }
            else
            {
                PlayerHP = data.health;
                Damage = data.damage;
                ShootSpeed = data.shootSpeed;
                MoveSpeed = data.moveSpeed;
            }
        }

        public void SetupPlayer(PlayerBase player)
        {
            var data = DataManager.Instance.GetPlayer(PlayerID);
            PlayerAttackStrategy attackStrategy = new AssaultRifle();
            Sprite sprite = null;

            switch (PlayerID)
            {
                case "P01":
                    attackStrategy = new AssaultRifle();
                    sprite = GameManager.Instance._spriteData.playerSprite[0];
                    break;
                case "P02":
                    attackStrategy = new AutoAim();
                    sprite = GameManager.Instance._spriteData.playerSprite[1];
                    break;
                case "P03":
                    attackStrategy = new MissileLauncher();
                    sprite = GameManager.Instance._spriteData.playerSprite[2];
                    break;
                case "P04":
                    attackStrategy = new LaserBeam();
                    sprite = GameManager.Instance._spriteData.playerSprite[3];
                    break;
                case "P05":
                    attackStrategy = new DroneCarrier();
                    sprite = GameManager.Instance._spriteData.playerSprite[4];
                    break;
                case "P06":
                    attackStrategy = new EnergyBarrier();
                    sprite = GameManager.Instance._spriteData.playerSprite[5];
                    break;
                case "P07":
                    attackStrategy = new ChargeShot();
                    sprite = GameManager.Instance._spriteData.playerSprite[6];
                    break;
            }

            var loadout = new PlayerLoadout
            {
                maxHp = data.health,
                moveSpeed = data.moveSpeed,
                shootDelay = data.shootSpeed,
                weapon = attackStrategy,
                sprite = sprite
            };

            player.ApplyLoadout(loadout);
        }

        public void OnPlayerHit()
        {
            PlayerHP--;
            uiManager.UpdateHp(PlayerHP);

            if (PlayerHP <= 0)
            {
                EventBusManager.Instance.playerEventBus.Publish(PlayerEventType.OnDead);
            }
        }

        public void SpawnPlayer()
        {
            Debug.Log($"Player {PlayerIndex} 스폰됨 | Damage: {Damage}, ShootSpeed: {ShootSpeed}, MoveSpeed: {MoveSpeed}");
            // TODO: 오브젝트 생성 및 설정
        }

        public void TakeDamage(int amount)
        {
            PlayerHP -= amount;
            uiManager.UpdateHp(PlayerHP);
            if (PlayerHP <= 0)
            {
                EventBusManager.Instance.playerEventBus.Publish(PlayerEventType.OnDead);
            }
        }
    }
}

namespace Chapter.Data
{
    public struct PlayerLoadout
    {
        public int maxHp;
        public float moveSpeed;
        public float shootDelay;
        public IWeaponStrategy weapon;
        public Sprite sprite;
    }
}
