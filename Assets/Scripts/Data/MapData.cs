#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	MapData
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using System.Collections.Generic;
using UnityEngine;

namespace LoveYouForever
{
	/// <summary>
	/// 物品类型
	/// </summary>
	public enum ItemType
	{
		Null = 0,
		Blue = 1,
		Green = 2,
		Black = 3,
		White = 4,
		Red = 5,
		
		ArrowUp = 6,
		ArrowDown = 7,
		ArrowLeft = 8,
		ArrowRight = 9,
	}

	/// <summary>
	/// 移动方向类型
	/// </summary>
	public enum DirType
	{
		Up = 6,
		Down = 7,
		Left = 8,
		Right = 9,
		Null = -1,
	}

	public struct SquareData
	{
		/// <summary>
		/// 方块类型
		/// </summary>
		public ItemType Type;

		/// <summary>
		/// 移动方向
		/// </summary>
		public DirType Dir;

		/// <summary>
		/// 方块坐标
		/// </summary>
		public Vector2Int Pos;
	}

	public class MapData : Single<MapData>
	{
		public static int MapW = 8;
		public static int MapH = 7;
		
		/// <summary>
		/// 地图数据
		/// </summary>
		public static List<int[]> Map;

		/// <summary>
		/// 行走数据
		/// </summary>
		public static int Walks = 1;

		/// <summary>
		/// 方块数据
		/// </summary>
		public static List<SquareData[]> Squares;

		/// <summary>
		/// 当前场上方块
		/// </summary>
		public static List<Squares> CurSquares = new List<Squares>();

		/// <summary>
		/// 关卡总数
		/// </summary>
		public static int Level;

		/// <summary>
		/// 当前关卡
		/// </summary>
		public static int CurLevel;

		public void Init()
		{
			InitMap();
			InitSquares();
			Level = Map.Count;
		}

		private void InitMap()
		{
			Map = new List<int[]>
			{
				new[]
				{
					0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 9, 5, 0, 7, 0, 0,
					0, 0, 3, 0, 0, 1, 0, 0,
					0, 0, 6, 0, 0, 8, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0,
				},
				new[]
				{
					0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 9, 0, 7, 0, 0,
					0, 0, 0, 0, 7, 0, 0, 0,
					0, 0, 0, 0, 3, 0, 0, 0,
					0, 0, 0, 1, 6, 8, 0, 0,
					0, 0, 0, 0, 2, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0,
				},
				new[]
				{
					0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 7, 0, 0, 0,
					0, 0, 9, 0, 5, 0, 0, 0,
					0, 9, 0, 4, 0, 2, 0, 0,
					0, 0, 6, 0, 0, 0, 0, 0,
					0, 0, 0, 0, 0, 0, 0, 0,
				},
			};
		}

		private void InitSquares()
		{
			Squares = new List<SquareData[]>
			{
				new []
				{
					new SquareData {Type = ItemType.Black, Dir = DirType.Right, Pos = new Vector2Int(1, 3)},
					new SquareData {Type = ItemType.Blue, Dir = DirType.Down, Pos = new Vector2Int(3, 1)},
					new SquareData {Type = ItemType.Red, Dir = DirType.Up, Pos = new Vector2Int(4, 5)},
				},
				new []
				{
					new SquareData {Type = ItemType.Green, Dir = DirType.Right, Pos = new Vector2Int(2, 4)},
					new SquareData {Type = ItemType.Blue, Dir = DirType.Left, Pos = new Vector2Int(6, 1)},
					new SquareData {Type = ItemType.Black, Dir = DirType.Down, Pos = new Vector2Int(3, 2)},
				},
				new []
				{
					new SquareData {Type = ItemType.White, Dir = DirType.Left, Pos = new Vector2Int(3, 5)},
					new SquareData {Type = ItemType.Red, Dir = DirType.Left, Pos = new Vector2Int(4, 4)},
					new SquareData {Type = ItemType.Green, Dir = DirType.Left, Pos = new Vector2Int(5, 5)},
				},
			};
		}

		public void IsWin()
		{
			foreach (var VARIABLE in CurSquares)
			{
				if(!VARIABLE.IsWin)
					return;
			}
			UIManager.Instance.GetPanel<GameUI>("GameUI").onEventExit();
		}

	}
}
