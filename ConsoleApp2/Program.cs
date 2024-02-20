using System;

public class Book
{
    public int Index { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsAvailable { get; set; }

    public Book(int index, bool isConfirmed, bool isAvailable)
    {
        Index = index;
        IsConfirmed = isConfirmed;
        IsAvailable = isAvailable;
    }
}

public class Library
{
    public bool CanBookBeIssued(Book book)
    {
        if (book.IsConfirmed && book.IsAvailable)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsBookAvailable(int bookIndex, bool isConfirmed, bool isInstock, bool isForReadingRoomOnly)
    {
        if (isConfirmed && isInstock)
        {
            if (isForReadingRoomOnly)
            {
                Console.WriteLine("Книга доступна только в читальном зале.");
            }
            else
            {
                Console.WriteLine("Книга доступна на выдачу.");
            }
            return true;
        }
        else
        {
            if (!isConfirmed)
            {
                Console.WriteLine("Индекс книги не подтвержден.");
            }
            else if (!isInstock)
            {
                Console.WriteLine("Книги нет в наличии.");
            }
            else if (!isForReadingRoomOnly)
            {
                Console.WriteLine("Можно работать с книгой вне читального зала.");
            }
            return false;
        }
    }
}

public class BookOrderProcess
{
    private Library library;

    public BookOrderProcess()
    {
        this.library = new Library();
    }

    public string GetBookAvailabilityStatus(int bookIndex)
    {
        if (library.IsBookAvailable(bookIndex, true, true, false))
        {
            if (library.CanBookBeIssued(new Book(bookIndex, true, true)))
            {
                return "Индекс книги подтвержден, книга есть в наличии и может быть выдана";
            }
            else if (!library.CanBookBeIssued(new Book(bookIndex, true, true)))
            {
                return "Индекс книги подтвержден, но книга сейчас на руках и не может быть выдана";
            }
            else if (library.CanBookBeIssued(new Book(bookIndex, true, false)))
            {
                return "Индекс книги подтвержден, книга есть в наличии, но с ней можно работать только в читальном зале";
            }
        }
        else
        {
            return "Индекс книги не подтвержден и книга не может быть выдана";
        }

        return "";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BookOrderProcess orderProcess = new BookOrderProcess();

        int bookIndex = 123;

        string availabilityStatus = orderProcess.GetBookAvailabilityStatus(bookIndex);

        Console.WriteLine(availabilityStatus);
    }
}

