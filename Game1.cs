﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingTarget;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _targetSprite;
    private Texture2D _crosshairsSprite;
    private Texture2D _backgroundSprite;
    private SpriteFont _font;
    
    private Vector2 _targetPosition = new Vector2(100, 300);
    private const int _targetRadius = 45;

    private MouseState _mouseState;
    private bool _mouseReleased;
    private int _score = 0;
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 1080;
        _graphics.PreferredBackBufferHeight = 608;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _targetSprite = Content.Load<Texture2D>("target");
        _backgroundSprite = Content.Load<Texture2D>("background");
        _crosshairsSprite = Content.Load<Texture2D>("crosshairs");
        _font = Content.Load<SpriteFont>("galleryFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _mouseState  = Mouse.GetState();
         
        if (_mouseState.LeftButton == ButtonState.Pressed && _mouseReleased)
        {
            float mouseTargetDistance = Vector2.Distance(_targetPosition, _mouseState.Position.ToVector2());
            Console.WriteLine(mouseTargetDistance);
            if (mouseTargetDistance < _targetRadius)
            {
                _score++;
            }
            _mouseReleased = false;
        }

        if (_mouseState.LeftButton == ButtonState.Released)
        {
            _mouseReleased = true;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        _spriteBatch.Draw(_backgroundSprite, new Vector2(0, 0), Color.White);
        _spriteBatch.DrawString(_font, _score.ToString(), new Vector2(10, 10), Color.White);
        _spriteBatch.Draw(_targetSprite, new Vector2(_targetPosition.X - _targetRadius, _targetPosition.Y - _targetRadius ), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}