using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartButtonScript : MonoBehaviour
{   
    public GameObject MainGameObject;
    public GameObject BattleGameObject;

    public GameObject NextGameButton;

    public Image creatureImage1;
    public Image creatureImage2;

    public Image creatureBattleImage1;
    public Image creatureBattleImage2;

    public Image VSImage;

    private Image fieldImage;

    public List<Sprite> fieldImages;
    enum Field {   
        Tokyo = 0,
        Savannah = 1,
        PacificOcean = 2,
        Space = 3,
    }

    public List<Sprite> CharacterImages;

    enum Character {
        Elephant = 0,
        Lion = 1,
        Zebra = 2,
        Dolphin = 3,
        Orca = 4,
        Human = 5,
    }

    public Text creature1Text;
    public Text creature2Text;

    public Text BattleText;
    public Image BattleTextImage;

    private int creature1_number;
    private int creature2_number;

    private int c1_scaled_power;
    private int c2_scaled_power;

    private string field;

    // Start is called before the first frame update
    void Start()
    {
        creatureImage1 = GameObject.Find("CreatureImage1").GetComponent<Image>();
        creatureImage2 = GameObject.Find("CreatureImage2").GetComponent<Image>();

        creatureBattleImage1 = GameObject.Find("CreatureBattleImage1").GetComponent<Image>();
        creatureBattleImage2 = GameObject.Find("CreatureBattleImage2").GetComponent<Image>();

        fieldImage = GameObject.Find("FieldImage").GetComponent<Image>();

        VSImage = GameObject.Find("VSImage").GetComponent<Image>();

        BattleText = GameObject.Find("BattleText").GetComponent<Text>();
        BattleText.enabled = false;
        BattleTextImage = GameObject.Find("BattleTextImage").GetComponent<Image>();
        BattleTextImage.enabled = false;

        BattleGameObject.SetActive(false);
        NextGameButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        elephant = new Creature("ゾウ",100,"サバンナ");
        lion = new Creature("ライオン",80,"サバンナ");
        zebra = new Creature("シマウマ",30,"サバンナ");
        dolphin = new Creature("イルカ",30,"太平洋");
        orca = new Creature("シャチ",80,"太平洋");
        human = new Creature("人間",10,"東京");

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
            { fieldImages[(int)Field.Tokyo], "東京" },
            { fieldImages[(int)Field.Savannah], "サバンナ" },
            { fieldImages[(int)Field.PacificOcean], "太平洋" },
            { fieldImages[(int)Field.Space], "宇宙" },
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
            { 1, CharacterImages[(int)Character.Elephant] },
            { 2, CharacterImages[(int)Character.Lion] },
            { 3, CharacterImages[(int)Character.Zebra] },
            { 4, CharacterImages[(int)Character.Dolphin] },
            { 5, CharacterImages[(int)Character.Orca] },
            { 6, CharacterImages[(int)Character.Human] },
        };

        MainGameObject.SetActive(false);
        BattleGameObject.SetActive(true);

        creature1_number = new_creatures[creatureImage1.sprite.name];
        creature2_number = new_creatures[creatureImage2.sprite.name];

        creatureBattleImage1.sprite = creatures_image[creature1_number];
        creatureBattleImage2.sprite = creatures_image[creature2_number];

        Creature creature1 = creatures[creature1_number];
        Creature creature2 = creatures[creature2_number];

        field = fields[fieldImage.sprite];

        judge_battle(creature1, creature2, field);
        
        Invoke(nameof(Destroy_VSImage), 1);

        Invoke(nameof(FinishBattle), 4);
    }

    void judge_battle(Creature c1, Creature c2, string field)
    {
        Dictionary<string, int> fieldScale = new Dictionary<string, int>(){
            {"東京", 15},
            {"サバンナ", 4},
            {"太平洋", 6},
            {"宇宙", 2}
        };

        if (c1.field == field){
            c1_scaled_power = c1.power * fieldScale[c1.field];
        }
        else if (field == "宇宙"){
            c1_scaled_power = c1.power / fieldScale["宇宙"];
        }
        else {
            c1_scaled_power = c1.power;
        }

        if (c2.field == field){
            c2_scaled_power = c2.power * fieldScale[c2.field];
        }
        else if (field == "宇宙"){
            c2_scaled_power = c2.power / fieldScale["宇宙"];
        }
        else {
            c2_scaled_power = c2.power;
        }

        creature1Text.text = c1.field + "\n攻撃力:" + 
            c1_scaled_power + "\n" + c1.name;

        creature2Text.text = c2.field + "\n攻撃力:" + 
            c2_scaled_power + "\n" + c2.name;

        if (c1_scaled_power > c2_scaled_power)
        {
            BattleText.text = "勝者\n" + c1.name;
        }
        else if (c1_scaled_power < c2_scaled_power)
        {
            BattleText.text = "勝者\n" + c2.name;
        }
        else
        {
            BattleText.text = "Draw";
        }
    }

    void FinishBattle(){
        BattleText.enabled = true;
        BattleTextImage.enabled = true;
        NextGameButton.SetActive(true);
    }

    void Destroy_VSImage(){
        VSImage.enabled = false;
    }
}
