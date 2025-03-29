using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.State
{
    public class PlainStateContext
    {
        public IPlainState CurrentState
        {
            get; set;
        }

        private readonly PlainController _plainController;

        public PlainStateContext(PlainController plainController)
        {
            _plainController = plainController;
        }

        public void Transition()
        {
            CurrentState.Handle(_plainController);
        }

        public void Transition(IPlainState state)
        {
            CurrentState = state;
            CurrentState.Handle(_plainController);
        }
    }
}
