using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class Movable : MonoBehaviour {
	public LayerMask tempMask;
	public bool debug;
	const float gravity = 9.8f;

	protected bool hasGravity;

	protected float maxWalkSpeed;
	protected float maxRunSpeed;
	protected float timeToMax;
	protected float jumpImpulse;
	protected float maxfallSpeed;
	protected float gravityModifier;
	protected float timeToStop;

	//movement property getters

	protected virtual float MaxWalkSpeed{
		get{
			return maxWalkSpeed;
		}
	}
	
	protected virtual float Gravity {
		get{
			if (!hasGravity){
				return 0;
			}
			return gravity * (gravityModifier != 0? gravityModifier : 1);
			
		}
	}
	
	protected virtual float JumpImpulse {
		get{
			return jumpImpulse;
		}
	}
	
	protected virtual float WalkAcceleration {
		get{
			return maxWalkSpeed / timeToMax;
		}
	}
	
	protected virtual float Deceleration {
		get{
			return maxWalkSpeed / timeToStop;
		}
	}

	//rapid accessors
	protected Transform t; 
	protected SpriteRenderer sr;
	protected BoxCollider2D bc;

	protected Vector2 velocity;
	
	protected bool grounded;
	protected float groundedRayCastDistance = 0.2f;
	
	protected Controller controller = new Controller();
	protected Rect box; //used to make calculations without having to get shit like "bc.size.x + transform.position.x + bc.origin.x"
	// Use this for initialization

	protected void SetVariables (bool hasGravity, float maxWalkSpeed, float maxRunSpeed, float timeToMax, float jumpImpulse, float maxfallSpeed, float gravityModifier, float timeToStop)
	{
		this.hasGravity = hasGravity;
		this.maxWalkSpeed = maxWalkSpeed;
		this.maxRunSpeed = maxRunSpeed;
		this.timeToMax = timeToMax;
		this.jumpImpulse = jumpImpulse;
		this.maxfallSpeed = maxfallSpeed;
		this.gravityModifier = gravityModifier;
		this.timeToStop = timeToStop;
	}
	

	protected virtual void Start () {
		t = transform;
		sr = GetComponent<SpriteRenderer>();
		bc = GetComponent<BoxCollider2D>();
		

	}
	
	// Update is called once per frame
	protected virtual void Update () {
		//get my bounds to use later - and it should stay a step ahead of me
		box = new Rect(
			bc.bounds.min.x + velocity.x * Time.deltaTime,
			bc.bounds.min.y + velocity.y * Time.deltaTime,
			bc.size.x,
			bc.size.y);
		Debug.DrawLine(box.min, box.max);

	}

	protected virtual void ApplyGravity () {
		if (!grounded){
			velocity += -Vector2.up * Gravity * Time.deltaTime;
		}

		float checkDistance = box.height / 2;

		if (grounded){
			checkDistance += groundedRayCastDistance;
		} else {
			checkDistance -= velocity.y * Time.deltaTime;
		}

		if (checkDistance >= box.height / 2){ 	// if check distance is smaller than it originally was,
												//  it means I'm moving up and don't want to check distance
			Debug.Log ("So I am calling raycheck that's fsho");
			RaycastHit2D[] rayInfo = RayCheck(Vector3.down, checkDistance, box.width, box.center, tempMask, 6);
			bool collided = false;
			float smallestDistance = Mathf.Infinity;
			int smallestRay = -1;
			int i = 0;
			foreach (var item in rayInfo) {
				if (item.distance > 0){
					collided = true;
					if (item.distance < smallestDistance){
						smallestDistance = item.distance;
						smallestRay = i;
						Debug.Log ("found something!");
					}
				}
				i ++;
			}

			if (collided && !grounded){
				grounded = true;
				float heartToFoot = t.position.y - bc.bounds.min.y;
				//for this we use bc.bounds 'cuz box is a step ahead
				t.position = new Vector3(t.position.x, rayInfo[smallestRay].point.y + heartToFoot, t.position.z);
				velocity = new Vector2(velocity.x, 0);
				
			} else if(!collided){
				grounded = false;
			}
		}
	}

	protected virtual Vector2 Jump () {
		grounded = false;
		return new Vector2(velocity.x, JumpImpulse);
		
	}

	protected virtual Vector2 Walk (int input) {
		float xSpeed = velocity.x;
		
		if (input != 0){
			xSpeed += WalkAcceleration * input * Time.deltaTime;
			
			xSpeed = Mathf.Clamp(xSpeed, -MaxWalkSpeed, MaxWalkSpeed);
		}
		
		if ((xSpeed > 0 && input <= 0) || (xSpeed < 0 && input >= 0)){
			
			int modifier = xSpeed > 0? 1 : -1;
			xSpeed -= Deceleration * Time.deltaTime * modifier;
			if (Mathf.Abs(xSpeed) < Deceleration * Time.deltaTime){
				xSpeed = 0;
			}
			
		}
		if (xSpeed != 0){
			int modifier = xSpeed > 0? 1 : -1;
				//we use box.height * 0.9 for 'breadth' because we don't want to check the very bottom of the box, 'cuz it'll find the ground always
			RaycastHit2D[] rayInfo = RayCheck(
					Vector3.right * modifier, 
					box.width / 2 + Mathf.Abs(xSpeed * Time.deltaTime), 
					box.height * 0.9f, 
					box.center, 
					tempMask, 
					8
			);
			
			bool collided = false;
			
			float smallestDistance = Mathf.Infinity;
			int smallestRay = -1;
			int i = 0;
			foreach (RaycastHit2D ray in rayInfo) {
				if (ray.distance > 0){
					collided = true;
					if (ray.distance < smallestDistance){
						smallestDistance = ray.distance;
						smallestRay = i;
					}
				}
				i ++;
			}
			
			if (collided){
				t.position = new Vector3(
					rayInfo[smallestRay].point.x - (t.position.x - box.min.x) * modifier,
					t.position.y, t.position.z);
				velocity = new Vector2(0, velocity.y);
			}
		}
		
		
		return new Vector2(xSpeed, velocity.y);
	}

	protected virtual void ApplyVelocity () {
		t.position += (Vector3) velocity * Time.deltaTime;
	}

	protected RaycastHit2D[] RayCheck (Vector3 direction, float distance, float breadth, Vector3 origin, int layermask, int number){
		
		RaycastHit2D[] rayHits = new RaycastHit2D[number];
		Vector3 side1 = origin + new Vector3(direction.y, -direction.x, 0).normalized * breadth / 2;
		Vector3 side2 = origin + new Vector3(-direction.y, direction.x, 0).normalized * breadth / 2;
		
		for (int i = 0; i < number; i ++){
			Vector3 o = Vector3.Lerp (side1, side2, (float)i / (float) (number - 1));

			rayHits[i] = Physics2D.Raycast(o, direction, distance, layermask);
			if (debug){
				Debug.DrawLine (o, o + (direction * distance));
			}
		}
		
		return rayHits;
	}
}
