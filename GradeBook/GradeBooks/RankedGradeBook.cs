using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade){
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var orderedList = Students.OrderByDescending(x => x.AverageGrade).Select(x => x.AverageGrade).ToList();
            int position = 0;

            while (averageGrade < orderedList[position])
            {
                position++;
            }

            if (position < (int)(orderedList.Count * 0.2)) return 'A';
            if (position < (int)(orderedList.Count * 0.4)) return 'B';
            if (position < (int)(orderedList.Count * 0.6)) return 'C';
            if (position < (int)(orderedList.Count * 0.8)) return 'D';

            return 'F';
        }

        public override void CalculateStatistics(){
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name){
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}