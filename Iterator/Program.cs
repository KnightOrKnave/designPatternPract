using System;
using System.Threading;

namespace Iterator
{

    public interface ImyIterator
    {
        public bool hasNext();
        public Object next();
    }
    public interface IAggregate
    {
        public ImyIterator myIterator();
    }

    public class Book
    {
        public string name{ get; set; }
        public Book(string name)
        {
            this.name = name;
        }
    }

    public class BookShelf : IAggregate
    {
        Book[] books;
        int last = 0;
        public BookShelf(int maxsize)
        {
            this.books = new Book[maxsize];
        }

        public Book getBookAt(int index)
        {
            return books[index];
        }

        public void appendBook(Book book)
        {
            this.books[last] = book;
            last++;
        }

        public int getLength()
        {
            return last;
        }

        public ImyIterator myIterator()
        {
            return new BookShelfIterator(this);
        }
    }

    public class BookShelfIterator : ImyIterator
    {
        BookShelf bookShelf;
        int index;
        public BookShelfIterator(BookShelf bookShelf)
        {
            this.bookShelf = bookShelf;
        }



        public bool hasNext()
        {
            if (index < bookShelf.getLength())
            {
                return true;
            }
            return false;
        }

        public object next()
        {
            Book book = bookShelf.getBookAt(index);
            index++;
            return book;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var bookShelf = new BookShelf(4);
            bookShelf.appendBook(new Book("Around ther World in 80"));
            bookShelf.appendBook(new Book("Bible"));
            bookShelf.appendBook(new Book("Cinderella"));
            bookShelf.appendBook(new Book("Doddy-Long-Legs"));
            ImyIterator it = bookShelf.myIterator();
            while (it.hasNext())
            {
                Book book = (Book)it.next();
                Console.WriteLine(book.name);
            }
        }
    }
}
