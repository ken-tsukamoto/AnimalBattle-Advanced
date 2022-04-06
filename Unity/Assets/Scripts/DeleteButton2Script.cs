using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton2Script : MonoBehaviour
{
    public Image creatureImage1;
    public Image creatureImage2;
    public GameObject deleteButton2;

    // Start is called before the first frame update
    void Start()
    {
        creatureImage1 = GameObject.Find("CreatureImage1").GetComponent<Image>();
        creatureImage2 = GameObject.Find("CreatureImage2").GetComponent<Image>();
        
        deleteButton2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        deleteButton2.SetActive(false);
        creatureImage2.enabled = false;
    }
}
