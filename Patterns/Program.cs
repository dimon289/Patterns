using System;
using System.Collections.Generic;

namespace Iterator.Collection
{
    public class EveryFlavorBean
    {
        private readonly string flavor;

        public EveryFlavorBean(string flavor)
        {
            this.flavor = flavor;
        }

        public string Flavor
        {
            get { return flavor; }
        }
    }

    public interface ICandyCollection
    {
        IBeanIterator CreateIterator();
    }

    public class BertieBottsEveryFlavorBeanBox : ICandyCollection
    {
        private List<EveryFlavorBean> items = new List<EveryFlavorBean>();

        public IBeanIterator CreateIterator()
        {
            return new BeanIterator(this);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public void Add(params string[] beans)
        {
            foreach (string bean in beans)
                items.Add(new EveryFlavorBean(bean));
        }

        public EveryFlavorBean this[int index]
        {
            get { return items[index]; }
        }
    }

    public interface IBeanIterator
    {
        EveryFlavorBean First();
        EveryFlavorBean Next();
        bool IsDone { get; }
        EveryFlavorBean CurrentBean { get; }
    }

    public class BeanIterator : IBeanIterator
    {
        private BertieBottsEveryFlavorBeanBox bertieBottsEveryFlavorBeanBox;
        private int current = 0;
        private int step = 1;

        public BeanIterator(BertieBottsEveryFlavorBeanBox bertieBottsEveryFlavorBeanBox)
        {
            this.bertieBottsEveryFlavorBeanBox = bertieBottsEveryFlavorBeanBox;
        }

        public bool IsDone => current >= bertieBottsEveryFlavorBeanBox.Count;

        public EveryFlavorBean First()
        {
            current = 0;
            return bertieBottsEveryFlavorBeanBox[current];
        }

        public EveryFlavorBean Next()
        {
            current += step;
            if (!IsDone)
                return bertieBottsEveryFlavorBeanBox[current];
            else
                return null;
        }

        public EveryFlavorBean CurrentBean => bertieBottsEveryFlavorBeanBox[current];
    }

    class Program
    {

        public static void Main()
        {
            BertieBottsEveryFlavorBeanBox beanBox = new BertieBottsEveryFlavorBeanBox();
            beanBox.Add("Banana", "Black Pepper", "Blueberry", "Booger", "Candyfloss", "Cherry", "Cinnamon", "Dirt", "Earthworm", "Earwax", "Grass", "Green Apple", "Marshmallow", "Rotten Egg", "Sausage", "Lemon", "Soap", "Tutti-Frutti", "Vomit", "Watermelon");

            BeanIterator iterator = (BeanIterator)beanBox.CreateIterator();

            for (EveryFlavorBean item = iterator.First(); !iterator.IsDone; item = iterator.Next())
            {
                Console.WriteLine(item.Flavor);
            }
        }
    }
}
