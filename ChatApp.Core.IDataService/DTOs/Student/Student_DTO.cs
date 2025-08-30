namespace ChatApp.Core.IDataService
{
    public class Student_DTO
    {
        public string StudentId { get; set; }

        public string StudentArabicName { get; set; }

        public string StudentEnglishName { get; set; }


        public int GuardianId { get; set; }
        public Guardian_DTO Guardian { get; set; }


    }
}
