﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehicleTracking.Tracking.Persistance.Context;

namespace VehicleTracking.Tracking.Persistance.Migrations
{
    [DbContext(typeof(TrackingDbContext))]
    partial class TrackingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VehicleTracking.Tracking.Domain.Entities.Location", b =>
                {
                    b.Property<long>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateEntered")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayName")
                        .HasColumnType("NVARCHAR(350)");

                    b.Property<DateTimeOffset>("LastDateModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<string>("Licence")
                        .HasColumnType("NVARCHAR(60)");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("PlaceId")
                        .HasColumnType("NVARCHAR(40)");

                    b.Property<long>("VehicleId")
                        .HasColumnType("bigint");

                    b.HasKey("LocationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("VehicleTracking.Tracking.Domain.Entities.TrackingHistory", b =>
                {
                    b.Property<long>("TrackingHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateEntered")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayName")
                        .HasColumnType("NVARCHAR(350)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<string>("Licence")
                        .HasColumnType("NVARCHAR(60)");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("PlaceId")
                        .HasColumnType("NVARCHAR(40)");

                    b.Property<long>("VehicleId")
                        .HasColumnType("bigint");

                    b.HasKey("TrackingHistoryId");

                    b.ToTable("TrackingHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
