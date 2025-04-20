using Chapter.Base;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Chapter.Command
{
    public class InputHandler : MonoBehaviour
    {
        private Invoker _invoker;
        private PlayerBase _player;
        private Command _attack;

        void Start()
        {
            _invoker = gameObject.AddComponent<Invoker>();
            _player = FindObjectOfType<PlayerBase>();

            _attack = new PlayerAttackCommand(_player);
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
                _invoker.ExecuteCommand(_attack);
        }
    }
}