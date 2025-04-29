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
        itemName = "크라켓의 조각상";
        itemDescription = "고대 생명체 ‘크라켓’을 기리기 위해 만들어진 석상.";
        itemSkillDescription = "DMG Up";
        itemType = itemType.Passive;
        itemPrefab = Resources.Load<GameObject>("Polyphemus");
    }

    public override void UseItem()
    {
        //공격력 + 0.5
        //공격력배율 * 1.5
        //눈물의 크기 커짐
    }
}




