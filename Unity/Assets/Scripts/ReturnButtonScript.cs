using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButtonScript : MonoBehaviour
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
        if (fieldText.text == "“Œ‹ž")
        {
            fieldText.text = "‰F’ˆ";
            fieldImage.sprite = SpaceImage;
        }
        else if (fieldText.text == "‰F’ˆ")
        {
            fieldText.text = "‘¾•½—m";
            fieldImage.sprite = PacificOceanImage;
        }
        else if (fieldText.text == "‘¾•½—m")
        {
            fieldText.text = "ƒTƒoƒ“ƒi";
            fieldImage.sprite = SavannahImage;
        }
        else
        {
            fieldText.text = "“Œ‹ž";
            fieldImage.sprite = TokyoImage;
        }
    }
}
