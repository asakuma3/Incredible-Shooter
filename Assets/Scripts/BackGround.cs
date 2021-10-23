using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asakuma
{
    public class BackGround : MonoBehaviour
    {
        public Transform m_player;
        public Vector2 m_limit; // 背景の移動範囲

        private void Update()
        {

            var pos = m_player.localPosition;
            var limit = Utils.m_moveLimit;

            // プレイヤーの位置を0 から 1 の値に置き換える
            var tx = 1 - Mathf.InverseLerp(-limit.x, limit.x, pos.x);
            var ty = 1 - Mathf.InverseLerp(-limit.y, limit.y, pos.y);

            // プレイヤーの現在地から背景の表示位置を算出する
            var x = Mathf.Lerp(-m_limit.x, m_limit.x, tx);
            var y = Mathf.Lerp(-m_limit.y, m_limit.y, ty);

            // 背景の表示位置を更新する
            transform.localPosition = new Vector3(x, y, 0);
        }
    }
}