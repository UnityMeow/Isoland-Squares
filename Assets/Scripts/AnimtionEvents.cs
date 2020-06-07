#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	AnimtionEvents
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LoveYouForever
{
	public class AnimtionEvents : MonoBehaviour
	{
		public void Test()
		{
			if (MapData.Walks == 1)
			{
				Animation ani = GetComponent<Animation>();
				ani.Play("SnakeIdle");
				gameObject.SetActive(false);
			}
		}
	}
}
