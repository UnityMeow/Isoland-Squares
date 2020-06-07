#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	GameUI
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LoveYouForever
{
	public class GameUI : UIBase
	{
		private Transform mapTr;
		private RectTransform leftPoint;
		private RectTransform restart;
		public Animation BackSnake;
		public override void Init()
		{
			base.Init();
			GetControl<Button>("ButtonExit").onClick.AddListener(onEventExit);
			BackSnake = GetGameObject("SnakeAnim").
				GetComponent<Animation>();
			mapTr = GetGameObject("Map").transform;
			leftPoint = GetGameObject("LeftPoint").transform as RectTransform;
			GetControl<Button>("ButtonBack").onClick.AddListener(OnEventBack);
			GetControl<Button>("ButtonRestar").onClick.AddListener(OnEventRestar);
			restart = GetGameObject("RestarSnake").transform as RectTransform;
		}

		public override void Show()
		{
			base.Show();
			MapData.CurLevel = Random.Range(0, MapData.Level);
			LoadLevel();
			LoadSquares();
		}
		
		private void OnEventBack()
		{
			if(MapData.Walks == 1)
				return;
			BackSnake.Play("SnakeMove");
			MapData.Walks--;
			foreach (var VARIABLE in MapData.CurSquares)
			{
				VARIABLE.Back(MapData.Walks);
			}
		}

		private void OnEventRestar()
		{
			MapData.Walks = 1;
			var data = MapData.Squares[MapData.CurLevel];
			for (int i = 0; i < MapData.CurSquares.Count; i++)
			{
				MapData.CurSquares[i].Restar(data[i]);
			}

			restart.DOLocalRotate(new Vector3(0, 0, 360), 0.35f,RotateMode.FastBeyond360);
		}
		
		public void onEventExit()
		{
			Hide(()=>
			{
				UIManager.Instance.ShowPanel<MainUI>("MainUI", "MainUI");
				ClearMap();
			});
		}

		private void LoadLevel()
		{
			int[] data = MapData.Map[MapData.CurLevel];
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] > 0)
				{
					string resName = data[i] < 6 ? data[i].ToString() : "6";
					GameObject go = AddItem("Item", resName, i % MapData.MapW, i / MapData.MapW);
					if (data[i] > 5)
						RotateItem(go.transform as RectTransform, data[i]);
				}
			}
		}
		
		private void LoadSquares()
		{
			var data = MapData.Squares[MapData.CurLevel];
			for (int i = 0; i < data.Length; i++)
			{
				SquareData squareData = data[i];
				int res = (int) squareData.Type;
				GameObject go = AddItem("Squares", res.ToString(), squareData.Pos.x, squareData.Pos.y);
				RotateItem(go.transform.Find("Bg/Dir") as RectTransform, (int) squareData.Dir);
				Squares square = go.transform.GetChild(0).gameObject.AddComponent<Squares>();
				square.CurData = squareData;
				square.LeftPoint = leftPoint.localPosition;
				MapData.CurSquares.Add(square);
			}
		}

		private GameObject AddItem(string pName,string sName, int ix,int iy)
		{
			GameObject go = UIManager.Instance.LoadUIPrefab(pName,mapTr,pName);
			go.transform.GetChild(0).GetComponent<Image>().sprite = 
				ResLoad.LoadAsset<Sprite>($"UI/Map/{pName}",sName);
			float x = leftPoint.localPosition.x + ix * 88;
			float y = leftPoint.localPosition.y - iy * 88;
			(go.transform as RectTransform).localPosition = new Vector2(x,y);
			return go;
		}
		
		private void RotateItem(RectTransform rt,int dir)
		{
			switch ((ItemType)dir)
			{
				case ItemType.ArrowUp :
					rt.DOLocalRotate(Vector3.zero, 0);
					break;
				case ItemType.ArrowDown :
					rt.DOLocalRotate(new Vector3(0,0,180), 0);
					break;
				case ItemType.ArrowLeft :
					rt.DOLocalRotate(new Vector3(0,0,90), 0);
					break;
				case ItemType.ArrowRight :
					rt.DOLocalRotate(new Vector3(0,0,-90), 0);
					break;
			}
		}

		private void ClearMap()
		{
			for (int i = mapTr.childCount - 1; i > 0; i--)
			{
				GameObject.Destroy(mapTr.GetChild(i).gameObject);
			}
			MapData.CurSquares.Clear();
			MapData.Walks = 1;
			BackSnake.gameObject.SetActive(false);
		}
	}
}
