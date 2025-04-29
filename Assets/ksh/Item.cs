using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class Item
{
    protected string itemName;
    protected string itemDescription;
    protected string itemSkillDescription;
    protected itemType itemType;
    protected GameObject itemPrefab;
    
    public abstract void UseItem();
}

public enum itemType
{
    Passive, Active
}

public class SadOnion : Item //패시브아이템
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
        //공격 속도 0.7 증가
    }
}

public class TheInnerEye : Item //패시브아이템
{
    public TheInnerEye()
    {
        itemName = "내면의 눈";
        itemDescription = "캐릭터의 이마에 천진반처럼 눈이 하나 더 생긴다.";
        itemSkillDescription = "Triple Shot";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("TheInnerEye");
    }

    public override void UseItem()
    {
        //3방향으로 날아가는 눈물 스킬
        //공격속도 배율 * 0.51 (절반 정도 느려짐)
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
    }

    public override void UseItem()
    {
        //공격력 + 1
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
    }

    public override void UseItem()
    {
        //공격력 + 1
        //이동속도 + 0.4
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
    }

    public override void UseItem()
    {
        //최대체력 +1
        //체력 모두 회복
        //공격력 + 0.3
        //이동속도 + 0.3
        //공격력 배율 * 1.5
        //사거리 +2.5
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
    }

    public override void UseItem()
    {
        //눈물 유도 스킬
        //눈물 보라색으로 변하기
    }
}

public class BlueCap : Item //패시브아이템
{
    public BlueCap()
    {
        itemName = "파란 환각버섯";
        itemDescription = "파란 빛을 띠는 환각 버섯. 먹으면 정신이 흐려지고, 이상한 현상이 일어난다.";
        itemSkillDescription = "HP + Tears Up , Shot Speed Down";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("BlueCap");
    }

    public override void UseItem()
    {
        //최대 체력 +1
        //공격 속도 +0.7
        //눈물 속도 -0.16
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
    }

    public override void UseItem()
    {
        //공격력 + 0.5
        //공격력배율 * 1.5
        //눈물의 크기 커짐
    }
}

public class TornPhoto : Item //패시브아이템
{
    public TornPhoto()
    {
        itemName = "찢어진 사진";
        itemDescription = "오래된 기억을 떠올리게 하는 이 사진은 시간이 지나며 일부가 훼손되었다. 하지만 그 속에서 여전히 과거의 의미를 찾을 수 있다.";
        itemSkillDescription = "Tears + Shot Speed Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("TornPhoto");
    }

    public override void UseItem()
    {
        //공격속도 + 0.7
        //눈물 속도 + 0.16
    }
}

public class Polyphemus : Item //패시브아이템
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
        //공격력 + 4
        //공격력배율 * 2
        //공격속도 배율 * 0.42
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
    }

    public override void UseItem()
    {
        //사용시 데미지 +2
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
    }

    public override void UseItem()
    {
        //체력 1칸 회복
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
    }

    public override void UseItem()
    {
        //주위에 보호막이 쳐지며 10초간 무적
    }
}

public class ShoopDaWhoop : Item //액티브아이템
{
    public ShoopDaWhoop()
    {
        itemName = "모두 다 사라져빔!!";
        itemDescription = "다 사라져라!!! (단, 책임은 지지 않습니다)";
        itemSkillDescription = "BLLLARRRRGGG!";
        itemType = itemType.Active;
        itemPrefab = Resources.Load<GameObject>("ShoopDaWhoop");
    }

    public override void UseItem()
    {
        //레이저 발사(데미지 +13)
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
    }

    public override void UseItem()
    {
        //블랙하트 +0.5
        //이동속도 -0.18
        //공격력 +2
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
    }

    public override void UseItem()
    {
        //8방향 눈물 발사
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
    }

    public override void UseItem()
    {
        //소울 하트 +1
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
    }

    public override void UseItem()
    {
        //방안에 랜덤으로 폭탄 소환
    }
}




