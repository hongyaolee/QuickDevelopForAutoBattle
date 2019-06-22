/***
*	
*	Title：自动战斗场景测试版快速开发
*	
*	Description:
*	       实现战斗逻辑
*	       1、遍历敌我双方每一个位置，如果该位置有英雄，则执行该英雄的攻击或者技能操作
*	       2、英雄的攻击目标锁定，判断每一个位置当前要攻击的敌人
*	       3、英雄攻击锁定的目标(攻击方法在英雄基类实现，这里调用)
*	       4、计算伤害并减少血量
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
    public class BattleScript : MonoBehaviour
    {
        private BaseStance[] HeroList;              //英雄列表，即上阵英雄配置列表
        private BaseStance[] EnemyList;             //敌人列表
        private List<BaseStance> AllRoleList = new List<BaseStance>();       //所有角色列表，包括敌我双方
        private BaseStance attackTarget;            //本回合要攻击的目标
        bool gameover = false;
        private void Start()
        {
            HeroList = GameObject.Find("BattleController/HeroTeam").GetComponentsInChildren<BaseStance>();
            EnemyList = GameObject.Find("BattleController/EnemyTeam").GetComponentsInChildren<BaseStance>();
            //分配列表位置编号
            for (int i = 0; i < 6; i++)
            {
                HeroList[i].stanceNum = (StanceNum)i;
            }
            for (int i = 0; i < 6; i++)
            {
                EnemyList[i].stanceNum = (StanceNum)i;
            }
            //战斗场景所有角色列表，用于执行战斗序列
            for (int i = 0; i < 6; i++)
            {
                AllRoleList.Add(HeroList[i]);
                AllRoleList.Add(EnemyList[i]);
            }
        }

        private void Update()
        {
            //if (!gameover)
            //{
            //    BattleAction();
            //}
        }

        /// <summary>
        /// 执行战斗操作
        /// </summary>
        private void BattleAction()
        {
            int heroNum=6;
            int enemyNum=6;

            foreach (BaseStance stance in AllRoleList)
            {
                //当前位置的角色还没死亡
                if (stance.transform.childCount > 0)
                {
                    //我方攻击
                    if (stance.tag == GlobalParameter.TAG_HERO)
                    {
                        AttackAction(stance, EnemyList);
                    }
                    //敌方攻击
                    else if (stance.tag == GlobalParameter.TAG_ENEMY)
                    {
                        AttackAction(stance, HeroList);
                    }

                }
                //计算双方伤亡情况，有一方全部死亡则结束战斗
                else
                {
                    if (stance.tag == GlobalParameter.TAG_HERO)
                        heroNum--;
                    else if (stance.tag == GlobalParameter.TAG_ENEMY)
                        enemyNum--;
                    Debug.Log("我方剩余"+heroNum+",敌方剩余"+enemyNum);
                }

            }
            if (heroNum == 0)
            {
                //执行失败操作
                Debug.Log("失败！");
                gameover = true;
            }
            else if(enemyNum == 0)
            {
                //执行胜利操作
                Debug.Log("胜利！");
                gameover = true;
            }
            
            
        }
        /// <summary>
        /// 攻击动作
        /// </summary>
        /// <param name="currentStance">当前轮到的准备攻击的序列</param>
        /// <param name="enemyList">攻击列表</param>
        private void AttackAction(BaseStance currentStance,BaseStance[] enemyList)
        {
            //确定攻击目标,按照攻击列表的顺序，如果找到攻击目标，则返回
            for (int i = 0; i < currentStance.AttackList.Count; i++)
            {
                BaseStance enemyStance = enemyList[(int)currentStance.AttackList[i]];
                if (enemyStance.transform.childCount > 0)
                {
                    BaseRole role = currentStance.transform.GetChild(0).GetComponent<BaseRole>();
                    BaseRole enemy = enemyStance.transform.GetChild(0).GetComponent<BaseRole>();

                    //执行攻击操作
                    role.AttackEnemy(enemy);
                    break;
                }

            }
        }


    }
}
