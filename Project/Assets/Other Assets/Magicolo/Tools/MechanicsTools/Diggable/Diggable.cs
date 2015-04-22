using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.MechanicsTools;

namespace Magicolo {
	[RequireComponent(typeof(SpriteRenderer))]
	[AddComponentMenu("Magicolo/Mechanics/Diggable")]
	public class Diggable : MonoBehaviourExtended {

		[SerializeField, PropertyField]
		Sprite map;
		public Sprite Map {
			get {
				return map;
			}
			set {
				map = value;
				
				UpdateMap();
			}
		}

		[SerializeField, PropertyField(typeof(LayerAttribute))]
		int layer;
		public int Layer {
			get {
				return layer;
			}
			set {
				layer = value;
			}
		}
		
		[SerializeField, PropertyField(typeof(MinAttribute))]
		float scale = 0.1F;
		public float Scale {
			get {
				return scale;
			}
			set {
				scale = value;
				
				UpdateMap();
			}
		}
		
		[SerializeField, Range(0, 1)]
		float alphaThreshold = 0.5F;
		public float AlphaThreshold {
			get {
				return alphaThreshold;
			}
			set {
				alphaThreshold = value;
			}
		}
		
		[SerializeField, PropertyField] 
		FogOfWar fogOfWar;
		public FogOfWar FogOfWar {
			get {
				return fogOfWar;
			}
			set {
				fogOfWar = value;
				
				UpdateFogOfWar();
			}
		}
		
		[SerializeField]
		Pool fxPool;
		public Pool FxPool {
			get {
				return fxPool;
			}
			set {
				fxPool = value;
			}
		}
	
		[SerializeField, HideInInspector] 
		int width;
		public int Width {
			get {
				return width;
			}
		}

		[SerializeField, HideInInspector] 
		int height;
		public int Height {
			get {
				return height;
			}
		}

		[SerializeField, HideInInspector] 
		Texture2D mapTexture;
		public Texture2D MapTexture {
			get {
				return mapTexture;
			}
		}

		DiggableZoneManager zoneManager;
		public DiggableZoneManager ZoneManager {
			get {
				return zoneManager ? zoneManager : (zoneManager = gameObject.FindOrAddChild("Zone Manager").GetOrAddComponent<DiggableZoneManager>());
			}
		}
		
		GameObject fxManager;
		public GameObject FxManager {
			get {
				return fxManager ? fxManager : (fxManager = gameObject.FindOrAddChild("FX Manager"));
			}
		}
	
		GameObject colliderManager;
		public GameObject ColliderManager {
			get {
				return colliderManager ? colliderManager : (colliderManager = gameObject.FindOrAddChild("Collider Manager"));
			}
		}
		
		public const float margin = 0.001F;
	
		bool _spriteRendererCached;
		SpriteRenderer _spriteRenderer;
		public SpriteRenderer spriteRenderer { 
			get { 
				_spriteRenderer = _spriteRendererCached ? _spriteRenderer : gameObject.GetComponent<SpriteRenderer>();
				_spriteRendererCached = true;
				return _spriteRenderer;
			}
		}
	
		Dictionary<float, Pool> boxColliderPools;
		Dictionary<float, Pool> BoxColliderPools {
			get {
				return boxColliderPools ?? (boxColliderPools = new Dictionary<float, Pool>());
			}
		}
		
		Sprite runtimeSprite;
		Texture2D runtimeMapTexture;
		
		void Awake() {
			hideFlags = HideFlags.NotEditable;
			
			InitializeMap();
			ZoneManager.Initialize(this);
		}
	
		void Reset() {
			this.SetExecutionOrder(-11);
		}
		
		void InitializeMap() {
			runtimeMapTexture = new Texture2D(mapTexture.width, mapTexture.height, TextureFormat.RGBA32, false);
			runtimeMapTexture.name = mapTexture.name;
			runtimeMapTexture.filterMode = mapTexture.filterMode;
			runtimeMapTexture.wrapMode = mapTexture.wrapMode;
			runtimeMapTexture.SetPixels(mapTexture.GetPixels());
			runtimeMapTexture.Apply();
			runtimeSprite = Sprite.Create(runtimeMapTexture, new Rect(0, 0, mapTexture.width, mapTexture.height), Vector2.zero, 1);
			runtimeSprite.name = Map.name;
			spriteRenderer.sprite = runtimeSprite;
		}

		void UpdateFogOfWar() {
			if (FogOfWar == null) {
				return;
			}
			
			Vector2 fowScale = new Vector2(Width, Height);
			FogOfWar.transform.parent = transform;
			FogOfWar.transform.SetLocalScale(fowScale, Axis.XY);
			FogOfWar.transform.SetLocalPosition(fowScale / 2, Axis.XY);
			FogOfWar.Definition = 1F / Scale;
			FogOfWar.GenerateHeightMap = true;
			FogOfWar.LayerMask = LayerMask.NameToLayer("Diggable");
			FogOfWar.Color = spriteRenderer.color;
		}
		
		void UpdateMap() {
			if (Map == null || Scale <= 0) {
				return;
			}
		
			transform.localScale = Vector3.one * Scale;
			spriteRenderer.sprite = Map;
		
			mapTexture = Map.texture;
			width = mapTexture.width;
			height = mapTexture.height;
			
			UpdateFogOfWar();
		}

		public void Dig(Vector3 worldPoint) {
			Dig(WorldToPixel(worldPoint));
		}
	
		public void Dig(Vector2 pixel) {
			Dig((int)pixel.x, (int)pixel.y);
		}
	
		public void Dig(int x, int y) {
			runtimeMapTexture.SetPixel(x, y, new Color(0, 0, 0, 0));
			runtimeMapTexture.Apply();
		
			DiggableZone zone = ZoneManager.GetZone(x, y);
		
			if (zone != null) {
				zone.Break();
			}
			
			if (fogOfWar != null) {
				Vector3 worldPoint = PixelToWorld(new Vector2(x, y));
				fogOfWar.SetHeight(worldPoint, 0);
			}
			
		}
		
		public void SpawnFX(Vector2 pixel) {
			if (FxPool.PooledObject != null) {
				ParticleSystem fx = FxPool.Spawn<ParticleSystem>(FxManager.transform, pixel);
				fx.Simulate(0, true);
				fx.Play();
				
				StartCoroutine(DespawnFX(fx));
			}
		}

		public Color GetPixel(float x, float y) {
			return GetPixel((int)x, (int)y);
		}
	
		public Color GetPixel(int x, int y) {
			return Application.isPlaying ? runtimeMapTexture.GetPixel(x, y) : mapTexture.GetPixel(x, y);
		}
	
		public void SetPixel(int x, int y, Color pixel) {
			runtimeMapTexture.SetPixel(x, y, pixel);
		}
	
		Vector2 WorldToPixel(Vector3 worldPoint) {
			return new Vector2((worldPoint.x - transform.position.x) / Scale, (worldPoint.y - transform.position.y) / Scale);
		}
		
		Vector3 PixelToWorld(Vector2 pixel) {
			return new Vector3(pixel.x * Scale + transform.position.x, pixel.y * Scale + transform.position.y, transform.position.z);
		}
		
		IEnumerator DespawnFX(ParticleSystem fx) {
			yield return new WaitForSeconds(fx.startLifetime);
			
			FxPool.Despawn(fx);
		}
	}
}
