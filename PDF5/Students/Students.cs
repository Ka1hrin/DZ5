public class Student
{
    public string LastName { get; set; }    // Фамилия
    public string FirstName { get; set; }   // Имя
    public int BirthYear { get; set; }      // Год рождения
    public string Exam { get; set; }        // Экзамен
    public int Score { get; set; }          // Баллы

  
    public Student(string lastName, string firstName, int birthYear, string exam, int score)
    {
        LastName = lastName;
        FirstName = firstName;
        BirthYear = birthYear;
        Exam = exam;
        Score = score;
    }
}