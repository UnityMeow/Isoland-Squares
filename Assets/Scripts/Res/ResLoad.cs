#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	ResLoad
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
	public class ResLoad : SingleMono<ResLoad>
	{
		public static GameObject LoadPrefab(string resName)
		{
			GameObject go = Resources.Load<GameObject>(resName);
			if (go == null)
			{
				Debug.Log(resName);
				Debug.Log("ResLoad: Resources加载路径加载失败");
				return null;
			}
			return Instantiate(go);
		}
		
		public static T LoadAsset<T>(string pathName, string resName)
			where T : Object
		{
			return Resources.Load<T>(pathName + "/" + resName);
		}
	}
}
