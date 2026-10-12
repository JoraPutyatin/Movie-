using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_дз
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            Movie m1 = new Movie("Побег из шоушенка", 142);
            Movie m2 = new Movie("Зеленая книга", 130);
            Movie m3 = new Movie("Одиссея", 172);

            List<Movie> movies = new List<Movie> { m1, m2, m3 };

            CinemaHall hall = new CinemaHall(10);

            BookingService service = new BookingService(movies, hall);


            service.ShowMovies();



            Console.Write("\nВыберите фильм (номер): ");
            int movieIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.WriteLine("\nСвободные места:");
            hall.ShowSeats();

            Console.Write("\nВыберите место (номер): ");
            int seatNumber = Convert.ToInt32(Console.ReadLine());


            service.Book(movieIndex, seatNumber);



            Console.WriteLine("\nИтоговая схема зала:");
            hall.ShowSeats();

            Console.ReadLine();
        }
    }

    

    class Movie
    {
        private string title;
        private int duration;

        public Movie(string title, int duration)
        {
            Title = title;
            Duration = duration;
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (value.Length != 0 && value.Length < 50) title = value;
            }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                if (value > 0) duration = value;
            }
        }

        public override string ToString()
        {
            return $"Фильм: {Title}. Длительность: {Duration} мин.";
        }
    }

    


    class Seat
    {
        private int number;
        private bool isReserved;

        public Seat(int number)
        {
            Number = number;
            isReserved = false;
        }

        public int Number
        {
            get { return number; }
            set
            {
                if (value > 0) number = value;
            }
        }

        public bool IsReserved
        {
            get { return isReserved; }
        }


        public void Reserve()
        {
            if (isReserved)
            {
                throw new InvalidOperationException($"Место {number} уже занято");
            }
            isReserved = true;
        }

        public override string ToString()
        {
            return $"[{number}{(isReserved ? " X" : " _")}]";
        }
    }




    class CinemaHall
    {
        private List<Seat> seats;

        public CinemaHall(int count)
        {
            seats = new List<Seat>();
            for (int i = 1; i <= count; i++)
            {
                seats.Add(new Seat(i));
            }
        }

        public Seat GetSeat(int number)
        {
            return seats[number - 1];
        }

        public void ShowSeats()
        {
            foreach (Seat seat in seats)
            {
                Console.Write(seat + " ");
            }
            Console.WriteLine();
            Console.WriteLine("[N _] — свободно, [N X] — занято");
        }
    }

    



    class BookingService
    {
        private List<Movie> movies;
        private CinemaHall hall;

        public BookingService(List<Movie> movies, CinemaHall hall)
        {
            this.movies = movies;
            this.hall = hall;
        }

        public void ShowMovies()
        {
            Console.WriteLine("Доступные фильмы:");
            for (int i = 0; i < movies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {movies[i].ToString()}");
            }
        }

        public void Book(int movieIndex, int seatNumber)
        {
            Movie movie = movies[movieIndex];
            Seat seat = hall.GetSeat(seatNumber);
            seat.Reserve();

            Console.WriteLine($"\nБронь оформлена!");
            Console.WriteLine($"Фильм: {movie.Title}");
            Console.WriteLine($"Место: {seat.Number}");
            Console.WriteLine($"Длительность: {movie.Duration} мин.");
        }
    }
}
