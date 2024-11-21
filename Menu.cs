using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

partial class Program
{
    static string currentLanguage = "Русский"; // Текущий язык
    static Dictionary<string, Dictionary<string, string>> localizationData;

    static void Menu()
    {
        LoadLocalization();
        MainMenu();
    }

    static void LoadLocalization()
    {
        try
        {
            string json = File.ReadAllText("localization.json");
            localizationData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading localization file: " + ex.Message);
            Environment.Exit(1);
        }
    }

    static string GetLocalizedString(string key)
    {
        if (localizationData.ContainsKey(currentLanguage) &&
            localizationData[currentLanguage].ContainsKey(key))
        {
            return localizationData[currentLanguage][key];
        }
        return key; // Возвращает ключ, если перевод не найден
    }

    static void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(GetLocalizedString("MainMenuTitle"));
            Console.WriteLine("1. " + GetLocalizedString("NewGame"));
            Console.WriteLine("2. " + GetLocalizedString("LoadGame"));
            Console.WriteLine("3. " + GetLocalizedString("Settings"));
            Console.WriteLine("4. " + GetLocalizedString("Exit"));
            Console.WriteLine("=======================");
            Console.Write(GetLocalizedString("ChooseOption"));

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    StartNewGame();
                    break;
                case "2":
                    LoadGame();
                    break;
                case "3":
                    Settings();
                    break;
                case "4":
                    ExitGame();
                    return;
                default:
                    Console.WriteLine(GetLocalizedString("InvalidChoice"));
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void StartNewGame()
    {
        Console.Clear();
        Console.WriteLine(GetLocalizedString("NewGameStart"));
        Console.WriteLine(GetLocalizedString("PressAnyKeyToReturn"));
        Console.ReadKey();
    }

    static void LoadGame()
    {
        Console.Clear();
        Console.WriteLine(GetLocalizedString("LoadGameStart"));
        Console.WriteLine(GetLocalizedString("PressAnyKeyToReturn"));
        Console.ReadKey();
    }

    static void Settings()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(GetLocalizedString("SettingsTitle"));
            Console.WriteLine("1. " + GetLocalizedString("ChangeVolume"));
            Console.WriteLine("2. " + GetLocalizedString("ChangeResolution"));
            Console.WriteLine("3. " + GetLocalizedString("ChangeLanguage"));
            Console.WriteLine("4. " + GetLocalizedString("BackToMenu"));
            Console.Write(GetLocalizedString("ChooseOption"));

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ChangeVolume();
                    break;
                case "2":
                    ChangeResolution();
                    break;
                case "3":
                    ChangeLanguage();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine(GetLocalizedString("InvalidChoice"));
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ChangeVolume()
    {
        Console.Clear();
        Console.WriteLine(GetLocalizedString("VolumeNotImplemented"));
        Console.WriteLine(GetLocalizedString("PressAnyKeyToReturn"));
        Console.ReadKey();
    }

    static void ChangeResolution()
    {
        Console.Clear();
        Console.WriteLine(GetLocalizedString("ResolutionNotImplemented"));
        Console.WriteLine(GetLocalizedString("PressAnyKeyToReturn"));
        Console.ReadKey();
    }

    static void ChangeLanguage()
    {
        Console.Clear();
        Console.WriteLine(GetLocalizedString("LanguageMenu"));
        Console.WriteLine("1. English");
        Console.WriteLine("2. Русский");
        Console.WriteLine("3. 日本語");
        Console.Write(GetLocalizedString("ChooseOption"));

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                currentLanguage = "English";
                break;
            case "2":
                currentLanguage = "Русский";
                break;
            case "3":
                currentLanguage = "日本語";
                break;
            default:
                Console.WriteLine(GetLocalizedString("InvalidChoice"));
                break;
        }
        Console.WriteLine(GetLocalizedString("LanguageChanged"));
        Console.ReadKey();
    }

    static void ExitGame()
    {
        Console.Clear();
        Console.WriteLine(GetLocalizedString("ExitConfirm"));
        string choice = Console.ReadLine();
        if (choice?.ToLower() == "y" || choice == "д" || choice == "はい")
        {
            Console.WriteLine(GetLocalizedString("Goodbye"));
            Environment.Exit(0);
        }
    }
}
