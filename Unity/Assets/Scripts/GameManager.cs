using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ChangeFieldImageView _changeFieldImageView;
    [SerializeField] CreatureShowView _creatureShowView;
    [SerializeField] BattleView _battleView;

    void Start()
    {
        _creatureShowView.SetCreatureShow();
        _changeFieldImageView.OnClickFieldImageButton();
        _battleView.Battle();
    }
}