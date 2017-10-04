namespace Stundenplan.Domain
{
    public class Lesson : TimeUnit {

        public Teacher Teacher {get; private set;}

        public Room Room {get; private set;}

        public Discipline Discipline {get; private set;}

    }
}