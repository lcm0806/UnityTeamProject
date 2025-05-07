using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Item
{
    public string itemName;
    public string itemDescription;
    public string itemSkillDescription;
    public itemType itemType;

    [Header("아이템 프리팹")]
    public GameObject itemPrefab;


    [Header("아이템 아이콘 (인스펙터에서 직접 설정)")]
    [Header("아이템 아이콘 (직접 드래그)")]
    [SerializeField] public Sprite itemIcon;

    public Attack attack;
    public Player player;


    public abstract void UseItem();
    public void ShowIconOnUI()
    {
        if (itemIcon == null)
        {
            Debug.LogWarning($"[Item] {itemName} 아이콘이 비어 있음!");
            return;
        }

        if (itemType == itemType.Passive)
        {
            var passiveUI = GameObject.FindObjectOfType<PassiveEquipmentUI>();
            passiveUI?.AddPassiveItem(itemIcon);
        }
        else if (itemType == itemType.Active)
        {
            var activeUI = GameObject.FindObjectOfType<ActiveEquipmentUI>();
            activeUI?.AddActiveItem(itemIcon);
        }
    }
}

public enum itemType
{
    Passive, Active, Normal
}

public class SadOnion : Item //패시브아이템
{
    public SadOnion()
    {
        itemName = "만준의 눈물나는 양파";
        itemDescription = "양파를 썰면 눈이 매워지면서 눈물을 흘리는 점을 반영해 캐릭터의 눈이 초롱초롱해지며 눈물을 폭포같이 흘린다.";
        itemSkillDescription = "Tears Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("SadOnion");
        itemIcon = Resources.Load<Sprite>("ImageSource/sadonion");
    }

    public override void UseItem()
    {
        Player.Instance.BulletSpeed += 1.7f;
        Debug.Log("총알속도증가!!");
    }
}

public class TheInnerEye : Item //패시브아이템
{
    public TheInnerEye()
    {
        itemName = "내면의 눈";
        itemDescription = "어디선가 슬퍼보이는 눈..";
        itemSkillDescription = "Triple Shot";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("TheInnerEye");
        itemIcon = Resources.Load<Sprite>("ImageSource/theinnereye");
    }

    public override void UseItem()
    {

        //눈물이 3갈래로 나감
        Attack playerAttack = Player.Instance.GetComponentInChildren<Attack>();
        playerAttack.SetTripleShot(true); // Attack 스크립트에 트리플 샷 활성화


        Player.Instance.BulletSpeed *= 0.51f;
    }
}

public class Pentagram : Item //패시브아이템
{
    public Pentagram()
    {
        itemName = "오망성";
        itemDescription = "고대 마법의 힘이 깃든 별 모양의 마법진.";
        itemSkillDescription = "DMG Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("Pentagram");
        itemIcon = Resources.Load<Sprite>("ImageSource/pentagram");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 1f;
    }
}

public class GrowthHormones : Item //패시브아이템
{
    public GrowthHormones()
    {
        itemName = "성장 호르몬";
        itemDescription = "단시간에 신체 능력을 극대화시키는 성장 촉진제.";
        itemSkillDescription = "Speed + DMG up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("GrowthHormones");
        itemIcon = Resources.Load<Sprite>("ImageSource/growthhormones");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 1f;
        Player.Instance.Speed += 0.4f;
    }
}

public class MagicMushroom : Item //패시브아이템
{
    public MagicMushroom()
    {
        itemName = "요술 버섯";
        itemDescription = "신비로운 힘을 품은 요술버섯. 먹으면 특별한 효과가 발생한다.";
        itemSkillDescription = "All Stats UP";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("MagicMushroom");
        itemIcon = Resources.Load<Sprite>("ImageSource/magicmushroom");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 0.3f;
        Player.Instance.Speed += 0.3f;
        Player.Instance.Damage *= 1.5f;
        player.MaxHealth += 1; //최대체력 +1 //맥스체력 설정
//        player.Health = player.MaxHealth; //모두회복
    }
}

public class SpoonBender : Item //패시브아이템
{
    public SpoonBender()
    {
        itemName = "초능력자";
        itemDescription = "깨어난 잠재력을 통해 초능력을 발휘하는 신비로운 힘.";
        itemSkillDescription = "Homing Shots";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("SpoonBender");
        itemIcon = Resources.Load<Sprite>("ImageSource/spoonbender");
    }

    public override void UseItem()
    {
        //눈물 유도 스킬
        Bullet bullet = new Bullet();
        bullet.IsHomingEnabled = true;
        //눈물 보라색으로 변하기
        //    Renderer bulletRenderer = bullet.GetComponent<Renderer>();
        //    if(bulletRenderer != null && bulletRenderer.material != null)
        //    {
        //        bulletRenderer.material.color = new Color(0.5f, 0f, 0.5f);
        //    }
        //}
    }
}

public class BlueCap : Item //패시브아이템
{
    public BlueCap()
    {
        itemName = "파란 환각버섯";
        itemDescription = "파란 빛을 띠는 환각 버섯. 먹으면 정신이 흐려지고, 이상한 현상이 일어난다.";
        itemSkillDescription = "HP + Tears Up, Speed Down";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("BlueCap");
        itemIcon = Resources.Load<Sprite>("ImageSource/bluecap");
    }

    public override void UseItem()
    {
        Player.Instance.BulletSpeed += 0.7f;
        Player.Instance.Speed -= 0.3f;
        player.MaxHealth += 1; //최대 체력 +1 //맥스체력설정
    }
}

public class CricketsState : Item //패시브아이템
{
    public CricketsState()
    {
        itemName = "크라켓의 조각상";
        itemDescription = "고대 생명체 ‘크라켓’을 기리기 위해 만들어진 석상.";
        itemSkillDescription = "DMG Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("CricketsState");
        itemIcon = Resources.Load<Sprite>("ImageSource/cricketsstate");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 0.5f;
        Player.Instance.Damage *= 1.5f;
         //눈물의 크기 커짐
    }
}

public class TornPhoto : Item //패시브아이템
{
    public TornPhoto()
    {
        itemName = "찢어진 사진";
        itemDescription = "오래된 기억을 떠올리게 하는 이 사진은 시간이 지나며 일부가 훼손되었다. 하지만 그 속에서 여전히 과거의 의미를 찾을 수 있다.";
        itemSkillDescription = "Shot Speed Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("TornPhoto");
        itemIcon = Resources.Load<Sprite>("ImageSource/tornphoto");
    }

    public override void UseItem()
    {
        Player.Instance.BulletSpeed += 0.7f;
    }
}

public class Polyphemus : Item //패시브아이템
{
    public Polyphemus()
    {
        itemName = "민호의 충혈된 빨간 눈";
        itemDescription = "죽은 거인의 썩지 않는 눈알.";
        itemSkillDescription = "Mega Tears";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("Polyphemus");
        itemIcon = Resources.Load<Sprite>("ImageSource/polyphemus");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 4f;
        Player.Instance.Damage *= 2f;
        Player.Instance.BulletSpeed *= 0.42f;
    }
}

public class BookOfBelial : Item //액티브아이템
{
    public BookOfBelial()
    {
        itemName = "벨리알의 서";
        itemDescription = "잊혀진 마법사 벨리아가 남긴 금지된 마법서.";
        itemSkillDescription = "Temporary DMG Up";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("BookOfBelial");
        itemIcon = Resources.Load<Sprite>("ImageSource/bookofbelial");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 2;
    }
}

public class YumHeart : Item //액티브아이템
{
    public YumHeart()
    {
        itemName = "???심장";
        itemDescription = "정체를 알 수 없는 심장. 이상하게도 먹으면 체력이 회복된다... 하지만, 무언가 잘못된 것 같기도?";
        itemSkillDescription = "+1 Hearts";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("YumHeart");
        itemIcon = Resources.Load<Sprite>("ImageSource/heart");
    }

    public override void UseItem()
    {
        Player.Instance.CulHealth += 1;
        if (Player.Instance.CulHealth > Player.Instance.MaxHealth)
        {
            Player.Instance.CulHealth = Player.Instance.MaxHealth;
        }
    }
}

public class BookOfShadow : Item //액티브아이템
{
    public BookOfShadow()
    {
        itemName = "그림자의 서";
        itemDescription = "잊혀진 고대 문명에서 만들어진 어둠의 서.";
        itemSkillDescription = "Temporary Invencibility";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("BookOfShadow");
        itemIcon = Resources.Load<Sprite>("ImageSource/bookofshadow");
    }

    public override void UseItem()
    {
        //주위에 보호막이 쳐지며 10초간 무적
        if (Player.Instance != null)
        {
            Invincible invincible = Player.Instance.GetComponent<Invincible>();
            if (invincible != null)
            {
                invincible.Activate_Sheild();
                Debug.Log($"{itemName} 사용! 10초 동안 무적 상태가 됩니다.");
            }
            else
            {
                Debug.LogError("플레이어 오브젝트에 Invincible 스크립트가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("플레이어 싱글톤 인스턴스를 찾을 수 없습니다.");
        }
    }
}

public class ShoopDaWhoop : Item //액티브아이템
{
    public ShoopDaWhoop()
    {
        itemName = "석숭이의 인형";
        itemDescription = "다 사라져라!!! (단, 책임은 지지 않습니다)";
        itemSkillDescription = "BLLLARRRRGGG!";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("ShoopDaWhoop");
        itemIcon = Resources.Load<Sprite>("ImageSource/shoopdawhoop");
    }

    public override void UseItem()
    {
        if (Player.Instance != null)
        {
            Rayser rayser = Player.Instance.GetComponent<Rayser>();
            if (rayser != null)
            {
               rayser.Laser(player.Damage += 13);
                Debug.Log($"{itemName} 사용! 레이저발사됩니다!");
            }
            else
            {
                Debug.LogError("플레이어 오브젝트에 Rayser 스크립트가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("플레이어 싱글톤 인스턴스를 찾을 수 없습니다.");
        }
    }
}

public class TheNail : Item //액티브아이템
{
    public TheNail()
    {
        itemName = "대못";
        itemDescription = "죄인을 심판하기 위해 만들어진 주술용 못.";
        itemSkillDescription = "Temporary demon form";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("TheNail");
        itemIcon = Resources.Load<Sprite>("ImageSource/nails");
    }

    public override void UseItem()
    {
        //Player.Instance.SoulHealth += 0.5f;
        Player.Instance.Speed -= 0.18f;
        Player.Instance.Damage += 2f;
    }
}

public class MrBoom : Item //액티브아이템
{
    public MrBoom()
    {
        itemName = "Mr.Boom의 폭탄";
        itemDescription = "폭발물 전문가인 Mr. Boom이 설계한 폭탄";
        itemSkillDescription = "bombs!";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("MrBoom");
        itemIcon = Resources.Load<Sprite>("ImageSource/mrBoom");
    }

    public override void UseItem()
    {

        //폭탄 던지기(공격력 +60)



    }
}

public class TammysBlessing : Item //액티브아이템
{
    public TammysBlessing()
    {
        itemName = "타미의 축복";
        itemDescription = "Tammy의 축복을 받으면, 세상이 마치 총알로 가득 찬 것처럼 느껴진다.";
        itemSkillDescription = "8-Way Shot!";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("TammysBlessing");
        itemIcon = Resources.Load<Sprite>("ImageSource/tammysblessing");
    }

    public override void UseItem()
    {
        //8방향 눈물 발사
        Attack playerAttack = Player.Instance.GetComponentInChildren<Attack>();
        playerAttack.Set8WayShot(true);
        playerAttack.Fire(Player.Instance.Damage);
        playerAttack.Set8WayShot(false);
    }
}

public class Cross : Item //액티브아이템
{
    public Cross()
    {
        itemName = "십자가";
        itemDescription = "이 신성한 십자가는 고대의 보호 마법을 담고 있다.";
        itemSkillDescription = "Reusable Soul Protection";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("Cross");
        itemIcon = Resources.Load<Sprite>("ImageSource/cross");
    }

    public override void UseItem()
    {
        //player.SoulHealth += 1f;
    }
}

public class AnarchistCookBook : Item //액티브아이템
{
    public AnarchistCookBook()
    {
        itemName = "무정부주의자의 요리책";
        itemDescription = "불확실하고 혼란스러운 전술이 가득한 책. 이 책을 사용하면 강력한 폭탄을 사용할 수 있게 된다.";
        itemSkillDescription = "Random 6-Boom!";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("AnarchistCookBook");
        itemIcon = Resources.Load<Sprite>("ImageSource/anarchistcookook");
    }

    public override void UseItem()
    {
        //방안에 랜덤으로 폭탄 6개 소환
        if (Player.Instance != null)
        {
            SixBomb sixBomb = Player.Instance.GetComponent<SixBomb>();
            if (sixBomb != null)
            {
               sixBomb.RandomSpawnBomb();
                Debug.Log($"{itemName} 사용! 랜덤으로 폭탄이 생성됩니다!");
            }
            else
            {
                Debug.LogError("플레이어 오브젝트에 SixBomb 스크립트가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("플레이어 싱글톤 인스턴스를 찾을 수 없습니다.");
        }
    }
}

public class TheHourglass : Item //액티브아이템
{
    public TheHourglass()
    {
        itemName = "모래시계";
        itemDescription = "시간을 되돌리는 힘이 담긴 고대의 유물.";
        itemSkillDescription = "Temporary enemy slowdown";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("TheHourglass");
        itemIcon = Resources.Load<Sprite>("ImageSource/thehourglass");
    }

    public override void UseItem()
    {
        //방에잇는 몬스터에게 8초간 둔화
    }
}

public class Potion : Item //노말아이템
{
    public Potion()
    {
        itemName = "포션";
        itemDescription = "달달한 딸기맛 체력회복템";
        itemSkillDescription = "+ 1 HP";
        itemType = itemType.Normal;
        itemPrefab = Resources.Load<GameObject>("Potion");
        itemIcon = Resources.Load<Sprite>("ImageSource/potion");
    }

    public override void UseItem()
    {
        Player.Instance.CulHealth += 1;
        if (Player.Instance.CulHealth > Player.Instance.MaxHealth)
        {
            Player.Instance.CulHealth = Player.Instance.MaxHealth;
        }
    }
}

public class GoldenKey : Item //액티브아이템
{
    public GoldenKey()
    {
        itemName = "열쇠";
        itemDescription = "황금 빛이 나는 열쇠";
        itemSkillDescription = "Get!";
        itemType = itemType.Normal;
        itemPrefab = Resources.Load<GameObject>("GoldenKey");
        itemIcon = Resources.Load<Sprite>("ImageSource/goldenkey");
    }

    public override void UseItem()
    {
        //키를 가지면 상자가 열린다.
    }
}

public class Bomb : Item //액티브아이템
{
    public Bomb()
    {
        itemName = "폭탄";
        itemDescription = "터진다!!";
        itemSkillDescription = "Boom!";
        itemType = itemType.Normal;
        itemPrefab = Resources.Load<GameObject>("Bomb");
        itemIcon = Resources.Load<Sprite>("ImageSource/bomb");
    }

    public override void UseItem()
    {
        
    }
}

