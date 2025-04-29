using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Strategy
{
    public interface IWeaponStrategy
    {
        void Shoot(Transform shooterTransform);
    }
}
