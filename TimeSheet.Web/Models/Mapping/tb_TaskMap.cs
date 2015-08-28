using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace TimeSheet.Web.Models.Mapping
{
    public class tb_TaskMap : EntityTypeConfiguration<tb_Task>
    {
        public tb_TaskMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ClientName)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.Description)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(500);

            this.Property(t => t.Hours)
                .IsRequired()
                .HasMaxLength(5);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tb_Task");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User_TaskId).HasColumnName("User_TaskId");
            this.Property(t => t.ClientName).HasColumnName("ClientName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Hours).HasColumnName("Hours");
            this.Property(t => t.Type).HasColumnName("Type");

            // Relationships
            this.HasRequired(t => t.tb_User_Task)
                .WithMany(t => t.tb_Task)
                .HasForeignKey(d => d.User_TaskId);

        }
    }
}
