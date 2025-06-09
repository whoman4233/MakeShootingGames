using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.State;
using Chapter.Base;

namespace Chapter.CharacterBase
{
    public class PlayerIdleState : IPlayerState
    {
        private PlayerBase Player;

        public void Enter(PlayerBase _player)
        {
            this.Player = _player;
        }
        public void Update()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if(input != Vector2.zero)
            {
                Player.ChangeState(new PlayerMoveState());
            }
        }
        public void Exit()
        {

        }
    }
}
