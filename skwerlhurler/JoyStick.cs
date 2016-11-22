using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler  {

	private Image bgImg; 
	private Image joystickImg; 

	void Start () {
		bgImg = GetComponent<Image> ();
		joystickImg = GetComponentInChildren<Image> (); 
	}
	
	public virtual void OnDrag(PointerEventData ped){
		Vector2 pos = Vector2.zero; 
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(bgImg.rectTransform, 
				ped.position, 
				ped.pressEventCamera,
				out ped)) 
		{
		
		}
	}

	public virtual void OnPointerDown(PointerEventData ped){
		Debug.Log ("OnPointerDown");
	}

	public virtual void OnPointerUp(PointerEventData ped){
		Debug.Log ("OnPointerUp");
	}
}
