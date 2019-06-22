/***
*	
*	Title：自动战斗场景测试版快速开发
*	        脚本挂在Role角色prefab上
*	Description:
*	        所有角色的基类，
*	       英雄相关的操作
*	       1、动画控制
*	       2、攻击选中目标
*	       3、伤害计算
*	       4、技能相关操作
*	            技能释放、怒气值的积累等
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
	public class BaseRole : MonoBehaviour 
	{
#region 基本属性及对属性的操作
        [SerializeField]
        private int hp;
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        [SerializeField]
        private int maxHp;
        public int MaxHP
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        [SerializeField]
        private int ap;         //怒气值，满100释放技能
        public int AP
        {
            get { return ap; }
            set { ap = value; }
        }
        [SerializeField]
        private int maxAp=100;
        public int MaxAP
        {
            get { return maxAp; }
            private set { maxAp = value; }
        }
        [SerializeField]
        private int attack;
        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }
        [SerializeField]
        private int deffence;
        public int Deffence
        {
            get { return deffence; }
            set { deffence = value; }
        }

        /// <summary>
        /// 恢复HP
        /// </summary>
        /// <param name="value"></param>
        private void AddHP(int value)
        {
            if (HP + value > MaxHP)
            {
                HP = MaxHP;
            }
            else
            {
                HP += value;
            }
        }

        /// <summary>
        /// 减少HP
        /// </summary>
        /// <param name="value"></param>
        private void SubHP(int value)
        {
            if (HP <= value)
            {
                HP = 0;
                //执行死亡操作
                HeroDead();
            }
            else
            {
                HP -= value;
                Debug.Log(gameObject.name+"HP-"+value);
            }
        }
        /// <summary>
        /// 攻击或者被攻击，增加ap值
        /// </summary>
        /// <param name="value"></param>
        private void AddAP(int value)
        {
            if (AP + value > MaxAP)
            {
                AP = MaxAP;
            }
            else
            {
                AP += value;
            }
        }
        /// <summary>
        /// 释放技能，清空AP
        /// </summary>
        private void ClearAP()
        {
            AP = 0;
        }

        private void HeroDead()
        {
            //死亡动作
            //TODO
            //销毁死亡对象
            Debug.Log(gameObject.name+"死了");
            DestroyImmediate(this.gameObject);
        }
        #endregion

        public BaseRole(int hp,int maxhp,int ap,int maxap,int attack,int deffence)
        {
            this.HP = hp;
            this.MaxAP = maxap;
            this.AP = ap;
            this.MaxAP = maxap;
            this.Attack = attack;
            this.Deffence = deffence;
        }

        /// <summary>
        /// 攻击操作
        /// </summary>
        public void AttackEnemy(BaseRole enemy)
        {
            //调用攻击动画
            //TODO
            //执行攻击相关操作
            Debug.Log(gameObject.name+"攻击"+enemy.gameObject.name);
            AddAP(20);                      //增加怒气值
            enemy.BeAttack(Attack);         //调用敌人的被攻击操作
        }

        /// <summary>
        /// 被攻击
        /// 1、减血
        /// 2、增加怒气
        /// </summary>
        public void BeAttack(int attack)
        {
            //减血，敌方攻击力-我方防御力=伤害值
            SubHP(attack-deffence);
            //增加怒气
            AddAP(20);
        }
	}
}
