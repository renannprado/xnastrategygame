using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DummyProject.Primitives;
using Microsoft.Xna.Framework;
using DummyProject.Helpers;
using System.Runtime.Serialization;

namespace DummyProject.Objects.Buildings.DataLayer
{
    public class CityBlock
    {
        public Rectangle Position { get; set; }

        //dar um jeito de setar esses valores
        public static int BaseWidth { get; set; } 
        public static int BaseHeight { get; set; }

        public List<Resource> Resources { get; private set; }

        public Polygon TopPolygon { get; private set; }

        #region deleteme
        //public bool IsHighlighted { get; set; }
        //public bool IsPainted { get; set; }
        //public List<Resource> Resources { get; private set; }
        //public Texture2D Texture { get; set; }
        //public String Coordinates { get; set; }
        #endregion

        //passar generic building pra não ter textura
        public GenericBuilding Building { get; private set; } 

        public bool IsBuildable { get; private set; }
        public bool HasBuilding { get { return Building == null ? false : true; } }
        
        public Point MatrixPosition { get; set; }

        public bool IsReferenceForBuilding { get; private set; }

        public CityBlock(Rectangle Position, Point MatrixPosition)
        {
            this.Position = Position;
            this.MatrixPosition = MatrixPosition;

            this.CalculateEdges();
        }

        private void CalculateEdges()
        {
            TopPolygon = new Polygon();

            //primeira aresta "de cima"
            /*
             *      Start
             *     /
             *    /
             *   /
             * End
             * */
            TopPolygon.Edges.Add(new Edge()
            {
                StartVertex = new Vector2
                (
                    Position.Left + BaseWidth / 2, //posição X + largura / 2
                    Position.Top //Y do topo do retangulo
                ),
                EndVertex = new Vector2
                (
                    Position.Left, //X do retangulo
                    Position.Top + BaseHeight / 2 //positção Y + altura / 4
                )
            });

            //segunda aresta "de cima"
            /*
             *   End
             *      \
             *       \
             *        \
             *        Start
             * */
            TopPolygon.Edges.Add(new Edge()
            {
                StartVertex = new Vector2
                (
                    Position.Left + BaseWidth, //posição X + largura / 2
                    Position.Top + BaseHeight / 2 //posição Y + altura / 2
                ),
                EndVertex = new Vector2
                (
                    Position.Left + BaseWidth / 2, //posição X + largura / 2
                    Position.Top //Y do topo do retangulo
                )
            });

            //primeira aresta "de baixo"
            /*
             *   Start
             *      \
             *       \
             *        \
             *        End
             * */
            TopPolygon.Edges.Add(new Edge()
            {
                StartVertex = new Vector2
                (
                    Position.Left, //X do retangulo
                    Position.Top + BaseHeight / 2 //positção Y + BaseHeight / 2
                ),
                EndVertex = new Vector2
                (
                    Position.Left + BaseWidth / 2, //largura / 2
                    Position.Y + BaseHeight //ja faço coment
                )
            });

            //segunda aresta  "de baixo"
            /*
             *      End
             *     /
             *    /
             *   /
             * Start
             * */
            TopPolygon.Edges.Add(new Edge()
            {
                StartVertex = new Vector2
                (
                    Position.Left + BaseWidth / 2, //largura / 2
                    Position.Y + BaseHeight //ja faço coment
                ),
                EndVertex = new Vector2
                (
                    Position.Left + BaseWidth, //largura / 2
                    Position.Y + BaseHeight / 2 //ja faço coment
                )
            });
        }

        public bool Build(GenericBuilding b, bool IsReferenceForBuilding)
        {
            if (Building == null && IsBuildable)
            {
                Building = b;
                this.IsReferenceForBuilding = IsReferenceForBuilding;

                if (Building.Area == new Point(1, 1) && !this.IsReferenceForBuilding)
                {
                    Building.Location = new Rectangle(
                            this.Position.X,
                            ((int)this.TopPolygon.Edges[0].EndVertex.Y - (Building.Texture.Height - (Utils.HeightOfSelectionTexture / 2))),
                            Building.Texture.Width, Building.Texture.Height);
                }
                else if (!this.IsReferenceForBuilding)// if (Building.Area == new Point(2,2))
                {
                    #region clean
                    //Building.Location = new Rectangle(
                    //        this.Position.X - (DrawableCityBlock.BaseWidth / 2),
                    //        ((int)this.TopPolygon.Edges[0].EndVertex.Y - (Building.Texture.Height - Utils.HeightOfSelectionTexture)),
                    //        Building.Texture.Width, Building.Texture.Height);

                    //pra desenhar a partir do bloco mais abaixo

                    //Building.Location = new Rectangle(
                    //        this.Position.X - ((DrawableCityBlock.BaseWidth / 2) * (Building.Area.X - 1)),
                    //        ((int)this.TopPolygon.Edges[0].EndVertex.Y - Utils.HeightOfSelectionTexture * Building.Area.Y),
                    //        Building.Texture.Width, Building.Texture.Height);
                    #endregion

                    //pra desenhar as partir do bloco mais as direita
                    Building.Location = new Rectangle(
                            (int)this.TopPolygon.Edges[3].EndVertex.X - (CityBlock.BaseWidth * Building.Area.X),// + ((DrawableCityBlock.BaseWidth / 2) * (Building.Area.X - 1)),
                            ((int)this.TopPolygon.Edges[0].StartVertex.Y + //(DrawableCityBlock.BaseHeight * (Building.Area.Y))),
                            -Building.Texture.Height),
                            Building.Texture.Width, Building.Texture.Height);
                }

                IsBuildable = false;

                return true;
            }
            else
            {
                return false;
            }
        }

        public CityBlock(SerializationInfo info, StreamingContext context)
        {
            //this.ID = (short)info.GetValue("ID", typeof(short));
            //this.Name = (string)info.GetValue("Name", typeof(string));
            //this.Label = (string)info.GetValue("Label", typeof(string));
            //this.isAvailable = (bool)info.GetValue("isAvailable", typeof(bool));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue("ID", ID);
            //info.AddValue("Name", Name);
            //info.AddValue("Label", Label);
            //info.AddValue("isAvailable", isAvailable);
        }
    }
}
