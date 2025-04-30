using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public interface IBossMoveStrategy
    {
        void ExecuteMovement(Transform boss);
    }
}