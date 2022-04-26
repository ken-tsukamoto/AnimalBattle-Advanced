using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleView : MonoBehaviour
{
    const string ElephantName = "ゾウ";
    const string LionName = "ライオン";
    const string ZebraName = "シマウマ";
    const string DolphinName = "イルカ";
    const string OrcaName = "シャチ";
    const string HumanName = "人間";

    const int ElephantPower = 100;
    const int LionPower = 80;
    const int ZebraPower = 30;
    const int DolphinPower = 30;
    const int OrcaPower = 80;
    const int HumanPower = 10;

    const string ElephantImage = "ゾウ2";
    const string LionImage = "ライオン2";
    const string ZebraImage = "シマウマ2";
    const string DolphinImage = "イルカ2";
    const string OrcaImage = "シャチ2";
    const string HumanImage = "人間2";

    const string Power = "攻撃力";
    const string Winner = "勝者";
    const string Draw = "Draw";

    const int FalseVsImageTime = 1;
    const int TrueSmokeAnimationTime = 1;
    const int FinishBattleTime = 2;

    [SerializeField] GameObject _mainGameObject;
    [SerializeField] GameObject _battleGameObject;
    [SerializeField] GameObject _playerCreatureSmoke;
    [SerializeField] GameObject _opponentCreatureSmoke;

    [SerializeField] Button _startBattleButton;
    [SerializeField] Button _nextGameButton;
    [SerializeField] Button _finishGameButton;

    [SerializeField] Animator _playerBattleAnimator;
    [SerializeField] Animator _opponentBattleAnimator;

    [SerializeField] List<Sprite> _characterBattleImages;
    [SerializeField] List<Sprite> _fieldImages;

    [SerializeField] Image _playerCreatureImage;
    [SerializeField] Image _opponentCreatureImage;
    [SerializeField] Image _playerCreatureBattleImage;
    [SerializeField] Image _opponentCreatureBattleImage;
    [SerializeField] Image _fieldImage;
    [SerializeField] Image _vsImage;
    [SerializeField] Image _battleTextImage;

    [SerializeField] Text _playerCreatureText;
    [SerializeField] Text _opponentCreatureText;
    [SerializeField] Text _battleText;

    int _playerScaledPower;
    int _opponentScaledPower;

    public void Battle()
    {
        _startBattleButton.onClick.AddListener(()=> StartBattle());
        _nextGameButton.onClick.AddListener(()=> NextGame());
        _finishGameButton.onClick.AddListener(()=> FinishGame());

        _battleGameObject.SetActive(false);
        _nextGameButton.gameObject.SetActive(false);

        _playerCreatureImage.enabled = false;
        _opponentCreatureImage.enabled = false;

        _battleText.enabled = false;
        _battleTextImage.enabled = false;
    }

    public void SetCreature()
    {   
        var elephant = new Creature(ElephantName, ElephantPower, FieldConstant.Savannah);
        var lion = new Creature(LionName, LionPower, FieldConstant.Savannah);
        var zebra = new Creature(ZebraName, ZebraPower, FieldConstant.Savannah);
        var dolphin = new Creature(DolphinName, DolphinPower, FieldConstant.PacificOcean);
        var orca = new Creature(OrcaName, OrcaPower, FieldConstant.PacificOcean);
        var human = new Creature(HumanName, HumanPower, FieldConstant.Tokyo);

        Dictionary<string, Creature> creatures = new Dictionary<string, Creature>()
        {
            { ElephantImage, elephant },
            { LionImage, lion },
            { ZebraImage, zebra },
            { DolphinImage, dolphin },
            { OrcaImage, orca },
            { HumanImage, human },
        };

        Dictionary<Sprite, string> fields = new Dictionary<Sprite, string>()
        {
            { _fieldImages[(int)FieldConstant.Field.Tokyo], FieldConstant.Tokyo },
            { _fieldImages[(int)FieldConstant.Field.Savannah], FieldConstant.Savannah },
            { _fieldImages[(int)FieldConstant.Field.PacificOcean], FieldConstant.PacificOcean },
            { _fieldImages[(int)FieldConstant.Field.Space], FieldConstant.Space },
        };

        Dictionary<string, Sprite> new_creatures_image = new Dictionary<string, Sprite>()
        {
            { ElephantImage, _characterBattleImages[(int)CreatureConstant.Creature.Elephant] },
            { LionImage, _characterBattleImages[(int)CreatureConstant.Creature.Lion] },
            { ZebraImage, _characterBattleImages[(int)CreatureConstant.Creature.Zebra] },
            { DolphinImage, _characterBattleImages[(int)CreatureConstant.Creature.Dolphin] },
            { OrcaImage, _characterBattleImages[(int)CreatureConstant.Creature.Orca] },
            { HumanImage, _characterBattleImages[(int)CreatureConstant.Creature.Human] },
        };

        _playerCreatureBattleImage.sprite = new_creatures_image[_playerCreatureImage.sprite.name];
        _opponentCreatureBattleImage.sprite = new_creatures_image[_opponentCreatureImage.sprite.name];

        Creature player = creatures[_playerCreatureImage.sprite.name];
        Creature opponent = creatures[_opponentCreatureImage.sprite.name];

        string _field = fields[_fieldImage.sprite];

        JudgeBattle(player, opponent, _field);
    }

    void JudgeBattle(Creature player, Creature opponent, string field)
    {
        SetScaledPower(player, opponent, field);

        _playerCreatureText.text = 
        $"{player.field}\n{Power}:{_playerScaledPower}\n{player.name}";
        _opponentCreatureText.text = 
        $"{opponent.field}\n{Power}:{_opponentScaledPower}\n{opponent.name}";

        if (_playerScaledPower > _opponentScaledPower)
        {
            _battleText.text = Winner + "\n" + player.name;
        }
        else if (_playerScaledPower < _opponentScaledPower)
        {
            _battleText.text = Winner + "\n" + opponent.name;
        }
        else
        {
            _battleText.text = Draw;
        }
    }

    void StartBattle()
    {
        _mainGameObject.SetActive(false);
        _battleGameObject.SetActive(true);

        SetCreature();

        StartCoroutine(BattleCoroutine());
    }

    void NextGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    void FinishGame()
    {
        Application.Quit();
    }

    public IEnumerator BattleCoroutine()
    {
        _playerCreatureSmoke.SetActive(false);
        _opponentCreatureSmoke.SetActive(false);

        yield return new WaitForSeconds(FalseVsImageTime);

        _vsImage.enabled = false;
        _playerBattleAnimator.SetTrigger("PlayerBattleTrigger");
        _opponentBattleAnimator.SetTrigger("OpponentBattleTrigger");

        yield return new WaitForSeconds(TrueSmokeAnimationTime);

        _playerCreatureSmoke.SetActive(true);
        _opponentCreatureSmoke.SetActive(true);

        yield return new WaitForSeconds(FinishBattleTime);

        _playerCreatureSmoke.SetActive(false);
        _opponentCreatureSmoke.SetActive(false);
        _battleText.enabled = true;
        _battleTextImage.enabled = true;
        _nextGameButton.gameObject.SetActive(true);
    }

    void SetScaledPower(Creature player, Creature opponent, string field)
    {
        Dictionary<string, int> fieldScale = new Dictionary<string, int>()
        {
            {FieldConstant.Tokyo, 15},
            {FieldConstant.Savannah, 4},
            {FieldConstant.PacificOcean, 6},
            {FieldConstant.Space, 2}
        };

        _playerScaledPower = player.power;
        _opponentScaledPower = opponent.power;

        if (player.field == field && opponent.field == field)
        {
            _playerScaledPower = player.power * fieldScale[player.field];
            _opponentScaledPower = opponent.power * fieldScale[opponent.field];
        }
        else if (player.field == field && opponent.field != field)
        {
            _playerScaledPower = player.power * fieldScale[player.field];
        }
        else if (player.field != field && opponent.field == field)
        {
            _opponentScaledPower = opponent.power * fieldScale[opponent.field];
        }
        else
        {
            _playerScaledPower = player.power / fieldScale[FieldConstant.Space];
            _opponentScaledPower = opponent.power / fieldScale[FieldConstant.Space];
        }
    }
}
