using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Manager
{
    public class BackGroundManager : MonoBehaviour
    {
        public float scrollSpeed = 2f;
        public float resetYPosition;
        public float startYPosition;

        void Start()
        {
            float spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
            resetYPosition = -spriteHeight;
            startYPosition = spriteHeight;
        }

        private void Update()
        {
            // 배경을 아래로 이동
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

            // 화면 아래로 벗어나면 위로 이동시켜 루프
            if (transform.position.y <= resetYPosition)
            {
                Vector3 newPos = transform.position;
                newPos.y = startYPosition;
                transform.position = newPos;
            }
        }
    }
}
