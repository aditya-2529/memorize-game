using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    [SerializeField]
    private Transform pField;
    [SerializeField]
    private GameObject btn;
    void Awake(){
        for(int i = 0; i < 8;i++){
            GameObject b = Instantiate(btn);
            b.name = "Button "+i;
            b.transform.SetParent(pField.transform,false);
        }
    }
}
