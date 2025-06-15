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

        [Header("�⺻ �̵�/���� ����")]
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
            //���� ���� ���Խ� ����Ŵ����� �̵�
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
            // Ǯ���� ���� �� �ʱ�ȭ �۾� (ex. ü�� ȸ��, ���� ���� ��)

        }

        public void OnRelease()
        {
            // Ǯ�� ��ȯ�� �� �ʱ�ȭ �۾� (ex. ����Ʈ ����, �Ѿ� ���� ��)

        }
    } 
}
