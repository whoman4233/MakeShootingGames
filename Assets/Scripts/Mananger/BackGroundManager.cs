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
            // ����� �Ʒ��� �̵�
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

            // ȭ�� �Ʒ��� ����� ���� �̵����� ����
            if (transform.position.y <= resetYPosition)
            {
                Vector3 newPos = transform.position;
                newPos.y = startYPosition;
                transform.position = newPos;
            }
        }
    }
}
