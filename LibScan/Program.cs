using System;
using System.Collections.Generic;
using System.IO;

namespace LibScan
{
    public class Book
    {
        public int Bookno;
        public int score;
    }
    public class Lib
    {
        public int noOfBooks;
        public int LibNo;
        public List<int> Books;
        public int SignUpTime;
        public int ShipsDay;
        public int TotalAward;
        public Lib(int noOfBooks, int libNo, List<int> books, int signUpTime, int shipsDay)
        {
            this.noOfBooks = noOfBooks;
            LibNo = libNo;
            Books = books;
            SignUpTime = signUpTime;
            ShipsDay = shipsDay;
            TotalAward = CalcReward();
        }

        int CalcReward()
        {
            int reward = 0;
            foreach (int book in Books)
                reward += Driver.Books[book].score;
            return reward;

        }
    }
    class Driver
    {
        public static int noOfLibs;
        public static int noOfBooks;
        public static int noOfDays;
        public static List<Book> Books = new List<Book>();
        public static List<Lib> Libs = new List<Lib>();

        public static void getData(string fileName)
        {
            string[] Lines = File.ReadAllLines(fileName);
            noOfBooks = int.Parse(Lines[0].Split()[0]);
            noOfLibs = int.Parse(Lines[0].Split()[1]);
            noOfDays = int.Parse(Lines[0].Split()[2]);
            GetBooks(Lines);
            GetLibs(Lines);
        }

        private  static void GetLibs(string[] Lines)
        {
            int LibCount = 0;
            for (int i = 2; i < Lines.Length; i += 2)
            {
                Libs.Add(
                    new Lib(
                        int.Parse(Lines[i].Split()[0]),
                        LibCount++,
                        AddBooks(Lines[i + 1].Split()),
                        int.Parse(Lines[i].Split()[1]),
                        int.Parse(Lines[i].Split()[2])

                    )); ;

            }
        }

        public static List<int> AddBooks(string[] books)
        {
            var BookList = new List<int>();
            foreach (var bookNo in books)
                BookList.Add(int.Parse(bookNo));
            return BookList;
        }
        private static void GetBooks(string[] Lines)
        {
            var bookScores = Lines[1].Split();
            int bookCounter = 0;
            foreach (var sc in bookScores)
                Books.Add(
                    new Book()
                    {
                        Bookno = bookCounter++,
                        score = int.Parse(sc)
                    }

                    );
        }

        static void Main(string[] args)
        {
            getData("a_example.txt");
            Console.WriteLine("Books= " + noOfBooks + "\n"
                              + "Libs " + noOfLibs + "\n"
                              + "Days " + noOfDays);


        }
    }

}
