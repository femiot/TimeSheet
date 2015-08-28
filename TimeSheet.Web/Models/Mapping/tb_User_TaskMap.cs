using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TimeSheet.Web.Models.Mapping
{
    public class tb_User_TaskMap : EntityTypeConfiguration<tb_User_Task>
    {
        public tb_User_TaskMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("tb_User_Task");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.TaskDate).HasColumnName("TaskDate");

            // Relationships
            this.HasRequired(t => t.AspNetUser)
                .WithMany(t => t.tb_User_Task)
                .HasForeignKey(d => d.UserId);

        }
    }
}
