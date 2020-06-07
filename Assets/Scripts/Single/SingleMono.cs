#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	SingleMono
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoveYouForever
{
	public class SingleMono<T> : MonoBehaviour
		where T : SingleMono<T>
	{
		static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					GameObject go = new GameObject("Singleton" + typeof(T));
					DontDestroyOnLoad(go);
					instance = go.AddComponent<T>();
				}
				return instance;
			}
		}
	}
}
