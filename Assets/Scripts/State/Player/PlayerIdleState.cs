using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class State : MonoBehaviour, IPlainState
    {
        private PlainController plainController;

        void Start(PlainController _plainController) 
        {
            if(!plainController)
            {
                plainController = _plainController;
            }



        }
        void Update () { }
        
        void Exit() { }
    }
}