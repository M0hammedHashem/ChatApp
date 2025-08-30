using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChatApp.Core.DbContextManager
{
    public interface IChatApp_DbContext : IDisposable
    {
        #region Entities.


        DbSet<SchoolBranch> SchoolBranches { get; set; }
        DbSet<Curriculum> Curriculums { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<CurriculumDepartment> CurriculumDepartments { get; set; }
        DbSet<SchoolClass> SchoolClasses { get; set; }
        DbSet<Section> Sections { get; set; }

        DbSet<Staff> Staffs { get; set; }
        DbSet<StaffLogin> StaffsLogin { get; set; }
        DbSet<StaffJobDetails> StaffJobDetails { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<StudentLogin> StudentsLogin { get; set; }
        DbSet<StudentSchoolDetails> StudentSchoolDetails { get; set; }

        DbSet<Guardian> Guardians { get; set; }
        DbSet<GuardianLogin> GuardiansLogin { get; set; }
        DbSet<Subject> Subjects { get; set; }



        #region ChatRoom 
        DbSet<ChatRoom> ChatRooms { get; set; }
        DbSet<ChatRoomUser> ChatRoomUsers { get; set; }
        DbSet<ChatRoomSettings> ChatRoomSettings { get; set; }
        DbSet<ChatRoomMessage> ChatRoomMessages { get; set; }
        DbSet<ChatRoomMembers> ChatRoomMembers { get; set; }
        DbSet<CurriculumChatRooms> CurriculumChatRooms { get; set; }
        DbSet<ClassChatRooms> ClassChatRooms { get; set; }
        DbSet<SectionChatRooms> SectionChatRooms { get; set; }
        DbSet<SubjectChatRooms> SubjectChatRooms { get; set; }



        #endregion

        #endregion

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync();


    }
}
