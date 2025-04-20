using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.State;
using Chapter.Base;

namespace Chapter.CharacterBase
{
    public class PlayerMoveState : MonoBehaviour, IPlayerState
    {
        private PlayerBase player;

        public void Enter(PlayerBase _player)
        {
            this.player = _player;
        }

        public void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            player.Move(input);

            if(input == Vector2.zero)
            {
                player.ChangeState(new PlayerIdleState());
            }
        }

        public void Exit() 
        {
            player.Move(Vector2.zero);
        }

    }
}
