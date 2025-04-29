using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.CharacterBase;
using Chapter.Base;
using Unity.VisualScripting;

namespace Chapter.Command
{
    public class PlayerMoveCommand : Command
    {
        private PlayerBase player;
        private Vector2 directon;

        public PlayerMoveCommand(PlayerBase _player, Vector2 directon)
        {
            this.player = _player;
            this.directon = directon;
        }

        public override void Execute()
        {
            player.Move(directon);
        }

    }
}