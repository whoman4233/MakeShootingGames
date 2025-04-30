using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Data
{
    [CreateAssetMenu(menuName = "Data/SpriteData")]
    public class SpriteData : ScriptableObject
    {
        public Sprite[] playerSprite;
        public Sprite[] enemySprite;
        public Sprite[] bulletSprite;
        public Sprite[] bossSprite;
    }
}
