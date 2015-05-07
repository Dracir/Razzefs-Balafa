using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FMG
	{
	public class ButtonToggle : MonoBehaviour {

		//the array of buttons.
		public Button[] buttons;

		public RectTransform selectCursor;
		public bool cursorRightSide = true;
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

		private Slider slider;

		private const string sliderCursorText = ">>";
		private const string buttonCursorText = "();";

		Text myText {
			get{
				return GetComponentInChildren<Text>();
			}
		}

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
			selectCursor = Instantiate<RectTransform>(selectCursor);
			selectCursor.parent = this.m_rectTransform;
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
			Debug.Log ("Changing button");
			if(selectedButton && selectedButton.image != null)
			{
				selectedButton.image.color = unselectedColor;
			}

			if(buttons!=null && index>-1 && index < buttons.Length && buttons[index])
			{
				selectedButton  = buttons[index];
			}




			if(selectedButton && selectedButton.image != null)
			{
				selectedButton.image.color = selectedColor;
			}

			slider = selectedButton.GetComponentInChildren<Slider>();

		}

		void Update () {
			//this is dumb because it apparently sets its own position somewhere in the script making m_orgPos totally dumb
			if(m_rectTransform==null || myText.color.a < 0.9)
			{
				return;
			}
			K_BUTTON_PRESS -= Time.deltaTime;

			//get input for things
			bool next = false;
			bool prev = false;
			float axis = Input.GetAxis("NextPrev");
			//Debug.Log ("My axis is " + axis);
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

			//deal with sliders
			if (slider != null){
				slider.value += Input.GetAxis ("Horizontal") * Time.deltaTime;
			}
		}
		
		void ResetCursor () {
			if (slider != null){
				selectCursor.GetComponent<Text>().text = sliderCursorText;
			} else{
				selectCursor.GetComponent<Text>().text = buttonCursorText;
			}
			RectTransform select = selectedButton.GetComponent<RectTransform>();
			selectCursor.position = select.position;
			selectCursor.position += (Vector3)(cursorRightSide? Vector2.right : -Vector2.right) * (select.rect.width/2 + selectCursor.rect.width/2);
		}
	}
}
