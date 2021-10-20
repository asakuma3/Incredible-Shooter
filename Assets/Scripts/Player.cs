using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Asakuma
{
    public class Player : MonoBehaviour
    {
        public float m_speed;   //プレイヤーの移動スピード
        public Shot m_shotPrefab;
        public float m_shotSpeed;
        public float m_shotAngleRange;  //ショットの角度幅
        public float m_shotTimer;   //弾の発射タイミングを管理
        public int m_shotCount; //発射数
        public float m_shotInterval;

        public int m_hpMax;
        public int m_hp;

        private void Awake()
        {
            m_hp = m_hpMax;
        }

        private void Update()
        {

            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            var velocity = new Vector3(h, v) * m_speed;
            transform.localPosition += velocity;

            // プレイヤーが画面外に出ないように位置を制限する
            transform.localPosition = Utils.ClampPosition(transform.localPosition);

            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            var direction = Input.mousePosition - screenPos;

            // マウスカーソルが存在する方向の角度を取得する
            var angle = Utils.GetAngle(Vector3.zero, direction);

            // プレイヤーがマウスカーソルの方向を見るようにする
            var angles = transform.localEulerAngles;
            angles.z = angle - 90;
            transform.localEulerAngles = angles;

            m_shotTimer += Time.deltaTime;

            if (m_shotTimer < m_shotInterval) return;

            m_shotTimer = 0;
            ShotNWay(angle, m_shotAngleRange, m_shotSpeed, m_shotCount);
        }

        //弾を発射する関数
        private void ShotNWay(float angleBase, float angleRange, float speed, int count)
        {
            var pos = transform.localPosition;
            var rot = transform.localRotation;
            if (1 < count)
            {
                for (int i = 0; i < count; i++)
                {
                    var angle = angleBase + angleRange * ((float)i / (count - 1) - 0.5f);
                    var shot = Instantiate(m_shotPrefab, pos, rot);
                    shot.Init(angle, speed);
                }
            }
            else if (count == 1)
            {
                var shot = Instantiate(m_shotPrefab, pos, rot);
                shot.Init(angleBase, speed);
            }
        }

        //プレイヤーが受けるダメージ計算
        public void Damage(int damage)
        {
            m_hp -= damage;
            if (0 < m_hp) return;
            gameObject.SetActive(false);
        }
    }
}