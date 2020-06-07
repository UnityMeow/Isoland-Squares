#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	GameEntry
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using UnityEngine;

namespace LoveYouForever
{
    /// <summary>
    /// 游戏启动程序
    /// </summary>
    public class GameEntry : MonoBehaviour
    {
        // 待修改
        public GameEntry Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            OnInit();
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnInit()
        { }

        protected virtual void OnStart()
        { }
    }
}