using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chapter.Base;

namespace Chapter.Strategy
{
    public interface IBulletStrategy
    {
        void Initailize(BulletBase bullet);
        void OnUpdate();
        void Trigger(Collider2D collider);
    }
}