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
    public GameObject itemPrefab;

    public abstract void UseItem();
}

public enum itemType
{
    Passive, Active, Normal
}

// 아래는 각 아이템 클래스들

public class SadOnion : Item
{
    public SadOnion()
    {
        itemName = "눈물나는 양파";
        itemDescription = "양파를 썰면 눈이 매워지면서 눈물을 흘리는 점을 반영해 캐릭터의 눈이 초롱초롱해지며 눈물을 폭포같이 흘린다.";
        itemSkillDescription = "Tears Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("SadOnion");
    }

    public override void UseItem()
    {
        Player.Instance.BulletSpeed += 0.7f;
    }
}

public class TheInnerEye : Item
{
    public TheInnerEye()
    {
        itemName = "내면의 눈";
        itemDescription = "어디선가 슬퍼보이는 눈..";
        itemSkillDescription = "Triple Shot";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("TheInnerEye");
    }

    public override void UseItem()
    {
        Player.Instance.BulletSpeed *= 0.51f;
    }
}

public class Pentagram : Item
{
    public Pentagram()
    {
        itemName = "오망성";
        itemDescription = "고대 마법의 힘이 깃든 별 모양의 마법진.";
        itemSkillDescription = "DMG Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("Pentagram");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 1f;
    }
}

public class GrowthHormones : Item
{
    public GrowthHormones()
    {
        itemName = "성장 호르몬";
        itemDescription = "단시간에 신체 능력을 극대화시키는 성장 촉진제.";
        itemSkillDescription = "Speed + DMG up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("GrowthHormones");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 1f;
        Player.Instance.Speed += 0.4f;
    }
}

public class MagicMushroom : Item
{
    public MagicMushroom()
    {
        itemName = "요술 버섯";
        itemDescription = "신비로운 힘을 품은 요술버섯. 먹으면 특별한 효과가 발생한다.";
        itemSkillDescription = "All Stats UP";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("MagicMushroom");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 0.3f;
        Player.Instance.Speed += 0.3f;
        Player.Instance.Damage *= 1.5f;
    }
}

public class SpoonBender : Item
{
    public SpoonBender()
    {
        itemName = "초능력자";
        itemDescription = "깨어난 잠재력을 통해 초능력을 발휘하는 신비로운 힘.";
        itemSkillDescription = "Homing Shots";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("SpoonBender");
    }

    public override void UseItem()
    {
        // 눈물 유도 스킬
    }
}

public class BlueCap : Item
{
    public BlueCap()
    {
        itemName = "파란 환각버섯";
        itemDescription = "파란 빛을 띠는 환각 버섯. 먹으면 정신이 흐려지고, 이상한 현상이 일어난다.";
        itemSkillDescription = "HP + Tears Up, Speed Down";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("BlueCap");
    }

    public override void UseItem()
    {
        Player.Instance.BulletSpeed += 0.7f;
        Player.Instance.Speed -= 0.3f;
    }
}

public class CricketsState : Item
{
    public CricketsState()
    {
        itemName = "크라켓의 조각상";
        itemDescription = "고대 생명체 ‘크라켓’을 기리기 위해 만들어진 석상.";
        itemSkillDescription = "DMG Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("CricketsState");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 0.5f;
        Player.Instance.Damage *= 1.5f;
    }
}

public class TornPhoto : Item
{
    public TornPhoto()
    {
        itemName = "찢어진 사진";
        itemDescription = "오래된 기억을 떠올리게 하는 이 사진은 시간이 지나며 일부가 훼손되었다.";
        itemSkillDescription = "Shot Speed Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("TornPhoto");
    }

    public override void UseItem()
    {
        Player.Instance.BulletSpeed += 0.7f;
    }
}

public class Polyphemus : Item
{
    public Polyphemus()
    {
        itemName = "폴리페무스";
        itemDescription = "죽은 거인의 썩지 않는 눈알.";
        itemSkillDescription = "Mega Tears";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("Polyphemus");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 4f;
        Player.Instance.Damage *= 2f;
        Player.Instance.BulletSpeed *= 0.42f;
    }
}

// === 액티브 아이템 ===

public class BookOfBelial : Item
{
    public BookOfBelial()
    {
        itemName = "벨리알의 서";
        itemDescription = "잊혀진 마법사 벨리아가 남긴 금지된 마법서.";
        itemSkillDescription = "Temporary DMG Up";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("BookOfBelial");
    }

    public override void UseItem()
    {
        Player.Instance.Damage += 2;
    }
}

public class YumHeart : Item
{
    public YumHeart()
    {
        itemName = "???심장";
        itemDescription = "정체를 알 수 없는 심장. 먹으면 체력이 회복된다.";
        itemSkillDescription = "+1 Hearts";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("YumHeart");
    }

    public override void UseItem()
    {
        Player.Instance.CulHealth += 1;
        if(Player.Instance.CulHealth > Player.Instance.MaxHealth)
        {
            Player.Instance.CulHealth = Player.Instance.MaxHealth;
        }
    }
}

public class BookOfShadow : Item
{
    public BookOfShadow()
    {
        itemName = "그림자의 서";
        itemDescription = "잊혀진 고대 문명에서 만들어진 어둠의 서.";
        itemSkillDescription = "Temporary Invincibility";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("BookOfShadow");
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

public class ShoopDaWhoop : Item
{
    public ShoopDaWhoop()
    {
        itemName = "모두 다 사라져빔!!";
        itemDescription = "다 사라져라!!!";
        itemSkillDescription = "BLLLARRRRGGG!";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("ShoopDaWhoop");
    }

    public override void UseItem()
    {
        // 레이저
    }
}

public class TheNail : Item
{
    public TheNail()
    {
        itemName = "대못";
        itemDescription = "죄인을 심판하기 위해 만들어진 주술용 못.";
        itemSkillDescription = "Temporary demon form";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("TheNail");
    }

    public override void UseItem()
    {
        //Player.Instance.SoulHealth += 0.5f;
        Player.Instance.Speed -= 0.18f;
        Player.Instance.Damage += 2f;
    }
}

public class MrBoom : Item
{
    public MrBoom()
    {
        itemName = "Mr.Boom의 폭탄";
        itemDescription = "폭발물 전문가 Mr. Boom이 설계한 폭탄.";
        itemSkillDescription = "bombs!";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("MrBoom");
    }

    public override void UseItem()
    {
        // 폭탄 설치
    }
}

public class TammysBlessing : Item
{
    public TammysBlessing()
    {
        itemName = "타미의 축복";
        itemDescription = "세상이 총알로 가득 찬 것처럼 느껴진다.";
        itemSkillDescription = "8-Way Shot!";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("TammysBlessing");
    }

    public override void UseItem()
    {
        // 8방향 공격
    }
}

public class Cross : Item
{
    public Cross()
    {
        itemName = "십자가";
        itemDescription = "고대의 보호 마법을 담고 있다.";
        itemSkillDescription = "Reusable Soul Protection";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("Cross");
    }

    public override void UseItem()
    {
        //player.SoulHealth += 1f;
    }
}

public class AnarchistCookBook : Item
{
    public AnarchistCookBook()
    {
        itemName = "무정부주의자의 요리책";
        itemDescription = "혼란의 전술이 담긴 폭탄책";
        itemSkillDescription = "Random 6-Boom!";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("AnarchistCookBook");
    }

    public override void UseItem()
    {
        // 폭탄 6개 생성
    }
}

public class TheHourglass : Item
{
    public TheHourglass()
    {
        itemName = "모래시계";
        itemDescription = "시간을 되돌리는 고대 유물";
        itemSkillDescription = "Temporary enemy slowdown";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("TheHourglass");
    }

    public override void UseItem()
    {
        // 몬스터 둔화
    }
}

// === Normal ===

public class Potion : Item
{
    public Potion()
    {
        itemName = "포션";
        itemDescription = "달달한 딸기맛 체력회복템";
        itemSkillDescription = "+1 HP";
        itemType = itemType.Normal;
        itemPrefab = Resources.Load<GameObject>("Potion");
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

public class GoldenKey : Item
{
    public GoldenKey()
    {
        itemName = "열쇠";
        itemDescription = "황금 빛이 나는 열쇠";
        itemSkillDescription = "Get!";
        itemType = itemType.Normal;
        itemPrefab = Resources.Load<GameObject>("GoldenKey");
    }

    public override void UseItem()
    {
        // 열쇠 효과
    }
}

public class Bomb : Item
{
    public Bomb()
    {
        itemName = "폭탄";
        itemDescription = "터진다!!";
        itemSkillDescription = "Boom!";
        itemType = itemType.Normal;
        itemPrefab = Resources.Load<GameObject>("Bomb");
    }

    public override void UseItem()
    {
        // 폭발
    }
}