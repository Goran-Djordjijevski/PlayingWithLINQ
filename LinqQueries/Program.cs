﻿using System;
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

            // Element - ElementAt and ElementAtOrDefault
            IList<int> intListElementAt = new List<int>() { 10, 21, 30, 45, 50, 87 };
            IList<string> strListElementAt = new List<string>() { "One", "Two", null, "Four", "Five" };

            int res1 = intListElementAt.ElementAt(0);
            string res2 = strListElementAt.ElementAt(0);

            int res3 = intListElementAt.ElementAt(1);
            string res4 = strListElementAt.ElementAt(1);

            int res5 = intListElementAt.ElementAtOrDefault(2);
            string res6 = strListElementAt.ElementAtOrDefault(2);

            int res7 = intListElementAt.ElementAtOrDefault(9);
            string res8 = strListElementAt.ElementAtOrDefault(9);

            // string res9 = strListElementAt.ElementAt(9); We have exception

            // Element - First and FirstOrDefault
            IList<int> intListFirst = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
            IList<string> strListFirst = new List<string>() { null, "Two", "Three", "Four", "Five" };
            IList<string> emptyListFirst = new List<string>();

            int res10 = intListFirst.First();
            int res11 = intListFirst.First(i => i % 2 == 0);

            string res12 = strListFirst.First();

            int res13 = intListFirst.FirstOrDefault();
            int res14 = intListFirst.FirstOrDefault(i => i % 2 == 0);

            string res15 = emptyListFirst.FirstOrDefault();

            //string res16 = strListFirst.FirstOrDefault(x => x.Contains("T")); We have exception

            // Element - Single or SingleOrDefault
            IList<int> oneElementListSingle = new List<int>() { 7 };
            IList<int> intListSingle = new List<int>() { 7, 10, 21, 30, 45, 50, 87 };
            IList<string> strListSingle = new List<string>() { null, "Two", "Three", "Four", "Five" };
            IList<string> emptyListSingle = new List<string>();

            int res17 = oneElementListSingle.Single();
            int res18 = oneElementListSingle.SingleOrDefault();

            string res19 = emptyListSingle.SingleOrDefault();

            // Equality - SequenceEqual
            IList<string> strList1 = new List<string>() { "One", "Two", "Three", "Four", "Three" };
            IList<string> strList2 = new List<string>() { "One", "Two", "Three", "Four", "Three" };

            bool isEqual = strList1.SequenceEqual(strList2);

            // Concatenation - Concat
            IList<string> collection1 = new List<string>() { "One", "Two", "Three" };
            IList<string> collection2 = new List<string>() { "Five", "Six" };

            var collection3 = collection1.Concat(collection2);

            // Generation - DefaultIfEmpty
            IList<string> emptyList = new List<string>();
            var newList = emptyList.DefaultIfEmpty();
            var newList2 = emptyList.DefaultIfEmpty("None");

            // Generation - Distinct
            IList<string> strListDistinct = new List<string>() { "One", "Two", "Three", "Two", "Three" };
            IList<int> intListDistinct = new List<int>() { 1, 2, 3, 2, 4, 4, 3, 5 };

            var distinct1 = strListDistinct.Distinct();
            var distinct2 = intListDistinct.Distinct();

            // Generation - Except
            List<string> strListExcept1 = new List<string>() { "One", "Two", "Three", "Four", "Five" };
            IList<string> strListExcept2 = new List<string>() { "Four", "Five", "Six", "Seven", "Eight" };

            var except = strListExcept1.Except(strListExcept2);

            Console.ReadLine();
        }
    }
}
