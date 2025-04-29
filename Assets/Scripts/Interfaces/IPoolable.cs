using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.ObjectPool
{
    public interface IPoolable
    {
        void OnGet();
        void OnRelease();
    }
}
