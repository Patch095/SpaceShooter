using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using System.Xml;

namespace Lezione_53__22Gennaio2018_Ships
{
    static class GfxManager
    {
        static Dictionary<string, Texture> textures;
        static Dictionary<string, Tuple<Texture, List<Animation>>> spritesheets;

        public static void Init()
        //static GfxManager()
        {
            textures = new Dictionary<string, Texture>();
            spritesheets = new Dictionary<string, Tuple<Texture, List<Animation>>>();
        }

        public static void Load()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Assets/SpriteSheetConf.xml");

            XmlNode root = doc.DocumentElement;

            foreach (XmlNode spritesheetNode in root.ChildNodes)
            {
                LoadSpritesheet(spritesheetNode);
            }

        }
        public static void LoadSpritesheet(XmlNode spritesheetNode)
        {
            XmlNode nameNode = spritesheetNode.FirstChild;

            string name = nameNode.InnerText;
            XmlNode filenameNode = nameNode.NextSibling;
            Texture texture = new Texture(filenameNode.InnerText);

            XmlNode frameNode = filenameNode.NextSibling;

            List <Animation> animations = new List<Animation>();

            if (frameNode.HasChildNodes)
            {
                int width = int.Parse(frameNode.FirstChild.InnerText);
                int height = int.Parse(frameNode.LastChild.InnerText);
                XmlNode animationsNode = frameNode.NextSibling;
                foreach(XmlNode animationNode in animationsNode.ChildNodes)
                {
                    animations.Add(LoadAnimation(animationNode, width, height));
                }
            }
            else
            {
                animations.Add(new Animation(texture.Width, texture.Height));
            }
            AddSpritesheet(name, texture, animations);
        }
        private static Animation LoadAnimation(XmlNode animationNode, int width, int height)
        {
            XmlNode currNode = animationNode.FirstChild;
            bool loop = bool.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            float fps = float.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int rows = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int cols = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int startX = int.Parse(currNode.InnerText);

            currNode = currNode.NextSibling;
            int startY = int.Parse(currNode.InnerText);

            return new Animation(width, height, cols, rows, fps, loop, startX, startY);
        }

        public static void AddTexture(string name, string filePath)
        {
            textures.Add(name, new Texture(filePath));
        }

        public static Texture GetTexture(string name)
        {
            if (textures.ContainsKey(name))
            {
                return textures[name];
            }
            return null;
        }

        public static void AddSpritesheet(string name, Texture t, List<Animation> a)
        {
            spritesheets.Add(name, new Tuple<Texture, List<Animation>>(t, a));
        }

        public static Tuple<Texture, List<Animation>> GetSpritesheet(string name)
        {
            if (spritesheets.ContainsKey(name))
            {
                List<Animation> newList = new List<Animation>();
                for(int i = 0; i < spritesheets[name].Item2.Count; i++)
                {
                    newList.Add((Animation)spritesheets[name].Item2[i].Clone());
                }

                return (new Tuple<Texture, List<Animation>>(spritesheets[name].Item1, newList));
            }
            return null;
        }

        public static void RemoveAll()
        {
            textures.Clear();
            spritesheets.Clear();
        }
    }
}