using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class StartButtonScript : MonoBehaviour
{   
    const string _savannah = "サバンナ";
    const string _pacificOcean = "太平洋";
    const string _tokyo = "東京";

    [SerializeField] GameObject _mainGameObject;
    [SerializeField] GameObject _battleGameObject;
    [SerializeField] GameObject _nextGameButton;

    [SerializeField] Image _creatureImagePlayer;
    [SerializeField] Image _creatureImageOpponent;
    [SerializeField] Image _creatureBattleImagePlayer;
    [SerializeField] Image _creatureBattleImageOpponent;
    [SerializeField] Image _vsImage;
    [SerializeField] Image _battleTextImage;
    [SerializeField] Image _fieldImage;

    [SerializeField] Text _creatureTextPlayer;
    [SerializeField] Text _creatureTextOpponent;
    [SerializeField] Text _battleText;

    [SerializeField] List<Sprite> _fieldImages;
    [SerializeField] List<Sprite> _characterImages;

    int _creatureNumberPlayer;
    int _creatureNumberOpponent;
    int _playerScaledPower;
    int _opponentScaledPower;

    string _field;

    enum Field {   
        Tokyo = 0,
        Savannah = 1,
        PacificOcean = 2,
        Space = 3,
    }
    enum Character {
        Elephant = 0,
        Lion = 1,
        Zebra = 2,
        Dolphin = 3,
        Orca = 4,
        Human = 5,
    }

    void Start()
    {
        _battleText.enabled = false;
        _battleTextImage.enabled = false;

        _battleGameObject.SetActive(false);
        _nextGameButton.SetActive(false);
    }

    class Creature{
        public string name;
        public int power;
        public string field;

        public Creature(string name, int power, string field)
        {
            this.name = name;
            this.power = power;
            this.field = field;
        }
    }

    public void OnClick(){

        Creature elephant, lion, zebra, dolphin, orca, human;
        elephant = new Creature("ゾウ", 100, _savannah);
        lion = new Creature("ライオン", 80, _savannah);
        zebra = new Creature("シマウマ", 30, _savannah);
        dolphin = new Creature("イルカ", 30, _pacificOcean);
        orca = new Creature("シャチ", 80, _pacificOcean);
        human = new Creature("人間", 10, _tokyo);

        Dictionary<int, Creature> creatures = new Dictionary<int, Creature>()
        {
            { 1, elephant },
            { 2, lion },
            { 3, zebra },
            { 4, dolphin },
            { 5, orca },
            { 6, human },
        };

        Dictionary<Sprite, string> fields = new Dictionary<Sprite, string>()
        {
            { _fieldImages[(int)Field.Tokyo], "東京" },
            { _fieldImages[(int)Field.Savannah], "サバンナ" },
            { _fieldImages[(int)Field.PacificOcean], "太平洋" },
            { _fieldImages[(int)Field.Space], "宇宙" },
        };

        Dictionary<string, int> new_creatures = new Dictionary<string, int>()
        {
            { "ゾウ2", 1 },
            { "ライオン2", 2 },
            { "シマウマ2", 3 },
            { "イルカ2", 4 },
            { "シャチ2", 5 },
            { "人間2", 6 },
        };

        Dictionary<int, Sprite> creatures_image = new Dictionary<int, Sprite>()
        {
            { 1, _characterImages[(int)Character.Elephant] },
            { 2, _characterImages[(int)Character.Lion] },
            { 3, _characterImages[(int)Character.Zebra] },
            { 4, _characterImages[(int)Character.Dolphin] },
            { 5, _characterImages[(int)Character.Orca] },
            { 6, _characterImages[(int)Character.Human] },
        };

        _mainGameObject.SetActive(false);
        _battleGameObject.SetActive(true);

        _creatureNumberPlayer = new_creatures[_creatureImagePlayer.sprite.name];
        _creatureNumberOpponent = new_creatures[_creatureImageOpponent.sprite.name];

        _creatureBattleImagePlayer.sprite = creatures_image[_creatureNumberPlayer];
        _creatureBattleImageOpponent.sprite = creatures_image[_creatureNumberOpponent];

        Creature player = creatures[_creatureNumberPlayer];
        Creature opponent = creatures[_creatureNumberOpponent];

        _field = fields[_fieldImage.sprite];

        judge_battle(player, opponent, _field);
        
        Invoke(nameof(EnabledVsImage), 1);

        Invoke(nameof(FinishBattle), 4);
    }

    void judge_battle(Creature player, Creature opponent, string field)
    {
        Dictionary<string, int> fieldScale = new Dictionary<string, int>(){
            {"東京", 15},
            {"サバンナ", 4},
            {"太平洋", 6},
            {"宇宙", 2}
        };

        if (player.field == field){
            _playerScaledPower = player.power * fieldScale[player.field];
        }
        else if (field == "宇宙"){
            _playerScaledPower = player.power / fieldScale["宇宙"];
        }
        else {
            _playerScaledPower = player.power;
        }

        if (opponent.field == field){
            _opponentScaledPower = opponent.power * fieldScale[opponent.field];
        }
        else if (field == "宇宙"){
            _opponentScaledPower = opponent.power / fieldScale["宇宙"];
        }
        else {
            _opponentScaledPower = opponent.power;
        }

        _creatureTextPlayer.text = player.field + "\n攻撃力:" + 
            _playerScaledPower + "\n" + player.name;

        _creatureTextOpponent.text = opponent.field + "\n攻撃力:" + 
            _opponentScaledPower + "\n" + opponent.name;

        if (_playerScaledPower > _opponentScaledPower)
        {
            _battleText.text = "勝者\n" + player.name;
        }
        else if (_playerScaledPower < _opponentScaledPower)
        {
            _battleText.text = "勝者\n" + opponent.name;
        }
        else
        {
            _battleText.text = "Draw";
        }
    }

    void FinishBattle(){
        _battleText.enabled = true;
        _battleTextImage.enabled = true;
        _nextGameButton.SetActive(true);
    }

    void EnabledVsImage(){
        _vsImage.enabled = false;
    }
}
