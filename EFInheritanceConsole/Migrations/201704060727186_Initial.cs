namespace EFInheritanceConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Canales",
                    c => new
                    {
                        CanalId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CanalId)
                .ForeignKey("dbo.Empresas", t => t.EmpresaId, cascadeDelete: true)
                .Index(t => t.EmpresaId);

            CreateTable(
                    "dbo.Empresas",
                    c => new
                    {
                        EmpresaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.EmpresaId);

            CreateTable(
                    "dbo.Turnos",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Abreviatura = c.String(nullable: false, maxLength: 3),
                        Activado = c.Boolean(nullable: false),
                        CreadoPor = c.String(),
                        FechaCreacion = c.DateTime(),
                        ModificadoPor = c.String(),
                        FechaModificacion = c.DateTime(),
                        EmpresaId = c.Int(),
                        CanalId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresas", t => t.EmpresaId)
                .ForeignKey("dbo.Canales", t => t.CanalId, cascadeDelete: true)
                .Index(t => t.EmpresaId)
                .Index(t => t.CanalId);

            CreateTable(
                    "dbo.Usuarios",
                    c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        EmpresaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Empresas", t => t.EmpresaId, cascadeDelete: true)
                .Index(t => t.EmpresaId);

            CreateTable(
                    "dbo.PresupuestosUsuariosAusencias",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        FechaDesde = c.DateTime(nullable: false),
                        FechaHasta = c.DateTime(),
                        Observaciones = c.String(),
                        CreadoPor = c.String(),
                        FechaCreacion = c.DateTime(),
                        ModificadoPor = c.String(),
                        FechaModificacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);

            CreateTable(
                    "dbo.PresupuestosUsuariosTiendas",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        TiendaId = c.Int(nullable: false),
                        FechaDesde = c.DateTime(nullable: false),
                        FechaHasta = c.DateTime(),
                        CreadoPor = c.String(),
                        FechaCreacion = c.DateTime(),
                        ModificadoPor = c.String(),
                        FechaModificacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tiendas", t => t.TiendaId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId)
                .Index(t => t.TiendaId);

            CreateTable(
                    "dbo.PresupuestosUsuariosTiendasNiveles",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PresupuestoUsuarioTiendaId = c.Long(nullable: false),
                        TipoUsuarioTienda = c.Int(nullable: false),
                        Nombre = c.String(),
                        Nivel = c.Int(nullable: false),
                        FechaDesde = c.DateTime(nullable: false),
                        Principal = c.Boolean(nullable: false),
                        CreadoPor = c.String(),
                        FechaCreacion = c.DateTime(),
                        ModificadoPor = c.String(),
                        FechaModificacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PresupuestosUsuariosTiendas", t => t.PresupuestoUsuarioTiendaId, cascadeDelete: true)
                .Index(t => t.PresupuestoUsuarioTiendaId);

            CreateTable(
                    "dbo.Tiendas",
                    c => new
                    {
                        TiendaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        CanalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TiendaId)
                .ForeignKey("dbo.Canales", t => t.CanalId, cascadeDelete: true)
                .Index(t => t.CanalId);

            CreateTable(
                    "dbo.PresupuestosUsuariosTiendasTurnosHoras",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PresupuestoUsuarioTiendaId = c.Long(nullable: false),
                        Abrevitura = c.String(),
                        Nombre = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        Horas = c.Int(nullable: false),
                        Minutos = c.Int(),
                        CreadoPor = c.String(),
                        FechaCreacion = c.DateTime(),
                        ModificadoPor = c.String(),
                        FechaModificacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PresupuestosUsuariosTiendas", t => t.PresupuestoUsuarioTiendaId, cascadeDelete: true)
                .Index(t => t.PresupuestoUsuarioTiendaId);

            CreateTable(
                    "dbo.PresupuestosUsuariosVacaciones",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        FechaDesde = c.DateTime(nullable: false),
                        FechaHasta = c.DateTime(nullable: false),
                        Observaciones = c.String(),
                        CreadoPor = c.String(),
                        FechaCreacion = c.DateTime(),
                        ModificadoPor = c.String(),
                        FechaModificacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Turnos", "CanalId", "dbo.Canales");
            DropForeignKey("dbo.Tiendas", "CanalId", "dbo.Canales");
            DropForeignKey("dbo.Usuarios", "EmpresaId", "dbo.Empresas");
            DropForeignKey("dbo.PresupuestosUsuariosVacaciones", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.PresupuestosUsuariosTiendas", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.PresupuestosUsuariosTiendasTurnosHoras", "PresupuestoUsuarioTiendaId",
                "dbo.PresupuestosUsuariosTiendas");
            DropForeignKey("dbo.PresupuestosUsuariosTiendas", "TiendaId", "dbo.Tiendas");
            DropForeignKey("dbo.PresupuestosUsuariosTiendasNiveles", "PresupuestoUsuarioTiendaId",
                "dbo.PresupuestosUsuariosTiendas");
            DropForeignKey("dbo.PresupuestosUsuariosAusencias", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Turnos", "EmpresaId", "dbo.Empresas");
            DropForeignKey("dbo.Canales", "EmpresaId", "dbo.Empresas");
            DropIndex("dbo.PresupuestosUsuariosVacaciones", new[] {"UsuarioId"});
            DropIndex("dbo.PresupuestosUsuariosTiendasTurnosHoras", new[] {"PresupuestoUsuarioTiendaId"});
            DropIndex("dbo.Tiendas", new[] {"CanalId"});
            DropIndex("dbo.PresupuestosUsuariosTiendasNiveles", new[] {"PresupuestoUsuarioTiendaId"});
            DropIndex("dbo.PresupuestosUsuariosTiendas", new[] {"TiendaId"});
            DropIndex("dbo.PresupuestosUsuariosTiendas", new[] {"UsuarioId"});
            DropIndex("dbo.PresupuestosUsuariosAusencias", new[] {"UsuarioId"});
            DropIndex("dbo.Usuarios", new[] {"EmpresaId"});
            DropIndex("dbo.Turnos", new[] {"CanalId"});
            DropIndex("dbo.Turnos", new[] {"EmpresaId"});
            DropIndex("dbo.Canales", new[] {"EmpresaId"});
            DropTable("dbo.PresupuestosUsuariosVacaciones");
            DropTable("dbo.PresupuestosUsuariosTiendasTurnosHoras");
            DropTable("dbo.Tiendas");
            DropTable("dbo.PresupuestosUsuariosTiendasNiveles");
            DropTable("dbo.PresupuestosUsuariosTiendas");
            DropTable("dbo.PresupuestosUsuariosAusencias");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Turnos");
            DropTable("dbo.Empresas");
            DropTable("dbo.Canales");
        }
    }
}