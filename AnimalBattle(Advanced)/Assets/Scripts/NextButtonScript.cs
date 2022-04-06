using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButtonScript : MonoBehaviour
{
    public Text fieldText;
    public Sprite TokyoImage;
    public Sprite SavannahImage;
    public Sprite PacificOceanImage;
    public Sprite SpaceImage;
    private Image fieldImage;

    // Start is called before the first frame update
    void Start()
    {
        fieldImage = GameObject.Find("FieldImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (fieldText.text == "東京")
        {
            fieldText.text = "サバンナ";
            fieldImage.sprite = SavannahImage;
        }
        else if (fieldText.text == "サバンナ")
        {
            fieldText.text = "太平洋";
            fieldImage.sprite = PacificOceanImage;
        }
        else if (fieldText.text == "太平洋")
        {
            fieldText.text = "宇宙";
            fieldImage.sprite = SpaceImage;
        }
        else
        {
            fieldText.text = "東京";
            fieldImage.sprite = TokyoImage;
        }
    }
}
