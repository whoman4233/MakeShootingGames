using Chapter.Base;
using System.Windows.Input;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Chapter.Command
{
    public class InputHandler : MonoBehaviour
    {
        private Invoker _invoker;
        private PlayerBase _player;
        private Command 
            _attack, _move;
        private Vector2 _direction;

        void Start()
        {
            _invoker = gameObject.AddComponent<Invoker>();
            _player = FindObjectOfType<PlayerBase>();

            _attack = new PlayerAttackCommand(_player);
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
                _invoker.ExecuteCommand(_attack);

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if(input != null)
            {
                _move = new PlayerMoveCommand(_player, input);
                _invoker.ExecuteCommand(_move);
            }


        }
    }
}