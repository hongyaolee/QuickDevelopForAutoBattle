/***
*	
*	Title：自动战斗场景测试版快速开发
*	
*	Description:
*	       英雄站位控制，挂在队伍的位置对象上
*	       功能：锁定本位置英雄当前回合应该攻击的敌人，并执行攻击操作
*
*	Author:hongyaolee
*
*	Date:2019.6
*
*	Version:1.0
***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    /// <summary>
    /// 站位编号
    /// </summary>
    public enum StanceNum
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

	public class BaseStance : MonoBehaviour 
	{
        public StanceNum stanceNum;
        public List<StanceNum> AttackList = new List<StanceNum>();
        private void Start()
        {
            InitAttackList();
            InitOrderInLayer();

            //Debug.Log("Init");
        }

        /// <summary>
        /// 初始化攻击列表
        /// 每个位置单独维护一个优先级列表，按照该列表进行攻击
        /// </summary>
        private void InitAttackList()
        {
            switch (stanceNum)
            {
                case StanceNum.One:
                case StanceNum.Four:
                    AttackList.Add(StanceNum.One);
                    AttackList.Add(StanceNum.Two);
                    AttackList.Add(StanceNum.Three);
                    AttackList.Add(StanceNum.Four);
                    AttackList.Add(StanceNum.Five);
                    AttackList.Add(StanceNum.Six);
                    break;
                case StanceNum.Two:
                case StanceNum.Five:
                    AttackList.Add(StanceNum.Two);
                    AttackList.Add(StanceNum.One);
                    AttackList.Add(StanceNum.Three);
                    AttackList.Add(StanceNum.Five);
                    AttackList.Add(StanceNum.Four);
                    AttackList.Add(StanceNum.Six);
                    break;
                case StanceNum.Three:
                case StanceNum.Six:
                    AttackList.Add(StanceNum.Three);
                    AttackList.Add(StanceNum.Two);
                    AttackList.Add(StanceNum.One);
                    AttackList.Add(StanceNum.Six);
                    AttackList.Add(StanceNum.Five);
                    AttackList.Add(StanceNum.Four);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 调整当前位置角色的图层深度
        /// </summary>
        private void InitOrderInLayer()
        {
            if (transform.childCount > 0)
            {
                GetComponentInChildren<SpriteRenderer>().sortingOrder = (int)stanceNum%3;
                Debug.Log(GetComponentInChildren<SpriteRenderer>().sortingOrder);
            }
        }
       

	}
}
