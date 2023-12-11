using System;
using System.Collections.Generic;

namespace Flyweight
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
            Example2();
            Example3();
        }

        public static void Example1()
        {
            var library = new Library();
            var book = library.GetPublication(
                Tuple.Create(
                    new Author("Patrick Rothfuss"),
                    "The Name of the Wind",
                    PublicationType.Book
                )
            );

            var graphicNovel = library.GetPublication(
                Tuple.Create(
                    new Author("Julie Doucet"),
                    "My New York Diary",
                    PublicationType.GraphicNovel
                )
            );

            book = library.GetPublication(
                Tuple.Create(
                    new Author("Patrick Rothfuss"),
                    "The Name of the Wind",
                    PublicationType.Book
                )
            );

            Console.WriteLine($"Library contains [{library.GetPublicationCount}] publications.");
        }

        public static void Example2()
        {
            var library = new Library();

            var patrickRothfuss = new Author("Patrick Rothfuss");
            var julieDoucet = new Author("Julie Doucet");

            var book = library.GetPublication(
                Tuple.Create(
                    patrickRothfuss,
                    "The Name of the Wind",
                    PublicationType.Book
                )
            );

            var graphicNovel = library.GetPublication(
                Tuple.Create(
                    julieDoucet,
                    "My New York Diary",
                    PublicationType.GraphicNovel
                )
            );

            book = library.GetPublication(
                Tuple.Create(
                    patrickRothfuss,
                    "The Name of the Wind",
                    PublicationType.Book
                )
            );

            Console.WriteLine($"Library contains [{library.GetPublicationCount}] publications.");
        }

        public static void Example3()
        {
            var library = new Library();

            library.GetPublication(
                Tuple.Create(
                    new Author("Dante"),
                    "Divine Comedy",
                    PublicationType.Epic
                )
            );
        }

    }

    public class Author
    {
        public string Name { get; set; }

        public Author(string name)
        {
            Name = name;
        }
    }

    public class Illustrator
    {
        public string Name { get; set; }

        public Illustrator(string name)
        {
            Name = name;
        }
    }

    public class Publisher
    {
        public string Name { get; set; }

        public Publisher(string name)
        {
            Name = name;
        }
    }

    public interface IPublication
    {
        Author Author { get; set; }
        Publisher Publisher { get; set; }
        string Title { get; set; }
    }

    public enum PublicationType
    {
        Book,
        Epic,
        GraphicNovel
    }

    public class Book : IPublication
    {
        public Author Author { get; set; }
        public int PageCount { get; set; }
        public Publisher Publisher { get; set; }
        public string Title { get; set; }

        public Book(Author author, Publisher publisher, string title)
        {
            Author = author;
            Publisher = publisher;
            Title = title;
        }

        public Book(Author author, int pageCount, Publisher publisher, string title)
        {
            Author = author;
            PageCount = pageCount;
            Publisher = publisher;
            Title = title;
        }
    }

    public class GraphicNovel : IPublication
    {
        public Author Author { get; set; }
        public Illustrator Illustrator { get; set; }
        public Publisher Publisher { get; set; }
        public string Title { get; set; }

        public GraphicNovel(Author author, Illustrator illustrator, Publisher publisher, string title)
        {
            Author = author;
            Illustrator = illustrator;
            Publisher = publisher;
            Title = title;
        }
    }

    public class Library
    {
        protected Dictionary<Tuple<Author, string, PublicationType>, IPublication> Publications =
            new Dictionary<Tuple<Author, string, PublicationType>, IPublication>();

        public int GetPublicationCount => Publications.Count;
        public class EpicPublication : IPublication
        {
            public Author Author { get; set; }
            public Publisher Publisher { get; set; }
            public string Title { get; set; }

            public EpicPublication(Author author, Publisher publisher, string title)
            {
                Author = author;
                Publisher = publisher;
                Title = title;
            }
        }
        public IPublication GetPublication(Tuple<Author, string, PublicationType> key)
        {
            IPublication publication = null;
            try
            {
                if (Publications.ContainsKey(key))
                {
                    publication = Publications[key];
                    Console.WriteLine("Existing Publication located:");
                    Console.WriteLine(publication);
                }
                else
                {
                    switch (key.Item3)
                    {
                        case PublicationType.Book:
                            publication = new Book(
                                author: key.Item1,
                                pageCount: 662,
                                publisher: new Publisher("DAW Books"),
                                title: key.Item2
                            );
                            break;
                        case PublicationType.GraphicNovel:
                            publication = new GraphicNovel(
                                author: key.Item1,
                                illustrator: new Illustrator(key.Item1.Name),
                                publisher: new Publisher("Drawn & Quarterly"),
                                title: key.Item2
                            );
                            break;
                        case PublicationType.Epic:
                            // Create a new Epic (ConcreteFlyweight) example.
                            publication = new EpicPublication(
                                author: key.Item1,
                                publisher: new Publisher("Epic Publishers"),
                                title: key.Item2
                            );
                            break;
                        default:
                            throw new ArgumentException($"[PublicationType.{key.Item3}] is not configured. Publication ('{key.Item2}' by {key.Item1.Name}) cannot be created.");
                    }
                    Console.WriteLine("New Publication created:");
                    Console.WriteLine(publication);
                    Publications.Add(key, publication);
                }
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception);
            }
            return publication;
        }

    }
}
