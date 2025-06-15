using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.State;
using Chapter.CharacterBase;
using Chapter.Data;
using Chapter.Event;
using Chapter.ObjectPool;
using Chapter.Strategy;
using Chapter.Singleton;
using Chapter.Manager;

namespace Chapter.Base
{
    public class PlayerBase : MonoBehaviour, IPoolable
    {
        IPlayerState currentState;

        private IPlayerState
            _IdleState, _MoveState;

        private IWeaponStrategy attackStrategy;

        private IEventBus<PlayerEventType> _eventBus;

        float lastShootTime = 0f;

        [Header("기본 이동/공격 설정")]
        public float moveSpeed = 5f;
        public float ShootSpeed;

        private SpriteRenderer sp;

        public int HP;

        public void Awake()
        {
            _IdleState = new PlayerIdleState();
            _MoveState = new PlayerMoveState();
            sp = GetComponent<SpriteRenderer>();
            _eventBus = new PlayerEventBus();

            GameManager.Instance._playerGameObject = this.gameObject;

            ChangeState(new PlayerIdleState());
        }

        private void OnDisable()
        {
        }

        public void ChangeState(IPlayerState state)
        {
            currentState?.Exit();
            currentState = state;
            currentState.Enter(this);
        }

        private void Update()
        {
            currentState?.Update();
        }

        public void Move(Vector2 direction)
        {
            transform.Translate(direction *  moveSpeed * Time.deltaTime);
        }
        
        public void Attack()
        {
            if(Time.time - lastShootTime > ShootSpeed)
            {
                attackStrategy?.Shoot(this.gameObject.transform);
                lastShootTime = Time.time;
            }
        }

        public void AttackSound()
        {
            //추후 사운드 삽입시 사운드매니저로 이동
        }

        public void ApplyLoadout(PlayerLoadout lo)
        {
            HP = lo.maxHp;
            moveSpeed = lo.moveSpeed;
            ShootSpeed = lo.shootDelay;
            attackStrategy = lo.weapon;
            sp.sprite = lo.sprite;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "EnemyBullet")
            {
                EventBusManager.Instance.playerEventBus.Publish(PlayerEventType.OnHit);
                PoolSystemManager.Instance.ReleaseEnemyBullet(collision.gameObject.GetComponent<EnemyBulletBase>());
            }
        }

        public void OnGet()
        {
            // 풀에서 꺼낼 때 초기화 작업 (ex. 체력 회복, 상태 리셋 등)

        }

        public void OnRelease()
        {
            // 풀로 반환될 때 초기화 작업 (ex. 이펙트 끄기, 총알 리셋 등)

        }
    } 
}
