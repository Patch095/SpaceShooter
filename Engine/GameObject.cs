using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class GameObject : IUpdatable, IDrawable, IActivable
    {
        protected Sprite sprite;
        protected Texture texture;
        protected DrawManager.Layer layer;

        public Vector2 Velocity
        {
            get { return (RigidBody != null ? RigidBody.Velocity : Vector2.Zero); }
            set { if (RigidBody != null) RigidBody.Velocity = value; }
        }

        public RigidBody RigidBody { get; protected set; }
        public Animation Animation { get; protected set; }
        public List<Animation> Animations { get; set; }

        public int Width { get { return (int)(sprite.Width * sprite.scale.X); } }
        public int Height { get { return (int)(sprite.Height * sprite.scale.Y); } }

        public Vector2 Position
        {
            get { return sprite.position; }
            set
            {
                sprite.position = value;
                if (RigidBody != null)
                    RigidBody.Position = value;
            }
        }
        public float X { get { return sprite.position.X; } set { sprite.position.X = value; } }
        public float Y { get { return sprite.position.Y; } set { sprite.position.Y = value; } }
        public Vector2 Pivot { get { return sprite.pivot; } }
        public DrawManager.Layer Layer { get { return layer; } }

        public bool IsActive { get; set; }

        public GameObject()
        {
            //position.X = 0;
            //position.Y = 0;
        }
        public GameObject(Vector2 spritePosition, string spritesheetName, DrawManager.Layer drawLayer = DrawManager.Layer.Playground)
        {

            Tuple<Texture, List<Animation>> spritesheet = GfxManager.GetSpritesheet(spritesheetName);
            texture = spritesheet.Item1;
            Animations = spritesheet.Item2;
            Animation = Animations[0];
            sprite = new Sprite(Animation.FrameWidth, Animation.FrameHeight)
            {
                position = spritePosition
            };
            /*texture = GfxManager.GetTexture(textureName);
            sprite = new Sprite(spriteWidth > 0 ? spriteWidth : texture.Width, spriteHeight > 0 ? spriteHeight : texture.Height)
            {
                position = spritePosition
            };*/
            layer = drawLayer;

            IsActive = true;

            UpdateManager.AddItem(this);
            DrawManager.AddItem(this);

            //Animation = new Animation((int)sprite.Width, (int)sprite.Height, cols, rows, fps, loop);
        }
        public GameObject(Sprite spriteRef)
        {
            sprite = spriteRef;
        }

        public void Translate(float deltaX, float deltaY)
        {
            sprite.position.X += deltaX;
            sprite.position.Y += deltaY;
        }

        public void SetSprite(Sprite newSprite)
        {
            sprite = newSprite;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }
        public Texture GetTexture()
        {
            return texture;
        }

        public virtual void Draw()
        {
            if (IsActive)
                sprite.DrawTexture(texture, Animation.OffsetX, Animation.OffsetY, (int)sprite.Width, (int)sprite.Height);
        }

        public virtual void Update()
        {
            if (IsActive)
            {
                if (Animation.IsActive)
                    Animation.Update();
                if (RigidBody != null)
                    sprite.position = RigidBody.Position;
            }
        }

        public virtual void OnCollide(GameObject other)
        {

        }
    }
}