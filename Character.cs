using System;
using System.Collections.Generic;

partial class Program
{
    // Статические списки, доступные по всему классу
    static readonly string[] Names = { "Качинский", "Петя", "Кента", "Ота", "Галиаф", "Акич" };
    static readonly string[] Races = { "Человек", "Иноземец", "Древний человек" };
    static readonly string[] Classes = { "Воин", "Маг", "Техник", "Бездомный", "Охотник" };
    static readonly string[] Genders = { "Мужской", "Женский" };
    static readonly string[] Traits = { "Храбрый", "Трусливый", "Мудрый", "Злой", "Добрый", "Хитрый" };

    // Точка входа программы
    static void Main()
    {
        CharacterStart();  // Вызов метода генерации персонажей
    }

    // Метод генерации и вывода персонажей
    static void CharacterStart()
    {
        int numCharacters = 5; // Количество персонажей
        var characters = GenerateCharacters(numCharacters);

        for (int i = 0; i < characters.Count; i++)
        {
            Console.WriteLine($"Персонаж {i + 1}:");
            Console.WriteLine(characters[i]);
            Console.WriteLine();
        }
    }

    // Метод генерации списка персонажей
    static List<Character> GenerateCharacters(int count)
    {
        var characters = new List<Character>();
        for (int i = 0; i < count; i++)
        {
            characters.Add(GenerateCharacter());
        }
        return characters;
    }

    // Метод генерации одного персонажа
    static Character GenerateCharacter()
    {
        return new Character
        {
            Name = RandomElement(Names),
            Race = RandomElement(Races),
            Class = RandomElement(Classes),
            Gender = RandomElement(Genders),
            Trait = RandomElement(Traits),
            Strength = RandomStat(1, 20),
            Dexterity = RandomStat(1, 20),
            Intelligence = RandomStat(1, 20),
            Charisma = RandomStat(1, 20),
        };
    }

    // Метод генерации случайного значения для характеристик
    static int RandomStat(int min, int max) => new Random().Next(min, max + 1);

    // Метод выбора случайного элемента из массива
    static string RandomElement(string[] array) => array[new Random().Next(array.Length)];
}

// Класс для персонажей
class Character
{
    public string Name { get; set; }
    public string Race { get; set; }
    public string Class { get; set; }
    public string Gender { get; set; }
    public string Trait { get; set; }
    public int Strength { get; set; }
    public int Dexterity { get; set; }
    public int Intelligence { get; set; }
    public int Charisma { get; set; }

    // Переопределение метода ToString для вывода информации о персонаже
    public override string ToString()
    {
        return
            $"  Имя: {Name}\n" +
            $"  Раса: {Race}\n" +
            $"  Класс: {Class}\n" +
            $"  Пол: {Gender}\n" +
            $"  Черта характера: {Trait}\n" +
            $"  Сила: {Strength}\n" +
            $"  Ловкость: {Dexterity}\n" +
            $"  Интеллект: {Intelligence}\n" +
            $"  Харизма: {Charisma}";
    }
}
