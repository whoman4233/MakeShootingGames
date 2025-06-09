using Chapter.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chapter.Data
{
    public class DataManager : Singleton<DataManager>
    {
        public Dictionary<string, PlayerShips> playerShipsDataMap;
        public Dictionary<string, EnemyShipsAttackPatten> EnemyShipsAttackPattenDataMap;
        public Dictionary<string, EnemyShipsMovePatten> EnemyShipsMovePattenDataMap;
        public Dictionary<string, BossShipsAttackPatten> BossShipsAttackPattenDataMap;
        public Dictionary<string, BossShipsMovePatten> BossShipsMovePattenDataMap;
        public Dictionary<string, EnemyStatus> EnemyStatusDataMap;

        private void Awake()
        { 
            base.Awake();
            LoadAllData();
        }

        void LoadAllData()
        {
            playerShipsDataMap = JsonLoader.LoadAsDictionary<PlayerShips>("PlayerShips", p => p.name);
            EnemyShipsAttackPattenDataMap = JsonLoader.LoadAsDictionary<EnemyShipsAttackPatten>("EnemyShipsAttackPatten", p => p.PattenID);
            EnemyShipsMovePattenDataMap = JsonLoader.LoadAsDictionary<EnemyShipsMovePatten>("EnemyShipsMovePatten", p => p.PattenID);
            BossShipsAttackPattenDataMap = JsonLoader.LoadAsDictionary<BossShipsAttackPatten>("BossShipsAttackPatten", p => p.PattenID);
            BossShipsMovePattenDataMap = JsonLoader.LoadAsDictionary<BossShipsMovePatten>("BossShipsMovePatten", p => p.PattenID);
            EnemyStatusDataMap = JsonLoader.LoadAsDictionary<EnemyStatus>("EnemyStatus", p => p.EnemyID);
        }

        public PlayerShips GetPlayer(string id)
        {
            return playerShipsDataMap.TryGetValue(id, out var data) ? data : null;
        }

        public EnemyShipsAttackPatten GetEnemyAttackPatten(string id)
        {
            return EnemyShipsAttackPattenDataMap.TryGetValue(id, out var data) ? data : null;
        }

        public EnemyShipsMovePatten GetEnemyMovePatten(string id)
        {
            return EnemyShipsMovePattenDataMap.TryGetValue(id, out var data) ? data : null;
        }

        public BossShipsAttackPatten GetBossAttackPatten(string id)
        {
            return BossShipsAttackPattenDataMap.TryGetValue(id, out var data) ? data : null;
        }

        public BossShipsMovePatten GetBossMovePatten(string id)
        {
            return BossShipsMovePattenDataMap.TryGetValue(id, out var data) ? data : null;
        }

        public EnemyStatus GetEnemyStatus(string id)
        {
            return EnemyStatusDataMap.TryGetValue(id, out var data) ? data : null;
        }


    }
}