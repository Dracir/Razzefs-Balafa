using UnityEngine;
using System.Collections;


public class Controller {
	public bool getRun;
	public bool getRunDown;
	public bool getRunUp;
	
	public float lastRunDownTime;
	private bool getRunLast;
	public bool isSpammingRun;
	private readonly float spamResetTimer = 0.3f;
	
	public bool getJump;
	public bool getJumpDown;
	public bool getJumpUp;
	
	private bool getJumpLast;
	private float lastJumpTime;
	private float jumpInputLeeway = 0.2f;
	
	public bool getL;
	public bool getLUp;
	public bool getLDown;
	
	public bool getR;
	public bool getRUp;
	public bool getRDown;
	
	public bool getD;
	public bool getDDown;
	public bool getDUp;
	
	public bool getU;
	public bool getUUp;
	public bool getUDown;
	
	public bool doubleTap;
	public bool aboutFace;

	public bool locked;
	
	public float hAxis;
	public float vAxis;
	
	
	public class ButtonNames {
		public string hAxis = "Horizontal";
		public string vAxis = "Vertical";
		public string jump = "Jump";
		public string run = "Run";
		
		public ButtonNames (int number){
			if (number > 0){
				hAxis += number.ToString();
				vAxis += number.ToString();
				jump += number.ToString();
				run += number.ToString();
			}
		}
	}
	
	private ButtonNames butts;
	private float hAxisLast = 0.0f;
	private float vAxisLast = 0.0f;
	private float getLLastTime = 0f;
	private float getRLastTime = 0f;
	private float doubleTapTime = 0.188f;
	
	private static KeyCode jump;
	private static KeyCode run;
	
	public Controller(int playerNo){
		try{
			jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button0", "Z"));
			run = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button1", "X"));
		}catch(System.Exception){
			jump = KeyCode.Z;
			run = KeyCode.X;
		}

		butts = new ButtonNames(playerNo);
	}
	
	public Controller() : this(0) {
		
	}
	
	public void GetInputs(){
		doubleTap = false;
		if (locked) return;
		
		getRun = Input.GetButton(butts.run) || Input.GetKey(run);
		getRunDown = Input.GetButtonDown(butts.run) || Input.GetKeyDown(run);
		getRunUp = Input.GetButtonUp(butts.run) || Input.GetKeyUp(run);
		
		if (getRunDown){
			isSpammingRun = Time.time - lastRunDownTime < spamResetTimer;
			
			lastRunDownTime = Time.time;
			
		}
		
		getJump = Input.GetButton(butts.jump) || Input.GetKey(jump);
		getJumpUp = Input.GetButtonUp(butts.jump) || Input.GetKeyUp(jump);
		
		if (getJump && !getJumpLast){
			lastJumpTime = Time.time;
		}
		
		getJumpDown = Time.time - lastJumpTime < jumpInputLeeway;
		
		getJumpLast = getJump;
		
		hAxis = Input.GetAxis(butts.hAxis);
		
		getL = hAxis < -0.3f;
		getR = hAxis > 0.3f;
		
		getLDown = (hAxisLast < 0.3f && hAxisLast > -0.3f) && getL;
		getRDown = (hAxisLast < 0.3f && hAxisLast > -0.3f) && getR;
		
		getLUp = hAxisLast < -0.3f && !getL;
		getRUp = hAxisLast > 0.3f && !getR;
		
		if (getLDown){						//check for doubleTap
			if (Time.time - getLLastTime < doubleTapTime){
				doubleTap = true;
			}
			getLLastTime = Time.time;
		}
		
		if (getRDown){
			if (Time.time - getRLastTime < doubleTapTime){
				doubleTap = true;
			}
			getRLastTime = Time.time;
		}
		hAxisLast = hAxis;
		
		VerticalInputs();
	}
	
	public void CursorInput(){
		VerticalInputs();
		
	}
	
	private void VerticalInputs(){
		vAxis = Input.GetAxis(butts.vAxis);
		
		getD = vAxis < -0.3f;
		getU = vAxis > 0.3f;
		
		getDDown = (vAxisLast < 0.3f && vAxisLast > -0.3f) && getD;
		getUDown = (vAxisLast < 0.3f && vAxisLast > -0.3f) && getU;
		
		getDUp = vAxisLast < -0.3f && !getD;
		getUUp = vAxisLast > 0.3f && !getU;
		vAxisLast = vAxis;
	}
	
	public void ResetJumpInput(){
		lastJumpTime = 0;
		getJumpDown = false;
	}
	
}