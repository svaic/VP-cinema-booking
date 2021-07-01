﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoRezervacija
{


    enum HallType
    {
        Large,
        Medium,
        Small
    }

    class Theater
    {
        public BindingList<Hall> Halls { get; set; }
        public string Name { get; set; }

        static List<Movie> movies = new List<Movie>
        {
            new Movie("Marvel Avengers", "Marvel", 350, Genre.Action),
            new Movie("Thor the Dark World", "Marvel", 350, Genre.Action),
            new Movie("Thor Love and Thunder", "Marvel", 200, Genre.Action),
            new Movie("Captain Amerika The Winter Soldier", "Marvel", 200, Genre.Action),
            new Movie("Harry Potter and the Deadly Hollows Part 1", "Marvel", 200, Genre.Fantasy),
            new Movie("Harry Potter and the Deadly Hollows Part 2", "Marvel", 350, Genre.Fantasy)
        };

        public static Comparison<Movie> DefaultComparator = Movie.Comparator.CompareByName;

        public Theater(string Name)
        {
            this.Name = Name;
            BuildHalls(10);
        }

        public void BuildHalls(int hallNumber)
        {
            Halls = new BindingList<Hall>();
            List<Movie> BestMovies = movies.OrderByDescending(i => i.Price).Take(hallNumber).ToList();
            
            HallType hallType = HallType.Small;

            BestMovies.Sort(DefaultComparator);

            for (int i = 0; i < hallNumber; i++)
            {
                if (BestMovies.Count > i) BuildHall(i + 1, BestMovies[i], hallType);
                else BuildHall(i, null, hallType);
            }
        }

        private void BuildHall(int number, Movie moviePlaying, HallType hallType)
        {
            if (hallType == HallType.Small) Halls.Add(new Hall(number, moviePlaying, 20));
            if (hallType == HallType.Medium) Halls.Add(new Hall(number, moviePlaying, 35));
            if (hallType == HallType.Large) Halls.Add(new Hall(number, moviePlaying, 50));
        }
        public void CompareBy(string Type, bool Ascending)
        {
            //Halls = new BindingList<Hall> (Halls.OrderBy(x=>x.CurrentMoviePlaying == null).ThenBy(x => x.CurrentMoviePlaying).ToList());
            if (Ascending)
                Halls = new BindingList<Hall>(Halls.OrderBy(x => x.CurrentMoviePlaying == null).ThenBy(x => x.CurrentMoviePlaying, new MovieComparator(Movie.SortType.GetValueOrDefault(Type, Movie.DefaultComparator))).ToList());
            else
                Halls = new BindingList<Hall>(Halls.OrderBy(x => x.CurrentMoviePlaying == null).ThenByDescending(x => x.CurrentMoviePlaying, new MovieComparator(Movie.SortType.GetValueOrDefault(Type, Movie.DefaultComparator))).ToList());
        }

        public void FilterBy(string Type)
        {

        }
    }
}