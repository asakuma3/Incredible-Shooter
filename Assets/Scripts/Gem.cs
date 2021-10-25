using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asakuma
{
    public class Gem : MonoBehaviour
    {
        public int m_exp;
        public float m_brake = 0.9f;    //散らばるときの減速量

        private Vector3 m_direction;
        private float m_speed;

        void Update()
        {
            //散らばる処理
            var velocity = m_direction * m_speed;
            transform.localPosition += velocity;
            m_speed *= m_brake;
            //transform.localPosition = Utils.ClampPosition(transform.localPosition); //宝石が画面外に出ない処理
        }

        //宝石が出現する時に初期化
        public void Init(int score, float dropRangeMin, float dropRangeMax)
        {
            var angle = Random.Range(0, 360);
            var f = angle * Mathf.Deg2Rad;  //ラジアンに変換
            m_direction = new Vector3(Mathf.Cos(f), Mathf.Sin(f), 0);   //進行方向のベクトルを作成
            m_speed = Mathf.Lerp(dropRangeMin, dropRangeMax, Random.value);
            Destroy(gameObject, 8);
        }
    }
}