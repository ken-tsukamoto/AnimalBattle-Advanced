using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton1Script : MonoBehaviour
{
    public Image creatureImage1;
    public Image creatureImage2;
    public GameObject deleteButton1;

    // Start is called before the first frame update
    void Start()
    {
        creatureImage1 = GameObject.Find("CreatureImage1").GetComponent<Image>();
        creatureImage2 = GameObject.Find("CreatureImage2").GetComponent<Image>();
        
        deleteButton1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        deleteButton1.SetActive(false);
        creatureImage1.enabled = false;
    }
}
