using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Etc
{
    // Lambda : 일회용 함수를 만드는데 사용하는 문법

    enum ItemType
    {
        Weapon,
        Armor,
        Amulet,
        Ring
    }

    enum Rarity
    {
        Normal,
        Uncommon,
        Rare
    }
    class Item
    {
        public ItemType itemType;
        public Rarity rarity;
    }

    class Lambda
    {
        delegate bool ItemSelector(Item item);

        // 추가적인 delegate 사용법 => generic사용 가능
        delegate Return MyFunc<Return>();
        delegate Return MyFunc<T, Return>(T item);
        delegate Return MyFunc<T1, T2, T3, Return>(T1 item1, T2 item2);
        // 위와 같이 generic을 사용하여 다양한 delegate생성 가능

        // 하지만! 여기서 더 강력한 기능이 C#에 있음
        // delegate를 "직접 선언하지 않아도" 이미 만들어진 것을 사용하여 바로 사용가능함 => C#언어 지원
        // -> 반환타입이 있는 delegate의 경우 Func 사용
        // -> 반환 타입이 없는 delegate의 경우 Action 사용


        static Item FindItem(ItemSelector selector)
        {
            foreach (Item item in _items)
            {
                if(selector(item)) 
                    return item;
            }
            return null;
        }

        static bool IsWeapon(Item item)
        {
            return item.itemType == ItemType.Weapon;
        }

        // 위에 까지가 delegate 타입을 만들고 각 아이템 타입에 맞춰서 찾아주는 함수를 만들었음
        // 하지만 위에 형태에서는 반드시 결국에 어떤 함수를 실행하게 될 것인지 여러 함수를 구현해야만 함
        // 여기서 한번만 사용할 함수를 굳이 만들어 복잡성을 증가시키는 대신에 lambda를 사용하여 해결함
        // -> 어짜피 ItemSelector 로 전달될 함수를 제공해야 함


        // 인벤토리
        static List<Item> _items = new List<Item>();
        static void Main(string[] args)
        {
            _items.Add(new Item() { itemType = ItemType.Weapon, rarity = Rarity.Normal});
            _items.Add(new Item() { itemType = ItemType.Armor, rarity = Rarity.Uncommon});
            _items.Add(new Item() { itemType = ItemType.Amulet, rarity = Rarity.Rare});


            // 1. delegate만 사용하는 경우
            Item item = FindItem(IsWeapon);

            // 2. delegate 키워드를 추가해 익명 함수(Anonymous Function)를 만들어서 굳이 IsWeapon을 구현하지 않고도 동일하게 작동하게 가능함
            Item item2 = FindItem(delegate (Item item)
            {
                return item.itemType == ItemType.Weapon;
            });

            // 3. lambda식을 사용하는 경우 : 위에서 delegate가 사라짐
            Item item3 = FindItem((Item item) => { return item.itemType == ItemType.Weapon; });

            // 4. delegate 타입을 아예 사용하지 않고 그냥 C#차원에서 지원하는 문법을 사용하여 처리
            Func<Item, bool> selector = (Item item) => { return (item.itemType == ItemType.Weapon); };
            // 기존에는 반드시 delegate타입을 만들고 이를 사용하는 메소드를 통해 사용하는 방식이었지만, Func를 통해 delegate 를 대체하고 바로 사용 가능함

            /*
             * 
             * Func와 Action 에 대한 추가적인 상세 설명
             * 
             * Func는 굳이 개발자가 delegate type을 만들고 그 delegate type 메소드를 담을 수 있는 변수를 즉시 만들고, 바로 메소드를 할당할 수 있게 함
             * 
             * 위에서는 그 메소드를 바로 만들어서 할당하는 것을 lambda식을 통해 이름없는 메소드를 즉시 만들어 할당함
             * 
             * 즉, Func<Item, bool> 이라는 C#에서 지원하는 문법이 delegate bool ItemSelector(Item item); 이 delegate type을 만드는 과정을 생략해준것
             * 우리는 각각 다른 return type, input parameter를 받는 상황에서 다른 delegate type을 만들 필요없이
             * 그냥 Func 혹은 Action을 통해 delegate type을 즉시 만들고 할당할 수 있는 것임
             * 
             * -> 이때 Func는 반환타입을 맨 마지막에 보유한 경우 사용하고, Action은 void 반환인 경우 사용함
             * 
             * Func나 Action을 통해 delegate type을 즉시 만들고
             * Func<Item, bool> selector = ... 이런 형식으로 해당 delegate type을 담을수 있는 selector와 같은 변수를 설정하여
             * 바로 해당 변수에 어떤 메소드를 담을 수 있음
             * 
             * 위에서는 lambda식을 이용하여 구체화된 delegate type 메소드를 만들지 않고 익명함수로 바로 할당함
             * (Item item) => { return item.itemType == ItemType.Weapon; } 이와 같은 람다식으로 Item을 받아서 그 Item의 itemType이 Weapon인지 검사해주는 함수를 즉시 할당함
             * 
             * 그래서 delegate type을 따르는 구체적인 함수를 selector에 담고
             * 
             * 이를 
             * Item item = FindItem(selector); 이와 같이 사용하여 List컬렉션에서 Weapon타입인 Item을 찾아서 반환하는 동작을 하도록 할 수 있음
             */
        }
    }
}
