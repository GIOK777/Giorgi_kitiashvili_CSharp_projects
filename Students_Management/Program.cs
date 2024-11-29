namespace Students_Management
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentManagement studentManagement = new StudentManagement();
            bool exit = false;

            while (!exit)
            {
                studentManagement.DisplayMenu();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        studentManagement.AddNewStudent();
                        break;
                    case "2":
                        studentManagement.DisplayAllStudents();
                        break;
                    case "3":
                        studentManagement.FindStudentByRollNumber();
                        break;
                    case "4":
                        studentManagement.UpdateStudentGrade();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Exit");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }
    }

    class Student
    {
        public string Name { get; set; }
        public int RollNumber { get; set; }
        public char Grade { get; set; }

        public Student(string name, int rollNumber, char grade)
        {
            Name = name;
            RollNumber = rollNumber;
            Grade = grade;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Roll Number: {RollNumber}, Name: {Name}, Grade: {Grade}");
        }
        // სტუდენტის მონაცემები გადავიყვანოთ სტრინგ ფორმატში (ფაილში ჩასაწერად)
        public string ToFileString() { return $"{RollNumber},{Name},{Grade}"; }

        // გადავიყვანოთ თითოეული სტუდენტის ჩანაწერი (ხაზი),- სტუდენტის ობიექტში
        public static Student FromFileString(string fileString)
        {
            var parts = fileString.Split(',');
            return new Student(parts[1], int.Parse(parts[0]), char.Parse(parts[2])); // იქმნება სტუდენტის ობიექტი
        }
    }

    class StudentManagement
    {
        private const string filePath = "C:\\Users\\GK\\Documents\\Students.txt";
        // სტუდენტების დამატების მეთოდი
        public void AddNewStudent()
        {
            Console.Write("Enter student name: ");
            string name = Console.ReadLine();

            Console.Write("Enter student roll number: ");
            int rollNumber = int.Parse(Console.ReadLine());

            char grade;
            while (true)
            {
                Console.Write("Enter student grade (A-F): ");
                grade = char.Parse(Console.ReadLine().ToUpper());
                if (grade >= 'A' && grade <= 'F')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid grade. Please enter a grade between A and F.");
                }
            }

            Student student = new Student(name, rollNumber, grade);

            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))

            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(student.ToFileString());
            }
            Console.WriteLine("Student added successfully!\n");
        }


        // სტუდენტების სიის გამოტანის მეთოდი
        public void DisplayAllStudents()
        {
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line;
                    Console.WriteLine("\nAll Students:");
                    while ((line = sr.ReadLine()) != null)
                    {
                        Student student = Student.FromFileString(line);
                        student.DisplayInfo();
                    }
                }
            }
            else
            {
                Console.WriteLine("No students available.\n");
            }
        }


        // სტუდენტის ძებნის მეთოდი
        public void FindStudentByRollNumber()
        {
            Console.Write("Enter the roll number of the student you want to find: ");
            int rollNumber = int.Parse(Console.ReadLine());

            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line;
                    bool found = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith(rollNumber.ToString()))
                        {
                            Student student = Student.FromFileString(line);
                            student.DisplayInfo();
                            found = true;
                            break;
                        }
                    }
                    if (!found) { Console.WriteLine("Student with the given roll number not found.\n"); }
                }
            }
            else { Console.WriteLine("No students found in the file.\n"); }
        }



        // სტუდენტის შეფასების განახლების მეთოდი
        public void UpdateStudentGrade()
        {
            Console.Write("Enter the roll number of the student you want to update: ");
            int rollNumber = int.Parse(Console.ReadLine());

            List<string> fileLines = new List<string>();
            bool found = false;

            // კითხულობს ფაილში არსებულ ყველა მონაცემს (ხაზს) რომ შეადაროს შემოყვანილი სიის ნომერი
            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // თუ ხაზი შეიცავს შემოყვანილ სიის ნომერს
                        if (line.StartsWith(rollNumber.ToString()))
                        {
                            Console.Write("Enter new student grade: ");
                            char newGrade = char.Parse(Console.ReadLine());

                            var parts = line.Split(',');

                            // ვტოვებთ სიის ნომერს და სახელს და მხოლოდ შეფასებას ვანახლებთ
                            Student updatedStudent = new Student(parts[1], int.Parse(parts[0]), newGrade);

                            // ვამატებთ ლისტში განახლებულ სტუდენტის ინფორმაციას (ხაზს)
                            fileLines.Add(updatedStudent.ToFileString());
                            found = true;
                        }
                        else { fileLines.Add(line); } // თუ არ მოხდა განახლება დარჩება ორიგინალი მონაცემი
                    }
                }

                // განახლებული მონაცემების სტუდენტი ჩავწეროთ უკან ფაილში
                if (found)
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (var line in fileLines)
                        {
                            sw.WriteLine(line);
                        }
                    }
                    Console.WriteLine("Student grade updated successfully!\n");
                }
                else { Console.WriteLine("Student not found.\n"); }
            }
            else { Console.WriteLine("No students found in the file.\n"); }
        }

        public void DisplayMenu()
        {
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Display All Students");
            Console.WriteLine("3. Find Student By Roll Number");
            Console.WriteLine("4. Update Student Grade");
            Console.WriteLine("5. Exit");
            Console.Write("Choose a number: ");
        }

    }
}

