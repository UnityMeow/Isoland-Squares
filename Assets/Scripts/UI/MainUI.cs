#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	MainUI
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LoveYouForever
{
	public class MainUI : UIBase
	{
		private Image stratImage;
		public override void Init()
		{
			base.Init();
			AddEventTrigger("StartButton",EventTriggerType.PointerClick,onEventStart);
			stratImage = GetControl<Image>("StartButton");
		}

		public override void Show()
		{
			base.Show();
			stratImage.raycastTarget = true;
		}

		/// <summary>
		/// 开始按钮
		/// </summary>
		/// <param name="data"></param>
		private void onEventStart(BaseEventData data)
		{
			stratImage.raycastTarget = false;
			Hide(() =>
			{
				UIManager.Instance.ShowPanel<GameUI>("GameUI", "GameUI");
			});
		}
	}
}
