using System.Collections.Generic;
using System.Data.Entity.Migrations;
using MyUniversityApp.Models;

namespace MyUniversityApp.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<UniversityDBcontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UniversityDBcontext context)
        {
          

           // var semister = new List<Semister>
           //{
           //    new Semister{SemisterName = "1st"},
           //    new Semister{SemisterName = "2nd"},
           //    new Semister{SemisterName = "3rd"},
           //    new Semister{SemisterName = "4th"},
           //    new Semister{SemisterName = "5th"},
           //    new Semister{SemisterName = "6th"},
           //    new Semister{SemisterName = "7th"},
           //    new Semister{SemisterName = "8th"},
           //    new Semister{SemisterName = "9th"},
           //    new Semister{SemisterName = "10th"},
           //    new Semister{SemisterName = "11th"},
           //    new Semister{SemisterName = "12th"}
               

           //};

           // semister.ForEach(s=> context.Semisters.AddOrUpdate(s));
           // context.SaveChanges();


           // var desig = new List<Designation>
           // {
           //     new Designation {DesignationName = "Chef Instractor"},
           //     new Designation {DesignationName = "Instractor"},
           //     new Designation {DesignationName = "Junior Instractor"},
           //     new Designation {DesignationName = "Lectarur"},
           //     new Designation {DesignationName = "Guest Lectarur"}
           // };

           // desig.ForEach(d => context.Designations.AddOrUpdate(d));
           // context.SaveChanges();

            //var room = new List<Room>
            //{
            //    new Room{RoomCode = "Room-101"},
            //    new Room{RoomCode = "Room-102"},
            //    new Room{RoomCode = "Room-103"},
            //    new Room{RoomCode = "Room-104"},
            //    new Room{RoomCode = "Room-105"},
            //    new Room{RoomCode = "Room-201"},
            //    new Room{RoomCode = "Room-202"},
            //    new Room{RoomCode = "Room-203"},
            //    new Room{RoomCode = "Room-204"},
            //    new Room{RoomCode = "Room-205"},
            //    new Room{RoomCode = "Room-301"},
            //    new Room{RoomCode = "Room-302"},
            //    new Room{RoomCode = "Room-303"},
            //    new Room{RoomCode = "Room-304"},
            //    new Room{RoomCode = "Room-305"}
            //};
            //room.ForEach(r=> context.Rooms.AddOrUpdate(r));
            //context.SaveChanges();

            //var day = new List<Day>
            //{
            //    new Day{DayName = "Sunday"},
            //    new Day{DayName = "Monday"},
            //    new Day{DayName = "Tuesday"},
            //    new Day{DayName = "Wednesday"},
            //    new Day{DayName = "Thursday"},
            //    new Day{DayName = "Friday"},
            //    new Day{DayName = "Saturday"}
            //};
            //day.ForEach(d=> context.Days.AddOrUpdate(d));
            //context.SaveChanges();
        }

        
    }
}
