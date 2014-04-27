using System.Collections.Generic;
using DummyProject.Factories;
using DummyProject.Helpers;
using DummyProject.Objects;
using DummyProject.Objects.Buildings.DataLayer;
using DummyProject.Primitives;
using DummyProject.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Point = Microsoft.Xna.Framework.Point;
using SpriteFont = Microsoft.Xna.Framework.Graphics.SpriteFont;
using System.Xml;
using Microsoft.Xna.Framework.Content;
using StrategyGameLibrary;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace DummyProject.Views
{
    public class CityMapViewManager : DrawableGameComponent
    {
        /// <summary>
        /// Ordem em que este componente será desenhado.
        /// </summary>
        private const int drawOrder = 1;
        private const int updateOrder = 1;

        public bool IsMouseOverCityMap { get; protected set; }
        public bool ShowCityEdges { get; set; }
        public bool ShowBlockEdges { get; set; }
        public bool ShowBuildings { get; set; }

        private DrawableCityBlock[,] Blocks;

        Texture2D selectionTexture = null;

        public Polygon MapPolygon { get; private set; }
        private Texture2D pequenaCasa = null;
        private SpriteFont spriteFont = null;

        SpriteBatch spriteBatch = null;

        private int Width;
        private int Height;

        TinyHouse testeHouse = null;

        private Texture2D grandeCasa = null;

        private Texture2D House3x3 = null;

        public CityMapViewManager(StrategyGameThread game, int width, int height) : base(game)
        {
            Width = width;
            Height = height;

            //coloca a camera no mais ou menos no centro do mapa
            //Camera.Location = Polygon.CalculateCenter(Camera.RelativeToCamera(this.MapPolygon));
            //Camera.Location = Polygon.CalculateCenter(Camera.RelativeToCamera(this.MapPolygon));
        }

        public override void Initialize()
        {
            this.IsMouseOverCityMap = false;
            this.DrawOrder = drawOrder;
            this.UpdateOrder = updateOrder;
            this.ShowBlockEdges = false;
            this.ShowBuildings = true;

            base.Initialize();
        }

        public DrawableCityBlock this[int x, int y] 
        {
            get
            {
                return Blocks[x, y];
            }
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(base.Game.GraphicsDevice);

            // Populate your tiles list with your textures here
            //Tiles = new List<Texture2D>();
            Texture2D tmp = base.Game.Content.Load<Texture2D>(@"CityMap\Cubes\cubo_terra");
            //Tiles.Add(tmp);

            selectionTexture = base.Game.Content.Load<Texture2D>(@"CityMap\selection_maior");
            //cuboSemLinha = this.content.Load<Texture2D>("cubosemlinha");

            pequenaCasa = base.Game.Content.Load<Texture2D>(@"CityMap\pequena_casa");

            //grandeCasa = base.Game.Content.Load<Texture2D>(@"Buildings\casa_grande");
            grandeCasa = base.Game.Content.Load<Texture2D>(@"Buildings\quartel_2x2");

            House3x3 = base.Game.Content.Load<Texture2D>(@"Buildings\casa3x3");

            spriteFont = base.Game.Content.Load<SpriteFont>(@"SpriteFont");

            //testeHouse = new TinyHouse(pequenaCasa);

            //  Initialise block 2D array
            Blocks = new DrawableCityBlock[Width, Height];

            // Used as a base width and height for varying sized tiles
            CityBlock.BaseWidth = tmp.Width;
            CityBlock.BaseHeight = tmp.Height / 2;

            Rectangle position = new Rectangle(0, 0, tmp.Width, tmp.Height);

            for (int x = 0; x < Width; x++)
            {
                position.X = (CityBlock.BaseWidth / 2) * x;
                position.Y = (CityBlock.BaseHeight / 2) * x;

                for (int y = 0; y < Height; y++)
                {
                    Blocks[x, y] = new DrawableCityBlock(new CityBlock(position, new Point(x,y)));

                    Blocks[x, y].Coordinates = x + "," + y;

                    position.Y += (CityBlock.BaseHeight / 2);
                    position.X += -(CityBlock.BaseWidth / 2);
                }
            }

            CalculateCityEdges();

            base.LoadContent();
        }

        private void CalculateCityEdges()
        {
            MapPolygon = new Polygon();
        
            //primeira aresta "de cima"
            /*
             *      Start
             *     /
             *    /
             *   /
             * End
             * */
            Edge e1 = new Edge
            {
                StartVertex = Blocks[0, 0].CityBlock.TopPolygon.Edges[0].StartVertex,
                EndVertex = Blocks[0, Height - 1].CityBlock.TopPolygon.Edges[0].EndVertex
            };

            ////segunda aresta "de cima"
            ///*
            // *   End
            // *      \
            // *       \
            // *        \
            // *        Start
            // * */
            Edge e2 = new Edge
            {
                StartVertex = Blocks[Width - 1, 0].CityBlock.TopPolygon.Edges[3].EndVertex,
                EndVertex = e1.StartVertex
            };

            ////primeira aresta "de baixo"
            ///*
            // *   Start
            // *      \
            // *       \
            // *        \
            // *        End
            // * */
            Edge e3 = new Edge
            {
                StartVertex = e1.EndVertex,
                EndVertex = Blocks[Width - 1, Height - 1].CityBlock.TopPolygon.Edges[3].StartVertex
            };

            ////segunda aresta  "de baixo"
            ///*
            // *      End
            // *     /
            // *    /
            // *   /
            // * Start
            // * */
            Edge e4 = new Edge
            {
                StartVertex = e3.EndVertex,
                EndVertex =  e2.StartVertex
            };

            MapPolygon.Edges.Add(e1);
            MapPolygon.Edges.Add(e2);
            MapPolygon.Edges.Add(e3);
            MapPolygon.Edges.Add(e4);
        }

        ///
        /// Called during StrategyGameThread update method to update
        /// map and input.
        ///
        public override void  Update(GameTime gameTime)
        {
            MouseState currentMouseState = GameSession.InputCache.CurrentMouseState;
            MouseState previousMouseState = GameSession.InputCache.PreviousMouseState;
            KeyboardState currentKeyboardState = GameSession.InputCache.CurrentKeyboardState;
            KeyboardState previousKeyboardState = GameSession.InputCache.PreviousKeyboardState;
            
            this.SetIsMouseOverMap(currentMouseState);
            
            int speed = 10;

            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                Camera.Location.Y += speed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                Camera.Location.Y -= speed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                Camera.Location.X += speed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                Camera.Location.X -= speed;
            }

            if (GameSession.InputCache.wasButtonPressedAndReleased(Keys.Z))
            {
                this.ShowBuildings = !this.ShowBuildings;
            }

            if (GameSession.InputCache.wasButtonPressedAndReleased(Keys.Q))
            {
                //XmlWriterSettings settings = new XmlWriterSettings();
                //settings.Indent = true;
                //
                //using (XmlWriter writer = XmlWriter.Create("example.xml", settings))
                //{
                //    IntermediateSerializer.Serialize(writer, new object(), null);
                //}

                
                //GenericBuildingTest.Teste(new StrategyGameLibrary.GenericBuildingTestcs());//, HoldableObjectType.Building));
                //ContentBuilder contentBuilder = new ContentBuilder();
                //ContentManager teste = new ContentManager();
                ((StrategyGameThread)base.Game).ContentBuilder.Clear();
                ((StrategyGameThread)base.Game).ContentBuilder.Add(@"C:\teste\BuildingObjects\MiniHouse.xml", "MiniHouse2", "XmlImporter", null);
                string a = ((StrategyGameThread)base.Game).ContentBuilder.Build();
//                contentBuilder.Build();

                //LoadableGameComponent teste = AssetLoader.Load<GenericBuildingTestcs>(@"BuildingObjects\", "MiniHouse");
            }

            //if (GameSession.InputCache.wasButtonPressedAndReleased(Keys.C))
            //{
            //    Camera.Location = Polygon.CalculateCenter(Camera.RelativeToCamera(this.MapPolygon));
            //}

            if (currentKeyboardState.IsKeyDown(Keys.G) && previousKeyboardState.IsKeyUp(Keys.G))
            {
                if (GameSession.MouseHolding.HoldableObject == null)
                {
                    GameSession.MouseHolding.HoldableObject =
                        new TinyHouse(new List<Resource>() { ResourceFactory.getInstance().createResouce(ResourceType.Wood, 10) },
                            20, 30,
                            //pequenaCasa, 
                            grandeCasa,
                            new Point(2,2), HoldableObjectType.Building);
                }
                else
                {
                    GameSession.MouseHolding.Reset();
                }
            }
            else if (currentKeyboardState.IsKeyDown(Keys.H) && !previousKeyboardState.IsKeyDown(Keys.H))
            {
                //if (GameSession.MouseHolding.HoldingTexture == null)
                //{
                //    GameSession.MouseHolding.HoldingTexture = selectionTexture;
                //}
                //else
                //{
                //    GameSession.MouseHolding.HoldingTexture = null;
                //}

                if (GameSession.MouseHolding.HoldableObject == null)
                {
                    GameSession.MouseHolding.HoldableObject =
                        new TinyHouse(new List<Resource>() { ResourceFactory.getInstance().createResouce(ResourceType.Wood, 10) },
                            20, 30,
                        pequenaCasa, 
                        //    grandeCasa,
                            new Point(1,1), HoldableObjectType.Building);
                }
                else
                {
                    GameSession.MouseHolding.Reset();
                }
            }
            else if (GameSession.InputCache.wasButtonPressedAndReleased(Keys.J))
            {
                if (GameSession.MouseHolding.HoldableObject == null)
                {
                    GameSession.MouseHolding.HoldableObject =
                        new TinyHouse(new List<Resource>() { ResourceFactory.getInstance().createResouce(ResourceType.Wood, 10) },
                            20, 30,
                        House3x3, 
                        //    grandeCasa,
                            new Point(3,3), HoldableObjectType.Building);
                }
                else
                {
                    GameSession.MouseHolding.Reset();
                }
            }

            if (IsMouseOverCityMap) //mouseover
            {
                //this.HighLightBlock(currentMouseState);
                //this.DrawHouse(currentMouseState

                /*if (GameSession.InputCache.hasLeftButtonClicked && IsMouseOverCityMap)
                    //&& previousMouseState.LeftButton == ButtonState.Released)
                {
                    // codigo que pinta a celula na qual o mouse está em cima
                    //Point p = new Point(currentMouseState.X, currentMouseState.Y);
                    //
                    //foreach (DrawableCityBlock gb in Blocks)
                    //    if (Polygon.PointInPolygon(p, Camera.RelativeToCamera(gb.TopPolygon)))
                    //    {
                    //        gb.IsPainted = true;
                    //        break;
                    //    }

                    if (GameSession.MouseHolding.HoldableObject != null)
                    {
                        if (GameSession.MouseHolding.HoldableObject.Type == HoldableObjectType.Building)
                        {
                           GenericBuilding gb = (GenericBuilding)GameSession.MouseHolding.HoldableObject;

                            if (this.IsMouseOverCityMap)
                            {
                               DrawableCityBlock[] tmpBlocks =
                                   this.GetCityBlocksIfAvailable(
                                   this.GetCityBlockAccordingToMousePosition(currentMouseState).MatrixPosition, gb.Area);

                                if (tmpBlocks != null && tmpBlocks.Length > 1)
                                {
                                    DrawableCityBlock rightBlock = tmpBlocks[0];
                                    for (int i = 0; i < tmpBlocks.Length; i++)
                                    {
                                        if (!Utils.RightSideBlock(rightBlock, tmpBlocks[i]))
                                        {
                                            rightBlock.Build((GenericBuilding)GameSession.MouseHolding.HoldableObject, true);
                                            rightBlock = tmpBlocks[i];
                                        }
                                        else
                                        {
                                            tmpBlocks[i].Build((GenericBuilding)GameSession.MouseHolding.HoldableObject, true);
                                        }
                                    }

                                    rightBlock.Build((GenericBuilding)GameSession.MouseHolding.HoldableObject, false);
                               }
                               else if (tmpBlocks != null)
                               { 
                                   tmpBlocks[0].Build((GenericBuilding)GameSession.MouseHolding.HoldableObject, false);
                               }
                            }
                        }

                        GameSession.MouseHolding.Reset();
                        //}
                    }
                    else if (GameSession.MouseHolding.HoldableObject == null)
                    { 
                        //Point p = new Point(currentMouseState.X, currentMouseState.Y);

                        DrawableCityBlock b = this.GetCityBlockAccordingToMousePosition(currentMouseState);

                        //foreach (DrawableCityBlock gb in Blocks)
                        //    if (Polygon.PointInPolygon(p, Camera.RelativeToCamera(gb.TopPolygon))) //mouse está sobre um bloco
                        //    {


                        if (b != null)
                            GameSession.CallOnSelectBuilding(b.Building);

                          //      break;
                        //    }
                    }
                }
                else if (currentMouseState.MiddleButton == ButtonState.Pressed 
                    && IsMouseOverCityMap && previousMouseState.MiddleButton == ButtonState.Released)
                {
                    
                }
                else if (currentMouseState.RightButton == ButtonState.Pressed
                    && IsMouseOverCityMap && previousMouseState.RightButton == ButtonState.Pressed)
                {
                    Point p = new Point(currentMouseState.X, currentMouseState.Y);

                    foreach (DrawableCityBlock b in Blocks)
                        if (Polygon.PointInPolygon(p, Camera.RelativeToCamera(b.TopPolygon))) //mouse está sobre um bloco
                        {
                            //selecionando a construção
                            if (b.HasBuilding)
                            {
                                //MessageBox.Show("Construção: " + gb.Building.ToString()); 
                                break;
                            }
                        }
                    //Point p = new Point(currentMouseState.X, currentMouseState.Y);
                    //
                    //foreach (DrawableCityBlock gb in Blocks)
                    //    if (Polygon.PointInPolygon(p, Camera.RelativeToCamera(gb.TopPolygon)))
                    //    {
                    //        gb.IsPainted = false;
                    //        break;
                    //    }


                }*/
            }
            //se clicar com o botao esquerdo
            //if (currentMouseState.LeftButton == ButtonState.Pressed)
            //{
            //    Point p = new Point(currentMouseState.X, currentMouseState.Y);
            //
            //    foreach (DrawableCityBlock gb in Blocks)
            //        if (Polygon.PointInPolygon(p, Camera.RelativeToCamera(gb.TopPolygon)))
            //        {
            //            gb.IsPainted = true;
            //            break;
            //        }
            //} //se clicar com o botao direito
            //if (currentMouseState.RightButton == ButtonState.Pressed)
            //{
            //    Point p = new Point(currentMouseState.X, currentMouseState.Y);
            //
            //    foreach (DrawableCityBlock gb in Blocks)
            //        if (Polygon.PointInPolygon(p, Camera.RelativeToCamera(gb.TopPolygon)))
            //        {
            //            gb.IsPainted = false;
            //            break;
            //        }
            //}
            
            base.Update(gameTime);
        }

        private void DrawCityEdges()
        {
            if (ShowCityEdges)
                PrimitiveDrawing.DrawPolygon(spriteBatch, Utils.BlankTexture, 3, Color.Aqua, Camera.RelativeToCamera(this.MapPolygon));
        }

        private DrawableCityBlock GetCityBlockAccordingToMousePosition(MouseState m)
        {
            Point p = new Point(m.X, m.Y);

            foreach (DrawableCityBlock b in Blocks)
                if (Polygon.PointInPolygon(p, Camera.RelativeToCamera(b.CityBlock.TopPolygon)))
                    return b;

            return null;
        }

        public void DrawCityBlocksAndBuildings()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    DrawableCityBlock block = Blocks[x, y];
                    
                    spriteBatch.Draw(block.Texture, Camera.RelativeToCamera(block.CityBlock.Position),
                        Color.White);
                    
                    if (ShowBlockEdges)
                        PrimitiveDrawing.DrawPolygon(spriteBatch, Utils.BlankTexture, 1, Color.Red, Camera.RelativeToCamera(block.CityBlock.TopPolygon));
                    
                    if (!block.IsBuildable)
                    {
                        Rectangle teste = new Rectangle(block.CityBlock.Position.X, block.CityBlock.Position.Y, selectionTexture.Width, selectionTexture.Height);
                        spriteBatch.Draw(selectionTexture, Camera.RelativeToCamera(teste), Color.Red);
                    }

                    Rectangle teste2 = Camera.RelativeToCamera(block.CityBlock.Position);
                    Vector2 v = new Vector2(teste2.X + 20, teste2.Y + 10);
                    
                    if (block.Building == null)
                    {
                        spriteBatch.DrawString(spriteFont, block.Coordinates, v, Color.Black);
                    }

                    if (block.IsReferenceForBuilding && block.Building != null)
                    {
                        spriteBatch.DrawString(spriteFont, "R", v, Color.Black);
                    }
                    else if (!block.IsReferenceForBuilding && block.Building != null)
                    {
                        spriteBatch.DrawString(spriteFont, "B", v, Color.Black);
                    }
                }

            if (ShowBuildings)
            {

                for (int y = 0; y < Height; y++)
                {
                    int tmpY = y;
                    for (int x = 0; tmpY >= 0; x++)
                    {
                        DrawableCityBlock block = Blocks[x, tmpY];

                        if (block.Building != null && !block.IsReferenceForBuilding)
                            spriteBatch.Draw(block.Building.Texture, Camera.RelativeToCamera(block.Building.Location), Color.White);

                        tmpY--;
                    }
                }

                int startX = 1;

                do
                {
                    int tmpY = Height - 1;
                    for (int x = startX; x < Width; x++)
                    {
                        DrawableCityBlock block = Blocks[x, tmpY];

                        if (block.Building != null && !block.IsReferenceForBuilding)
                            spriteBatch.Draw(block.Building.Texture, Camera.RelativeToCamera(block.Building.Location), Color.White);

                        tmpY--;
                    }

                    startX++;
                } while (startX < Width);
            }
        }
        
        ///
        /// Draws map
        ///
        /// <param name="spriteBatch" />
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            this.DrawCityBlocksAndBuildings();

            this.DrawHighlightedCityBlocks();

            this.DrawMouse();

            this.DrawCityEdges();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawHighlightedCityBlocks()
        {
            foreach (DrawableCityBlock b in Blocks)
            {
                Rectangle teste = new Rectangle(b.CityBlock.Position.X, b.CityBlock.Position.Y, 86, 51);

                if (b.IsHighlighted)
                    spriteBatch.Draw(selectionTexture, Camera.RelativeToCamera(teste), Color.Red);
                else if (b.IsPainted)
                    spriteBatch.Draw(selectionTexture, Camera.RelativeToCamera(teste), Color.WhiteSmoke);
            }
        }

        private void DrawCityBlockEdges()
        {
            if (ShowBlockEdges)
            {
                foreach (DrawableCityBlock cb in this.Blocks)
                    PrimitiveDrawing.DrawPolygon(spriteBatch, Utils.BlankTexture, 1, Color.Red, Camera.RelativeToCamera(cb.CityBlock.TopPolygon));
            }
        }

        private DrawableCityBlock GetMouseOverCityBlock(MouseState m)
        {
            Point p = new Point(m.X, m.Y);

            foreach (DrawableCityBlock b in Blocks)
                if (Polygon.PointInPolygon(p, Camera.RelativeToCamera(b.CityBlock.TopPolygon)))
                {
                    return b;
                }

            return null;
        }

        private void SetIsMouseOverMap(MouseState m)
        {
            if (Polygon.PointInPolygon(new Point(m.X, m.Y),
                Camera.RelativeToCamera(this.MapPolygon)))
            {
                this.IsMouseOverCityMap = true;
            }
            else
            {
                this.IsMouseOverCityMap = false;
            }
        }

        static int testV = 0;

        private void DrawMouse()
        {
            if (GameSession.MouseHolding.HoldableObject != null)
            {
                MouseState m = GameSession.InputCache.CurrentMouseState;
                KeyboardState ck = GameSession.InputCache.CurrentKeyboardState;
                KeyboardState pk = GameSession.InputCache.PreviousKeyboardState;

                if (ck.IsKeyDown(Keys.L) && pk.IsKeyUp(Keys.L))
                    testV++;
                else if (ck.IsKeyDown(Keys.M) && pk.IsKeyUp(Keys.M))
                    testV--;

                //frescuras para fazer com que o mouse fique +/- no centro da textura

                Rectangle textureRectangle = new Rectangle(m.X - (GameSession.MouseHolding.HoldableObject.Texture.Width / 2), m.Y, GameSession.MouseHolding.HoldableObject.Texture.Width,
                    GameSession.MouseHolding.HoldableObject.Texture.Height);
                
                textureRectangle.Location = new Point(textureRectangle.Location.X, 
                    textureRectangle.Location.Y);

                spriteBatch.Draw(GameSession.MouseHolding.HoldableObject.Texture, textureRectangle, Color.White);
            }
        }

        public DrawableCityBlock[] GetCityBlocksIfAvailable(Point clicked, Point area)
        {
            int size = area.X * area.Y;
            int count = 0;
            DrawableCityBlock[] tmpBlocks = new DrawableCityBlock[size];
            
            //retorna null se a area for maior que 1 e se a estrutura for sair fora do mapa
            if ((area.X > 1) && ((clicked.X + area.X > Width)
                || (clicked.Y + area.Y > this.Height)))
            {
                return null;
            }
            else if (area.X == 1 && area.Y == 1)
            {
                tmpBlocks[size - 1] = Blocks[clicked.X, clicked.Y];
                return tmpBlocks;
            }
            
            for (int x = clicked.X; x < clicked.X + area.X; x++)
                for (int y = clicked.Y; y < clicked.Y + area.Y; y++)
                    if (Blocks[x, y].IsBuildable)
                    {
                        tmpBlocks[count++] = Blocks[x, y];
                    }
            
            return Utils.IsAllValuesNotNull(tmpBlocks) ? tmpBlocks : null;
        }

        public static event MapClickDelegate OnMapClick;

        public static event BuildGenericBuilding OnBuildGenericBuilding;
    }
}
