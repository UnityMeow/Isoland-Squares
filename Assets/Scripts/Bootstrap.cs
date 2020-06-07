#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	Bootstrap
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using System.Runtime.Versioning;

namespace LoveYouForever
{
    public class Bootstrap : GameEntry
    {
        protected override void OnInit()
        {
            base.OnInit();
            //注册信息
        }

        protected override void OnStart()
        {
            base.OnStart();
            DontDestroyOnLoad(this);
            startGame();
        }

        private void startGame()
        {
            UIManager.Instance.ShowPanel<MainUI>("MainUI", "MainUI");
            MapData.Instance.Init();
        }
    }
}