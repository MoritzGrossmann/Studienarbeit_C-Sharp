using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stundenplan.Domain.Notebook
{
    public class Exam
    {
        public Lesson Lesson { get; private set; }

        public List<String> Subjects { get; private set; }

        public void AddSubject(String subject)
        {
            this.Subjects.Add(subject);
        }

        public void RemoveSubject(String subject)
        {
            this.Subjects.Remove(subject);
        }
    }
}
