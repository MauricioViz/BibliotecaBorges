using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BiblioAPI.Models;

public partial class BiblioBD : DbContext
{
    public BiblioBD()
    {
    }

    public BiblioBD(DbContextOptions<BiblioBD> options)
        : base(options)
    {
    }

    public virtual DbSet<TbAutor> TbAutors { get; set; }

    public virtual DbSet<TbAutorLibro> TbAutorLibros { get; set; }

    public virtual DbSet<TbEditorial> TbEditorials { get; set; }

    public virtual DbSet<TbEditorialLibro> TbEditorialLibros { get; set; }

    public virtual DbSet<TbLibro> TbLibros { get; set; }

    public virtual DbSet<TbPrestamo> TbPrestamos { get; set; }

    public virtual DbSet<TbPrestamoLibro> TbPrestamoLibros { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=VERSAH23-PC\\SQLEXPRESS;Initial Catalog=BibliotecaBD;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbAutor>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__TB_AUTOR__9AE8206A6D115148");

            entity.ToTable("TB_AUTOR");

            entity.Property(e => e.IdAutor)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idAutor");
            entity.Property(e => e.FechaNacimiento)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("fechaNacimiento");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.NomAutor)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nomAutor");
        });

        modelBuilder.Entity<TbAutorLibro>(entity =>
        {
            entity.HasKey(e => e.IdRel).HasName("PK__TB_AUTOR__3C8791D2E536C455");

            entity.ToTable("TB_AUTOR_LIBRO");

            entity.Property(e => e.IdRel)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idRel");
            entity.Property(e => e.IdAutor)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idAutor");
            entity.Property(e => e.IdLibro)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idLibro");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.TbAutorLibros)
                .HasForeignKey(d => d.IdAutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_AUTOR___idAut__29221CFB");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.TbAutorLibros)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_AUTOR___idLib__2A164134");
        });

        modelBuilder.Entity<TbEditorial>(entity =>
        {
            entity.HasKey(e => e.IdEditorial).HasName("PK__TB_EDITO__9DF182DB3EE0BD4C");

            entity.ToTable("TB_EDITORIAL");

            entity.Property(e => e.IdEditorial)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idEditorial");
            entity.Property(e => e.DirEditorial)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("dirEditorial");
            entity.Property(e => e.NomEditorial)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nomEditorial");
            entity.Property(e => e.TlfEditorial)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("tlfEditorial");
        });

        modelBuilder.Entity<TbEditorialLibro>(entity =>
        {
            entity.HasKey(e => e.IdRel).HasName("PK__TB_EDITO__3C8791D27D143657");

            entity.ToTable("TB_EDITORIAL_LIBRO");

            entity.Property(e => e.IdRel)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idRel");
            entity.Property(e => e.IdEditorial)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idEditorial");
            entity.Property(e => e.IdLibro)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idLibro");

            entity.HasOne(d => d.IdEditorialNavigation).WithMany(p => p.TbEditorialLibros)
                .HasForeignKey(d => d.IdEditorial)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_EDITOR__idEdi__2CF2ADDF");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.TbEditorialLibros)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_EDITOR__idLib__2DE6D218");
        });

        modelBuilder.Entity<TbLibro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__TB_LIBRO__5BC0F02647CD0F43");

            entity.ToTable("TB_LIBRO");

            entity.Property(e => e.IdLibro)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idLibro");
            entity.Property(e => e.EstLibro)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estLibro");
            entity.Property(e => e.FecPublic)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("fecPublic");
            entity.Property(e => e.GeneroLibro)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("generoLibro");
            entity.Property(e => e.TituloLibro)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tituloLibro");
        });

        modelBuilder.Entity<TbPrestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamo).HasName("PK__TB_PREST__A4876C133FD28143");

            entity.ToTable("TB_PRESTAMO");

            entity.Property(e => e.IdPrestamo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idPrestamo");
            entity.Property(e => e.EstPrestamo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estPrestamo");
            entity.Property(e => e.FecDevolucion)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("fecDevolucion");
            entity.Property(e => e.FecPrestamo)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("fecPrestamo");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idUsuario");
            entity.Property(e => e.MultaPrestamo).HasColumnName("multaPrestamo");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TbPrestamos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_PRESTA__idUsu__72C60C4A");
        });

        modelBuilder.Entity<TbPrestamoLibro>(entity =>
        {
            entity.HasKey(e => e.IdRel).HasName("PK__TB_PREST__3C8791D2505C136F");

            entity.ToTable("TB_PRESTAMO_LIBRO");

            entity.Property(e => e.IdRel)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idRel");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdLibro)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idLibro");
            entity.Property(e => e.IdPrestamo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idPrestamo");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.TbPrestamoLibros)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_PRESTA__idLib__3D2915A8");

            entity.HasOne(d => d.IdPrestamoNavigation).WithMany(p => p.TbPrestamoLibros)
                .HasForeignKey(d => d.IdPrestamo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TB_PRESTA__idPre__3C34F16F");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__TB_USUAR__645723A6F095A732");

            entity.ToTable("TB_USUARIO");

            entity.Property(e => e.IdUsuario)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("idUsuario");
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("correoUsuario");
            entity.Property(e => e.NomUsuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nomUsuario");
            entity.Property(e => e.PassUsuario)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("passUsuario");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tipoUsuario");
            entity.Property(e => e.TlfUsuario)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("tlfUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
