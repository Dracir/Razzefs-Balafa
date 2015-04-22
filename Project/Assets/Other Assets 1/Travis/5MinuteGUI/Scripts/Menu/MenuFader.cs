using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuFader : MonoBehaviour {

	public float fadeAmount;
	
	private float prevFadeAmount;
	
	private Image[] pics;
	private Text[] texts;
	private Outline[] outlines;
	
	private Color[] picsColours;
	private Color[] picsColoursClear;
	private Color[] textsColours;
	private Color[] textsColoursClear;
	private Color[] outlinesColours;
	private Color[] outlinesColoursClear;
	private float initY;
	private bool OuttaThere {
		get {
			return transform.position.z > 100;
		}
		set {
			transform.position = new Vector3(transform.position.x, (value? 10000 : initY), (value? 10000 : 0));
		}
	}
	
	// Use this for initialization
	void Start () {
		pics = GetComponentsInChildren<Image>();
		texts = GetComponentsInChildren<Text>();
		outlines = GetComponentsInChildren<Outline>();
		
		picsColours = new Color[pics.Length];
		picsColoursClear = new Color[pics.Length];
		textsColours = new Color[texts.Length];
		textsColoursClear = new Color[texts.Length];
		outlinesColours = new Color[outlines.Length];
		outlinesColoursClear = new Color[outlines.Length];
		
		for (int i = 0; i < pics.Length; i ++){
			picsColours[i] = pics[i].color;
			picsColoursClear[i] = new Color(pics[i].color.r, pics[i].color.g, pics[i].color.b, 0);
		}
		for (int i = 0; i < texts.Length; i ++){
			textsColours[i] = texts[i].color;
			textsColoursClear[i] = new Color(texts[i].color.r, texts[i].color.g, texts[i].color.b, 0);
		}
		for (int i = 0; i < outlines.Length; i ++){
			outlinesColours[i] = outlines[i].effectColor;
			outlinesColoursClear[i] = new Color(outlines[i].effectColor.r, outlines[i].effectColor.g, outlines[i].effectColor.b, 0);
		}
		
		initY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (fadeAmount != prevFadeAmount){
			for (int i = 0; i < pics.Length; i ++){
				pics[i].color = Color.Lerp(picsColours[i], picsColoursClear[i], 1f - fadeAmount);
			}
			for (int i = 0; i < texts.Length; i ++){
				texts[i].color = Color.Lerp(textsColours[i], textsColoursClear[i], 1f - fadeAmount);
			}
			for (int i = 0; i < outlines.Length; i ++){
				outlines[i].effectColor = Color.Lerp(outlinesColours[i], outlinesColoursClear[i], 1f - fadeAmount);
			}
		}
		
		if (fadeAmount == 0 && !OuttaThere){
			OuttaThere = true;
		} else if (fadeAmount != 0 && OuttaThere){
			OuttaThere = false;
		}
		
		prevFadeAmount = fadeAmount;
	}
}
