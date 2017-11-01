using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqQueries
{
    class Program
    {
        static void Main(string[] args)
        {
            // Filtering - Where operator
            IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
            };

            var filteredResult = studentList.Where(s => s.Age > 12 && s.Age < 20);

            // Filtering - OfType operator
            IList mixedList = new ArrayList();
            mixedList.Add(0);
            mixedList.Add("One");
            mixedList.Add("Two");
            mixedList.Add(3);
            mixedList.Add(new Student() { StudentID = 1, StudentName = "Bill" });

            var stringResult = mixedList.OfType<string>();

            // Sorting - OrderBy/OrderByDescending operator
            IList<Student> studentListOrderBy = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 },
                new Student() { StudentID = 6, StudentName = "Ram" , Age = 18 }
            };

            var studentsInAscOrder = studentListOrderBy.OrderBy(s => s.StudentName);
            var studentsInDescOrder = studentListOrderBy.OrderByDescending(s => s.StudentName);
            var studentsOrderThenBy = studentListOrderBy.OrderBy(s => s.StudentName).ThenBy(s => s.Age);

            // Grouping - GroupBy and ToLookup
            IList<Student> studentListGroupBy = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Abram" , Age = 21 }
            };

            var groupedResult = studentListGroupBy.GroupBy(s => s.Age);

            foreach (var ageGroup in groupedResult)
            {
                Console.WriteLine($"Age Group: {ageGroup.Key}");

                foreach (var student in ageGroup)
                {
                    Console.WriteLine($"Student Name: {student.StudentName}");
                }
            }

            // Joining - Join and GroupJoin
            IList<Student> studentListJoin = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", StandardId = 1 },
                new Student() { StudentID = 2, StudentName = "Moin", StandardId = 1 },
                new Student() { StudentID = 3, StudentName = "Bill", StandardId = 2 },
                new Student() { StudentID = 4, StudentName = "Ram" , StandardId = 2 },
                new Student() { StudentID = 5, StudentName = "Ron" , StandardId = 3 }
            };

            IList<Standard> standardList = new List<Standard>() {
                new Standard(){ StandardId = 1, StandardName="Standard 1"},
                new Standard(){ StandardId = 2, StandardName="Standard 2"},
                new Standard(){ StandardId = 3, StandardName="Standard 3"}
            };

            var innerJoin = studentListJoin.Join(standardList,
                                                 student => student.StandardId,
                                                 standard => standard.StandardId,
                                                 (student, standard) => new
                                                 {
                                                     StudentName = student.StudentName,
                                                     StandardName = standard.StandardName
                                                 });

            var groupJoin = standardList.GroupJoin(studentListJoin,
                                                   student => student.StandardId,
                                                   standard => standard.StandardId,
                                                   (standard, studentsGroup) => new
                                                   {
                                                       Students = studentsGroup,
                                                       StandardFullName = standard.StandardName
                                                   });
            
            foreach (var item in groupJoin)
            {
                Console.WriteLine(item.StandardFullName);

                foreach (var stud in item.Students)
                    Console.WriteLine(stud.StudentName);
            }

            // Projection - Select and SelectMany
            IList<Student> studentListProjection = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 } ,
                new Student() { StudentID = 2, StudentName = "Moin",  Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 15 }
            };

            var selectResult = studentListProjection.Select(s => new
            {
                Name = s.StudentName,
                Age = s.Age
            });

            // Quantifier - All, Any and Contain
            IList<Student> studentListAll = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
            };

            bool areAllStudentsTeenager = studentListAll.All(s => s.Age > 12 && s.Age < 20);
            bool isAnyStudentTeenager = studentListAll.Any(s => s.Age > 12 && s.Age < 20);

            IList<int> intList = new List<int>() { 1, 2, 3, 4, 5 };
            bool result = intList.Contains(15);

            IList<Student> studentListContains = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 }
             };

            // Aggregation - Aggregate
            IList<String> strList = new List<String>() { "One", "Two", "Three", "Four", "Five" };
            var commaSeperatedString = strList.Aggregate((s1, s2) => s1 + ", " + s2);

            // Aggregation - Average
            IList<int> intListAverage = new List<int>() { 10, 20, 30 };
            double average = intListAverage.Average();

            IList<Student> studentListAverage = new List<Student> () {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 } ,
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron", Age = 15 }
            };

            double averageAge = studentList.Average(x => x.Age);

            // Aggregation - Count
            IList<Student> studentListCount = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 13 } ,
                new Student() { StudentID = 2, StudentName = "Moin", Age = 21 } ,
                new Student() { StudentID = 3, StudentName = "Bill", Age = 18 } ,
                new Student() { StudentID = 4, StudentName = "Ram", Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Mathew", Age = 15 }
            };

            int numOfStudents = studentListCount.Count();
            int ageOfStudents = studentListCount.Count(x => x.Age >= 18);

            Console.ReadLine();
        }
    }
}
