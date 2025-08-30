
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Core.DbContextManager
{
    public sealed class ChatApp_DbContext : IdentityDbContext, IChatApp_DbContext
    {

        public ChatApp_DbContext(DbContextOptions<ChatApp_DbContext> options)
            : base(options) // Pass the options to the base IdentityDbContext constructor
        {
        }


        #region Entities.

        public DbSet<SchoolBranch> SchoolBranches { get; set; }
        public DbSet<Curriculum> Curriculums { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CurriculumDepartment> CurriculumDepartments { get; set; }
        public DbSet<SchoolClass> SchoolClasses { get; set; }
        public DbSet<Section> Sections { get; set; }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffLogin> StaffsLogin { get; set; }
        public DbSet<StaffJobDetails> StaffJobDetails { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentLogin> StudentsLogin { get; set; }
        public DbSet<Guardian> Guardians { get; set; }
        public DbSet<GuardianLogin> GuardiansLogin { get; set; }
        public DbSet<StudentSchoolDetails> StudentSchoolDetails { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        #region Chat Room
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomMessage> ChatRoomMessages { get; set; }
        public DbSet<ChatRoomUser> ChatRoomUsers { get; set; }
        public DbSet<ChatRoomSettings> ChatRoomSettings { get; set; }
        public DbSet<ChatRoomMembers> ChatRoomMembers { get; set; }
        public DbSet<CurriculumChatRooms> CurriculumChatRooms { get; set; }
        public DbSet<ClassChatRooms> ClassChatRooms { get; set; }
        public DbSet<SectionChatRooms> SectionChatRooms { get; set; }
        public DbSet<SubjectChatRooms> SubjectChatRooms { get; set; }

        #endregion

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ChatRoomMembers>()
        .HasKey(crm => new { crm.ChatRoomId, crm.ChatRoomUserId });

            modelBuilder.Entity<CurriculumChatRooms>()
        .HasKey(crm => new { crm.ChatRoomId, crm.CurriculumId });


            modelBuilder.Entity<ClassChatRooms>()
        .HasKey(crm => new { crm.ChatRoomId, crm.SchoolClassId });


            modelBuilder.Entity<SectionChatRooms>()
        .HasKey(crm => new { crm.ChatRoomId, crm.SectionId });
            modelBuilder.Entity<SubjectChatRooms>()
        .HasKey(crm => new { crm.ChatRoomId, crm.SubjectId });

            modelBuilder.Entity<ChatRoom>()
    .HasMany(cr => cr.Messages)        // A ChatRoom has many Messages
    .WithOne(m => m.ChatRoom)          // A Message has one ChatRoom
    .HasForeignKey(m => m.ChatRoomId)  // The foreign key is ChatRoomId on the Message entity
    .IsRequired();

            // Configure the One-to-One Relationship (ChatRoom has one LastMessage)
            modelBuilder.Entity<ChatRoom>()
                .HasOne(cr => cr.LastMessage)              // A ChatRoom has one LastMessage
                .WithOne(m => m.LastMessageChatRoom)       // A ChatRoomMessage can be a LastMessage for one ChatRoom
                .HasForeignKey<ChatRoom>(cr => cr.LastMessageId) // The foreign key is LastMessageId on the ChatRoom entity
                .IsRequired(false);

        }
    }

}
