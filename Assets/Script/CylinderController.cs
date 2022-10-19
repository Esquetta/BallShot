using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CylinderController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    bool ButtonPresssed;
    public GameObject Cylinder;
    [SerializeField] public float RotatePowerX;
    [SerializeField] public float RotatePowerY;
    [SerializeField] public float RotatePowerZ=0f;
    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPresssed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonPresssed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (ButtonPresssed)
        {
            Cylinder.transform.Rotate(RotatePowerX * Time.deltaTime, RotatePowerY*Time.deltaTime,RotatePowerZ,Space.Self);
        }
        

    }
}
