#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	Single
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LoveYouForever
{
	public class Single<T>
		where T : class
	{
		private static T instance;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					Type type = typeof(T);
					instance = Activator.CreateInstance(type,true) as T;
				}
				return instance;
			}
		}
		protected Single()
		{ }
	}
}
