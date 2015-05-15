using UnityEngine;
using System.Collections;

public class MovementTracker : MonoBehaviour {
	
	CharacterMove mover;
	CharacterMoveMoving moving;
	CharacterJump jumper;
	CharacterJumpJumping jumping;
	CharacterJumpFalling falling;
	
	const float ballScale = 0.2f;
	const float killBallTimer = 25f;
	const float ballDropInterval = 1f;
	float ballTimer;
	
	bool grounded;
	bool jeTombe;
	
	bool JustGrounded {
		get{
			return jumper.Grounded && !grounded;
		}
	}

	bool JustJumped {
		get{
			return !jumper.Grounded && grounded;
		}
	}

	bool JustFalling {
		get{
			return !grounded && !jeTombe && jumper.Rigidbody.velocity.y < 0;
		}
	}
	void Start () {
		mover = GetComponent<CharacterMove>();
		moving = GetComponent<CharacterMoveMoving>();
		jumper = GetComponent<CharacterJump>();
		jumping = GetComponent<CharacterJumpJumping>();
		falling = GetComponent<CharacterJumpFalling>();
		
	}
	
	void Update () {
		
		ballTimer += Time.deltaTime;
		if (ballTimer >= ballDropInterval){
			LeaveBall(Color.blue);
		}
		
		if (JustFalling){
			LeaveBall(Color.yellow);
		}
		if (JustJumped){
			LeaveBall(Color.magenta);
		}
		if (JustGrounded){
			LeaveBall(Color.black);
		}
		
		
		jeTombe = jumper.Rigidbody.velocity.y < 0;
		grounded = jumper.Grounded;
	}
	
	
	void LeaveBall(Color colour)
	{
		
		GameObject ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		ball.GetComponent<MeshRenderer>().material.color = colour;
		ball.transform.localScale = Vector3.one * ballScale;
		ball.transform.position = transform.position;
		
		Destroy(ball, killBallTimer);
		
		ballTimer = 0;
	}
}
