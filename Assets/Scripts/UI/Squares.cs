#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	Squares
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LoveYouForever
{
	public class Squares : MonoBehaviour
	{
		public SquareData CurData;
		public Vector2 LeftPoint;
		public bool IsWin;
		public Dictionary<int, SquareData> Walk;
		public Animation Snake;

		public void Start()
		{
			GetComponent<Button>().onClick.AddListener(OnEvent);
			Walk = new Dictionary<int, SquareData>();
			Snake = UIManager.Instance.GetPanel<GameUI>("GameUI").BackSnake;
		}

		/// <summary>
		/// 移动
		/// </summary>
		public void OnEvent()
		{
			Vector2Int pos = CurData.Pos;
			bool b = Obstruct(CurData.Dir);
			if (b)
			{
				MapData.Walks++;
				Snake.gameObject.SetActive(true);
				Snake.Play("SnakeIdle");
			}
			Invoke("win", 0.6f);
		}

		/// <summary>
		/// 回退
		/// </summary>
		/// <param name="num"></param>
		public void Back(int num)
		{
			if (Walk.ContainsKey(num))
			{
				CurData.Pos = Walk[num].Pos;
				SetPos(Walk[num].Dir);
				Walk.Remove(num);
			}
		}

		public void Restar(SquareData data)
		{
			CurData.Pos = data.Pos;
			SetPos(data.Dir);
			Walk.Clear();
			Snake.gameObject.SetActive(false);
		}

		/// <summary>
		/// 被推动
		/// </summary>
		public bool Pushed(DirType dir)
		{
			return Obstruct(dir);
		}

		private Vector2Int Move(DirType dir)
		{
			Vector2Int pos = CurData.Pos;
			switch (dir)
			{
				case DirType.Up:
					if (CurData.Pos.y - 1 >= 0)
					{
						pos = new Vector2Int(CurData.Pos.x, CurData.Pos.y - 1);
					}
					break;
				case DirType.Down:
					if (CurData.Pos.y + 1 < MapData.MapH)
					{
						pos = new Vector2Int(CurData.Pos.x, CurData.Pos.y + 1);
					}
					break;
				case DirType.Left:
					if (CurData.Pos.x - 1 >= 0)
					{
						pos = new Vector2Int(CurData.Pos.x - 1, CurData.Pos.y);
					}
					break;
				case DirType.Right:
					if (CurData.Pos.x + 1 < MapData.MapW)
					{
						pos = new Vector2Int(CurData.Pos.x + 1, CurData.Pos.y);
					}
					break;
			}
			return pos;
		}

		private bool Obstruct(DirType dir)
		{
			var pos = Move(dir);
			var square = IsSquare(pos);

			if (square != null)
			{
				bool b = square.Pushed(dir);
				if(b)
				{
					Walk.Add(MapData.Walks,CurData);
					// 目标成功推动则设置当前位置 否则 不设置
					CurData.Pos = pos;
					SetPos();
					return true;
				}
				return false;
			}
			if (pos == CurData.Pos)
				return false;
			Walk.Add(MapData.Walks,CurData);
			CurData.Pos = pos;
			SetPos();
			return true;
		}

		/// <summary>
		/// 是否有方块阻挡
		/// </summary>
		public Squares IsSquare(Vector2Int pos)
		{
			foreach (var VARIABLE in MapData.CurSquares)
			{
				if (VARIABLE != this && VARIABLE.CurData.Pos == pos)
				{
					return VARIABLE;
				}
			}
			return null;
		}

		private void SetPos(DirType dir = DirType.Null)
		{
			float x = LeftPoint.x + CurData.Pos.x * 88;
			float y = LeftPoint.y - CurData.Pos.y * 88;
			int index = CurData.Pos.x + CurData.Pos.y * MapData.MapW;
			int item = MapData.Map[MapData.CurLevel][index];
			if (item > 5)
			{
				RotateItem(transform.GetChild(0) as RectTransform, item);
				CurData.Dir = (DirType) item;
			}
			else if (dir != DirType.Null)
			{
				RotateItem(transform.GetChild(0) as RectTransform, (int)dir);
				CurData.Dir = dir;
			}

			if (item == (int) CurData.Type && item < 6 && item > 0)
			{
				IsWin = true;
				GetComponent<Image>().sprite = 
					ResLoad.LoadAsset<Sprite>("UI/Map/Target",item.ToString());
			}
			else
			{
				IsWin = false;
				GetComponent<Image>().sprite = 
					ResLoad.LoadAsset<Sprite>("UI/Map/Squares",((int) CurData.Type).ToString());
			}
			(transform.parent as RectTransform).DOLocalMove(new Vector2(x, y), 0.4f);
		}
		
		private void RotateItem(RectTransform rt,int dir)
		{
			float t = 0.5f;
			switch ((ItemType)dir)
			{
				case ItemType.ArrowUp :
					rt.DOLocalRotate(Vector3.zero, t).SetEase(Ease.OutBack);;
					break;
				case ItemType.ArrowDown :
					rt.DOLocalRotate(new Vector3(0,0,180), t).SetEase(Ease.OutBack);
					break;
				case ItemType.ArrowLeft :
					rt.DOLocalRotate(new Vector3(0,0,90), t).SetEase(Ease.OutBack);;
					break;
				case ItemType.ArrowRight :
					rt.DOLocalRotate(new Vector3(0,0,-90), t).SetEase(Ease.OutBack);;
					break;
			}
		}
		
		public void win()
		{
			MapData.Instance.IsWin();
		}
	}
}
