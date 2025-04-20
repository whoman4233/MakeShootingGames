using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.State;
using Chapter.CharacterBase;
using Chapter.Data;
using Chapter.Event;

namespace Chapter.Base
{
    public class PlayerBase : MonoBehaviour
    {
        IPlayerState currentState;

        private IPlayerState
            _IdleState, _MoveState;

        private IEventBus<PlayerEventType> _eventBus;

        bool _IsAttack;

        [Header("기본 이동/공격 설정")]
        public float moveSpeed = 5f;
        public float ShootSpeed;


        public void Start()
        {
            _IdleState = gameObject.AddComponent<PlayerIdleState>();
            _MoveState = gameObject.AddComponent<PlayerMoveState>();

            _eventBus.Subscribe(PlayerEventType.OnAttack, AttackSound);

            ChangeState(new PlayerIdleState());
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe(PlayerEventType.OnAttack, AttackSound);
        }

        public void ChangeState(IPlayerState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter(this);
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void Move(Vector2 direction)
        {
            transform.Translate(direction *  moveSpeed * Time.deltaTime);
        }
        
        public void Attack()
        {
            _IsAttack = !_IsAttack;
            _eventBus.Publish(PlayerEventType.OnAttack);
        }

        public void AttackSound()
        {
            //추후 사운드 삽입시 사운드매니저로 이동
        }

        public void SetSelectPlayerPlainStatus(string _playerPlainID)
        {
            moveSpeed = DataManager.Instance.playerShipsDataMap[_playerPlainID].moveSpeed;
            ShootSpeed = DataManager.Instance.playerShipsDataMap[_playerPlainID].shootSpeed;
        }
    }
}
