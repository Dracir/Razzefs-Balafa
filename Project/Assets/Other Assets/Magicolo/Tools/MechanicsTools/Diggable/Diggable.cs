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
				
				UpdateFogOfWar();
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
		
		[SerializeField, PropertyField(typeof(RangeAttribute), 0, 1)]
		float alphaThreshold = 0.5F;
		public float AlphaThreshold {
			get {
				return alphaThreshold;
			}
			set {
				alphaThreshold = value;
			}
		}
		
		[SerializeField, PropertyField(typeof(MinAttribute), 0.001F)]
		float resistance = 1;
		public float Resistance {
			get {
				return resistance;
			}
			set {
				resistance = value;
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
		
		[SerializeField, PropertyField]
		GameObject digFX;
		public GameObject DigFX {
			get {
				return digFX;
			}
			set {
				digFX = value;
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
		Rect rect;
		public Rect Rect {
			get {
				return rect;
			}
		}

		[SerializeField, HideInInspector]
		Texture2D mapTexture;
		public Texture2D MapTexture {
			get {
				return mapTexture;
			}
		}

		bool colliderManagerCached;
		DiggableColliderManager colliderManager;
		public DiggableColliderManager ColliderManager { 
			get { 
				colliderManager = colliderManagerCached ? colliderManager : gameObject.FindOrAddChild("Collider Manager").GetOrAddComponent<DiggableColliderManager>();
				colliderManagerCached = true;
				return colliderManager;
			}
		}
		
		bool zoneManagerCached;
		DiggableZoneManager zoneManager;
		public DiggableZoneManager ZoneManager {
			get {
				zoneManager = zoneManagerCached ? zoneManager : gameObject.FindOrAddChild("Zone Manager").GetOrAddComponent<DiggableZoneManager>();
				zoneManagerCached = true;
				return zoneManager;
			}
		}
		
		bool fxManagerCached;
		DiggableFxManager fxManager;
		public DiggableFxManager FxManager {
			get {
				fxManager = fxManagerCached ? fxManager : gameObject.FindOrAddChild("Fx Manager").GetOrAddComponent<DiggableFxManager>();
				fxManagerCached = true;
				return fxManager;
			}
		}
		
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
		Color[] pixels;

		void Awake() {
			hideFlags = HideFlags.NotEditable;
			
			InitializeMap();
			ColliderManager.Initialize(this);
			ZoneManager.Initialize(this);
			FxManager.Initialize(this);
		}
		
		void Reset() {
			this.SetExecutionOrder(-11);
		}
		
		void InitializeMap() {
			runtimeMapTexture = new Texture2D(mapTexture.width, mapTexture.height, TextureFormat.RGBA32, false);
			runtimeMapTexture.name = mapTexture.name;
			runtimeMapTexture.filterMode = mapTexture.filterMode;
			runtimeMapTexture.wrapMode = mapTexture.wrapMode;
			pixels = mapTexture.GetPixels();
			runtimeMapTexture.SetPixels(pixels);
			runtimeMapTexture.Apply();
			runtimeSprite = Sprite.Create(runtimeMapTexture, new Rect(0, 0, mapTexture.width, mapTexture.height), Vector2.zero, 1);
			runtimeSprite.name = Map.name;
			spriteRenderer.sprite = runtimeSprite;
		}

		void UpdateFogOfWar() {
			if (FogOfWar == null) {
				return;
			}
			
			FogOfWar.transform.parent = transform;
			FogOfWar.transform.SetLocalScale(Rect.size, Axes.XY);
			FogOfWar.transform.SetLocalPosition(Rect.center, Axes.XY);
			FogOfWar.Definition = (int)(1F / Scale).Round();
			FogOfWar.GenerateHeightMap = true;
			FogOfWar.LayerMask = new LayerMask().AddToMask(Layer);
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
			rect = new Rect(float.MaxValue, float.MaxValue, float.MinValue, float.MinValue);
			
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					Color pixel = mapTexture.GetPixel(x, y);
					
					if (pixel.a >= AlphaThreshold) {
						rect.xMin = x < rect.xMin ? x : rect.xMin;
						rect.yMin = y < rect.yMin ? y : rect.yMin;
						rect.xMax = x > rect.xMax ? x : rect.xMax;
						rect.yMax = y > rect.yMax ? y : rect.yMax;
					}
				}
			}
			
			rect.width += 1;
			rect.height += 1;
			
			UpdateFogOfWar();
		}

		public void Dig(Vector3 worldPoint, float strength) {
			Dig(WorldToPixel(worldPoint), strength);
		}
		
		public void Dig(Vector3 worldPoint) {
			Dig(WorldToPixel(worldPoint), 1);
		}
		
		public void Dig(Vector2 pixel, float strength) {
			Dig((int)pixel.x, (int)pixel.y, strength);
		}
		
		public void Dig(Vector2 pixel) {
			Dig((int)pixel.x, (int)pixel.y, 1);
		}
		
		void Dig(int x, int y, float strength) {
			DiggableZone zone = ZoneManager.GetZone(x, y);
			float force = Mathf.Clamp01(strength / Resistance);
			
			if (zone != null) {
				Color pixel = pixels[y * Width + x];
				pixel.a -= force;
				pixels[y * Width + x] = pixel;
				
				if (pixel.a <= 0) {
					runtimeMapTexture.SetPixel(x, y, pixel);
					runtimeMapTexture.Apply();
					zone.Break(x, y);
				}
				
				FxManager.SpawnFX(new Vector2(x, y), force);
				
				if (fogOfWar != null) {
					fogOfWar.SetHeight(PixelToWorld(new Vector2(x, y)), Mathf.Clamp01(pixel.a));
				}
			}
		}
		
		public Color GetPixel(int x, int y) {
			return runtimeMapTexture.GetPixel(x, y);
		}
		
		public void SetPixel(int x, int y, Color pixel) {
			pixels[y * Width + x] = pixel;
			runtimeMapTexture.SetPixel(x, y, pixel);
			runtimeMapTexture.Apply();
		}
		
		Vector2 WorldToPixel(Vector3 worldPoint) {
			return new Vector2((worldPoint.x - transform.position.x) / Scale, (worldPoint.y - transform.position.y) / Scale);
		}
		
		Vector3 PixelToWorld(Vector2 pixel) {
			return new Vector3(pixel.x * Scale + transform.position.x, pixel.y * Scale + transform.position.y, transform.position.z);
		}
	}
}
