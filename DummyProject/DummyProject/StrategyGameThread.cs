using System;
using System.Windows;
using System.Windows.Forms;
using DummyProject.Factories;
using DummyProject.Helpers;
using DummyProject.Views;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StrategyGameLibrary;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = Microsoft.Xna.Framework.Point;
using System.Collections;

namespace DummyProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class StrategyGameThread : Microsoft.Xna.Framework.Game
    {
        #region Parte da gambiarra
        
        private Window parentWindow = null;        
        private GraphicsDeviceManager graphics;
        private IntPtr drawSurface;
        private PictureBox pictureBox;
        
        #endregion

        private SpriteBatch spriteBatch;
        public static Texture2D dummyTexture = null;

        public ContentManager CustomContentManager { get; set; }
        public ContentBuilder ContentBuilder { get; set; }
        public ServiceContainer ServiceContainer { get; set; }
        //Texture2D teste = null;
        //static TileMap myMap = new TileMap();
        //int squaresAcross = 17;
        //int squaresDown = 37;
        //Texture2D blankTexture = null;
        //int baseOffsetX = -32;
        //int baseOffsetY = -64;
        //float heightRowDepthMod = 0.0000001f;
       
        public CityMapViewManager mapTeste = null;

        #region gameComponents

        private FPSComponent fpsComponent = null;

        #endregion

        #region Untouchable
        /// <summary>
        /// Don't touch this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = drawSurface;            
        }

        public void UpdateTitle()
        {
            Point p = new Point(Mouse.GetState().X, Mouse.GetState().Y);
            //parentWindow.Title = "Camera.X: " + Camera.Location.X.ToString()
            //    + " Camera.Y: " + Camera.Location.Y.ToString()
            //    + " Mouse.X: " + p.X.ToString() 
            //    + " Mouse.Y: " + p.Y.ToString() + " IsMouseOverCity: " + mapTeste.IsMouseOverCityMap;
            //parentWindow.Title = "e1: " + mapTeste.MapPolygon.Edges[0] +
            //                     " e2: " + mapTeste.MapPolygon.Edges[1] +
            //                     " e3: " + mapTeste.MapPolygon.Edges[2] +
            //                     " e4: " + mapTeste.MapPolygon.Edges[3];
        }

        /// <summary>
        /// Don't touch this
        /// </summary>
        public new void Run()
        {
            base.Run();
        }

        /// <summary>
        /// Don't touch this
        /// </summary>
        /// <param name="drawSurface"></param>
        /// <param name="parentWindow"></param>
        /// <param name="surfacePictureBox"></param>
        public StrategyGameThread(IntPtr drawSurface, Window parentWindow, PictureBox surfacePictureBox)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreparingDeviceSettings += this.graphics_PreparingDeviceSettings;
            
            this.drawSurface = drawSurface;
            this.pictureBox = surfacePictureBox;

            Mouse.WindowHandle = this.drawSurface;

            this.parentWindow = parentWindow;
        }

        /// <summary>
        /// Don't touch this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (parentWindow.WindowState != WindowState.Minimized)
            {
                PictureBox pcbTemp = (PictureBox)sender;
                graphics.PreferredBackBufferWidth = pcbTemp.Width;
                graphics.PreferredBackBufferHeight = pcbTemp.Height;
                graphics.ApplyChanges();
            }
        }
        #endregion

        public StrategyGameThread()
        {
            graphics = new GraphicsDeviceManager(this);
        }        

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            #region Gambiarra
            //Escondendo o form do XNA
            Form MyGameForm = (Form)Form.FromHandle(Window.Handle);
            MyGameForm.Opacity = 0;
            MyGameForm.Hide();
            MyGameForm.ShowInTaskbar = false;
            #endregion

            Content.RootDirectory = "Content";

            //setando o content loader pro factory
            ResourceFactory.ContentLoader = Content;

            //nunca usar essa porra, ela vai fuder tudo
            //graphics.IsFullScreen = true;

            //descomentar
            //fpsComponent = new FPSComponent(this);
            //mapTeste = new CityMapViewManager(this, 10, 10);
            //GameSession.InputCache = new InputCache(this);
            //
            //this.Components.Add(fpsComponent);
            //this.Components.Add(mapTeste);
            //this.Components.Add(GameSession.InputCache);
            //
            //GameSession.State = GameState.CityView;

            AssetLoader.Game = this;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //textura sem nada
            Utils.BlankTexture = new Texture2D(this.GraphicsDevice, 1, 1);
            Utils.BlankTexture.SetData<Color>(new Color[] { Color.White });

            //setando content manager do AssetLoader
            AssetLoader.Game = this;

            ServiceContainer = new StrategyGameLibrary.ServiceContainer();
            ContentBuilder = new StrategyGameLibrary.ContentBuilder();
            CustomContentManager = new ContentManager(ServiceContainer, ContentBuilder.OutputDirectory);


            #region clean
            //
            //Tile.TileSetTexture = Content.Load<Texture2D>(@"CityMap\testeTileSet3");
            
            //teste
            //mapa = Content.Load<Texture2D>(@"CityMap\grade");
            //blankTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            //Color[,] arrayCor = new Color[50,50];
            //for (int i = 0; i < 50; i++)
            //    for (int y = 0; y > 50; y++)
            //    arrayCor[i,y] = Color.LightGray;
            //
            //blankTexture.SetData(arrayCor);

            //+teste
            //selectionTexture = new Texture2D(this.GraphicsDevice, 1, 1);
            //selectionTexture.SetData<Color>(new Color[] { Color.White });
            #endregion
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            this.Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //descomentar
            //MouseState previousMouseState = GameSession.InputCache.PreviousMouseState;
            //MouseState currentMouseState = GameSession.InputCache.CurrentMouseState;
            //KeyboardState previousKeyboardState = GameSession.InputCache.PreviousKeyboardState;
            //KeyboardState currentKeyboardState = GameSession.InputCache.CurrentKeyboardState;
            //
            //// esconde ou mostra o componente FPS
            //if (currentKeyboardState.IsKeyDown(Keys.LeftControl)
            //    && currentKeyboardState.IsKeyDown(Keys.D1)
            //    && previousKeyboardState.IsKeyUp(Keys.D1))
            //    fpsComponent.Visible = !fpsComponent.Visible;
            //
            //if (currentKeyboardState.IsKeyDown(Keys.B) && previousKeyboardState.IsKeyUp(Keys.B))
            //{
            //    mapTeste.ShowBlockEdges = !mapTeste.ShowBlockEdges;
            //}
            //
            //if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
            //{
            //    if (GameSession.State == GameState.CityView)
            //    {
            //        GameSession.State = GameState.MapView;
            //    }
            //    else
            //    {
            //        GameSession.State = GameState.CityView;
            //    }
            //}
            //
            //switch (GameSession.State)
            //{
            //    case GameState.CityView:
            //    {
            //        mapTeste.Enabled = true;
            //        mapTeste.Visible = true;
            //
            //        break;
            //    }
            //    case GameState.MapView:
            //    {
            //        mapTeste.Enabled = false;
            //        mapTeste.Visible = false;
            //
            //        break;
            //    }
            //}

            #region clean
            //KeyboardState ks = Keyboard.GetState();
            //if (ks.IsKeyDown(Keys.Left))
            //{
            //    Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 2, 0,
            //        (myMap.MapWidth - squaresAcross) * Tile.TileStepX);
            //}
            //
            //if (ks.IsKeyDown(Keys.Right))
            //{
            //    Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 2, 0,
            //        (myMap.MapWidth - squaresAcross) * Tile.TileStepX);
            //}
            //
            //if (ks.IsKeyDown(Keys.Up))
            //{
            //    Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 2, 0,
            //        (myMap.MapHeight - squaresDown) * Tile.TileStepY);
            //}
            //
            //if (ks.IsKeyDown(Keys.Down))
            //{
            //    Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 2, 0,
            //        (myMap.MapHeight - squaresDown) * Tile.TileStepY);
            //}
            #endregion

            this.UpdateTitle();

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);

            #region clean
            //int points = 8;
            //VertexPositionColor[] primitiveList = new VertexPositionColor[points];
            //            
            //for (int x = 0; x < points / 2; x++)
            //{
            //    for (int y = 0; y < 2; y++)
            //    {
            //        primitiveList[(x * 2) + y] = new VertexPositionColor(
            //            new Vector3(x * 100, y * 100, 0), Color.White);
            //    }
            //}
            ////
            //// Initialize an array of indices of type short.
            //short[] lineListIndices = new short[(points * 2) - 2];
            //
            //// Populate the array with references to indices in the vertex buffer
            //for (int i = 0; i < points - 1; i++)
            //{
            //    lineListIndices[i * 2] = (short)(i);
            //    lineListIndices[(i * 2) + 1] = (short)(i + 1);
            //}
            ////
            ////GraphicsDevice.Indices = lineListIndices;
            ////GraphicsDevice.SetVertexBuffer(vertexBuffer);
            ////
            ////GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
            ////    PrimitiveType.LineList,
            ////    primitiveList,
            ////    0,  // vertex buffer offset to add to each element of the index buffer
            ////    8,  // number of vertices in pointList
            ////    lineListIndices,  // the index buffer
            ////    0,  // first index element to read
            ////    7   // number of primitives to draw
            ////);
            //
            //VertexBuffer vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 8, BufferUsage.Nothing);
            //
            //vertexBuffer.SetData<VertexPositionColor>(primitiveList);
            //
            //IndexBuffer lineListIndexBuffer = new IndexBuffer(
            //    GraphicsDevice,
            //    IndexElementSize.SixteenBits,
            //    sizeof(short) * lineListIndices.Length,
            //    BufferUsage.Nothing);
            //
            //lineListIndexBuffer.SetData<short>(lineListIndices);
            //
            //GraphicsDevice.Indices = lineListIndexBuffer;
            //GraphicsDevice.SetVertexBuffer(vertexBuffer);
            //
            ////GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, 8, 0, 7);
            #endregion

            //spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Begin();

            #region old
            //spriteBatch.Draw(mapa, new Vector2(20,20), Color.CornflowerBlue);

            //spriteBatch.DrawString(spriteFont, "teste",
            //    localFrase, Color.Black);

            //spriteBatch.Draw(selectionTexture, horizontalBar1, Color.White);
            //spriteBatch.Draw(selectionTexture, horizontalBar2, Color.White);
            //spriteBatch.Draw(selectionTexture, verticalBar1, Color.White);
            //spriteBatch.Draw(selectionTexture, verticalBar2, Color.White);

            //Rectangle a = new Rectangle(20, 20, 10, 30);

            //int xPos = (int)player.Position.X;
            //int yPos = (int)player.Position.Y;
            //Vector2 origin = new Vector2(50, 50);


            //spriteBatch.Draw(selectionTexture, a, Color.Red);

            //spriteBatch.Draw(selectionTexture, 
            //    new Vector2(15 + 20, 30 - 10), null,
            //    Color.Red, 50f, cannonOrigin, 10f, SpriteEffects.Nothing, 0);

            //spriteBatch.Draw(selectionTexture, new Vector2(30f,30f), null, Color.White,
            //        0.5f, origin, 1.0f, SpriteEffects.Nothing, 0.0f);

            #endregion


            Primitives.PrimitiveDrawing.DrawLine(spriteBatch, Utils.BlankTexture, 1, Color.Red,
                new Primitives.Edge(new Vector2(0,0), new Vector2(0, 40)));

            /*Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            for (int y = 0; y < squaresDown; y++)
            {
                int rowOffset = 0;
                if ((firstY + y) % 2 == 1)
                    rowOffset = Tile.OddRowXOffset;

                for (int x = 0; x < squaresAcross; x++)
                {
                    int mapx = (firstX + x);
                    int mapy = (firstY + y);
                    depthOffset = 0.7f - ((mapx + (mapy * Tile.TileWidth)) / maxdepth);

                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].BaseTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            new Rectangle(
                                (x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX,
                                (y * Tile.TileStepY) - offsetY + baseOffsetY,
                                Tile.TileWidth, Tile.TileHeight),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            SpriteEffects.Nothing,
                            1.0f);
                    }

                    int heightRow = 0;

                    foreach (int tileID in myMap.Rows[mapy].Columns[mapx].HeightTiles)
                    {
                        spriteBatch.Draw(
                            Tile.TileSetTexture,
                            new Rectangle(
                                (x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX,
                                (y * Tile.TileStepY) - offsetY + baseOffsetY - (heightRow * Tile.HeightTileOffset),
                                Tile.TileWidth, Tile.TileHeight),
                            Tile.GetSourceRectangle(tileID),
                            Color.White,
                            0.0f,
                            Vector2.Zero,
                            SpriteEffects.Nothing,
                            depthOffset - ((float)heightRow * heightRowDepthMod));
                        heightRow++;
                    }
                }
            }

            //this.DrawLine(spriteBatch, blankTexture, 10f, Color.Black, new Vector2(10, 10),
            //    new Vector2(40, 20));


            spriteBatch.End();*/

            //mapTeste.Draw(spriteBatch);

            //this.DrawLine(spriteBatch, selectionTexture, 10, Color.Yellow, new Vector2(55, 55),
            //    new Vector2(100, 100));

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle,
                                                          Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        //public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        //{
        //    Vector2[] vertex = new Vector2[4];
        //    vertex[0] = new Vector2(rectangle.Left, rectangle.Top);
        //    vertex[1] = new Vector2(rectangle.Right, rectangle.Top);
        //    vertex[2] = new Vector2(rectangle.Right, rectangle.Bottom);
        //    vertex[3] = new Vector2(rectangle.Left, rectangle.Bottom);
        //
        //    DrawPolygon(spriteBatch, vertex, 4, color, lineWidth);
        //}
        //
        //public static void DrawPolygon(SpriteBatch spriteBatch, Vector2[] vertex, int count, Color color, int lineWidth)
        //{
        //    if (count < gt)
        //    {
        //        for (int i = 0; i < lt; count - 1; i++)
        //        {
        //            DrawLineSegment(spriteBatch, vertex[i], vertex[i + 1], color, lineWidth);
        //        }
        //        DrawLineSegment(spriteBatch, vertex[count - 1], vertex[0], color, lineWidth);
        //    }
        //}

        void DrawLine(SpriteBatch batch, Texture2D blank,
              float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            batch.Draw(blank, point1, new Rectangle(5,5,5,5), color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }

        //public static void DrawLineSegment(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, int lineWidth)
        //{
        //    float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
        //    float length = Vector2.Distance(point1, point2);
        //    spriteBatch.Draw(blankTexture, point1, null, color,
        //    angle, Vector2.Zero, new Vector2(length, lineWidth),
        //    SpriteEffects.Nothing, 0f);
        //}
    }
}
