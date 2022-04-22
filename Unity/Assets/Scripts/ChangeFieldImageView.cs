using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeFieldImageView : MonoBehaviour
{
    [SerializeField] Button _nextFieldImageButton;
    [SerializeField] Button _returnFieldImageButton;

    [SerializeField] List<Sprite> _fieldImages;

    [SerializeField] Image _fieldImage;

    [SerializeField] Text _fieldText;

    public void ChangeFieldImage(){
        _nextFieldImageButton.onClick.AddListener(()=> ChangeNextFieldImage());
        _returnFieldImageButton.onClick.AddListener(()=> ChangeReturnFieldImage());
    }

    public void ChangeNextFieldImage()
    {
        if (_fieldText.text == FieldConstant.Tokyo)
        {
            _fieldText.text = FieldConstant.Savannah;
            _fieldImage.sprite = _fieldImages[(int)FieldConstant.Field.Savannah];
        }
        else if (_fieldText.text == FieldConstant.Savannah)
        {
            _fieldText.text = FieldConstant.PacificOcean;
            _fieldImage.sprite = _fieldImages[(int)FieldConstant.Field.PacificOcean];
        }
        else if (_fieldText.text == FieldConstant.PacificOcean)
        {
            _fieldText.text = FieldConstant.Space;
            _fieldImage.sprite = _fieldImages[(int)FieldConstant.Field.Space];
        }
        else
        {
            _fieldText.text = FieldConstant.Tokyo;
            _fieldImage.sprite = _fieldImages[(int)FieldConstant.Field.Tokyo];
        }
    }

    public void ChangeReturnFieldImage()
    {
        if (_fieldText.text == FieldConstant.Tokyo)
        {
            _fieldText.text = FieldConstant.Space;
            _fieldImage.sprite = _fieldImages[(int)FieldConstant.Field.Space];
        }
        else if (_fieldText.text == FieldConstant.Space)
        {
            _fieldText.text = FieldConstant.PacificOcean;
            _fieldImage.sprite = _fieldImages[(int)FieldConstant.Field.PacificOcean];
        }
        else if (_fieldText.text == FieldConstant.PacificOcean)
        {
            _fieldText.text = FieldConstant.Savannah;
            _fieldImage.sprite = _fieldImages[(int)FieldConstant.Field.Savannah];
        }
        else
        {
            _fieldText.text = FieldConstant.Tokyo;
            _fieldImage.sprite = _fieldImages[(int)FieldConstant.Field.Tokyo];
        }
    }
}
