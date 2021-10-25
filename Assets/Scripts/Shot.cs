using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asakuma
{
    public class Shot : MonoBehaviour
    {
        private Vector3 m_velocity;

        private void Update()
        {
            transform.localPosition += m_velocity;
        }

        public void Init(float angle, float speed)
        {
            var direction = Utils.GetDirection(angle);

            m_velocity = direction * speed;

            // íeÇ™êiçsï˚å¸Çå¸Ç≠ÇÊÇ§Ç…Ç∑ÇÈ
            var angles = transform.localEulerAngles;
            angles.z = angle - 90;
            transform.localEulerAngles = angles;

            Destroy(gameObject, 2);
        }

        //private void OnTriggerEnter2D(Collider2D col)
        //{
        //    if (col.gameObject.tag == "Enemy")
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}