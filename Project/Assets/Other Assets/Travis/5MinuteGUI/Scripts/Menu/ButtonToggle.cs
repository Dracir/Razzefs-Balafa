using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FMG
	{
	public class ButtonToggle : MonoBehaviour {

		//the array of buttons.
		public Button[] buttons;

		public GameObject selectCursor;
		//the selected button
		public Button selectedButton;

		//the unselected color
		public Color unselectedColor = Color.white;

		//the selected color.
		public Color selectedColor = Color.green;


		private int m_selectedIndex=0;
		private bool m_isAxisInUse;

		private RectTransform m_rectTransform;
		private Vector3 m_orgPos;
		private Image cursor;

		//use the button toggle.
		public bool useButtonToggle = true;

		private static float K_BUTTON_PRESS = 0f;
		void Start () {	
			ButtonToggle.K_BUTTON_PRESS=0;
			if(useButtonToggle==false)
			{
				Destroy(this);
			}else{
				init ();
			}

			Transform cursorObject = Instantiate(selectCursor).transform;
			cursor = cursorObject.GetComponent<Image>();
			cursor.transform.parent = this.transform;
			ResetCursor();
		}
		void init()
		{
			selectIndex(0);
			RectTransform rt = gameObject.GetComponent<RectTransform>();
			m_rectTransform=rt;
			if(rt)
			{
				m_orgPos = rt.position;
			}
		}

		public void selectIndex(int index)	
		{
			if(selectedButton)
			{
				selectedButton.image.color = unselectedColor;
			}

			if(buttons!=null && index>-1 && index < buttons.Length && buttons[index])
			{
				selectedButton  = buttons[index];
			}




			if(selectedButton)
			{
				selectedButton.image.color = selectedColor;
			}
		}

		void Update () {
			//this is dumb because it apparently sets its own position somewhere in the script making m_orgPos totally dumb
			if(m_rectTransform==null || )
			{
				return;
			}
			K_BUTTON_PRESS -= Time.deltaTime;

			//get input for things
			bool next = false;
			bool prev = false;
			float axis = Input.GetAxis("NextPrev");
			Debug.Log ("axis is " + axis);
			if(axis != 0)
			{
				if(m_isAxisInUse == false)
				{
					if (axis > 0){
						prev = true;
					} else {
						next = true;
					}
					m_isAxisInUse = true;
					Debug.Log ("Changing things!");
				} else {
					Debug.Log ("Not changing");
				}
			}
			if(Input.GetAxisRaw("NextPrev") == 0)
			{
				m_isAxisInUse = false;
			}    

			if(Input.GetButtonDown("SelectButton"))
			{
				if(K_BUTTON_PRESS<=0)
				{
					K_BUTTON_PRESS = 0.1f;

					PointerEventData pointer = new PointerEventData(EventSystem.current);
					if(selectedButton!=null)
					{
						Debug.Log ("ButtonToggle:PRESS");

						ExecuteEvents.Execute(selectedButton.gameObject, pointer, ExecuteEvents.pointerClickHandler);
					}
				}else{
					Debug.Log ("m_buttonPress" + K_BUTTON_PRESS);
				}
			}
			if(prev)
			{
				m_selectedIndex--;
				if(m_selectedIndex<0)
				{
					m_selectedIndex=buttons.Length-1;
				}
				selectIndex(m_selectedIndex);
				ResetCursor();
			}
			if(next)
			{

				m_selectedIndex++;
				if(m_selectedIndex>buttons.Length-1)
				{
					m_selectedIndex=0;
				}
				selectIndex(m_selectedIndex);
				ResetCursor();
			}
		}
		
		void ResetCursor () {
			RectTransform select = selectedButton.GetComponent<RectTransform>();
			RectTransform curs = cursor.rectTransform;
			curs.position = select.position;
			curs.position -= (Vector3)Vector2.right * (select.rect.width/2 + curs.rect.width/2);
		}
	}
}
