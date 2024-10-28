using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace coding;

public class Game1 : Game
{
    Texture2D ballTexture;
    Vector2 ballPosition;
    float ballSpeed;
    int deadZone;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

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

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        ballTexture = Content.Load<Texture2D>("ball");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

 // The time since Update was called last.
        float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        var kstate = Keyboard.GetState();
        
        if (kstate.IsKeyDown(Keys.Up))
        {
            ballPosition.Y -= updatedBallSpeed;
        }
        
        if (kstate.IsKeyDown(Keys.Down))
        {
            ballPosition.Y += updatedBallSpeed;
        }
        
        if (kstate.IsKeyDown(Keys.Left))
        {
            ballPosition.X -= updatedBallSpeed;
        }
        
        if (kstate.IsKeyDown(Keys.Right))
        {
            ballPosition.X += updatedBallSpeed;
        }

        if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
        {
            ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
        }
        else if (ballPosition.X < ballTexture.Width / 8)
        {
            ballPosition.X = ballTexture.Width / 8;
        }

        if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
        {
            ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
        }
        else if (ballPosition.Y < ballTexture.Height / 30)
        {
            ballPosition.Y = ballTexture.Height / 30;
        }

        if(Joystick.LastConnectedIndex == 0)
        {
        JoystickState jstate = Joystick.GetState((int) PlayerIndex.One);

        if (jstate.Axes[1] < -deadZone)
        {
            ballPosition.Y -= updatedBallSpeed;
        }
        else if (jstate.Axes[1] > deadZone)
        {
            ballPosition.Y += updatedBallSpeed;
        }

        if (jstate.Axes[0] < -deadZone)
        {
            ballPosition.X -= updatedBallSpeed;
        }
        else if (jstate.Axes[0] > deadZone)
        {
            ballPosition.X += updatedBallSpeed;
        }
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
           _spriteBatch.Begin();
        _spriteBatch.Draw(ballTexture, ballPosition, Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
