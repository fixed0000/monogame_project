﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

namespace coding;

public class Game1 : Game
{
    Texture2D ballTexture;
    Vector2 ballPosition;
    float ballSpeed;
    int deadZone;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font; // Шрифт для текста
    private Dictionary<string, Dictionary<string, string>> _localization;
    private string _currentLanguage = "Русский";
    private string[] _menuItems;
    private int _selectedIndex = 0;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 4,
                                   _graphics.PreferredBackBufferHeight / 4);
        ballSpeed = 100f;
        deadZone = 4096;
        LoadLocalization();
        SetMenuItems();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        ballTexture = Content.Load<Texture2D>("ball");
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("DefaultFont");
    }

    private void LoadLocalization()
        {
            try
            {
                string json = File.ReadAllText("localization.json");
                _localization = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка загрузки локализации: " + ex.Message);
                Exit();
            }
        }

        private void SetMenuItems()
        {
            _menuItems = new string[]
            {
                GetLocalizedString("NewGame"),
                GetLocalizedString("LoadGame"),
                GetLocalizedString("Settings"),
                GetLocalizedString("Exit")
            };
        }

        private string GetLocalizedString(string key)
        {
            if (_localization.ContainsKey(_currentLanguage) &&
                _localization[_currentLanguage].ContainsKey(key))
            {
                return _localization[_currentLanguage][key];
            }
            return key; // Возвращает ключ, если перевод не найден
        }

    protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyboardState = Keyboard.GetState();

            // Навигация по меню
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _selectedIndex = (_selectedIndex - 1 + _menuItems.Length) % _menuItems.Length;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                _selectedIndex = (_selectedIndex + 1) % _menuItems.Length;
            }

            // Выбор опции
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                HandleMenuSelection();
            }

            base.Update(gameTime);
        }

        private void HandleMenuSelection()
        {
            switch (_selectedIndex)
            {
                case 0: // Новая игра
                    Console.WriteLine(GetLocalizedString("NewGameStart"));
                    break;
                case 1: // Загрузка игры
                    Console.WriteLine(GetLocalizedString("LoadGameStart"));
                    break;
                case 2: // Настройки
                    _currentLanguage = _currentLanguage == "Русский" ? "English" : "Русский"; // Пример смены языка
                    SetMenuItems(); // Обновление пунктов меню
                    break;
                case 3: // Выход
                    Exit();
                    break;
            }
        }

    protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Заголовок меню
            var title = GetLocalizedString("MainMenuTitle");
            _spriteBatch.DrawString(_font, title, new Vector2(100, 50), Color.White);

            // Отрисовка пунктов меню
            for (int i = 0; i < _menuItems.Length; i++)
            {
                var color = i == _selectedIndex ? Color.Yellow : Color.White;
                _spriteBatch.DrawString(_font, _menuItems[i], new Vector2(100, 100 + i * 40), color);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
