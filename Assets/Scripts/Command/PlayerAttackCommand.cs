using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.CharacterBase;
using Chapter.Base;
using Unity.VisualScripting;

namespace Chapter.Command
{
    public class PlayerAttackCommand : Command
    {
        private PlayerBase player;

        public PlayerAttackCommand(PlayerBase _player)
        {
            this.player = _player;
        }

        public override void Execute() 
        {
            player.Attack();
        }

    }
}