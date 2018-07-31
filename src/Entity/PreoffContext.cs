using Microsoft.EntityFrameworkCore;

namespace Preoff.Entity
{
    public partial class PreoffContext : DbContext
    {
        public virtual DbSet<AircTable> AircTable { get; set; }
        public virtual DbSet<AircTypeTable> AircTypeTable { get; set; }
        public virtual DbSet<AircView> AircView { get; set; }
        public virtual DbSet<AirFacTable> AirFacTable { get; set; }
        public virtual DbSet<AirLoadTable> AirLoadTable { get; set; }
        public virtual DbSet<AirLoadView> AirLoadView { get; set; }
        public virtual DbSet<CameraFacTable> CameraFacTable { get; set; }
        public virtual DbSet<CameraTable> CameraTable { get; set; }
        public virtual DbSet<CameraTypeTable> CameraTypeTable { get; set; }
        public virtual DbSet<CameraTypeView> CameraTypeView { get; set; }
        public virtual DbSet<CameraView> CameraView { get; set; }
        public virtual DbSet<DivisionTable> DivisionTable { get; set; }
        public virtual DbSet<EquipFacTable> EquipFacTable { get; set; }
        public virtual DbSet<EventImgTable> EventImgTable { get; set; }
        public virtual DbSet<EventTable> EventTable { get; set; }
        public virtual DbSet<EventTypeTable> EventTypeTable { get; set; }
        public virtual DbSet<EventVideoTable> EventVideoTable { get; set; }
        public virtual DbSet<EventView> EventView { get; set; }
        public virtual DbSet<ExecTaskTable> ExecTaskTable { get; set; }
        public virtual DbSet<ExecTaskView> ExecTaskView { get; set; }
        public virtual DbSet<PermissonTable> PermissonTable { get; set; }
        public virtual DbSet<RolePermissionTable> RolePermissionTable { get; set; }
        public virtual DbSet<RoleTable> RoleTable { get; set; }
        public virtual DbSet<StreamVideoServerTable> StreamVideoServerTable { get; set; }
        public virtual DbSet<TaskStateTable> TaskStateTable { get; set; }
        public virtual DbSet<TaskTable> TaskTable { get; set; }
        public virtual DbSet<TaskTypeTable> TaskTypeTable { get; set; }
        public virtual DbSet<TaskUserTable> TaskUserTable { get; set; }
        public virtual DbSet<TaskView> TaskView { get; set; }
        public virtual DbSet<UnitTable> UnitTable { get; set; }
        public virtual DbSet<UnitView> UnitView { get; set; }
        public virtual DbSet<UserRoleTable> UserRoleTable { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }
        public virtual DbSet<UserView> UserView { get; set; }
        public virtual DbSet<FireStationDataTable> FireStationData { get; set; }
        public virtual DbSet<HotsPotsTable> HotsPotsTable { get; set; }
        public virtual DbSet<PlaceTable> PlaceTable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public PreoffContext(DbContextOptions<PreoffContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AircTable>(entity =>
            {
                entity.ToTable("aircTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AirDesc)
                    .HasColumnName("air_desc")
                    .HasMaxLength(100);

                entity.Property(e => e.AirFacTableId).HasColumnName("airFacTable_id");

                entity.Property(e => e.AirLoadTableId).HasColumnName("airLoadTable_id");

                entity.Property(e => e.AircTypeTableId).HasColumnName("aircTypeTable_id");

                entity.Property(e => e.RegDate)
                    .HasColumnName("reg_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SerialNum)
                    .HasColumnName("serial_num")
                    .HasMaxLength(50);

                entity.Property(e => e.UnitTableId).HasColumnName("unitTable_id");

                entity.Property(e => e.UsedDesc)
                    .HasColumnName("used_desc")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AircTypeTable>(entity =>
            {
                entity.ToTable("aircTypeTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AircTypeName)
                    .HasColumnName("airc_type_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<AircView>(entity =>
            {
                entity.ToTable("aircView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AirDesc)
                    .HasColumnName("air_desc")
                    .HasMaxLength(100);

                entity.Property(e => e.AirFacTableId).HasColumnName("airFacTable_id");

                entity.Property(e => e.AirLoadTableId).HasColumnName("airLoadTable_id");

                entity.Property(e => e.AircTypeName)
                    .HasColumnName("airc_type_name")
                    .HasMaxLength(20);

                entity.Property(e => e.AircTypeTableId).HasColumnName("aircTypeTable_id");

                entity.Property(e => e.EquipName)
                    .HasColumnName("equip_name")
                    .HasMaxLength(20);

                entity.Property(e => e.FacName)
                    .HasColumnName("fac_name")
                    .HasMaxLength(50);

                entity.Property(e => e.RegDate)
                    .HasColumnName("reg_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SerialNum)
                    .HasColumnName("serial_num")
                    .HasMaxLength(50);

                entity.Property(e => e.UnitName)
                    .HasColumnName("unit_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitTableId).HasColumnName("unitTable_id");

                entity.Property(e => e.UsedDesc)
                    .HasColumnName("used_desc")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AirFacTable>(entity =>
            {
                entity.ToTable("airFacTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FacAddr)
                    .HasColumnName("fac_addr")
                    .HasMaxLength(100);

                entity.Property(e => e.FacName)
                    .HasColumnName("fac_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LinkMan)
                    .HasColumnName("link_man")
                    .HasMaxLength(20);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<AirLoadTable>(entity =>
            {
                entity.ToTable("airLoadTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EquipDesc).HasColumnName("equip_desc");

                entity.Property(e => e.EquipFacTableId).HasColumnName("equipFacTable_id");

                entity.Property(e => e.EquipName)
                    .HasColumnName("equip_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<AirLoadView>(entity =>
            {
                entity.ToTable("airLoadView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EquipDesc).HasColumnName("equip_desc");

                entity.Property(e => e.EquipFacName)
                    .HasColumnName("equip_fac_name")
                    .HasMaxLength(50);

                entity.Property(e => e.EquipFacTableId).HasColumnName("equipFacTable_id");

                entity.Property(e => e.EquipName)
                    .HasColumnName("equip_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CameraFacTable>(entity =>
            {
                entity.ToTable("cameraFacTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CameraFacAddr)
                    .HasColumnName("camera_fac_addr")
                    .HasMaxLength(100);

                entity.Property(e => e.CameraFacName)
                    .HasColumnName("camera_fac_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LinkMan)
                    .HasColumnName("link_man")
                    .HasMaxLength(20);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CameraTable>(entity =>
            {
                entity.ToTable("cameraTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CameraAddr)
                    .HasColumnName("camera_addr")
                    .HasMaxLength(50);

                entity.Property(e => e.CameraName)
                    .HasColumnName("camera_name")
                    .HasMaxLength(20);

                entity.Property(e => e.CameraPort)
                    .HasColumnName("camera_port")
                    .HasMaxLength(5);

                entity.Property(e => e.CameraPwd)
                    .HasColumnName("camera_pwd")
                    .HasMaxLength(20);

                entity.Property(e => e.CameraTypeTableId).HasColumnName("cameraTypeTable_id");

                entity.Property(e => e.CameraX).HasColumnName("camera_x");

                entity.Property(e => e.CameraY).HasColumnName("camera_y");

                entity.Property(e => e.CameraZ).HasColumnName("camera_z");

                entity.Property(e => e.IpAddr)
                    .HasColumnName("ip_addr")
                    .HasMaxLength(20);

                entity.Property(e => e.UnitTableId).HasColumnName("unitTable_id");
            });

            modelBuilder.Entity<CameraTypeTable>(entity =>
            {
                entity.ToTable("cameraTypeTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CameraFacTableId).HasColumnName("cameraFacTable_id");

                entity.Property(e => e.CameraTypeName)
                    .HasColumnName("camera_type_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CameraTypeView>(entity =>
            {
                entity.ToTable("cameraTypeView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CameraFacName)
                    .HasColumnName("camera_fac_name")
                    .HasMaxLength(50);

                entity.Property(e => e.CameraFacTableId).HasColumnName("cameraFacTable_id");

                entity.Property(e => e.CameraTypeName)
                    .HasColumnName("camera_type_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CameraView>(entity =>
            {
                entity.ToTable("cameraView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CameraAddr)
                    .HasColumnName("camera_addr")
                    .HasMaxLength(50);

                entity.Property(e => e.CameraName)
                    .HasColumnName("camera_name")
                    .HasMaxLength(20);

                entity.Property(e => e.CameraPort)
                    .HasColumnName("camera_port")
                    .HasMaxLength(5);

                entity.Property(e => e.CameraPwd)
                    .HasColumnName("camera_pwd")
                    .HasMaxLength(20);

                entity.Property(e => e.CameraTypeName)
                    .HasColumnName("camera_type_name")
                    .HasMaxLength(20);

                entity.Property(e => e.CameraTypeTableId).HasColumnName("cameraTypeTable_id");

                entity.Property(e => e.CameraX).HasColumnName("camera_x");

                entity.Property(e => e.CameraY).HasColumnName("camera_y");

                entity.Property(e => e.CameraZ).HasColumnName("camera_z");

                entity.Property(e => e.IpAddr)
                    .HasColumnName("ip_addr")
                    .HasMaxLength(20);

                entity.Property(e => e.UnitName)
                    .HasColumnName("unit_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitTableId).HasColumnName("unitTable_id");
            });

            modelBuilder.Entity<DivisionTable>(entity =>
            {
                entity.ToTable("divisionTable");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(12)
                    .ValueGeneratedNever();

                entity.Property(e => e.DivisionName)
                    .HasColumnName("division_name")
                    .HasMaxLength(100);

                entity.Property(e => e.PId)
                    .HasColumnName("p_id")
                    .HasMaxLength(12);
            });

            modelBuilder.Entity<EquipFacTable>(entity =>
            {
                entity.ToTable("equipFacTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EquipFacAddr)
                    .HasColumnName("equip_fac_addr")
                    .HasMaxLength(100);

                entity.Property(e => e.EquipFacName)
                    .HasColumnName("equip_fac_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LinkMan)
                    .HasColumnName("link_man")
                    .HasMaxLength(20);

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<EventImgTable>(entity =>
            {
                entity.ToTable("eventImgTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EventTableId).HasColumnName("eventTable_id");

                entity.Property(e => e.ImgPath)
                    .HasColumnName("img_path")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<EventTable>(entity =>
            {
                entity.ToTable("eventTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EventDesc)
                    .HasColumnName("event_desc")
                    .HasMaxLength(200);

                entity.Property(e => e.EventTime)
                    .HasColumnName("event_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventTypeTableId).HasColumnName("eventTypeTable_id");

                entity.Property(e => e.ExecTaskTableId).HasColumnName("execTaskTable_id");

                entity.Property(e => e.PosX).HasColumnName("pos_x");

                entity.Property(e => e.PosY).HasColumnName("pos_y");

                entity.Property(e => e.PosZ).HasColumnName("pos_z");
            });

            modelBuilder.Entity<EventTypeTable>(entity =>
            {
                entity.ToTable("eventTypeTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EventDesc)
                    .HasColumnName("event_desc")
                    .HasMaxLength(200);

                entity.Property(e => e.EventName)
                    .HasColumnName("event_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<EventVideoTable>(entity =>
            {
                entity.ToTable("eventVideoTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EventTableId).HasColumnName("eventTable_id");

                entity.Property(e => e.VideoPath)
                    .HasColumnName("video_path")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<EventView>(entity =>
            {
                entity.ToTable("eventView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EventDesc)
                    .HasColumnName("event_desc")
                    .HasMaxLength(200);

                entity.Property(e => e.EventName)
                    .HasColumnName("event_name")
                    .HasMaxLength(20);

                entity.Property(e => e.EventTime)
                    .HasColumnName("event_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.EventTypeTableId).HasColumnName("eventTypeTable_id");

                entity.Property(e => e.ExecTaskTableId).HasColumnName("execTaskTable_id");

                entity.Property(e => e.PosX).HasColumnName("pos_x");

                entity.Property(e => e.PosY).HasColumnName("pos_y");

                entity.Property(e => e.PosZ).HasColumnName("pos_z");
            });

            modelBuilder.Entity<ExecTaskTable>(entity =>
            {
                entity.ToTable("execTaskTable");
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AircTableId).HasColumnName("aircTable_id");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TaskStateTableId).HasColumnName("taskStateTable_id");

                entity.Property(e => e.TaskTableId).HasColumnName("taskTable_id");

                entity.Property(e => e.UserTableId).HasColumnName("userTable_id");
            });

            modelBuilder.Entity<ExecTaskView>(entity =>
            {
                entity.ToTable("execTaskView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AircTableId).HasColumnName("aircTable_id");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TaskName)
                    .HasColumnName("task_name")
                    .HasMaxLength(50);

                entity.Property(e => e.TaskStateTableId).HasColumnName("taskStateTable_id");

                entity.Property(e => e.TaskTableId).HasColumnName("taskTable_id");

                entity.Property(e => e.UserTableId).HasColumnName("userTable_id");

                entity.Property(e => e.ViewName)
                    .HasColumnName("view_name")
                    .HasMaxLength(20);
                entity.Property(e => e.StateName)
                    .HasColumnName("state_name")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<PermissonTable>(entity =>
            {
                entity.ToTable("permissonTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PId).HasColumnName("p_id");
                entity.Property(e => e.Icon).HasColumnName("icon");
                entity.Property(e => e.PermissonDesc)
                    .HasColumnName("permisson_desc")
                    .HasMaxLength(100);

                entity.Property(e => e.PermissonName)
                    .HasColumnName("permisson_name")
                    .HasMaxLength(20);

                entity.Property(e => e.RouterName)
                .HasColumnName("router_name")
                .HasMaxLength(100);

                entity.Property(e => e.PermissonSeq)
                    .HasColumnName("permisson_seq")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<RolePermissionTable>(entity =>
            {
                entity.ToTable("rolePermissionTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PermissonTableId).HasColumnName("permissonTable_id");

                entity.Property(e => e.RoleTableId).HasColumnName("roleTable_id");
            });

            modelBuilder.Entity<RoleTable>(entity =>
            {
                entity.ToTable("roleTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleDesc).HasColumnName("role_desc");

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<StreamVideoServerTable>(entity =>
            {
                entity.ToTable("streamVideoServerTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PlayName)
                    .HasColumnName("play_name")
                    .HasMaxLength(20);

                entity.Property(e => e.PlayPwd)
                    .HasColumnName("play_pwd")
                    .HasMaxLength(20);

                entity.Property(e => e.PushName)
                    .HasColumnName("push_name")
                    .HasMaxLength(20);

                entity.Property(e => e.PushPwd)
                    .HasColumnName("push_pwd")
                    .HasMaxLength(20);

                entity.Property(e => e.ServerAddr)
                    .HasColumnName("server_addr")
                    .HasMaxLength(50);

                entity.Property(e => e.ServerName)
                    .HasColumnName("server_name")
                    .HasMaxLength(20);

                entity.Property(e => e.ServerPort)
                    .HasColumnName("server_port")
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<TaskStateTable>(entity =>
            {
                entity.ToTable("taskStateTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StateName)
                    .HasColumnName("state_name")
                    .HasMaxLength(10);
            });
            
            modelBuilder.Entity<TaskTable>(entity =>
            {
                entity.ToTable("taskTable");
                entity.Property(e => e.Id).HasColumnName("id");
                
                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.PubTime)
                    .HasColumnName("pub_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.TaskDesc).HasColumnName("task_desc");

                entity.Property(e => e.TaskName)
                    .HasColumnName("task_name")
                    .HasMaxLength(50);

                entity.Property(e => e.TaskTypeTableId).HasColumnName("taskTypeTable_id");

                entity.Property(e => e.UserTableId).HasColumnName("userTable_id");
            });
            //modelBuilder.Entity<TaskTable>().HasMany(c => c.ListExec);
            modelBuilder.Entity<TaskTypeTable>(entity =>
            {
                entity.ToTable("taskTypeTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TaskTypeName)
                    .HasColumnName("task_type_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TaskUserTable>(entity =>
            {
                entity.ToTable("taskUserTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TaskTableId).HasColumnName("taskTable_id");

                entity.Property(e => e.UserTableId).HasColumnName("userTable_id");
            });

            modelBuilder.Entity<TaskView>(entity =>
            {
                entity.ToTable("taskView");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.PubTime)
                    .HasColumnName("pub_time")
                    .HasColumnType("datetime");


                entity.Property(e => e.TaskDesc).HasColumnName("task_desc");

                entity.Property(e => e.TaskName)
                    .HasColumnName("task_name")
                    .HasMaxLength(50);

                entity.Property(e => e.TaskTypeName)
                    .HasColumnName("task_type_name")
                    .HasMaxLength(20);

                entity.Property(e => e.TaskTypeTableId).HasColumnName("taskTypeTable_id");

                entity.Property(e => e.UserTableId).HasColumnName("userTable_id");

                entity.Property(e => e.ViewName)
                    .HasColumnName("view_name")
                    .HasMaxLength(20);
                entity.Property(e => e.TaskStateTableId)
                    .HasColumnName("taskStateTable_id");
                entity.Property(e => e.StateName)
                    .HasColumnName("state_name")
                    .HasMaxLength(10);
                entity.Property(e => e.ExecTaskTableId).HasColumnName("execTaskTable_Id");
            });

            modelBuilder.Entity<UnitTable>(entity =>
            {
                entity.ToTable("unitTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DivisionTableId)
                    .HasColumnName("divisionTable_id")
                    .HasMaxLength(12);

                entity.Property(e => e.StreamVideoServerTableId).HasColumnName("streamVideoServerTable_id");

                entity.Property(e => e.UnitAddr)
                    .HasColumnName("unit_addr")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitDesc).HasColumnName("unit_desc");

                entity.Property(e => e.UnitName)
                    .HasColumnName("unit_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitPhone)
                    .HasColumnName("unit_phone")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<UnitView>(entity =>
            {
                entity.ToTable("unitView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DivisionTableId)
                    .HasColumnName("divisionTable_id")
                    .HasMaxLength(12);

                entity.Property(e => e.ServerName)
                    .HasColumnName("server_name")
                    .HasMaxLength(20);

                entity.Property(e => e.StreamVideoServerTableId).HasColumnName("streamVideoServerTable_id");

                entity.Property(e => e.UnitAddr)
                    .HasColumnName("unit_addr")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitDesc).HasColumnName("unit_desc");

                entity.Property(e => e.UnitName)
                    .HasColumnName("unit_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitPhone)
                    .HasColumnName("unit_phone")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<UserRoleTable>(entity =>
            {
                entity.ToTable("userRoleTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleTableId).HasColumnName("roleTable_id");

                entity.Property(e => e.UserTableId).HasColumnName("userTable_id");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.ToTable("userTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Birthday).HasColumnName("birthday")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(20);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(4);

                entity.Property(e => e.LastLoginTime)
                    .HasColumnName("last_login_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LoginCount).HasColumnName("login_count");

                entity.Property(e => e.LoginName)
                    .HasColumnName("login_name")
                    .HasMaxLength(20);

                entity.Property(e => e.LoginPwd)
                    .HasColumnName("login_pwd")
                    .HasMaxLength(32);

                entity.Property(e => e.RealName)
                    .HasColumnName("real_name")
                    .HasMaxLength(20);

                entity.Property(e => e.RegTime)
                    .HasColumnName("reg_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(20);

                entity.Property(e => e.UnitTableId).HasColumnName("unitTable_id");

                entity.Property(e => e.ViewName)
                    .HasColumnName("view_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<UserView>(entity =>
            {
                entity.ToTable("userView");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Birthday).HasColumnName("birthday")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(20);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(4);

                entity.Property(e => e.LastLoginTime)
                    .HasColumnName("last_login_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LoginCount).HasColumnName("login_count");

                entity.Property(e => e.LoginName)
                    .HasColumnName("login_name")
                    .HasMaxLength(20);

                entity.Property(e => e.LoginPwd)
                    .HasColumnName("login_pwd")
                    .HasMaxLength(32);

                entity.Property(e => e.RealName)
                    .HasColumnName("real_name")
                    .HasMaxLength(20);

                entity.Property(e => e.RegTime)
                    .HasColumnName("reg_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasMaxLength(20);

                entity.Property(e => e.UnitName)
                    .HasColumnName("unit_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UnitTableId).HasColumnName("unitTable_id");

                entity.Property(e => e.ViewName)
                    .HasColumnName("view_name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<FireStationDataTable>(entity =>
            {
                entity.ToTable("fireStationData");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CatDate)
                    .HasColumnName("cat_date")
                    .HasMaxLength(8);

                entity.Property(e => e.CatHour)
                    .HasColumnName("cat_hour")
                    .HasMaxLength(2);

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(12);

                entity.Property(e => e.DateType)
                    .HasColumnName("date_type")
                    .HasMaxLength(5);

                entity.Property(e => e.Firelevel).HasColumnName("firelevel");

                entity.Property(e => e.Humidity).HasColumnName("humidity");

                entity.Property(e => e.Rain).HasColumnName("rain");

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.Property(e => e.WindSpeed).HasColumnName("wind_speed");

                entity.Property(e => e.Winddirect).HasColumnName("winddirect");
            });

            modelBuilder.Entity<PlaceTable>(entity =>
            {
                entity.ToTable("placeTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PlaceName)
                    .HasColumnName("place_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasMaxLength(50);

                entity.Property(e => e.PlaceType)
                    .HasColumnName("place_type")
                    .HasMaxLength(50);

                entity.Property(e => e.MaxX).HasColumnName("max_x");

                entity.Property(e => e.MaxY).HasColumnName("max_y");

                entity.Property(e => e.MinX).HasColumnName("min_x");

                entity.Property(e => e.MinY).HasColumnName("min_y");
                entity.Property(e => e.LayerID).HasColumnName("layer_id").HasMaxLength(50);
            });

            modelBuilder.Entity<HotsPotsTable>(entity =>
            {
                entity.ToTable("hotsPotsTable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AttackTime)
                    .HasColumnName("attack_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.BriefIntroduction)
                    .HasColumnName("brief_introduction")
                    .HasMaxLength(255);

                entity.Property(e => e.CauseOfFire)
                    .HasColumnName("cause_of_fire")
                    .HasMaxLength(255);

                entity.Property(e => e.Continuously)
                    .HasColumnName("continuously")
                    .HasMaxLength(50);

                entity.Property(e => e.CountyName)
                    .HasColumnName("county_name")
                    .HasMaxLength(50);

                entity.Property(e => e.FeedbackSituation)
                    .HasColumnName("feedback_situation")
                    .HasMaxLength(255);

                entity.Property(e => e.FireArea).HasColumnName("fire_area");

                entity.Property(e => e.FireForestryArea).HasColumnName("fire_forestry_area");

                entity.Property(e => e.HotsPotsCode)
                    .HasColumnName("hots_pots_code")
                    .HasMaxLength(10);

                entity.Property(e => e.IgnitingTime)
                    .HasColumnName("igniting_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LandType)
                    .HasColumnName("land_type")
                    .HasMaxLength(10);

                entity.Property(e => e.MonitorUnit)
                    .HasColumnName("monitor_unit")
                    .HasMaxLength(50);

                entity.Property(e => e.MonitorUser)
                    .HasColumnName("monitor_user")
                    .HasMaxLength(20);

                entity.Property(e => e.OtherLosses)
                    .HasColumnName("other_losses")
                    .HasMaxLength(50);

                entity.Property(e => e.PicPath)
                    .HasColumnName("pic_path")
                    .HasMaxLength(255);

                entity.Property(e => e.PixelsNumber).HasColumnName("pixels_number");

                entity.Property(e => e.ReceiveTime)
                    .HasColumnName("receive_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ReportFile)
                    .HasColumnName("report_file")
                    .HasMaxLength(50);

                entity.Property(e => e.ReportUnit)
                    .HasColumnName("report_unit")
                    .HasMaxLength(10);

                entity.Property(e => e.ReportUser)
                    .HasColumnName("report_user")
                    .HasMaxLength(50);

                entity.Property(e => e.SatelLite)
                    .HasColumnName("satel_lite")
                    .HasMaxLength(50);

                entity.Property(e => e.Smoke)
                    .HasColumnName("smoke")
                    .HasMaxLength(10);

                entity.Property(e => e.SystemCode)
                    .HasColumnName("system_code")
                    .HasMaxLength(50);

                entity.Property(e => e.VictimForestryArea).HasColumnName("victim_forestry_area");

                entity.Property(e => e.XPos).HasColumnName("x_pos");

                entity.Property(e => e.YPos).HasColumnName("y_pos");
            });
        }
    }
}
