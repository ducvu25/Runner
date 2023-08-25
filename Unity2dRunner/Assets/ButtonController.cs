using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Animator animator ;
    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Selected");
        AudioController.instance.PlaySound((int)AudioSetting.click);
    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
   
}
